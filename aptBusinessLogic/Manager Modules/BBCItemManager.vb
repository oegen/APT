'----------------------------------------------------------------------------------------------
' Filename    : BBCItemManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module BBCItemManager

#Region "Properties"

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

    Public Enum BBCItemSearchCriteria
        PART_NUMBER = 0
        DESCRIPTION = 2
    End Enum

#End Region

#Region "Get Variations"


    Public Function GetNewBBCItem(ByVal newBBCItemId As Integer) As NewBBCItem
        Return DAOGetter.NewBBCItemDAO(Context).GetByID(newBBCItemId)
    End Function

    Public Function GetAllNewBBCItem() As List(Of NewBBCItem)
        Return DAOGetter.NewBBCItemDAO(Context).GetAll()
    End Function

    Public Function GetAllActiveBBCItem() As List(Of NewBBCItem)
        Return (From o In Context.NewBBCItem
                Where o.Active = True
                Select o).ToList
    End Function

    Public Function GetProjectBBCItems(ByVal projectId As Integer)
        Return DAOGetter.ProjectBBCItemsDAO(Context).GetByProject(projectId)
    End Function

    Public Function GetProjectBBCItem(ByVal projectBBCItemId As Integer) As ProjectBBCItem
        Return DAOGetter.ProjectBBCItemsDAO(Context).GetByID(projectBBCItemId)
    End Function

#Region "New BBC Item"

#End Region

#End Region

#Region "Insertion / Update / Removal"

    Public Sub SaveNewBBCItem(ByVal saveBBCItem As NewBBCItem)

        Dim audit As String = ""

        If saveBBCItem.ID = 0 Then
            DAOGetter.NewBBCItemDAO(Context).Insert(saveBBCItem)
        Else
            DAOGetter.NewBBCItemDAO(Context).Update(saveBBCItem)
        End If
    End Sub

    Public Sub SetBBCItemActivity(ByVal bbcItemId As Integer,
                                  ByVal active As Boolean)
        Dim bbcItem As NewBBCItem = GetNewBBCItem(bbcItemId)
        bbcItem.Active = active
        SaveNewBBCItem(bbcItem)
    End Sub

    Public Sub RemoveBBCItemFromProject(ByVal projectBBCItemId As Integer, ByVal userId As Integer)
        Dim deleteProjectBBCItem As ProjectBBCItem = GetProjectBBCItem(projectBBCItemId)
        Dim audit As String = String.Format("BBC Item {0} has been removed from the project", deleteProjectBBCItem.BBCItem.Description)
        DAOGetter.ProjectBBCItemsDAO(Context).Delete(deleteProjectBBCItem)

        ' TODO: Now remove from kits

        AuditTrailManager.PostAudit(audit, userId, deleteProjectBBCItem.Project.ID,
                                  AppSettingsGet.DeleteAuditChangeTypeID, AppSettingsGet.BBCItemSectionID)
    End Sub

    Public Sub SaveProjectBBCItem(ByVal projectBBCItem As ProjectBBCItem, ByVal userId As Integer)

        Dim auditTrail As String = ""
        Dim changeTypeId As Integer

        If projectBBCItem.ID = 0 Then
            changeTypeId = AppSettingsGet.AddAuditChangeTypeID
            auditTrail = GetNewBBCItemAuditLog(projectBBCItem)
            DAOGetter.ProjectBBCItemsDAO(Context).Insert(projectBBCItem)
        Else
            changeTypeId = AppSettingsGet.EditAuditChangeTypeID
            auditTrail = GetModifiedProjectBBCItemAuditLog(projectBBCItem)
            DAOGetter.ProjectBBCItemsDAO(Context).Update(projectBBCItem)
        End If

        AuditTrailManager.PostAudit(auditTrail, userId, projectBBCItem.Project.ID, changeTypeId, AppSettingsGet.BBCItemSectionID)

    End Sub

#End Region

#Region "Clone"

    Private Function GetCopyProjectBBCItem(ByVal projectBBCItemId As Integer) As ProjectBBCItem
        Dim staticContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)
        Return DAOGetter.ProjectBBCItemsDAO(staticContext).GetByID(projectBBCItemId)
    End Function

