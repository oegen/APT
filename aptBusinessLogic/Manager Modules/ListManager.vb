'----------------------------------------------------------------------------------------------
' Filename    : ListManager.vb
' Description : Any list functionality and logical implications will be applied within the 
'               list manager.
'
' Release Initials  Date        Comment
' 1       LP/TL     27/05/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module ListManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Get Variations"

    Public Function GetList(ByVal listId As Integer) As AptList
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.ListDAO(context).GetByID(listId)
    End Function

    Public Function GetAllList() As List(Of AptList)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        Return DAOGetter.ListDAO(context).GetAll
    End Function

    Public Function GetListNode(ByVal id As Integer) As ListNode

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ListNodeDAO(context).GetByID(id)

    End Function

    Public Function GetListsNodes(ByVal listId As Integer) As List(Of ListNode)
        Return DAOGetter.ListNodeDAO(context).GetByList(listId)
    End Function

#End Region

#Region "Insertion / Removal"

    Public Sub SaveList(ByRef saveList As AptList)

        If ListNameExists(saveList.Name, saveList.ID) Then
            Throw New NameAlreadyExistsException("A list with this name already exists")
        End If

        If saveList.ID = 0 Then
            saveList.Active = True
            saveList.pProtected = True 'TODO: do we need to take this out? seems pretty pointless

            DAOGetter.ListDAO(Context).Insert(saveList)
        Else
            DAOGetter.ListDAO(Context).Update(saveList)
        End If

    End Sub

    Public Sub SaveNode(ByRef saveNode As ListNode)

        If NodeNameExists(saveNode.Name, saveNode.List.ID, saveNode.ID) Then
            Throw New NameAlreadyExistsException("A Node with this name already exists in this list")
        End If

        If saveNode.ID = 0 Then
            saveNode.Active = True
            DAOGetter.ListNodeDAO(Context).Insert(saveNode)
        Else
            DAOGetter.ListNodeDAO(Context).Update(saveNode)
        End If

    End Sub

    Public Sub RemoveNode(ByRef nodeId As Integer)

        Dim removeNode As ListNode = DAOGetter.ListNodeDAO(Context).GetByID(nodeId)
        removeNode.Active = False
        SaveNode(removeNode)

    End Sub

#End Region

#Region "Queries"

    Private Function ListNameExists(ByVal listName As String, Optional ByVal listId As Integer = 0) As Boolean

        If DAOGetter.ListDAO(Context).GetByName(listName, listId).Count > 0 Then
            Return True
        End If

        Return False

    End Function

    Private Function NodeNameExists(ByVal nodeName As String, ByVal listId As Integer, _
                                    Optional ByVal nodeId As Integer = 0) As Boolean

        If DAOGetter.ListNodeDAO(Context).GetByName(nodeName, listId, nodeId).Count > 0 Then
            Return True
        End If

        Return False

    End Function

#End Region


End Module
