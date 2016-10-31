'----------------------------------------------------------------------------------------------
' Filename    : ReserveTimeManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptDAL
Imports aptEntities
Imports GenericDAL
Imports GenericUtilities

Public Module ReserveTimeManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Gets"

    Public Function GetReserveTimeByID(ByVal id As Integer) As ReservedTime
        Return DAOGetter.ReservedTimeDAO(Context).GetByID(id)
    End Function

    Public Function GetAllReservedTimes(Optional ByVal active As Boolean = True) As List(Of ReservedTime)
        Return (From rt In DAOGetter.ReservedTimeDAO(Context).GetAll()
                Where rt.Active = active
                Select rt).ToList
    End Function

    Public Function GetReservedTimeForProject(ByVal projectId As Integer)
        Return DAOGetter.ReservedTimeDAO(Context).GetByProject(projectId)
    End Function

    Public Function GetTotalAvailableHoursInAWeek(ByVal weekNum As Integer, ByVal yearNumber As Integer) As Integer

        Dim hoursAvailable As Integer = AppSettingsGet.NumArtworkers * AppSettingsGet.WorkingWeekHours

        Dim workingException As Integer = WorkingWeekManager.TotalHoursUnavailable()
        Dim adHocExceptions As Integer = AdhocExceptionManager.TotalHoursUnavailable(weekNum, yearNumber)
        Dim meetingExceptions As Integer = BlockMeetingExceptionManager.TotalHoursUnavailable(weekNum, yearNumber)
        Dim totalExceptions As Integer = workingException + adHocExceptions + meetingExceptions
        Dim reservedTime As Integer = GetReservedHoursInAWeek(weekNum, yearNumber)

        hoursAvailable = (hoursAvailable - totalExceptions) - reservedTime

        If hoursAvailable <= 0 Then
            Return 0
        Else
            Return hoursAvailable
        End If

    End Function

    Public Function GetReservedHoursInAWeek(ByVal weekNumber As Integer, ByVal yearNumber As Integer) As Integer
        Dim totalReservedHours As Integer = 0
        Dim weeksReservedTimes As List(Of ReservedTime) = DAOGetter.ReservedTimeDAO(Context).GetAllWithinWeekAndYear(weekNumber, yearNumber)

        For Each reservedTime As ReservedTime In weeksReservedTimes
            totalReservedHours += reservedTime.Duration
        Next

        Return totalReservedHours
    End Function

    Public Function GetAllWithinWeekAndYear(ByVal weekNumber As Integer, ByVal yearNumber As Integer) As List(Of ReservedTime)
        Return DAOGetter.ReservedTimeDAO(Context).GetAllWithinWeekAndYear(weekNumber, yearNumber)
    End Function

    'Public Function GetRemainingFreeHoursInWeek(ByVal weekNumber As Integer, ByVal yearNumber As Integer) As Integer

    '    Dim hoursLeft As Integer = GetTotalAvailableHoursInAWeek(weekNumber, yearNumber) - GetReservedHoursInAWeek(weekNumber, yearNumber)

    '    If hoursLeft <= 0 Then
    '        Return 0
    '    Else
    '        Return hoursLeft
    '    End If

    'End Function

    Public Function GetReservedTimeByProjectAndWeek(ByVal projectId As Integer, ByVal weekNumber As Integer, ByVal yearNumber As Integer) As List(Of ReservedTime)
        Return DAOGetter.ReservedTimeDAO(Context).GetProjectReservedTimeInWeek(projectId, weekNumber, yearNumber)
    End Function

    Public Function GetTotalHoursReservedForProject(ByVal projectId As Integer, Optional ByRef startWeek As Integer = 0, Optional ByRef startYear As Integer = 0,
                                                    Optional ByRef endWeek As Integer = 0, Optional ByRef endYear As Integer = 0) As Integer

        Dim projectsReservedTimes As List(Of ReservedTime) = DAOGetter.ReservedTimeDAO(Context).GetByProject(projectId)
        Dim totalHours As Integer = 0
        Dim firstLoop As Boolean = True

        For Each reservedTime In projectsReservedTimes
            totalHours += reservedTime.Duration

            If firstLoop = True Then
                startWeek = reservedTime.WeekNumber
                startYear = reservedTime.Year
                endWeek = startWeek
                endYear = startYear
                firstLoop = False
            End If

            ' Check whether the reserved time is earlier than the current earliest date
            If (reservedTime.Year < startYear) _
                Or (reservedTime.Year = startYear AndAlso reservedTime.WeekNumber < startWeek) Then

                startYear = reservedTime.Year
                startWeek = reservedTime.WeekNumber
            End If

            If (reservedTime.Year > endYear) _
                Or (reservedTime.Year = endYear AndAlso reservedTime.WeekNumber > endWeek) Then

                endWeek = reservedTime.Year
                endYear = reservedTime.WeekNumber
            End If

        Next

        Return totalHours

    End Function

