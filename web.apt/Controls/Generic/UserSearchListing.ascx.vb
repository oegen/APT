'----------------------------------------------------------------------------------------------
' Filename    : UserSearchListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_User_UserSearchListing
    Inherits System.Web.UI.UserControl

#Region "Constants"
    Private Const COMMAND_NAME As String = "select_click"
#End Region

#Region "Properties"

    Public Property SelectedUserId As Integer
        Get
            Return ViewState("_selectedUserId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_selectedUserId") = value
        End Set
    End Property

    #End Region

#Region "Events"

    Public Event UserSelected As CommandEventHandler

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearch.Click
        Dim userList As List(Of AptUser) = UserManager.SearchUsers(txtSearch.Text)

        gvSearchResults.DataSource = userList
        gvSearchResults.DataBind()

        If userList.Count = 0 Then
            gvSearchResults.Visible = False
            lblNoResults.Visible = True
        Else
            gvSearchResults.Visible = True
            lblNoResults.Visible = False
        End If
    End Sub

    Protected Sub gvSearchResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSearchResults.RowDataBound
        If e.Row.DataItem IsNot Nothing Then
            Dim currentUser As AptUser = CType(e.Row.DataItem, AptUser)
            Dim userLogin As AptLogin = UserManager.GetLoginForUser(currentUser.ID)
            Dim lblUsername As Label = CType(e.Row.FindControl("lblUsername"), Label)
            Dim lblFullName As Label = CType(e.Row.FindControl("lblFullName"), Label)
            Dim lnkSelect As LinkButton = CType(e.Row.FindControl("lnkSelect"), LinkButton)

            lblUsername.Text = userLogin.Username
            lblFullName.Text = currentUser.FullName
            lnkSelect.CommandName = COMMAND_NAME
            lnkSelect.CommandArgument = currentUser.ID
        End If
    End Sub

    Protected Sub gvSearchResults_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSearchResults.RowCommand
        If e.CommandName = COMMAND_NAME Then
            SelectedUserId = CType(e.CommandArgument, Integer)

            Dim selectedUser As AptUser = UserManager.GetUser(SelectedUserId)

            txtSearch.Text = selectedUser.FullName
            gvSearchResults.Visible = False

            RaiseEvent UserSelected(Me, New CommandEventArgs("User Selected", SelectedUserId))
        End If
    End Sub

#End Region

End Class
