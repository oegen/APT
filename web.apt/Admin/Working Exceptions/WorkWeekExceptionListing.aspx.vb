'----------------------------------------------------------------------------------------------
' Filename    : WorkWeekExceptionListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        05/12/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Admin_Working_Exceptions_WorkWeekExceptionListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub ctrlWorkingWeekExceptionListing_WorkingWeekExceptionRemoved(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlWorkingWeekExceptionListing.WorkingWeekExceptionRemoved
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Working week exception has been removed")
    End Sub

#End Region

End Class
