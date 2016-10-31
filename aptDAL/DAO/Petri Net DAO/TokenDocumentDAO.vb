'----------------------------------------------------------------------------------------------
' Filename    : TokenDocumentDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        08/08/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class TokenDocumentDAO : Inherits GenericDAO(Of TokenDocument)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetAllByTokenID(ByVal tokenId As Integer) As List(Of TokenDocument)

        Return (From t In context.TokenDocuments
                Where t.Token.ID = tokenId
                Select t).ToList

    End Function

    Public Function GetByDocumentID(ByVal documentId As Integer) As List(Of TokenDocument)

        Return (From t In context.TokenDocuments
                Where t.Document.ID = documentId
                Select t).ToList

    End Function

    Public Function GetByTokenAndDocumentID(ByVal tokenId As Integer, ByVal documentId As Integer) As TokenDocument

        Return (From t In context.TokenDocuments
                Where t.Token.ID = tokenId And t.Document.ID = documentId
                Select t).SingleOrDefault

    End Function

End Class
