'----------------------------------------------------------------------------------------------
' Filename    : TopBar.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Controls_Navigation_TopBar
    Inherits System.Web.UI.UserControl

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loggedInUser As AptUser = UserManager.GetUser(SessionManager.LoggedInUserId)

        If loggedInUser IsNot Nothing Then
            lblUser.Text = String.Format("Hi {0}, Welcome to APT", loggedInUser.Forename)
        End If

        lnkClearName.Attributes.Add("onclick", ProduceJavascript(txtProjectName.ID))
        lnkClearOwner.Attributes.Add("onclick", ProduceJavascript(txtProjectOwner.ID))
        lnkClearCoordinator.Attributes.Add("onclick", ProduceJavascript(txtCoordinator.ID))
        lnkClearArtworker.Attributes.Add("onclick", ProduceJavascript(txtArtworker.ID))

        If Page.IsPostBack = False Then
            BindDropDown()
        End If
    End Sub

    Protected Sub lnkSearchName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearchName.Click
        Response.Redirect(String.Format("~/Project/ProjectSearchResults.aspx?searchType={0}&value={1}", _
                          Convert.ToInt32(SearchType.Name), txtProjectName.Text))
    End Sub

    Protected Sub lnkSearchOwner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearchOwner.Click
        Response.Redirect(String.Format("~/Project/ProjectSearchResults.aspx?searchType={0}&value={1}", _
                          Convert.ToInt32(SearchType.OwnerName), txtProjectOwner.Text))
    End Sub

    Protected Sub lnkSearchCoordinator_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearchCoordinator.Click
        Response.Redirect(String.Format("~/Project/ProjectSearchResults.aspx?searchType={0}&value={1}", _
                          Convert.ToInt32(SearchType.Coordinator), txtCoordinator.Text))
    End Sub

    Protected Sub lnkSearchArtworker_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearchArtworker.Click
        Response.Redirect(String.Format("~/Project/ProjectSearchResults.aspx?searchType={0}&value={1}", _
                          Convert.ToInt32(SearchType.Artworker), txtArtworker.Text))
    End Sub

    Protected Sub lnkSearchBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearchBrand.Click
        Response.Redirect(String.Format("~/Project/ProjectSearchResults.aspx?searchType={0}&value={1}", _
                          Convert.ToInt32(SearchType.Brand), ddlBrand.SelectedValue))
    End Sub

    Protected Sub txtAinNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAinNumber.TextChanged
        Dim projectId As Integer

        If Integer.TryParse(txtAinNumber.Text, projectId) Then
            If ProjectManager.GetProject(projectId) IsNot Nothing Then
                Response.Redirect(String.Format("~/Project/ProjectDetails.aspx?projectId={0}", projectId))
            End If
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Function ProduceJavascript(ByVal componentID As String) As String
        Return String.Format("document.getElementById('{0}_{1}').value = '';", Me.ClientID, componentID)
    End Function

    Private Sub BindDropDown()
        Dim brandList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.BrandListId)

        ddlBrand.SelectedValue = Nothing

        modComponent.BindDropDown(ddlBrand, brandList, "ID", "Name", "Brand", "- Select a {0} -")
    End Sub

#End Region

End Class
