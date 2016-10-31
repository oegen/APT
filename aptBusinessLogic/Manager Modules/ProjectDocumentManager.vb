'----------------------------------------------------------------------------------------------
' Filename    : ProjectDocumentManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptDAL
Imports GenericDAL
Imports GenericUtilities
Imports System.IO

Public Module ProjectDocumentManager

    Public ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property


#Region "Get Variations"

#Region "Project Document"

    Public Function GetProjectDocumentById(ByVal projectDocumentId As Integer) As ProjectDocument
        Return DAOGetter.ProjectDocumentDAO(context).GetByID(projectDocumentId)
    End Function

    Public Function GetProjectsDocuments(ByVal projectId As Integer, Optional ByVal categoryId As Integer = 0, Optional ByVal active As Boolean = True) As List(Of ProjectDocument)

        Dim projectDocumentList As New List(Of ProjectDocument)
        Dim finalProjectList As New List(Of ProjectDocument)

        If categoryId <> 0 Then
            projectDocumentList = DAOGetter.ProjectDocumentDAO(Context).GetByProjectAndCategory(projectId, categoryId)
        Else
            projectDocumentList = DAOGetter.ProjectDocumentDAO(Context).GetByProject(projectId)
        End If

        Return projectDocumentList

    End Function

    Public Function GetProjectDocumentBySecretKey(ByVal secretKey As String) As ProjectDocument
        Return DAOGetter.ProjectDocumentDAO(Context).GetProjectDocumentBySecretKey(secretKey)
    End Function

#End Region

#Region "Document Version"

    Public Function GetLatestDocumentVersion(ByVal documentId As Integer) As DocumentVersion
        Return DAOGetter.DocumentVersionDAO(Context).GetLatestVersion(documentId)
    End Function

    Public Function GetDocumentVersions(ByVal documentId As Integer) As List(Of DocumentVersion)
        Return DAOGetter.DocumentVersionDAO(Context).GetVersions(documentId)
    End Function

    Public Function GetDocumentVersion(ByVal docVersionId As Integer) As DocumentVersion
        Return DAOGetter.DocumentVersionDAO(Context).GetByID(docVersionId)
    End Function

#End Region

#End Region

#Region "Insertion / Update / Removal"

