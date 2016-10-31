'----------------------------------------------------------------------------------------------
' Filename    : ProjectListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities
Imports System.Collections.Generic

Partial Class Task_ProjectListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' TODO - Check user is logged in and permissions
        If SessionManager.LoggedInUserId = 0 Then
            Response.Redirect("~/Login.aspx")
        End If

        If Page.IsPostBack = False Then
            SetupPage()
        End If
    End Sub

    Protected Sub ddlBrand_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBrand.SelectedIndexChanged
        ctrlProjectListing.BrandId = ddlBrand.SelectedValue
        ctrlProjectListing.SetupProjectListing()
        StoreFilter()
    End Sub

    Protected Sub ddlSortBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSortBy.SelectedIndexChanged
        ctrlProjectListing.SortMethod = ddlSortBy.SelectedValue
        ctrlProjectListing.SetupProjectListing()
        StoreFilter()
    End Sub

    Protected Sub lnkTradeInDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTradeInDate.Click
        Dim selectedDate As Date

        If Date.TryParse(txtTradeInDate.Text, selectedDate) Then
            ctrlProjectListing.TradeDate = selectedDate
            ctrlProjectListing.SetupProjectListing()
            StoreFilter()
        End If
    End Sub

    Protected Sub lnkGoOwner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGoOwner.Click
        ctrlProjectListing.OwnerFilter = txtOwner.Text
        ctrlProjectListing.SetupProjectListing()
        StoreFilter()
    End Sub

    Protected Sub lnkClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkClear.Click
        ctrlProjectListing.BrandId = 0
        ctrlProjectListing.SortMethod = 0
        ctrlProjectListing.OwnerFilter = ""
        ctrlProjectListing.TradeDate = Nothing
        ctrlProjectListing.SetupProjectListing()

        ddlBrand.SelectedValue = 0
        ddlSortBy.SelectedValue = 0
        txtOwner.Text = "Owner"
        txtTradeInDate.Text = ""

        ProjectFilterManager.ClearProjectFilter(Me.Page, FilterUse.Project)
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPage()
        ctrlProjectListing.UserId = SessionManager.LoggedInUserId

        BindDropDowns()
        SetFilter()
    End Sub

    Private Sub BindDropDowns()
        BindBrands()
        BindSortBy()
    End Sub

    Private Sub BindBrands()
        Dim brandList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.BrandListId)

        modComponent.BindDropDown(ddlBrand, brandList, "ID", "Name", "Brands", "All {0}")
    End Sub

    Private Sub BindSortBy()
        ddlSortBy.Items.Add(New ListItem("Name", Controls_Project_ProjectListing.SortBy.Name))
        ddlSortBy.Items.Add(New ListItem("Brand", Controls_Project_ProjectListing.SortBy.Brand))
        ddlSortBy.Items.Add(New ListItem("Owner", Controls_Project_ProjectListing.SortBy.Owner))
        ddlSortBy.Items.Add(New ListItem("In-Trade Date", Controls_Project_ProjectListing.SortBy.TradeInDate))
        ddlSortBy.Items.Add(New ListItem("AIN Number", Controls_Project_ProjectListing.SortBy.AIN))
    End Sub

    Private Sub SetFilter()
        Dim filter As ProjectFilter = ProjectFilterManager.GetProjectFilterFromCookie(Me.Page, FilterUse.Project)
        If Not filter Is Nothing Then
            Dim filtered As Boolean = False
            If Not filter.Brand = -1 Then
                ddlBrand.SelectedValue = filter.Brand.ToString()
                ctrlProjectListing.BrandId = filter.Brand
                filtered = True
            End If
            If Not filter.SortBy = -1 Then
                ddlSortBy.SelectedValue = filter.SortBy.ToString()
                ctrlProjectListing.SortMethod = filter.SortBy
                filtered = True
            End If
            If Not filter.TradeDate = Date.MinValue Then
                txtTradeInDate.Text = filter.TradeDate.ToString("dd/MM/yyyy")
                ctrlProjectListing.TradeDate = filter.TradeDate
                filtered = True
            End If
            If Not filter.Owner = String.Empty Then
                txtOwner.Text = filter.Owner
                ctrlProjectListing.OwnerFilter = filter.Owner
                filtered = True
            End If
            If filtered Then
                ctrlProjectListing.SetupProjectListing()
            End If
        End If
    End Sub

    Private Sub StoreFilter()
        Dim filter As ProjectFilter = New ProjectFilter
        filter.Brand = Integer.Parse(ddlBrand.SelectedValue)
        filter.SortBy = Integer.Parse(ddlSortBy.SelectedValue)
        If Not txtOwner.Text = "Owner" Then
            filter.Owner = txtOwner.Text
        End If
        Date.TryParse(txtTradeInDate.Text, filter.TradeDate)
        ProjectFilterManager.AddEditProjectFilter(Me.Page, filter, FilterUse.Project)
    End Sub

#End Region
End Class
