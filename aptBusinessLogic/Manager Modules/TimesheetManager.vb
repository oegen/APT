'----------------------------------------------------------------------------------------------
' Filename    : TimesheetManager.vb
' Description : Deals with the initialisation / deletion of timesheets.
'
' Release Initials  Date        Comment
' 1       LP/TL     02/06/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module TimesheetManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        End Get
    End Property

#Region "Gets"

#Region "Timesheets"

    Public Function GetAllTimesheets()
        Return DAOGetter.TimesheetDAO(Context).GetAll()
    End Function

    Public Function GetTimesheet(ByVal timeSheetId As Integer) As Timesheet
        Return DAOGetter.TimesheetDAO(Context).GetByID(timeSheetId)
    End Function

    Public Function GetTimesheetsByUser(ByVal userId As Integer) As List(Of Timesheet)
        Return DAOGetter.TimesheetDAO(Context).GetUsersTimesheets(userId)
    End Function

    Public Function GetTimeSheetsByUserAndWorkDate(ByVal filterDate As Date, ByVal userId As Integer) As List(Of Timesheet)
        Return DAOGetter.TimesheetDAO(Context).GetByDateAndUser(filterDate, userId)
    End Function

    Public Function GetTimeSheetsByWorkDate(ByVal filterDate As Date) As List(Of Timesheet)
        Return DAOGetter.TimesheetDAO(Context).GetByDate(filterDate)
    End Function

    Public Function GetTimeSheetBySubTypeId(ByVal subTypeId As Integer) As List(Of Timesheet)

        Return (From o In Context.Timesheets
                Where o.ContextEntityId = AppSettingsGet.EntityElementId _
                    AndAlso o.EntityParentId = subTypeId
                Select o).ToList()

    End Function

    Public Function GetTimesheetsByLatestEntryDates() As List(Of Timesheet)
        Return DAOGetter.TimesheetDAO(Context).GetByEntryDateOrder
    End Function

    Public Function GetTimesheetByUserSurname(ByVal surname As String) As List(Of Timesheet)
        Return DAOGetter.TimesheetDAO(Context).GetBySurname(surname)
    End Function

    Public Function GetTimeSheetByProjectId(ByVal projectId As Integer) As List(Of Timesheet)
        Return DAOGetter.TimesheetDAO(Context).GetByProject(AppSettingsGet.EntityProjectId, projectId)
    End Function

    Public Function GetTimeSheetByElementId(ByVal elementId As Integer) As List(Of Timesheet)
        Return DAOGetter.TimesheetDAO(Context).GetByElement(elementId)
    End Function

#End Region

#Region "Timesheet Reasons"

    Public Function GetActiveTimesheetReasons() As List(Of TimesheetReason)
        Dim activeTimeReasons As List(Of TimesheetReason) = DAOGetter.TimesheetReasonDAO(Context).GetActive()

        Return (From o In activeTimeReasons
                Order By o.ReasonText Ascending
                Select o).ToList()
    End Function

    Public Function GetTimesheetReason(ByVal id As Integer) As TimesheetReason
        Return DAOGetter.TimesheetReasonDAO(Context).GetByID(id)
    End Function

#End Region

#End Region

#Region "Inserts / Update / Delete"

#Region "Timesheets"

    Public Sub SaveTimesheet(ByVal saveTimeSheet As Timesheet)

        If saveTimeSheet.ID = 0 Then
            saveTimeSheet.EntryDate = DateTime.Now
            DAOGetter.TimesheetDAO(Context).Insert(saveTimeSheet)
        Else
            DAOGetter.TimesheetDAO(Context).Update(saveTimeSheet)
        End If

    End Sub

    Public Sub DeleteTimesheet(ByVal timesheetId As Integer)

        Dim deleteTimeSheet As Timesheet = GetTimesheet(timesheetId)
        DAOGetter.TimesheetDAO(Context).Delete(deleteTimeSheet)

    End Sub

#End Region

#End Region

#Region "Queries"

    Public Sub GetAverageTime(ByRef hours As Integer, ByRef minutes As Integer, ByVal subTypeId As Integer)

        Dim timesheetsWithSubtypeEntries As List(Of Timesheet) = GetTimeSheetBySubTypeId(subTypeId)
        Dim totalMinutes As Integer = 0

        For Each timesheetEntry In timesheetsWithSubtypeEntries
            totalMinutes += (timesheetEntry.HourSpent * 60)
            totalMinutes += timesheetEntry.MinutesSpent
        Next

        If totalMinutes > 0 Then
            Dim averageInMinutes As Double = totalMinutes / timesheetsWithSubtypeEntries.Count

            hours = Math.Floor(averageInMinutes / 60)
            minutes = averageInMinutes Mod 60
        Else
            hours = 0
            minutes = 0
        End If

    End Sub

    Public Sub GetTotalTimeEnteredOnDate(ByVal enterDate As Date, ByVal userId As Integer, ByRef enteredHour As Integer, ByRef enteredMinutes As Integer)

        Dim filterDate As Date
        enteredHour = 0
        enteredMinutes = 0

        If Date.TryParse(enterDate, filterDate) = True Then
            Dim timesheets As List(Of Timesheet) = GetTimeSheetsByUserAndWorkDate(filterDate, userId)
            GetTotalHoursAndMinutes(timesheets, enteredHour, enteredMinutes)
        Else
            Throw New Exception("Invalid Date")
        End If

    End Sub

    Private Sub GetTotalHoursAndMinutes(ByRef timesheetList As List(Of Timesheet), ByRef hours As Integer, ByRef minutes As Integer)

        Dim totalMinutes As Integer = 0

        For Each timesheetEntry In timesheetList
            totalMinutes += (timesheetEntry.HourSpent * 60)
            totalMinutes += timesheetEntry.MinutesSpent
        Next

        If totalMinutes > 0 Then
            hours = Math.Floor(totalMinutes / 60)
            minutes = totalMinutes Mod 60
        Else
            hours = 0
            minutes = 0
        End If

    End Sub

#End Region

End Module
