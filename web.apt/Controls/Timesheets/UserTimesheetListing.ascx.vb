'----------------------------------------------------------------------------------------------
' Filename    : UserTimesheetListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Controls_TimeSheets_UserTimeSheetListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Public Property UserId As Integer
        Get
            Return ViewState(Me.UniqueID & "_userId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_userId") = value
        End Set
    End Property

    Public Property FilterDate As Nullable(Of DateTime)
        Get
            Return ViewState(Me.UniqueID & "_filterDate")
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            ViewState(Me.UniqueID & "_filterDate") = value
            BindUserTimeSheets()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindUserTimeSheets()
        End If

    End Sub

    Protected Sub grdvTimeSheets_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvTimeSheets.PageIndexChanging

        grdvTimeSheets.PageIndex = e.NewPageIndex
        BindUserTimeSheets()

    End Sub

    Protected Sub grdvTimeSheets_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvTimeSheets.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpTimesheet As Timesheet = CType(e.Row.DataItem, Timesheet)
            Dim lblName As Label = CType(e.Row.FindControl("lblName"), Label)
            Dim lblTimeTaken As Label = CType(e.Row.FindControl("lblTimeTaken"), Label)
            Dim lblDateOfWork As Label = CType(e.Row.FindControl("lblDateOfWork"), Label)
            Dim hypView As HyperLink = CType(e.Row.FindControl("hypView"), HyperLink)
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)

            If tmpTimesheet.ContextEntityId = AppSettingsGet.EntityProjectId Then
                lblName.Text = ProjectManager.GetProject(tmpTimesheet.EntityParentId).AINName
            Else
                lblName.Text = ElementManager.GetElement(tmpTimesheet.EntityParentId).TimesheetsDisplayString
            End If

            lnkDelete.CommandArgument = tmpTimesheet.ID
            modUtilities.AddConfirmBoxToLinkButton(lnkDelete, "Are you sure you want to remove this timesheet entry?")

            lblTimeTaken.Text = String.Format("{0} hours, {1} minutes", tmpTimesheet.HourSpent, tmpTimesheet.MinutesSpent)
            lblDateOfWork.Text = tmpTimesheet.DateOfWork.ToString("dd/MM/yyyy")
            hypView.NavigateUrl = String.Format(String.Format("~/Timesheet/Timesheet.aspx?timesheetId={0}", tmpTimesheet.ID))

        End If

    End Sub

#End Region

#Region "Public Methods"

    Public Sub BindUserTimeSheets()

        If UserId <> 0 Then
            Dim userTimesheets As List(Of Timesheet)

            If FilterDate IsNot Nothing Then
                userTimesheets = TimesheetManager.GetTimeSheetsByUserAndWorkDate(FilterDate, SessionManager.LoggedInUserId)
            Else
                userTimesheets = TimesheetManager.GetTimesheetsByUser(UserId)
            End If

            If userTimesheets.Count = 0 Then
                pnlNoItems.Visible = True
                pnlTimesheet.Visible = False
            Else
                pnlNoItems.Visible = False
                pnlTimesheet.Visible = True
            End If

            grdvTimeSheets.DataSource = userTimesheets
            grdvTimeSheets.DataBind()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Protected Sub lnkDelete_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim timeSheetId As Integer = e.CommandArgument
        TimesheetManager.DeleteTimesheet(timeSheetId)
        BindUserTimeSheets()

    End Sub

#End Region

End Class
