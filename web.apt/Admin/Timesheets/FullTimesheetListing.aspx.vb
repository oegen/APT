'----------------------------------------------------------------------------------------------
' Filename    : FullTimesheetListing.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        29/11/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Admin_Timesheets_FullTimesheetListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub lnkProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkProject.Click
        ctrlFullTimesheetListing.SearchTimesheetsByProjectId(txtProject.Text)
    End Sub

    Protected Sub lnkSurname_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSurname.Click
        ctrlFullTimesheetListing.SearchTimesheetsBySurname(txtSurname.Text)
    End Sub

    Protected Sub lnkDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDate.Click
        ctrlFullTimesheetListing.SearchTimesheetsByDate(txtDate.Text)
    End Sub

#End Region

End Class
