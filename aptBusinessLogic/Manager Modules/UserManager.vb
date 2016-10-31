'----------------------------------------------------------------------------------------------
' Filename    : UserManager.vb
' Description : All user data access layer calls and user logic will be found within the user 
'               manager module.
'
' Release Initials  Date        Comment
' 1       LP/TL     27/05/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL
Imports GenericUtilities

Public Module UserManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

    Private ReadOnly Property ProjectRoles As List(Of Integer)
        Get

            Dim allProjectRoles As New List(Of Integer)(New Integer() {
                AppSettingsGet.OwnerRoleID, _
                AppSettingsGet.LegalApproverID, _
                AppSettingsGet.PORaiserID, _
                AppSettingsGet.StudioQAID, _
                AppSettingsGet.GraphicsCoordinatorID, _
                AppSettingsGet.BrandManagerID,
                AppSettingsGet.WilliamsLeaProjectManagerID})

            Return allProjectRoles

        End Get
    End Property


#Region "Get Variations"

    Public Function SearchUsers(Optional ByVal searchStr As String = "", Optional ByVal active As Boolean = True) As List(Of AptUser)
        Dim allUsers As List(Of AptUser) = DAOGetter.AptUserDAO(context).GetAll()

        Return (From u In allUsers
                Where (u.Surname.ToLowerInvariant().StartsWith(searchStr.ToLowerInvariant()) _
                      Or u.FullName.ToLowerInvariant().StartsWith(searchStr.ToLowerInvariant())) _
                      And u.Active = active
                Select u).ToList
    End Function

    Public Function GetUser(ByVal userId As Integer) As AptUser
        Return DAOGetter.AptUserDAO(context).GetByID(userId)
    End Function

    Public Function GetUserByUsername(ByVal username As String) As AptUser
        Return DAOGetter.AptUserDAO(context).GetUserByUsername(username)
    End Function

    Public Function SearchUserByUserName(ByVal username As String) As List(Of AptUser)
        Return DAOGetter.UserLoginsDAO(Context).SearchByUsername(username)
    End Function

    Public Function SearchUserBySurname(ByVal surname As String) As List(Of AptUser)
        Return DAOGetter.AptUserDAO(Context).SearchBySurname(surname)
    End Function

    Public Function GetUserByLDAP() As AptUser
        Dim ldapUser As User = GetCurrentLDAPUser()

        Return GetUserByUsername(ldapUser.Username)
    End Function

    Public Function GetLoginForUser(ByVal userId As Integer) As AptLogin
        Return DAOGetter.UserLoginsDAO(context).GetLoginByUser(userId)
    End Function

    Public Function GetAllUsers() As List(Of AptUser)
        Return DAOGetter.AptUserDAO(Context).GetAll()
    End Function

    Public Function GetUserLoginByUsername(ByVal username As String) As AptLogin
        Return DAOGetter.UserLoginsDAO(Context).GetLoginByUsername(username)
    End Function

    Public Function GetDefaultStudioQAUsers() As List(Of AptUser)

        Dim defaultUserId As List(Of String) = AppSettingsGet.DefaultStudioQAUserID.Split(",").ToList()
        Dim defaultStudioQAUsers As New List(Of AptUser)

        For Each userId As Integer In defaultUserId
            Dim tmpUser As AptUser = UserManager.GetUser(userId)

            If tmpUser IsNot Nothing Then
                defaultStudioQAUsers.Add(tmpUser)
            End If
        Next

        Return defaultStudioQAUsers

    End Function

    Public Function GetDefaultPORaiserUsers() As List(Of AptUser)

        Dim defaultUserId As List(Of String) = AppSettingsGet.DefaultPORaisersUserID.Split(",").ToList()
        Dim defaultPORaiserUsers As New List(Of AptUser)

        For Each userId As Integer In defaultUserId
            Dim tmpUser As AptUser = UserManager.GetUser(userId)

            If tmpUser IsNot Nothing Then
                defaultPORaiserUsers.Add(tmpUser)
            End If
        Next

        Return defaultPORaiserUsers

    End Function

    Public Function GetDefaultProjectCoordinators() As List(Of AptUser)

        Dim defaultUserId As List(Of String) = AppSettingsGet.DefaultProjectCoordinatorsID.Split(",").ToList()
        Dim defaultCoordinatorUsers As New List(Of AptUser)

        For Each userId As Integer In defaultUserId
            Dim tmpUser As AptUser = UserManager.GetUser(userId)

            If tmpUser IsNot Nothing Then
                defaultCoordinatorUsers.Add(tmpUser)
            End If
        Next

        Return defaultCoordinatorUsers

    End Function

    Public Function GetDefaultWLeaProjManager() As List(Of AptUser)

        Dim defaultUserId As List(Of String) = AppSettingsGet.DefaultWLeaProjectManager.Split(",").ToList()
        Dim defaultWleaProjectManagers As New List(Of AptUser)

        For Each userId As Integer In defaultUserId
            Dim tmpUser As AptUser = UserManager.GetUser(userId)

            If tmpUser IsNot Nothing Then
                defaultWleaProjectManagers.Add(tmpUser)
            End If
        Next

        Return defaultWleaProjectManagers

    End Function

    Public Function GetDefaultMDAUsers() As List(Of AptUser)

        Dim defaultUserId As List(Of String) = AppSettingsGet.DefaultWLeaProjectManager.Split(",").ToList()
        Dim defaultMDAManagers As New List(Of AptUser)

        For Each userId As Integer In defaultUserId
            Dim tmpUser As AptUser = UserManager.GetUser(userId)

            If tmpUser IsNot Nothing Then
                defaultMDAManagers.Add(tmpUser)
            End If
        Next

        Return defaultMDAManagers

    End Function

