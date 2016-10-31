Imports System.Collections.Generic
Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities
Imports System.Globalization

Partial Class Controls_Reserve_Time_ReserveTime
    Inherits System.Web.UI.UserControl

#Region "Constants"

    Public Const GET_HOURS_USED_SCRIPT As String = "<script type=""text/javascript"">function GetHoursUsed{0}() {{return {1};}}</script>"
    Public Const GET_AVAILABLE_HOURS_SCRIPT As String = "<script type=""text/javascript"">function GetTotalAvailableHours{0}() {{return {1};}}</script>"
    Public Const PROGRESS_BAR_DIV As String = "<span class=""progressBar{0}"" id=""pb{0}"">0%</span>"
    Public Const PROGRESS_BAR_STYLING As String = "<script type=""text/javascript"">$(document).ready(function () {{$(""#pb{0}"").progressBar({{steps: 50,stepDuration: 1000, max: 100, showText: false, textFormat: 'percentage', width: 240, height: 12, callback: null, boxImage: '../App_Themes/images/progressbar.gif', barImage: {{ 0: '../App_Themes/_default/images/progressbg_green.gif', 25: '../App_Themes/_default/images/progressbg_yellow.gif', 50: '../App_Themes/_default/images/progressbg_orange.gif', 75: '../App_Themes/_default/images/progressbg_red.gif' }} }} ) }}); </script>"
    Public Const REMAINING_LABEL As String = "<label id=""lblRemainingHours{0}"">Enter hours to display notification.</label>"
    Public Const OVERSPILL_LABEL As String = "<label id=""lblOverspill{0}"" style=""display: none;""></label>"

    Public Const SCRIPT_TAG_FORMAT_STRING As String = "<script type=""text/javascript"">{0}</script>"
    Public Const GET_WORKING_HOURS_SCRIPT As String = "function GetWorkingWeekHours() {{ return {0}; }}"
    Public Const GET_TOTAL_ARTWORKERS_SCRIPT As String = "function GetTotalArtworkers() {{ return {0}; }}"

#End Region

#Region "Private Fields"

    Private weekCount As Integer = 1

