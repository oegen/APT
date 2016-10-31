'----------------------------------------------------------------------------------------------
' Filename    : HistoryGenerator.vb
' Description : Deals with getting all the history associated with a project.
'
' Release Initials  Date        Comment
' 1       LP        07/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptDAL
Imports aptEntities
Imports GenericDAL

Public Module HistoryGenerator

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

    Public Function GetTokenHistory(ByVal projectId As Integer) As List(Of Token)

        Dim tokenList As List(Of Token) = DAOGetter.TokenDAO(Context).GetTokensByProject(projectId)

        Return (From t In tokenList
                Where t.TokenStatus.ID = AppSettingsGet.TokenStatusConsumed
                Select t
                Order By t.ConsumedDate Descending).ToList

    End Function

    Public Function GetEntityHistory(ByVal projectId As Integer, Optional ByVal elementId As Integer = 0) As List(Of EntityHistory)

        Dim consumedTokens As List(Of Token)

        If elementId <> 0 Then
            consumedTokens = (From t In GetTokenHistory(projectId)
                              Where (t.ContextParentID = elementId)
                              Select t).ToList()
        Else
            consumedTokens = GetTokenHistory(projectId)
        End If

        Dim projectElementHistory As New List(Of EntityHistory)
        Dim previousTokenPlaceId As Integer = 0

        For Each consumedToken As Token In consumedTokens

            If consumedToken.Place.ID <> previousTokenPlaceId Then

                Dim tmpTransition As Transition = WorkflowManager.GetTransitionByToken(consumedToken)
                Dim elementHistory As New EntityHistory

                elementHistory.Action = tmpTransition.Task.Name
                elementHistory.Comment = consumedToken.Comment
                elementHistory.DateTimeCompleted = consumedToken.ConsumedDate
                elementHistory.TasksDocuments = GetTokenDocuments(consumedToken.ID)

                If consumedToken.AptUser IsNot Nothing Then
                    elementHistory.User = consumedToken.AptUser.FullName
                Else
                    elementHistory.User = "N/A"
                End If

                projectElementHistory.Add(elementHistory)

            End If

            ' This is a hack to stop duplicate tokens appearing in the history table
            previousTokenPlaceId = consumedToken.Place.ID
        Next

        Return projectElementHistory

    End Function

End Module
