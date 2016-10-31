'----------------------------------------------------------------------------------------------
' Filename    : BlockMeetingException.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        02/12/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Admin_Working_Exceptions_BlockMeetingException
    Inherits System.Web.UI.Page

    Protected Sub ctrlBlockMeetingException_SaveBlockMeetingSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlBlockMeetingException.SaveBlockMeetingSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Block Meeting Exception has been saved")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("blockMeetingExId")) Then
                ctrlBlockMeetingException.BlockMeetingExceptionId = Request.QueryString("blockMeetingExId")
            End If
        End If

    End Sub

End Class
