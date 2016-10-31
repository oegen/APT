'----------------------------------------------------------------------------------------------
' Filename    : AdditionalElement.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Elements_AdditionalElement
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            PermissionCheck()
            LoadPremiumElementInformation()
        End If

    End Sub

    Protected Sub ctrlAdditionalElement_SaveAdditionalElementSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAdditionalElement.SaveAdditionalElementSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Additional Element has been saved successfully")
    End Sub

    Private Sub LoadPremiumElementInformation()

        If IsNumeric(Request.QueryString("projectId")) Then
            ctrlAdditionalElement.ProjectId = Request.QueryString("projectId")
            ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
            ctrlSubNavProject.ProjectId = Request.QueryString("projectId")

            If IsNumeric(Request.QueryString("additionalElementId")) Then

                If ElementManager.DoesAdditionalElementBelongToProject(Request.QueryString("additionalElementId"), _
                                                                       Request.QueryString("projectId")) Then

                    ctrlAdditionalElement.AdditionalElementId = Request.QueryString("additionalElementId")

                End If
            End If
        End If

    End Sub

    Private Sub PermissionCheck()

        If PermissionsManager.CanAddeditPremiumElements(SessionManager.LoggedInUserId) = False Then
            ctrlAdditionalElement.IsReadOnly = True
        End If

    End Sub

#End Region

End Class
