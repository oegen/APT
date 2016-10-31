Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ArcResponseDAO : Inherits GenericDAO(Of ArcResponse)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByActive(ByVal active As Boolean) As List(Of ArcResponse)

        Return (From o In Context.ArcResponses
                Where o.Active = active
                Select o).ToList()

    End Function

End Class
