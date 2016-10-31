'----------------------------------------------------------------------------------------------
' Filename    : Login.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic

Partial Class Login
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub lnkLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogin.Click

        If Page.IsValid Then

            Dim errorMessage As String = ""
            Dim userId As Integer = 0

            If UserManager.VerifyUserLogin(txtUsername.Text, txtPassword.Text, userId, errorMessage) = True Then
                ' logged in
                ' change the last login time
                ' set the session for the login
                SessionManager.LoggedInUserId = userId
                Response.Redirect("~/Default.aspx")
            Else
                ' login failed
                lblError.Text = errorMessage
            End If

        End If

    End Sub

#End Region

End Class
