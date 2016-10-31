'----------------------------------------------------------------------------------------------
' Filename    : AppSettingsGet.vb
' Description : Stores all the AppSettings keys for ease of access.
'
' Release Initials  Date        Comment
' 1       LP/TL     07/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration.ConfigurationManager

Public Module AppSettingsGet

#Region "Database Keys"
    Public ReadOnly Property SQLConnectionStr
        Get
            Return AppSettings("SQLConnection")
        End Get
    End Property

    Public ReadOnly Property ContextKey
        Get
            Return AppSettings("ContextKey")
        End Get
    End Property
#End Region

#Region "Site / E-mail Keys"
    Public ReadOnly Property SiteURL As String
        Get
            Return AppSettings("SiteURL")
        End Get
    End Property

    Public ReadOnly Property SenderAddress As String
        Get
            Return AppSettings("SenderAddress")
        End Get
    End Property

    Public ReadOnly Property ErrorEmailAddress As String
        Get
            Return AppSettings("ErrorEmailAddress")
        End Get
    End Property

    Public ReadOnly Property WLeaGroupEmailAddress As String
        Get
            Return AppSettings("WLeaGroupEmailAddress")
        End Get
    End Property

    Public ReadOnly Property TestEmail As String
        Get
            Return AppSettings("TestEmail")
        End Get
    End Property

#End Region

#Region "Workflow Related Keys"

#Region "Token Status Keys"

    Public ReadOnly Property TokenStatusFree As Integer
        Get
            Return CType(AppSettings("TokenStatusFree"), Integer)
        End Get
    End Property

    Public ReadOnly Property TokenStatusLock As Integer
        Get
            Return CType(AppSettings("TokenStatusLock"), Integer)
        End Get
    End Property

    Public ReadOnly Property TokenStatusConsumed As Integer
        Get
            Return CType(AppSettings("TokenStatusCons"), Integer)
        End Get
    End Property

    Public ReadOnly Property TokenStatusCancelled As Integer
        Get
            Return CType(AppSettings("TokenStatusCancelled"), Integer)
        End Get
    End Property

#End Region

#Region "Places IDs"

    Public ReadOnly Property StartPlaceID As Integer
        Get
            Return CType(AppSettings("StartPlaceID"), Integer)
        End Get
    End Property

    Public ReadOnly Property NonBDStartPlaceID As Integer
        Get
            Return CType(AppSettings("NonBDStartPlaceID"), Integer)
        End Get
    End Property

    Public ReadOnly Property AllowPrintPlaceID As Integer
        Get
            Return CType(AppSettings("AllowPrintPlaceID"), Integer)
        End Get
    End Property

    Public ReadOnly Property PreCollectBriefPlaceID As Integer
        Get
            Return CType(AppSettings("PreCollectBriefPlaceID"), Integer)
        End Get
    End Property

    Public ReadOnly Property PreWilliamsLeaFinalCostsPlaceID As Integer
        Get
            Return CType(AppSettings("PreWilliamsLeaFinalCostsID"), Integer)
        End Get
    End Property

    Public ReadOnly Property PostPrintProductionBDPlaceID As Integer
        Get
            Return CType(AppSettings("PostPrintProductionBDPlaceID"), Integer)
        End Get
    End Property

    Public ReadOnly Property PostPrintProductionNonBDPlaceID As Integer
        Get
            Return CType(AppSettings("PostPrintProductionNonBDPlaceID"), Integer)
        End Get
    End Property

    Public ReadOnly Property PrintRequiredPlaceID As Integer
        Get
            Return AppSettings("PrintRequiredPlaceID")
        End Get
    End Property

    Public ReadOnly Property EndPlaceID As Integer
        Get
            Return AppSettings("EndPlaceID")
        End Get
    End Property

    Public ReadOnly Property FinalisedBriefBDPlaceID As Integer
        Get
            Return AppSettings("FinalisedBriefBDPlaceID")
        End Get
    End Property

    Public ReadOnly Property FinalisedBriefNonBDPlaceID As Integer
        Get
            Return AppSettings("FinalisedBriefNonBDPlaceID")
        End Get
    End Property

    Public ReadOnly Property BriefToProcurementPlaceID As Integer
        Get
            Return AppSettings("BriefToProcurementPlaceID")
        End Get
    End Property

    Public ReadOnly Property StudioQAPlaceID As Integer
        Get
            Return AppSettings("StudioQAPlaceID")
        End Get
    End Property

