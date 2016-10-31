'----------------------------------------------------------------------------------------------
' Filename    : WorkflowManager.vb
' Description : All workflow related logic and functionality.
'
' Release Initials  Date        Comment
' 1       LP        01/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptDAL
Imports aptEntities
Imports GenericDAL
Imports GenericUtilities

Public Module WorkflowManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Process Completion Functionality"
    ''' <summary>
    ''' Completes the process of which the token is pointing towards. A check is completed to see whether all of the INs of the 
    ''' pointed to place are provided. If they are, the OUT tokens are generated and the process complete.
    ''' </summary>
    ''' <param name="tokenID">The token that is to be marked as complete</param>
    Public Sub CompleteProcess(ByVal tokenID As Integer, Optional ByVal preCondition As String = "", _
                               Optional ByVal userId As Integer = 0, Optional ByVal comment As String = "")

        modLogManager.Debug(String.Format("CompleteProcess - Start : Token ID = {0}", tokenID))
        modLogManager.Debug(String.Format("CompleteProcess - Start : precondition = {0}", preCondition))
        modLogManager.Debug(String.Format("CompleteProcess - Start : userId = {0}", userId))
        modLogManager.Debug(String.Format("CompleteProcess - Start : comment = {0}", comment))

        Dim currentToken As Token = DAOGetter.TokenDAO(Context).GetByID(tokenID)
        Dim allTokensAtPlace As List(Of Token) = GetFreeTokensAtPlaceInProject(currentToken.Project.ID, currentToken.Place.ID)
        Dim completedUser As AptUser = UserManager.GetUser(userId)

        modLogManager.Debug("CompleteProcess - Generate new tokens leading from the transition of the completed token")
        GenerateNewTokens(currentToken, preCondition)
        modLogManager.Debug("CompleteProcess - Tokens generated")

        ' All old tokens that were combined at the place of the current token
        For Each singleToken As Token In allTokensAtPlace
            'Only do this if we aren't in the element section of the workflow 
            ' OR we are in the element workflow and the element is the same
            If singleToken.ID = currentToken.ID _
                Or singleToken.ContextEntity.ID <> AppSettingsGet.EntityElementId _
                Or singleToken.ContextParentID = currentToken.ContextParentID Then

                ' Consume the Token (NOM NOM), we're now done with it.
                singleToken.TokenStatus = DAOGetter.TokenStatusDAO(Context).GetByID(AppSettingsGet.TokenStatusConsumed)
                singleToken.ConsumedDate = Date.Now.ToString("dd/MM/yyyy HH:mm:ss")
                singleToken.Comment = comment

                If completedUser IsNot Nothing Then
                    singleToken.AptUser = completedUser
                Else
                    If currentToken.AptUser IsNot Nothing Then
                        singleToken.AptUser = currentToken.AptUser
                    End If
                End If

                ' Update all this in the db, got to keep it informed
                DAOGetter.TokenDAO(Context).Update(singleToken)
            End If
        Next

        modLogManager.Debug("CompleteProcess - Call Task Library Manager Complete Process Function")
        TaskLibraryManager.CompletePostCompleteProcess(currentToken, preCondition)
        modLogManager.Debug("CompleteProcess - Complete")
    End Sub

    Private Sub GenerateNewTokens(ByVal currentToken As Token, ByVal preCondition As String)

        'If we're at the end do nothing
        If currentToken.Place.PlaceType.ID = AppSettingsGet.PlaceTypeFinish Then
            Return
        End If

        ' Get the transition that has been completed
        Dim completedTransition As Transition = DAOGetter.TransitionDAO(Context).GetTransitionFromPlace(currentToken.Place)

        ' Create new tokens for all the transitions OUTs (Places)
        Dim newPlaces As List(Of Place) = GetPlacesFromTransition(completedTransition, preCondition)

        ' Under the completion of transition 51 - the apt workflow requires breaking the petrinet rules.
        ' Instead of creating both tokens, if it's a BD project create the token for the place pointing to transition 51 (place 64)
        ' If it's a non-bd project then go to the place pointing to transition 57 (place 39)
        If completedTransition.ID = AppSettingsGet.PrintProductionTransitionID Then
            Dim placeId As Integer = AppSettingsGet.PostPrintProductionNonBDPlaceID

            If currentToken.Project.IsBDProject Then
                placeId = AppSettingsGet.PostPrintProductionBDPlaceID
            End If

            newPlaces = (From p In newPlaces
                         Where p.ID = placeId
                         Select p).ToList

            ' If we've just completed transition 40, we don't want any new tokens! (Transition 40 allows elements to be printed)
        ElseIf completedTransition.ID = AppSettingsGet.PrintGoAheadTransitionID Then
            Return ' Don't add any!
        End If

        For Each place As Place In newPlaces
            ' If the place is the start of point for the elements then don't create the singular place
            ' Create multiple element tokens
            If place.ID = AppSettingsGet.ElementPlaceStartID Then
                CreateTokenForProjectElements(place, currentToken, preCondition)
            Else
                CreateTokenForPlace(place, currentToken, preCondition)
            End If

            Console.WriteLine(String.Format("Generated Token at Place {0}", place.ID))
        Next
    End Sub

    Private Function GetPlacesFromTransition(ByVal currentTransition As Transition, ByVal preCondition As String) As List(Of Place)

        ' Get the arcs leaving the transition so we can check out the rules associated with that arc.
        Dim arcList As List(Of Arc) = DAOGetter.ArcDAO(Context).GetArcsFromTransition(currentTransition.ID)

        ' If XOR - check the preCondition (accepted or rejected). Otherwise if it's SEQ we want all places
        Return (From a In arcList
                Where (a.ArcType.ID = AppSettingsGet.XORArcTypeId And a.PreCondition = preCondition) _
                       Or a.ArcType.ID = AppSettingsGet.SEQArcTypeId
                Select a.Place).ToList
    End Function

    Public Function CreateStartToken(ByVal projectId As Integer, ByVal isBDProject As Boolean) As Integer
        Dim initialToken As New Token

        initialToken.EnabledDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        initialToken.TokenStatus = DAOGetter.TokenStatusDAO(Context).GetByID(AppSettingsGet.TokenStatusFree)
        initialToken.Project = GetProject(projectId)
        initialToken.cCase = DAOGetter.CaseDAO(Context).GetByID(AppSettingsGet.WorkflowCaseId)
        initialToken.ContextEntity = DAOGetter.EntityDAO(Context).GetByID(AppSettingsGet.EntityProjectId)

        If isBDProject Then
            initialToken.Place = DAOGetter.PlaceDAO(Context).GetByID(AppSettingsGet.StartPlaceID)
        Else
            initialToken.Place = DAOGetter.PlaceDAO(Context).GetByID(AppSettingsGet.NonBDStartPlaceID)
        End If

        DAOGetter.TokenDAO(Context).Insert(initialToken)

        Return initialToken.ID
    End Function

#End Region

#Region "Get Oustanding Work Functionality"

    Public Function GetTokensByUserAndStatus(ByVal userId As Integer, ByVal tokenStatus As Integer, Optional ByVal projectId As Integer = 0) As List(Of Token)
        ' Get all tokens that are free
        Dim tokenList As List(Of Token) = DAOGetter.TokenDAO(Context).GetTokensByStatus(tokenStatus)

        If projectId <> 0 Then
            tokenList = (From token In tokenList
                         Where token.Project.ID = projectId AndAlso CheckIfNotElementEndToken(token)
                         Select token).ToList
        End If

        ' make sure all INs have been satisfied
        If tokenStatus = 1 Then
            RemoveTokensAwaitingOtherRoutes(tokenList)
        End If

        ' Get the transition and pass that the check if the user actually has access to it
        ' The reason for the inside function call is because we need to keep the token, if we seperated this
        ' and got the list of transitions assocaited, we wouldn't be able to get back to the token list.
        ' It looks bad, but if this method wasn't used, there would be a minimum of 2 loops and many lines of code!
        tokenList = (From token In tokenList
                     Where token.Place.ID <> AppSettingsGet.EndPlaceID _
                        AndAlso FulfillsTransitionSecurityLookup(userId, projectId, GetTransitionByToken(token).ID)
                     Select token).ToList

        Return tokenList
    End Function

    Public Sub FilterOutProjectsWithNoTokens(ByRef projectList As List(Of Project), ByVal userId As Integer)

        projectList = (From p In projectList
                       Where DoesProjectHaveTokensByUser(userId, p.ID)
                       Select p).ToList

    End Sub

    Public Function DoesProjectHaveTokensByUser(ByVal userId As Integer, ByVal projectId As Integer) As Boolean

        If GetTokensByUserAndStatus(userId, AppSettingsGet.TokenStatusConsumed, projectId).Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function GetTokenByStatus(ByVal tokenStatus As Integer, ByVal projectId As Integer) As List(Of Token)

        Return (From t In DAOGetter.TokenDAO(Context).GetTokensByStatus(tokenStatus)
                Where t.Project.ID = projectId
                Select t).ToList

    End Function

    Public Function GetTokenByContextAndEntity(ByVal contextId As Integer, ByVal entityId As Integer, ByVal tokenStatus As Integer) As List(Of Token)
        Return DAOGetter.TokenDAO(Context).GetFreeByContextAndEntity(contextId, entityId, tokenStatus)
    End Function

    Private Function CheckIfNotElementEndToken(ByVal currentToken As Token) As Boolean
        Dim isElementReady As Boolean = True

        If currentToken.Place.ID = AppSettingsGet.ElementPlaceEndID Then
            ' Check if all elements have completed the element process
            Dim elementsByProject As List(Of Element) = ElementManager.GetNonStoppedElementsByProject(currentToken.Project.ID)
            Dim tokensAtPlace As List(Of Token) = GetFreeTokensAtPlaceInProject(currentToken.Project.ID, AppSettingsGet.ElementPlaceEndID)

            ' Multiplied by 2 because the petrinet disobeying rules that apt has forced upon us
            ' means that the element section of the workflow ends where 2 transitions combine
            ' thus having to multiply the number of tokens required by 2.
            If (elementsByProject.Count * 2) > tokensAtPlace.Count Then
                isElementReady = False
            End If
        End If

        Return isElementReady
    End Function

    Private Sub RemoveTokensAwaitingOtherRoutes(ByRef tokenList As List(Of Token))
        ' Do a check, see if all place 'OUTs' are fulfilled.
        ' For instance, a place that requires two routes to be complete must have all the tokens from those routes present
        ' The exception to this rule is to ignore the Pre Conditioned Arcs (these may be rejects) which would cause the 
        ' workflow to stop at any point that can be returned to via a rejection.
        Dim newTokenList As New List(Of Token)

        For Each cToken In tokenList

            Dim currentToken As New Token
            currentToken = cToken

            ' Firstly, Are we at place 26, if we are this place doesn't abide by the petrinet rules and needs APT specific rulings
            ' place 26 is where there is a project meets element workflow crossover
            If currentToken.Place.ID = AppSettingsGet.AllowPrintPlaceID Then

                If IsElementTokenAllowedForPrint(currentToken) Then
                    newTokenList.Add(currentToken)
                End If

                ' If we're at delivery despatch we don't want to wait for other tokens to arrive (petrinet breaking apt rule)
            ElseIf currentToken.Place.ID = AppSettingsGet.PostPrintProductionNonBDPlaceID Then

                newTokenList.Add(currentToken)

            Else
                ' Otherwise carry on with the standard petrinet logic

                ' Get all the tokens at the tokens specified place that are active
                Dim tokens As List(Of Token) = (From t In tokenList
                                                Where t.Place.ID = currentToken.Place.ID
                                                Select t).ToList
                Dim tokensAtPlace As Integer = tokens.Count
                Dim rejectedToken As Boolean = ((From t In tokens
                                                Where t.Rejected
                                                Select t).ToList.Count > 0)

                ' Get all tokens that are required by law at that place
                Dim tokensRequiredAtPlace As Integer = (From a In DAOGetter.ArcDAO(Context).GetArcsFromPlace(currentToken.Place, "OUT")
                                                        Where a.PreCondition <> AppSettingsGet.RejectedPreCondition _
                                                            AndAlso a.PreCondition <> AppSettingsGet.StartedPreCondition
                                                        Select a).ToList.Count

                ' OK, so do we have sufficient number of tokens at the place, can the process continue?
                If rejectedToken OrElse tokensAtPlace >= tokensRequiredAtPlace OrElse AptSpecificRuleMatch(currentToken, tokensAtPlace) = True Then
                    ' If there is a token already at the place then don't add it
                    ' And add it if it's an element though!
                    ' Also add if it it's an element AND the element id's are the same
                    Dim isPlaceInList As Boolean = (From t In newTokenList
                                                    Where t.Place.ID = currentToken.Place.ID AndAlso _
                                                            (t.ContextEntity.ID <> AppSettingsGet.EntityElementId OrElse _
                                                              (t.ContextEntity.ID = AppSettingsGet.EntityElementId AndAlso t.ContextParentID = currentToken.ContextParentID))
                                                    Select True).SingleOrDefault

                    If isPlaceInList = False Then
                        newTokenList.Add(currentToken)
                    End If
                End If
            End If

        Next

        tokenList = newTokenList
    End Sub

    ' Another apt rule that breaks the petrinet rules is where the non-bd connect to the workflow.
    ' There are 2 places that will require a different number of arcs going in to the values set by the 
    ' petri net rules. At place 18 and 33 a BD project requires 2 present tokens and a non-bd project only
    ' requires 1 present token.
    Public Function AptSpecificRuleMatch(ByVal currentToken As Token, ByVal numTokensAtPlace As Integer) As Boolean

        If currentToken.Place.ID = AppSettingsGet.PreCollectBriefPlaceID Or currentToken.Place.ID = AppSettingsGet.PreWilliamsLeaFinalCostsPlaceID Then

            If currentToken.Project.IsBDProject Then

                ' Number of available tokens at the place must be 2 or more
                If numTokensAtPlace > 1 Then
                    Return True
                Else
                    Return False
                End If

            Else

                ' Number of available tokens at the place must 1 or more
                If numTokensAtPlace > 0 Then
                    Return True
                Else
                    Return False
                End If

            End If

        End If

        ' Special case for place 68
        ' This place was modified for the MDA changes
        ' The above rule will never succeed because it assumes that there will never be more than
        ' one rejection arc going to a place

        'If currentToken.Place.ID = AppSettingsGet.BriefToProcurementPlaceID Then
        '    Return True
        'End If

        'If currentToken.Place.ID = AppSettingsGet.PrintRequiredPlaceID Then
        '    ' This part of the workflow does not need two tokens if it is a non DB project 
        '    If currentToken.Project.IsBDProject = False Then

        '        If numTokensAtPlace > 0 Then
        '            Return True
        '        Else
        '            Return False
        '        End If

        '    End If
        'End If

        Return False

    End Function

    Private Function IsElementTokenAllowedForPrint(ByVal currentToken As Token) As Boolean
        ' If print isn't required this can be passed straight off!
        If currentToken.Project.IsBDProject = False AndAlso currentToken.Project.PrintRequired = False Then
            Return True
        End If

        ' Has transition 40 been complete?
        Dim consumedTokens As List(Of Token) = GetTokenByStatus(AppSettingsGet.TokenStatusConsumed, currentToken.Project.ID)

        Dim transition40Count As Integer = (From t In consumedTokens
                                            Where GetTransitionByToken(t).ID = AppSettingsGet.PrintGoAheadTransitionID
                                            Select t).ToList.Count

        If transition40Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ContainsToken(ByVal token As Token, ByVal tokenList As List(Of Token)) As Boolean

        Dim tokenSelected As Token = (From t In tokenList
                                      Where t.ID <> token.ID And t.ContextEntity.ID = token.ContextEntity.ID _
                                          And t.ContextParentID = token.ContextParentID
                                      Select t).FirstOrDefault

        Dim contains As Boolean = True

        If tokenSelected IsNot Nothing Then
            contains = False
        End If

        Return contains

    End Function

    Public Function HasElementWorkflowStarted(ByVal projectId As Integer) As Boolean

        Dim tmpList As List(Of Token) = GetTokensAtPlaceInProject(projectId, _
                                                                  AppSettingsGet.PreCollectBriefPlaceID, _
                                                                  AppSettingsGet.TokenStatusConsumed)

        If tmpList.Count > 0 Then
            If WorkflowManager.AptSpecificRuleMatch(tmpList.Item(0), tmpList.Count) = True Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function HasBriefBeenSignedOff(ByVal projectId As Integer) As Boolean

        Dim printReqTokenList As List(Of Token) = GetAllTokensAtPlaceInProject(projectId, AppSettingsGet.PrintRequiredPlaceID)
        Dim ElementStartTokenList As List(Of Token) = GetAllTokensAtPlaceInProject(projectId, AppSettingsGet.PreCollectBriefPlaceID)

        If printReqTokenList.Count > 0 OrElse ElementStartTokenList.Count > 0 Then
            Return True
        End If

        Return False

    End Function

#End Region

#Region "Security Lookups"

    Private Function FulfillsTransitionSecurityLookup(ByVal userId As Integer, ByVal projectId As Integer, ByVal transitionId As Integer) As Boolean
        Dim transition As Transition = DAOGetter.TransitionDAO(Context).GetByID(transitionId)

        If transition Is Nothing Then
            Return False
        End If

        Dim transitionSecurities As List(Of TransitionSecurity) = DAOGetter.TransitionSecurityDAO(Context).GetByTransition(transitionId)
        Dim isAssigned As Boolean = False

        ' Go through all of the transitions security levels
        For Each TransitionSecurity In transitionSecurities
            ' Checkout what security rules need to be applied to this transition
            ' Depending on the type of security there is to apply, use the appropriate rules
            ' to determine whether the user is allowed access to this section
            Select Case TransitionSecurity.SecurityLookup.ID
                Case AppSettingsGet.SecurityLookupUserID
                    isAssigned = IIf(userId = TransitionSecurity.ParentID, True, False)
                Case AppSettingsGet.SecurityLookupRoleID
                    isAssigned = UserManager.UserHasGlobalRole(userId, TransitionSecurity.ParentID)
                Case AppSettingsGet.SecurityLookupProjectID
                    isAssigned = UserManager.UserHasProjectRole(userId, TransitionSecurity.ParentID, projectId)
            End Select

            ' If it's true, no need to check the others
            If isAssigned = True Then
                Return isAssigned
            End If
        Next

        Return isAssigned
    End Function

    Public Function GetRolesByTransition(ByVal transitionId As Integer, ByVal securityLookupId As Integer) As List(Of Role)
        Dim transitionSecurities As List(Of TransitionSecurity) = DAOGetter.TransitionSecurityDAO(Context).GetByTransition(transitionId)

        Return (From ts In transitionSecurities
                Where ts.Transition.ID = transitionId And ts.SecurityLookup.ID = securityLookupId
                Select UserManager.GetRoleByID(ts.ParentID)).ToList
    End Function

#End Region

#Region "Tokens"

    Public Function GetTokenByID(ByVal tokenId As Integer) As Token
        Return DAOGetter.TokenDAO(Context).GetByID(tokenId)
    End Function

    Public Function GetAllTokensAtPlaceInProject(ByVal projectId As Integer, ByVal placeId As Integer) As List(Of Token)

        Dim projectTokens As List(Of Token) = DAOGetter.TokenDAO(Context).GetTokensByProject(projectId)

        Return (From t In projectTokens
                Where t.Place.ID = placeId
                Select t).ToList()

    End Function

    Public Function GetFreeTokensAtPlaceInProject(ByVal projectId As Integer, ByVal placeId As Integer) As List(Of Token)
        Return GetTokensAtPlaceInProject(projectId, placeId, AppSettingsGet.TokenStatusFree)
    End Function

    Public Function GetTokensAtPlaceInProject(ByVal projectId As Integer, ByVal placeId As Integer, ByVal tokenStatus As Integer) As List(Of Token)
        Dim tokenList As List(Of Token) = DAOGetter.TokenDAO(Context).GetTokensByStatus(tokenStatus)

        Return (From t In tokenList
                Where t.Project.ID = projectId And t.Place.ID = placeId
                Select t).ToList
    End Function

    Public Function GetTokenByElementAndUser(ByVal elementId As Integer, ByVal userId As Integer) As Token
        Dim tokenList As List(Of Token) = GetTokensByUserAndStatus(userId, AppSettingsGet.TokenStatusFree)

        Return (From t In tokenList
                Where t.ContextEntity.ID = AppSettingsGet.EntityElementId And t.ContextParentID = elementId
                Select t).FirstOrDefault
    End Function

    Public Function GetStartTokenByProject(projectId As Integer) As Token

        Return DAOGetter.TokenDAO(Context).GetFirstByProject(projectId)

    End Function

    Public Function GetAllStartTokens() As List(Of Token)
        Dim startTokens As List(Of Token) = New List(Of Token)
        startTokens.AddRange(DAOGetter.TokenDAO(Context).GetAllTokensAtPlace(AppSettingsGet.StartPlaceID))
        startTokens.AddRange(DAOGetter.TokenDAO(Context).GetAllTokensAtPlace(AppSettingsGet.NonBDStartPlaceID))
        Return startTokens
    End Function

    Public Function GetLastTokenByProject(projectId As Integer) As Token

        Dim tokensByProject = DAOGetter.TokenDAO(Context).GetTokensByProject(projectId)
        Return (From t In tokensByProject
                Where t.Place.ID = AppSettingsGet.EndPlaceID
                Select t).FirstOrDefault

    End Function

    ' Generates a token by place and for a specific project.
    Private Sub CreateTokenForPlace(ByVal place As Place, ByVal currentToken As Token, ByVal preCondition As String)
        modLogManager.Debug(String.Format("CreateTokenForPlace - Create token at place {0}", place.ID))

        Dim newToken As New Token

        newToken.Project = currentToken.Project
        newToken.TokenStatus = DAOGetter.TokenStatusDAO(Context).GetByID(AppSettingsGet.TokenStatusFree)
        newToken.Place = place
        newToken.ContextEntity = GetEntityById(AppSettingsGet.EntityProjectId)
        newToken.cCase = GetCaseByID(AppSettingsGet.WorkflowCaseId)
        newToken.EnabledDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        If preCondition = "r" Then
            newToken.Rejected = True
        End If

        ' If we're an element token then we need to retain the context id and the element id (and if we aren't at the end of elements)
        If currentToken.ContextEntity.ID = AppSettingsGet.EntityElementId And place.ID <> AppSettingsGet.ElementPlaceEndID Then
            newToken.ContextEntity = currentToken.ContextEntity
            newToken.ContextParentID = currentToken.ContextParentID
        End If

        DAOGetter.TokenDAO(Context).Insert(newToken)

        TaskLibraryManager.NewTokenGenerated(newToken.ID)
        modLogManager.Debug("CreateTokenForPlace - Complete")
    End Sub

    Public Sub CreateNewToken(ByVal newToken As Token)

        newToken.TokenStatus = DAOGetter.TokenStatusDAO(Context).GetByID(AppSettingsGet.TokenStatusFree)
        newToken.EnabledDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        newToken.cCase = GetCaseByID(AppSettingsGet.WorkflowCaseId)
        DAOGetter.TokenDAO(Context).Insert(newToken)

        TaskLibraryManager.NewTokenGenerated(newToken.ID)

    End Sub

    Private Sub CreateTokenForProjectElements(ByVal place As Place, ByVal currentToken As Token, ByVal preCondition As String)
        Dim projectsElements As List(Of Element) = ElementManager.GetNonStoppedElementsByProject(currentToken.Project.ID)

        ' Get all the tokens at the tokens specified place
        Dim tokensAtPlace As Integer = (From t In GetFreeTokensAtPlaceInProject(currentToken.Project.ID, place.ID)
                                        Where t.Place.ID = currentToken.Place.ID
                                        Select t).ToList.Count

        Console.WriteLine(String.Format("Number of Elements : {0}", projectsElements.Count))

        For Each projectElement In projectsElements
            Dim newToken As New Token

            newToken.Project = currentToken.Project
            newToken.ContextEntity = GetEntityById(AppSettingsGet.EntityElementId)
            newToken.ContextParentID = projectElement.ID
            newToken.TokenStatus = DAOGetter.TokenStatusDAO(Context).GetByID(AppSettingsGet.TokenStatusFree)
            newToken.Place = place
            newToken.cCase = GetCaseByID(AppSettingsGet.WorkflowCaseId)
            newToken.EnabledDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            If preCondition = "r" Then
                newToken.Rejected = True
            End If

            DAOGetter.TokenDAO(Context).Insert(newToken)

            TaskLibraryManager.NewTokenGenerated(newToken.ID)

            Console.WriteLine(String.Format("Token created for Element {0}", projectElement.ID))
        Next

    End Sub

    Public Sub UpdateToken(ByRef saveToken As Token)
        DAOGetter.TokenDAO(Context).Update(saveToken)
    End Sub

    Public Function GetTokenStatus(ByVal Id As Integer) As TokenStatus
        Return DAOGetter.TokenStatusDAO(Context).GetByID(Id)
    End Function

    Public Sub ChangeTokenStatus(ByVal token As Token, ByVal tokenStatusId As Integer)

        token.TokenStatus = GetTokenStatus(tokenStatusId)
        UpdateToken(token)

    End Sub

#End Region

#Region "Transitions"

    Public Function GetTransitionByToken(ByVal currentToken As Token) As Transition
        ' Get transition by the place
        Dim tokenTransition As Transition = DAOGetter.ArcDAO(Context).GetTransitionFromStartPlace(currentToken.Place.ID)

        Return tokenTransition
    End Function

    ''' <summary>
    ''' Gets all the transitions that come after the places sent in. Does not get any that are in a finished state.
    ''' </summary>
    ''' <param name="placeList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTransitionsOfPlaces(ByVal placeList As List(Of Place)) As List(Of Transition)
        Return (From p In placeList
                Where p.PlaceType.ID <> AppSettingsGet.PlaceTypeFinish
                Select DAOGetter.TransitionDAO(Context).GetTransitionFromPlace(p)).ToList
    End Function

    Public Function IsTransitionComplete(ByVal transitionId As Integer, ByVal projectId As Integer) As Boolean
        Return DoesTransitionHaveTokenStatus(transitionId, projectId, AppSettingsGet.TokenStatusConsumed)
    End Function

    Public Function IsFreeTokenAtTransition(ByVal transitionId As Integer, ByVal projectId As Integer) As Boolean
        Return DoesTransitionHaveTokenStatus(transitionId, projectId, AppSettingsGet.TokenStatusFree)
    End Function

    Public Function DoesTransitionHaveTokenStatus(ByVal transitionId As Integer, ByVal projectId As Integer, ByVal tokenStatus As Integer) As Boolean
        ' Get the place from the transition

        Dim places As List(Of Place) = DAOGetter.PlaceDAO(Context).GetPlacesFromTransition(DAOGetter.TransitionDAO(Context).GetByID(transitionId))

        For Each Place As Place In places

            ' Get the token from the place
            Dim tokensAtPlace As List(Of Token) = GetTokensAtPlaceInProject(projectId, Place.ID, tokenStatus)

            ' Any tokens there? If not let's just let them know the bad news and not carry on
            If tokensAtPlace.Count = 0 Then
                Return False
            End If

            ' If any of the tokens aren't consumed then this it's just not ready
            For Each Token As Token In tokensAtPlace
                If Token.TokenStatus.ID <> tokenStatus Then
                    Return False
                End If
            Next

        Next

        ' You know that this transition is done, Woop.
        Return True

    End Function

#End Region

#Region "Places"

    ''' <summary>
    ''' Does what it says. Getting any places that the list of tokens are currently at. it also completes a check,
    ''' only free tokens will be included.
    ''' </summary>
    ''' <param name="tokenList">The tokens in which the places are to be attained</param>
    ''' <returns>The list of places that the free tokens are at.</returns>
    Private Function GetPlacesAssociatedwithTokens(ByVal tokenList As List(Of Token)) As List(Of Place)
        Dim tokenPlaces As New List(Of Place)

        ' loop tokens, getting all the places associated with them
        For Each token As Token In tokenList
            If token.TokenStatus.ID = AppSettingsGet.TokenStatusFree Then
                tokenPlaces.AddRange(DAOGetter.PlaceDAO(Context).GetAllPlacesByToken(token))
            End If
        Next

        Return tokenPlaces
    End Function

    ''' <summary>
    ''' Checks whether the place in question has all required sequential tokens currently in position.
    ''' </summary>
    ''' <param name="place">The place being queried</param>
    ''' <returns>A boolean represeting whether all tokens have arrived at the place.</returns>
    Public Function CheckPlaceHasAllTokens(ByVal place As Place, ByVal projectId As Integer) As Boolean
        Dim tokenList As List(Of Token) = DAOGetter.TokenDAO(Context).GetTokensAtPlaceByStatus(place.ID, projectId, 1)
        Dim rejectedToken As Boolean = ((From t In tokenList
                                                Where t.Rejected
                                                Select t).ToList.Count > 0)

        ' Get all tokens that are required by law at that place
        Dim tokensRequiredAtPlace As Integer = (From a In DAOGetter.ArcDAO(Context).GetArcsFromPlace(place, "OUT")
                                                Where a.PreCondition <> AppSettingsGet.RejectedPreCondition _
                                                    AndAlso a.PreCondition <> AppSettingsGet.StartedPreCondition
                                                Select a).ToList.Count
        Dim retVal As Boolean = False

        If rejectedToken OrElse tokenList.Count = tokensRequiredAtPlace Then
            retVal = True
        End If

        Return retVal
    End Function

    Public Function GetPlace(ByVal placeId As Integer) As Place
        Return DAOGetter.PlaceDAO(Context).GetByID(placeId)
    End Function