#End Region

#Region "Insert / Update"

    Public Sub AddReserveTime(ByVal newReserveTimeItem As ReservedTime, ByVal loggedInUserId As Integer)
        modLogManager.Debug("AddReserveTime - Start")
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        newReserveTimeItem.Active = True

        DAOGetter.ReservedTimeDAO(context).Insert(newReserveTimeItem)

        modLogManager.Debug("AddReserveTime - Inserted, no post audit")

        Dim auditTrailMessage As String = String.Format("Reserve Time Added {0}{0} Week Number : {1}{0}Year : {2}{0}Duration (hours) : {3}{0}Number of Artworkers : {4}", _
                                                        AppSettingsGet.HTMLNewLine, newReserveTimeItem.WeekNumber, newReserveTimeItem.Year, _
                                                        newReserveTimeItem.Duration, newReserveTimeItem.NumArtworkers)

        AuditTrailManager.PostAudit(auditTrailMessage, loggedInUserId, newReserveTimeItem.Project.ID, _
                                    AppSettingsGet.AddAuditChangeTypeID, AppSettingsGet.ReserveTimeAuditSectionID)

        modLogManager.Debug("AddReserveTime - Complete")
    End Sub

    Public Sub UpdateReserveTime(ByVal reservedTime As ReservedTime, ByVal loggedInUserId As Integer)
        modLogManager.Debug("UpdateReserveTime - Start")

        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        Dim singularContext As APTContext = ContextService.CreateNewDataContext(Of APTContext)(AppSettingsGet.SQLConnectionStr)

        ' Get the old copy
        Dim oldReservedTime As ReservedTime = DAOGetter.ReservedTimeDAO(singularContext).GetByID(reservedTime.ID)

        '' Audit trail message (summary of changes)
        'Dim changedPropertiesSummary As String = modReflection.GenerateAlteredPropertiesSummary(oldReservedTime, reservedTime, AppSettingsGet.HTMLNewLine)

        'Dim auditTrailMessage As String = String.Format("Reserve Time Details have been edited. {0}{0}{1}", _
        '                                                AppSettingsGet.HTMLNewLine, changedPropertiesSummary)

        'If changedPropertiesSummary <> "" Then

        '    AuditTrailManager.PostAudit(auditTrailMessage, loggedInUserId, reservedTime.Project.ID, _
        '                            AppSettingsGet.EditAuditChangeTypeID, AppSettingsGet.ReserveTimeAuditSectionID)

        'End If

        DAOGetter.ReservedTimeDAO(context).Update(reservedTime)

        modLogManager.Debug("UpdateReserveTime - Completimo")
    End Sub

    Public Sub RemoveReserveTime(ByVal reserveTimeId As Integer, ByVal loggedInUserId As Integer)
        modLogManager.Debug("RemoveReserveTime - Start")

        Dim reservedTime As ReservedTime = GetReserveTimeByID(reserveTimeId)
        Dim context As APTContext = ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

        reservedTime.Active = False

        DAOGetter.ReservedTimeDAO(context).Update(reservedTime)

        modLogManager.Debug("RemoveReserveTime - Active set to false, now post audit")

        Dim auditTrailMessage As String = String.Format("The time reserved on {0} for {1} hour(s) has been deleted.", _
                                                        modDates.FirstDateOfWeek(reservedTime.Year, reservedTime.WeekNumber, Globalization.CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek), _
                                                        reservedTime.Duration)

        AuditTrailManager.PostAudit(auditTrailMessage, loggedInUserId, reservedTime.Project.ID, AppSettingsGet.DeleteAuditChangeTypeID, AppSettingsGet.ReserveTimeAuditSectionID)

        modLogManager.Debug("RemoveReserveTime - Complete")
    End Sub

#End Region