#End Region

#Region "Insert / Update / Remove"

#Region "User"

    Public Sub AddNewUser(ByVal user As AptUser, ByVal username As String, _
                          Optional ByVal generateLogin As Boolean = True, Optional ByVal sendPasswordEmail As Boolean = True, _
                          Optional ByVal active As Boolean = True)

        modLogManager.Debug("AddNewUser - Start")

        If GetUserByUsername(username) IsNot Nothing Then
            Throw New UserAlreadyExistsException(username)
        End If

        user.Active = active

        DAOGetter.AptUserDAO(Context).Insert(user)

        modLogManager.Debug(String.Format("AddNewUser - New user inserted in to the database, do we need to create a login? {0}", generateLogin))

        ' If the developer requests a login to be generated, do so!
        If generateLogin = True Then
            ' Create a login for the new user
            Dim newLogin As New AptLogin()

            newLogin.Created = DateTime.Now
            newLogin.Modified = DateTime.Now
            newLogin.User = user
            newLogin.Username = username
            newLogin.Password = modAccount.GenerateRandomPassword(AppSettingsGet.PasswordCharacters, AppSettingsGet.PasswordNumerics, AppSettingsGet.PasswordLength)

            DAOGetter.UserLoginsDAO(Context).Insert(newLogin)
            modLogManager.Debug(String.Format("AddNewUser - Login inserted, do we need to insert password? {0}", sendPasswordEmail))
        End If

        ' Send e-mail if requested to do so
        If sendPasswordEmail = True Then
            EmailSender.SendRegistrationEmail(user)
            modLogManager.Debug("AddNewUser - Password sent")
        End If

        modLogManager.Debug("AddNewUser - Complete")

    End Sub

    Public Sub UpdateUser(ByVal user As AptUser)
        modLogManager.Debug("UpdateUser - Start")

        user.Modified = DateTime.Now.ToString("dd/MM/yyyy HH:ss")

        DAOGetter.AptUserDAO(Context).Update(user)

        modLogManager.Debug("UpdateUser - Complete")
    End Sub

    Public Sub DisableUser(ByVal userId As Integer)
        modLogManager.Debug("DisableUser - Start")

        Dim user As AptUser = DAOGetter.AptUserDAO(Context).GetByID(userId)

        user.Active = False
        user.Modified = DateTime.Now.ToString("dd/MM/yyyy HH:ss")

        DAOGetter.AptUserDAO(Context).Update(user)

        modLogManager.Debug("DisableUser - Complete")
    End Sub

    Public Sub EnableUser(ByVal userId As Integer)
        modLogManager.Debug("EnableUser - Start")

        Dim user As AptUser = DAOGetter.AptUserDAO(Context).GetByID(userId)

        user.Active = True
        user.Modified = DateTime.Now.ToString("dd/MM/yyyy HH:ss")

        DAOGetter.AptUserDAO(Context).Update(user)

        modLogManager.Debug("EnableUser - Complete")
    End Sub

