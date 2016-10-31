'----------------------------------------------------------------------------------------------
' Filename    : APTContext.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq.Mapping
Imports System.Data.Linq
Imports aptEntities

Partial Public Class APTContext : Inherits DataContext

#Region "Petri Nets"
    Public AccessLevels As Table(Of AccessLevel)
    Public Arcs As Table(Of Arc)
    Public ArcTypes As Table(Of ArcType)
    Public ArcResponses As Table(Of ArcResponse)
    Public Cases As Table(Of wfCase)
    Public CaseStatuses As Table(Of CaseStatus)
    Public Entities As Table(Of Entity)
    Public Places As Table(Of Place)
    Public PlaceTypes As Table(Of PlaceType)
    Public SecurityLookups As Table(Of SecurityLookup)
    Public Tasks As Table(Of Task)
    Public TokenDocuments As Table(Of TokenDocument)
    Public Tokens As Table(Of Token)
    Public TokenStatuses As Table(Of TokenStatus)
    Public Transitions As Table(Of Transition)
    Public TransitionSecurities As Table(Of TransitionSecurity)
    Public Triggers As Table(Of Trigger)
    Public Workflows As Table(Of Workflow)
    Public WorkItems As Table(Of WorkItem)
    Public WorkItemSecurity As Table(Of WorkitemSecurity)
    Public WorkItemStatus As Table(Of WorkitemStatus)
#End Region

#Region "Project Entities"

    Public AdditionalElements As Table(Of AdditionalElement)
    Public Adhocs As Table(Of Adhoc)
    Public AptTypes As Table(Of ElementType)
    Public AptUsers As Table(Of AptUser)
    Public AuditChangeTypes As Table(Of AuditChangeType)
    Public Audits As Table(Of Audit)
    Public AuditSections As Table(Of AuditSection)
    Public BlockMeetingExceptions As Table(Of BlockMeetingException)
    Public DocumentVersions As Table(Of DocumentVersion)
    Public Elements As Table(Of Element)
    Public ElementAdditionalDetails As Table(Of ElementAdditionalDetails)
    Public ElementArtworkDetails As Table(Of ElementArtworkDetails)
    Public ElementKittingDetails As Table(Of ElementKittingDetails)
    Public ElementWorkflowStates As Table(Of ElementWorkflowState)
    Public EntitySchemaMappings As Table(Of EntitySchemaMapping)
    Public FreelancerTime As Table(Of FreelancerTime)
    Public AptLists As Table(Of AptList)
    Public Kits As Table(Of Kit)
    Public KitElements As Table(Of KitElement)
    Public KitQuotes As Table(Of KitQuote)
    Public ListNodes As Table(Of ListNode)
    Public NewBBCItem As Table(Of NewBBCItem)
    Public PremiumElementDetails As Table(Of PremiumElementDetails)
    Public PremiumBrief As Table(Of PremiumBrief)
    Public ProjectsAccesses As Table(Of ProjectAccess)
    Public ProjectAccessLevels As Table(Of ProjectAccessLevel)
    Public ProjectBBCItems As Table(Of ProjectBBCItem)
    Public ProjectCategories As Table(Of ProjectDocumentCategory)
    Public ProjectCosting As Table(Of ProjectCostings)
    Public Projects As Table(Of Project)
    Public ProjectDocuments As Table(Of ProjectDocument)
    Public ProjectEntities As Table(Of ProjectEntity)
    Public ProjectKittingBrief As Table(Of ProjectKittingBrief)
    Public ProjectRoleAssociations As Table(Of ProjectRoleAssociation)
    Public ProjectSecurityLevels As Table(Of ProjectSecurityLevel)
    Public ReservedTimes As Table(Of ReservedTime)
    Public Roles As Table(Of Role)
    Public Schemas As Table(Of Schema)
    Public SchemaData As Table(Of SchemaData)
    Public SchemaDefinitions As Table(Of SchemaDefinition)
    Public SubclassTypes As Table(Of SubclassType)
    Public Tags As Table(Of Tag)
    Public TagLists As Table(Of TagList)
    Public TaskLibraryItems As Table(Of TaskLibraryItem)
    Public Timesheets As Table(Of Timesheet)
    Public TimesheetReasons As Table(Of TimesheetReason)
    Public UserLogins As Table(Of AptLogin)
    Public UserRoles As Table(Of UserRole)
    Public WorkingWeeks As Table(Of WorkingWeek)
    Public WorkingWeekExceptions As Table(Of WorkingWeekException)

#End Region

#Region "Constructor"
    Sub New()
        MyBase.New("")
    End Sub

    Sub New(ByVal pConnection As String)
        MyBase.New(pConnection)
    End Sub
#End Region

End Class
