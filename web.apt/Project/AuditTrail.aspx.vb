Imports aptBusinessLogic

Partial Class Project_AuditTrail
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            PermissionCheck()

            Dim projectId As Integer

            If Integer.TryParse(Request.QueryString("projectId"), projectId) Then
                ctrlAuditTrail.ProjectId = projectId
                ctrlProjectHeader.ProjectId = projectId
                ctrlSubNavProject.ProjectId = projectId
            End If

        End If

    End Sub

  

#End Region

#Region "Private Implementation"

    Private Sub PermissionCheck()
        If PermissionsManager.CanAccessAuditTrail(SessionManager.LoggedInUserId) = False Then
            Response.Redirect("~/default.aspx")
        End If
    End Sub

#End Region

End Class