#End Region

#Region "UserRoles"

    Public Sub AddUserRole(ByVal saveUserRole As UserRole)

        modLogManager.Debug(String.Format("AddUserRole - Start : User - {0} : Role - {1}", saveUserRole.User.FullName, saveUserRole.Role.Description))

        saveUserRole.Modified = DateTime.Now

        If saveUserRole.ID = 0 Then
            saveUserRole.Created = DateTime.Now
            DAOGetter.UserRoleDAO(Context).Insert(saveUserRole)
        Else
            DAOGetter.UserRoleDAO(Context).Update(saveUserRole)
        End If

        modLogManager.Debug("AddUserRole - Complete")

    End Sub

    Public Sub RemoveRoleFromUser(ByVal deleteUserRole As UserRole)
        modLogManager.Debug(String.Format("RemoveRoleFromUser - Start : User - {0} : Role - {1}", deleteUserRole.User.FullName, deleteUserRole.Role.Description))

        DAOGetter.UserRoleDAO(Context).Delete(deleteUserRole)

        modLogManager.Debug("RemoveRoleFromUser - Complete")
    End Sub

    Public Function GetUserRole(ByVal id As Integer) As UserRole
        Return DAOGetter.UserRoleDAO(Context).GetByID(id)
    End Function

    Public Function GetUserRoleByUser(ByVal userId As Integer) As List(Of UserRole)
        Return DAOGetter.UserRoleDAO(Context).GetAllByUser(userId)
    End Function

    Public Sub SwitchProjectRoleUser(ByVal roleToChangeId As Integer, ByVal oldUserId As Integer, ByVal newUserId As Integer)

        ' Switches a user in a particular role to another user for ALL projects

        Dim oldUserRoles As List(Of ProjectRoleAssociation) = DAOGetter.ProjectRoleAssociationDAO(Context).GetByUserAndRole(oldUserId, roleToChangeId)
        Dim newUser As AptUser = UserManager.GetUser(newUserId)

        For Each oldRoleAssociation As ProjectRoleAssociation In oldUserRoles
            oldRoleAssociation.User = newUser
            SaveProjectRoleAssociation(oldRoleAssociation)
        Next

    End Sub

#End Region

#Region "ProjectRoleAssociation"

    Private Sub SaveProjectRoleAssociation(ByRef saveAssociation As ProjectRoleAssociation)

        If saveAssociation.ID = 0 Then
            DAOGetter.ProjectRoleAssociationDAO(Context).Insert(saveAssociation)
        Else
            DAOGetter.ProjectRoleAssociationDAO(Context).Update(saveAssociation)
        End If

    End Sub

#End Region

#End Region

#Region "LDAP Queries"

    Public Function SearchLDAPUsers(ByVal searchStr As String)
        Return LDAPCalls.SearchUsers(searchStr)
    End Function

    Public Function GetCurrentLDAPUser() As User
        Return LDAPCalls.GetUser(LDAPCalls.GetServerVariable)
    End Function

#End Region

#Region "User Access / Permissions"

