'----------------------------------------------------------------------------------------------
' Filename    : DocumentOverview.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports GenericUtilities
Imports aptEntities
Imports aptBusinessLogic
Imports System.IO
Imports System.Collections.Generic

Partial Class Controls_Project_DocumentOverview
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event UploadSuccess As CommandEventHandler
    Public Event UploadFail As EventHandler

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            Initialise()
        End If

    End Sub

    Protected Sub ddlProjectCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProjectCategory.SelectedIndexChanged
        ctlDocumentListing.CategoryId = ddlProjectCategory.SelectedValue
    End Sub

    Protected Sub btnUploadDocument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadDocument.Click

        If Page.IsValid Then
            If ddlProjectCategory.SelectedValue <> 0 Then
                UploadFile()
            Else
                lblError.Text = "Select a category to upload to"
            End If

        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub Initialise()

        Dim activeProjectCategories As List(Of ProjectDocumentCategory) = _
                ProjectDocumentCategoryManager.GetProjectDocumentCategories()

        modComponent.BindDropDown(ddlProjectCategory, activeProjectCategories, "Id", "Name", "category", "- select a {0} - ")

        ctlDocumentListing.ProjectId = ProjectId

    End Sub

    Private Sub UploadFile()

        If fuDocument.UploadedFiles.Count > 0 Then

            Dim userId As Integer = SessionManager.LoggedInUserId
            Dim selectedDocumentCategoryId As Integer = ddlProjectCategory.SelectedValue

            For Each file As Telerik.Web.UI.UploadedFile In fuDocument.UploadedFiles
                Dim errorMessage As String = ""
                Dim fileUploadSuccess As Boolean = False
                Dim documentId As Integer = 0

                fileUploadSuccess = ProjectDocumentManager.UploadProjectDocument(AppSettingsGet.DocumentsFolderPath, file.FileName, _
                                                                            file.InputStream, _
                                                                            ProjectId, userId, _
                                                                            selectedDocumentCategoryId, errorMessage, _
                                                                            documentId)
                If fileUploadSuccess = True Then
                    RaiseEvent UploadSuccess(Me, New CommandEventArgs("Uploaded Document", documentId))
                Else
                    RaiseEvent UploadFail(Me, New EventArgs())
                End If
            Next
            ctlDocumentListing.BindData()
        Else
            lblError.Text = "Select a file to upload"
        End If

    End Sub

#End Region

End Class
