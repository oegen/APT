'----------------------------------------------------------------------------------------------
' Filename    : ProjectListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities 
Imports System.Collections.Generic

Partial Class Controls_Project_ProjectListing
    Inherits System.Web.UI.UserControl

#Region "Enumerations"

    Public Enum SortBy
        Name = 0
        Brand = 1
        Owner = 2
        TradeInDate = 3
        AIN = 4
    End Enum

#End Region

#Region "Properties"

    Public Property UserId As Integer
        Get
            Return ViewState("_userId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_userId") = value
            SetupProjectListing()
        End Set
    End Property

    Public Property BrandId As Integer
        Get
            Return ViewState("_brandId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_brandId") = value
            SetupProjectListing()
        End Set
    End Property

    Public Property OwnerFilter As String
        Get
            Return ViewState("_ownerFilter")
        End Get
        Set(ByVal value As String)
            ViewState("_ownerFilter") = value
            SetupProjectListing()
        End Set
    End Property

    Public Property SortMethod As SortBy
        Get
            Return ViewState("_sortMethod")
        End Get
        Set(ByVal value As SortBy)
            ViewState("_sortMethod") = value
            SetupProjectListing()
        End Set
    End Property

    Public Property TradeDate As Nullable(Of Date)
        Get
            Return ViewState("_tradeDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            ViewState("_tradeDate") = value
            SetupProjectListing()
        End Set
    End Property

    Public Property PageSize As Integer
        Get
            If ViewState("_pageSize") = 0 Then
                ' Paging count has not explicity been set so return the app setting for it
                Return AppSettingsGet.ProjectListingCount
            Else
                Return ViewState("_pageSize")
            End If
        End Get
        Set(ByVal value As Integer)
            ViewState("_pageSize") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub rptrProjectList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrProjectList.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim project As Project = CType(e.Item.DataItem, Project)
            Dim lblProjectName As Label = CType(e.Item.FindControl("lblProjectName"), Label)
            Dim lblAINNumber As Label = CType(e.Item.FindControl("lblAINNumber"), Label)
            Dim lblOwner As Label = CType(e.Item.FindControl("lblOwner"), Label)
            Dim hypViewProject As HyperLink = CType(e.Item.FindControl("hypViewProject"), HyperLink)

            lblProjectName.Text = project.Name
            lblAINNumber.Text = project.ID
            lblOwner.Text = ProjectManager.GetProjectOwner(project.ID).FullName
            hypViewProject.NavigateUrl = String.Format("~/Project/ProjectDetails.aspx?projectId={0}", project.ID)
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Public Sub SetupProjectListing(Optional ByVal currentPage As Integer = 0)

        ctrlPaging.Visible = False

        Dim usersProjectList As List(Of Project)

        If UserManager.DoesUserHaveAGlobalRole(UserId) Then
            usersProjectList = ProjectManager.GetActiveProjectsByCount()
        Else
            usersProjectList = ProjectManager.GetProjectsByUser(UserId)
        End If

        ' Filter by the options selected
        If BrandId <> 0 Then
            ProjectManager.FilterByBrand(usersProjectList, BrandId)
        End If

        If OwnerFilter IsNot Nothing AndAlso OwnerFilter IsNot "" Then
            ProjectManager.FilterByOwner(usersProjectList, OwnerFilter)
        End If

        If TradeDate IsNot Nothing Then
            ProjectManager.FilterByTradeInDate(usersProjectList, TradeDate)
        End If

        ' SortMethod
        SortProjectList(usersProjectList)

        If usersProjectList.Count > 0 Then

            If currentPage <> 0 Then
                ' CurrentPage has been specified so go to that page
                usersProjectList = modPaging.Page(usersProjectList, currentPage, PageSize)
            Else
                ctrlPaging.InitPaging(1, usersProjectList.Count, PageSize)
                usersProjectList = modPaging.Page(usersProjectList, 1, PageSize)
            End If

            rptrProjectList.DataSource = usersProjectList
            rptrProjectList.DataBind()

            lblNoResults.Visible = False
            rptrProjectList.Visible = True
            ctrlPaging.Visible = True
        Else
            lblNoResults.Visible = True
            rptrProjectList.Visible = False
        End If
    End Sub

    Private Sub SortProjectList(ByRef projecList As List(Of Project))
        Select Case SortMethod
            Case SortBy.Name
                ProjectManager.SortByName(projecList)
            Case SortBy.Brand
                ProjectManager.SortByBrand(projecList)
            Case SortBy.Owner
                ProjectManager.SortByOwner(projecList)
            Case SortBy.TradeInDate
                ProjectManager.SortByTradeInDate(projecList)
            Case SortBy.AIN
                ProjectManager.SortByID(projecList)
        End Select
    End Sub

#End Region

    Protected Sub ctrlPaging_PageChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ctrlPaging.PageChanged
        Dim newPage As Integer = e.CommandArgument
        SetupProjectListing(newPage)
    End Sub

End Class