#Region "Utilities"

    Public Sub CreateReservedTimeItems(ByVal startWeek As Integer, ByVal startYear As Integer, ByVal totalNumberOfHours As Integer _
                                       , ByVal numWeeks As Integer, ByVal numArtworkers As Integer, ByVal projectId As Integer _
                                       , ByVal userId As Integer)

        Dim currentProject As Project = ProjectManager.GetProject(projectId)
        Dim currentUser As AptUser = UserManager.GetUser(userId)
        Dim runningHourCount As Integer = 0
        ' Dim artworkerCapabilities As Integer = GetTotalAvailableHoursInAWeek(startWeek, startYear)  'numArtworkers * WorkingWeekManager.GetWorkingWeekHours()
        ' Dim artworkerCapabilities As Integer = WorkingWeekManager.GetWorkingWeekHours() 'hack 
        Dim artworkerCapabilities As Integer = 35
        Dim freelancerWorkHours As Integer = 0

        For i As Integer = 0 To numWeeks - 1

            Dim reservedTimeItem As New ReservedTime
            Dim yearNumber As Integer = startYear + Math.Truncate((startWeek + i) / 52)
            Dim weekNumber As Integer = ((startWeek + i) Mod 52)
            weekNumber = IIf(weekNumber = 0, 52, weekNumber)
            Dim usedHours As Integer = GetReservedHoursInAWeek(startWeek + i, yearNumber)
            Dim AvailableHours As Integer = GetTotalAvailableHoursInAWeek(startWeek, startYear) ' - exceptions
            Dim remainingHours As Integer = totalNumberOfHours - runningHourCount
            Dim potentialHours = AvailableHours
            Dim actualHours = 0

            artworkerCapabilities = artworkerCapabilities * numArtworkers

            If AvailableHours > artworkerCapabilities Then
                potentialHours = artworkerCapabilities
            ElseIf AvailableHours = 0 Then
                ' ALL TIME HAS BEEN USED UP SO LET'S SET THE FREELANCER TIME TO THIS
                freelancerWorkHours = totalNumberOfHours
                Exit For
            End If

            If potentialHours > remainingHours Then
                actualHours = remainingHours
            Else
                actualHours = potentialHours
            End If

            ' If the free hours are greater than the hours we have to allocate then set the hours remaining, otherwise use the entire allocation

            If actualHours > 0 Then
                reservedTimeItem.Duration = actualHours
                reservedTimeItem.Project = currentProject
                reservedTimeItem.User = currentUser
                reservedTimeItem.WeekNumber = weekNumber
                reservedTimeItem.Year = yearNumber
                reservedTimeItem.NumArtworkers = numArtworkers

                AddReserveTime(reservedTimeItem, userId)

                runningHourCount += (actualHours * numArtworkers)
            Else
                ' User may have reserved multiple weeks but the time needed has already been slotted in so just exit
                Exit Sub
            End If

            If i = numWeeks - 1 Then
                ' If this is the last iteration and there are still hours left then the remaining hours need to be 
                ' give to the freelancer
                If remainingHours > actualHours Then
                    freelancerWorkHours = remainingHours - (actualHours)
                End If
            End If
        Next

        ' Do what is required with remaining weeks
        ' If runningHourCount > 0 Then
        If freelancerWorkHours > 0 Then
            ' Set freelancers, do what is required
            Dim freelancerTime As New FreelancerTime

            freelancerTime.Project = currentProject
            ' freelancerTime.Duration = runningHourCount
            freelancerTime.Duration = freelancerWorkHours
            freelancerTime.NumWeeks = numWeeks
            freelancerTime.StartWeek = startWeek
            freelancerTime.User = currentUser
            freelancerTime.Year = startYear

            AddFreelanceTime(freelancerTime, userId)
        End If
    End Sub

    Public Function GetReserveWeekObjects(ByVal startWeek As Integer, ByVal numWeeks As Integer, ByVal yearNumber As Integer) As List(Of ReserveWeek)
        Dim weekList As New List(Of ReserveWeek)

        For i As Integer = 0 To numWeeks - 1

            Dim week As New ReserveWeek
            Dim weekNum As Integer = startWeek + i

            ' Check if we cross over into the next year (52 weeks in a year)
            If weekNum > 52 Then
                weekNum = weekNum - 52
                yearNumber += 1
            End If

            week.UsedHours = ReserveTimeManager.GetReservedHoursInAWeek(weekNum, yearNumber)
            week.AvailableHours = AppSettingsGet.NumArtworkers * AppSettingsGet.WorkingWeekHours
            'week.AvailableHours = GetTotalAvailableHoursInAWeek(startWeek, yearNumber) ' TODO - all exceptions - it was this
            'week.AvailableHours = WorkingWeekManager.GetWorkingWeekHours()
            week.WeekNumber = weekNum
            week.Year = yearNumber

            weekList.Add(week)
        Next

        Return weekList

    End Function

    '' Represents reserved time
    'Public Function GetTotalHoursUsed(ByVal weekNumber As Integer, ByVal yearNumber As Integer) As Integer
    '    Dim totalHours As Integer = 0
    '    Dim reservedTimes As List(Of ReservedTime) = DAOGetter.ReservedTimeDAO(Context).GetAllWithinWeekAndYear(weekNumber, yearNumber)

    '    For Each reservedTime In reservedTimes
    '        totalHours += reservedTime.Duration
    '    Next

    '    Return totalHours
    'End Function

    Public Function GetTotalFreelancerTimeForProject(ByVal projectId As Integer) As Integer
        Dim totalHours As Integer = 0
        Dim times As List(Of FreelancerTime) = DAOGetter.FreelancerTimeDAO(Context).GetTimesByProject(projectId)

        For Each fTime In times
            totalHours += fTime.Duration
        Next

        Return totalHours
    End Function

