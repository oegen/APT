'----------------------------------------------------------------------------------------------
' Filename    : LeftNav.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic

Partial Class Controls_Navigation_LeftNav
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Private Const CSS_CLASS As String = "class"

    Private Const ViewProjectsCss As String = "view-projects"
    Private Const ViewTasksCss As String = "view-tasks"
    Private Const ReportsCss As String = "reports"
    Private Const AdminCss As String = "admin"
    Private Const CalenderCss As String = "calendar"
    Private Const TimeSheetCss As String = "time-sheet"

    Private Const ViewProjectsSelectedCss As String = "view-projects-selected"
    Private Const ViewTasksSelectedCss As String = "view-tasks-selected"
    Private Const ReportsSelectedCss As String = "reports-selected"
    Private Const AdminSelectedCss As String = "admin-selected"
    Private Const CalenderSelectedCss As String = "calendar-selected"
    Private Const TimeSheetSelectedCss As String = "time-sheet-selected"

    Public Enum Pages
        ViewProjects = 0
        ViewTasks = 1
        Reports = 2
        Admin = 3
        Schedule = 4
        Timesheet = 5
    End Enum

#End Region

#Region "Properties"

    Public Property ActivePage As Pages
        Get
            Return Session("_activePage")
        End Get
        Set(ByVal value As Pages)
            Session("_activePage") = value
            SetupPageClasses()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            SetupPageClasses()
            CheckUserPermission()
        End If
    End Sub

    Protected Sub lnkViewProjects_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewProjects.Click
        ActivePage = Pages.ViewProjects
        Response.Redirect("~/Project/ProjectListing.aspx")
    End Sub

    Protected Sub lnkViewTasks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewTasks.Click
        ActivePage = Pages.ViewTasks
        Response.Redirect("~/Task/TaskListing.aspx")
    End Sub

    Protected Sub lnkReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReport.Click
        ActivePage = Pages.Reports
        Response.Redirect("~/Reports/KPI-Reports.aspx")
    End Sub

    Protected Sub lnkAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAdmin.Click
        ActivePage = Pages.Admin
        Response.Redirect("~/Admin/Users/UserListing.aspx")
    End Sub

    Protected Sub lnkSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSchedule.Click
        ActivePage = Pages.Schedule
        Response.Redirect("~/Schedule/Schedule.aspx")
    End Sub

    Protected Sub lnkTimesheet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTimesheet.Click
        ActivePage = Pages.Timesheet
        Response.Redirect("~/Timesheet/TimesheetListing.aspx")
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPageClasses()
        SetAllInactive()
        SetCurrentlyActive()
    End Sub

    Private Sub SetAllInactive()
        liViewProjects.Attributes.Add(CSS_CLASS, ViewProjectsCss)
        liViewTasks.Attributes.Add(CSS_CLASS, ViewTasksCss)
        liReport.Attributes.Add(CSS_CLASS, ReportsCss)
        liAdmin.Attributes.Add(CSS_CLASS, AdminCss)
        liSchedule.Attributes.Add(CSS_CLASS, CalenderCss)
        liTimesheet.Attributes.Add(CSS_CLASS, TimeSheetCss)
    End Sub

    Private Sub SetCurrentlyActive()
        Select Case ActivePage
            Case Pages.ViewProjects
                liViewProjects.Attributes.Add(CSS_CLASS, ViewProjectsSelectedCss)
            Case Pages.ViewTasks
                liViewTasks.Attributes.Add(CSS_CLASS, ViewTasksSelectedCss)
            Case Pages.Reports
                liReport.Attributes.Add(CSS_CLASS, ReportsSelectedCss)
            Case Pages.Admin
                liAdmin.Attributes.Add(CSS_CLASS, AdminSelectedCss)
            Case Pages.Schedule
                liSchedule.Attributes.Add(CSS_CLASS, CalenderSelectedCss)
            Case Pages.Timesheet
                liTimesheet.Attributes.Add(CSS_CLASS, TimeSheetSelectedCss)
        End Select
    End Sub

    Private Sub CheckUserPermission()

        ' Admin Check
        If UserManager.UserIsAdmin(SessionManager.LoggedInUserId) = False Then
            ' user is not an admin so hide the admin pages
            liAdmin.Visible = False
        End If

        ' Designer Check

        If PermissionsManager.CanUserAccessTimesheets(SessionManager.LoggedInUserId) = False Then
            ' user is not an admin so hide the admin pages
            liTimesheet.Visible = False
        End If

        If UserManager.UserHasProjectRole(SessionManager.LoggedInUserId, AppSettingsGet.GraphicsCoordinatorID) = False Then
            liSchedule.Visible = False
        End If

    End Sub

#End Region

End Class
