
Partial Class Admin_Users_User
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("userId")) Then
                ctrlAddEditUser.UserId = Request.QueryString("userId")
                ctrlUserRoles.UserId = Request.QueryString("userId")
                ctrlUserRoles.Visible = True
            End If
        End If

    End Sub

    Protected Sub ctrlAddEditUser_UserSaved(ByVal sender As Object, ByVal e As CommandEventArgs) Handles ctrlAddEditUser.UserSaved
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("User details have been saved successfully")
        ctrlUserRoles.UserId = e.CommandArgument
        ctrlUserRoles.Visible = True
    End Sub

End Class
