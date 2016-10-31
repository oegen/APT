'----------------------------------------------------------------------------------------------
' Filename    : ProjectBBCItemListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Element_ProjectBBCItemListing
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event ProjectBBCItemRemoved As CommandEventHandler

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
            LoadProjectBBCItems()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub grdvBBCItem_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBBCItem.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim projectBBCItem As ProjectBBCItem = CType(e.Row.DataItem, ProjectBBCItem)
            Dim ltlDescription As Literal = CType(e.Row.FindControl("ltlDescription"), Literal)
            Dim ltlQuantity As Literal = CType(e.Row.FindControl("ltlQuantity"), Literal)
            Dim ltlPackQuantity As Literal = CType(e.Row.FindControl("ltlPackQuantity"), Literal)
            Dim ltlDeliveryDate As Literal = CType(e.Row.FindControl("ltlDeliveryDate"), Literal)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)

            ltlDescription.Text = projectBBCItem.BBCItem.Description
            ltlQuantity.Text = projectBBCItem.Quantity
            ltlPackQuantity.Text = projectBBCItem.PackQuantity
            ltlDeliveryDate.Text = projectBBCItem.DeliveryDate
            lnkDelete.CommandArgument = projectBBCItem.ID

            'TODO: Hyperlink
            hypEdit.NavigateUrl = String.Format("~/Elements/ProjectBBCItems.aspx?projectId={0}&itemid={1}", ProjectId, projectBBCItem.ID)

        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim projectBBCItemId As Integer = e.CommandArgument
        Dim projectBBCItem As ProjectBBCItem = BBCItemManager.GetProjectBBCItem(projectBBCItemId)
        Dim deletionArg As New CommandEventArgs("bbcItem", projectBBCItem.BBCItem.Description)
        BBCItemManager.RemoveBBCItemFromProject(projectBBCItemId, SessionManager.LoggedInUserId)
        RaiseEvent ProjectBBCItemRemoved(Me, deletionArg)
        LoadProjectBBCItems()
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadProjectBBCItems()

        pnlBBCItem.Visible = True
        pnlNoItems.Visible = True

        Dim bbcItems As List(Of ProjectBBCItem) = BBCItemManager.GetProjectBBCItems(ProjectId)

        If bbcItems.Count > 0 Then
            grdvBBCItem.DataSource = bbcItems
            grdvBBCItem.DataBind()
            pnlNoItems.Visible = False
        Else
            pnlBBCItem.Visible = False
        End If

    End Sub

#End Region

End Class
