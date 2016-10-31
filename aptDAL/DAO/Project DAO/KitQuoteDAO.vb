'----------------------------------------------------------------------------------------------
' Filename    : KitQuoteDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        21/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class KitQuoteDAO : Inherits GenericDAO(Of KitQuote)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetQuotes(ByVal kitId As Integer) As List(Of KitQuote)
        Return (From o In Context.KitQuotes
                Where o.Kit.ID = kitId
                Select o).ToList()
    End Function

    Public Function GetLatestByKit(ByVal kitId As Integer) As KitQuote
        Return (From o In Context.KitQuotes
                Where o.Kit.ID = kitId
                Order By o.ID Descending
                Select o).FirstOrDefault()
    End Function

End Class
