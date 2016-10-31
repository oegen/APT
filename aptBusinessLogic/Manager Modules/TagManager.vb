'----------------------------------------------------------------------------------------------
' Filename    : TagManager.vb
' Description : Tag manager to deal with all tag functionality and logic.
'
' Release Initials  Date        Comment
' 1       LP        27/05/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module TagManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Get Variations"

    Public Function GetAllTags() As List(Of Tag)
        Return DAOGetter.TagDAO(Context).GetAll()
    End Function

    Public Function GetTags(Optional ByVal active As Boolean = True) As List(Of Tag)
        Return DAOGetter.TagDAO(context).GetByActive(active)
    End Function

    Public Function GetProjectTags(ByVal projectId As Integer, Optional ByVal active As Boolean = True) As List(Of Tag)
        Return (From tagList In context.TagLists
                Where tagList.Project.ID = projectId And tagList.Tag.Active = active
                Select tagList.Tag).ToList
    End Function

    Public Function GetTag(ByVal tagId As Integer) As Tag
        Return DAOGetter.TagDAO(Context).GetByID(tagId)
    End Function

#End Region

#Region "Insert / Update / Remove"

    Public Sub SaveTag(ByRef saveTag As Tag)

        If TagNameExist(saveTag.Name, saveTag.ID) Then
            Throw New NameAlreadyExistsException(String.Format("A Tag with the name {0} already exists", saveTag.Name))
        End If

        If saveTag.ID = 0 Then
            DAOGetter.TagDAO(Context).Insert(saveTag)
        Else
            DAOGetter.TagDAO(Context).Update(saveTag)
        End If

    End Sub

    Public Sub DeleteTag(ByVal tagId As Integer)
        Dim tag As Tag = DAOGetter.TagDAO(context).GetByID(tagId)
        tag.Active = False

        DAOGetter.TagDAO(context).Update(tag)
    End Sub

    ' Removes or adds a tag dependant on whether it exits or not
    Public Sub AlterProjectTag(ByVal tagId As Integer, ByVal projectId As Integer, ByVal remove As Boolean)
        Dim currentProject As Project = ProjectManager.GetProject(projectId)
        Dim currentTag As Tag = DAOGetter.TagDAO(context).GetByID(tagId)

        If ProjectHasTag(tagId, projectId) = remove Then
            If remove = True Then
                Dim tagToAlter As TagList = (From tl In context.TagLists
                                             Where tl.Project.ID = projectId And tl.Tag.ID = tagId
                                             Select tl).SingleOrDefault

                DAOGetter.TagListDAO(context).Delete(tagToAlter)
            Else
                Dim newTagList As New TagList() With {.Project = currentProject, .Tag = currentTag}

                DAOGetter.TagListDAO(context).Insert(newTagList)
            End If
        End If
    End Sub

    Public Sub SetTagActivity(ByVal tagId As Integer, ByVal active As Boolean)

        Dim saveActiveTag As Tag = GetTag(tagId)
        saveActiveTag.Active = active
        SaveTag(saveActiveTag)

    End Sub

#End Region

#Region "Queries"

    Public Function ProjectHasTag(ByVal tagId As Integer, ByVal projectId As Integer) As Boolean

        Dim projectTagAssociation As TagList = (From tagList In context.TagLists
                                                Where tagList.Project.ID = projectId And tagList.Tag.ID = tagId
                                                Select tagList).SingleOrDefault

        Return IIf(projectTagAssociation IsNot Nothing, True, False)

    End Function

    Public Function TagNameExist(ByVal tagName As String, Optional ByVal tagId As Integer = 0) As Boolean

        If DAOGetter.TagDAO(Context).GetByName(tagName, tagId).Count > 0 Then
            Return True
        End If

        Return False

    End Function

#End Region

End Module