#End Region

#Region "Audit"

    Public Function GetModifiedProjectBBCItemAuditLog(ByVal modifiedProjBBCItem As ProjectBBCItem, Optional ByVal newLine As String = "<br />") As String

        Dim oldProjectBBCItem As ProjectBBCItem = GetCopyProjectBBCItem(modifiedProjBBCItem.ID)
        Dim summary As String = ""

        If modifiedProjBBCItem.Quantity <> oldProjectBBCItem.Quantity Then
            summary += String.Format("Quantity for {0} has been changed from {1} to {2}", modifiedProjBBCItem.BBCItem.Description, oldProjectBBCItem.Quantity, modifiedProjBBCItem.Quantity)
            summary += newLine
        End If

        If modifiedProjBBCItem.PackQuantity <> oldProjectBBCItem.PackQuantity Then
            summary += String.Format("Pack Quantity for {0} has been changed from {1} to {2}", modifiedProjBBCItem.BBCItem.Description, oldProjectBBCItem.PackQuantity, modifiedProjBBCItem.PackQuantity)
            summary += newLine
        End If

        If modifiedProjBBCItem.DeliveryDate <> oldProjectBBCItem.DeliveryDate Then
            summary += String.Format("Delivery Date in {0} has been changed from {1} to {2}", modifiedProjBBCItem.BBCItem.Description, oldProjectBBCItem.DeliveryDate.ToString("dd/MM/yyyy"), modifiedProjBBCItem.DeliveryDate.ToString("dd/MM/yyyy"))
            summary += newLine
        End If

        If modifiedProjBBCItem.BBCItem.ID <> oldProjectBBCItem.BBCItem.ID Then
            summary += String.Format("BBC Item has been modified from {0} to {1}", oldProjectBBCItem.BBCItem.Description, modifiedProjBBCItem.BBCItem.Description)
            summary += newLine
        End If

        Return summary

    End Function

    Private Function GetNewBBCItemAuditLog(ByVal newBBCItem As ProjectBBCItem)

        Dim summary As String = ""
        summary += String.Format("BBC Item {0} has been added to the project", newBBCItem.BBCItem.Description)

        Return summary

    End Function

#End Region

#Region "Queries"

#Region "Old BBC Item"

    Public Function DoesBBCItemBelongToProject(ByVal projectBBCItemId As Integer, ByVal projectId As Integer) As Boolean

        'Dim tmpBBCItem As BBCItem = DAOGetter.BBCItemDAO(Context).GetByID(bbcItemId)

        'If tmpBBCItem IsNot Nothing Then
        '    If tmpBBCItem.Project.ID = projectId Then
        '        Return True
        '    End If
        'End If

        Dim tmpBBCItem As ProjectBBCItem = DAOGetter.ProjectBBCItemsDAO(Context).GetByID(projectBBCItemId)

        'If tmpBBCItem IsNot Nothing Then
        '    If tmpBBCItem.Project.ID = projectId Then
        '        Return True
        '    End If
        'End If

        If tmpBBCItem IsNot Nothing Then
            If tmpBBCItem.Project.ID = projectId Then
                Return True
            End If
        End If

        Return False

    End Function

#End Region

    Public Function SearchBBCItem(ByVal searchString As String, ByVal critera As BBCItemSearchCriteria) As List(Of NewBBCItem)

        Dim results As New List(Of NewBBCItem)

        If searchString <> "" Then

            If critera = BBCItemSearchCriteria.PART_NUMBER Then
                results = DAOGetter.NewBBCItemDAO(Context).SearchByPartNumber(searchString)
            Else
                results = DAOGetter.NewBBCItemDAO(Context).SearchByDescription(searchString)
            End If

        End If

        Return results

    End Function

    Public Function GetBBCItemsByBrand(ByVal brandId As Integer) As List(Of NewBBCItem)
        Return DAOGetter.NewBBCItemDAO(Context).GetByBrand(brandId)
    End Function

#End Region

End Module
