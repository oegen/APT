'----------------------------------------------------------------------------------------------
' Filename    : NewBBCItemDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class NewBBCItemDAO : Inherits GenericDAO(Of NewBBCItem)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function SearchByPartNumber(ByVal searchText As String) As List(Of NewBBCItem)
        Return (From o In Context.NewBBCItem
            Where o.PartNumber.Contains(searchText)
            Select o).ToList()
    End Function

    Public Function SearchByDescription(ByVal searchText As String) As List(Of NewBBCItem)
        Return (From o In Context.NewBBCItem
            Where o.Description.Contains(searchText)
            Select o).ToList()
    End Function


    Public Function GetByBrand(ByVal brandId As Integer) As List(Of NewBBCItem)
        Return (From o In Context.NewBBCItem
                Where o.Brand.ID = brandId
                Select o).ToList()
    End Function

End Class
