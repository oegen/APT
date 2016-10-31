
Partial Class Admin_Lists_Node
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            If IsNumeric(Request.QueryString("nodeId")) Then
                ctrlNode.NodeId = Request.QueryString("nodeId")
            End If

            If IsNumeric(Request.QueryString("aptListId")) Then
                ctrlNode.ListId = Request.QueryString("aptListId")
                hypAddNode.NavigateUrl = String.Format("~/Admin/Lists/NodeListing.aspx?aptListId={0}", Request.QueryString("aptListId"))
            End If

        End If

    End Sub

    Protected Sub ctrlNode_NodeSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlNode.NodeSaveSuccess
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("List Item has been saved successfully")
    End Sub

End Class

