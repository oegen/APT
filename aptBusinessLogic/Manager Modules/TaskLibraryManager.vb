'----------------------------------------------------------------------------------------------
' Filename    : TaskLibraryManager.vb
' Description : Task library manager will deal with any unique tasks that may been that bit 
'               of extra functionality. It will do things such as get the tasks unique url
'               so that specific task page can be displayed. It will also complete a post
'               completion check, which will be called after each task is complete which will 
'               subsequently do things such as send emails and complete and other post task 
'               activities.
'
' Release Initials  Date        Comment
' 1       LP        29/06/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module TaskLibraryManager

    Public Function GetByTask(ByVal taskId As Integer) As TaskLibraryItem
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.TaskLibraryItemDAO(context).GetByTask(taskId)
    End Function

#Region "Can Tasks be Completed?"

    Public Function CanTaskBeCompleted(ByVal tokenId As Integer, ByRef errorMessage As String) As Boolean

        Dim currentToken As Token = WorkflowManager.GetTokenByID(tokenId)
        Dim transitionOfToken As Transition = WorkflowManager.GetTransitionByToken(currentToken)

        Select Case transitionOfToken.ID

            Case AppSettingsGet.FinaliseAPTBriefTransitionID, AppSettingsGet.FinaliseAPTBriefTransitionNonBDID
                Return CanBriefBeFinalised(currentToken.ID, errorMessage)

            Case AppSettingsGet.KittingFinalTransitionID
                Return CanKitBeFinalised(currentToken.ID, errorMessage)

        End Select

        Return True

    End Function

    Private Function CanBriefBeFinalised(ByVal tokenId As Integer, ByRef errorMessage As String) As Boolean

        Dim currentToken As Token = WorkflowManager.GetTokenByID(tokenId)

        errorMessage = "The project must have elements before the brief can be finalised."

        ' If there are no elements this will return false, otherwise true
        Return ElementManager.GetElementsByProject(currentToken.Project.ID).Count > 0

    End Function

    Private Function CanKitBeFinalised(ByVal tokenId As Integer, ByVal errorMessage As String) As Boolean

        Dim currentToken As Token = WorkflowManager.GetTokenByID(tokenId)

        errorMessage = "The project must have elements before the brief can be finalised."

        If KitManager.GetKitsByProject(currentToken.Project.ID).Count > 0 AndAlso _
            ProjectManager.GetProjectKittingBriefByProject(currentToken.Project.ID) IsNot Nothing Then

            Return True

        End If

        Return False

    End Function

#End Region

