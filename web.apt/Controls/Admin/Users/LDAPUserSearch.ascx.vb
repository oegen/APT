'----------------------------------------------------------------------------------------------
' Filename    : LDAPUserSearch.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports GenericUtilities
Imports aptBusinessLogic
Imports System.Collections.Generic

Partial Class Controls_Admin_Users_LDAPUserSearch
    Inherits System.Web.UI.UserControl

#Region "Constants"

    Private Const USER_SELECTED_CMD As String = "user_selected"

#End Region

#Region "Properties"

    Public Property SelectedUser As String
        Get
            Return ViewState("_selectedUser")
        End Get
        Set(ByVal value As String)
            ViewState("_selectedUser") = value
        End Set
    End Property

    Public WriteOnly Property LabelText As String
        Set(ByVal value As String)
            lblAttribute.Text = value
        End Set
    End Property

    Public Event UserSelected As EventHandler

#End Region

#Region "Events"

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearch.Click
        If Page.IsValid Then
            SearchUsers()
        End If
    End Sub

    Protected Sub gvSearchResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSearchResults.RowDataBound
        If e.Row.DataItem IsNot Nothing Then
            Dim currentUser As User = CType(e.Row.DataItem, User)
            Dim lblName As Label = CType(e.Row.FindControl("lblName"), Label)
            Dim lblUsername As Label = CType(e.Row.FindControl("lblUsername"), Label)
            Dim lblDepartment As Label = CType(e.Row.FindControl("lblDepartment"), Label)
            Dim lblJobTitle As Label = CType(e.Row.FindControl("lblJobTitle"), Label)
            Dim lblEmail As Label = CType(e.Row.FindControl("lblEmail"), Label)
            Dim lnkSelect As LinkButton = CType(e.Row.FindControl("lnkSelect"), LinkButton)

            lblName.Text = currentUser.FullName
            lblUsername.Text = currentUser.Username
            lblDepartment.Text = currentUser.Department
            lblJobTitle.Text = currentUser.Title
            lblEmail.Text = currentUser.Email
            lnkSelect.CommandArgument = currentUser.Username
            lnkSelect.CommandName = USER_SELECTED_CMD
        End If
    End Sub

    Protected Sub gvSearchResults_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSearchResults.RowCommand
        If e.CommandName = USER_SELECTED_CMD Then
            SelectedUser = e.CommandArgument

            RaiseEvent UserSelected(Me, New EventArgs())
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SearchUsers()
        Dim userList As List(Of User) = LDAPManager.SearchUsers(txtSearch.Text)

        gvSearchResults.DataSource = userList
        gvSearchResults.DataBind()
    End Sub

#End Region


End Class
