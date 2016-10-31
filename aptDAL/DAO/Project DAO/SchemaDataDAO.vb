'----------------------------------------------------------------------------------------------
' Filename    : SchemaDataDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class SchemaDataDAO : Inherits GenericDAO(Of SchemaData)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public ReadOnly Property SchemaData As IQueryable(Of SchemaData)
        Get
            Return Context.SchemaData.AsQueryable()
        End Get
    End Property

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetSchemaDataByEntity(ByVal entityId As Integer) As List(Of SchemaData)

        Return (From o In Context.SchemaData
                Where o.ParentID = entityId
                Select o).ToList()

    End Function

    Public Function GetParentIdBySchemaAndValue(schemaId As Integer, value As String) As List(Of Integer)

        Return (From o In Context.SchemaData
                Where o.SchemaDefinition.ID = schemaId _
                AndAlso o.SchemaElementValue = value
                Select o.ParentID).ToList()
    End Function

End Class
