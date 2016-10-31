'----------------------------------------------------------------------------------------------
' Filename    : ProjectManager.vb
' Description : Deals with the initialisation / deletion of projects.
'
' Release Initials  Date        Comment
' 1       LP        27/05/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL

Public Module WorkingWeekManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

    Public Function GetWorkingWeekdays() As List(Of WorkingWeek)
        Return DAOGetter.WorkingWeekDAO(context).GetAll()
    End Function

    Public Function GetAllBookedTimings(ByVal userId As Integer, Optional ByVal active As Boolean = True) As List(Of WorkingWeekException)
        Return (From o In context.WorkingWeekExceptions
                Where o.User.ID = userId And o.Active = active
                Select o).ToList
    End Function

    'Public Function GetAllByWeekday(ByVal userId As Integer, ByVal weekdayId As Integer, Optional ByVal active As Boolean = True) As List(Of WorkingWeekException)

    '    Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

    '    Return (From o In context.WorkingWeekExceptions
    '            Where o.User.ID = userId And o.Weekday.ID = weekdayId
    '            Select o).ToList
    'End Function

    Public Sub CreateWorkingWeekExcpetion(ByVal workingWeek As WorkingWeekException)
        DAOGetter.WorkingWeekExceptionDAO(context).Insert(workingWeek)
    End Sub

    Public Sub RemoveWorkingWeekException(ByVal workingWeekExceptionId As Integer)

        Dim workingWeekEx As WorkingWeekException = DAOGetter.WorkingWeekExceptionDAO(context).GetByID(workingWeekExceptionId)
        workingWeekEx.Active = False

        DAOGetter.WorkingWeekExceptionDAO(context).Update(workingWeekEx)
    End Sub

    Public Function IsTimeFree(ByVal userId As Integer, ByVal workingWeekId As Integer, ByVal startTime As Integer, _
                               ByVal endTime As Integer, Optional ByVal active As Boolean = True) As Boolean
        Dim timeFree As Boolean = True
        'Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        '' Get all working week exceptions associated with the user on the specified weekday
        'Dim userExceptionList As List(Of WorkingWeekException) = (From o In context.WorkingWeekExceptions
        '                                                          Where o.Active = active And o.User.ID = userId And o.Weekday.ID = workingWeekId
        '                                                          Select o).ToList

        'For Each timeBooked As WorkingWeekException In userExceptionList
        '    If (startTime > timeBooked.StartTime And startTime < timeBooked.EndTime) _
        '        Or (endTime > timeBooked.StartTime And endTime < timeBooked.EndTime) _
        '        Or (startTime < timeBooked.StartTime And endTime > timeBooked.StartTime) Then

        '        timeFree = False
        '    End If
        'Next

        Return timeFree
    End Function

#Region "Gets"

    Public Function GetWorkingWeekException(ByVal id As Integer) As WorkingWeekException
        Return DAOGetter.WorkingWeekExceptionDAO(Context).GetByID(id)
    End Function

    Public Function GetAllActiveWorkingWeekExceptions() As List(Of WorkingWeekException)
        Return DAOGetter.WorkingWeekExceptionDAO(Context).GetByActive(True).ToList()
    End Function

#End Region

#Region "Insert / Update / Remove"

    Public Sub SaveWorkingWeekException(ByVal saveWorkingWeekEx As WorkingWeekException)

        saveWorkingWeekEx.Modified = DateTime.Now

        If saveWorkingWeekEx.ID = 0 Then
            saveWorkingWeekEx.Created = DateTime.Now
            DAOGetter.WorkingWeekExceptionDAO(Context).Insert(saveWorkingWeekEx)
        Else
            DAOGetter.WorkingWeekExceptionDAO(Context).Update(saveWorkingWeekEx)
        End If

    End Sub

#End Region

    Public Function GetWorkingWeekHours() As Integer

        'Dim workingWeekDays As List(Of WorkingWeek) = DAOGetter.WorkingWeekDAO(Context).GetAll()
        'Dim totalHours As Integer = 0

        'For Each workingDay As WorkingWeek In workingWeekDays
        '    totalHours += (workingDay.EndTime - workingDay.StartTime)
        'Next

        ' Instead of working out the time we are just going to return a a static time from the web config
        ' Return totalHours
        Return AppSettingsGet.WorkingWeekHours
        'Return AppSettingsGet.TotalArtworkerHours
    End Function

    Public Function GetDayTotalWorkingHours(ByVal workingDay As DayOfWeek) As Integer

        'Dim workingHours As Integer = 0
        'Dim day As WorkingWeek = DAOGetter.WorkingWeekDAO(Context).GetWorkingDayByWeekDayString(workingDay.ToString)

        'If day IsNot Nothing Then
        '    Return (day.EndTime - day.StartTime)
        'End If

        'Return 0

        Return 7

    End Function

    Public Function TotalHoursUnavailable() As Integer

        Dim activeWorkingWeekEx As List(Of WorkingWeekException) = DAOGetter.WorkingWeekExceptionDAO(Context).GetByActive(True)
        Dim hoursUnavailable As Integer = 0

        For Each tmpWorkWeekEx As WorkingWeekException In activeWorkingWeekEx
            hoursUnavailable += tmpWorkWeekEx.Hours
        Next

        Return hoursUnavailable

    End Function

End Module
