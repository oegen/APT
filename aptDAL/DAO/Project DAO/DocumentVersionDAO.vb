'----------------------------------------------------------------------------------------------
' Filename    : DocumentVersionDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        04/10/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class DocumentVersionDAO : Inherits GenericDAO(Of DocumentVersion)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetLatestVersion(ByVal documentId As Integer) As DocumentVersion
        Return (From o In Context.DocumentVersions
                Where o.Document.ID = documentId
                Order By o.ID Descending
                Select o).FirstOrDefault
    End Function

    Public Function GetVersions(ByVal documentId As Integer) As List(Of DocumentVersion)

        Return (From o In Context.DocumentVersions
                Where o.Document.ID = documentId
                Order By o.ID Descending
                Select o).ToList()

    End Function

End Class
