
Partial Class Admin_Lists_NodeListing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("aptListId")) Then
                ctrlNodeListing.ListId = Request.QueryString("aptListId")
                hypAddNode.NavigateUrl = String.Format("~/Admin/Lists/Node.aspx?AptListId={0}", Request.QueryString("aptListId"))
            End If
        End If

    End Sub

    Protected Sub ctrlNodeListing_NodeRemoved(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlNodeListing.NodeRemoved
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("Node removal successful")
    End Sub

End Class
