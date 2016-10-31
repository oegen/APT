'----------------------------------------------------------------------------------------------
' Filename    : ProjectKittingInfoDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ProjectKittingBriefDAO : Inherits GenericDAO(Of ProjectKittingBrief)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByProject(ByVal projectId As Integer)

        Return (From o In Context.ProjectKittingBrief
                Where o.Project.ID = projectId
                Select o).SingleOrDefault

    End Function

End Class
