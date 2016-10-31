'----------------------------------------------------------------------------------------------
' Filename    : AdminProjectListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        28/11/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Admin_Project_AdminProjectListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ctrlAdminProjectListing.CurrentFilterMode = ASP.controls_admin_project_adminprojectlisting_ascx.FilterMode.ALL
        End If
    End Sub

    Protected Sub ddlFilterMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFilterMode.SelectedIndexChanged
        ctrlAdminProjectListing.CurrentFilterMode = ddlFilterMode.SelectedValue
    End Sub

    Protected Sub ctrlAdminProjectListing_ArchiveProject(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAdminProjectListing.ArchiveProject
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("The project has been archived")
    End Sub

    Protected Sub ctrlAdminProjectListing_StartProject(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAdminProjectListing.StartProject
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("The project has been restarted")
    End Sub

    Protected Sub ctrlAdminProjectListing_StopProject(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAdminProjectListing.StopProject
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("The project has been stopped")
    End Sub

    Protected Sub ctrlAdminProjectListing_UnarchiveProject(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAdminProjectListing.UnarchiveProject
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("The project has been unarchived")
    End Sub

#End Region

End Class
