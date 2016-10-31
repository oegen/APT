'----------------------------------------------------------------------------------------------
' Filename    : SaveAnnotate.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration.ConfigurationManager
Imports aptBusinessLogic
Imports aptEntities

Partial Class PDFAnnotation_SaveAnnotate
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If String.IsNullOrEmpty(Request.QueryString("docKey")) = False Then
            If Request.Files.Count = 0 Then
                Response.Write("No files received.")
            Else
                ' Get the file from the request
                Dim annotatedDocument As ProjectDocument = ProjectDocumentManager.GetProjectDocumentBySecretKey(Request.QueryString("docKey"))

                Dim File As HttpPostedFile
                File = Request.Files.Get(0)
                ' File.SaveAs(Server.MapPath(AppSettings("DocumentsFolderPath")) & File.FileName)
                File.SaveAs(Server.MapPath("~/test.pdf"))

                AuditTrailManager.PostAudit("Document {0} has been annotated", annotatedDocument.Name, _
                                            annotatedDocument.Project.ID, AppSettingsGet.EditAuditChangeTypeID, _
                                            AppSettingsGet.ProjectDocumentSectionID)

            End If
        End If

    End Sub

#End Region

End Class

