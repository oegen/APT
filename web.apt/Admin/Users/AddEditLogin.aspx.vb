
Partial Class Admin_Users_AddEditLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("userId")) Then
                ctrlEditUserLogin.UserId = Request.QueryString("userId")
            End If
        End If

    End Sub

    Protected Sub ctrlEditUserLogin_LoginSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlEditUserLogin.LoginSaveSuccess
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("Login details has been saved successfully")
    End Sub

End Class
