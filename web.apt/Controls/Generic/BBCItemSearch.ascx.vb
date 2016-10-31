'----------------------------------------------------------------------------------------------
' Filename    : BBCItemSearch.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Generic_BBCItemSearch
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Public Event BBCItemSelected As EventHandler

    Private Enum SearchCriteria

        PART_NUMBER = 0
        BRAND = 1
        DESCRIPTION = 2

    End Enum

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Public Property SelectedBBCItemId As Integer
        Get
            Return ViewState(Me.UniqueID & "_selectedBBCItemId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_selectedBBCItemId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearch.Click
        If Page.IsValid Then
            SearchBBCItems()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            BindBBCItemBrands()
        End If
    End Sub

    Protected Sub lnkSelect_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
        ' User has selected a bbc item
        SelectedBBCItemId = e.CommandArgument
        RaiseEvent BBCItemSelected(Me, New EventArgs)
    End Sub

    Protected Sub ddlSearchCriteria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSearchCriteria.SelectedIndexChanged
        HandleViewBySearchOptions()
    End Sub

    Protected Sub gvSearchResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSearchResults.RowDataBound

        If e.Row.DataItem IsNot Nothing Then
            Dim tmpBBCItem As NewBBCItem = CType(e.Row.DataItem, NewBBCItem)
            Dim lnkSelect As LinkButton = CType(e.Row.FindControl("lnkSelect"), LinkButton)

            lnkSelect.CommandArgument = tmpBBCItem.ID
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SearchBBCItems()

        Dim searchResults As List(Of NewBBCItem)
        plcSearchResults.Visible = False
        plcNoResults.Visible = False

        Select Case ddlSearchCriteria.SelectedValue
            Case SearchCriteria.PART_NUMBER
                searchResults = BBCItemManager.SearchBBCItem(txtSearch.Text, BBCItemSearchCriteria.PART_NUMBER)
            Case SearchCriteria.BRAND
                searchResults = BBCItemManager.GetBBCItemsByBrand(ddlBrands.SelectedValue)
            Case SearchCriteria.DESCRIPTION
                searchResults = BBCItemManager.SearchBBCItem(txtSearch.Text, BBCItemSearchCriteria.DESCRIPTION)
        End Select

        If searchResults.Count > 0 Then
            plcSearchResults.Visible = True
            gvSearchResults.DataSource = searchResults
            gvSearchResults.DataBind()
        Else
            plcNoResults.Visible = True
        End If

    End Sub

    Private Sub BindBBCItemBrands()

        Dim brandList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.BBCBrandsListId)

        If brandList.Count > 0 Then
            ddlBrands.DataValueField = "ID"
            ddlBrands.DataTextField = "Name"
            ddlBrands.DataSource = brandList
            ddlBrands.DataBind()
        End If

    End Sub

    Private Sub HandleViewBySearchOptions()

        ' Just reset evertyhing and handle it with the case
        txtSearch.Visible = True
        ddlBrands.Visible = True

        ' Hide anything to do with results since we are changing modes
        plcSearchResults.Visible = False
        plcNoResults.Visible = False

        Select Case ddlSearchCriteria.SelectedValue
            Case SearchCriteria.PART_NUMBER
                reqTextSearch.Enabled = True
                ddlBrands.Visible = False
            Case SearchCriteria.BRAND
                reqTextSearch.Enabled = False
                txtSearch.Visible = False
            Case SearchCriteria.DESCRIPTION
                reqTextSearch.Enabled = True
                ddlBrands.Visible = False
        End Select

    End Sub

#End Region

 
End Class
