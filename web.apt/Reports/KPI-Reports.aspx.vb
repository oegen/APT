Imports System.Collections.Generic
Imports System.Linq
Imports aptEntities
Imports aptBusinessLogic

Partial Class Reports_KPI_Reports
    Inherits System.Web.UI.Page

    Private _timesheetEntries As List(Of Timesheet)
    Private UserList As List(Of AptUser) = UserManager.GetAllUsers()


    Private TotalTimeOnProjects As Integer
    Private Property TimeSheetEntries As List(Of Timesheet)
        Get
            Return _timesheetEntries
        End Get
        Set(value As List(Of Timesheet))
            _timesheetEntries = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            TimeSheetEntries = TimesheetManager.GetAllTimesheets()
            ltlFilter.Text = "All"
            PopulateRepeaters()

            'For Date Range Filter
            dtpBegin.SelectedDate = TimeSheetEntries.OrderBy(Function(doc) doc.EntryDate).First().EntryDate
            dtpEnd.SelectedDate = DateTime.Now.ToShortDateString
            TotalLiterals()
        End If

    End Sub

#Region "Repeaters"

    Protected Sub rptrItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrArtworkers.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim ltlName As Literal = CType(e.Item.FindControl("ltlName"), Literal)
            Dim ltlNoOfProjects As Literal = CType(e.Item.FindControl("ltlNoOfProjects"), Literal)
            Dim ltlPercentShareProjects As Literal = CType(e.Item.FindControl("ltlPercentShareProjects"), Literal)
            Dim ltlActualTime As Literal = CType(e.Item.FindControl("ltlActualTime"), Literal)
            Dim ltlPercentShareTime = CType(e.Item.FindControl("ltlPercentShareTime"), Literal)

            Dim userId = CType(e.Item.DataItem, Integer)
            Dim userTimeSheets = TimeSheetEntries.Where(Function(doc) doc.User.ID = userId).ToList()

            ltlName.Text = GetUser(userId).FullName
            ltlNoOfProjects.Text = GetNumberOfProjectsForUser(userId)
            ltlPercentShareProjects.Text = Math.Round((CType(GetNumberOfProjectsForUser(userId), Double) / GetTotalNumberOfProjects()) * 100, 2)

            Dim userTimeInMinutes = GetAmountOfTimeForUser(userId)
            ltlActualTime.Text = CStr(Fix(userTimeInMinutes / 60)) + " hours " + CStr(userTimeInMinutes Mod 60) + " minutes"
            ltlPercentShareTime.Text = CStr(Math.Round((userTimeInMinutes / TotalTimeOnProjects) * 100, 2))

        End If

    End Sub

    Private Sub PopulateRepeaters()
        TotalTimeOnProjects = GetTotalAmountOfTime()
        Dim distinctUsers As List(Of Integer) = TimeSheetEntries.Select(Function(doc) doc.User.ID).Distinct().ToList()
        rptrArtworkers.DataSource = distinctUsers.Where(Function(doc) UserManager.UserHasGlobalRole(doc, AppSettingsGet.ArtworkerID))
        rptrDesigners.DataSource = distinctUsers.Where(Function(doc) UserManager.UserHasGlobalRole(doc, AppSettingsGet.DesignerID))

        'FreeLance ID needs setting here
        rptrFreeLancers.DataSource = distinctUsers.Where(Function(doc) UserManager.UserHasGlobalRole(doc, 0))

        rptrArtworkers.DataBind()
        rptrDesigners.DataBind()
        'rptrFreeLancers.DataBind()
    End Sub

#End Region

