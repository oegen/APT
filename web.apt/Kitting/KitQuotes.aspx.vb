'----------------------------------------------------------------------------------------------
' Filename    : KitQuotes.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        21/02/2012  First release.
'----------------------------------------------------------------------------------------------

Partial Class Kitting_KitQuotes
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("kitId") Then
            ctrlKitQuoteListing.KitId = Request.QueryString("kitId")
        End If

        If IsNumeric(Request.QueryString("projectId")) Then
            ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
            ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
            hypKit.NavigateUrl = String.Format("~/Kitting/KitListing.aspx?projectId={0}", Request.QueryString("projectId"))
        End If

    End Sub

#End Region

End Class
