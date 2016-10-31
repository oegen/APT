'----------------------------------------------------------------------------------------------
' Filename    : KitElementsDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class KitElementsDAO : Inherits GenericDAO(Of KitElement)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetKitElementsByItemAndKit(ByVal typeId As Integer, _
                                               ByVal quoteId As Integer, _
                                               ByVal itemTypeId As Integer) As KitElement

        Return (From o In Context.KitElements
                Where o.ItemId = typeId AndAlso o.KitQuote.ID = quoteId AndAlso o.ItemType = itemTypeId
                Select o).SingleOrDefault

    End Function

    Public Function GetKitElementsByQuote(ByVal quoteId As Integer) As List(Of KitElement)

        Return (From o In Context.KitElements
                Where o.KitQuote.ID = quoteId
                Select o).ToList()

    End Function

    Public Function GetKitElementByElement(ByVal id As Integer, ByVal itemTypeId As Integer) As List(Of KitElement)

        Return (From o In Context.KitElements
                Where id = o.ItemId _
                    AndAlso o.ItemType = itemTypeId
                Select o).ToList()

    End Function

End Class

