'----------------------------------------------------------------------------------------------
' Filename    : ListDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ListDAO : Inherits GenericDAO(Of AptList)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of AptList)
        Return (From o In Context.AptLists
                Where o.Active = active
                Select o).ToList()
    End Function

    Public Function GetByName(ByVal listName As String, Optional ByVal listId As Integer = 0) As List(Of AptList)

        If listId = 0 Then
            Return (From o In Context.AptLists
                    Where o.Name = listName
                    Select o).ToList()
        Else
            Return (From o In Context.AptLists
                    Where o.Name = listName AndAlso o.ID <> listId
                    Select o).ToList()
        End If

    End Function

End Class

