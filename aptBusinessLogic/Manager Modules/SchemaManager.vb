'----------------------------------------------------------------------------------------------
' Filename    : SchemaManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module SchemaManager

    Public ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Gets"

#Region "Schema Data"

    Public Function GetSchemaDataByEntityId(ByVal entityId As Integer) As List(Of SchemaData)
        Return DAOGetter.SchemaDataDAO(context).GetSchemaDataByEntity(entityId)
    End Function

    Public Function GetSchemaDataByProjectIdAndDefinitionId(ByVal schemaDefinitionId As Integer, ByVal projectId As Integer) As SchemaData

        Return (From sd In DAOGetter.SchemaDataDAO(Context).GetAll()
                Where sd.SchemaEntityID = AppSettingsGet.SchemaProjectEntityID _
                  And sd.ParentID = projectId _
                  And sd.SchemaDefinition.ID = schemaDefinitionId
                Select sd).SingleOrDefault

    End Function

    Public Function GetSchemaDataByProjectsAndDefinitionId(ByVal schemaDefinitionId As Integer, ByVal projectIds As List(Of Integer)) As List(Of SchemaData)
        ' The same as GetSchemaDataByProjectIdAndDefinitionId but for multiple projects

        Return (From sd In DAOGetter.SchemaDataDAO(Context).GetAll()
                Where sd.SchemaEntityID = AppSettingsGet.SchemaProjectEntityID _
                  And projectIds.Contains(sd.ParentID) _
                  And sd.SchemaDefinition.ID = schemaDefinitionId
                Select sd).ToList()

    End Function

    Public Function GetCopySchemaDataByElement(ByVal elementId As Integer) As List(Of SchemaData)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.SchemaDataDAO(staticContext).GetSchemaDataByEntity(elementId)
    End Function

    Public Function GetParentIdBySchemaAndValue(schemaId As Integer, value As String) As List(Of Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.SchemaDataDAO(Context).GetParentIdBySchemaAndValue(schemaId, value)
    End Function

#End Region

#Region "Schema Definition"

    Public Function GetSchemaDefinitionById(ByVal definitionId As Integer) As SchemaDefinition
        Return DAOGetter.SchemaDefinitionDAO(Context).GetByID(definitionId)
    End Function

#End Region

#Region "Schema"

    Public Function GetSchemasByEntity(ByVal entityId As Integer) As List(Of Schema)
        Return DAOGetter.SchemaDAO(Context).GetByEntity(entityId)
    End Function

    Public Function GetSchema(ByVal id As Integer) As Schema
        Return DAOGetter.SchemaDAO(Context).GetByID(id)
    End Function

#End Region

#End Region

#Region "Insert / Update"

    Public Sub SaveSchemaData(ByVal saveSchemaData As SchemaData)

        saveSchemaData.Modified = DateTime.Now()

        If saveSchemaData.ID = 0 Then
            saveSchemaData.Created = DateTime.Now()
            DAOGetter.SchemaDataDAO(Context).Insert(saveSchemaData)
        Else
            DAOGetter.SchemaDataDAO(Context).Update(saveSchemaData)
        End If

    End Sub

#End Region

End Module
