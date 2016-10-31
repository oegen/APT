'----------------------------------------------------------------------------------------------
' Filename    : TimesheetDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class TimesheetDAO : Inherits GenericDAO(Of Timesheet)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetUsersTimesheets(ByVal userId As Integer) As List(Of Timesheet)

        Return (From t In Context.Timesheets
                Where t.User.ID = userId
                Order By t.EntryDate Descending
                Select t).ToList

    End Function

    Public Function GetByDateAndUser(ByVal filterDate As Date, ByVal userId As Integer) As List(Of Timesheet)

        Return (From t In Context.Timesheets
                Where t.User.ID = userId _
                    AndAlso t.DateOfWork = filterDate
                Order By t.EntryDate Descending
                Select t).ToList

    End Function

    Public Function GetByDate(ByVal filterDate As Date) As List(Of Timesheet)

        Return (From t In Context.Timesheets
                Where t.DateOfWork = filterDate
                Order By t.EntryDate Descending
                Select t).ToList

    End Function

    Public Function GetBySurname(ByVal surname As String) As List(Of Timesheet)

        Return (From t In Context.Timesheets
               Where t.User.Surname.Contains(surname)
               Order By t.EntryDate Descending
               Select t).ToList

    End Function

    Public Function GetByEntryDateOrder() As List(Of Timesheet)

        Return (From t In Context.Timesheets
                Order By t.EntryDate Descending
                Select t).ToList

    End Function

    Public Function GetByProject(ByVal ProjectEntityId As Integer, ByVal projectId As Integer) As List(Of Timesheet)

        Return (From t In Context.Timesheets
                Where t.ContextEntityId = ProjectEntityId _
                    AndAlso t.EntityParentId = projectId
                Select t).ToList()

    End Function

    Public Function GetByElement(ByVal elementId As Integer) As List(Of Timesheet)

        Return (From t In Context.Timesheets
                Where t.EntityParentId = elementId
                Select t).ToList()
    End Function

End Class
