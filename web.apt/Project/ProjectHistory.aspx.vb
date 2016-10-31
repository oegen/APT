Imports aptBusinessLogic

Partial Class Project_ProjectHistory
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            PermissionCheck()

            If IsNumeric(Request.QueryString("projectId")) Then
                ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
                ctrlProjectHistory.ProjectId = Request.QueryString("projectId")
                ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
            End If

        End If

    End Sub

    Private Sub PermissionCheck()

        If PermissionsManager.CanAccessProjectHistory(SessionManager.LoggedInUserId) = False Then
            Response.Redirect("~/default.aspx")
        End If

    End Sub

#End Region

End Class
