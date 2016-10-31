'----------------------------------------------------------------------------------------------
' Filename    : Timesheet.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Timesheets_Timesheet
    Inherits System.Web.UI.UserControl

    Public Event TimesheetEntrySuccess As EventHandler
    Public Event TimesheetEntryFailure As EventHandler
    Private TimeEnteredString As String = "You have entered {0} hour(s) and {1} minutes of work for this date"

#Region "Properties"

    Public Property TimesheetId As Integer
        Get
            Return ViewState(Me.UniqueID & "_timesheetId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_timesheetId") = value
        End Set
    End Property

    Public Property UserId As Integer
        Get
            Return ViewState(Me.UniqueID & "_userId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_userId") = value
        End Set
    End Property

    Public ReadOnly Property HourID As String
        Get
            Return txtHourSpent.TextBoxClientID
        End Get
    End Property

    Public ReadOnly Property MinuteID As String
        Get
            Return txtMinuteSpent.TextBoxClientID
        End Get
    End Property

    Public Property AutoEntryHour As Integer
        Get
            Return ViewState(Me.UniqueID & "_averageHour")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_averageHour") = value
        End Set
    End Property

    Public Property AutoEntryMinute As Integer
        Get
            Return ViewState(Me.UniqueID & "_averageMinute")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_averageMinute") = value
        End Set
    End Property

    Private Property ElementId As Integer
        Get
            Return ViewState("elementId")
        End Get
        Set(value As Integer)
            ViewState("elementId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        plcBindingFix.DataBind()
        If Page.IsPostBack = False Then
            BindProjects()
            BindTimesheetReasons()
            LoadTimesheet()
        End If

    End Sub

    Protected Sub ddlProject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProject.SelectedIndexChanged
        ctrlElementTimeSheetListing.ProjectId = ddlProject.SelectedValue
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            SaveTimesheet()
        End If

    End Sub

    Protected Sub chkProjectLevel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkProjectLevel.CheckedChanged
        plcProjectLevelTimeInfo.Visible = chkProjectLevel.Checked
        ctrlElementTimeSheetListing.Visible = Not chkProjectLevel.Checked
        plcSave.Visible = chkProjectLevel.Checked
        ctrlElementTimeSheetListing.WorkDateTime = txtDateOfWork.SelectedDate
    End Sub

    Protected Sub txtDateOfWork_DateSelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateOfWork.DateSelectionChanged

        Dim enteredHours As Integer = 0
        Dim enteredMinutes As Integer = 0

        TimesheetManager.GetTotalTimeEnteredOnDate(txtDateOfWork.SelectedDate, SessionManager.LoggedInUserId, enteredHours, enteredMinutes)

        If enteredHours = 0 AndAlso enteredMinutes = 0 Then
            lblTimeEntered.Text = "You have not entered any times in for this date"
        Else
            lblTimeEntered.Text = String.Format(TimeEnteredString, enteredHours, enteredMinutes)
        End If

        ctrlElementTimeSheetListing.WorkDateTime = txtDateOfWork.SelectedDate

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindTimesheetReasons()

        Dim timesheetReasons As List(Of TimesheetReason) = TimesheetManager.GetActiveTimesheetReasons()
        ddlReason.BindDataToDropDown(timesheetReasons, "ID", "ReasonText", "Reason")

    End Sub

    Private Sub BindProjects()

        Dim userProjects As ArrayList = ProjectManager.GetProjectsForDropdownBinding(ProjectManager.GetProjectsByUser(UserId))

        If userProjects.Count = 0 Then
            pnlElementDetails.Visible = False
        End If

        ddlProject.BindDataToDropDown(userProjects, "ID", "Name", "project")
        'liAutoPopulate.Visible = False

    End Sub

    Private Sub LoadTimesheet()

        txtDateOfWork.SelectedDate = Date.Today

        If TimesheetId <> 0 Then

            ' If we're loading a timesheet we don't want the user changing the level of things or the project
            ddlProject.Enabled = False
            liProjectLevel.Visible = False

            Dim loadTimesheet As Timesheet = TimesheetManager.GetTimesheet(TimesheetId)

            If loadTimesheet IsNot Nothing AndAlso loadTimesheet.User.ID = SessionManager.LoggedInUserId Then

                'Only let the user edit the timesheet if they were the ones that entered it

                'BindProjectElements()

                With loadTimesheet
                    txtHourSpent.Text = .HourSpent
                    txtMinuteSpent.Text = .MinutesSpent
                    ddlReason.SelectedValue = .Reason.ID
                    txtDateOfWork.SelectedDate = .DateOfWork

                    If IsCurrentEntryProjectLevel(.ContextEntityId) Then
                        ddlProject.SelectedValue = .EntityParentId
                    Else
                        liElement.Visible = True
                        Dim loadElement As Element = ElementManager.GetElement(.EntityParentId)
                        ddlProject.SelectedValue = loadElement.Project.ID
                        chkProjectLevel.Checked = False
                        lblElement.InnerText = loadElement.TimesheetsDisplayString
                        ElementId = loadElement.ID
                        'BindProjectElements()
                        'ddlElement.SelectedValue = .EntityParentId
                    End If
                End With
            Else
                TimesheetId = 0
            End If
        End If

    End Sub

    Private Sub SaveTimesheet()

        Dim saveTimesheet As New Timesheet

        If TimesheetId <> 0 Then
            saveTimesheet = TimesheetManager.GetTimesheet(TimesheetId)
        End If

        With saveTimesheet
            .HourSpent = txtHourSpent.Text
            .MinutesSpent = txtMinuteSpent.Text
            .Reason = TimesheetManager.GetTimesheetReason(ddlReason.SelectedValue)
            .User = UserManager.GetUser(SessionManager.LoggedInUserId)
            .DateOfWork = txtDateOfWork.SelectedDate

            If chkProjectLevel.Checked Then
                .ContextEntityId = AppSettingsGet.EntityProjectId
                .EntityParentId = ddlProject.SelectedValue
            Else
                ' This is only used for editing elements really
                .ContextEntityId = AppSettingsGet.EntityElementId
                .EntityParentId = ElementId
            End If

        End With

        TimesheetManager.SaveTimesheet(saveTimesheet)
        TimesheetId = 0
        RaiseEvent TimesheetEntrySuccess(Me, New EventArgs())

    End Sub

    Private Function IsCurrentEntryProjectLevel(ByVal contextEntityId As Integer) As Boolean

        If contextEntityId = AppSettingsGet.EntityProjectId Then
            Return True
        End If

        Return False

    End Function

#End Region

    Protected Sub ctrlElementTimeSheetListing_TimesheetElementEntrySuccess(sender As Object, e As System.EventArgs) Handles ctrlElementTimeSheetListing.TimesheetElementEntrySuccess
        RaiseEvent TimesheetEntrySuccess(Me, New EventArgs())
    End Sub

End Class

