'----------------------------------------------------------------------------------------------
' Filename    : SchemaDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class SchemaDAO : Inherits GenericDAO(Of Schema)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of Schema)
        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.Schemas
                Where o.Active = active
                Select o).ToList()
    End Function

    Public Function GetByEntity(ByVal entityId As Integer) As List(Of Schema)
        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.Schemas
        Where o.Active = True AndAlso o.Entity.ID = entityId
        Select o).ToList()

    End Function

End Class