#End Region

#Region "Place Type Keys"

    Public ReadOnly Property PlaceTypeStart
        Get
            Return AppSettings("PlaceTypeStart")
        End Get
    End Property

    Public ReadOnly Property PlaceTypeIntermediate
        Get
            Return AppSettings("PlaceTypeIntermediate")
        End Get
    End Property

    Public ReadOnly Property PlaceTypeFinish
        Get
            Return AppSettings("PlaceTypeFinish")
        End Get
    End Property

    Public ReadOnly Property ElementPlaceStartID As Integer
        Get
            Return CType(AppSettings("ElementPlaceStartID"), Integer)
        End Get
    End Property

    Public ReadOnly Property ElementPlaceEndID As Integer
        Get
            Return CType(AppSettings("ElementPlaceEndID"), Integer)
        End Get
    End Property

#End Region

#Region "Security Lookup Keys"

    Public ReadOnly Property SecurityLookupUserID As Integer
        Get
            Return CType(AppSettings("securityLookupUserID"), Integer)
        End Get
    End Property

    Public ReadOnly Property SecurityLookupRoleID As Integer
        Get
            Return CType(AppSettings("securityLookupRoleID"), Integer)
        End Get
    End Property

    Public ReadOnly Property SecurityLookupProjectID As Integer
        Get
            Return CType(AppSettings("securityLookupProjectID"), Integer)
        End Get
    End Property

#End Region

#Region "Context Keys"

    Public ReadOnly Property EntityProjectId As Integer
        Get
            Return CType(AppSettings("entityProjectId"), Integer)
        End Get
    End Property

    Public ReadOnly Property EntityElementId As Integer
        Get
            Return CType(AppSettings("entityElementId"), Integer)
        End Get
    End Property

    Public ReadOnly Property WorkflowCaseId As Integer
        Get
            Return CType(AppSettings("workflowCaseId"), Integer)
        End Get
    End Property

#End Region

#Region "Arc Type Keys"

    Public ReadOnly Property SEQArcTypeId As Integer
        Get
            Return CType(AppSettings("SEQArcTypeId"), Integer)
        End Get
    End Property

    Public ReadOnly Property XORArcTypeId As Integer
        Get
            Return CType(AppSettings("XORArcTypeId"), Integer)
        End Get
    End Property

#End Region

