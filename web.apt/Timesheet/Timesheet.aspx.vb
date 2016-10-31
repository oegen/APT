'----------------------------------------------------------------------------------------------
' Filename    : Timesheet.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Timesheet_Timesheet
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            PermissionCheck()
            LoadPage()
        End If
    End Sub

    Protected Sub ctrlTimesheet_TimesheetEntrySuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlTimesheet.TimesheetEntrySuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Timesheet entry has been saved successfully")
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadPage()
        Dim timesheetId As Integer

        If Integer.TryParse(Request.QueryString("timesheetId"), timesheetId) Then
            ctrlTimesheet.TimesheetId = timesheetId
        End If

        ctrlTimesheet.UserId = SessionManager.LoggedInUserId

    End Sub

    Private Sub PermissionCheck()
        If PermissionsManager.CanUserAccessTimesheets(SessionManager.LoggedInUserId) = False Then
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

#End Region

End Class
