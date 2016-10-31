Imports aptBusinessLogic

Partial Class Project_ProjectCostings
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("projectId")) Then
                PermissionCheck()
                ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
                ctrlProjectCostings.ProjectId = Request.QueryString("projectId")
                ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
            End If
        End If
    End Sub

    Protected Sub ctrlProjectCostings_SaveProjectCostingsSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlProjectCostings.SaveProjectCostingsSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Project costings has been saved")
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub PermissionCheck()
        If PermissionsManager.CanAccessCostings(SessionManager.LoggedInUserId, Request.QueryString("projectId")) = False Then
            Response.Redirect("~/default.aspx")
        End If
    End Sub

#End Region

End Class
