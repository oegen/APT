'----------------------------------------------------------------------------------------------
' Filename    : KittingBrief.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        07/10/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Kitting_KittingBrief
    Inherits System.Web.UI.Page


#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            InitPage()
        End If
    End Sub

    Protected Sub ctrlKittingBrief_KittingBriefSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlKittingBrief.KittingBriefSaveSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Kitting brief have been saved successfully")
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub InitPage()
        If IsNumeric(Request.QueryString("projectId")) Then
            PermissionCheck()
            ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
            ctrlKittingBrief.ProjectId = Request.QueryString("projectId")
            ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
        End If
    End Sub

    Private Sub PermissionCheck()
        If PermissionsManager.CanAlterKittingBrief(SessionManager.LoggedInUserId) = False Then
            ctrlKittingBrief.IsReadOnly = True
        End If
    End Sub

#End Region

End Class

