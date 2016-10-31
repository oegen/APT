'----------------------------------------------------------------------------------------------
' Filename    : KitManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module KitManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Get Variations"

    Public Function GetKit(ByVal kitId As Integer) As Kit
        Return DAOGetter.KitDAO(Context).GetByID(kitId)
    End Function

    Public Function GetKitElement(ByVal kitElementId As Integer) As KitElement
        Return DAOGetter.KitElementsDAO(Context).GetByID(kitElementId)
    End Function

    Public Function GetKitElementByTypeAndKit(ByVal typeId As Integer, ByVal quoteId As Integer, ByVal itemTypeId As Integer) As KitElement
        Return DAOGetter.KitElementsDAO(Context).GetKitElementsByItemAndKit(typeId, quoteId, itemTypeId)
    End Function

    Public Function GetKitElementsByQuote(ByVal quoteId As Integer) As List(Of KitElement)
        Return DAOGetter.KitElementsDAO(Context).GetKitElementsByQuote(quoteId)
    End Function

    Public Function GetKitsByProject(ByVal projectId As Integer) As List(Of Kit)
        Return DAOGetter.KitDAO(Context).GetKitByProject(projectId)
    End Function

    Public Function GetQuotesByKit(ByVal kitId As Integer) As List(Of KitQuote)
        Return DAOGetter.KitQuoteDAO(Context).GetQuotes(kitId)
    End Function

    Public Function GetLatestKitQuote(ByVal kitId As Integer) As KitQuote
        Return DAOGetter.KitQuoteDAO(Context).GetLatestByKit(kitId)
    End Function

    Public Function GetKitQuote(ByVal quoteId As Integer) As KitQuote
        Return DAOGetter.KitQuoteDAO(Context).GetByID(quoteId)
    End Function

#End Region

#Region "Insertion / Updating / Removal"

    Public Sub SaveAllKitElements(ByVal saveKitElements As List(Of KitElement), ByVal userId As Integer)

        If saveKitElements.Count > 0 Then

            Dim auditLog As String = ""
            Dim kitName As String = saveKitElements(0).KitQuote.Kit.Name

            auditLog = GetModifiedKitContentAuditLog(kitName)

            For Each tmpKitElement As KitElement In saveKitElements
                SaveKitElement(tmpKitElement)
            Next

            ' TODOKIT:
            ' AuditTrailManager.PostAudit(auditLog, userId, saveKitElements(0).Element.Project.ID, AppSettingsGet.EditAuditChangeTypeID, AppSettingsGet.KitSectionID)

        End If

    End Sub

    Private Sub SaveKitElement(ByRef saveKitElement As KitElement)

        If saveKitElement.ID = 0 Then
            DAOGetter.KitElementsDAO(Context).Insert(saveKitElement)
        Else
            DAOGetter.KitElementsDAO(Context).Update(saveKitElement)
        End If

    End Sub

    Public Sub SaveKitAndQuote(ByRef saveKit As Kit, ByVal userId As Integer, Optional ByVal saveQuote As KitQuote = Nothing)

        If saveKit.ID = 0 Then
            Dim changeType As Integer = AppSettingsGet.AddAuditChangeTypeID
            Dim auditLog As String = GetNewKitAuditLog(saveKit)
            saveKit.Active = True
            DAOGetter.KitDAO(Context).Insert(saveKit)
            AuditTrailManager.PostAudit(auditLog, userId, saveKit.Project.ID, changeType, AppSettingsGet.KitSectionID)
        Else
            DAOGetter.KitDAO(Context).Update(saveKit)
        End If

        If saveQuote IsNot Nothing Then
            saveQuote.Kit = saveKit
            SaveKitQuote(saveQuote, userId)
        End If

    End Sub

    Private Sub SaveKitQuote(ByRef saveQuote As KitQuote, ByVal userId As Integer)

        Dim auditLog As String = ""
        Dim changeType As Integer

        saveQuote.Number = KitManager.GetLatestQuoteNumberForKit(saveQuote.Kit.ID)

        If saveQuote.ID = 0 Then
            changeType = AppSettingsGet.AddAuditChangeTypeID
            auditLog = String.Format("A new quote has been added for kit {0}", saveQuote.Kit.Name)
            DAOGetter.KitQuoteDAO(Context).Insert(saveQuote)
        Else
            changeType = AppSettingsGet.EditAuditChangeTypeID
            DAOGetter.KitQuoteDAO(Context).Update(saveQuote)
        End If

        AuditTrailManager.PostAudit(auditLog, userId, saveQuote.Kit.Project.ID, changeType, AppSettingsGet.KitSectionID)

    End Sub

    Public Sub SaveKitContents(ByVal saveKitContents As List(Of KitItem),
                               ByVal quoteId As Integer,
                               ByVal itemType As KitItemType)

        For Each saveKitItem As KitItem In saveKitContents

            Dim tmpKitElement As KitElement = GetKitElementByTypeAndKit(saveKitItem.ItemID, _
                                                                        quoteId, _
                                                                        KitManager.GetKitElementTypeId(itemType))

            If tmpKitElement Is Nothing Then
                tmpKitElement = New KitElement
                tmpKitElement.KitQuoteId = quoteId
                tmpKitElement.ItemId = saveKitItem.ItemID
                tmpKitElement.ItemType = GetKitElementTypeId(itemType)
            End If

            If saveKitItem.Quantity = 0 Then
                ' The item needs to be deleted
                KitManager.RemoveKitElement(tmpKitElement)
            Else
                tmpKitElement.Quantity = saveKitItem.Quantity
                SaveKitElement(tmpKitElement)
            End If

        Next

    End Sub

    Public Sub RemoveKitElement(ByVal deleteKitElement As KitElement)
        DAOGetter.KitElementsDAO(Context).Delete(deleteKitElement)
    End Sub

    Public Sub RemoveKit(ByVal kitId As Integer)

        ' we need to delete all the kit elements and then the kit

        Dim deleteKit As Kit = DAOGetter.KitDAO(Context).GetByID(kitId)
        Dim deleteQuotes As List(Of KitQuote) = DAOGetter.KitQuoteDAO(Context).GetQuotes(kitId)

        For Each quote In deleteQuotes

            Dim deleteKitElements As List(Of KitElement) = DAOGetter.KitElementsDAO(Context).GetKitElementsByQuote(quote.ID)

            For Each tmpKitElement In deleteKitElements
                ' Remove all the contents from the kit elements
                DAOGetter.KitElementsDAO(Context).Delete(tmpKitElement)
            Next
            ' Remove the quote
            DAOGetter.KitQuoteDAO(Context).Delete(quote)
        Next

        DAOGetter.KitDAO(Context).Delete(deleteKit)

    End Sub

    Public Sub RemoveItemByTypeFromKits(ByVal itemId As Integer, ByVal typeId As Integer)

        Dim kitContentsWithDeleteBBCItem As List(Of KitElement) = _
          DAOGetter.KitElementsDAO(Context).GetKitElementByElement(itemId, typeId)

        For Each deleteKitContent In kitContentsWithDeleteBBCItem
            KitManager.RemoveKitElement(deleteKitContent)
        Next

    End Sub

