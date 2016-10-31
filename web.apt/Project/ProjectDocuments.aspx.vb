'----------------------------------------------------------------------------------------------
' Filename    : ProjectDocuments.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Project_ProjectDocuments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            PermissionCheck()
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SelectedTab = ProjectTabIndex.DOCUMENTS
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SetSubNavSelectedItem(ConstantLibrary.ProjectSubNavItems.EDIT_PROJECT)

            If IsNumeric(Request.QueryString("projectId")) Then
                ctrlDocumentOverview.ProjectId = Request.QueryString("projectId")
            End If
        End If
    End Sub

    Protected Sub ctrlDocumentOverview_UploadSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlDocumentOverview.UploadSuccess
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("File has been uploaded")
    End Sub

    Private Sub PermissionCheck()

        'If PermissionsManager.CanAccessDocuments(SessionManager.LoggedInUserId, Request.QueryString("projectId")) = False Then
        '    Response.Redirect("~/default.aspx")
        'End If

    End Sub

End Class
