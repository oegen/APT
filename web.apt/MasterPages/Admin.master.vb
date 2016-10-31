'----------------------------------------------------------------------------------------------
' Filename    : Admin.master.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        06/10/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic

Partial Class MasterPages_Admin
    Inherits System.Web.UI.MasterPage



    Public Sub DisplayConfirmationMessage(ByVal message As String, Optional ByVal messageBoxType As MessageBoxType = MessageBoxType.Success, _
                                      Optional ByVal timeOut As Integer = 2)

        CType(Me.Master, MasterPage).DisplayConfirmationMessage(message, messageBoxType, timeOut)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If UserManager.UserHasGlobalRole(SessionManager.LoggedInUserId, AppSettingsGet.AdminRoleID) = False Then
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

End Class

