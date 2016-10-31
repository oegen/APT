'----------------------------------------------------------------------------------------------
' Filename    : UserDisplayAndSelect.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Generic_UserDisplayAndSelect
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property SelectedUserId As Integer
        Get
            Return ViewState(String.Format("selectedUser{0}", Me.UniqueID))
        End Get
        Set(ByVal value As Integer)
            ViewState(String.Format("selectedUser{0}", Me.UniqueID)) = value
            SetUser()
        End Set
    End Property

    Public Property Enabled As Boolean
        Get
            Return lnkChange.Visible
        End Get
        Set(ByVal value As Boolean)
            lnkChange.Visible = value
        End Set
    End Property

    Public Property ErrorMessage() As String
        Get
            Return vldUser.ErrorMessage
        End Get
        Set(ByVal value As String)
            vldUser.ErrorMessage = value
        End Set
    End Property

    Public Property ValidationGroup As String
        Get
            Return vldUser.ValidationGroup
        End Get
        Set(ByVal value As String)

            If String.IsNullOrEmpty(value) Then
                vldUser.Enabled = False
                vldUser.ValidationGroup = value
            Else
                vldUser.Enabled = True
                vldUser.ValidationGroup = value
            End If

        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub lnkChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkChange.Click
        ctrlSearchuser.Visible = True
        pnlUserViewer.Visible = False
    End Sub

    Protected Sub ctrlSearchuser_UserSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlSearchuser.UserSelected
        ctrlSearchuser.Visible = False
        pnlUserViewer.Visible = True

        SelectedUserId = ctrlSearchuser.SelectedUserId
    End Sub

    Protected Sub vldUser_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles vldUser.ServerValidate
        If lblUser.Text = "" Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetUser()
        'Dim selectedUserLogin As AptLogin = UserManager.GetLoginForUser(SelectedUserId)
        Dim selectUser As AptUser = UserManager.GetUser(SelectedUserId)
        ' lblUser.Text = selectedUserLogin.Username
        lblUser.Text = selectUser.FullName
    End Sub

#End Region

End Class
