'----------------------------------------------------------------------------------------------
' Filename    : FullTimesheetsListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        29/11/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Controls_Admin_Timesheets_FullTimesheetsListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Public Enum FilterMode

        ALL = 0
        SURNAME = 2
        PROJECT = 4
        WORK_DATE = 6

    End Enum

#End Region

#Region "Properties"

    Private Property CurrentFilterMode As FilterMode
        Get
            If ViewState(Me.UniqueID & "_currentFilterMode") Is Nothing Then
                ViewState(Me.UniqueID & "_currentFilterMode") = FilterMode.ALL
            End If

            Return ViewState(Me.UniqueID & "_currentFilterMode")
        End Get
        Set(ByVal value As FilterMode)
            ViewState(Me.UniqueID & "_currentFilterMode") = value
            BindTimesheets()
        End Set
    End Property

    Private Property Surname As String
        Get
            Return ViewState(Me.UniqueID & "_surname")
        End Get
        Set(ByVal value As String)
            ViewState(Me.UniqueID & "_surname") = value
        End Set
    End Property

    Private Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

    Private Property WorkDate As Date
        Get
            Return ViewState(Me.UniqueID & "_workDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.UniqueID & "_workDate") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub grdvTimeSheets_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvTimeSheets.PageIndexChanging
        grdvTimeSheets.PageIndex = e.NewPageIndex
        BindTimesheets()
    End Sub

#End Region

#Region "Public Methods"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            CurrentFilterMode = FilterMode.ALL
            BindTimesheets()
        End If

    End Sub

    Public Sub SearchTimesheetsBySurname(ByVal searchSurname As String)
        grdvTimeSheets.PageIndex = 0
        Surname = searchSurname
        CurrentFilterMode = FilterMode.SURNAME
    End Sub

    Public Sub SearchTimesheetsByProjectId(ByVal searchId As Integer)
        grdvTimeSheets.PageIndex = 0
        ProjectId = searchId
        CurrentFilterMode = FilterMode.PROJECT
    End Sub

    Public Sub SearchTimesheetsByDate(ByVal searchDate As Date)
        grdvTimeSheets.PageIndex = 0
        WorkDate = searchDate
        CurrentFilterMode = FilterMode.WORK_DATE
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindTimesheets()

        Dim bindTimesheet As List(Of Timesheet)

        Select Case CurrentFilterMode
            Case FilterMode.ALL
                bindTimesheet = TimesheetManager.GetTimesheetsByLatestEntryDates()
            Case FilterMode.PROJECT
                bindTimesheet = TimesheetManager.GetTimeSheetByProjectId(ProjectId)
            Case FilterMode.SURNAME
                bindTimesheet = TimesheetManager.GetTimesheetByUserSurname(Surname)
            Case FilterMode.WORK_DATE
                bindTimesheet = TimesheetManager.GetTimeSheetsByWorkDate(WorkDate)
        End Select

        If bindTimesheet.Count > 0 Then
            grdvTimeSheets.DataSource = bindTimesheet
            grdvTimeSheets.DataBind()
        Else
            plcEmpty.Visible = True
            plcTimesheet.Visible = False
            DisplayNoResultMessage()
        End If

    End Sub

    Private Sub DisplayNoResultMessage()

        Select Case CurrentFilterMode
            Case FilterMode.ALL
                ltlNoResult.Text = "No timesheet entries have been made yet"
            Case FilterMode.PROJECT
                ltlNoResult.Text = "No timesheet entries have been made for this project yet"
            Case FilterMode.WORK_DATE
                ltlNoResult.Text = "No timesheet entries have been on this date"
            Case FilterMode.SURNAME
                ltlNoResult.Text = "No timesheets for a user with this surname could be found"
        End Select

    End Sub

#End Region

    Protected Sub grdvTimeSheets_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvTimeSheets.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim bindTimesheet As Timesheet = e.Row.DataItem
            Dim ltlProject As Literal = e.Row.FindControl("ltlProject")
            Dim ltlElement As Literal = e.Row.FindControl("ltlElement")
            Dim ltlName As Literal = e.Row.FindControl("ltlName")
            Dim ltlEntryDate As Literal = e.Row.FindControl("ltlEntryDate")
            Dim ltlDate As Literal = e.Row.FindControl("ltlDate")
            Dim ltlTimeSpent As Literal = e.Row.FindControl("ltlTimeSpent")
            Dim ltlReason As Literal = e.Row.FindControl("ltlReason")

            Dim tmpProject As Project

            ltlElement.Text = "N/A"

            If bindTimesheet.ContextEntityId = AppSettingsGet.EntityProjectId Then
                tmpProject = ProjectManager.GetProject(bindTimesheet.EntityParentId)
            Else
                Dim tmpElement As Element = ElementManager.GetElement(bindTimesheet.EntityParentId)
                tmpProject = tmpElement.Project
                ltlElement.Text = tmpElement.DisplayString
            End If

            ltlProject.Text = tmpProject.AINName
            ltlName.Text = bindTimesheet.User.FullName
            ltlEntryDate.Text = FormatHelper.FormatDateWithoutTime(bindTimesheet.EntryDate.ToString)
            ltlDate.Text = FormatHelper.FormatDateWithoutTime(bindTimesheet.DateOfWork.ToString)
            ltlTimeSpent.Text = bindTimesheet.TimeSpentShortHand

            If bindTimesheet.Reason IsNot Nothing Then
                ltlReason.Text = bindTimesheet.Reason.ReasonText
            Else
                ltlReason.Text = "N/A"
            End If
        End If

    End Sub

End Class

