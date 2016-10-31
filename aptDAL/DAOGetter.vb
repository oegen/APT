'----------------------------------------------------------------------------------------------
' Filename    : DAOGetter.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports GenericDAL

Public Module DAOGetter

#Region "Petri Net DAO"
    Public ReadOnly Property AccessLevelDAO(ByVal pContext As DataContext) As AccessLevelDAO
        Get
            Return New AccessLevelDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ArcDAO(ByVal pContext As DataContext) As ArcDAO
        Get
            Return New ArcDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ArcTypeDAO(ByVal pContext As DataContext) As ArcTypeDAO
        Get
            Return New ArcTypeDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property CaseDAO(ByVal pContext As DataContext) As CaseDAO
        Get
            Return New CaseDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property CaseStatusDAO(ByVal pContext As DataContext) As CaseStatusDAO
        Get
            Return New CaseStatusDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property DocumentVersionDAO(ByVal pContext As DataContext) As DocumentVersionDAO
        Get
            Return New DocumentVersionDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property EntityDAO(ByVal pContext As DataContext) As EntityDAO
        Get
            Return New EntityDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property PlaceDAO(ByVal pContext As DataContext) As PlaceDAO
        Get
            Return New PlaceDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property PlaceTypeDAO(ByVal pContext As DataContext) As PlaceTypeDAO
        Get
            Return New PlaceTypeDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property SecurityLookupDAO(ByVal pContext As DataContext) As SecurityLookupDAO
        Get
            Return New SecurityLookupDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TaskDAO(ByVal pContext As DataContext) As TaskDAO
        Get
            Return New TaskDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TokenDAO(ByVal pContext As DataContext) As TokenDAO
        Get
            Return New TokenDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TokenDocumentDAO(ByVal pContext As DataContext) As TokenDocumentDAO
        Get
            Return New TokenDocumentDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TokenStatusDAO(ByVal pContext As DataContext) As TokenStatusDAO
        Get
            Return New TokenStatusDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TransitionDAO(ByVal pContext As DataContext) As TransitionDAO
        Get
            Return New TransitionDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TransitionSecurityDAO(ByVal pContext As DataContext) As TransitionSecurityDAO
        Get
            Return New TransitionSecurityDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TriggerDAO(ByVal pContext As DataContext) As TriggerDAO
        Get
            Return New TriggerDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property WorkflowDAO(ByVal pContext As DataContext) As WorkflowDAO
        Get
            Return New WorkflowDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property WorkitemDAO(ByVal pContext As DataContext) As WorkitemDAO
        Get
            Return New WorkitemDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property WorkitemSecurityDAO(ByVal pContext As DataContext) As WorkitemSecurityDAO
        Get
            Return New WorkitemSecurityDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property WorkitemStatusDAO(ByVal pContext As DataContext) As WorkitemStatusDAO
        Get
            Return New WorkitemStatusDAO(pContext)
        End Get
    End Property

#End Region