#Region "Global Roles"

    Public Function GetRoleByID(ByVal roleId As Integer) As Role
        Return DAOGetter.RoleDAO(Context).GetByID(roleId)
    End Function

    Public Function GetUsersGlobalRoles(ByVal userId As Integer) As List(Of Role)
        Return (From ur In DAOGetter.UserRoleDAO(Context).GetAllByUser(userId)
                Where GetAllGlobalRoles.Contains(ur.Role)
                Select ur.Role).ToList
    End Function

    Public Function GetGlobalRolesNotAssignedToUser(ByVal userId As Integer) As List(Of Role)

        Dim usersRoles As List(Of Role) = GetUsersGlobalRoles(userId)
        Dim allGlobalRoles As List(Of Role) = GetAllGlobalRoles()
        Dim returnedRoles As New List(Of Role)

        For Each tmpRole In allGlobalRoles
            If usersRoles.Contains(tmpRole) = False Then
                returnedRoles.Add(tmpRole)
            End If
        Next

        Return returnedRoles

    End Function

    Public Function GetUsersInGlobalRole(ByVal roleId As Integer) As List(Of AptUser)

        Return (From ur In DAOGetter.UserRoleDAO(Context).GetByRole(roleId)
                Select ur.User).ToList

    End Function

    Public Function GetAllGlobalRoles() As List(Of Role)

        'Dim globalRoles() As Integer = {AppSettingsGet.OwnerRoleID, _
        '                                AppSettingsGet.LegalApproverID, _
        '                                AppSettingsGet.PORaiserID, _
        '                                AppSettingsGet.StudioQAID, _
        '                                AppSettingsGet.GraphicsCoordinatorID}

        'Return (From r In DAOGetter.RoleDAO(Context).GetAll()
        '        Where r.ID <> AppSettingsGet.OwnerRoleID _
        '           Or r.ID <> AppSettingsGet.LegalApproverID _
        '           Or r.ID <> AppSettingsGet.PORaiserID _
        '           Or r.ID <> AppSettingsGet.StudioQAID _
        '           Or r.ID <> AppSettingsGet.GraphicsCoordinatorID
        '        Select r).ToList

        Return (From r In DAOGetter.RoleDAO(Context).GetAll()
                Where ProjectRoles.Contains(r.ID) = False
                Select r).ToList

    End Function

    Public Sub GiveUserGlobalAccess(ByVal userId As Integer, ByVal roleId As Integer)
        If UserHasGlobalRole(userId, roleId) = False Then
            Dim userRole As New UserRole

            modLogManager.Debug(String.Format("GiveUserGlobalAccess - Start : User - {0} : Role - {1}", userRole.User.FullName, userRole.Role.Description))

            userRole.User = DAOGetter.AptUserDAO(Context).GetByID(userId)
            userRole.Role = DAOGetter.RoleDAO(Context).GetByID(roleId)
            userRole.Created = Date.Now.ToString("dd/MM/yyyy")
            userRole.Modified = Date.Now.ToString("dd/MM/yyyy")

            DAOGetter.UserRoleDAO(Context).Insert(userRole)

            modLogManager.Debug("GiveUserGlobalAccess - Complete")
        End If
    End Sub

    Public Sub RevokeUserGlobalRole(ByVal userId As Integer, ByVal roleId As Integer)
        If UserHasGlobalRole(userId, roleId) Then
            Dim userRole As UserRole = DAOGetter.UserRoleDAO(Context).GetByUserAndRole(userId, roleId)

            modLogManager.Debug(String.Format("RevokeUserGlobalRole - Start : User - {0} : Role - {1}", userRole.User.FullName, userRole.Role.Description))

            DAOGetter.UserRoleDAO(Context).Delete(userRole)

            modLogManager.Debug("GiveUserGlobalAccess - Complete")
        End If
    End Sub

    Public Function UserIsAdmin(ByVal userId As Integer) As Boolean
        Return UserHasGlobalRole(userId, AppSettingsGet.AdminRoleID)
    End Function

    Public Function DoesUserHaveAGlobalRole(ByVal userId As Integer, Optional ByVal excludeRoles As List(Of Integer) = Nothing) As Boolean
        Dim userHasARole As Boolean = False
        Dim rolesToExclude = New List(Of Integer)
        Dim rolesToCheck = New List(Of Integer)

        If excludeRoles IsNot Nothing Then
            rolesToExclude = excludeRoles
        End If

        If rolesToExclude.Contains(AppSettingsGet.ArtworkerID) = False Then
            'userHasARole = userHasARole OrElse UserHasGlobalRole(userId, AppSettingsGet.ArtworkerID)
            rolesToCheck.Add(AppSettingsGet.ArtworkerID)
        End If

        If rolesToExclude.Contains(AppSettingsGet.AdminRoleID) = False Then
            rolesToCheck.Add(AppSettingsGet.AdminRoleID)
        End If

        If rolesToExclude.Contains(AppSettingsGet.PrinterID) = False Then
            rolesToCheck.Add(AppSettingsGet.PrinterID)
        End If

        If rolesToExclude.Contains(AppSettingsGet.DesignerID) = False Then
            rolesToCheck.Add(AppSettingsGet.DesignerID)
        End If

        If rolesToExclude.Contains(AppSettingsGet.BDProjectManagerID) = False Then
            rolesToCheck.Add(AppSettingsGet.BDProjectManagerID)
        End If

        If rolesToExclude.Contains(AppSettingsGet.BDAccountManagerID) = False Then
            rolesToCheck.Add(AppSettingsGet.BDAccountManagerID)
        End If

        'If rolesToExclude.Contains(AppSettingsGet.BrandManagerID) = False Then
        '    userHasARole = userHasARole OrElse UserHasGlobalRole(userId, AppSettingsGet.BrandManagerID)
        'End If

        If rolesToExclude.Contains(AppSettingsGet.StudioManagerID) = False Then
            rolesToCheck.Add(AppSettingsGet.StudioManagerID)
        End If

        ' This is a project role
        'If rolesToExclude.Contains(AppSettingsGet.LegalApproverID) = False Then
        '    userHasARole = userHasARole OrElse UserHasGlobalRole(userId, AppSettingsGet.LegalApproverID)
        'End If

        'If rolesToExclude.Contains(AppSettingsGet.WilliamsLeaProjectManagerID) = False Then
        '    userHasARole = userHasARole OrElse UserHasGlobalRole(userId, AppSettingsGet.WilliamsLeaProjectManagerID)
        'End If

        If rolesToExclude.Contains(AppSettingsGet.WilliamsLeaAccountManagerID) = False Then
            rolesToCheck.Add(AppSettingsGet.WilliamsLeaAccountManagerID)
        End If

        If rolesToExclude.Contains(AppSettingsGet.MDAProjectManagerID) = False Then
            rolesToCheck.Add(AppSettingsGet.MDAProjectManagerID)
        End If

        If rolesToExclude.Contains(AppSettingsGet.MDAKittingID) = False Then
            rolesToCheck.Add(AppSettingsGet.MDAKittingID)
        End If

        If rolesToExclude.Contains(AppSettingsGet.MDAProjectManagerID) = False Then
            rolesToCheck.Add(AppSettingsGet.MDAProjectManagerID)
        End If


        If rolesToExclude.Contains(AppSettingsGet.MDAOnSiteID) = False Then
            userHasARole = userHasARole OrElse UserHasGlobalRole(userId, AppSettingsGet.MDAOnSiteID)
            rolesToCheck.Add(AppSettingsGet.MDAOnSiteID)
        End If

        Dim userRoles As List(Of Integer) = DAOGetter.UserRoleDAO(Context) _
                                                     .GetAllByUser(userId) _
                                                     .Where(Function(x) x.Role IsNot Nothing) _
                                                     .Select(Function(x) x.Role.ID) _
                                                     .ToList()

        If rolesToCheck.Intersect(userRoles).Count() > 0 Then
            userHasARole = True
        End If

        Return userHasARole

    End Function

    Public Function UserHasGlobalRole(ByVal userId As Integer, ByVal roleId As Integer) As Boolean
        Dim hasAccess As Boolean = False
        Dim userWithRole As AptUser = DAOGetter.AptUserDAO(Context).UserHasAccess(userId, roleId)

        If userWithRole IsNot Nothing Then
            hasAccess = True
        End If

        Return hasAccess
    End Function

    Function DoesUserHaveFilteredGlobalRole(ByVal userId As Integer, ByVal globalRoles As List(Of Integer)) As Boolean

        Dim userGlobalRoles As List(Of Role) = UserManager.GetUsersGlobalRoles(userId)
        ' userGlobalRoles = New List(Of Role)

        For Each tmpRole As Role In userGlobalRoles
            modLogManager.Debug(String.Format("The user role is {0} and the ID is {1}", tmpRole.Title, tmpRole.ID))
        Next

        If (From ur In userGlobalRoles
            Where globalRoles.Contains(ur.ID)
            Select ur).Count > 0 Then
            ' A user has a role that allows them to access costings
            Return True
        End If

        Return False

    End Function