#End Region

#Region "Clone"

    Private Function GetCopyKit(ByVal kitId As Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.KitDAO(staticContext).GetByID(kitId)
    End Function

#End Region

#Region "Audit Log"

    Public Function GetNewKitAuditLog(ByVal newKit As Kit)

        Dim auditLog As String = ""
        auditLog += String.Format("A new kit {0} has been added", newKit.Name)

        Return auditLog

    End Function

    Public Function GetModifiedKitAuditLog(ByVal newKit As Kit, Optional ByVal newLine As String = "<br />") As String

        Dim oldKit As Kit = GetCopyKit(newKit.ID)
        Dim auditLog As String = ""

        If oldKit.Name <> newKit.Name Then
            auditLog += String.Format("Kit Name {0} details has been altered", newKit.Name)
            auditLog += newLine
        End If

        If oldKit.Name <> newKit.Name Then
            auditLog += String.Format("Name has been from {1} changed to {0}", newKit.Name, oldKit.Name)
            auditLog += newLine
        End If

        Return auditLog

    End Function

    Public Function GetNewKitContentAuditLog(ByVal saveKitElement As KitElement) As String

        Dim auditLog As String = ""
        auditLog += String.Format("Contents for {0} has been entered", saveKitElement.KitQuote.Kit.Name)

        Return auditLog

    End Function

    Public Function GetModifiedKitContentAuditLog(ByVal kitName As String) As String

        Dim auditLog As String = ""
        auditLog += String.Format("Contents in {0} have been modified", kitName)

        Return auditLog

    End Function

#End Region

#Region "Query"

    Public Function GetKitElementTypeId(ByVal itemType As KitItemType) As Integer

        Select Case itemType

            Case KitItemType.ELEMENT
                Return AppSettingsGet.ElementTypeID
            Case KitItemType.PREMIUM_ELEMENT
                Return AppSettingsGet.PremiumElementTypeID
            Case KitItemType.BBC_ITEM
                Return AppSettingsGet.BBCItemTypeID

        End Select

    End Function

    Public Function GetKitElementName(ByVal id As Integer, ByVal itemType As Integer) As String

        Select Case itemType

            Case AppSettingsGet.ElementTypeID
                Return DAOGetter.ElementDAO(Context).GetByID(id).Name
            Case AppSettingsGet.PremiumElementTypeID
                Return DAOGetter.AdditionalElementDAO(Context).GetByID(id).Name
            Case AppSettingsGet.BBCItemTypeID
                ' Return DAOGetter.BBCItemDAO(Context).GetByID(id).Item
                Return DAOGetter.ProjectBBCItemsDAO(Context).GetByID(id).BBCItem.Description
        End Select

    End Function

    Public Function GetKitElementTypeString(ByVal itemType As Integer) As String

        Select Case itemType

            Case AppSettingsGet.ElementTypeID
                Return "Element"
            Case AppSettingsGet.PremiumElementTypeID
                Return "Premium Element"
            Case AppSettingsGet.BBCItemTypeID
                Return "BBC Item"
        End Select

    End Function

    Public Function HasKitBeenFinalised(ByVal projectId As Integer) As Boolean

        If aptBusinessLogic.IsFreeTokenAtTransition(AppSettingsGet.KittingFinalTransitionID, projectId) = False Then

            If aptBusinessLogic.IsTransitionComplete(AppSettingsGet.KittingFinalTransitionID, projectId) Then
                Return True
            End If

        End If

        Return False

    End Function

    Public Function DoesKitBelongToProject(ByVal kitId As Integer, ByVal projectId As Integer) As Boolean

        Dim tmpKit As Kit = DAOGetter.KitDAO(Context).GetByID(kitId)

        If tmpKit IsNot Nothing Then
            If tmpKit.Project.ID = projectId Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function GetLatestQuoteNumberForKit(ByVal kitId As Integer) As Integer
        Return DAOGetter.KitQuoteDAO(Context).GetQuotes(kitId).Count + 1
    End Function

#End Region

End Module