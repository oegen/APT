'----------------------------------------------------------------------------------------------
' Filename    : ProjectRoleGlobalChange.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        11/11/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities
Imports System.Collections.Generic

Partial Class Controls_Admin_ProjectRoleGlobalChange_ProjectRoleGlobalChange
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event ProjectRolesSwitched As EventHandler

#End Region

#Region "Constants and Enumerations"

    Private ERROR_MESSAGE As String = "<br/><br/><br/><br/>You must select both users to continue"


#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            BindProjectRoles()
        End If
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        If Page.IsValid Then
            ApplyNewUserToOldUserRoles()
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub ApplyNewUserToOldUserRoles()

        If ctrlOldUser.SelectedUserId <> 0 AndAlso ctrlNewUser.SelectedUserId <> 0 Then
            UserManager.SwitchProjectRoleUser(ddlProjectRoles.SelectedValue, ctrlOldUser.SelectedUserId, ctrlNewUser.SelectedUserId)
            RaiseEvent ProjectRolesSwitched(Me, New EventArgs)
        Else
            lblError.Text = ERROR_MESSAGE
        End If

    End Sub

    Private Sub BindProjectRoles()
        Dim allProjectRoles As List(Of Role) = UserManager.GetAllProjectRoles()
        modComponent.BindDropDown(ddlProjectRoles.DropDownList, allProjectRoles, "ID", "Title", "Role")
    End Sub

#End Region


End Class
