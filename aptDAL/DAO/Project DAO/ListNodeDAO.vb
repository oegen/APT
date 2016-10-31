'----------------------------------------------------------------------------------------------
' Filename    : ListNodeDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ListNodeDAO : Inherits GenericDAO(Of ListNode)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of ListNode)
        Return (From o In context.ListNodes
                Where o.Active = active
                Select o).ToList()
    End Function

    Public Function GetByList(ByVal listId As Integer, Optional ByVal active As Boolean = True) As List(Of ListNode)
        Return (From o In Context.ListNodes
                Where o.Active = active AndAlso o.List.ID = listId
                Select o).ToList()
    End Function

    Public Function GetByName(ByVal nodeName As String, ByVal listId As Integer, Optional ByVal nodeId As Integer = 0) As List(Of ListNode)

        If nodeId = 0 Then
            Return (From o In Context.ListNodes
                    Where o.Name = nodeName AndAlso o.List.ID = listId
                    Select o).ToList()
        Else
            Return (From o In Context.ListNodes
                    Where o.Name = nodeName AndAlso o.List.ID = listId _
                    AndAlso o.ID <> nodeId).ToList()
        End If

    End Function

End Class
