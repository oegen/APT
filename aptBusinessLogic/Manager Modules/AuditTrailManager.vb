'----------------------------------------------------------------------------------------------
' Filename    : AuditTrailManager.vb
' Description : General Management for Audit Trails.
'
' Release Initials  Date        Comment
' 1       LP        31/05/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module AuditTrailManager

#Region "Getters"

    Public Function GetAll() As List(Of Audit)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.AuditDAO(context).GetAll()
    End Function

    Public Function GetAllByProject(ByVal projectId As Integer) As List(Of Audit)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Dim auditList As List(Of Audit) = DAOGetter.AuditDAO(context).GetAllByProject(projectId)

        Return (From a In auditList
                Select a
                Order By a.AuditDate Descending).ToList
    End Function

    Public Function GetChangeTypeById(ByVal changeTypeId As Integer) As AuditChangeType
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.AuditChangeTypeDAO(context).GetByID(changeTypeId)
    End Function

    Public Function GetAuditSectionById(ByVal auditSectionId As Integer) As AuditSection
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.AuditSectionDAO(context).GetByID(auditSectionId)
    End Function

    Public Function GetLatestAuditByProject(ByVal projectId As Integer) As Audit
        Dim auditList As List(Of Audit) = GetAllByProject(projectId)

        Return (From a In auditList
                Select a
                Order By a.AuditDate).LastOrDefault
    End Function

#End Region

#Region "Insert"

    Public Sub PostAudit(ByVal auditToPost As Audit)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        auditToPost.AuditDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")

        DAOGetter.AuditDAO(context).Insert(auditToPost)
    End Sub

    Public Sub PostAudit(ByVal message As String, ByVal userId As Integer, ByVal projectId As Integer, _
                         ByVal changeTypeID As Integer, ByVal auditSectionID As Integer)
        Dim addAudit As Audit = GenerateAudit(message, userId, projectId)

        addAudit.ChangeType = GetChangeTypeById(changeTypeID)
        addAudit.Section = GetAuditSectionById(auditSectionID)
        addAudit.Message = message
        addAudit.Project = ProjectManager.GetProject(projectId)
        addAudit.User = UserManager.GetUser(userId)

        PostAudit(addAudit)
    End Sub

    Private Function GenerateAudit(ByVal message As String, ByVal userId As Integer, ByVal projectId As Integer) As Audit
        Dim newAudit As New Audit

        newAudit.Message = message
        newAudit.Project = ProjectManager.GetProject(projectId)

        Return newAudit
    End Function

#End Region

End Module