#End Region

#Region "Project Roles"

    Public Function GetAllProjectRoles() As List(Of Role)

        Dim returnProjectRoles As New List(Of Role)

        For Each roleId As Integer In ProjectRoles

            Dim tmpRole As Role = UserManager.GetRoleByID(roleId)
            If tmpRole IsNot Nothing Then
                returnProjectRoles.Add(tmpRole)
            End If
        Next

        Return returnProjectRoles

    End Function

    Public Sub GiveUserProjectRole(ByVal projectId As Integer, ByVal userId As Integer, ByVal roleId As Integer)

        Dim projectsAssociations As List(Of ProjectRoleAssociation) = DAOGetter.ProjectRoleAssociationDAO(Context).GetByProject(projectId)

        Dim usersRole As ProjectRoleAssociation = (From pa In projectsAssociations
                                                   Where pa.User.ID = userId And pa.Role.ID = roleId
                                                   Select pa).SingleOrDefault


        If usersRole Is Nothing Then
            Dim newRole As New ProjectRoleAssociation()

            newRole.Project = ProjectManager.GetProject(projectId)
            newRole.Role = DAOGetter.RoleDAO(Context).GetByID(roleId)
            newRole.User = UserManager.GetUser(userId)

            DAOGetter.ProjectRoleAssociationDAO(Context).Insert(newRole)
        End If
    End Sub

    'Public Function UserHasProjectAccess(ByVal userId As Integer, ByVal projectId As Integer) As Boolean
    '    ' Checks to see if a user is on a project if they aren't then they can't see it
    '    ' There are however some exceptions, the following roles can see ALL projects no matter what 
    '    ' Admin

    '    If UserManager.UserHasProjectRole(userId, AppSettingsGet.AdminRoleID) Then
    '        Return True
    '    End If

    '    Dim userHasAccess As Boolean = False

    '    Dim usersProjects As List(Of Project) = ProjectManager.GetProjectsByUser(userId)

    '    If (From p In usersProjects
    '        Where p.ID = projectId
    '        Select p).Count > 0 Then

    '        userHasAccess = True
    '    End If

    '    Return userHasAccess

    'End Function

    Public Function UserHasProjectRole(ByVal userID As Integer, ByVal roleId As Integer, ByVal projectId As Integer) As Boolean
        Dim userHasRole As Boolean = False
        Dim currentUserInRole As AptUser = GetUserInProjectRole(projectId, roleId)

        If currentUserInRole IsNot Nothing AndAlso currentUserInRole.ID = userID Then
            userHasRole = True
        End If

        Return userHasRole
    End Function

    Public Function UserHasFilteredProjectRole(ByVal userID As Integer, ByVal projectId As Integer, ByVal roles As List(Of Integer)) As Boolean

        Dim projectRoles As List(Of ProjectRoleAssociation) = DAOGetter.ProjectRoleAssociationDAO(Context).GetByUserAndProject(userID, projectId)

        If (From o In projectRoles
            Where roles.Contains(o.Role.ID)
            Select o).Count > 0 Then
            Return True
        End If

        Return False

    End Function

    Public Function GetUserInProjectRole(ByVal projectId As Integer, ByVal roleId As Integer) As AptUser

        Dim projectsRoleAssocs As List(Of ProjectRoleAssociation) = DAOGetter.ProjectRoleAssociationDAO(Context).GetByProject(projectId)

        Return (From pra In projectsRoleAssocs
                Where pra.Role.ID = roleId
                Select pra.User).SingleOrDefault

    End Function

    Public Sub RevokeUserProjectRole(ByVal userId As Integer, ByVal roleId As Integer, ByVal projectId As Integer)
        Dim projectAssociations As List(Of ProjectRoleAssociation) = DAOGetter.ProjectRoleAssociationDAO(Context).GetByProject(projectId)

        Dim roleToRemove As ProjectRoleAssociation = (From pa In projectAssociations
                                                      Where pa.User.ID = userId And pa.Role.ID = roleId
                                                      Select pa).SingleOrDefault

        modLogManager.Debug(String.Format("RevokeUserProjectRole - Start : User - {0} : Role - {1}", roleToRemove.User.FullName, roleToRemove.Role.Description))

        If roleToRemove IsNot Nothing Then
            DAOGetter.ProjectRoleAssociationDAO(Context).Delete(roleToRemove)
        End If

        modLogManager.Debug("RevokeUserProjectRole - Complete")
    End Sub

    Public Sub AlterUserInProjectRole(ByVal projectId As Integer, ByVal userId As Integer, _
                                      ByVal roleId As Integer, ByVal loggedInUserId As Integer)

        Dim currentUserInRole As AptUser = GetUserInProjectRole(projectId, roleId)
        Dim newUser As AptUser = GetUser(userId)
        Dim roleObj As Role = GetRoleByID(roleId)

        ' If the user id provided isn't already present in that role
        If currentUserInRole IsNot Nothing AndAlso currentUserInRole.ID <> userId Then
            Dim currentUsersFullName As String = currentUserInRole.FullName

            ' Remove the current user
            RevokeUserProjectRole(currentUserInRole.ID, roleId, projectId)

            ' Add the new one
            GiveUserProjectRole(projectId, userId, roleId)

            ' Add audit message
            Dim auditMessage As String = String.Format("{1} has been removed as {0} and replaced by {2}", roleObj.Title, currentUsersFullName, newUser.FullName)

            AuditTrailManager.PostAudit(auditMessage, loggedInUserId, projectId, _
                                        AppSettingsGet.EditAuditChangeTypeID, AppSettingsGet.ProjectCoreDetailsAuditSectionID)
        End If

        ' If there isn't a user in the role at all add one
        If currentUserInRole Is Nothing Then
            GiveUserProjectRole(projectId, userId, roleId)

            ' Audit Message
            Dim auditMessage As String = String.Format("{0} has been added as the projects {1}", newUser.FullName, roleObj.Title)

        End If

    End Sub

    Public Function GetUserDataByRole(ByVal roleId As Integer) As List(Of ProjectRoleAssociation)
        Return DAOGetter.ProjectRoleAssociationDAO(Context).GetByRole(roleId)
    End Function

    Public Function UserHasProjectRole(ByVal userId As Integer, ByVal roleId As Integer) As Boolean

        If DAOGetter.ProjectRoleAssociationDAO(Context).GetByUserAndRole(userId, roleId).Count > 0 Then
            Return True
        End If

        Return False

    End Function

    Public Function UserHasRoleInProject(ByVal userId As Integer, ByVal projectId As Integer)

        If DAOGetter.ProjectRoleAssociationDAO(Context).GetByUserAndProject(userId, projectId).Count > 0 Then
            Return True
        End If

        Return False

    End Function

