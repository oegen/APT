'----------------------------------------------------------------------------------------------
' Filename    : ProjectEntityDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ProjectEntityDAO : Inherits GenericDAO(Of ProjectEntity)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of ProjectEntity)
        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.ProjectEntities
                Where o.Active = active
                Select o).ToList()
    End Function

End Class
