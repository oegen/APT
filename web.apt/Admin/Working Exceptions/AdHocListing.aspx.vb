'----------------------------------------------------------------------------------------------
' Filename    : AdHocListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        05/12/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Admin_Working_Exceptions_AdHocListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub ctrlAdHocListing_AdhocExceptionRemoved(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAdHocListing.AdhocExceptionRemoved
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Ad hoc exception has been removed")
    End Sub

#End Region

End Class
