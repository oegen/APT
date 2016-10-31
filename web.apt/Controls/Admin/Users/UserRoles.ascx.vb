Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic

Partial Class Controls_Admin_Users_UserRoles
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property UserId As Integer
        Get
            Return ViewState(Me.UniqueID & "_userId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_userId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindUserRoles()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindUserRoles()

        Dim userRoles As List(Of UserRole) = UserManager.GetUserRoleByUser(UserId)
        Dim bindUserRoles As New List(Of ListItem)

        lbCurrentRoles.Items.Clear() ' This is used when we want to "rebind" the list box

        For Each tmpUserRoles In userRoles

            Dim userRoleListItem As ListItem = New ListItem()

            userRoleListItem.Text = tmpUserRoles.Role.Title
            userRoleListItem.Value = tmpUserRoles.ID

            lbCurrentRoles.Items.Add(userRoleListItem)
        Next

        Dim availableRoles As List(Of Role) = UserManager.GetGlobalRolesNotAssignedToUser(UserId)

        lbAvailableRoles.DataSource = availableRoles
        lbAvailableRoles.DataValueField = "ID"
        lbAvailableRoles.DataTextField = "Title"
        lbAvailableRoles.DataBind()

    End Sub

    Private Sub AddRolesToUser()

        Dim selectedRoles As List(Of Role) = GetSelectedRoles(lbAvailableRoles)

        For Each selectedRole As Role In selectedRoles

            Dim saveUserRole As New UserRole
            saveUserRole.User = UserManager.GetUser(UserId)
            saveUserRole.Role = selectedRole

            UserManager.AddUserRole(saveUserRole)

        Next

    End Sub

    Private Sub RemoveRolesFromUser()

        Dim selectedUserRoles As List(Of UserRole) = GetSelectedUserRoles(lbCurrentRoles)

        For Each selectedRole As UserRole In selectedUserRoles
            UserManager.RemoveRoleFromUser(selectedRole)
        Next

    End Sub

#End Region

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAdd.Click

        If Page.IsValid Then
            AddRolesToUser()
            BindUserRoles()
        End If

    End Sub

    Private Function GetSelectedRoles(ByRef currentListBox As ListBox) As List(Of Role)

        Dim selectedRoles As New List(Of Role)

        For Each role As ListItem In currentListBox.Items

            If role.Selected = True Then
                Dim addRole As Role = UserManager.GetRoleByID(role.Value)
                selectedRoles.Add(addRole)
            End If

        Next

        Return selectedRoles

    End Function

    Private Function GetSelectedUserRoles(ByRef currentListBox As ListBox) As List(Of UserRole)

        Dim userRoles As New List(Of UserRole)

        For Each role As ListItem In currentListBox.Items

            If role.Selected = True Then
                Dim userRole As UserRole = UserManager.GetUserRole(role.Value)
                userRoles.Add(userRole)
            End If

        Next

        Return userRoles

    End Function

    Protected Sub lnkRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRemove.Click

        If Page.IsValid Then
            RemoveRolesFromUser()
            BindUserRoles()
        End If

    End Sub

End Class
