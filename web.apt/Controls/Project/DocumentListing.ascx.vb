'----------------------------------------------------------------------------------------------
' Filename    : DocumentListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports GenericUtilities
Imports System.Collections.Generic
Imports System.IO

Partial Class Controls_Generic_DocumentListing
    Inherits System.Web.UI.UserControl

#Region "Private Fields"

    Private _documentList As New List(Of ProjectDocument)
    Private _allowedAnnotationExtensions As New List(Of String)
    Private _deletionConfirmationMessage As String = "Are you sure you want to delete this document?"
    Private _annotatePdfUrl As String = "~/PDFAnnotation/Annotate.aspx?docKey={0}"
    Private _javaAnnotatePdfUrl As String = "~/PdfTest.aspx?docKey={0}"

#End Region

#Region "Properties"

    Public Property PageSize As Integer
        Get
            Return grdvDocuments.PageSize
        End Get
        Set(ByVal value As Integer)
            grdvDocuments.PageSize = value
        End Set
    End Property

    Public Property DocumentList As List(Of ProjectDocument)
        Get
            Return _documentList
        End Get
        Set(ByVal value As List(Of ProjectDocument))
            _documentList = value
        End Set
    End Property

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

    Public Property CategoryId As Integer
        Get
            Return ViewState(Me.UniqueID & "_categoryId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_categoryId") = value
            BindData()
        End Set
    End Property

    Private WriteOnly Property HasDocuments As Boolean
        Set(ByVal value As Boolean)
            If value = True Then
                pnlDocuments.Visible = True
                pnlNoItems.Visible = False
            Else
                pnlDocuments.Visible = False
                pnlNoItems.Visible = True
            End If
        End Set
    End Property

    Private Property ShowAnnotate As Boolean
        Get
            Return ViewState(Me.UniqueID & "_showAnnotate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.UniqueID & "_showAnnotate") = value
        End Set
    End Property
#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        InitialiseAllowedExtensions()

        If Page.IsPostBack = False Then

            ShowAnnotate = PermissionsManager.CanAnnotateDocuments(SessionManager.LoggedInUserId, ProjectId)

            If CategoryId = 0 Then
                BindData()
            End If
        End If

    End Sub

    Protected Sub grdvDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvDocuments.PageIndexChanging
        grdvDocuments.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub grdvDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvDocuments.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim currentDocument As ProjectDocument = CType(e.Row.DataItem, ProjectDocument)
            Dim hypDocumentName As HyperLink = CType(e.Row.FindControl("hypDocumentName"), HyperLink)
            Dim lblUsername As Label = CType(e.Row.FindControl("lblUsername"), Label)
            Dim lblDateUploaded As Label = CType(e.Row.FindControl("lblDateUploaded"), Label)
            Dim lblCategory As Label = CType(e.Row.FindControl("lblCategory"), Label)
            Dim hypAnnotate As HyperLink = CType(e.Row.FindControl("hypAnnotate"), HyperLink)
            Dim javaAnnotate As HyperLink = CType(e.Row.FindControl("hypJavaAnnotate"), HyperLink)
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            Dim lnkHistory As LinkButton = CType(e.Row.FindControl("lnkHistory"), LinkButton)
            Dim lblSize As Label = CType(e.Row.FindControl("lblSize"), Label)
            Dim lnkDocumentName As LinkButton = CType(e.Row.FindControl("lnkDocumentName"), LinkButton)

            With currentDocument

                hypDocumentName.Text = .Name
                hypDocumentName.NavigateUrl = SiteUtils.GetFilePath(currentDocument)

                Dim currentLogin As AptLogin = UserManager.GetLoginForUser(.User.ID)

                If currentLogin IsNot Nothing Then
                    lblUsername.Text = currentLogin.Username
                End If

                lblDateUploaded.Text = .Created
                lblCategory.Text = .Category.Name

                Dim fInfo As FileInfo = New FileInfo(modUtilities.GetPhysicalPath((String.Format("{0}{1}", AppSettingsGet.DocumentsFolderPath, .Path))))

                If fInfo IsNot Nothing Then
                    lblSize.Text = String.Format("{0} {1}", FormatNumber((fInfo.Length / 1024) / 1024, 2), "MB") ' Show in MBs
                End If

                If ShowAnnotate = True AndAlso _allowedAnnotationExtensions.Contains(.FileExtension) Then
                    hypAnnotate.NavigateUrl = String.Format(_annotatePdfUrl, currentDocument.SecretKey)
                    javaAnnotate.NavigateUrl = String.Format(_javaAnnotatePdfUrl, currentDocument.SecretKey)
                    hypAnnotate.Visible = True
                Else
                    hypAnnotate.Visible = False
                End If

                ' quick hack to show old pdf annotation

                If Request.QueryString("showOld") IsNot Nothing Then

                    javaAnnotate.Visible = True
                    javaAnnotate.NavigateUrl = String.Format(_javaAnnotatePdfUrl, currentDocument.SecretKey)

                End If

                lnkHistory.CommandArgument = .SecretKey
                lnkDelete.CommandArgument = .ID
                modUtilities.AddConfirmBoxToLinkButton(lnkDelete, _deletionConfirmationMessage)

                lnkDocumentName.Text = .Name
                lnkDocumentName.CommandArgument = .ID

            End With

        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim projectDocumentId As Integer = CType(sender, LinkButton).CommandArgument
        ProjectDocumentManager.RemoveProjectDocument(projectDocumentId, SessionManager.LoggedInUserId)
        BindData()

    End Sub

    Protected Sub lnkHistory_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim secretKey As String = CType(sender, LinkButton).CommandArgument
        Response.Redirect(String.Format("~/Project/DocumentHistory.aspx?ProjectId={0}&key={1}", ProjectId, secretKey))
    End Sub

    Protected Sub lnkOpen_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim documentId As String = CType(sender, LinkButton).CommandArgument
        Dim openDocument As ProjectDocument = ProjectDocumentManager.GetProjectDocumentById(documentId)

        Dim fs
        Dim fileName As String = "BroadcastHistory"

        fs = System.IO.File.Open(Server.MapPath(SiteUtils.GetFilePath(openDocument)), System.IO.FileMode.Open)
        Dim btFile(fs.Length) As Byte
        fs.Read(btFile, 0, fs.Length)
        fs.Close()

        With Response

            .AddHeader("Content-disposition", "attachment;filename=" & openDocument.Name)
            .ContentType = "application/octet-stream"
            .BinaryWrite(btFile)
            .End()

        End With

    End Sub

#End Region

#Region "Public Methods"

    Public Sub BindData()

        Dim projectDocuments As List(Of ProjectDocument) = ProjectDocumentManager.GetProjectsDocuments(ProjectId, CategoryId)

        If projectDocuments.Count = 0 Then
            HasDocuments = False
            If CategoryId = 0 Then
                ltlNoItem.Text = "No Documents have been uploaded for this project"
            End If
        Else
            HasDocuments = True
        End If

        grdvDocuments.DataSource = projectDocuments
        grdvDocuments.DataBind()

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub InitialiseAllowedExtensions()
        _allowedAnnotationExtensions.Add(".pdf")
    End Sub

#End Region

End Class