#End Region

#Region "Entities"

    Public Function GetEntityById(ByVal entityId As Integer) As Entity
        Return DAOGetter.EntityDAO(Context).GetByID(entityId)
    End Function

#End Region

#Region "Cases"

    Public Function GetCaseByID(ByVal caseId As Integer) As wfCase
        Return DAOGetter.CaseDAO(Context).GetByID(caseId)
    End Function

#End Region

#Region "ArcResponses"

#Region "Gets"

    Public Function GetArcResponse(ByVal arcResponseId As Integer) As ArcResponse
        Return DAOGetter.ArcResponseDAO(Context).GetByID(arcResponseId)
    End Function

    Public Function GetAllArcResponses() As List(Of ArcResponse)
        Return DAOGetter.ArcResponseDAO(Context).GetAll()
    End Function

    Public Function GetArcResponsesByActive(Optional ByVal active As Boolean = True) As List(Of ArcResponse)
        Return DAOGetter.ArcResponseDAO(Context).GetByActive(active)
    End Function

#End Region

#Region "Save / Update / Delete"

    Public Sub SaveArcResponse(ByRef saveArcResponse As ArcResponse)

        saveArcResponse.Modified = DateTime.Now

        If saveArcResponse.ID = 0 Then
            saveArcResponse.Active = True
            saveArcResponse.Created = DateTime.Now
            DAOGetter.ArcResponseDAO(Context).Insert(saveArcResponse)
        Else
            DAOGetter.ArcResponseDAO(Context).Update(saveArcResponse)
        End If

    End Sub

    Public Sub SetArcResponseActivity(ByVal arcResponseId As Integer, ByVal active As Boolean)

        Dim enableArcResponse As ArcResponse = GetArcResponse(arcResponseId)

        enableArcResponse.Active = active
        SaveArcResponse(enableArcResponse)

    End Sub

