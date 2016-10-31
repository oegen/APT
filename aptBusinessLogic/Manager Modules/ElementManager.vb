'----------------------------------------------------------------------------------------------
' Filename    : ElementManager.vb
' Description : All element related functionality can be found within the element manager module.
'
' Release Initials  Date        Comment
' 3       TL        23/02/2011  Added new fields to premium products audit trail
' 2       TL        23/02/2011  Removed Cost per item and print costs field from element print details audit
' 1       LP/TL     27/05/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL
Imports GenericUtilities

Public Module ElementManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Get Variations"

#Region "Elements"

    Public Function GetElement(ByVal elementId As Integer) As Element
        Return DAOGetter.ElementDAO(context).GetByID(elementId)
    End Function

    Public Function GetAllElements() As List(Of Element)
        Return DAOGetter.ElementDAO(context).GetByActive()
    End Function

    Public Function GetAllElementsByProject(ByVal projectId As Integer) As List(Of Element)
        Return DAOGetter.ElementDAO(context).GetAllByProject(projectId)
    End Function

    Public Function GetElementSubClasses(ByVal elementId As Integer) As List(Of SubclassType)
        Return DAOGetter.SubclassTypeDAO(context).GetByElement(GetElement(elementId))
    End Function

    Public Function GetElementSchemaByElementSubType(ByVal subtypeId As Integer) As Schema
        Dim subType As SubclassType = DAOGetter.SubclassTypeDAO(context).GetByID(subtypeId)
        Return subType.ElementSchema
    End Function

    Public Function GetSchemaDefinitionByElementSchemas(ByVal schemaId As Integer) As List(Of SchemaDefinition)
        Return DAOGetter.SchemaDefinitionDAO(context).GetBySchema(schemaId)
    End Function

    Public Function GetElementsByProject(ByVal projectId As Integer) As List(Of Element)
        Return DAOGetter.ElementDAO(context).GetByProject(projectId)
    End Function

    Public Function GetNonStoppedElementsByProject(ByVal projectId As Integer) As List(Of Element)
        Return DAOGetter.ElementDAO(context).GetNonStoppedByProject(projectId)
    End Function

    Public Function GetElementAdditionalInfoByElement(ByVal elementId As Integer) As ElementAdditionalDetails
        Return DAOGetter.ElementAdditionalDetailsDAO(context).GetByElement(elementId)
    End Function

    Public Function GetElementAdditionalInfo(ByVal elementAdditionalInfoId As Integer) As ElementAdditionalDetails
        Return DAOGetter.ElementAdditionalDetailsDAO(context).GetByID(elementAdditionalInfoId)
    End Function

    Public Function GetPremiumElementDetails(ByVal premiumElementDetailId As Integer)
        Return DAOGetter.PremiumElementDetailsDAO(context).GetByID(premiumElementDetailId)
    End Function

    Public Function GetPremiumElementDetailsByAdditionalElement(ByVal additionalElementId As Integer)
        Return DAOGetter.PremiumElementDetailsDAO(context).GetByAdditionalElement(additionalElementId)
    End Function

    Public Function GetLatestElementIdInProject(ByVal projectId As Integer) As Integer
        Dim elementList As List(Of Element) = GetElementsByProject(projectId)
        Dim latestElementId As Integer

        If elementList.Count > 0 Then
            latestElementId = elementList(elementList.Count - 1).ID
        End If

        Return latestElementId
    End Function

    Public Function GetElementTotalEstimationTime(ByVal projectId As Integer) As Decimal
        Dim elementList As List(Of Element) = GetElementsByProject(projectId)
        Dim runningHourCount As Integer

        For Each element In elementList
            ' Get the additional element details
            'Dim elementAdditionalInfo As ElementAdditionalDetails = GetElementAdditionalInfoByElement(element.ID)
            'Dim artworkTime As Integer

            'If Integer.TryParse(elementAdditionalInfo.ArtworkTime, artworkTime) Then
            '    runningHourCount += artworkTime
            'End If

            runningHourCount += element.SubclassType.TotalTimeInHours
        Next

        Return runningHourCount
    End Function

#End Region

#Region "Additional Elements"

    Public Function GetAdditionalElement(ByVal additionalElementId As Integer) As AdditionalElement
        Return DAOGetter.AdditionalElementDAO(context).GetByID(additionalElementId)
    End Function

    Public Function GetAdditionalElementsByProject(ByVal projectId As Integer) As List(Of AdditionalElement)
        Return DAOGetter.AdditionalElementDAO(context).GetAdditionalElementsByProject(projectId, True)
    End Function

#End Region

#Region "Element Artwork Info"

    Public Function GetElementArtworkInfoByElement(ByVal elementId As Integer) As ElementArtworkDetails
        Return DAOGetter.ElementArtworkDetailsDAO(context).GetByElement(elementId)
    End Function

    Public Function GetElementArtworkInfo(ByVal elementArtworkInfoId As Integer) As ElementArtworkDetails
        Return DAOGetter.ElementArtworkDetailsDAO(context).GetByID(elementArtworkInfoId)
    End Function

#End Region

#Region "Element Kitting Info"

    Public Function GetElementKittingInfo(ByVal elementKittingInfoId As Integer) As ElementKittingDetails
        Return DAOGetter.ElementKittingDetailsDAO(context).GetByID(elementKittingInfoId)
    End Function

    Public Function GetElementKittingDetailsByElement(ByVal elementId As Integer) As ElementKittingDetails
        Return DAOGetter.ElementKittingDetailsDAO(context).GetByElement(elementId)
    End Function

