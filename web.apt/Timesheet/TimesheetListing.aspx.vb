'----------------------------------------------------------------------------------------------
' Filename    : TimesheetListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Timesheet_TimesheetListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            PermissionCheck()
            ctrlTimesheetListing.UserId = SessionManager.LoggedInUserId
        End If
    End Sub

    Protected Sub lnkDateOfWork_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDateOfWork.Click
        If String.IsNullOrEmpty(txtDateOfWork.Text) = False Then
            Dim filterDate As DateTime = DateTime.Today
            DateTime.TryParse(txtDateOfWork.Text, filterDate)
            ctrlTimesheetListing.FilterDate = filterDate
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub PermissionCheck()
        If PermissionsManager.CanUserAccessTimesheets(SessionManager.LoggedInUserId) = False Then
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

#End Region

End Class
