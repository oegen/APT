Imports aptBusinessLogic

Partial Class Admin_Element_Manager_ElementType
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("AptTypeId")) Then
                ctrlAptType.AptTypeId = Request.QueryString("AptTypeId")
            End If
        End If

    End Sub

    Protected Sub ctrlAptType_AptTypeSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAptType.AptTypeSaveSuccess
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("Element Type has been saved successfully")
    End Sub

End Class
