Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities
Imports System.Collections.Generic

Partial Class Controls_Reserve_Time_ViewReservedTime
    Inherits System.Web.UI.UserControl

#Region "Constants"

    Private Const EDIT_CLICKED As String = "edit_click"
    Private Const DELETE_CLICKED As String = "delete_click"
    Private Const ACTIONS_COLUMN As Integer = 4

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

    Protected Sub gvReservedTime_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvReservedTimes.RowDataBound
        If e.Row.DataItem IsNot Nothing Then
            Dim reservedItem As ReservedTime = CType(e.Row.DataItem, ReservedTime)
            Dim lblWeekNum As Label = CType(e.Row.FindControl("lblWeekNum"), Label)
            Dim ltlDuration As Literal = CType(e.Row.FindControl("ltlDuration"), Literal)
            Dim ltlNumArtworkers As Literal = CType(e.Row.FindControl("ltlNumArtworkers"), Literal)
            Dim ltlReservedBy As Literal = CType(e.Row.FindControl("ltlReservedBy"), Literal)
            Dim weekDate As Date = modDates.FirstDateOfWeek(reservedItem.Year, reservedItem.WeekNumber, _
                                                            Globalization.CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)

            lblWeekNum.Text = String.Format("{0} (starting {1})", reservedItem.WeekNumber, weekDate.ToString("dd/MM/yyyy"))
            ltlDuration.Text = reservedItem.Duration
            ltlNumArtworkers.Text = reservedItem.NumArtworkers
            ltlReservedBy.Text = reservedItem.User.FullName

            If CanEditDelete Then
                Dim lnkEdit As LinkButton = CType(e.Row.FindControl("lnkEdit"), LinkButton)
                Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)

                lnkEdit.CommandName = EDIT_CLICKED
                lnkEdit.CommandArgument = reservedItem.ID
                lnkDelete.CommandName = DELETE_CLICKED
                lnkDelete.CommandArgument = reservedItem.ID
            End If
            
        End If
    End Sub

    Protected Sub gvReservedTimes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvReservedTimes.RowCommand
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
        plcReserveTime.Visible = True
        plcError.Visible = False

        Dim reservedTimeList As List(Of ReservedTime) = ReserveTimeManager.GetReservedTimeForProject(projectId)

        If UserManager.UserHasProjectRole(SessionManager.LoggedInUserId, AppSettingsGet.GraphicsCoordinatorID, projectId) Then
            CanEditDelete = True
        Else
            gvReservedTimes.Columns(ACTIONS_COLUMN).Visible = False
        End If

        If reservedTimeList.Count > 0 Then
            gvReservedTimes.DataSource = reservedTimeList
            gvReservedTimes.DataBind()
        Else
            plcError.Visible = True
            plcReserveTime.Visible = False
        End If

    End Sub
#End Region

#Region "Private Implementation"

    Private Sub EditClicked(ByVal reservedItemID As String)
        Response.Redirect(String.Format("~/Reserve Time/EditReserveTime.aspx?reserveTimeId={0}&projectId={1}", reservedItemID, projectId))
    End Sub

    Private Sub DeleteClicked(ByVal reservedItemID As String)

        ReserveTimeManager.RemoveReserveTime(reservedItemID, SessionManager.LoggedInUserId)

        SetupControl()

    End Sub

#End Region

End Class
