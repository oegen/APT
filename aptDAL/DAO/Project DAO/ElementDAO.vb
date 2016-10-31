'----------------------------------------------------------------------------------------------
' Filename    : ElementDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ElementDAO : Inherits GenericDAO(Of Element)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of Element)
        Return (From e In context.Elements
                Where e.Active = active
                Select e).ToList()
    End Function

    Public Function GetByProject(ByVal projectId As Integer, Optional ByVal active As Boolean = True) As List(Of Element)
        Return (From o In context.Elements
                Where o.Active = active And
                    o.Project.ID = projectId
                Select o).ToList()
    End Function

    Public Function GetNonStoppedByProject(ByVal projectId As Integer, Optional ByVal stopped As Boolean = False) As List(Of Element)
        Return (From o In context.Elements
                Where o.ElementStopped = stopped AndAlso o.Project.ID = projectId
                Select o).ToList()
    End Function

    Public Function GetAllByProject(ByVal projectId As Integer) As List(Of Element)
        Return (From o In Context.Elements
                Where o.Project.ID = projectId
                Select o).ToList()
    End Function

End Class
