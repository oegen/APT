'----------------------------------------------------------------------------------------------
' Filename    : ProjectRoleGlobalChange.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        11/11/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Admin_Project_Role_Global_Change_ProjectRoleGlobalChange
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub ctrlProjectRoleGlobalChange_ProjectRolesSwitched(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlProjectRoleGlobalChange.ProjectRolesSwitched
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("The roles of the old user have now been give to the new user")
    End Sub

#End Region

End Class
