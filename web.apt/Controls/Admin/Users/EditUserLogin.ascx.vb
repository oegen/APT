'----------------------------------------------------------------------------------------------
' Filename    : EditUserLogin.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Admin_Users_EditUserLogin
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event LoginSaveSuccess As EventHandler
    Public Event LoginsaveFail As EventHandler

    Public Property UserId As Integer
        Get
            Return ViewState("userId")
        End Get
        Set(ByVal value As Integer)
            ViewState("userId") = value
            LoadLogin()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        If Page.IsValid Then
            SaveLogin()
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadLogin()
        Dim currentLogin As AptLogin = UserManager.GetLoginForUser(UserId)

        lblName.Text = currentLogin.User.FullName
        txtUsername.Text = currentLogin.Username
        txtPassword.Text = currentLogin.Password

        liDateCreated.Visible = True
        liDateLastLoggedIn.Visible = True
        liDateModified.Visible = True

        If currentLogin.Created.HasValue Then
            lblDateCreated.Text = currentLogin.Created.Value.ToString("dd/MM/yyyy HH:mm:ss")
        Else
            lblDateCreated.Text = "N/A"
        End If

        If currentLogin.Modified.HasValue Then
            lblDateModified.Text = currentLogin.Modified.Value.ToString("dd/MM/yyyy HH:mm:ss")
        Else
            lblDateModified.Text = "N/A"
        End If

        If currentLogin.DateLastLogin.HasValue Then
            lblDateLastLoggedIn.Text = currentLogin.DateLastLogin.Value.ToString("dd/MM/yyyy HH:mm:ss")
        Else
            lblDateLastLoggedIn.Text = "N/A"
        End If

    End Sub

    Private Sub SaveLogin()

        Dim currentLogin As AptLogin = UserManager.GetLoginForUser(UserId)

        If UserManager.DoesUsernameExist(txtUsername.Text, UserId) Then
            lblError.Visible = True
            lblError.Text = "<br /><br /><br /><br />This username is already in use, please choose another one"
            Exit Sub
        End If

        If currentLogin IsNot Nothing Then
            currentLogin.Password = txtPassword.Text
            currentLogin.Username = txtUsername.Text

            Try
                UserManager.UpdateLogin(currentLogin)
                RaiseEvent LoginSaveSuccess(Me, New EventArgs)
            Catch ex As Exception

            End Try

        End If

    End Sub

#End Region

End Class
