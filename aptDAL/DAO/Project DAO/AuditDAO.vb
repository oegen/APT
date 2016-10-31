'----------------------------------------------------------------------------------------------
' Filename    : AuditDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class AuditDAO : Inherits GenericDAO(Of Audit)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public Function GetAllByProject(ByVal projectId As Integer)
        Dim context As APTContext = CType(_context, APTContext)

        Return (From a In context.Audits
                Where a.Project.ID = projectId
                Select a).ToList
    End Function

End Class
