'----------------------------------------------------------------------------------------------
' Filename    : ProjectManager.vb
' Description : All logical functionality related to a project will be found within this module.
'
' Release Initials  Date        Comment
' 2       TL        23/02/2011  The costings in GetPrintCostings now changed since PrintCost has been removed from additional print details
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL
Imports System.Globalization
Imports GenericUtilities

Public Module ProjectManager

#Region "Get Variations"

#Region "Project"

    ''' <summary>
    ''' Gets a project by its ID, simples
    ''' </summary>
    ''' <param name="projectId">The project id required</param>
    ''' <returns>The project object</returns>
    ''' <remarks></remarks>
    Public Function GetProject(ByVal projectId As Integer) As Project
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.ProjectDAO(context).GetByID(projectId)
    End Function

    ''' <summary>
    ''' Returns a project that doesn't change using the same context that is used throughout the project.
    ''' Creates a new context to use to get a copy from the db.
    ''' </summary>
    ''' <param name="projectId">The ID of the project</param>
    ''' <returns>The project object or nothing</returns>
    ''' <remarks></remarks>
    Public Function GetPinnedProject(ByVal projectId As Integer) As Project
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.ProjectDAO(staticContext).GetByID(projectId)
    End Function

    ''' <summary>
    ''' Does what it says.
    ''' </summary>
    ''' <param name="searchStr">Will search the project names, if the project contains the search string then it will be returned</param>
    ''' <param name="active">Defaulted to true, this can be altered to return all disabled projects.</param>
    ''' <returns>A list of all projects or those that contain the search string (if provided)</returns>
    ''' <remarks></remarks>
    Public Function GetAllActiveProjects(Optional ByVal searchStr As String = "", Optional ByVal active As Boolean = True) As List(Of Project)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return (From p In DAOGetter.ProjectDAO(context).GetAll()
                Where p.Name.Contains(searchStr) And p.Active = active
                Select p).ToList
    End Function

    Public Function GetAllProjects() As List(Of Project)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return (From p In DAOGetter.ProjectDAO(context).GetAll()
                Order By p.ID Descending
                Select p).ToList()

    End Function

    Public Function GetStoppedProjects() As List(Of Project)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return (From p In DAOGetter.ProjectDAO(context).GetAll()
                Where p.Stopped = True
                Order By p.ID Descending
                Select p).ToList

    End Function

    Public Function GetArchivedProjects() As List(Of Project)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return (From p In DAOGetter.ProjectDAO(context).GetAll()
                Where p.Stopped = False _
                AndAlso p.Active = True
                Order By p.ID Descending
                Select p).ToList

    End Function

    ''' <summary>
    ''' Will return count number of projects
    ''' </summary>
    ''' <param name="count">The number of projects wish to be returned.</param>
    ''' <returns>Returns count number of projects in a list</returns>
    ''' <remarks></remarks>
    Public Function GetActiveProjectsByCount(Optional ByVal count As Integer = 0) As List(Of Project)

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        If count = 0 Then
            Return (From p In DAOGetter.ProjectDAO(context).GetAll()
               Where p.Active = True
               Order By p.ID Descending _
               Select p).ToList
        Else
            Return (From p In DAOGetter.ProjectDAO(context).GetAll()
               Where p.Active = True
               Order By p.ID Descending _
               Take count _
               Select p).ToList
        End If

    End Function

    ''' <summary>
    ''' Gets all the projects related to the user.
    ''' </summary>
    ''' <param name="userId">The ID of the user in question</param>
    ''' <returns>All the projects related to that user.</returns>
    ''' <remarks></remarks>
    Public Function GetProjectsByUser(ByVal userId As Integer) As List(Of Project)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Dim projectList As List(Of Project)

        If UserManager.DoesUserHaveAGlobalRole(userId) Then
            projectList = DAOGetter.ProjectDAO(context).GetByActive()
        Else
            projectList = DAOGetter.ProjectDAO(context).GetByUser(userId)
        End If

        ' Remove those projects that are inactive
        Return (From p In projectList
                Where p.Active = True
                Select p).Distinct.ToList()
    End Function

    ''' <summary>
    ''' Will get all the projects that have reserved tiem in the question week and year.
    ''' </summary>
    ''' <param name="weekNumber">The week to match up against the projects reserved time.</param>
    ''' <param name="year">The year to match up against the projects reserved time.</param>
    ''' <returns>A list of projects that have reserved time that matches the weekNumber and year</returns>
    ''' <remarks></remarks>
    Public Function GetProjectsWithReservedTimeInWeek(ByVal weekNumber As Integer, ByVal year As Integer) As List(Of Project)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.ProjectDAO(context).GetProjectWithReservedTimeInWeek(weekNumber, year)
    End Function

    ''' <summary>
    ''' This functions accepts a list of projects which are converted and returned as an arraylist of anonymous types that contain the attributes 
    ''' ID - This is the project ID 
    ''' Name - This is a combination fo the name and the ID (e.g. 7039 - Tom's Kitchen)
    ''' </summary>
    ''' <param name="projectList">List of projects that need to be converted for binding</param>
    ''' <returns>An array list of anonymous types ready to be binded</returns>
    ''' <remarks>This is used for binding to dropdowns (we have to make it an anonymous type in order the change the text for the project name)</remarks>
    Public Function GetProjectsForDropdownBinding(ByVal projectList As IList(Of Project)) As ArrayList

        ' This functions accepts a list of projects which are converted and returned as an array list of anonymous types that contain the attributes:
        ' - ID - this is the project ID
        ' - Name - This is a combination fo the name and the ID (e.g. 7039 - Tom's Kitchen)

        Dim userProjAnon As New ArrayList

        For Each proj As Project In projectList
            Dim tmpProj = New With {.ID = proj.ID, .Name = String.Format("{0} - {1}", proj.ID, proj.Name)}
            userProjAnon.Add(tmpProj)
        Next

        Return userProjAnon

    End Function

    Public Function GetProjectsByUserAndRole(ByVal userId As Integer, ByVal roleId As Integer) As List(Of ProjectRoleAssociation)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.ProjectRoleAssociationDAO(context).GetByUserAndRole(userId, roleId)
    End Function

#End Region

#Region "Project Roles"

    Public Function GetProjectOwner(ByVal projectId As Integer) As AptUser
        Return GetUserInProjectRole(projectId, AppSettingsGet.OwnerRoleID)
    End Function

    Public Function GetProjectPORaiser(ByVal projectId As Integer) As AptUser
        Return GetUserInProjectRole(projectId, AppSettingsGet.PORaiserID)
    End Function

    Public Function GetProjectLegalApprover(ByVal projectId As Integer) As AptUser
        Return GetUserInProjectRole(projectId, AppSettingsGet.LegalApproverID)
    End Function

    Public Function GetProjectApprover(ByVal projectId As Integer) As AptUser
        Return GetUserInProjectRole(projectId, AppSettingsGet.ProjectApproverID)
    End Function

    Public Function GetProjectStudioQA(ByVal projectId As Integer) As AptUser
        Return GetUserInProjectRole(projectId, AppSettingsGet.StudioQAID)
    End Function

    Public Function GetProjectCoordinator(ByVal projectId As Integer) As AptUser
        Return GetUserInProjectRole(projectId, AppSettingsGet.GraphicsCoordinatorID)
    End Function

    Public Function GetProjectBrandManager(ByVal projectId As Integer) As AptUser
        Return GetUserInProjectRole(projectId, AppSettingsGet.BrandManagerID)
    End Function

    Public Function GetWilliamsLeaProjectManager(ByVal projectId As Integer) As AptUser
        Return GetUserInProjectRole(projectId, AppSettingsGet.WilliamsLeaProjectManagerID)
    End Function

#End Region

#Region "Project Misc"

    Public Function GetProjectBrandId(ByVal projectId As Integer) As Integer
        Dim schemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.BrandListDefinitionID, projectId)

        Return GetSchemaDataValue(schemaData)
    End Function

    Public Function GetProjectBrandIds(ByVal projectIds As List(Of Integer)) As Dictionary(Of Integer, Integer)

        Dim projectBrandIds As New Dictionary(Of Integer, Integer)
        Dim schemaData As List(Of SchemaData) = SchemaManager.GetSchemaDataByProjectsAndDefinitionId(AppSettingsGet.BrandListDefinitionID, projectIds)

        For Each data As SchemaData In schemaData
            projectBrandIds.Add(data.ParentID, GetSchemaDataValue(data))
        Next

        Return projectBrandIds

    End Function

    Public Function GetProjectOwners(ByVal projectIds As List(Of Integer)) As Dictionary(Of Integer, String)

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Dim ownerRoleId = Integer.Parse(AppSettingsGet.OwnerRoleID)
        Dim projectsRoleAssocs As List(Of ProjectRoleAssociation) = DAOGetter.ProjectRoleAssociationDAO(context).GetByProjectsAndRole(projectIds, ownerRoleId)
        Dim projectOwners As New Dictionary(Of Integer, String)

        For Each pra As ProjectRoleAssociation In projectsRoleAssocs
            projectOwners.Add(pra.ID, pra.User.FullName)
        Next

        Return projectOwners

    End Function

    Public Function GetProjectReferenceNumber(ByVal projectId As Integer) As String
        Dim schemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.PrintReferenceNumberID, projectId)

        If schemaData IsNot Nothing Then
            Return schemaData.SchemaElementValue
        Else
            Return ""
        End If
    End Function

    Public Function GetProjectTypeOfWork(ByVal projectId As Integer) As Integer
        Dim schemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.TypeOfWorkDefinitionID, projectId)

        Return GetSchemaDataValue(schemaData)
    End Function

    Public Function GetProjectBusinessArea(ByVal projectId As Integer) As Integer
        Dim schemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.BusinessAreaDefinitionID, projectId)

        Return GetSchemaDataValue(schemaData)
    End Function

    Public Function GetProjectBrandId(ByVal projectId) As Integer

        Dim schemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.BrandListDefinitionID, projectId)

        Return GetSchemaDataValue(schemaData)

    End Function

    Public Function GetProjectBrand(ByVal projectId) As String

        Dim schemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.BrandListDefinitionID, projectId)
        Dim brandListNode As ListNode = ListManager.GetListNode(schemaData.ID)

        Return brandListNode.Name

    End Function

    Public Function GetBriefSubmittedDate(ByVal projectId) As String

        ' Note this is not when the brief is first saved but when it is finally submitted 
        Dim tokens As List(Of Token) = WorkflowManager.GetFreeTokensAtPlaceInProject(projectId, AppSettingsGet.BriefSubmittedPlaceId)

        If tokens.Count > 0 Then
            If tokens(0).TokenStatus.ID = AppSettingsGet.TokenStatusConsumed Then
                Return tokens(0).ConsumedDate.ToString("dd/MM/yyyy")
            End If
        End If

        Return ""

    End Function

    Public Function GetProjectQuote(ByVal projectId As Integer) As Integer
        Dim schemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.QuoteDefinitionID, projectId)

        If schemaData IsNot Nothing Then
            Return schemaData.SchemaElementValue
        Else
            Return 0
        End If
    End Function

    Private Function GetSchemaDataValue(ByVal schemaData As SchemaData) As Integer
        If schemaData IsNot Nothing Then
            Return CType(schemaData.SchemaElementValue, Integer)
        Else
            Return 0
        End If
    End Function

    Public Function GetProjectKittingBrief(ByVal projectKitBriefId As Integer) As ProjectKittingBrief

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ProjectKittingBriefDAO(context).GetByID(projectKitBriefId)

    End Function

    Public Function GetProjectKittingBriefByProject(ByVal projectId As Integer) As ProjectKittingBrief

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ProjectKittingBriefDAO(context).GetByProject(projectId)

    End Function

    Public Function GetPremiumBrief(ByVal premiumBriefId As Integer) As PremiumBrief

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.PremiumBriefDAO(context).GetByID(premiumBriefId)

    End Function

    Public Function GetPremiumBriefByProject(ByVal projectId As Integer) As PremiumBrief

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.PremiumBriefDAO(context).GetByProject(projectId)

    End Function

    Public Function GetAllProjectAndUserAssociations() As List(Of ProjectRoleAssociation)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ProjectRoleAssociationDAO(context).GetAllProjectAndUserAssociations()
    End Function

#End Region

#End Region

#Region "Insertion / Update / Removal"

    Public Sub AddNewProject(ByRef newProject As Project, ByVal userId As Integer, ByVal loggedInUserId As Integer)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        modLogManager.Debug(String.Format("AddNewProject - Adding new project - Name : {0} - Is BD : {1}", newProject.Name, newProject.IsBDProject))
        modLogManager.Info(String.Format("AddNewProject - Adding new project - Name : {0}", newProject.Name))

        DAOGetter.ProjectDAO(context).Insert(newProject)

        modLogManager.Debug(String.Format("AddNewProject - Add Successful, given ID {0}", newProject.ID))

        modLogManager.Debug("AddNewProject - Create new start token for the project")

        Dim startTokenId As Integer = WorkflowManager.CreateStartToken(newProject.ID, newProject.IsBDProject)

        modLogManager.Debug("AddNewProject - New start token created!")

        modLogManager.Debug("AddNewProject - Assign project roles for the new project")
        ' Create User Roles
        UserManager.GiveUserProjectRole(newProject.ID, userId, AppSettingsGet.OwnerRoleID)
        modLogManager.Debug("AddNewProject - Owner set")
        UserManager.GiveUserProjectRole(newProject.ID, userId, AppSettingsGet.PORaiserID)
        modLogManager.Debug("AddNewProject - PO Raiser set")
        UserManager.GiveUserProjectRole(newProject.ID, userId, AppSettingsGet.LegalApproverID)
        modLogManager.Debug("AddNewProject - Legal Approver set")
        UserManager.GiveUserProjectRole(newProject.ID, userId, AppSettingsGet.BrandManagerID)
        modLogManager.Debug("AddNewProject - Brand Manager set")

        ' Set Studio QA and Coordinator
        Dim studioQADefault As AptUser = UserManager.GetUserByUsername(AppSettingsGet.DefaultQARaiser)

        Dim coordinatorDefault As New AptUser

        If newProject.IsBDProject Then
            coordinatorDefault = UserManager.GetUserByUsername(AppSettingsGet.DefaultBDCoordinator)
            UserManager.GiveUserProjectRole(newProject.ID, coordinatorDefault.ID, AppSettingsGet.GraphicsCoordinatorID)
            modLogManager.Debug("AddNewProject - Coordinator set")
        Else
            ' Karl asked for no coord if non bd
            ' Karl has asked for it back so uncomment
            ' Recomment from line 361 if this is changed back (no coord if non bd)
            ' Karl has asked for no default coord again
            ' coordinatorDefault = UserManager.GetUserByUsername(AppSettingsGet.DefaultNonBDCoordinator)
        End If

        UserManager.GiveUserProjectRole(newProject.ID, studioQADefault.ID, AppSettingsGet.StudioQAID)
        modLogManager.Debug("AddNewProject - Studio QA set")

        ' Audit Trail
        modLogManager.Debug("AddNewProject - Post Audit Trail")
        Dim auditTrailMessage As String = String.Format("A new project has been created. {0}{0}{1}", AppSettingsGet.HTMLNewLine, GenerateNewProjectSummary(newProject))

        AuditTrailManager.PostAudit(auditTrailMessage, loggedInUserId, newProject.ID, AppSettingsGet.AddAuditChangeTypeID, AppSettingsGet.ProjectCoreDetailsAuditSectionID)

        ' Send the start email which was missed out
        ' Only do this for a BD project because non BD projects  
        SendResponsibleEmail(startTokenId)

        If newProject.IsBDProject = False Then
            ' We want to automatically bypass the first step if the project is a non BD project
            WorkflowManager.CompleteProcess(startTokenId, , userId, "AUTOMATICALLY BYPASSED STEP - NON BD PROJECT")
        End If

        modLogManager.Debug("AddNewProject - Function Complete")
    End Sub

    Public Function UpdateProject(ByVal project As Project, ByVal oldProject As Project,
                                  ByVal userId As Integer, _
                                  Optional ByVal schemaAttributesAudit As String = "") As Boolean

        modLogManager.Debug("UpdateProject - Start")
        Dim retVal As Boolean = True
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        ' Audit trail message (summary of changes)
        'Dim changedPropertiesSummary As String = modReflection.GenerateAlteredPropertiesSummary(oldProject, project, AppSettingsGet.HTMLNewLine)

        Dim changedPropertiesSummary As String = GetModifiedProjectSummary(project, oldProject, schemaAttributesAudit)

        Dim auditTrailMessage As String = String.Format("Project Details have been edited. {0}{0}{1}", _
                                                        AppSettingsGet.HTMLNewLine, _
                                                        changedPropertiesSummary)

        If changedPropertiesSummary <> "" Then

            AuditTrailManager.PostAudit(auditTrailMessage, userId, project.ID, _
                                    AppSettingsGet.EditAuditChangeTypeID, AppSettingsGet.ProjectCoreDetailsAuditSectionID)

        End If

        DAOGetter.ProjectDAO(context).Update(project)

        modLogManager.Debug(String.Format("UpdateProject - Complete - Return value = {0}", retVal))

        Return retVal
    End Function

    Public Sub AlterOwner(ByVal projectId As Integer, ByVal userId As Integer, ByVal loggedInUserId As Integer)
        UserManager.AlterUserInProjectRole(projectId, userId, AppSettingsGet.OwnerRoleID, loggedInUserId)
    End Sub

    Public Sub AlterPORaiser(ByVal projectId As Integer, ByVal userId As Integer, ByVal loggedInUserId As Integer)
        UserManager.AlterUserInProjectRole(projectId, userId, AppSettingsGet.PORaiserID, loggedInUserId)
    End Sub

    Public Sub AlterLegalApprover(ByVal projectId As Integer, ByVal userId As Integer, ByVal loggedInUserId As Integer)
        UserManager.AlterUserInProjectRole(projectId, userId, AppSettingsGet.LegalApproverID, loggedInUserId)
    End Sub

    Public Sub AlterProjectApprover(ByVal projectId As Integer, ByVal userId As Integer, ByVal loggedInUserId As Integer)
        UserManager.AlterUserInProjectRole(projectId, userId, AppSettingsGet.ProjectApproverID, loggedInUserId)
    End Sub

    Public Sub AlterStudioQA(ByVal projectId As Integer, ByVal userId As Integer, ByVal loggedInUserId As Integer)
        UserManager.AlterUserInProjectRole(projectId, userId, AppSettingsGet.StudioQAID, loggedInUserId)
    End Sub

    Public Sub AlterCoordinator(ByVal projectId As Integer, ByVal userId As Integer, ByVal loggedInUserId As Integer)
        UserManager.AlterUserInProjectRole(projectId, userId, AppSettingsGet.GraphicsCoordinatorID, loggedInUserId)
    End Sub

    Public Sub AlterBrandManager(ByVal projectId As Integer, ByVal userId As Integer, ByVal loggedInUserId As Integer)
        UserManager.AlterUserInProjectRole(projectId, userId, AppSettingsGet.BrandManagerID, loggedInUserId)
    End Sub

    Public Sub AlterWilliamsLeaProjectManager(ByVal projectId As Integer, ByVal UserId As Integer, ByVal loggedInUserId As Integer)
        UserManager.AlterUserInProjectRole(projectId, UserId, AppSettingsGet.WilliamsLeaProjectManagerID, loggedInUserId)
    End Sub

    Public Sub StartStopProject(ByVal projectId As Integer)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Dim tmpProject As Project = DAOGetter.ProjectDAO(context).GetByID(projectId)

        If tmpProject.Stopped = True Then
            tmpProject.Stopped = False
            tmpProject.Active = True
        Else
            ' User wants to start the project again
            tmpProject.Stopped = True
            tmpProject.Active = False
        End If

        DAOGetter.ProjectDAO(context).Update(tmpProject)
    End Sub

    Public Sub ArchiveUnarchiveProject(ByVal projectId As Integer)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Dim tmpProject As Project = DAOGetter.ProjectDAO(context).GetByID(projectId)

        If tmpProject.Active = True Then
            tmpProject.Active = False
        Else
            tmpProject.Active = True
        End If

        DAOGetter.ProjectDAO(context).Update(tmpProject)
    End Sub

#Region "Project Schema Values"

    Public Sub AlterPrintReference(ByVal projectId As Integer, ByVal printReferenceNumber As String)
        Dim printRefSchemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.PrintReferenceNumberID, projectId)

        If printRefSchemaData Is Nothing Then
            printRefSchemaData = New SchemaData()
        End If

        SetGeneralSchemaDataProjectFields(printRefSchemaData)

        printRefSchemaData.SchemaElementValue = printReferenceNumber
        printRefSchemaData.SchemaDefinition = SchemaManager.GetSchemaDefinitionById(AppSettingsGet.PrintReferenceNumberID)
        printRefSchemaData.SchemaElementType = "textfield"
        printRefSchemaData.ParentID = projectId

        SchemaManager.SaveSchemaData(printRefSchemaData)
    End Sub

    Public Sub AlterQuote(ByVal projectId As Integer, ByVal quoteValue As String)
        Dim quoteSchemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.QuoteDefinitionID, projectId)

        If quoteSchemaData Is Nothing Then
            quoteSchemaData = New SchemaData
        End If

        SetGeneralSchemaDataProjectFields(quoteSchemaData)

        quoteSchemaData.SchemaElementValue = quoteValue
        quoteSchemaData.SchemaDefinition = SchemaManager.GetSchemaDefinitionById(AppSettingsGet.QuoteDefinitionID)
        quoteSchemaData.SchemaElementType = "textfield"
        quoteSchemaData.ParentID = projectId

        SchemaManager.SaveSchemaData(quoteSchemaData)
    End Sub

    Public Sub AlterBrandSelection(ByVal projectId As Integer, ByVal selectedBrandId As Integer)
        Dim brandSchemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.BrandListDefinitionID, projectId)

        If brandSchemaData Is Nothing Then
            brandSchemaData = New SchemaData()
        End If

        SetGeneralSchemaDataProjectFields(brandSchemaData)

        brandSchemaData.SchemaElementValue = selectedBrandId
        brandSchemaData.SchemaDefinition = SchemaManager.GetSchemaDefinitionById(AppSettingsGet.BrandListDefinitionID)
        brandSchemaData.SchemaElementType = "list"
        brandSchemaData.ParentID = projectId

        SchemaManager.SaveSchemaData(brandSchemaData)
    End Sub

    Public Sub AlterTypeOfWorkSelection(ByVal projectId As Integer, ByVal selectedTypeOfWorkId As Integer)
        Dim typeOfWorkSchemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.TypeOfWorkDefinitionID, projectId)

        If typeOfWorkSchemaData Is Nothing Then
            typeOfWorkSchemaData = New SchemaData()
        End If

        SetGeneralSchemaDataProjectFields(typeOfWorkSchemaData)

        typeOfWorkSchemaData.SchemaElementValue = selectedTypeOfWorkId
        typeOfWorkSchemaData.SchemaDefinition = SchemaManager.GetSchemaDefinitionById(AppSettingsGet.TypeOfWorkDefinitionID)
        typeOfWorkSchemaData.SchemaElementType = "list"
        typeOfWorkSchemaData.ParentID = projectId

        SchemaManager.SaveSchemaData(typeOfWorkSchemaData)
    End Sub

    Public Sub AlterBusinessAreaSelection(ByVal projectId As Integer, ByVal selectBusinessAreaId As Integer)

        Dim businessAreaSchemaData As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.BusinessAreaDefinitionID, projectId)

        If businessAreaSchemaData Is Nothing Then
            businessAreaSchemaData = New SchemaData()
        End If

        SetGeneralSchemaDataProjectFields(businessAreaSchemaData)

        businessAreaSchemaData.SchemaElementValue = selectBusinessAreaId
        businessAreaSchemaData.SchemaDefinition = SchemaManager.GetSchemaDefinitionById(AppSettingsGet.BusinessAreaDefinitionID)
        businessAreaSchemaData.SchemaElementType = "list"
        businessAreaSchemaData.ParentID = projectId

        SchemaManager.SaveSchemaData(businessAreaSchemaData)

    End Sub

    Private Sub SetGeneralSchemaDataProjectFields(ByRef schemaData As SchemaData)
        schemaData.Created = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        schemaData.Modified = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        schemaData.ParentID = 0
        schemaData.SchemaEntityID = AppSettingsGet.SchemaProjectEntityID
    End Sub

#End Region

    Public Function RemoveProject(ByVal projectId As Integer) As Boolean
        modLogManager.Debug(String.Format("RemoveProject - Start (ID = {0}", projectId))
        Dim retVal As Boolean = True
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Dim project As Project = DAOGetter.ProjectDAO(context).GetByID(projectId)

        project.Active = False
        DAOGetter.ProjectDAO(context).Update(project)

        modLogManager.Debug("RemoveProject - Complete")
        Return retVal
    End Function

    Public Sub SaveProjectKittingBrief(ByVal projectKittingBrief As ProjectKittingBrief, ByVal userId As Integer)

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Dim auditLog As String = ""
        Dim changeType As Integer

        If projectKittingBrief.ID = 0 Then
            changeType = AppSettingsGet.AddAuditChangeTypeID
            auditLog = GetNewKittingBriefAuditLog(projectKittingBrief)
            DAOGetter.ProjectKittingBriefDAO(context).Insert(projectKittingBrief)
        Else
            changeType = AppSettingsGet.EditAuditChangeTypeID
            auditLog = GetModifiedKittingBriefAuditLog(projectKittingBrief)
            DAOGetter.ProjectKittingBriefDAO(context).Update(projectKittingBrief)
        End If

        AuditTrailManager.PostAudit(auditLog, userId, projectKittingBrief.Project.ID, changeType, AppSettingsGet.ProjectCoreDetailsAuditSectionID)

    End Sub

    Public Sub SavePremiumBrief(ByVal premiumBrief As PremiumBrief, ByVal userId As Integer)

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Dim auditLog As String = ""
        Dim changeType As Integer

        If premiumBrief.ID = 0 Then
            changeType = AppSettingsGet.AddAuditChangeTypeID
            auditLog = GetNewPremiumBriefAuditLog(premiumBrief)
            DAOGetter.PremiumBriefDAO(context).Insert(premiumBrief)
        Else
            changeType = AppSettingsGet.EditAuditChangeTypeID
            auditLog = GetModifiedPremiumBriefAuditLog(premiumBrief)
            DAOGetter.PremiumBriefDAO(context).Update(premiumBrief)
        End If

        AuditTrailManager.PostAudit(auditLog, userId, premiumBrief.Project.ID, changeType, AppSettingsGet.ProjectCoreDetailsAuditSectionID)

    End Sub

    Public Sub SaveProjectCostings(ByVal SaveProjectCostings As ProjectCostings)

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        If SaveProjectCostings.ID = 0 Then
            DAOGetter.ProjectCostingsDAO(context).Insert(SaveProjectCostings)
        Else
            DAOGetter.ProjectCostingsDAO(context).Update(SaveProjectCostings)
        End If

    End Sub

#End Region

#Region "Searching / Filtering"

    Public Function SearchProjects(ByVal searchStr As String) As List(Of Project)

        Return (From p In GetAllActiveProjects()
                Where p.Name.ToLowerInvariant().Contains(searchStr.ToLowerInvariant())
                Select p).ToList
    End Function

    Public Function SearchProjectsByOwner(ByVal searchStr As String) As List(Of Project)

        Return (From p In GetAllActiveProjects()
                Where GetProjectOwner(p.ID) IsNot Nothing AndAlso GetProjectOwner(p.ID).FullName.ToLowerInvariant().Contains(searchStr.ToLowerInvariant())
                Select p).ToList
    End Function

    Public Function SearchProjectsByCoordinator(ByVal searchStr As String) As List(Of Project)

        Return (From p In GetAllActiveProjects()
                Where UserManager.GetUserInProjectRole(p.ID, AppSettingsGet.GraphicsCoordinatorID) IsNot Nothing _
                    AndAlso UserManager.GetUserInProjectRole(p.ID, AppSettingsGet.GraphicsCoordinatorID).FullName.ToLowerInvariant().Contains(searchStr.ToLowerInvariant())
                Select p).ToList
    End Function

    Public Function SearchProjectsByArtworker(ByVal searchStr As String) As List(Of Project)

        Return (From p In GetAllActiveProjects()
                Where UserManager.GetUserInProjectRole(p.ID, AppSettingsGet.ArtworkerID) IsNot Nothing _
                    AndAlso UserManager.GetUserInProjectRole(p.ID, AppSettingsGet.ArtworkerID).FullName.ToLowerInvariant().Contains(searchStr.ToLowerInvariant())
                Select p).ToList
    End Function

    Public Sub FilterByBrand(ByRef projectList As List(Of Project), ByVal brandId As Integer)

        Dim projectIds = projectList.Select(Function(x) x.ID)
        Dim projectBrandIds = GetProjectBrandIds(projectIds)
        Dim projectIdsWithFilteredBrand = (From p In projectBrandIds
                                           Where p.Value = brandId
                                           Select p.Key).ToList()

        projectList = (From p In projectList
                       Where p.Active AndAlso _
                       projectIdsWithFilteredBrand.Contains(p.ID)
                       Select p).ToList
    End Sub

    Public Sub FilterByOwner(ByRef projectList As List(Of Project), ByVal ownerText As String)
        projectList = (From p In projectList
                       Where GetProjectOwner(p.ID).FullName.ToLowerInvariant.Contains(ownerText.ToLowerInvariant)
                       Select p).ToList
    End Sub

    Public Sub FilterByTradeInDate(ByRef projectList As List(Of Project), ByVal tradeInDate As Date)
        projectList = (From p In projectList
                       Where p.RequiredDate.Date = tradeInDate
                       Select p).ToList
    End Sub

    Public Sub SortByName(ByRef projectList As List(Of Project))
        projectList = (From p In projectList
                       Select p
                       Order By p.Name).ToList
    End Sub

    Public Sub SortByOwner(ByRef projectList As List(Of Project))
        projectList = (From p In projectList
                       Select p
                       Order By GetProjectOwner(p.ID).Surname).ToList
    End Sub

    Public Sub SortByBrand(ByRef projectList As List(Of Project))

    End Sub

    Public Sub SortByTradeInDate(ByRef projectList As List(Of Project))
        projectList = (From p In projectList
                       Select p
                       Order By p.RequiredDate.Date.Ticks Descending).ToList
    End Sub

    Public Sub SortByID(ByRef projectList As List(Of Project))
        projectList = (From p In projectList
                       Select p
                       Order By p.ID Descending).ToList
    End Sub

#End Region

#Region "Utilities"

    Public Function ProjectAllowanceTimes(ByVal requiredDate As DateTime) As Boolean
        Dim weeksBetween As Integer = DateDiff(DateInterval.WeekOfYear, Date.Now, requiredDate)
        Dim isDateApplicable = True

        If weeksBetween < AppSettingsGet.TotalProjectDurationInWeeks Then
            isDateApplicable = False
        End If

        Return isDateApplicable
    End Function

    Public Function GenerateNewProjectSummary(ByVal projectObj As Project) As String
        Dim projectSummary As String = ""

        projectSummary += String.Format("Name : {0}{1}", projectObj.Name, AppSettingsGet.HTMLNewLine)
        projectSummary += String.Format("Required Date : {0}{1}", projectObj.RequiredDate.ToString("dd/MM/yyyy"), AppSettingsGet.HTMLNewLine)

        Return projectSummary
    End Function

#End Region

#Region "Costings"

    Public Function GetArtworkCostings(ByVal projectId As Integer) As Decimal

        Dim projectElements As List(Of Element) = ElementManager.GetElementsByProject(projectId)
        Dim artworkCosting As Decimal = 0.0

        For Each tmpElement As Element In projectElements

            Dim tmpAdditionalDetails As ElementAdditionalDetails = ElementManager.GetElementAdditionalInfo(tmpElement.ID)

            If tmpAdditionalDetails IsNot Nothing Then
                artworkCosting += tmpAdditionalDetails.ArtworkCost
            End If

        Next

        Return artworkCosting

    End Function

    Public Function GetPrintCostings(ByVal projectId As Integer) As Decimal

        Dim projectElements As List(Of Element) = ElementManager.GetElementsByProject(projectId)
        Dim itemCosting As Decimal = 0.0

        For Each tmpElement As Element In projectElements

            Dim tmpAdditionalDetails As ElementAdditionalDetails = ElementManager.GetElementAdditionalInfo(tmpElement.ID)
            Dim tmpArtworkDetails As ElementArtworkDetails = ElementManager.GetElementArtworkInfoByElement(tmpElement.ID)

            If tmpAdditionalDetails IsNot Nothing And tmpArtworkDetails IsNot Nothing Then
                itemCosting += tmpAdditionalDetails.PrintCost
                ' itemCosting += tmpArtworkDetails.PrintCost ' Newly Added this is for non BD projects apparently
            End If

        Next

        Return itemCosting

    End Function

    Public Function GetKittingCosts(ByVal projectId As Integer) As Decimal

        Dim projectKits As List(Of Kit) = KitManager.GetKitsByProject(projectId)
        Dim kittingCosting As Decimal = 0.0

        For Each tmpKit As Kit In projectKits
            'kittingCosting += tmpKit.Cost
        Next

        Return kittingCosting

    End Function

    Public Function GetPremiumCosts(ByVal projectId As Integer) As Decimal

        Dim projectAdditionalElements As List(Of AdditionalElement) = ElementManager.GetAdditionalElementsByProject(projectId)
        Dim premiumCosting As Decimal = 0.0

        For Each tmpAdditionalElement As AdditionalElement In projectAdditionalElements
            premiumCosting += tmpAdditionalElement.Cost
        Next

        Return premiumCosting

    End Function

    Public Function GetProjectCostings(ByVal projectId As Integer) As ProjectCostings
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ProjectCostingsDAO(context).GetByProject(projectId)
    End Function

#End Region

#Region "Clone"

    Private Function GetCopyPremiumBrief(ByVal id As Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.PremiumBriefDAO(staticContext).GetByID(id)
    End Function

    Private Function GetCopyKittingBrief(ByVal id As Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ProjectKittingBriefDAO(staticContext).GetByID(id)
    End Function

#End Region

#Region "Audit Functionality"

    Private Function GetNewKittingBriefAuditLog(ByVal newKittingBrief As ProjectKittingBrief, Optional ByVal newline As String = "<br />") As String

        Dim auditLog As String = ""
        auditLog += "Final Kitting brief has been submitted"

        Return auditLog

    End Function

    Private Function GetModifiedKittingBriefAuditLog(ByVal newKittingBrief As ProjectKittingBrief, Optional ByVal newLine As String = "<br />") As String

        Dim oldKittingBrief As ProjectKittingBrief = GetCopyKittingBrief(newKittingBrief.ID)
        Dim auditLog As String = ""
        auditLog += "Final Kitting Brief has been altered"
        auditLog += newLine

        If newKittingBrief.Kit.ID <> oldKittingBrief.Kit.ID Then
            auditLog += String.Format("Kit has been changed from {0} to {1}", oldKittingBrief.Kit.Name, newKittingBrief.Kit.Name)
            auditLog += newLine
        End If

        If newKittingBrief.BuiltByMDA <> oldKittingBrief.BuiltByMDA Then
            auditLog += String.Format("Build By MDA has been changed from {0} to {1}", oldKittingBrief.BuiltByMDA, newKittingBrief.BuiltByMDA)
            auditLog += newLine
        End If

        If newKittingBrief.Funding <> oldKittingBrief.Funding Then
            auditLog += String.Format("Funding has been changed from {0} to {1}", oldKittingBrief.BuiltByMDA, newKittingBrief.BuiltByMDA)
            auditLog += newLine
        End If

        If newKittingBrief.StockCode <> oldKittingBrief.StockCode Then
            auditLog += String.Format("Stock Code has been changed from {0} to {1}", oldKittingBrief.StockCode, newKittingBrief.StockCode)
            auditLog += newLine
        End If

        'If newKittingBrief.NoPartsPerKit <> oldKittingBrief.NoPartsPerKit Then
        '    auditLog += String.Format("Number of Parts per Kit has been changed from {0} to {1}", oldKittingBrief.NoPartsPerKit, newKittingBrief.NoPartsPerKit)
        '    auditLog += newLine
        'End If

        If newKittingBrief.TotalNoKits <> oldKittingBrief.TotalNoKits Then
            auditLog += String.Format("Total Number of Kits has been changed from {0} to {1}", oldKittingBrief.TotalNoKits, newKittingBrief.TotalNoKits)
            auditLog += newLine
        End If

        If newKittingBrief.Instructions <> oldKittingBrief.Instructions Then
            auditLog += String.Format("Instructions have been altered")
            auditLog += newLine
        End If

        If newKittingBrief.KitsOnStockDate <> oldKittingBrief.KitsOnStockDate Then
            auditLog += String.Format("Kits on Stock Date has been changed from {0} to {1}",
                                      FormatHelper.FormatDateWithoutTime(oldKittingBrief.KitsOnStockDate), _
                                      FormatHelper.FormatDateWithoutTime(newKittingBrief.KitsOnStockDate))
            auditLog += newLine
        End If

        'If newKittingBrief.ProposalRequiredDate <> oldKittingBrief.ProposalRequiredDate Then
        '    auditLog += String.Format("Proposal Requirement Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldKittingBrief.ProposalRequiredDate), _
        '                              FormatHelper.FormatDateWithoutTime(newKittingBrief.ProposalRequiredDate))
        '    auditLog += newLine
        'End If

        'If newKittingBrief.QuoteProvidedDate <> oldKittingBrief.QuoteProvidedDate Then
        '    auditLog += String.Format("Quote Provided Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldKittingBrief.QuoteProvidedDate), _
        '                              FormatHelper.FormatDateWithoutTime(newKittingBrief.QuoteProvidedDate))
        '    auditLog += newLine
        'End If

        'If newKittingBrief.KitBuildDate <> oldKittingBrief.KitBuildDate Then
        '    auditLog += String.Format("Kit To Be Built Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldKittingBrief.KitBuildDate), _
        '                              FormatHelper.FormatDateWithoutTime(newKittingBrief.KitBuildDate))
        '    auditLog += newLine
        'End If

        'If newKittingBrief.DeliveryDate <> oldKittingBrief.DeliveryDate Then
        '    auditLog += String.Format("Delivery Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldKittingBrief.DeliveryDate), _
        '                              FormatHelper.FormatDateWithoutTime(newKittingBrief.DeliveryDate))
        '    auditLog += newLine
        'End If

        If newKittingBrief.InTradeDate <> oldKittingBrief.InTradeDate Then
            auditLog += String.Format("In-trade date has been changed from {0} to {1}",
                                      FormatHelper.FormatDateWithoutTime(oldKittingBrief.InTradeDate), _
                                      FormatHelper.FormatDateWithoutTime(newKittingBrief.InTradeDate))
            auditLog += newLine
        End If

        If newKittingBrief.ExpiryDate <> oldKittingBrief.ExpiryDate Then
            auditLog += String.Format("Expiry has been changed from {0} to {1}",
                                      FormatHelper.FormatDateWithoutTime(oldKittingBrief.ExpiryDate), _
                                      FormatHelper.FormatDateWithoutTime(newKittingBrief.ExpiryDate))
            auditLog += newLine
        End If

        Return auditLog

    End Function

    Private Function GetNewPremiumBriefAuditLog(ByVal newPremBrief As PremiumBrief) As String

        Dim auditLog As String = ""
        auditLog += "Premium brief has been submitted"

        Return auditLog

    End Function

    Private Function GetModifiedPremiumBriefAuditLog(ByVal newPremBrief As PremiumBrief, Optional ByVal newLine As String = "<br />") As String

        Dim auditLog As String = ""
        Dim oldPremiumBrief As PremiumBrief = GetCopyPremiumBrief(newPremBrief.ID)
        auditLog += "Premium brief has been altered"
        auditLog += newLine

        'If newPremBrief.OutlineBrief <> oldPremiumBrief.OutlineBrief Then
        '    auditLog += String.Format("Outline Brief has been altered")
        '    auditLog += newLine
        'End If

        'If newPremBrief.BriefSubmittedDate <> oldPremiumBrief.BriefSubmittedDate Then
        '    auditLog += String.Format("Brief Submitted Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldPremiumBrief.BriefSubmittedDate),
        '                              FormatHelper.FormatDateWithoutTime(newPremBrief.BriefSubmittedDate))
        '    auditLog += newLine
        'End If

        'If newPremBrief.ProposalRequiredDate <> oldPremiumBrief.ProposalRequiredDate Then
        '    auditLog += String.Format("Proposal Required Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldPremiumBrief.ProposalRequiredDate),
        '                              FormatHelper.FormatDateWithoutTime(newPremBrief.ProposalRequiredDate))
        '    auditLog += newLine
        'End If

        'If newPremBrief.QuoteProvidedDate <> oldPremiumBrief.QuoteProvidedDate Then
        '    auditLog += String.Format("Quote Provided Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldPremiumBrief.QuoteProvidedDate),
        '                              FormatHelper.FormatDateWithoutTime(newPremBrief.QuoteProvidedDate))
        '    auditLog += newLine
        'End If

        'If newPremBrief.ArtworkAvailableDate <> oldPremiumBrief.ArtworkAvailableDate Then
        '    auditLog += String.Format("Artwork Available Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldPremiumBrief.ArtworkAvailableDate),
        '                              FormatHelper.FormatDateWithoutTime(newPremBrief.ArtworkAvailableDate))
        '    auditLog += newLine
        'End If

        'If newPremBrief.PODate <> oldPremiumBrief.PODate Then
        '    auditLog += String.Format("PO Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldPremiumBrief.PODate),
        '                              FormatHelper.FormatDateWithoutTime(newPremBrief.PODate))
        '    auditLog += newLine
        'End If

        'If newPremBrief.DeliveryDate <> oldPremiumBrief.DeliveryDate Then
        '    auditLog += String.Format("Delivery Date has been changed from {0} to {1}",
        '                              FormatHelper.FormatDateWithoutTime(oldPremiumBrief.DeliveryDate),
        '                              FormatHelper.FormatDateWithoutTime(newPremBrief.DeliveryDate))
        '    auditLog += newLine
        'End If

        'If newPremBrief.DeliveryAddress <> oldPremiumBrief.DeliveryAddress Then
        '    auditLog += String.Format("Delivery Address has been changed from {0} to {1}",
        '                              oldPremiumBrief.DeliveryAddress,
        '                              newPremBrief.DeliveryAddress)
        '    auditLog += newLine
        'End If

        If newPremBrief.TotalCostingEstimate <> oldPremiumBrief.TotalCostingEstimate Then
            auditLog += String.Format("Estimated Total Costing has been changed from {0} to {1}",
                                      oldPremiumBrief.TotalCostingEstimate, newPremBrief.TotalCostingEstimate)
            auditLog += newLine
        End If

        If newPremBrief.TotalCostingFinal <> oldPremiumBrief.TotalCostingFinal Then
            auditLog += String.Format("Final Total Costing has been changed from {0} to {1}",
                                      oldPremiumBrief.TotalCostingFinal, newPremBrief.TotalCostingFinal)
            auditLog += newLine
        End If

        If newPremBrief.ProductionTimeCostEstimate <> oldPremiumBrief.ProductionTimeCostEstimate Then
            auditLog += String.Format("Estimated Production Time Cost has been changed from {0} to {1}",
                                      oldPremiumBrief.ProductionTimeCostEstimate, newPremBrief.ProductionTimeCostEstimate)
            auditLog += newLine
        End If

        If newPremBrief.ProductionTimeCostFinal <> oldPremiumBrief.ProductionTimeCostFinal Then
            auditLog += String.Format("Final Production Time Cost has been changed from {0} to {1}",
                                      oldPremiumBrief.ProductionTimeCostFinal, newPremBrief.ProductionTimeCostFinal)
            auditLog += newLine
        End If


        If newPremBrief.PreviousCostEstimate <> oldPremiumBrief.PreviousCostEstimate Then
            auditLog += String.Format("Estimated Previous Cost has been changed from {0} to {1}",
                                      oldPremiumBrief.PreviousCostEstimate, newPremBrief.PreviousCostEstimate)
            auditLog += newLine
        End If

        If newPremBrief.PreviousCostFinal <> oldPremiumBrief.PreviousCostFinal Then
            auditLog += String.Format("Final Previous Cost has been changed from {0} to {1}",
                                      oldPremiumBrief.PreviousCostFinal, newPremBrief.PreviousCostFinal)
            auditLog += newLine
        End If

        If newPremBrief.PreviousSupplier <> oldPremiumBrief.PreviousSupplier Then
            auditLog += String.Format("Information regarding the previous supplier has been edited")
            auditLog += newLine
        End If

        Return auditLog

    End Function

    Private Function GetModifiedProjectSummary(ByVal changedProject As Project, ByVal oldProject As Project, _
                                               ByVal schemaAudit As String,
                                               Optional ByVal newLine As String = "<br>") As String

        Dim summary As String = ""

        If changedProject.Name <> oldProject.Name Then
            summary += String.Format("Project Name has been changed from {0} to {1}", oldProject.Name, changedProject.Name)
            summary += newLine
        End If

        If changedProject.BudgetCode <> oldProject.BudgetCode Then
            If oldProject.BudgetCode Is Nothing Then
                summary += String.Format("SAP Codes has been set to {0}", changedProject.BudgetCode)
                summary += newLine
            Else
                summary += String.Format("SAP Codes has been changed from {0} to {1}", oldProject.BudgetCode, changedProject.BudgetCode)
                summary += newLine
            End If
        End If

        If changedProject.RequiredDate <> oldProject.RequiredDate Then
            summary += String.Format("In-Trade Date has been changed from {0} to {1}", oldProject.RequiredDate, changedProject.RequiredDate)
            summary += newLine
        End If

        If oldProject.RequiredPrintDate.HasValue = False Then
            If changedProject.RequiredPrintDate.HasValue Then
                summary += String.Format("Print Required Delivery Date has been set to {0}", changedProject.RequiredPrintDate.Value.ToString("dd/MM/yyyy"))
                summary += newLine
            End If
        Else
            If oldProject.RequiredPrintDate.Value <> changedProject.RequiredPrintDate.Value Then
                summary += String.Format("Print Required Date has been changed from {0} to {1}", oldProject.RequiredPrintDate.Value.ToString("dd/MM/yyyy"), changedProject.RequiredPrintDate.Value.ToString("dd/MM/yyyy"))
                summary += newLine
            End If
        End If

        'If String.IsNullOrEmpty(rolesAudit) = False Then
        '    summary += rolesAudit
        'End If

        If String.IsNullOrEmpty(schemaAudit) = False Then
            summary += schemaAudit
        End If


        Return summary

    End Function

#End Region

End Module
