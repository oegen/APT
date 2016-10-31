'----------------------------------------------------------------------------------------------
' Filename    : PremiumBrief.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Project_PremiumBrief
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            InitPage()
        End If

    End Sub

    Protected Sub ctrlPremiumBrief_PremiumBriefSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlPremiumBrief.PremiumBriefSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Project Premium brief has been saved successfully")
    End Sub

    Private Sub InitPage()
        If IsNumeric(Request.QueryString("projectId")) Then
            PermissionCheck()
            ctrlPremiumBrief.ProjectId = Request.QueryString("projectId")
            ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
            ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
        End If
    End Sub

    Private Sub PermissionCheck()
        If PermissionsManager.CanAlterPremiumBrief(SessionManager.LoggedInUserId) = False Then
            ctrlPremiumBrief.IsReadOnly = True
        End If
    End Sub

End Class