#Region "Events"

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click

        TimeSheetEntries = TimesheetManager.GetAllTimesheets()

        Select Case rlstDateFilter.SelectedValue
            Case "YTD"
                Dim beginningOfCurrentYear As DateTime = Date.Parse("01/01/" + CStr(DateTime.Now.Year))
                TimeSheetEntries = TimeSheetEntries.Where(Function(doc) doc.EntryDate > beginningOfCurrentYear).ToList()
            Case "MAT"
                Dim dateOneYearAgo As DateTime = DateTime.Now.AddYears(-1)
                TimeSheetEntries = TimeSheetEntries.Where(Function(doc) doc.EntryDate > dateOneYearAgo).ToList()
            Case "Date Range"
                TimeSheetEntries = TimeSheetEntries _
                    .Where(Function(doc) doc.EntryDate > dtpBegin.SelectedDate) _
                    .Where(Function(doc) doc.EntryDate < dtpEnd.SelectedDate) _
                    .ToList()
        End Select

        Select Case ddlProjectStatus.SelectedValue
            Case "Completed"
                Dim completed As List(Of Timesheet) = New List(Of Timesheet)
                Dim projectId As Integer = 0
                For Each timesheetEntry As Timesheet In TimeSheetEntries
                    projectId = GetProjectIdFromTimesheetEntry(timesheetEntry)
                    If WorkflowManager.GetLastTokenByProject(projectId) IsNot Nothing Then
                        completed.Add(timesheetEntry)
                    End If
                Next

                TimeSheetEntries = completed

            Case "Archived"
                Dim archived As List(Of Timesheet) = New List(Of Timesheet)
                Dim projectId As Integer = 0
                For Each timesheetEntry In TimeSheetEntries
                    projectId = GetProjectIdFromTimesheetEntry(timesheetEntry)
                    If ProjectManager.GetProject(projectId).Active Then
                        archived.Add(timesheetEntry)
                    End If
                Next

                TimeSheetEntries = archived

            Case "In Progress"
                Dim inProgress As List(Of Timesheet) = New List(Of Timesheet)
                Dim projectId As Integer = 0
                For Each timesheetEntry As Timesheet In TimeSheetEntries
                    projectId = GetProjectIdFromTimesheetEntry(timesheetEntry)
                    If (Not ProjectManager.GetProject(projectId).Active) And _
                       (WorkflowManager.GetLastTokenByProject(projectId) Is Nothing) And _
                       (Not ProjectManager.GetProject(projectId).Stopped) Then

                        inProgress.Add(timesheetEntry)

                    End If
                Next

                TimeSheetEntries = inProgress

            Case "Stopped & Cancelled"
                Dim stopped As List(Of Timesheet) = New List(Of Timesheet)
                Dim projectId As Integer = 0
                For Each timesheetEntry As Timesheet In TimeSheetEntries
                    projectId = GetProjectIdFromTimesheetEntry(timesheetEntry)
                    If ProjectManager.GetProject(projectId).Stopped Then
                        stopped.Add(timesheetEntry)
                    End If
                Next

                TimeSheetEntries = stopped

        End Select

        PopulateRepeaters()
        TotalLiterals()

    End Sub

#End Region

#Region "Getters"

    Private Function GetUser(userId As Integer) As AptUser
        Return UserList.Where(Function(doc) doc.ID = userId).FirstOrDefault()
    End Function

    Private Function GetProjectIdFromTimesheetEntry(timesheetEntry As Timesheet) As Integer
        If (timesheetEntry.ContextEntityId = 1) Then
            Return timesheetEntry.EntityParentId
        ElseIf (timesheetEntry.ContextEntityId = 2) Then
            Return ElementManager.GetElement(timesheetEntry.EntityParentId).Project.ID
        End If

        Return 0
    End Function

    Private Function GetNumberOfProjectsForUser(userId As Integer) As Integer

        Dim userTimesheetEntries As List(Of Timesheet) = TimeSheetEntries.Where(Function(doc) doc.User.ID = userId).ToList()

        Dim projectIds As List(Of Integer) = userTimesheetEntries.Where(Function(e) e.ContextEntityId = 1).Select(Function(e) e.EntityParentId).ToList()
        Dim elementIds As List(Of Integer) = userTimesheetEntries.Where(Function(e) e.ContextEntityId = 2).Select(Function(e) e.EntityParentId).ToList()

        For Each elementId As Integer In elementIds
            projectIds.Add(ElementManager.GetElement(elementId).Project.ID)
        Next

        Return projectIds.Distinct().Count()

    End Function

    Private Function GetTotalNumberOfProjects() As Integer

        Dim projectIds As List(Of Integer) = TimeSheetEntries.Where(Function(e) e.ContextEntityId = 1).Select(Function(e) e.EntityParentId).ToList()
        Dim elementIds As List(Of Integer) = TimeSheetEntries.Where(Function(e) e.ContextEntityId = 2).Select(Function(e) e.EntityParentId).ToList()

        For Each elementId As Integer In elementIds
            projectIds.Add(ElementManager.GetElement(elementId).Project.ID)
        Next

        Return projectIds.Distinct().Count()

    End Function

    Private Function GetAmountOfTimeForUser(userId As Integer) As Integer

        Dim userTimesheetEntries As List(Of Timesheet) = TimeSheetEntries.Where(Function(doc) doc.User.ID = userId).ToList()

        Dim totalHours = 0
        Dim totalMinutes = 0

        For Each userTimesheetEntry As Timesheet In userTimesheetEntries
            totalHours += userTimesheetEntry.HourSpent
            totalMinutes += userTimesheetEntry.MinutesSpent
        Next

        Return (totalHours * 60) + totalMinutes

    End Function

    Private Function GetTotalAmountOfTime() As Integer

        Dim totalHours = 0
        Dim totalMinutes = 0

        For Each timesheetEntry In TimeSheetEntries
            totalHours += timesheetEntry.HourSpent
            totalMinutes += timesheetEntry.MinutesSpent
        Next

        Return (totalHours * 60) + totalMinutes

    End Function

    Private Sub TotalLiterals()
        ltlTotalProjects.Text = GetTotalNumberOfProjects()
        ltlTotalTime.Text = CStr(Fix(GetTotalAmountOfTime() / 60)) + " hours " + CStr(GetTotalAmountOfTime() Mod 60) + " minutes"
    End Sub

#End Region

End Class
