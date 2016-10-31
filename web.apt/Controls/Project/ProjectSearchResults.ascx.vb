'----------------------------------------------------------------------------------------------
' Filename    : ProjectSearchResults.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Project_ProjectSearchResults
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property SearchedAttribute As SearchType
        Get
            Return ViewState("_searchedAttribute")
        End Get
        Set(ByVal value As SearchType)
            ViewState("_searchedAttribute") = value
        End Set
    End Property

    Public Property SearchValue As String
        Get
            Return ViewState("_searchValue")
        End Get
        Set(ByVal value As String)
            ViewState("_searchValue") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LoadSearchResults()
        End If
    End Sub

    Protected Sub rptrProjectList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrProjectList.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim project As Project = CType(e.Item.DataItem, Project)
            Dim lblProjectName As Label = CType(e.Item.FindControl("lblProjectName"), Label)
            Dim lblAINNumber As Label = CType(e.Item.FindControl("lblAINNumber"), Label)
            'Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)
            Dim lblOwner As Label = CType(e.Item.FindControl("lblOwner"), Label)
            Dim hypViewProject As HyperLink = CType(e.Item.FindControl("hypViewProject"), HyperLink)

            lblProjectName.Text = project.Name
            lblAINNumber.Text = project.ID
            'lblDescription.Text = project.Description
            lblOwner.Text = ProjectManager.GetProjectOwner(project.ID).FullName
            hypViewProject.NavigateUrl = String.Format("~/Project/ProjectDetails.aspx?projectid={0}", project.ID)
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadSearchResults()
        Dim projectList As List(Of Project) = ProjectManager.GetAllActiveProjects()

        Select Case SearchedAttribute
            Case SearchType.Name
                projectList = ProjectManager.SearchProjects(SearchValue)
            Case SearchType.OwnerName
                projectList = ProjectManager.SearchProjectsByOwner(SearchValue)
            Case SearchType.Coordinator
                projectList = ProjectManager.SearchProjectsByCoordinator(SearchValue)
            Case SearchType.Artworker
                projectList = ProjectManager.SearchProjectsByArtworker(SearchValue)
            Case SearchType.Brand
                ProjectManager.FilterByBrand(projectList, Convert.ToInt32(SearchValue))
        End Select

        If projectList.Count > 0 Then
            rptrProjectList.Visible = True
            rptrProjectList.DataSource = projectList
            rptrProjectList.DataBind()
        Else
            rptrProjectList.Visible = False
            lblNoResults.Visible = True
        End If
    End Sub

#End Region

End Class
