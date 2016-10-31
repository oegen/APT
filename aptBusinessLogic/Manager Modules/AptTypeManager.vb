'----------------------------------------------------------------------------------------------
' Filename    : AptTypeManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptDAL
Imports GenericDAL
Imports GenericUtilities

Public Module AptTypeManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Get Variations"

#Region "Types"

    Public Function GetAllAptTypes() As List(Of ElementType)
        Dim allAptTypes As List(Of ElementType) = DAOGetter.AptTypeDAO(Context).GetAll()

        Return (From o In allAptTypes
                Order By o.Name
                Select o).ToList()

    End Function

    Public Function GetAptTypes(Optional ByVal active As Boolean = True) As List(Of ElementType)
        Return DAOGetter.AptTypeDAO(Context).GetByActive(active)
    End Function

    Public Function GetAptType(ByVal id As Integer) As ElementType
        Return DAOGetter.AptTypeDAO(Context).GetByID(id)
    End Function

    Public Sub SetAptTypeActivity(ByVal typeId As Integer, ByVal active As Boolean)
        Dim tmpType As ElementType = GetAptType(typeId)

        tmpType.Active = active
        SaveType(tmpType)
    End Sub

    Public Function GetActiveAndSelectedType(ByVal selectedTypeId As Integer)

        Return (From o In GetAllAptTypes()
                Where o.Active = True OrElse o.ID = selectedTypeId
                Select o).ToList()

    End Function

#End Region

#Region "SubTypes"

    Public Function GetSubclassType(ByVal id As Integer) As SubclassType
        Return DAOGetter.SubclassTypeDAO(Context).GetByID(id)
    End Function

    Public Function GetSubclassTypesByAptType(ByVal AptTypeId As Integer, Optional ByVal active As Boolean = True) As List(Of SubclassType)
        Return DAOGetter.SubclassTypeDAO(Context).GetByType(AptTypeId, active)
    End Function

    Public Function GetAllSubclassTypesByAptType(ByVal AptTypeId As Integer) As List(Of SubclassType)
        Return DAOGetter.SubclassTypeDAO(Context).GetAllByType(AptTypeId)
    End Function

    Public Sub SetSubclassTypeActivity(ByVal subClassTypeId As Integer, ByVal active As Boolean)

        Dim tmpSubclassType As SubclassType = GetSubclassType(subClassTypeId)
        tmpSubclassType.Active = active
        SaveSubclassType(tmpSubclassType)

    End Sub

    Public Function GetActiveAndSelectedSubclassType(ByVal selectedTypeId As Integer, selectedSubClassTypeId As Integer) As List(Of SubclassType)

        Return (From o In GetAllSubclassTypesByAptType(selectedTypeId)
                Where o.Active = True OrElse o.ID = selectedSubClassTypeId
                Select o).ToList()

    End Function


#End Region

#End Region

#Region "Insert / Updates / Deletion"

    Public Sub SaveType(ByRef saveType As ElementType)

        saveType.Modified = DateTime.Now

        If saveType.ID = 0 Then
            saveType.Created = DateTime.Now
            saveType.Active = True
            DAOGetter.AptTypeDAO(Context).Insert(saveType)
        Else
            DAOGetter.AptTypeDAO(Context).Update(saveType)
        End If

    End Sub

    Public Sub SaveSubclassType(ByRef saveSubclassType As SubclassType)

        saveSubclassType.Modified = DateTime.Now

        If saveSubclassType.ID = 0 Then
            saveSubclassType.Created = DateTime.Now
            DAOGetter.SubclassTypeDAO(Context).Insert(saveSubclassType)
        Else
            DAOGetter.SubclassTypeDAO(Context).Update(saveSubclassType)
        End If

    End Sub

#End Region

End Module
