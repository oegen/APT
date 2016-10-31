'----------------------------------------------------------------------------------------------
' Filename    : ProjectBBCItemsListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Elements_ProjectBBCItemsListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub ctrlBBCItemListing_ProjectBBCItemRemoved(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ctrlBBCItemListing.ProjectBBCItemRemoved
        CType(Me.Master, MasterPage).DisplayConfirmationMessage(String.Format("BBC Item {0} has been removed from the project", e.CommandArgument))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            PermissionCheck()

            hypAddBBCItem.NavigateUrl = String.Format("~/Elements/ProjectBBCItems.aspx?projectId={0}", Request.QueryString("projectid"))
            ctrlProjectHeader.ProjectId = Request.QueryString("projectid")
            ctrlBBCItemListing.ProjectId = Request.QueryString("projectid")
            ctrlSubNavProject.ProjectId = Request.QueryString("projectid")

        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub PermissionCheck()
        If PermissionsManager.CanAddEditBBCItems(SessionManager.LoggedInUserId) = False Then
            hypAddBBCItem.Visible = False
        End If
    End Sub

#End Region

End Class
