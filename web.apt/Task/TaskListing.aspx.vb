'----------------------------------------------------------------------------------------------
' Filename    : TaskListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Task_TaskListing
    Inherits System.Web.UI.Page

#Region "Properties"
    Private Property TotalItems As Integer
        Get
            Return ViewState("_totalItems")
        End Get
        Set(ByVal value As Integer)
            ViewState("_totalItems") = value
        End Set
    End Property

    Public Property UserId As Integer
        Get
            Return ViewState("_userId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_userId") = value
            'SetupProjectListing()
        End Set
    End Property

    Public Property BrandId As Integer
        Get
            Return ViewState("_brandId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_brandId") = value
            'SetupProjectListing()
        End Set
    End Property

    Public Property OwnerFilter As String
        Get
            Return ViewState("_ownerFilter")
        End Get
        Set(ByVal value As String)
            ViewState("_ownerFilter") = value
            'SetupProjectListing()
        End Set
    End Property

    Public Property SortMethod As ProjectSortBy
        Get
            Return ViewState("_sortMethod")
        End Get
        Set(ByVal value As ProjectSortBy)
            ViewState("_sortMethod") = value
            'SetupProjectListing()
        End Set
    End Property

    Public Property PageSize As Integer
        Get
            If ViewState("_pageSize") = 0 Then
                ' Paging count has not explicity been set so return the app setting for it
                Return 10
            Else
                Return ViewState("_pageSize")
            End If
        End Get
        Set(ByVal value As Integer)
            ViewState("_pageSize") = value
        End Set
    End Property

    Public Property TradeDate As Nullable(Of Date)
        Get
            Return ViewState("_tradeDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            ViewState("_tradeDate") = value
            'SetupProjectListing()
        End Set
    End Property
#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim t As New System.Timers.Timer()
        't.Start()

        Dim d1 = DateTime.Now()

        If Page.IsPostBack = False Then
            LoadPage()
        End If

        Dim d2 = DateTime.Now()

        'ltlTimer.Text = (d2 - d1).TotalMilliseconds

    End Sub

    Protected Sub rptrProjectListing_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrProjectListing.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim singleProject As Project = CType(e.Item.DataItem, Project)
            Dim lblProjectName As Label = CType(e.Item.FindControl("lblProjectName"), Label)
            Dim lblProjectAIN As Label = CType(e.Item.FindControl("lblProjectAIN"), Label)
            Dim ctrlTaskListing As Controls_Generic_TaskListing = CType(e.Item.FindControl("ctrlTaskListing"), Controls_Generic_TaskListing)

            lblProjectName.Text = singleProject.Name
            lblProjectAIN.Text = singleProject.ID
            ctrlTaskListing.ProjectId = singleProject.ID

        End If
    End Sub

    Protected Sub ddlBrand_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBrand.SelectedIndexChanged
        BrandId = ddlBrand.SelectedValue
        StoreFilter()
        SetupProjectListing()
    End Sub

    Protected Sub ddlSortBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSortBy.SelectedIndexChanged
        SortMethod = ddlSortBy.SelectedValue
        StoreFilter()
        SetupProjectListing()
    End Sub


    Protected Sub lnkGoOwner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGoOwner.Click
        OwnerFilter = txtOwner.Text
        StoreFilter()
        SetupProjectListing()
    End Sub

    Protected Sub lnkClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkClear.Click
        BrandId = 0
        SortMethod = 0
        OwnerFilter = ""
        TradeDate = Nothing

        ddlBrand.SelectedValue = 0
        ddlSortBy.SelectedValue = 0
        txtOwner.Text = "Owner"

        ProjectFilterManager.ClearProjectFilter(Me.Page, FilterUse.Task)
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadPage()
        UserId = SessionManager.LoggedInUserId

        BindDropDowns()
        SetFilter()
        SetupProjectListing()
    End Sub

    ' Written for the markup to call to get the class (last item needs a different class to others)
    Public Function GetItemClass(ByVal itemIndex As Integer) As String
        If itemIndex = TotalItems - 1 Then
            Return "last-ul"
        Else
            Return ""
        End If
    End Function

    Public Sub SetupProjectListing(Optional ByVal currentPageIndex As Integer = 1)

        'Dim usersProjectList As List(Of Project) = ProjectManager.GetProjectsBy3User3Comprehension(UserId, currentPageIndex, PageSize, TotalItems, BrandId, OwnerFilter, TradeDate, SortMethod)

        Dim filters = GetFilters(currentPageIndex)
        Dim finder As New MyTasksFinder(filters)

        Dim usersProjectList As List(Of Project) = finder.Find()

        If usersProjectList.Count > 0 Then

            SetupPaging(currentPageIndex, filters.TotalDataItems)

            rptrProjectListing.DataSource = usersProjectList
            rptrProjectListing.DataBind()

            lblNoResults.Visible = False
            rptrProjectListing.Visible = True
        Else
            lblNoResults.Visible = True
            rptrProjectListing.Visible = False
        End If
    End Sub

    Private Function GetFilters(ByVal currentPage As Integer) As ProjectSearchFilters

        Dim filters As New ProjectSearchFilters()

        filters.BrandId = BrandId
        filters.OwnerFilter = OwnerFilter
        filters.PageIndex = currentPage
        filters.PageSize = PageSize
        filters.SortBy = SortMethod
        filters.TotalDataItems = TotalItems
        filters.TradeDate = TradeDate
        filters.UserId = UserId

        Return filters

    End Function

    Private Sub BindDropDowns()
        BindBrands()
        BindSortBy()
    End Sub

    Private Sub BindBrands()
        Dim brandList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.BrandListId)

        modComponent.BindDropDown(ddlBrand, brandList, "ID", "Name", "Brands", "All {0}")
    End Sub

    Private Sub BindSortBy()
        ddlSortBy.Items.Add(New ListItem("Name", ProjectSortBy.Name))
        ddlSortBy.Items.Add(New ListItem("Brand", ProjectSortBy.Brand))
        ddlSortBy.Items.Add(New ListItem("Owner", ProjectSortBy.Owner))
        ddlSortBy.Items.Add(New ListItem("In-Trade Date", ProjectSortBy.TradeInDate))
        ddlSortBy.Items.Add(New ListItem("AIN Number", ProjectSortBy.AIN))
    End Sub

    Private Sub SetFilter()
        Dim filter As ProjectFilter = ProjectFilterManager.GetProjectFilterFromCookie(Me.Page, FilterUse.Task)
        If Not filter Is Nothing Then
            Dim filtered As Boolean = False
            If Not filter.Brand = -1 Then
                ddlBrand.SelectedValue = filter.Brand.ToString()
                BrandId = filter.Brand
                filtered = True
            End If
            If Not filter.SortBy = -1 Then
                ddlSortBy.SelectedValue = filter.SortBy.ToString()
                SortMethod = filter.SortBy
                filtered = True
            End If
      
            If Not filter.Owner = String.Empty Then
                txtOwner.Text = filter.Owner
                OwnerFilter = filter.Owner
                filtered = True
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
        ProjectFilterManager.AddEditProjectFilter(Me.Page, filter, FilterUse.Task)
    End Sub

    Protected Sub ctrlPagingBottom_PageChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ctrlPagingBottom.PageChanged
        Dim newPage As Integer = e.CommandArgument
        ' We need to change the top nav paging as well
        ctrlPagingTop.CurrentPage = newPage
        ChangePage(newPage)
    End Sub

    Protected Sub ctrlPagingTop_PageChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ctrlPagingTop.PageChanged
        Dim newPage As Integer = e.CommandArgument
        ' We need to change the bottom nav paging as well
        ctrlPagingBottom.CurrentPage = newPage
        ChangePage(newPage)
    End Sub

    Private Sub ChangePage(ByVal newPage As Integer)
        SetupProjectListing(newPage)
    End Sub

    Private Sub SetupPaging(ByVal currentPage As Integer, ByVal totalPages As Integer)
        ctrlPagingTop.InitPaging(currentPage, totalPages, PageSize)
        ctrlPagingBottom.InitPaging(currentPage, totalPages, PageSize)
    End Sub

#End Region

End Class
