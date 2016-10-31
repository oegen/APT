'----------------------------------------------------------------------------------------------
' Filename    : ProjectDocumentCategoryManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module ProjectDocumentCategoryManager

#Region "Get Variations"

    Public Function GetProjectDocumentCategory(ByVal projectDocumentCategoryId As Integer) As ProjectDocumentCategory

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ProjectDocumentCategoryDAO(context).GetByID(projectDocumentCategoryId)

    End Function

    Public Function GetProjectDocumentCategories(Optional ByVal active As Boolean = True) As List(Of ProjectDocumentCategory)

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ProjectDocumentCategoryDAO(context).GetByActive(active)

    End Function

#End Region

End Module
