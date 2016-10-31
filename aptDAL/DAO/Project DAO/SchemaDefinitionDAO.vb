'----------------------------------------------------------------------------------------------
' Filename    : SchemaDefinitionDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class SchemaDefinitionDAO : Inherits GenericDAO(Of SchemaDefinition)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property


    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of SchemaDefinition)

        Return (From o In Context.SchemaDefinitions
                Where o.Active = active
                Select o).ToList()

    End Function

    Public Function GetBySchema(ByVal schemaId As Integer) As List(Of SchemaDefinition)

        Return (From o In context.SchemaDefinitions
                Where o.Schema.ID = schemaId
                Select o).ToList()

    End Function

End Class
