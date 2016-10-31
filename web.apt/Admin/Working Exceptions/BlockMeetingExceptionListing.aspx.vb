'----------------------------------------------------------------------------------------------
' Filename    : BlockMeetingExceptionListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        05/12/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Admin_Working_Exceptions_BlockMeetingExceptionListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub ctrlBlockMeetingExceptionListing_WorkingExceptionRemoved(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlBlockMeetingExceptionListing.WorkingExceptionRemoved
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Block meeting exception has been removed")
    End Sub

#End Region

End Class
