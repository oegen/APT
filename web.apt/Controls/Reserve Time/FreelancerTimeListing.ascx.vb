Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities
Imports System.Collections.Generic

Partial Class Controls_Reserve_Time_FreelancerTimeListing
    Inherits System.Web.UI.UserControl

#Region "Constants"

    Private Const EDIT_CLICKED As String = "edit_click"
    Private Const DELETE_CLICKED As String = "delete_click"
    Private Const ACTIONS_COLUMN As Integer = 5

#End Region

#Region "Properties"
    Public Property projectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
            SetupControl()
        End Set
    End Property

    Public Property CanEditDelete As Boolean
        Get
            Return ViewState("_CanEditDelete")
        End Get
        Set(ByVal value As Boolean)
            ViewState("_CanEditDelete") = value
        End Set
    End Property
#End Region

#Region "Events"

    Protected Sub gvFreelanceTimes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFreelanceTimes.RowDataBound
        If e.Row.DataItem IsNot Nothing Then
            Dim freelancerTime As FreelancerTime = CType(e.Row.DataItem, FreelancerTime)
            Dim lblWeekNum As Label = CType(e.Row.FindControl("lblWeekNum"), Label)
            Dim ltlTotalWeeks As Literal = CType(e.Row.FindControl("ltlTotalWeeks"), Literal)
            Dim ltlDuration As Literal = CType(e.Row.FindControl("ltlDuration"), Literal)
            Dim ltlAvgHoursPerWeek As Literal = CType(e.Row.FindControl("ltlAvgHoursPerWeek"), Literal)
            Dim ltlReservedBy As Literal = CType(e.Row.FindControl("ltlReservedBy"), Literal)
            Dim weekDate As Date = modDates.FirstDateOfWeek(freelancerTime.Year, freelancerTime.StartWeek, _
                                                            Globalization.CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)

            lblWeekNum.Text = String.Format("{0} (starting {1})", freelancerTime.StartWeek, weekDate.ToString("dd/MM/yyyy"))
            ltlTotalWeeks.Text = String.Format("{0} week(s)", freelancerTime.NumWeeks)
            ltlDuration.Text = String.Format("{0} hours", freelancerTime.Duration)
            ltlReservedBy.Text = freelancerTime.User.FullName

            ltlAvgHoursPerWeek.Text = String.Format("{0} hours", Math.Round(Convert.ToDouble(freelancerTime.Duration) / Convert.ToDouble(freelancerTime.NumWeeks), 2))

            If CanEditDelete Then
                Dim lnkEdit As LinkButton = CType(e.Row.FindControl("lnkEdit"), LinkButton)
                Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)

                lnkEdit.CommandName = EDIT_CLICKED
                lnkEdit.CommandArgument = freelancerTime.ID
                lnkDelete.CommandName = DELETE_CLICKED
                lnkDelete.CommandArgument = freelancerTime.ID
            End If
        End If
    End Sub

    Protected Sub gvFreelanceTimes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFreelanceTimes.RowCommand
        Select Case e.CommandName
            Case EDIT_CLICKED
                EditClicked(e.CommandArgument)
            Case DELETE_CLICKED
                DeleteClicked(e.CommandArgument)
        End Select
    End Sub

#End Region

#Region "Public Methods"
    Public Sub SetupControl()
        Dim freelancerTimeList As List(Of FreelancerTime) = ReserveTimeManager.GetAllFreelanceTimes(projectId)

        If UserManager.UserHasProjectRole(SessionManager.LoggedInUserId, AppSettingsGet.GraphicsCoordinatorID, projectId) Then
            CanEditDelete = True
        Else
            gvFreelanceTimes.Columns(ACTIONS_COLUMN).Visible = False
        End If

        If freelancerTimeList.Count > 0 Then
            gvFreelanceTimes.DataSource = freelancerTimeList
            gvFreelanceTimes.DataBind()
            lblErrorMessage.Visible = False
            plcFreeLancerTime.Visible = True
        Else
            plcFreeLancerTime.Visible = False
            lblErrorMessage.Text = "There are no results to display."
            lblErrorMessage.Visible = True
        End If
    End Sub
#End Region

#Region "Private Implementation"

    Private Sub EditClicked(ByVal freelanceItemID As String)

        Response.Redirect(String.Format("~/Reserve Time/EditFreelancerTime.aspx?freelanceTimeId={0}&projectId={1}", freelanceItemID, projectId))

    End Sub

    Private Sub DeleteClicked(ByVal freelanceItemID As String)

        ReserveTimeManager.RemoveFreelanceTime(freelanceItemID, SessionManager.LoggedInUserId)

        SetupControl()

    End Sub

#End Region

End Class
