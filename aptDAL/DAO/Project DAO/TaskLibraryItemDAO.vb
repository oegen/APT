'----------------------------------------------------------------------------------------------
' Filename    : TaskLibraryItemDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class TaskLibraryItemDAO : Inherits GenericDAO(Of TaskLibraryItem)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public Function GetByTask(ByVal taskId As Integer) As TaskLibraryItem

        Dim context As APTContext = CType(_context, APTContext)

        Return (From t In context.TaskLibraryItems
                Where t.Task.ID = taskId
                Select t).SingleOrDefault

    End Function

End Class
