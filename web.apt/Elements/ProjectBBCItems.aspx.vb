'----------------------------------------------------------------------------------------------
' Filename    : ProjectBBCItems.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Elements_ProjectBBCItems
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            InitPage()
        End If

    End Sub

    Private Sub InitPage()
        If IsNumeric(Request.QueryString("projectid")) Then
            'ctrlBBCItem.ProjectId = Request.QueryString("projectid")
            ctrlProjectHeader.ProjectId = Request.QueryString("projectid")
            ctrlSubNavProject.ProjectId = Request.QueryString("projectid")
            ctrlBBCItem.ProjectId = Request.QueryString("projectid")

            PermissionCheck()

            If IsNumeric(Request.QueryString("itemid")) Then
                If BBCItemManager.DoesBBCItemBelongToProject(Request.QueryString("itemid"), _
                                                            Request.QueryString("projectid")) Then
                    ctrlBBCItem.ProjectBBCItemId = Request.QueryString("itemid")
                End If
            End If
        End If

    End Sub

    Private Sub PermissionCheck()
        If PermissionsManager.CanAddEditBBCItems(SessionManager.LoggedInUserId) = False Then
            ctrlBBCItem.IsReadOnly = True
        End If
    End Sub

    Protected Sub ctrlBBCItem_SaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlBBCItem.SaveSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Project BBC Item has been saved")
    End Sub

End Class
