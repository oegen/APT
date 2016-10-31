'----------------------------------------------------------------------------------------------
' Filename    : ProjectDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        22/08/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ProjectCostingsDAO : Inherits GenericDAO(Of ProjectCostings)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByProject(ByVal id As Integer) As ProjectCostings

        Return (From o In Context.ProjectCosting
                Where o.Project.ID = id
                Select o).SingleOrDefault

    End Function

End Class

