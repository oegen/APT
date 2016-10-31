'----------------------------------------------------------------------------------------------
' Filename    : AdditionalElementListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Elements_AdditionalElementListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            PageInit()
        End If
    End Sub

    Protected Sub ctrlAdditionalElementListing_AdditionalElementDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAdditionalElementListing.AdditionalElementDeleted
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Additional Element has been deleted")
    End Sub

#End Region

#Region "Private Implementation"

#End Region

    Private Sub PageInit()
        If IsNumeric(Request.QueryString("projectId")) Then
            PermissionCheck()
            ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
            ctrlAdditionalElementListing.ProjectId = Request.QueryString("projectId")
            ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
            hypAddNew.NavigateUrl = String.Format("~/Elements/AdditionalElement.aspx?projectId={0}", Request.QueryString("projectId"))
        End If
    End Sub

    Private Sub PermissionCheck()

        If PermissionsManager.CanAddeditPremiumElements(SessionManager.LoggedInUserId) = False Then
            hypAddNew.Visible = False
        End If

    End Sub

End Class
