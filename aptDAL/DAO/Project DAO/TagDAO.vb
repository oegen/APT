'----------------------------------------------------------------------------------------------
' Filename    : TagDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class TagDAO : Inherits GenericDAO(Of Tag)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of Tag)
        Dim context As APTContext = CType(_context, APTContext)

        Return (From t In context.Tags
                Where t.Active = active
                Select t).ToList()
    End Function

    Public Function GetByName(ByVal tagName As String, ByVal tagId As Integer) As List(Of Tag)

        If tagId = 0 Then
            Return (From t In Context.Tags
               Where t.Name = tagName
               Select t).ToList
        Else
            Return (From t In Context.Tags
               Where t.Name = tagName AndAlso t.ID <> tagId
               Select t).ToList
        End If

    End Function

End Class