#End Region

#End Region

#Region "Document Task Association"

    Public Sub AssociatedDocumentWithToken(ByVal documentId As Integer, ByVal tokenId As Integer)
        Dim tokensDocument As New TokenDocument

        tokensDocument.Token = GetTokenByID(tokenId)
        tokensDocument.Document = ProjectDocumentManager.GetProjectDocumentById(documentId)

        If DAOGetter.TokenDocumentDAO(Context).GetByTokenAndDocumentID(tokenId, documentId) Is Nothing Then
            DAOGetter.TokenDocumentDAO(Context).Insert(tokensDocument)
        End If
    End Sub

    Public Function GetTokenDocuments(ByVal tokenId As Integer) As List(Of TokenDocument)
        Return DAOGetter.TokenDocumentDAO(Context).GetAllByTokenID(tokenId)
    End Function

#End Region

#Region "Special Queries"

    Public Function HasProjectBriefBeenFinalised(ByVal projectId As Integer)

        ' Just a crude check to see if there a is a token with a specific placeId that has been consumed

        If GetTokensAtPlaceInProject(projectId, AppSettingsGet.FinalisedBriefBDPlaceID, AppSettingsGet.TokenStatusConsumed).Count > 0 Then
            Return True
        End If

        If GetTokensAtPlaceInProject(projectId, AppSettingsGet.FinalisedBriefNonBDPlaceID, AppSettingsGet.TokenStatusConsumed).Count > 0 Then
            Return True
        End If

        Return False

    End Function

    Public Function IsStartPlace(ByVal placeId As Integer) As Boolean

        If placeId = AppSettingsGet.NonBDStartPlaceID OrElse placeId = AppSettingsGet.StartPlaceID Then
            Return True
        End If

        Return False

    End Function

#End Region

End Module
