'----------------------------------------------------------------------------------------------
' Filename    : BBCItemListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Admin_BBCItems_BBCItemListing
    Inherits System.Web.UI.UserControl

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindBBCItems()
        End If

    End Sub

    Protected Sub grdvBBCItem_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBBCItem.RowDataBound

        If e.Row.DataItem IsNot Nothing Then
            Dim BBCItem As NewBBCItem = CType(e.Row.DataItem, NewBBCItem)
            Dim ltlBrand As Literal = CType(e.Row.FindControl("ltlBrand"), Literal)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim lnkSetActivity As LinkButton = CType(e.Row.FindControl("lnkSetActivity"), LinkButton)

            hypEdit.NavigateUrl = String.Format("~/Admin/BBCItems/BBCItem.aspx?BBCItemId={0}", BBCItem.ID)
            lnkSetActivity.CommandArgument = BBCItem.ID
            ltlBrand.Text = BBCItem.Brand.Name
        End If

    End Sub

    Protected Sub lnkSetActivity_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim bbcItemId As Integer = e.CommandArgument
        BBCItemManager.SetBBCItemActivity(bbcItemId, False)
        BindBBCItems()
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindBBCItems()
        Dim activeBBCItems As List(Of NewBBCItem) = BBCItemManager.GetAllActiveBBCItem()

        grdvBBCItem.DataSource = activeBBCItems
        grdvBBCItem.DataBind()
    End Sub

#End Region

End Class