#End Region

#End Region

#Region "Insertion / Updating / Removal"

#Region "Elements"

    Public Sub SaveElement(ByVal saveElement As Element, ByVal saveSchemaData As List(Of SchemaData),
                           ByVal userId As Integer, Optional ByVal newLine As String = "<br />",
                           Optional ByVal saveAdditionalDetails As ElementAdditionalDetails = Nothing)

        Dim auditMessage As String = ""
        Dim isNewElement As Boolean = False
        Dim auditChangeTypeId As Integer

        If saveElement.ID = 0 Then
            isNewElement = True
            auditChangeTypeId = AppSettingsGet.AddAuditChangeTypeID
            saveElement.ItemOrder = GetElementItemOrder(saveElement.Project.ID)
            AddNewElement(saveElement)
        Else
            auditChangeTypeId = AppSettingsGet.EditAuditChangeTypeID
            auditMessage = GetModifiedElementAuditLog(saveElement,
                                                          saveAdditionalDetails,
                                                          saveSchemaData,
                                                          newLine)
            UpdateElement(saveElement)
        End If

        For Each elementSchemaData As SchemaData In saveSchemaData
            elementSchemaData.ParentID = saveElement.ID
            SchemaManager.SaveSchemaData(elementSchemaData)
        Next

        If saveAdditionalDetails IsNot Nothing Then
            SaveElementAdditionalInfo(saveAdditionalDetails)
        End If

        If isNewElement Then
            auditMessage = GetNewElementAuditLog(saveElement, saveAdditionalDetails, saveSchemaData)
        End If

        AuditTrailManager.PostAudit(auditMessage, userId, saveElement.Project.ID,
                                    auditChangeTypeId, AppSettingsGet.ElementCoreDetailsSectionID)


    End Sub

    Public Sub AddNewElement(ByVal element As Element)
        element.Created = Date.Today
        element.Modified = Date.Today
        DAOGetter.ElementDAO(context).Insert(element)
    End Sub

    Public Sub UpdateElement(ByVal element As Element)
        element.Modified = Date.Today
        DAOGetter.ElementDAO(context).Update(element)
    End Sub

    Public Sub SetElementActivity(ByVal saveElement As Element, ByVal stopElement As Boolean, ByVal userId As Integer)

        saveElement.ElementStopped = stopElement

        Dim elementTokens As List(Of Token)
        Dim auditLog As String

        If stopElement = True Then
            ' Stopping element
            auditLog = String.Format("{0} has now been stopped", saveElement.Name)
            elementTokens = WorkflowManager.GetTokenByContextAndEntity(AppSettingsGet.EntityElementId, saveElement.ID, AppSettingsGet.TokenStatusFree)
            KitManager.RemoveItemByTypeFromKits(saveElement.ID, AppSettingsGet.ElementTypeID)
        Else
            ' Starting element
            auditLog = String.Format("{0} has now been started", saveElement.Name)
            elementTokens = WorkflowManager.GetTokenByContextAndEntity(AppSettingsGet.EntityElementId, saveElement.ID, AppSettingsGet.TokenStatusCancelled)
        End If

        ' We need to cancel all the tokens to do with this element that is currently in the workflow

        If elementTokens.Count > 0 Then
            For Each token In elementTokens
                If stopElement = True Then
                    WorkflowManager.ChangeTokenStatus(token, AppSettingsGet.TokenStatusCancelled)
                Else
                    WorkflowManager.ChangeTokenStatus(token, AppSettingsGet.TokenStatusFree)
                End If
            Next
        Else
            ' No tokens have been created BUT has the workflow started for elements? if so then we need to create the tokens for it

            If WorkflowManager.GetTokenByContextAndEntity(AppSettingsGet.EntityElementId, saveElement.ID, AppSettingsGet.TokenStatusConsumed).Count = 0 Then
                ' Check to see if the element has finished or not if it has then we don't want to create a new token

                If WorkflowManager.HasElementWorkflowStarted(saveElement.Project.ID) = True Then
                    Dim elementWorkflowToken As New Token

                    elementWorkflowToken.Place = WorkflowManager.GetPlace(AppSettingsGet.ElementPlaceStartID)
                    elementWorkflowToken.Project = saveElement.Project
                    elementWorkflowToken.AptUser = UserManager.GetUser(userId)
                    elementWorkflowToken.Comment = "Element Started"
                    elementWorkflowToken.ContextParentID = saveElement.ID
                    elementWorkflowToken.ContextEntity = GetEntityById(AppSettingsGet.EntityElementId)

                    WorkflowManager.CreateNewToken(elementWorkflowToken)
                End If
            End If

        End If

        UpdateElement(saveElement)

        AuditTrailManager.PostAudit(auditLog, userId, saveElement.Project.ID, AppSettingsGet.EditAuditChangeTypeID, AppSettingsGet.ElementCoreDetailsSectionID)

    End Sub

    Public Function HasElementWorkflowFinished(ByVal elementId As Integer) As Boolean

        If WorkflowManager.GetTokenByContextAndEntity(AppSettingsGet.EntityElementId, elementId, AppSettingsGet.TokenStatusConsumed).Count > 0 Then
            If WorkflowManager.GetTokenByContextAndEntity(AppSettingsGet.EntityElementId, elementId, AppSettingsGet.TokenStatusFree).Count = 0 Then
                Return True
            End If
        End If

        Return False

    End Function

    Private Function GetElementItemOrder(ByVal projectId As Integer) As Integer
        Return GetAllElementsByProject(projectId).Count + 1
    End Function

