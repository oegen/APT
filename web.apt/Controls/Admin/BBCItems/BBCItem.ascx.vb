'----------------------------------------------------------------------------------------------
' Filename    : BBCItem.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Admin_BBCItems_BBCItem
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event BBCItemSaveSuccess As EventHandler
    Public Event BBCItemSaveFailure As EventHandler

    Public Property BBCItemId As Integer
        Get
            Return ViewState(Me.UniqueID & "_bbcItemId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_bbcItemId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        If Page.IsValid Then
            SaveBBCBrand()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindBBCBrands()
            If BBCItemId <> 0 Then
                LoadBBCItemDetails()
            End If
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadBBCItemDetails()
        Dim loadBBCItem As NewBBCItem = BBCItemManager.GetNewBBCItem(BBCItemId)

        txtPartNumber.Text = loadBBCItem.PartNumber
        txtDescription.Text = loadBBCItem.Description
        ddlBrands.SelectedValue = loadBBCItem.Brand.ID
    End Sub

    Private Sub BindBBCBrands()
        ddlBrands.BindDataToDropDown(AppSettingsGet.BBCBrandsListId, "Brand", "- Select a {0} -")
    End Sub

    Private Sub SaveBBCBrand()
        Dim saveBBCItem As New NewBBCItem

        If BBCItemId <> 0 Then
            saveBBCItem = BBCItemManager.GetNewBBCItem(BBCItemId)
        End If

        saveBBCItem.PartNumber = txtPartNumber.Text
        saveBBCItem.Description = txtDescription.Text
        saveBBCItem.Brand = ListManager.GetListNode(ddlBrands.SelectedValue)

        SaveNewBBCItem(saveBBCItem)
        BBCItemId = saveBBCItem.ID

        RaiseEvent BBCItemSaveSuccess(Me, New EventArgs())
    End Sub

#End Region



End Class