#End Region

#Region "Freelancer Times"

    Public Function GetAllFreelanceTimes(ByVal projectId As Integer) As List(Of FreelancerTime)
        Return DAOGetter.FreelancerTimeDAO(Context).GetTimesByProject(projectId)
    End Function

    Public Function GetFreelanceTimeById(ByVal freelanceId As Integer) As FreelancerTime
        Return DAOGetter.FreelancerTimeDAO(Context).GetByID(freelanceId)
    End Function

    Public Function GetFreelanceTimeByWeek(ByVal weekNumber As Integer, ByVal yearNumber As Integer) As List(Of FreelancerTime)
        Return DAOGetter.FreelancerTimeDAO(Context).GetByWeek(weekNumber, yearNumber)
    End Function

    Public Sub AddFreelanceTime(ByVal freelanceTime As FreelancerTime, ByVal loggedInUserId As Integer)
        modLogManager.Debug("AddFreelanceTime - Start")
        DAOGetter.FreelancerTimeDAO(Context).Insert(freelanceTime)
        modLogManager.Debug("AddFreelanceTime - Inserted, now post audit")

        Dim auditTrailMessage As String = String.Format("Reserve Time Added {0}{0} Number of Weeks : {1}{0}Year : {2}{0}Duration (hours) : {3}", _
                                                        AppSettingsGet.HTMLNewLine, freelanceTime.NumWeeks,
                                                        freelanceTime.Year, freelanceTime.Duration)

        AuditTrailManager.PostAudit(auditTrailMessage, loggedInUserId, freelanceTime.Project.ID, _
                                    AppSettingsGet.AddAuditChangeTypeID, AppSettingsGet.ReserveTimeAuditSectionID)

        modLogManager.Debug("AddFreelanceTime - Complete")
    End Sub

    Public Sub UpdateFreelanceTime(ByVal freelanceTime As FreelancerTime, ByVal loggedInUserId As Integer)
        modLogManager.Debug("UpdateFreelanceTime - Start")
        DAOGetter.FreelancerTimeDAO(Context).Update(freelanceTime)
        modLogManager.Debug("UpdateFreelanceTime - Complete")
    End Sub

    Public Sub RemoveFreelanceTime(ByVal freelanceTimeId As Integer, ByVal loggedInUserId As Integer)
        modLogManager.Debug("RemoveFreelanceTime - Start")
        Dim freelancerTime As FreelancerTime = GetFreelanceTimeById(freelanceTimeId)

        DAOGetter.FreelancerTimeDAO(Context).Delete(freelancerTime)

        modLogManager.Debug("RemoveFreelanceTime - Deleted the object, now post audit")

        Dim auditTrailMessage As String = String.Format("Reserve Time Removed {0}{0} Number of Weeks : {1}{0}Year : {2}{0}Duration (hours) : {3}", _
                                                        AppSettingsGet.HTMLNewLine, freelancerTime.NumWeeks,
                                                        freelancerTime.Year, freelancerTime.Duration)

        AuditTrailManager.PostAudit(auditTrailMessage, loggedInUserId, freelancerTime.Project.ID, _
                                    AppSettingsGet.DeleteAuditChangeTypeID, AppSettingsGet.ReserveTimeAuditSectionID)

        modLogManager.Debug("RemoveFreelanceTime - Complete")
    End Sub

#End Region

End Module