#End Region

#Region "AdditionalElements"

    Public Sub SaveAdditionalElement(ByVal saveAdditionalElement As AdditionalElement, ByVal userId As Integer)
        Dim auditLog As String = ""
        Dim changeType As Integer

        If saveAdditionalElement.ID = 0 Then
            changeType = AppSettingsGet.AddAuditChangeTypeID
            auditLog = GetNewAdditionalElementAuditLog(saveAdditionalElement)
            saveAdditionalElement.Active = True
            DAOGetter.AdditionalElementDAO(context).Insert(saveAdditionalElement)
        Else
            changeType = AppSettingsGet.EditAuditChangeTypeID
            auditLog = GetModifiedAdditionalElementAuditLog(saveAdditionalElement)
            DAOGetter.AdditionalElementDAO(context).Update(saveAdditionalElement)
        End If

        AuditTrailManager.PostAudit(auditLog, userId, saveAdditionalElement.Project.ID, changeType, AppSettingsGet.AdditionalElementSectionID)
    End Sub

    Public Sub SetAdditionalElementInActive(ByVal additionalElementId As Integer, ByVal userId As Integer)
        Dim additionalElement As AdditionalElement = DAOGetter.AdditionalElementDAO(context).GetByID(additionalElementId)
        additionalElement.Active = False

        KitManager.RemoveItemByTypeFromKits(additionalElementId, AppSettingsGet.PremiumElementTypeID)

        AuditTrailManager.PostAudit(String.Format("Premium product {0} has been deleted", additionalElement.Name), userId, additionalElement.Project.ID,
                                    AppSettingsGet.DeleteAuditChangeTypeID, AppSettingsGet.AdditionalElementSectionID)

        DAOGetter.AdditionalElementDAO(context).Update(additionalElement)
    End Sub

    Public Function DoesAdditionalElementBelongToProject(ByVal additionalElementId As Integer, ByVal projectId As Integer) As Boolean

        Dim tmpAdditionalelement As AdditionalElement = ElementManager.GetAdditionalElement(additionalElementId)

        ' Need to do a check to see if this additional element belongs to this project
        If tmpAdditionalelement IsNot Nothing Then
            If tmpAdditionalelement.Project.ID = projectId Then
                Return True
            End If
        End If

        Return False

    End Function

#End Region

#Region "ElementAdditionalInfo"

    Public Sub SaveElementAdditionalInfo(ByVal elementAdditionalInfo As ElementAdditionalDetails)

        If elementAdditionalInfo.ID = 0 Then
            AddNewElementAdditionalInfo(elementAdditionalInfo)
        Else
            UpdateElementAdditionalInfo(elementAdditionalInfo)
        End If

    End Sub

    Public Sub AddNewElementAdditionalInfo(ByVal elementAdditionalInfo As ElementAdditionalDetails)
        DAOGetter.ElementAdditionalDetailsDAO(context).Insert(elementAdditionalInfo)
    End Sub

    Public Sub UpdateElementAdditionalInfo(ByVal elementAdditionalInfo As ElementAdditionalDetails)
        DAOGetter.ElementAdditionalDetailsDAO(context).Update(elementAdditionalInfo)
    End Sub

#End Region

#Region "ElementArtworkInfo"

    Public Sub SaveElementArtworkInfo(ByVal saveElementArtworkInfo As ElementArtworkDetails, ByVal userId As Integer)

        Dim changeType As Integer
        Dim auditLog As String = ""

        If saveElementArtworkInfo.ID = 0 Then
            auditLog = GetNewElementArtworkInfoAudit(saveElementArtworkInfo.Element.Name)
            changeType = AppSettingsGet.AddAuditChangeTypeID
            DAOGetter.ElementArtworkDetailsDAO(context).Insert(saveElementArtworkInfo)
        Else
            auditLog = GetModifiedElementArtworkInfoAudit(GetCopyArtworkInfo(saveElementArtworkInfo.ID), saveElementArtworkInfo)
            changeType = AppSettingsGet.EditAuditChangeTypeID
            DAOGetter.ElementArtworkDetailsDAO(context).Update(saveElementArtworkInfo)
        End If

        AuditTrailManager.PostAudit(auditLog, userId,
                                    saveElementArtworkInfo.Element.Project.ID, _
                                    changeType, AppSettingsGet.ElementCoreDetailsSectionID)

    End Sub

#End Region

#Region "ElementKittingInfo"

    Public Sub SaveElementKittingInfo(ByVal elementKittingInfo As ElementKittingDetails, ByVal userId As Integer)

        Dim changeType As Integer
        Dim auditLog As String = ""

        If elementKittingInfo.ID = 0 Then
            auditLog = GetNewElementKittingInfoAudit(elementKittingInfo.Element.Name)
            changeType = AppSettingsGet.AddAuditChangeTypeID
            DAOGetter.ElementKittingDetailsDAO(context).Insert(elementKittingInfo)
        Else

            auditLog = GetModifiedElementKittingInfoAudit(elementKittingInfo, GetCopyKittingInfo(elementKittingInfo.ID))
            changeType = AppSettingsGet.EditAuditChangeTypeID
            DAOGetter.ElementKittingDetailsDAO(context).Update(elementKittingInfo)
        End If

        AuditTrailManager.PostAudit(auditLog, userId,
                                    elementKittingInfo.Element.Project.ID, _
                                    changeType, AppSettingsGet.ElementCoreDetailsSectionID)

    End Sub

