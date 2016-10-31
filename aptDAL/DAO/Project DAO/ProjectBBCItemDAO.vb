'----------------------------------------------------------------------------------------------
' Filename    : ProjectBBCItemDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ProjectBBCItemDAO : Inherits GenericDAO(Of ProjectBBCItem)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByProject(ByVal projectId As Integer) As List(Of ProjectBBCItem)
        Return (From o In Context.ProjectBBCItems
                Where o.Project.ID = projectId
                Select o).ToList()
    End Function

End Class
