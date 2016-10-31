'----------------------------------------------------------------------------------------------
' Filename    : PermissionsManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptDAL
Imports aptEntities
Imports GenericUtilities

Public Module PermissionsManager

#Region "Project Permissions"

    ''' <summary>
    ''' Will calculate the positioning of the project and dependant on that position does the user have
    ''' the required role to be able to edit the project details.
    ''' </summary>
    ''' <param name="userId">The user in question</param>
    ''' <param name="projectId">The project in which is requested to be edited</param>
    ''' <returns>A boolean, true for editable and false for non-editable</returns>
    ''' <remarks></remarks>
    Public Function CanUserEditProject(ByVal userId As Integer, ByVal projectId As Integer) As Boolean
        ' Project details can be edited at all times by Coordinator, system admin, WLEA PM and MDA PM

        ' Are you the current projects co-ordinator?
        If UserHasProjectRole(userId, AppSettingsGet.GraphicsCoordinatorID, projectId) Then
            Return True
        End If

        ' Are you a system admin?
        If UserHasGlobalRole(userId, AppSettingsGet.AdminRoleID) Then
            Return True
        End If

        '' Are you a Williams Lea Project Manager?
        If UserHasGlobalRole(userId, AppSettingsGet.WilliamsLeaGlobalProjectManagerID) Then
            Return True
        End If

        '' Are you an MDA Project Manager?
        'If UserHasGlobalRole(userId, AppSettingsGet.MDAProjectManagerID) Then
        '    Return True
        'End If

        ' If the main transitions have been complete and we have reached this point in the function then return false
        ' If the sign off after challenge brief is consumed AND there isn't a free token at sign off and challenge brief (in rejection cases)
        If WorkflowManager.IsTransitionComplete(AppSettingsGet.BriefSignOffTransitionID, projectId) Then
            If WorkflowManager.IsFreeTokenAtTransition(AppSettingsGet.BriefSignOffTransitionID, projectId) = False _
                AndAlso WorkflowManager.IsFreeTokenAtTransition(AppSettingsGet.ChallengeBriefTransitionID, projectId) = False Then

                Return False
            End If
        End If

        ' do the same as above but for non bd
        If WorkflowManager.IsTransitionComplete(AppSettingsGet.BriefSignOffNonBDTransitionID, projectId) Then
            If WorkflowManager.IsFreeTokenAtTransition(AppSettingsGet.BriefSignOffNonBDTransitionID, projectId) = False _
                AndAlso WorkflowManager.IsFreeTokenAtTransition(AppSettingsGet.ChallengeBriefNonBDTransitionID, projectId) = False Then

                Return False
            End If
        End If

        ' OK, so we're before the sign off of the brief, is the user the BD AM, BD PM, WLEA AM and all MDA
        ' Karl says we do not need these anymore only the admin, coord and owner should be able to edit after this point

        If UserHasProjectRole(userId, AppSettingsGet.OwnerRoleID) Then
            Return True
        End If

        ' Are you a BD Account Manager?
        'If UserHasGlobalRole(userId, AppSettingsGet.BDAccountManagerID) Then
        '    Return True
        'End If

        '' Are you a BD Project Manager?
        'If UserHasGlobalRole(userId, AppSettingsGet.BDProjectManagerID) Then
        '    Return True
        'End If

        '' Are you a WLEA Account Manager?
        'If UserHasGlobalRole(userId, AppSettingsGet.WilliamsLeaAccountManagerID) Then
        '    Return True
        'End If

        '' Are you a MDA Procurement?
        'If UserHasGlobalRole(userId, AppSettingsGet.MDAProcurementID) Then
        '    Return True
        'End If

        '' Are you a MDA Kitting?
        'If UserHasGlobalRole(userId, AppSettingsGet.MDAKittingID) Then
        '    Return True
        'End If

        '' Are you a MDA On-site?
        'If UserHasGlobalRole(userId, AppSettingsGet.MDAOnSiteID) Then
        '    Return True
        'End If

        ' Sorry, you don't have enough power to achieve this functionality.
        Return False

    End Function

    ''' <summary>
    ''' Returns a boolean representing whether the user in question can reserve time for the project.
    ''' </summary>
    ''' <param name="userId">The user in whom is attempting to reserve time</param>
    ''' <param name="projectId">The project the time will be reserved against</param>
    ''' <returns>True for if the user can reserve time, false if not.</returns>
    ''' <remarks></remarks>
    Public Function CanAccessReserveTime(ByVal userId As Integer, ByVal projectId As Integer) As Boolean
        ' The ruling behind this one is that the user must fulfill both the following rules:
        ' Must be a co-ordinator
        ' The reserve time task must have already been completed
        If WorkflowManager.IsTransitionComplete(AppSettingsGet.ReserveTimeTransitionBDID, projectId) OrElse _
            WorkflowManager.IsTransitionComplete(AppSettingsGet.ReserveTimeTransitionNonBDID, projectId) Then

            If UserHasProjectRole(userId, AppSettingsGet.GraphicsCoordinatorID, projectId) Then
                Return True
            End If
        End If

        ' Me don't think so
        Return False

    End Function

    Public Function CanAccessProjectHistory(ByVal userId As Integer) As Boolean
        ' If the user is an admion or a graphics coordinator then they can see the project
        If UserHasGlobalRole(userId, AppSettingsGet.AdminRoleID) OrElse _
            UserHasProjectRole(userId, AppSettingsGet.GraphicsCoordinatorID) Then
            Return True
        End If

        Return False
    End Function

    Public Function CanAccessAuditTrail(ByVal userId As Integer) As Boolean
        If UserHasGlobalRole(userId, AppSettingsGet.AdminRoleID) OrElse _
        UserHasProjectRole(userId, AppSettingsGet.GraphicsCoordinatorID) Then
            Return True
        End If

        Return False
    End Function

    Public Function CanAccessDocuments(ByVal userId As Integer, ByVal projectId As Integer) As Boolean
        Dim excludeRoles As New List(Of Integer)(New Integer() {AppSettingsGet.BrandManagerID, AppSettingsGet.StudioManagerID})

        If UserManager.DoesUserHaveAGlobalRole(userId, excludeRoles) OrElse _
            UserManager.UserHasProjectRole(userId, AppSettingsGet.PORaiserID, projectId) Then
            Return True
        End If

        Return False

    End Function

    Public Function CanAccessProject(ByVal userId As Integer, ByVal projectId As Integer) As Boolean

        If UserManager.DoesUserHaveAGlobalRole(userId) = True Then
            ' If a user has any global roles then they can see any project
            Return True
        End If

        If UserManager.UserHasRoleInProject(userId, projectId) Then
            ' If they are a part of the project then they can see the project
            Return True
        End If

        Return False

    End Function

    Public Function CanAccessCostings(ByVal userId As Integer, ByVal projectId As Integer) As Boolean

        ' First do a global check

        Dim allowedToAccessCostings As New List(Of Integer)(New Integer() {AppSettingsGet.BDProjectManagerID, AppSettingsGet.BDAccountManagerID, _
                                                                           AppSettingsGet.BrandManagerID, AppSettingsGet.MDAProcurementID, _
                                                                           AppSettingsGet.MDAKittingID, AppSettingsGet.MDAProjectManagerID,
                                                                           AppSettingsGet.AdminRoleID})

        modLogManager.Debug(String.Format("There are {0}", allowedToAccessCostings.Count))

        modLogManager.Debug(String.Format("BDProjectManagerID is {0}", AppSettingsGet.BDProjectManagerID))

        For Each item As Integer In allowedToAccessCostings
            modLogManager.Debug(String.Format("ID is {0}", item))
        Next

        If DoesUserHaveFilteredGlobalRole(userId, allowedToAccessCostings) Then
            Return True
        End If

        ' Now do a project level check
        If UserManager.UserHasProjectRole(userId, AppSettingsGet.OwnerRoleID, projectId) Then
            Return True
        End If

        Return False

    End Function

    Public Function CanAnnotateDocuments(ByVal userId As Integer, ByVal projectId As Integer) As Boolean

        Dim globalRolesAllowedToAccessDocuments As New List(Of Integer)(New Integer() {AppSettingsGet.BDProjectManagerID, AppSettingsGet.BDAccountManagerID, _
                                                                            AppSettingsGet.BrandListDefinitionID, AppSettingsGet.DesignerID, _
                                                                            AppSettingsGet.ArtworkerID, AppSettingsGet.StudioManagerID, _
                                                                            AppSettingsGet.AdminRoleID})

        If DoesUserHaveFilteredGlobalRole(userId, globalRolesAllowedToAccessDocuments) Then
            Return True
        End If

        Dim projectRolesAllowedToAccessDocuments As New List(Of Integer)(New Integer() {AppSettingsGet.GraphicsCoordinatorID, AppSettingsGet.OwnerRoleID, _
                                                                                        AppSettingsGet.LegalApproverID})

        If UserHasFilteredProjectRole(userId, projectId, projectRolesAllowedToAccessDocuments) Then
            Return True
        End If

        Return False

    End Function

    Public Function CanAccessSchedule(ByVal userId As Integer) As Boolean

        If UserManager.UserHasRoleInProject(userId, AppSettingsGet.GraphicsCoordinatorID) Then
            Return True
        End If

        Return False

    End Function

    Public Function CanUserAccessTimesheets(ByVal userId As Integer) As Boolean
        If UserManager.UserHasGlobalRole(userId, AppSettingsGet.DesignerID) OrElse _
           UserManager.UserHasGlobalRole(userId, AppSettingsGet.ArtworkerID) Then
            Return True
        End If

        Return False
    End Function

    Public Function CanAddEditBBCItems(ByVal userId As Integer) As Boolean

        Dim rolesAllowedToAlterBBCItems As New List(Of Integer)(New Integer() {AppSettingsGet.BDProjectManagerID, AppSettingsGet.BDAccountManagerID, _
                                                                               AppSettingsGet.BrandManagerID, AppSettingsGet.MDAProcurementID, _
                                                                               AppSettingsGet.MDAProjectManagerID, AppSettingsGet.AdminRoleID})

        Return UserManager.DoesUserHaveFilteredGlobalRole(userId, rolesAllowedToAlterBBCItems)
    End Function

    Public Function CanAlterKittingBrief(ByVal userId As Integer)

        Dim rolesAllowedToAlterKittingBrief As New List(Of Integer)(New Integer() {AppSettingsGet.BDProjectManagerID, AppSettingsGet.BDAccountManagerID, _
                                                                               AppSettingsGet.BrandManagerID, AppSettingsGet.MDAKittingID, _
                                                                               AppSettingsGet.MDAProjectManagerID, AppSettingsGet.AdminRoleID})

        Return UserManager.DoesUserHaveFilteredGlobalRole(userId, rolesAllowedToAlterKittingBrief)
    End Function

    Public Function CanAddEditPremiumElements(ByVal userId As Integer)
        Dim rolesAllowedToAlterPremElements As New List(Of Integer)(New Integer() {AppSettingsGet.BDProjectManagerID, AppSettingsGet.BDAccountManagerID, _
                                                                            AppSettingsGet.BrandManagerID, AppSettingsGet.MDAKittingID, _
                                                                            AppSettingsGet.MDAProjectManagerID, AppSettingsGet.AdminRoleID})

        Return UserManager.DoesUserHaveFilteredGlobalRole(userId, rolesAllowedToAlterPremElements)
    End Function

    Public Function CanAlterPremiumBrief(ByVal userId As Integer) As Boolean
        Dim rolesAllowedToAlterPremiumBrief As New List(Of Integer)(New Integer() {AppSettingsGet.BDProjectManagerID, AppSettingsGet.BDAccountManagerID, _
                                                                           AppSettingsGet.BrandManagerID, AppSettingsGet.MDAKittingID, _
                                                                           AppSettingsGet.MDAProjectManagerID, AppSettingsGet.AdminRoleID})

        Return UserManager.DoesUserHaveFilteredGlobalRole(userId, rolesAllowedToAlterPremiumBrief)
    End Function

#End Region

#Region "Element Permissions"

    Public Function CanUserSaveElement(ByVal userId As Integer, ByVal projectId As Integer) As Boolean

        If UserManager.UserHasGlobalRole(userId, AppSettingsGet.AdminRoleID) OrElse _
           UserManager.UserHasGlobalRole(userId, AppSettingsGet.WilliamsLeaProjectManagerID) OrElse _
           UserManager.UserHasGlobalRole(userId, AppSettingsGet.WilliamsLeaAccountManagerID) OrElse _
           UserManager.UserHasGlobalRole(userId, AppSettingsGet.WilliamsLeaGlobalProjectManagerID) OrElse _
           UserManager.UserHasProjectRole(userId, AppSettingsGet.GraphicsCoordinatorID) Then
            Return True
        End If

        If WorkflowManager.IsTransitionComplete(AppSettingsGet.ElementWorkflowFinishTransitionID, projectId) Then
            Return False
        End If

        Return True

    End Function

    Public Function CanUserAlterKitting(ByVal userId As Integer, ByVal projectId As Integer) As Boolean
        ' If the user isn't the BD Project Manager
        If UserManager.UserHasGlobalRole(userId, AppSettingsGet.BDProjectManagerID) = False Then
            Return False
        End If

        Dim currentTokens As List(Of Token) = WorkflowManager.GetTokensByUserAndStatus(userId, AppSettingsGet.TokenStatusConsumed, projectId)

        ' If the project has passed the finalise brief task
        Dim counterList As List(Of Token) = (From t In currentTokens
                                             Where WorkflowManager.GetTransitionByToken(t).ID = AppSettingsGet.FinaliseAPTBriefTransitionID
                                             Select t).ToList

        ' Any tokens consumed at the finalised stage? Yes? No editing for you!
        If counterList.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

End Module
