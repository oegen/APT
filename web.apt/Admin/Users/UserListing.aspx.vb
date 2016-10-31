
Partial Class Admin_Users_UserListing
    Inherits System.Web.UI.Page

    Protected Sub ctrlUserList_DisabledUser(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUserListing.DisabledUser
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("User has been disabled")
    End Sub

    Protected Sub ctrlUserList_EnabledUser(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUserListing.EnabledUser
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("User has been re-enabled")
    End Sub

    Protected Sub lnkSurname_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSurname.Click
        If String.IsNullOrEmpty(txtSurname.Text) = False Then
            ctrlUserListing.Surname = txtSurname.Text
        End If
    End Sub

    Protected Sub lnkUsername_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUsername.Click
        If String.IsNullOrEmpty(txtUsername.Text) = False Then
            ctrlUserListing.Surname = txtUsername.Text
        End If
    End Sub

End Class
