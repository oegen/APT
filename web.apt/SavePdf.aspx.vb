'----------------------------------------------------------------------------------------------
' Filename    : SavePdf.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        01/12/2011  First release.
'----------------------------------------------------------------------------------------------
Imports System.Web.Configuration.WebConfigurationManager

Partial Class SavePdf
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Files.Count = 0 Then
            Response.Write("No files received.")
        Else
            ' Get the file from the request
            Dim File As HttpPostedFile
            File = Request.Files.Get(0)
            File.SaveAs(Server.MapPath(AppSettings("DocumentsFolderPath")) + "/" + File.FileName)

            ' Output the file details to the browser
            Response.Write("File Uploaded: " & File.FileName & "<br>")
            Response.Write("Size: " & File.ContentLength & " bytes<br>")
            Response.Write("Type: " & File.ContentType & "<br><br>")
        End If

    End Sub

End Class
