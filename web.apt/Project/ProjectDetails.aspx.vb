'----------------------------------------------------------------------------------------------
' Filename    : ProjectDetails.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Project_ProjectDetails
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            SetupPage()
        End If
    End Sub

    Protected Sub ctrlProjectDetails_ProjectArchived(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlProjectDetails.ProjectArchived
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("The project has now been archived")
    End Sub

    Protected Sub ctrlProjectDetails_ProjectStopped(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlProjectDetails.ProjectStopped
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("This project has now been stopped")
    End Sub

    Protected Sub ctrlProjectDetails_ProjectDetailsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlProjectDetails.ProjectDetailsChanged
        'CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("The project details were saved successfully.<br />Dont forget to add some elements!")
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("No quote will be generated without the correct financial cost codes being present", , 4)
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayContentTitle(ctrlProjectDetails.ProjectId)
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPage()
        Dim projectId As Integer

        If Integer.TryParse(Request.QueryString("projectId"), projectId) Then
            ctrlProjectDetails.ProjectId = projectId

            ' Set master page tab
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SelectedTab = ProjectTabIndex.INFO
            ' set master page project
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SetSubNavSelectedItem(ConstantLibrary.ProjectSubNavItems.EDIT_PROJECT)

            CheckPermissions(projectId)
        Else
            ' Redirect somewhere safe (no project id available)
            Response.Redirect("~/Project/ProjectListing.aspx")
        End If
    End Sub

    Private Sub CheckPermissions(ByVal projectId As Integer)

        ' Project details are only allowed to be edited by the project co-ordinator, williams lea and MDA
        ctrlProjectDetails.ReadOnlyMode = Not PermissionsManager.CanUserEditProject(SessionManager.LoggedInUserId, projectId)

    End Sub

#End Region

End Class
