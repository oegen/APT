Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_Lists_NodeListing
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property ListId As Integer
        Get
            Return ViewState(Me.UniqueID & "_listId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_listId") = value
        End Set
    End Property
    Public Event NodeRemoved As EventHandler

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindNodes()
        End If

    End Sub

    Protected Sub grdvNodeListing_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvNodeListing.PageIndexChanging

        grdvNodeListing.PageIndex = e.NewPageIndex
        BindNodes()

    End Sub

    Protected Sub grdvNodeListing_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvNodeListing.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpNode As ListNode = CType(e.Row.DataItem, ListNode)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim lnkRemove As LinkButton = CType(e.Row.FindControl("lnkRemove"), LinkButton)

            hypEdit.NavigateUrl = String.Format("~/Admin/Lists/Node.aspx?nodeId={0}&aptListId={1}", tmpNode.ID, tmpNode.List.ID)
            lnkRemove.CommandArgument = tmpNode.ID

        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim nodeId As Integer = CType(sender, LinkButton).CommandArgument
        ListManager.RemoveNode(nodeId)
        BindNodes()
        RaiseEvent NodeRemoved(Me, New EventArgs)

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindNodes()

        If ListId <> 0 Then

            Dim nodeList As List(Of ListNode) = ListManager.GetListsNodes(ListId)

            grdvNodeListing.DataSource = nodeList
            grdvNodeListing.DataBind()

        End If

    End Sub

#End Region

End Class
