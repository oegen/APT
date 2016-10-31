'----------------------------------------------------------------------------------------------
' Filename    : SubclassTypeDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class SubclassTypeDAO : Inherits GenericDAO(Of SubclassType)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of SubclassType)

        Return (From o In Context.SubclassTypes
                Where o.Active = active
                Select o).ToList()

    End Function

    Public Function GetByType(ByVal typeId As Integer, Optional ByVal active As Boolean = True) As List(Of SubclassType)

        Return (From o In Context.SubclassTypes
                Where o.Active = active And o.Type.ID = typeId
                Select o).ToList()

    End Function

    Public Function GetAllByType(ByVal typeId As Integer, Optional ByVal active As Boolean = True) As List(Of SubclassType)

        Return (From o In Context.SubclassTypes
                Where o.Type.ID = typeId
                Select o).ToList()

    End Function

    Public Function GetByElement(ByVal element As Element, Optional ByVal active As Boolean = True) As List(Of SubclassType)

        Return (From o In GetByActive(active)
                Where o.ElementSchema IsNot Nothing And o.ElementSchema.ID = element.ID
                Select o).ToList

    End Function

End Class
