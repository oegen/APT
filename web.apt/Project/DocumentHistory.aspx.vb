'----------------------------------------------------------------------------------------------
' Filename    : DocumentHistory.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        04/10/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Project_DocumentHistory
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("projectId")) Then
                ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
                ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
            End If

            If Request.QueryString("key") IsNot Nothing Then
                Dim tmpDoc As ProjectDocument = ProjectDocumentManager.GetProjectDocumentBySecretKey(Request.QueryString("key"))

                If tmpDoc IsNot Nothing Then
                    ctrlDocumentHistoryListing.DocumentId = tmpDoc.ID
                End If
            End If

        End If

    End Sub

#End Region

End Class