#Region "Transition IDs"

    Public ReadOnly Property FinaliseAPTBriefTransitionID As Integer
        Get
            Return CType(AppSettings("FinaliseAPTBriefTransitionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property FinaliseAPTBriefTransitionNonBDID As Integer
        Get
            Return AppSettings("FinaliseAPTBriefTransitionNonBDID")
        End Get
    End Property

    Public ReadOnly Property PrintProductionTransitionID As Integer
        Get
            Return CType(AppSettings("PrintProductionTransitionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property PreElementStartTransitionID As Integer
        Get
            Return CType(AppSettings("PreElementStartTransitionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property ElementWorkflowFinishTransitionID As Integer
        Get
            Return AppSettings("ElementWorkflowFinishTransitionID")
        End Get
    End Property

    Public ReadOnly Property PrintGoAheadTransitionID As Integer
        Get
            Return CType(AppSettings("PrintGoAheadTransitionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property BriefSubmittedPlaceId As Integer
        Get
            Return CType(AppSettings("BriefSubmittedPlaceId"), Integer)
        End Get
    End Property

    Public ReadOnly Property ReserveTimeTransitionBDID As Integer
        Get
            Return AppSettings("ReserveTimeTransitionBDID")
        End Get
    End Property

    Public ReadOnly Property ReserveTimeTransitionNonBDID As Integer
        Get
            Return AppSettings("ReserveTimeTransitionNonBDID")
        End Get
    End Property

    Public ReadOnly Property IsPrintProjectTransitionID As Integer
        Get
            Return AppSettings("IsPrintProjectTransitionID")
        End Get
    End Property

    Public ReadOnly Property BriefSignOffTransitionID As Integer
        Get
            Return AppSettings("BriefSignOffTransitionID")
        End Get
    End Property

    Public ReadOnly Property BriefSignOffNonBDTransitionID As Integer
        Get
            Return AppSettings("BriefSignOffNonBDTransitionID")
        End Get
    End Property

    Public ReadOnly Property ChallengeBriefTransitionID As Integer
        Get
            Return AppSettings("ChallengeBriefTransitionID")
        End Get
    End Property

    Public ReadOnly Property ChallengeBriefNonBDTransitionID As Integer
        Get
            Return AppSettings("ChallengeBriefNonBDTransitionID")
        End Get
    End Property

    Public ReadOnly Property KittingFinalTransitionID As Integer
        Get
            Return AppSettings("KittingFinalTransitionID")
        End Get
    End Property

    Public ReadOnly Property NotifyWLeaTransitionID As Integer
        Get
            Return AppSettings("NotifyWLeaTransitionID")
        End Get
    End Property

    Public ReadOnly Property WLeaBudgetProposal As Integer
        Get
            Return AppSettings("WLeaBudgetProposal")
        End Get
    End Property

    Public ReadOnly Property AinNoRaiseBD As Integer
        Get
            Return AppSettings("AinNoRaiseBD")
        End Get
    End Property
#End Region

#End Region

#Region "Password Keys"

    Public ReadOnly Property PasswordCharacters As String
        Get
            Return AppSettings("PasswordCharacters")
        End Get
    End Property

    Public ReadOnly Property PasswordNumerics As String
        Get
            Return AppSettings("PasswordNumerics")
        End Get
    End Property

    Public ReadOnly Property PasswordLength As Integer
        Get
            Return CType(AppSettings("PasswordLength"), Integer)
        End Get
    End Property

#End Region

#Region "Role Keys"
    Public ReadOnly Property OwnerRoleID As Integer
        Get
            Return CType(AppSettings("OwnerRoleID"), Integer)
        End Get
    End Property

    Public ReadOnly Property AdminRoleID As Integer
        Get
            Return CType(AppSettings("AdminRoleID"), Integer)
        End Get
    End Property

    Public ReadOnly Property PORaiserID As Integer
        Get
            Return CType(AppSettings("PORaiserID"), Integer)
        End Get
    End Property

    Public ReadOnly Property LegalApproverID As Integer
        Get
            Return CType(AppSettings("LegalApproverID"), Integer)
        End Get
    End Property

    Public ReadOnly Property GraphicsCoordinatorID As Integer
        Get
            Return CType(AppSettings("GraphicsCoordinatorID"), Integer)
        End Get
    End Property

    Public ReadOnly Property ArtworkerID As Integer
        Get
            Return CType(AppSettings("ArtworkerID"), Integer)
        End Get
    End Property

    'Public ReadOnly Property SystemAdminID As Integer
    '    Get
    '        Return CType(AppSettings("SystemAdminID"), Integer)
    '    End Get
    'End Property

    Public ReadOnly Property PrinterID As Integer
        Get
            Return CType(AppSettings("PrinterID"), Integer)
        End Get
    End Property

    Public ReadOnly Property DesignerID As Integer
        Get
            Return CType(AppSettings("DesignerID"), Integer)
        End Get
    End Property

    Public ReadOnly Property BrandManagerID As Integer
        Get
            Return CType(AppSettings("BrandManagerID"), Integer)
        End Get
    End Property

    Public ReadOnly Property StudioManagerID As Integer
        Get
            Return CType(AppSettings("StudioManagerID"), Integer)
        End Get
    End Property

    Public ReadOnly Property WilliamsLeaProjectManagerID As Integer
        Get
            Return CType(AppSettings("WilliamsLeaProjectManagerID"), Integer)
        End Get
    End Property

    Public ReadOnly Property WilliamsLeaGlobalProjectManagerID As Integer
        Get
            Return CType(AppSettings("WilliamsLeaGlobalProjectManagerID"), Integer)
        End Get
    End Property

    Public ReadOnly Property WilliamsLeaAccountManagerID As Integer
        Get
            Return CType(AppSettings("WilliamsLeaAccountManagerID"), Integer)
        End Get
    End Property

    Public ReadOnly Property ProjectApproverID As Integer
        Get
            Return CType(AppSettings("ProjectApproverID"), Integer)
        End Get
    End Property

    Public ReadOnly Property StudioQAID As Integer
        Get
            Return CType(AppSettings("StudioQAID"), Integer)
        End Get
    End Property

    Public ReadOnly Property MDAProcurementID As Integer
        Get
            Return CType(AppSettings("MDAProcurementID"), Integer)
        End Get
    End Property

    Public ReadOnly Property MDAKittingID As Integer
        Get
            Return CType(AppSettings("MDAKittingID"), Integer)
        End Get
    End Property

    Public ReadOnly Property MDAProjectManagerID As Integer
        Get
            Return CType(AppSettings("MDAProjectManagerID"), Integer)
        End Get
    End Property

    Public ReadOnly Property MDAOnSiteID As Integer
        Get
            Return CType(AppSettings("MDAOnSiteID"), Integer)
        End Get
    End Property

    Public ReadOnly Property BDProjectManagerID As Integer
        Get
            Return CType(AppSettings("BDProjectManagerID"), Integer)
        End Get
    End Property

    Public ReadOnly Property BDAccountManagerID As Integer
        Get
            Return CType(AppSettings("BDAccountManagerID"), Integer)
        End Get
    End Property

    Public ReadOnly Property DefaultQARaiser As String
        Get
            Return AppSettings("DefaultStudioQA")
        End Get
    End Property

    Public ReadOnly Property DefaultBDCoordinator As String
        Get
            Return AppSettings("DefaultBDCoordinator")
        End Get
    End Property

    Public ReadOnly Property DefaultNonBDCoordinator As String
        Get
            Return AppSettings("DefaultNonBDCoordinator")
        End Get
    End Property

#End Region

#Region "File Upload"

    Public ReadOnly Property DocumentsFolderPath As String
        Get
            Return CType(AppSettings("DocumentsFolderPath"), String)
        End Get
    End Property

    Public ReadOnly Property WebRoot As String
        Get
            Return AppSettings("WebRoot")
        End Get
    End Property

#End Region

#Region "Project Keys"

    Public ReadOnly Property TotalProjectDurationInWeeks As Integer
        Get
            Return CType(AppSettings("TotalProjectDuration"), Integer)
        End Get
    End Property

#End Region

#Region "Lists"

    Public ReadOnly Property TradeListId As Integer
        Get
            Return CType(AppSettings("TradeListId"), Integer)
        End Get
    End Property

    Public ReadOnly Property TCListId As Integer
        Get
            Return CType(AppSettings("TCListId"), Integer)
        End Get
    End Property

    Public ReadOnly Property BusinessAreaId As Integer
        Get
            Return CType(AppSettings("BusinessAreaId"), Integer)
        End Get
    End Property

    Public ReadOnly Property ElementPageId As Integer
        Get
            Return CType(AppSettings("ElementPageId"), Integer)
        End Get
    End Property

    Public ReadOnly Property ElementQuantityId As Integer
        Get
            Return CType(AppSettings("ElementQuantityId"), Integer)
        End Get
    End Property

    Public ReadOnly Property BBCBrandsListId As Integer
        Get
            Return CType(AppSettings("BBCBrandsListId"), Integer)
        End Get
    End Property

#End Region

#Region "List Keys"

    Public ReadOnly Property BrandListId As Integer
        Get
            Return CType(AppSettings("BrandListId"), Integer)
        End Get
    End Property

    Public ReadOnly Property TypeOfWorkListId As Integer
        Get
            Return CType(AppSettings("TypeOfWorkListId"), Integer)
        End Get
    End Property

#End Region

#Region "Pre-Conditions"
    Public ReadOnly Property RejectedPreCondition As String
        Get
            Return AppSettings("RejectedPreCondition")
        End Get
    End Property

    Public ReadOnly Property AcceptedPreCondition As String
        Get
            Return AppSettings("AcceptedPreCondition")
        End Get
    End Property

    Public ReadOnly Property StartedPreCondition As String
        Get
            Return AppSettings("StartedPreCondition")
        End Get
    End Property
#End Region

#Region "Task Keys"
    Public ReadOnly Property StudioQATaskId As Integer
        Get
            Return CType(AppSettings("StudioQATaskId"), Integer)
        End Get
    End Property

    Public ReadOnly Property BDApprovalTaskId As Integer
        Get
            Return CType(AppSettings("BDApprovalTaskId"), Integer)
        End Get
    End Property

    Public ReadOnly Property SubmitToBDTaskId As Integer
        Get
            Return CType(AppSettings("SubmitToDBTaskId"), Integer)
        End Get
    End Property

#End Region

#Region "Working Hours"

    Public ReadOnly Property WorkingWeekHours As String
        Get
            Return AppSettings("WorkingWeekHours")
        End Get
    End Property

#End Region

#Region "Entity IDs"

    Public ReadOnly Property SchemaProjectEntityID As Integer
        Get
            Return CType(AppSettings("SchemaProjectEntityID"), Integer)
        End Get
    End Property

    Public ReadOnly Property SchemaElementEntityID As Integer
        Get
            Return CType(AppSettings("SchemaElementEntityID"), Integer)
        End Get
    End Property

#End Region

#Region "Schema Definition IDs"

    Public ReadOnly Property PrintReferenceNumberID As Integer
        Get
            Return AppSettings("PrintReferenceNumberID")
        End Get
    End Property

    Public ReadOnly Property BrandListDefinitionID As Integer
        Get
            Return AppSettings("BrandListDefinitionID")
        End Get
    End Property

    Public ReadOnly Property TypeOfWorkDefinitionID As Integer
        Get
            Return AppSettings("TypeOfWorkDefinitionID")
        End Get
    End Property

    Public ReadOnly Property QuoteDefinitionID As Integer
        Get
            Return AppSettings("QuoteDefinitionID")
        End Get
    End Property

    Public ReadOnly Property BusinessAreaDefinitionID As Integer
        Get
            Return AppSettings("BusinessAreaDefinitionID")
        End Get
    End Property

#End Region

#Region "Audit Change Type Keys"

    Public ReadOnly Property AddAuditChangeTypeID As Integer
        Get
            Return CType(AppSettings("AddAuditChangeTypeID"), Integer)
        End Get
    End Property

    Public ReadOnly Property EditAuditChangeTypeID As Integer
        Get
            Return CType(AppSettings("EditAuditChangeTypeID"), Integer)
        End Get
    End Property

    Public ReadOnly Property DeleteAuditChangeTypeID As Integer
        Get
            Return CType(AppSettings("DeleteAuditChangeTypeID"), Integer)
        End Get
    End Property

    Public ReadOnly Property ProjectCoreDetailsAuditSectionID As Integer
        Get
            Return CType(AppSettings("ProjectCoreDetailsAuditSectionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property ElementCoreDetailsSectionID As Integer
        Get
            Return CType(AppSettings("ElementCoreDetailsSectionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property ProjectDocumentSectionID As Integer
        Get
            Return CType(AppSettings("ProjectDocumentSectionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property AdditionalElementSectionID As Integer
        Get
            Return CType(AppSettings("AdditionalElementSectionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property BBCItemSectionID As Integer
        Get
            Return CType(AppSettings("BBCItemSectionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property KitSectionID As Integer
        Get
            Return CType(AppSettings("KitSectionID"), Integer)
        End Get
    End Property

    Public ReadOnly Property ReserveTimeAuditSectionID As Integer
        Get
            Return CType(AppSettings("ReserveTimeAuditSectionID"), Integer)
        End Get
    End Property

#End Region

#Region "HTML Specific Attributes"

    Public ReadOnly Property HTMLNewLine As String
        Get
            Return AppSettings("HTMLNewLine")
        End Get
    End Property

    Public ReadOnly Property NavSelectedItemStyle As String
        Get
            Return AppSettings("NavSelectedItemStyle")
        End Get
    End Property

#End Region

#Region "Working Exceptions"

    Public ReadOnly Property AdHocTypeId As Integer
        Get
            Return AppSettings("AdHocTypeId")
        End Get
    End Property

    Public ReadOnly Property BlockMeetingId As Integer
        Get
            Return AppSettings("BlockMeetingId")
        End Get
    End Property

#End Region

#Region "Kitting"

    Public ReadOnly Property BBCItemTypeID As Integer
        Get
            Return AppSettings("BBCItemTypeId")
        End Get
    End Property

    Public ReadOnly Property ElementTypeID As Integer
        Get
            Return AppSettings("ElementTypeId")
        End Get
    End Property

    Public ReadOnly Property PremiumElementTypeID As Integer
        Get
            Return AppSettings("PremiumElementTypeId")
        End Get
    End Property

#End Region

#Region "Misc"

    Public ReadOnly Property ProjectListingCount As Integer
        Get
            Return AppSettings("ProjectListingCount")
        End Get
    End Property

    Public ReadOnly Property FilenameMaxLength As Integer
        Get
            Return AppSettings("FilenameMaxLength")
        End Get
    End Property

#End Region

#Region "Reserve Time"

    Public ReadOnly Property NumArtworkers As Integer
        Get
            Return CType(AppSettings("NumArtworkers"), Integer)
        End Get
    End Property

    Public ReadOnly Property TotalArtworkerHours
        Get
            Return AppSettings("TotalArtworkerHours")
        End Get
    End Property

#End Region

#Region "Default Users"

    Public ReadOnly Property DefaultPORaisersUserID As String
        Get
            Return AppSettings("DefaultPORaisersUserID")
        End Get
    End Property

    Public ReadOnly Property DefaultStudioQAUserID As String
        Get
            Return AppSettings("DefaultStudioQAUserID")
        End Get
    End Property

#End Region

#Region "Default Project Info"

    Public ReadOnly Property DefaultCoorsAddress As String
        Get
            Return AppSettings("DefaultCoorsAddress")
        End Get
    End Property

    Public ReadOnly Property NumberOfDelAddress As Integer
        Get
            Return AppSettings("NumberOfDelAddress")
        End Get
    End Property

    Public ReadOnly Property DefaultProjectCoordinatorsID As String
        Get
            Return AppSettings("DefaultProjectCoordinatorsID")
        End Get
    End Property

    Public ReadOnly Property DefaultWLeaProjectManager As String
        Get
            Return AppSettings("DefaultWLeaProjectManager")
        End Get
    End Property

    Public ReadOnly Property DefaultMDAManager As String
        Get
            Return AppSettings("DefaultMDAManager")
        End Get
    End Property

#End Region

    Public ReadOnly Property ArtworkCostPerHour
        Get
            Return AppSettings("ArtworkCostPerHour")
        End Get
    End Property

    Public ReadOnly Property SmptServer
        Get
            Return AppSettings("smptServer")
        End Get
    End Property

    Public ReadOnly Property DefaultAinRaisedEmail
        Get
            Return AppSettings("DefaultAinRaisedEmail")
        End Get
    End Property
End Module
