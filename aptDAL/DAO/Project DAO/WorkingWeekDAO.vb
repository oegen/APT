'----------------------------------------------------------------------------------------------
' Filename    : WorkingWeekDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class WorkingWeekDAO : Inherits GenericDAO(Of WorkingWeek)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetWorkingDayByWeekDayString(ByVal day As String) As WorkingWeek

        Return (From o In Context.WorkingWeeks
                Where o.Weekday = day
                Select o).SingleOrDefault

    End Function

End Class
