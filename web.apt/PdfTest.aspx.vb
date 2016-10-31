'----------------------------------------------------------------------------------------------
' Filename    : PdfTest.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        01/12/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class PdfTest
    Inherits System.Web.UI.Page

    Private _openUrl As String

    Public Property OpenURL As String
        Get
            Return _openUrl
        End Get
        Set(ByVal value As String)
            _openUrl = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        plcBindingFix.DataBind()
        If Request.QueryString("docKey") IsNot Nothing Then
            LoadPDF()
        End If

    End Sub

    Private Sub LoadPDF()

        Dim loadDoc As ProjectDocument = ProjectDocumentManager.GetProjectDocumentBySecretKey(Request.QueryString("docKey"))

        If loadDoc IsNot Nothing Then
            _openUrl = ResolveUrl(AppSettingsGet.DocumentsFolderPath & loadDoc.Path)
        End If

    End Sub

End Class
