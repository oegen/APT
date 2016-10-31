'----------------------------------------------------------------------------------------------
' Filename    : DocumentViewer.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        04/10/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports RadPdf.Data.Document
Imports System.IO

Partial Class Controls_Project_DocumentViewer
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Private Property DocumentName As String
        Get
            Return ViewState(Me.UniqueID & "_documentName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.UniqueID & "_documentName") = value
        End Set
    End Property

    Private Property DocumentId As Integer
        Get
            Return ViewState(Me.UniqueID & "_documentId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_documentId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub PdfWebControl1_Saved(ByVal sender As Object, ByVal e As RadPdf.Integration.DocumentSavedEventArgs) Handles PdfWebControl1.Saved
        ' We only want to save another copy of the document IF the user has saved the document
        SaveDocumentVersion()
    End Sub

#End Region

#Region "Public Methods"

    Public Sub LoadPDF(ByVal secretKey As String)

        ' When loading a PDF that the user can modify it should only ever be the LATEST

        ' PdfWebControl1.AllowFindMatchingPdf = False
        PdfWebControl1.RenderDpi = 400
        PdfWebControl1.ViewerZoomDefault = RadPdf.Web.UI.ViewerZoom.ZoomFitAll

        Dim projectDoc As ProjectDocument = ProjectDocumentManager.GetProjectDocumentBySecretKey(secretKey)
        Dim openURL As String

        If projectDoc IsNot Nothing Then
            openURL = ResolveUrl(AppSettingsGet.DocumentsFolderPath & projectDoc.Path)

            Dim latestDocVersion As DocumentVersion = ProjectDocumentManager.GetLatestDocumentVersion(projectDoc.ID)

            DocumentId = projectDoc.ID
            DocumentName = projectDoc.Name

            If latestDocVersion IsNot Nothing Then
                PdfWebControl1.CopyDocument(latestDocVersion.RadPDFDocumentId, True)
            Else

                Dim pdfData As Byte() = System.IO.File.ReadAllBytes(Server.MapPath(openURL))

                Using fStream As New FileStream(Server.MapPath(openURL), FileMode.Open)
                    PdfWebControl1.CreateDocument(projectDoc.Name, fStream)
                End Using

                'PdfWebControl1.CreateDocument(projectDoc.Name, pdfData)
            End If
        Else
            ' 404
            Throw New Exception("Invalid Key")
        End If

    End Sub


    Public Sub LoadPDFReadOnly(ByVal radPdfDocId As Integer)
        SetReadOnlySettings()
        PdfWebControl1.LoadDocument(radPdfDocId)
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SaveDocumentVersion()
        Dim newDocumentVersion As New DocumentVersion
        newDocumentVersion.Document = ProjectDocumentManager.GetProjectDocumentById(DocumentId)
        newDocumentVersion.CreationDateTime = DateTime.Now
        newDocumentVersion.RadPDFDocumentId = PdfWebControl1.DocumentID
        newDocumentVersion.AptUser = UserManager.GetUser(SessionManager.LoggedInUserId)

        ProjectDocumentManager.SaveDocumentVersion(newDocumentVersion)
    End Sub

    Private Sub SetReadOnlySettings()
        PdfWebControl1.HideTopBar = True
        PdfWebControl1.HideToolsAnnotateTab = True
        PdfWebControl1.HideToolsInsertTab = True
        PdfWebControl1.HideSideBar = True
        PdfWebControl1.HideViewMenu = False
    End Sub

#End Region

End Class