#Region "Project Document"

    Public Sub AddNewProjectDocument(ByVal newProjectDocument As ProjectDocument)
        DAOGetter.ProjectDocumentDAO(Context).Insert(newProjectDocument)
    End Sub

    Public Function UploadProjectDocument(ByVal savePath As String,
                                      ByVal fileName As String,
                                      ByVal fileInputStream As Stream,
                                      ByVal projectId As Integer,
                                      ByVal uploaderUserId As Integer,
                                      ByVal projectDocumentCategoryId As Integer,
                                      Optional ByRef errorMessage As String = "",
                                      Optional ByRef documentId As Integer = 0) As Boolean

        Dim fileUploadSuccess As Boolean = False
        Dim uploadedProjectDocument As New ProjectDocument

        ' We have to use this to get the filename because there is a bug in IE7 where it will 
        ' get the full path instead of the filename for some reason (file upload control)

        Dim saveFileName As String = System.IO.Path.GetFileName(fileName)

        ' Check the length of the filename 

        If saveFileName.Length > AppSettingsGet.FilenameMaxLength Then
            saveFileName = saveFileName.Substring(0, AppSettingsGet.FilenameMaxLength).ToString
        End If

        With uploadedProjectDocument
            .Project = ProjectManager.GetProject(projectId)
            .Category = ProjectDocumentCategoryManager.GetProjectDocumentCategory(projectDocumentCategoryId)
            .User = UserManager.GetUser(uploaderUserId)
            .Created = DateTime.Now
            .Modified = DateTime.Now
            .Path = ""
            .Active = True
            .Name = saveFileName
            .SecretKey = modAccount.GenerateRandomPassword(AppSettingsGet.PasswordCharacters, _
                                                           AppSettingsGet.PasswordNumerics, _
                                                           AppSettingsGet.PasswordLength)

            While DoesSecretKeyExist(.SecretKey)

                .SecretKey = modAccount.GenerateRandomPassword(AppSettingsGet.PasswordCharacters, _
                                                               AppSettingsGet.PasswordNumerics, _
                                                               AppSettingsGet.PasswordLength)
            End While

        End With

        ProjectDocumentManager.AddNewProjectDocument(uploadedProjectDocument)

        If uploadedProjectDocument.ID <> 0 Then

            Try

                Dim fullSavePath As String = savePath & uploadedProjectDocument.ID & Path.GetExtension(fileName)
                Dim physicalPath As String = modUtilities.GetPhysicalPath(fullSavePath)

                modUtilities.SaveFile(fileInputStream, physicalPath)
                uploadedProjectDocument.Path = uploadedProjectDocument.ID & Path.GetExtension(fileName)
                ProjectDocumentManager.UpdateProject(uploadedProjectDocument)

                fileUploadSuccess = True
                documentId = uploadedProjectDocument.ID

                AuditTrailManager.PostAudit(String.Format("A new document {0} has been uploaded", fileName),
                                            uploaderUserId, projectId,
                                            AppSettingsGet.AddAuditChangeTypeID,
                                            AppSettingsGet.ProjectDocumentSectionID)
            Catch ex As Exception

                Dim logError As String = String.Format("Error occured when user tried to upload the document {0}", fileName)
                logError += Environment.NewLine
                logError += ex.ToString

                If ex.InnerException IsNot Nothing Then
                    logError += Environment.NewLine
                    logError += ex.InnerException.ToString
                End If

                modLogManager.LogError(logError)
                ProjectDocumentManager.DeleteProjectDocument(uploadedProjectDocument.ID)
                errorMessage = "An error occured when uploading the file, please try again later"
            End Try

        Else
            errorMessage = "An error occured when uploading the file, please try again later"
        End If

        Return fileUploadSuccess

    End Function

    Public Sub UpdateProject(ByVal editedProjectDocument As ProjectDocument)
        DAOGetter.ProjectDocumentDAO(Context).Update(editedProjectDocument)
    End Sub

    Public Sub RemoveProjectDocument(ByVal projectDocumentId As Integer, ByVal userId As Integer)
        Dim projectDoc As ProjectDocument = DAOGetter.ProjectDocumentDAO(Context).GetByID(projectDocumentId)

        AuditTrailManager.PostAudit(String.Format("Document {0} has been removed from the project", projectDoc.Name), userId,
                                                    projectDoc.Project.ID, AppSettingsGet.DeleteAuditChangeTypeID,
                                                    AppSettingsGet.ProjectDocumentSectionID)

        projectDoc.Active = False
        DAOGetter.ProjectDocumentDAO(Context).Update(projectDoc)
    End Sub

    Public Sub DeleteProjectDocument(ByVal projectDocumentId As Integer)
        Dim projectDoc As ProjectDocument = DAOGetter.ProjectDocumentDAO(Context).GetByID(projectDocumentId)
        DAOGetter.ProjectDocumentDAO(Context).Delete(projectDoc)
    End Sub

#End Region

#Region "Document Version"

    Public Sub SaveDocumentVersion(ByVal saveDocumentVersion As DocumentVersion)
        If saveDocumentVersion.ID = 0 Then
            DAOGetter.DocumentVersionDAO(Context).Insert(saveDocumentVersion)
        Else
            DAOGetter.DocumentVersionDAO(Context).Update(saveDocumentVersion)
        End If
    End Sub

#End Region


#End Region

#Region "Utilities"

    Public Function RadPdfBelongsToDocument(ByVal radPdfId As Integer, ByVal documentId As Integer) As Boolean

        Dim versions As List(Of DocumentVersion) = GetDocumentVersions(documentId)

        If (From v In versions
            Where v.RadPDFDocumentId = radPdfId
            Select v).Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function DoesSecretKeyExist(ByVal secretKey As String) As Boolean

        Dim secretKeyExists As Boolean = False

        If DAOGetter.ProjectDocumentDAO(context).GetProjectDocumentBySecretKey(secretKey) IsNot Nothing Then
            secretKeyExists = True
        End If

        Return secretKeyExists

    End Function

    Private Function FilterProjectsByCategory(ByVal projectList As List(Of ProjectDocument), ByVal filterCategoryId As Integer) As List(Of ProjectDocument)
        Return (From p In projectList
                Where p.Category.ID = filterCategoryId
                Select p).ToList
    End Function

#End Region

End Module
