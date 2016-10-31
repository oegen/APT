'----------------------------------------------------------------------------------------------
' Filename    : KitDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class KitDAO : Inherits GenericDAO(Of Kit)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetKitByProject(ByVal projectId As Integer) As List(Of Kit)

        Return (From o In Context.Kits
               Where o.Project.ID = projectId
               Select o).ToList()

    End Function

End Class

