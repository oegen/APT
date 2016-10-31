'----------------------------------------------------------------------------------------------
' Filename    : Annotate.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports System.Configuration.ConfigurationManager

Partial Class PDFAnnotation_Annotate
    Inherits System.Web.UI.Page

    '#Region "Properties"

    '    Private Property DocumentName As String
    '        Get
    '            Return ViewState(Me.UniqueID & "_documentName")
    '        End Get
    '        Set(ByVal value As String)
    '            ViewState(Me.UniqueID & "_documentName") = value
    '        End Set
    '    End Property

    '    Private Property DocumentId As Integer
    '        Get
    '            Return ViewState(Me.UniqueID & "_documentId")
    '        End Get
    '        Set(ByVal value As Integer)
    '            ViewState(Me.UniqueID & "_documentId") = value
    '        End Set
    '    End Property

    '#End Region

    '#Region "Events"

    '    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '        If Page.IsPostBack = False Then
    '            'LoadPDF()
    '            PdfWebControl1.LoadDocument(74)
    '        End If
    '    End Sub

    '#End Region

    '    Private Sub LoadPDF()

    '        Dim projectDoc As ProjectDocument = ProjectDocumentManager.GetProjectDocumentBySecretKey(Request.QueryString("docKey"))
    '        Dim openURL As String

    '        If projectDoc IsNot Nothing Then
    '            OpenURL = ResolveUrl(AppSettings("DocumentsFolderPath") & projectDoc.Path)

    '            Dim latestDocVersion As DocumentVersion = ProjectDocumentManager.GetLatestDocumentVersion(projectDoc.ID)

    '            DocumentId = projectDoc.ID
    '            DocumentName = projectDoc.Name

    '            If latestDocVersion IsNot Nothing Then
    '                ' PdfWebControl1.LoadDocument(latestDocVersion.RadPDFDocumentId)
    '                PdfWebControl1.CopyDocument(latestDocVersion.RadPDFDocumentId, True)
    '            Else
    '                Dim pdfData As Byte() = System.IO.File.ReadAllBytes(Server.MapPath(OpenURL))
    '                PdfWebControl1.CreateDocument(projectDoc.Name, pdfData)
    '                ' This is the first time this document has been opened
    '                ' save it to keep a copy of the original copy
    '                SaveDocumentVersion()
    '            End If
    '        Else
    '            ' 404
    '        End If

    '    End Sub

    '    Protected Sub PdfWebControl1_Saved(ByVal sender As Object, ByVal e As RadPdf.Integration.DocumentSavedEventArgs) Handles PdfWebControl1.Saved
    '        ' We only want to save another copy of the document IF the user has saved the document
    '        'PdfWebControl1.CreateDocument(DocumentName, e.DocumentData)
    '        SaveDocumentVersion()
    '    End Sub

    '    Private Sub SaveDocumentVersion()
    '        Dim newDocumentVersion As New DocumentVersion
    '        newDocumentVersion.Document = ProjectDocumentManager.GetProjectDocumentById(DocumentId)
    '        newDocumentVersion.CreationDateTime = DateTime.Now
    '        newDocumentVersion.RadPDFDocumentId = PdfWebControl1.DocumentID
    '        newDocumentVersion.AptUser = UserManager.GetUser(SessionManager.LoggedInUserId)

    '        ProjectDocumentManager.SaveDocumentVersion(newDocumentVersion)
    '    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If Request.QueryString("docKey") IsNot Nothing Then
                Dim loadDoc As ProjectDocument = ProjectDocumentManager.GetProjectDocumentBySecretKey(Request.QueryString("docKey"))

                If loadDoc IsNot Nothing Then
                    If Request.QueryString("readOnly") IsNot Nothing Then
                        If RadPdfBelongsToDocument(Request.QueryString("radPdfId"), loadDoc.ID) Then
                            ctrlDocViewer.LoadPDFReadOnly(Request.QueryString("radPdfId"))
                        Else
                            ' User tried to access a document via querystring
                            Response.Redirect("~/Default.aspx")
                        End If
                    Else
                        ctrlDocViewer.LoadPDF(Request.QueryString("docKey"))
                    End If
                Else
                    Response.Redirect("~/Default.aspx")
                End If
            Else
                Response.Redirect("~/Default.aspx")
            End If
        End If

    End Sub

End Class
