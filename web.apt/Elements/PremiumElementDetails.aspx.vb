'----------------------------------------------------------------------------------------------
' Filename    : PremiumElementDetails.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Elements_PremiumElementDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            PageInit()
        End If

    End Sub

    Protected Sub ctrlPremiumElementDetails_ElementPremiumDetailsSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlPremiumElementDetails.ElementPremiumDetailsSaveSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Premium Element brief has been saved successfully")
    End Sub

    Private Sub PageInit()

        PermissionCheck()

        If IsNumeric(Request.QueryString("additionalElementId")) Then
            ctrlPremiumElementDetails.AdditionalElementId = Request.QueryString("additionalElementId")
        End If

        If IsNumeric(Request.QueryString("projectId")) Then
            ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
            ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
        End If

    End Sub

    Private Sub PermissionCheck()
        If PermissionsManager.CanAlterPremiumBrief(SessionManager.LoggedInUserId) = False Then
            ctrlPremiumElementDetails.IsReadOnly = True
        End If
    End Sub

End Class