#End Region

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
        End Set
    End Property

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

    Public Event TaskCompleted As EventHandler

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        plcBindingFix.DataBind()
        plcBindingFix2.DataBind()
        If Page.IsPostBack = False Then
            dtpStartWeek.SelectedDate = Date.Now

            SetupPage()
            ' Page.RegisterStartupScript("Page_Load", "<script type=""text/javascript"">SetAllProgressBars(GetProjectHoursTextValue(), GetNumWeeksTextValue(), GetNumArtworkersTextValue());</script>")
        End If

    End Sub

    Protected Sub lnkRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefresh.Click
        Dim gregCal As New GregorianCalendar
        Dim weekNum As Integer = gregCal.GetWeekOfYear(dtpStartWeek.SelectedDate, CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)

        If dtpStartWeek.SelectedDate.HasValue Then
            Dim yearNum As Integer = dtpStartWeek.SelectedDate.Value.Year

            WeekNumber = weekNum
            YearNumber = yearNum

            LoadReserveTime()
        End If
    End Sub

    Protected Sub rptrProgressBars_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrProgressBars.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim reservedWeek As ReserveWeek = CType(e.Item.DataItem, ReserveWeek)
            Dim lblWeek As Label = CType(e.Item.FindControl("lblWeek"), Label)
            Dim ltlProgressBar As Literal = CType(e.Item.FindControl("ltlProgressBar"), Literal)
            Dim ltlRemainingLabel As Literal = CType(e.Item.FindControl("ltlRemainingLabel"), Literal)
            Dim ltlOverspillLabel As Literal = CType(e.Item.FindControl("ltlOverspillLabel"), Literal)

            lblWeek.Text = String.Format("Week : {0}", reservedWeek.WeekNumber)
            ltlProgressBar.Text = String.Format(PROGRESS_BAR_DIV, weekCount)
            ltlRemainingLabel.Text = String.Format(REMAINING_LABEL, weekCount)
            ltlOverspillLabel.Text = String.Format(OVERSPILL_LABEL, weekCount)

            weekCount += 1
        End If
    End Sub

    Protected Sub rptrJavascript_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrJavascript.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim reservedWeek As ReserveWeek = CType(e.Item.DataItem, ReserveWeek)
            Dim ltlGetHoursScript As Literal = CType(e.Item.FindControl("ltlGetHoursScript"), Literal)
            Dim ltlGetAvailableHoursScript As Literal = CType(e.Item.FindControl("ltlGetAvailableHoursScript"), Literal)
            Dim ltlProgressBarDefinition As Literal = CType(e.Item.FindControl("ltlProgressBarDefinition"), Literal)

            ltlGetHoursScript.Text = String.Format(GET_HOURS_USED_SCRIPT, weekCount, reservedWeek.UsedHours)
            ltlGetAvailableHoursScript.Text = String.Format(GET_AVAILABLE_HOURS_SCRIPT, weekCount, reservedWeek.AvailableHours)
            ltlProgressBarDefinition.Text = String.Format(PROGRESS_BAR_STYLING, weekCount)

            weekCount += 1
        End If
    End Sub

    Protected Sub txtNumWeeks_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumWeeks.TextChanged
        LoadReserveTime()
    End Sub

    Protected Sub dtpStartWeek_DateSelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpStartWeek.DateSelectionChanged
        Dim calender As New GregorianCalendar
        Dim dateSelected As Date = dtpStartWeek.SelectedDate.Value

        WeekNumber = calender.GetWeekOfYear(dateSelected, CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        If Page.IsValid = True Then
            SaveReservedTime()
        End If
    End Sub

    Protected Sub ctrlElementEstimations_UseTotalTime(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlElementEstimations.UseTotalTime
        txtProjectHours.Text = ctrlElementEstimations.TotalTime
    End Sub

#End Region

#Region "Private Implemenation"

    Private Sub SetupPage()
        ' Get total amount of reserved time for the current project
        Dim currentProject As Project = ProjectManager.GetProject(ProjectId)

        If currentProject IsNot Nothing Then

            Dim startWeek As Integer
            Dim startYear As Integer
            Dim endWeek As Integer
            Dim endYear As Integer
            Dim totalHoursUsed As Integer = ReserveTimeManager.GetTotalHoursReservedForProject(currentProject.ID, startWeek, startYear, endYear, endWeek)
            Dim freelancerHours As Integer = ReserveTimeManager.GetTotalFreelancerTimeForProject(currentProject.ID)

            If totalHoursUsed > 0 Then
                Dim startDate As Date = modDates.FirstDateOfWeek(startYear, startWeek, Globalization.CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)
                Dim endDate As Date = modDates.FirstDateOfWeek(endYear, endWeek, Globalization.CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)

                If freelancerHours > 0 Then
                    ltlFreelancersRequired.Text = String.Format("There are a total <em>{0} hours</em> required from freelancers during the time of this project.", _
                                                                freelancerHours)
                End If

                If startDate = endDate Then
                    ltlDescription.Text = String.Format("This project already has <em>{0} hours</em> reserved on {1}.", _
                                                        totalHoursUsed, startDate.ToString("dd/MM/yyyy"))
                Else
                    ltlDescription.Text = String.Format("This project already has <em>{0} hours</em> reserved between the dates <em>{1}</em> and <em>{2}</em>.", _
                                                        totalHoursUsed, startDate.ToString("dd/MM/yyyy"), endDate.ToString("dd/MM/yyyy"))
                End If

            End If

            ctrlElementEstimations.ProjectId = currentProject.ID
        End If

        ' If the brief has been finalised then display the element estimations, otherwise display the message highlighting that the
        ' element estimations will be displayed if the brief is finalised.
        'If WorkflowManager.IsTransitionComplete(AppSettingsGet.FinaliseAPTBriefTransitionID, ProjectId) _
        '    OrElse WorkflowManager.IsTransitionComplete(AppSettingsGet.FinaliseAPTBriefTransitionNonBDID, ProjectId) Then

        If WorkflowManager.HasProjectBriefBeenFinalised(currentProject.ID) Then
            ctrlElementEstimations.Visible = True
            pcFinaliseBriefIncomplete.Visible = False
        Else
            ctrlElementEstimations.Visible = False
            pcFinaliseBriefIncomplete.Visible = True
        End If

    End Sub


    Public Sub LoadReserveTime()
        Dim reservedWeeksRequested As List(Of ReserveWeek) = ReserveTimeManager.GetReserveWeekObjects(WeekNumber, CType(txtNumWeeks.Text, Integer), _
                                                                                                      YearNumber)
        weekCount = 1

        rptrJavascript.DataSource = reservedWeeksRequested
        rptrJavascript.DataBind()

        weekCount = 1

        rptrProgressBars.DataSource = reservedWeeksRequested
        rptrProgressBars.DataBind()

        WriteArtworkerTotalsAndWorkingHours()

    End Sub

    Private Sub WriteArtworkerTotalsAndWorkingHours()
        Dim script As String = ""
        Dim workingWeekHours As Decimal = WorkingWeekManager.GetWorkingWeekHours()
        Dim totalArtworkers As Integer = AppSettingsGet.NumArtworkers
        Dim getWorkingWeekHoursScript As String = String.Format(GET_WORKING_HOURS_SCRIPT, workingWeekHours)
        Dim getTotalArtworkersScript As String = String.Format(GET_TOTAL_ARTWORKERS_SCRIPT, totalArtworkers)
        Dim combineBothFunctions As String = String.Format("{0}{1}", getWorkingWeekHoursScript, getTotalArtworkersScript)

        script = String.Format(SCRIPT_TAG_FORMAT_STRING, combineBothFunctions)

        ltlArtworkersTotalAndHoursScript.Text = script
    End Sub

    Private Sub SaveReservedTime()
        Dim totalNumberOfHours As Integer = txtProjectHours.Text
        Dim numWeeks As Integer = txtNumWeeks.Text
        Dim numArtworkers As Integer = txtNumArtworkers.Text

        ReserveTimeManager.CreateReservedTimeItems(WeekNumber, YearNumber, totalNumberOfHours, numWeeks, _
                                                   numArtworkers, ProjectId, SessionManager.LoggedInUserId)

        RaiseEvent TaskCompleted(Me, New EventArgs())

        SetupPage()
    End Sub

#End Region

End Class
