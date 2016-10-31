'----------------------------------------------------------------------------------------------
' Filename    : WorkingWeekExceptionDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class WorkingWeekExceptionDAO : Inherits GenericDAO(Of WorkingWeekException)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of WorkingWeekException)
        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.WorkingWeekExceptions
                Where o.Active = active
                Select o).ToList()
    End Function

    Public Function GetByuser(ByVal user As AptUser) As List(Of WorkingWeekException)
        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.WorkingWeekExceptions
                Where o.User.ID = user.ID
                Select o).ToList
    End Function

End Class
