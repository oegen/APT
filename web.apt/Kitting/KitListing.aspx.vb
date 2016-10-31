Imports aptBusinessLogic

Partial Class Kitting_KitListing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            If IsNumeric(Request.QueryString("projectId")) Then
                ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
                ctrlKitListing.ProjectId = Request.QueryString("projectId")
                hypKit.NavigateUrl = String.Format("~/kitting/kit.aspx?projectId={0}", Request.QueryString("projectId"))
                hypKitBrief.NavigateUrl = String.Format("~/kitting/kittingbrief.aspx?projectId={0}", Request.QueryString("projectId"))
                ctrlSubNavProject.ProjectId = Request.QueryString("projectId")

            End If

            If KitManager.HasKitBeenFinalised(Request.QueryString("projectId")) = True Then
                hypKit.Visible = False
                hypKitBrief.Visible = False
            End If

        End If

        ' this is outside of the postback block because a user can delete a kit, so it should check this then
        If KitManager.GetKitsByProject(Request.QueryString("projectId")).Count = 0 Then
            ' If the project has not had a kit added to it yet then it can't complete the final kitting brief
            hypKitBrief.Visible = False
        End If

    End Sub

End Class
