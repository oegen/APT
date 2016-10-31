
Partial Class Admin_Element_Manager_SubclassType
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("aptTypeId")) Then
                ctrlSubclassType.AptTypeId = Request.QueryString("aptTypeId")
            End If

            If IsNumeric(Request.QueryString("subclassTypeId")) Then
                ctrlSubclassType.SubclassTypeId = Request.QueryString("subclassTypeId")
            End If
        End If

    End Sub

    Protected Sub ctrlSubclassType_SubclassTypeSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlSubclassType.SubclassTypeSaveSuccess
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("Subclass Type has been saved successfully")
    End Sub

End Class