#Region "Project DAO"

    Public ReadOnly Property AdditionalElementDAO(ByVal pContext As DataContext) As AdditionalElementDAO
        Get
            Return New AdditionalElementDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property AdhocDAO(ByVal pContext As DataContext) As AdhocDAO
        Get
            Return New AdhocDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property AptTypeDAO(ByVal pContext As DataContext) As ElementTypeDAO
        Get
            Return New ElementTypeDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property AptUserDAO(ByVal pContext As DataContext) As AptUserDAO
        Get
            Return New AptUserDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ArcResponseDAO(ByVal pContext As DataContext) As ArcResponseDAO
        Get
            Return New ArcResponseDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property AuditChangeTypeDAO(ByVal pContext As DataContext) As AuditChangeTypeDAO
        Get
            Return New AuditChangeTypeDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property AuditDAO(ByVal pContext As DataContext) As AuditDAO
        Get
            Return New AuditDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property AuditSectionDAO(ByVal pContext As DataContext) As AuditSectionDAO
        Get
            Return New AuditSectionDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property BlockMeetingExceptionDAO(ByVal pContext As DataContext) As BlockMeetingExceptionDAO
        Get
            Return New BlockMeetingExceptionDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ElementDAO(ByVal pContext As DataContext) As ElementDAO
        Get
            Return New ElementDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ElementAdditionalDetailsDAO(ByVal pContext As DataContext) As ElementAdditionalDetailsDAO
        Get
            Return New ElementAdditionalDetailsDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ElementArtworkDetailsDAO(ByVal pContext As DataContext) As ElementArtworkDetailsDAO
        Get
            Return New ElementArtworkDetailsDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ElementKittingDetailsDAO(ByVal pContext As DataContext) As ElementKittingDetailsDAO
        Get
            Return New ElementKittingDetailsDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ElementWorkflowStateDAO(ByVal pContext As DataContext) As ElementWorkflowStateDAO
        Get
            Return New ElementWorkflowStateDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property EntitySchemaMappingDAO(ByVal pContext As DataContext) As EntitySchemaMappingDAO
        Get
            Return New EntitySchemaMappingDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property FreelancerTimeDAO(ByVal pContext As DataContext) As FreelancerDAO
        Get
            Return New FreelancerDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property KitDAO(ByVal pContext As DataContext) As KitDAO
        Get
            Return New KitDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property KitElementsDAO(ByVal pContext As DataContext) As KitElementsDAO
        Get
            Return New KitElementsDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property KitQuoteDAO(ByVal pContext As DataContext) As KitQuoteDAO
        Get
            Return New KitQuoteDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ListDAO(ByVal pContext As DataContext) As ListDAO
        Get
            Return New ListDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ListNodeDAO(ByVal pContext As DataContext) As ListNodeDAO
        Get
            Return New ListNodeDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property NewBBCItemDAO(ByVal pContext As DataContext) As NewBBCItemDAO
        Get
            Return New NewBBCItemDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property PremiumBriefDAO(ByVal pContext As DataContext) As PremiumBriefDAO
        Get
            Return New PremiumBriefDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property PremiumElementDetailsDAO(ByVal pContext As DataContext) As PremiumElementDetailsDAO
        Get
            Return New PremiumElementDetailsDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectAccessDAO(ByVal pContext As DataContext) As ProjectAccessDAO
        Get
            Return New ProjectAccessDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectAccessLevelDAO(ByVal pContext As DataContext) As ProjectAccessLevelDAO
        Get
            Return New ProjectAccessLevelDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectBBCItemsDAO(ByVal pContext As DataContext) As ProjectBBCItemDAO
        Get
            Return New ProjectBBCItemDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectCostingsDAO(ByVal pContext As DataContext) As ProjectCostingsDAO
        Get
            Return New ProjectCostingsDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectDocumentCategoryDAO(ByVal pContext As DataContext) As ProjectDocumentCategoryDAO
        Get
            Return New ProjectDocumentCategoryDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectDAO(ByVal pContext As DataContext) As ProjectDAO
        Get
            Return New ProjectDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectDocumentDAO(ByVal pContext As DataContext) As ProjectDocumentDAO
        Get
            Return New ProjectDocumentDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectEntityDAO(ByVal pContext As DataContext) As ProjectEntityDAO
        Get
            Return New ProjectEntityDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectKittingBriefDAO(ByVal pContext As DataContext) As ProjectKittingBriefDAO
        Get
            Return New ProjectKittingBriefDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectRoleAssociationDAO(ByVal pContext As DataContext) As ProjectRoleAssociationDAO
        Get
            Return New ProjectRoleAssociationDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ProjectSecurityLevelDAO(ByVal pContext As DataContext) As ProjectSecurityLevelDAO
        Get
            Return New ProjectSecurityLevelDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property ReservedTimeDAO(ByVal pContext As DataContext) As ReservedTimeDAO
        Get
            Return New ReservedTimeDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property RoleDAO(ByVal pContext As DataContext) As RoleDAO
        Get
            Return New RoleDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property SchemaDAO(ByVal pContext As DataContext) As SchemaDAO
        Get
            Return New SchemaDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property SchemaDataDAO(ByVal pContext As DataContext) As SchemaDataDAO
        Get
            Return New SchemaDataDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property SchemaDefinitionDAO(ByVal pContext As DataContext) As SchemaDefinitionDAO
        Get
            Return New SchemaDefinitionDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property SubclassTypeDAO(ByVal pContext As DataContext) As SubclassTypeDAO
        Get
            Return New SubclassTypeDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TagDAO(ByVal pContext As DataContext) As TagDAO
        Get
            Return New TagDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TagListDAO(ByVal pContext As DataContext) As TagListDAO
        Get
            Return New TagListDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TaskLibraryItemDAO(ByVal pContext As DataContext) As TaskLibraryItemDAO
        Get
            Return New TaskLibraryItemDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TimesheetDAO(ByVal pContext As DataContext) As TimesheetDAO
        Get
            Return New TimesheetDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property TimesheetReasonDAO(ByVal pContext As DataContext) As TimesheetReasonDAO
        Get
            Return New TimesheetReasonDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property UserLoginsDAO(ByVal pContext As DataContext) As AptLoginDAO
        Get
            Return New AptLoginDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property UserRoleDAO(ByVal pContext As DataContext) As UserRoleDAO
        Get
            Return New UserRoleDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property WorkingWeekDAO(ByVal pContext As DataContext) As WorkingWeekDAO
        Get
            Return New WorkingWeekDAO(pContext)
        End Get
    End Property

    Public ReadOnly Property WorkingWeekExceptionDAO(ByVal pContext As DataContext) As WorkingWeekExceptionDAO
        Get
            Return New WorkingWeekExceptionDAO(pContext)
        End Get
    End Property

#End Region

End Module
