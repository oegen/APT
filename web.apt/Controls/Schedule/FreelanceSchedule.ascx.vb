Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Controls_Schedule_FreelanceScheduleItem
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property WeekNumber As Integer
        Get
            Return ViewState("_weekNumber")
        End Get
        Set(ByVal value As Integer)
            ViewState("_weekNumber") = value
        End Set
    End Property

    Public Property YearNumber As Integer
        Get
            Return ViewState("_yearNumber")
        End Get
        Set(ByVal value As Integer)
            ViewState("_yearNumber") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub gvFreelanceTimes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFreelanceTimes.RowDataBound
        If e.Row.DataItem IsNot Nothing Then
            Dim freelanceTime As FreelancerTime = CType(e.Row.DataItem, FreelancerTime)
            Dim lnkProject As HyperLink = CType(e.Row.FindControl("lnkProject"), HyperLink)
            Dim lblAIN As Label = CType(e.Row.FindControl("lblAIN"), Label)
            Dim lblStartWeekNum As Label = CType(e.Row.FindControl("lblStartWeekNum"), Label)
            Dim lblTotalWeeks As Label = CType(e.Row.FindControl("lblTotalWeeks"), Label)
            Dim lblDuration As Label = CType(e.Row.FindControl("lblDuration"), Label)
            Dim lblAvgHoursPerWeek As Label = CType(e.Row.FindControl("lblAvgHoursPerWeek"), Label)
            Dim lblReservedBy As Label = CType(e.Row.FindControl("lblReservedBy"), Label)

            lnkProject.Text = freelanceTime.Project.Name
            lnkProject.NavigateUrl = String.Format("~/Project/ProjectDetails.aspx?projectId={0}", freelanceTime.Project.ID)
            lblAIN.Text = freelanceTime.Project.ID
            lblStartWeekNum.Text = String.Format("Week {0} (starting {1})", freelanceTime.StartWeek, _
                                                 modDates.FirstDateOfWeek(freelanceTime.Year, freelanceTime.StartWeek, Globalization.CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek).ToString("dd/MM/yyyy"))
            lblTotalWeeks.Text = freelanceTime.NumWeeks
            lblDuration.Text = freelanceTime.Duration
            lblAvgHoursPerWeek.Text = IIf(freelanceTime.Duration <> 0 AndAlso freelanceTime.NumWeeks <> 0, freelanceTime.Duration / freelanceTime.NumWeeks, "N/A")
            lblReservedBy.Text = freelanceTime.User.FullName
        End If
    End Sub

#End Region

#Region "Public Methods"

    Public Sub SetupFreelanceTime()

        plcFreelanceTime.Visible = False
        plcEmpty.Visible = False

        Dim freelanceTimes As List(Of FreelancerTime) = ReserveTimeManager.GetFreelanceTimeByWeek(WeekNumber, YearNumber)

        If freelanceTimes.Count > 0 Then
            gvFreelanceTimes.DataSource = freelanceTimes
            gvFreelanceTimes.DataBind()
            plcFreelanceTime.Visible = True
        Else
            plcEmpty.Visible = True
        End If

    End Sub

#End Region

End Class