#Region "Post Completion Tasks"

    Public Sub CompletePostCompleteProcess(ByVal currentToken As Token, Optional ByVal preCondition As String = "")
        Dim transitionOfToken As Transition = WorkflowManager.GetTransitionByToken(currentToken)
        Dim taskCompleted As Task = transitionOfToken.Task
        Dim tokenProject As Project = currentToken.Project

        Select Case taskCompleted.ID

            Case AppSettingsGet.IsPrintProjectTransitionID
                ' If the print was a yes then set print required
                IIf(preCondition = "a", tokenProject.PrintRequired = True, tokenProject.PrintRequired = False)

            Case AppSettingsGet.BriefSignOffTransitionID, AppSettingsGet.BriefSignOffNonBDTransitionID
                If preCondition = "r" Then
                    ' Has been rejected so email the project owner (rejection emails have not been implemented - this is specifically for this transition)
                    Dim projectOwner As AptUser = ProjectManager.GetProjectOwner(currentToken.Project.ID)

                    AptSendEmail(AppSettingsGet.SenderAddress, projectOwner.EmailAddress,
                                 currentToken.Comment,
                                 String.Format("{0} - The Brief has been rejected", currentToken.Project.AINName),
                                 AppSettingsGet.SmptServer, , , , )

                    'Oegen.Email.SendEmail(AppSettingsGet.SenderAddress, projectOwner.EmailAddress, String.Format("{0} - The Brief has been rejected", currentToken.Project.AINName), currentToken.Comment, , , , , , , AppSettingsGet.SmptServer)
                End If

        End Select
    End Sub

    ' Check the next task, if there is any work to do on these tasks then do that.
    ' E.g. Any BD related projects may need to skip a certain task.
    Public Sub NewTokenGenerated(ByVal tokenId As Integer)
        Dim currentToken As Token = WorkflowManager.GetTokenByID(tokenId)
        Dim transitionOfToken As Transition = WorkflowManager.GetTransitionByToken(currentToken)

        If transitionOfToken IsNot Nothing Then
            Dim taskCompleted As Task = transitionOfToken.Task
            Dim tokenProject As Project = currentToken.Project
            Dim canSendEmail As Boolean = True

            Select Case taskCompleted.ID

                'Case AppSettingsGet.StudioQATaskId
                Case AppSettingsGet.BDApprovalTaskId, AppSettingsGet.SubmitToBDTaskId
                    ' We need to call complete if the project associated with the token is not a bd project, 
                    ' we(don) 't require  completion of the bd approval, so just skip the step by completing it automatically
                    If tokenProject.IsBDProject = False Then
                        WorkflowManager.CompleteProcess(tokenId, AppSettingsGet.AcceptedPreCondition)
                        canSendEmail = False
                    End If

                Case AppSettingsGet.PreElementStartTransitionID
                    ' Are we ready to rock and roll with this?
                    ' Are there enough tokens in place to state whether we go ahead with the element workflow?
                    If WorkflowManager.AptSpecificRuleMatch(currentToken, _
                                                            WorkflowManager.GetFreeTokensAtPlaceInProject(currentToken.Project.ID, currentToken.Place.ID).Count) Then

                        WorkflowManager.CompleteProcess(tokenId)
                    End If

            End Select

            If canSendEmail Then
                SendResponsibleEmail(tokenId)
            End If

        End If
    End Sub

    Public Sub SendResponsibleEmail(ByVal tokenId As Integer)
        ' Firstly, check if the token is ready to rock and roll (isn't awaiting other tokens at the place)
        Dim currentToken As Token = WorkflowManager.GetTokenByID(tokenId)

        If WorkflowManager.CheckPlaceHasAllTokens(currentToken.Place, currentToken.Project.ID) OrElse
            IsStartPlace(currentToken.Place.ID) Then
            Dim transitionAssociated As Transition = WorkflowManager.GetTransitionByToken(currentToken)

            If transitionAssociated.EnableEmail Then

                If MatchTaskSpecificEmailRule(currentToken, transitionAssociated.ID) = False Then
                    ' Only email if a rule has not been matched
                    GetAndEmailUsers(currentToken, transitionAssociated.ID)
                End If

            End If

        End If
    End Sub

    Private Sub GetAndEmailUsers(ByVal currentToken As Token, ByVal associatedTransitionId As Integer)

        Dim nonProjectRoles As List(Of Role) = WorkflowManager.GetRolesByTransition(associatedTransitionId, AppSettingsGet.SecurityLookupRoleID)
        Dim projectRoles As List(Of Role) = WorkflowManager.GetRolesByTransition(associatedTransitionId, AppSettingsGet.SecurityLookupProjectID)
        Dim usersToEmail As New List(Of AptUser)

        ' Get users in non-project role
        For Each role As Role In nonProjectRoles
            usersToEmail.AddRange(UserManager.GetUsersInGlobalRole(role.ID))
        Next

        ' Get users in project role
        For Each role As Role In projectRoles
            usersToEmail.Add(UserManager.GetUserInProjectRole(currentToken.Project.ID, role.ID))
        Next

        ' Send all user emails off
        For Each userToEmail As AptUser In usersToEmail
            EmailSender.SendTaskToCompleteEmail(userToEmail, currentToken.ID)
        Next

    End Sub

#Region "Task Specific Functionality"

    Private Sub CompletePostPrintTaskCompletion(ByVal tokenId As Integer, ByVal preCondition As String)



    End Sub

    Private Function MatchTaskSpecificEmailRule(ByVal currentToken As Token, ByVal transitionId As Integer) As Boolean

        ' This function is a hack to override any email functionality 
        ' this has been created specifically for:
        ' - step 17 (W/Lea Budget Proposals)
        ' - step G (Notify W/Lea)

        Select Case transitionId

            Case AppSettingsGet.NotifyWLeaTransitionID, AppSettingsGet.WLeaBudgetProposal
                ' At these tasks we email to everyone at MDA
                EmailSender.SendTaskToCompleteEmail(Nothing, currentToken.ID, AppSettingsGet.WLeaGroupEmailAddress)
                Return True
            Case AppSettingsGet.AinNoRaiseBD
                EmailSender.SendTaskToCompleteEmail(Nothing, currentToken.ID, AppSettingsGet.DefaultAinRaisedEmail)
                Return True
        End Select

        Return False

    End Function

#End Region

#End Region

End Module