#End Region

#Region "Logins"

    Public Sub UpdateLogin(ByVal login As AptLogin)
        modLogManager.Debug(String.Format("UpdateLogin - Start : Username = {0}", login.Username))

        Dim duplicateUsernameLogin As AptLogin = GetUserLoginByUsername(login.Username)

        If duplicateUsernameLogin IsNot Nothing Then
            If duplicateUsernameLogin.ID <> login.ID Then
                Throw New UserAlreadyExistsException(login.Username)
            End If
        End If

        login.Modified = DateTime.Now.ToString("dd/MM/yyyy HH:ss")

        DAOGetter.UserLoginsDAO(Context).Update(login)

        modLogManager.Debug("UpdateLogin - Complete")
    End Sub

    Public Function VerifyUserLogin(ByVal username As String, ByVal password As String, ByRef userId As Integer, Optional ByRef errorMessage As String = "") As Boolean
        Dim validLogin As Boolean = False
        Dim user As AptUser = GetUserByUsername(username)

        If user IsNot Nothing Then
            Dim loginForUser As AptLogin = GetLoginForUser(user.ID)

            If loginForUser.Password = password Then
                userId = user.ID
                validLogin = True

                ' Set logged in date
                loginForUser.DateLastLogin = Date.Now

                UpdateLogin(loginForUser)
            Else
                errorMessage = "Incorrect password"
            End If
        Else
            errorMessage = "Username could not be found"
        End If

        Return validLogin

    End Function

    Public Function DoesUsernameExist(ByVal username As String, Optional ByVal userId As Integer = 0) As Boolean

        If userId <> 0 Then
            ' An existing login
            Dim saveLogin As AptLogin = UserManager.GetLoginForUser(userId)

            If saveLogin IsNot Nothing AndAlso username = saveLogin.Username Then
                Return False
            End If
        End If

        If DAOGetter.UserLoginsDAO(Context).GetLoginByUsername(username) IsNot Nothing Then
            Return True
        End If

        Return False

    End Function

#End Region

#End Region

End Module