#End Region

#Region "PremiumElementDetails"

    Public Sub SavePremiumElementDetails(ByVal premiumElementDetails As PremiumElementDetails, ByVal userId As Integer)

        Dim auditLog As String = ""
        Dim changeType As Integer

        If premiumElementDetails.ID = 0 Then
            auditLog = GetNewPremiumElementDetailsAuditLog(premiumElementDetails)
            changeType = AppSettingsGet.AddAuditChangeTypeID
            DAOGetter.PremiumElementDetailsDAO(Context).Insert(premiumElementDetails)
        Else
            auditLog = GetModifiedPremiumElementDetailsAuditLog(premiumElementDetails)
            changeType = AppSettingsGet.EditAuditChangeTypeID
            DAOGetter.PremiumElementDetailsDAO(Context).Update(premiumElementDetails)
        End If

        AuditTrailManager.PostAudit(auditLog, userId,
                                    premiumElementDetails.AdditionalElement.Project.ID, _
                                    changeType, AppSettingsGet.AdditionalElementSectionID)

    End Sub

#End Region

#End Region

#Region "Clones"

    Private Function GetCopyElement(ByVal elementId As Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ElementDAO(staticContext).GetByID(elementId)
    End Function

    Private Function GetCopyElementAdditionalDetails(ByVal additionalElementDetailsId As Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ElementAdditionalDetailsDAO(staticContext).GetByID(additionalElementDetailsId)
    End Function

    Private Function GetCopyAdditionalElement(ByVal additionalElementId As Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.AdditionalElementDAO(staticContext).GetByID(additionalElementId)
    End Function

    Private Function GetCopyPremiumElementDetails(ByVal premElementDetailsId As Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.PremiumElementDetailsDAO(staticContext).GetByID(premElementDetailsId)
    End Function

    Private Function GetCopyKittingInfo(ByVal kittingInfoId As Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ElementKittingDetailsDAO(staticContext).GetByID(kittingInfoId)
    End Function

    Private Function GetCopyArtworkInfo(ByVal artworkInfoId As Integer)
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ElementArtworkDetailsDAO(staticContext).GetByID(artworkInfoId)
    End Function

#End Region

#Region "Audit Functionality"

#Region "Element"

    Private Function GetModifiedElementSummary(ByVal changedElement As Element,
                                  Optional ByVal newLine As String = "<br>") As String

        Dim oldElement As Element = GetCopyElement(changedElement.ID)
        Dim summary As String = ""

        If oldElement.Name <> changedElement.Name Then
            summary += String.Format("Element Name has been changed from {0} to {1}", oldElement.Name, changedElement.Name)
            summary += newLine
        End If

        If oldElement.Description <> changedElement.Description Then
            summary += "Element Description has been changed"
            summary += newLine
        End If

        If oldElement.TradeListNode.Name <> changedElement.TradeListNode.Name Then
            summary += String.Format("Trade has been changed from {0} to {1}", oldElement.TradeListNode.Name, changedElement.TradeListNode.Name)
            summary += newLine
        End If

        If oldElement.TCListNode.Name <> changedElement.TCListNode.Name Then
            summary += String.Format("T&Cs has been changed from {0} to {1}", oldElement.TCListNode.Name, changedElement.TCListNode.Name)
            summary += newLine
        End If

        If oldElement.BrandListNode.Name <> changedElement.BrandListNode.Name Then
            summary += String.Format("Business area has been changed from {0} to {1}", oldElement.BrandListNode.Name, changedElement.BrandListNode.Name)
            summary += newLine
        End If

        If oldElement.AmendTime <> changedElement.AmendTime Then
            summary += String.Format("Amend time has been changed from {0} to {1}", oldElement.AmendTime, changedElement.AmendTime)
            summary += newLine
        End If

        ' What should this be? seems pretty pointless if it's the type since we can get this from the subtype
        'If oldelement.SchemaType.Name <> changedElement.SchemaType.Name Then
        '    summary += String.Format("Type has been changed from {0} to {1}", oldelement.SchemaType.Name, changedElement.SchemaType.Name)
        '    summary += newLine
        'End If

        If oldElement.SubclassType.Type.Name <> changedElement.SubclassType.Type.Name Then
            summary += String.Format("Type has been changed from {0} to {1}", oldElement.SubclassType.Type.Name, changedElement.SubclassType.Type.Name)
            summary += newLine
        End If

        If oldElement.SubclassType.Name <> changedElement.SubclassType.Name Then
            summary += String.Format("Subclass Type has been changed from {0} to {1}", oldElement.SubclassType.Name, changedElement.SubclassType.Name)
            summary += newLine
        End If

        If oldElement.ArtworkDeliveryDate.HasValue = False Then
            If changedElement.ArtworkDeliveryDate.HasValue Then
                summary += String.Format("Artwork Required Date has been set to {0}", changedElement.ArtworkDeliveryDate.Value.ToString("dd/MM/yyyy"))
                summary += newLine
            End If
        Else
            If oldElement.ArtworkDeliveryDate.Value <> changedElement.ArtworkDeliveryDate.Value Then
                summary += String.Format("Artwork Required Date has been changed from {0} to {1}", oldElement.ArtworkDeliveryDate.Value.ToString("dd/MM/yyyy"), changedElement.ArtworkDeliveryDate.Value.ToString("dd/MM/yyyy"))
                summary += newLine
            End If
        End If

        Return summary

    End Function

    Private Function GetModifiedAddDetailsSummary(ByVal newAdditionalInfo As ElementAdditionalDetails,
                                                  ByVal newLine As String) As String

        Dim oldAdditionalInfo As ElementAdditionalDetails = GetCopyElementAdditionalDetails(newAdditionalInfo.ID)
        Dim summary As String = ""

        If oldAdditionalInfo.ArtworkCost <> newAdditionalInfo.ArtworkCost Then
            summary += String.Format("Artwork Cost has been changed from {0} to {1}", oldAdditionalInfo.ArtworkCost, newAdditionalInfo.ArtworkCost)
            summary += newLine
        End If

        If oldAdditionalInfo.ArtworkTime <> newAdditionalInfo.ArtworkTime Then
            summary += String.Format("Artwork Time has been changed from {0} to {1}", oldAdditionalInfo.ArtworkTime, newAdditionalInfo.ArtworkTime)
            summary += newLine
        End If

        If oldAdditionalInfo.CostPerItem <> newAdditionalInfo.CostPerItem Then
            summary += String.Format("Cost Per Item has been changed from {0} to {1}", oldAdditionalInfo.CostPerItem, newAdditionalInfo.CostPerItem)
            summary += newLine
        End If

        If oldAdditionalInfo.PrintCost <> newAdditionalInfo.PrintCost Then
            summary += String.Format("Print Cost has been changed from {0} to {1}", oldAdditionalInfo.PrintCost, newAdditionalInfo.PrintCost)
            summary += newLine
        End If

        If oldAdditionalInfo.PrintLeadTimes <> newAdditionalInfo.PrintLeadTimes Then
            summary += String.Format("Print Lead Times has been changed from {0} to {1}", oldAdditionalInfo.PrintLeadTimes, newAdditionalInfo.PrintLeadTimes)
            summary += newLine
        End If

        If oldAdditionalInfo.Quantity <> newAdditionalInfo.Quantity Then
            summary += String.Format("Quantity has been changed from {0} to {1}", oldAdditionalInfo.Quantity, newAdditionalInfo.Quantity)
            summary += newLine
        End If

        Return summary

    End Function

    Private Function GetModifiedSchemaData(ByVal elementId As Integer,
                                            ByVal newSchemaDataList As List(Of SchemaData),
                                            ByVal newLine As String)

        Dim oldSchemaDataList As List(Of SchemaData) = SchemaManager.GetCopySchemaDataByElement(elementId)
        Dim summary As String = ""

        If oldSchemaDataList.Count <> newSchemaDataList.Count Then
            ' Then the subclass type has been changed so we just list out the values that it has rather than give a comparison
            For Each schemaValue As SchemaData In newSchemaDataList
                Dim newNode As ListNode = ListManager.GetListNode(schemaValue.SchemaElementValue)
                summary += String.Format("The property {0} has been set to {1}", schemaValue.SchemaDefinition.Name, newNode.Name)
                summary += newLine
            Next
        Else
            ' Some values of the subclass type have been changed
            For i As Integer = 0 To oldSchemaDataList.Count - 1

                Dim oldSchemaData As SchemaData = oldSchemaDataList(i)
                Dim newSchemaData As SchemaData = newSchemaDataList(i)

                If oldSchemaData.SchemaElementValue <> newSchemaData.SchemaElementValue Then

                    Dim oldListNode As ListNode = ListManager.GetListNode(oldSchemaData.SchemaElementValue)
                    Dim newListNode As ListNode = ListManager.GetListNode(newSchemaData.SchemaElementValue)

                    summary += String.Format("The property {0} has changed from {1} to {2}",
                                                oldSchemaData.SchemaDefinition.Name,
                                                oldListNode.Name,
                                                newListNode.Name)
                    summary += newLine
                End If

            Next
        End If

        Return summary

    End Function

    Private Function GetModifiedElementAuditLog(ByVal changedElement As Element,
                                                    ByVal changedAdditionalDetails As ElementAdditionalDetails,
                                                    ByVal changedSchemaDataList As List(Of SchemaData),
                                                    ByVal newLine As String) As String

        Dim summary As String = ""
        summary += GetModifiedElementSummary(changedElement, newLine)
        summary += GetModifiedAddDetailsSummary(changedAdditionalDetails, newLine)
        summary += GetModifiedSchemaData(changedElement.ID, changedSchemaDataList, newLine)

        Return summary

    End Function

    Private Function GetNewElementAuditLog(ByVal newElement As Element,
                                          ByVal additionalDetails As ElementAdditionalDetails,
                                          ByVal saveElementSchemaData As List(Of SchemaData)) As String

        Dim summary As String = ""
        summary += String.Format("A new element {0} has been added to the project", newElement.Name)

        Return summary

    End Function

#End Region

#Region "Additional Element"

    Private Function GetModifiedAdditionalElementAuditLog(ByVal modifiedAdditionalElement As AdditionalElement, Optional ByVal newLine As String = "<br />") As String

        Dim summary As String = ""
        Dim oldAdditionalElement As AdditionalElement = GetCopyAdditionalElement(modifiedAdditionalElement.ID)

        If oldAdditionalElement.Name <> modifiedAdditionalElement.Name Then
            summary += String.Format("Premium element name has been changed from {0} to {1}", oldAdditionalElement.Name, modifiedAdditionalElement.Name)
            summary += newLine
        End If

        If oldAdditionalElement.Description <> modifiedAdditionalElement.Description Then
            summary += String.Format("The description for {0} has been changed", oldAdditionalElement.Name)
            summary += newLine
        End If

        If oldAdditionalElement.Cost <> modifiedAdditionalElement.Cost Then
            summary += String.Format("The unit cost for {0} has been changed from {1} to {2}",
                                     oldAdditionalElement.Name, oldAdditionalElement.Cost, modifiedAdditionalElement.Cost)
            summary += newLine
        End If

        Return summary

    End Function

    Private Function GetNewAdditionalElementAuditLog(ByVal newAdditionalElement As AdditionalElement, Optional ByVal newline As String = "<br />") As String

        Dim summary As String = ""
        summary += String.Format("A new premium product {0} has been added", newAdditionalElement.Name)

        Return summary

    End Function

    Private Function GetNewPremiumElementDetailsAuditLog(ByVal savePremElementDetails As PremiumElementDetails) As String

        Dim summary As String = ""
        summary += String.Format("Premium Element details have been submitted for {0}", savePremElementDetails.AdditionalElement.Name)

        Return summary

    End Function

    Private Function GetModifiedPremiumElementDetailsAuditLog(ByVal savePremElementDetails As PremiumElementDetails, Optional ByVal newLine As String = "<br />") As String

        Dim oldDetails As PremiumElementDetails = GetCopyPremiumElementDetails(savePremElementDetails.ID)
        Dim summary As String = ""
        summary += String.Format("Premium Element details have been altered for {0}", savePremElementDetails.AdditionalElement.Name)
        summary += newLine

        'If savePremElementDetails.OutlineBrief <> oldDetails.OutlineBrief Then
        '    summary += String.Format("Outline Brief has been altered")
        '    summary += newLine
        'End If

        'If savePremElementDetails.DoesItemExistOnBBC <> oldDetails.DoesItemExistOnBBC Then
        '    summary += String.Format("Does Item Exist on BBC has been changed from {0} to {1}",
        '                             oldDetails.DoesItemExistOnBBC,
        '                             savePremElementDetails.DoesItemExistOnBBC)
        '    summary += newLine
        'End If

        If savePremElementDetails.StockCode <> oldDetails.StockCode Then
            summary += String.Format("Stock Code has been changed from {0} to {1}",
                                     oldDetails.StockCode,
                                     savePremElementDetails.StockCode)
            summary += newLine
        End If

        If savePremElementDetails.BudgetPerItem <> oldDetails.BudgetPerItem Then
            summary += String.Format("Target pricing has been changed from {0} to {1}",
                                     oldDetails.BudgetPerItem,
                                     savePremElementDetails.BudgetPerItem)
            summary += newLine
        End If

        If savePremElementDetails.QuantityBreaks <> oldDetails.QuantityBreaks Then
            summary += String.Format("Quantity Breaks has been changed from {0} to {1}",
                                     oldDetails.QuantityBreaks,
                                     savePremElementDetails.QuantityBreaks)
            summary += newLine
        End If


        If savePremElementDetails.Colours <> oldDetails.Colours Then
            summary += String.Format("Colours has been changed from {0} to {1}",
                                     oldDetails.Colours,
                                     savePremElementDetails.Colours)
            summary += newLine
        End If

        If savePremElementDetails.Brand <> oldDetails.Brand Then
            summary += String.Format("Brand has been changed from {0} to {1}",
                                     oldDetails.Brand,
                                     savePremElementDetails.Brand)
            summary += newLine
        End If

        If savePremElementDetails.BrandPosition <> oldDetails.BrandPosition Then
            summary += String.Format("Brand Position has been changed from {0} to {1}",
                                     oldDetails.BrandPosition,
                                     savePremElementDetails.BrandPosition)
            summary += newLine
        End If

        If savePremElementDetails.NumberOfBranding <> oldDetails.NumberOfBranding Then
            summary += String.Format("Number of Branding has been changed from {0} to {1}",
                                     oldDetails.NumberOfBranding,
                                     savePremElementDetails.NumberOfBranding)
            summary += newLine
        End If

        If savePremElementDetails.SizeOfItem <> oldDetails.SizeOfItem Then
            summary += String.Format("Size of Item has been changed from {0} to {1}",
                                     oldDetails.SizeOfItem,
                                     savePremElementDetails.SizeOfItem)
            summary += newLine
        End If

        If savePremElementDetails.Textile <> oldDetails.Textile Then
            summary += String.Format("Textile has been changed from {0} to {1}",
                                     oldDetails.Textile,
                                     savePremElementDetails.Textile)
            summary += newLine
        End If

        If savePremElementDetails.PurposeOfItem <> oldDetails.PurposeOfItem Then
            summary += String.Format("Purpose of Item has been changed from {0} to {1}",
                                     oldDetails.PurposeOfItem,
                                     savePremElementDetails.PurposeOfItem)
            summary += newLine
        End If

        If savePremElementDetails.ItemPackaging <> oldDetails.ItemPackaging Then
            summary += String.Format("Item packaging has been changed from {0} to {1}",
                                     oldDetails.ItemPackaging,
                                     savePremElementDetails.ItemPackaging)
            summary += newLine
        End If

        If savePremElementDetails.WeightRestriction <> oldDetails.WeightRestriction Then
            summary += String.Format("Weight Restriction has been changed from {0} to {1}",
                                     oldDetails.WeightRestriction,
                                     savePremElementDetails.WeightRestriction)
            summary += newLine
        End If

        If savePremElementDetails.SampleRequired <> oldDetails.SampleRequired Then
            summary += String.Format("Sample Required has been changed from {0} to {1}",
                                     oldDetails.SampleRequired,
                                     savePremElementDetails.SampleRequired)
            summary += newLine
        End If

        'If savePremElementDetails.DesignRequired <> oldDetails.DesignRequired Then
        '    summary += String.Format("Design Required has been changed from {0} to {1}",
        '                             oldDetails.DesignRequired,
        '                             savePremElementDetails.DesignRequired)
        '    summary += newLine
        'End If

        If savePremElementDetails.HasBDDesignedItem <> oldDetails.HasBDDesignedItem Then
            summary += String.Format("Has BD Designed Item has been changed from {0} to {1}",
                                     oldDetails.HasBDDesignedItem,
                                     savePremElementDetails.HasBDDesignedItem)
            summary += newLine
        End If

        If savePremElementDetails.SimilarProducts <> oldDetails.SimilarProducts Then
            summary += String.Format("Similar Products has been changed from {0} to {1}",
                                     oldDetails.SimilarProducts,
                                     savePremElementDetails.SimilarProducts)
            summary += newLine
        End If

        If savePremElementDetails.QuoteCost <> oldDetails.QuoteCost Then
            summary += String.Format("Quote Costs have been changed from {0} to {1}",
                                     oldDetails.QuoteCost,
                                     savePremElementDetails.QuoteCost)
            summary += newLine
        End If

        If savePremElementDetails.Supplier <> oldDetails.Supplier Then
            summary += String.Format("Supplier has been changed from {0} to {1}",
                                     oldDetails.Supplier,
                                     savePremElementDetails.Supplier)
            summary += newLine
        End If

        If savePremElementDetails.EtaDate <> oldDetails.EtaDate Then
            summary += String.Format("ETA Date has been changed from {0} to {1}",
                                     oldDetails.EtaDate.ToString("dd/MM/yyyy"),
                                     savePremElementDetails.EtaDate.ToString("dd/MM/yyyy"))
            summary += newLine
        End If

        If savePremElementDetails.ImageUploaded <> oldDetails.ImageUploaded Then
            summary += String.Format("Image Uploaded has been changed from {0} to {1}",
                                     If(oldDetails.ImageUploaded = True, "Yes", "No"),
                                     If(savePremElementDetails.ImageUploaded = True, "Yes", "No"))
            summary += newLine
        End If

        If savePremElementDetails.FinalRequirement <> oldDetails.FinalRequirement Then
            summary += String.Format("Final Requirements has been changed from {0} to {1}",
                                     If(oldDetails.ImageUploaded = True, "Yes", "No"),
                                     If(savePremElementDetails.ImageUploaded = True, "Yes", "No"))
            summary += newLine
        End If

        If savePremElementDetails.BriefSubmittedDate <> oldDetails.BriefSubmittedDate Then
            summary += String.Format("Brief Submitted Date has been changed from {0} to {1}",
                                      FormatHelper.FormatDateWithoutTime(oldDetails.BriefSubmittedDate),
                                      FormatHelper.FormatDateWithoutTime(savePremElementDetails.BriefSubmittedDate))
            summary += newLine
        End If

        If savePremElementDetails.ProposalRequiredDate <> oldDetails.ProposalRequiredDate Then
            summary += String.Format("Proposal Required Date has been changed from {0} to {1}",
                                      FormatHelper.FormatDateWithoutTime(oldDetails.ProposalRequiredDate),
                                      FormatHelper.FormatDateWithoutTime(savePremElementDetails.ProposalRequiredDate))
            summary += newLine
        End If

        If savePremElementDetails.QuoteProvidedDate <> oldDetails.QuoteProvidedDate Then
            summary += String.Format("Quote Provided Date has been changed from {0} to {1}",
                                      FormatHelper.FormatDateWithoutTime(oldDetails.QuoteProvidedDate),
                                      FormatHelper.FormatDateWithoutTime(savePremElementDetails.QuoteProvidedDate))
            summary += newLine
        End If

        If savePremElementDetails.ArtworkAvailableDate <> oldDetails.ArtworkAvailableDate Then
            summary += String.Format("Artwork Available Date has been changed from {0} to {1}",
                                      FormatHelper.FormatDateWithoutTime(oldDetails.ArtworkAvailableDate),
                                      FormatHelper.FormatDateWithoutTime(savePremElementDetails.ArtworkAvailableDate))
            summary += newLine
        End If

        If savePremElementDetails.PODate <> oldDetails.PODate Then
            summary += String.Format("PO Date has been changed from {0} to {1}",
                                      FormatHelper.FormatDateWithoutTime(oldDetails.PODate),
                                      FormatHelper.FormatDateWithoutTime(savePremElementDetails.PODate))
            summary += newLine
        End If

        If savePremElementDetails.DeliveryDate <> oldDetails.DeliveryDate Then
            summary += String.Format("Delivery Date has been changed from {0} to {1}",
                                      FormatHelper.FormatDateWithoutTime(oldDetails.DeliveryDate),
                                      FormatHelper.FormatDateWithoutTime(savePremElementDetails.DeliveryDate))
            summary += newLine
        End If

        If savePremElementDetails.DeliveryAddress <> oldDetails.DeliveryAddress Then
            summary += String.Format("Delivery Address has been changed from {0} to {1}",
                                      oldDetails.DeliveryAddress,
                                      savePremElementDetails.DeliveryAddress)
            summary += newLine
        End If

        Return summary

    End Function

#End Region

#Region "Element Kitting Info"

    Private Function GetNewElementKittingInfoAudit(ByVal elementName As String) As String
        Dim summary As String = ""
        summary += String.Format("Kitting information has been submitted for {0}", elementName)

        Return summary
    End Function

    Private Function GetModifiedElementKittingInfoAudit(ByVal modifiedKitInfo As ElementKittingDetails,
                                                        ByVal oldKitInfo As ElementKittingDetails,
                                                        Optional ByVal newLine As String = "<br />") As String
        Dim summary As String = ""
        summary += String.Format("Kitting information has been modified for {0}", modifiedKitInfo.Element.Name)
        summary += newLine

        If modifiedKitInfo.CostPerItem <> oldKitInfo.CostPerItem Then
            summary += String.Format("Cost per item has been changed from {0} to {1}", oldKitInfo.CostPerItem, modifiedKitInfo.CostPerItem)
        End If

        summary += modDates.AuditNullableDateCheck(oldKitInfo.ExpiryDate, modifiedKitInfo.ExpiryDate, "Expiry Date")

        If oldKitInfo.PONumber <> modifiedKitInfo.PONumber Then
            summary += String.Format("PO Number has been changed from {0} to {1}", oldKitInfo.PONumber, modifiedKitInfo.PONumber)
            summary += newLine
        End If

        If oldKitInfo.Existing <> modifiedKitInfo.Existing Then
            If modifiedKitInfo.Existing = True Then
                summary += String.Format("Existing has been set to Existing")
            Else
                summary += String.Format("Existing has been set to New")
            End If
            summary += newLine
        End If

        If oldKitInfo.Supplier <> modifiedKitInfo.Supplier Then
            summary += String.Format("Print Supplier has been changed from {0} to {1}", oldKitInfo.Supplier, modifiedKitInfo.Supplier)
            summary += newLine
        End If

        summary += modDates.AuditNullableDateCheck(oldKitInfo.DueDateIntoMDA, modifiedKitInfo.DueDateIntoMDA, "Due Date into MDA")

        Return summary
    End Function

#End Region

#Region "Element Artwork Info"

    Private Function GetNewElementArtworkInfoAudit(ByVal elementName As String) As String
        Dim summary As String = ""
        summary += String.Format("Artwork details for {0} have been added", elementName)

        Return summary
    End Function

    Private Function GetModifiedElementArtworkInfoAudit(ByVal oldArtworkInfo As ElementArtworkDetails,
                                                   ByVal modifiedArtworkInfo As ElementArtworkDetails,
                                                   Optional ByVal newLine As String = "<br />") As String

        Dim summary As String = ""
        summary += String.Format("Kitting information has been modified for {0}", modifiedArtworkInfo.Element.Name)
        summary += newLine

        If modifiedArtworkInfo.NoOfColours <> oldArtworkInfo.NoOfColours Then
            summary += String.Format("No of colours has been changed from {0} to {1}", oldArtworkInfo.NoOfColours, modifiedArtworkInfo.NoOfColours)
            summary += newLine
        End If

        If modifiedArtworkInfo.FinishedSize <> oldArtworkInfo.FinishedSize Then
            summary += String.Format("Finished Size has been changed from {0} to {1}", oldArtworkInfo.FinishedSize, modifiedArtworkInfo.FinishedSize)
            summary += newLine
        End If

        If modifiedArtworkInfo.Material <> oldArtworkInfo.Material Then
            summary += String.Format("Material has been changed from {0} to {1}", oldArtworkInfo.Material, modifiedArtworkInfo.Material)
            summary += newLine
        End If

        If modifiedArtworkInfo.Finishing <> oldArtworkInfo.Finishing Then
            summary += String.Format("Finishing has been changed from {0} to {1}", oldArtworkInfo.Finishing, modifiedArtworkInfo.Finishing)
            summary += newLine
        End If

        If modifiedArtworkInfo.NoOfDelAdds <> oldArtworkInfo.NoOfDelAdds Then
            summary += String.Format("No of Delivery Adds has been changed from {0} to {1}", oldArtworkInfo.NoOfDelAdds, modifiedArtworkInfo.NoOfDelAdds)
            summary += newLine
        End If

        If modifiedArtworkInfo.DeliveryDetails <> oldArtworkInfo.DeliveryDetails Then
            summary += String.Format("No of Delivery Adds has been changed from {0} to {1}", oldArtworkInfo.DeliveryDetails, modifiedArtworkInfo.DeliveryDetails)
            summary += newLine
        End If

        If modifiedArtworkInfo.PackSize <> oldArtworkInfo.PackSize Then
            summary += String.Format("Pack Size has been changed from {0} to {1}", oldArtworkInfo.PackSize, modifiedArtworkInfo.PackSize)
            summary += newLine
        End If

        Return summary

    End Function

#End Region

#End Region

End Module
