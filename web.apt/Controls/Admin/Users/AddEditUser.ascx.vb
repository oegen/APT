'----------------------------------------------------------------------------------------------
' Filename    : AddEditUser.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities
Imports System.Collections.Generic

Partial Class Controls_Admin_AddEditUser
    Inherits System.Web.UI.UserControl

#Region "Constants"

    Private Const REMOVE_CMD As String = "userRoleRemove"
    Private Const USERNAME_EXISTS_ERROR As String = "<br /><br /><br /><br />This username is already in use, please choose another one"

#End Region

#Region "Properties"

    Public Property UserId As Integer
        Get
            Return ViewState("userId")
        End Get
        Set(ByVal value As Integer)
            ViewState("userId") = value
            LoadUser()
        End Set
    End Property

    Public Event UserSaved As EventHandler

#End Region

#Region "Events"

    Protected Sub lnkAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAddUser.Click
        If Page.IsValid Then
            SaveUser()
        End If
    End Sub

    Protected Sub lnkLDAPPopulate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLDAPPopulate.Click
        ctrlLdapUserSearch.Visible = True
    End Sub

    Protected Sub ctrlLdapUserSearch_User_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlLdapUserSearch.UserSelected
        LDAPUserSelected()
    End Sub

    'Protected Sub rptrUserRoles_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrUserRoles.ItemDataBound
    '    If e.Item.DataItem IsNot Nothing Then
    '        Dim roleItem As Role = CType(e.Item.DataItem, Role)
    '        Dim lblRole As Label = CType(e.Item.FindControl("lblRole"), Label)
    '        Dim lnkRemove As LinkButton = CType(e.Item.FindControl("lnkRemove"), LinkButton)

    '        lblRole.Text = roleItem.Title
    '        lnkRemove.CommandArgument = roleItem.ID
    '        lnkRemove.CommandName = REMOVE_CMD
    '    End If
    'End Sub

    'Protected Sub rptrUserRoles_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptrUserRoles.ItemCommand
    '    If e.CommandName = REMOVE_CMD Then
    '        UserManager.RevokeUserGlobalRole(SessionManager.LoggedInUserId, CType(e.CommandArgument, Integer))
    '    End If
    'End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadUser()
        Dim currentUser As AptUser = UserManager.GetUser(UserId)
        Dim usersLogin As AptLogin = UserManager.GetLoginForUser(UserId)

        If currentUser IsNot Nothing Then
            txtTitle.Text = currentUser.Title
            txtForename.Text = currentUser.Forename
            txtSurname.Text = currentUser.Surname
            txtUsername.Text = usersLogin.Username
            txtEmail.Text = currentUser.EmailAddress
        Else
            UserId = 0
        End If
    End Sub

    Private Sub SaveUser()

        Dim saveUser As New AptUser
        Dim usersLogin As New AptLogin

        If UserId <> 0 Then
            saveUser = UserManager.GetUser(UserId)
            usersLogin = UserManager.GetLoginForUser(UserId)
        End If

        If UserManager.DoesUsernameExist(txtUsername.Text, UserId) Then
            lblError.Visible = True
            lblError.Text = USERNAME_EXISTS_ERROR
            Exit Sub
        End If

        saveUser.Title = txtTitle.Text
        saveUser.Forename = txtForename.Text
        saveUser.Surname = txtSurname.Text
        usersLogin.Username = txtUsername.Text
        saveUser.EmailAddress = txtEmail.Text

        Try

            If UserId <> 0 Then
                UserManager.UpdateUser(saveUser)
                UserManager.UpdateLogin(usersLogin)
            Else
                UserManager.AddNewUser(saveUser, usersLogin.Username, True, False) ' TODO: implement send email and then enable send email to true?
                UserId = saveUser.ID
            End If

            RaiseEvent UserSaved(Me, New CommandEventArgs("", saveUser.ID))

        Catch ex As UserAlreadyExistsException
            lblError.Text = USERNAME_EXISTS_ERROR
        End Try

    End Sub

    Private Sub LDAPUserSelected()
        Dim selectedUsername As String = ctrlLdapUserSearch.SelectedUser

        ' Get the user from ldap
        Dim selectedUser As User = LDAPManager.GetUser(selectedUsername)

        ' set the page from the user
        ' TODO: ask lloyd what is going on here

        txtTitle.Text = selectedUser.Title
        txtForename.Text = selectedUser.FirstName
        txtSurname.Text = selectedUser.Surname
        txtUsername.Text = selectedUser.Username

        'lblForename.Text = selectedUser.FirstName
        'lblSurname.Text = selectedUser.Surname
        'lblUsername.Text = selectedUser.Username
        'lblEmail.Text = selectedUser.Email

        ctrlLdapUserSearch.Visible = False
    End Sub

#End Region

End Class
