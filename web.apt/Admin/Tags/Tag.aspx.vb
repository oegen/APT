
Partial Class Admin_Tags_Tag
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("tagId")) Then
                ctrlTag.TagId = Request.QueryString("tagId")
            End If
        End If

    End Sub

    Protected Sub ctrlTag_TagSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlTag.TagSaveSuccess
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("Tag has been saved successfully")
    End Sub

End Class
