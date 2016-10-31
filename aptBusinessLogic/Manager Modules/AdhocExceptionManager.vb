'----------------------------------------------------------------------------------------------
' Filename    : AdhocExceptionManager.vb
' Description : Deals with the initialisation / deletion of adhocs.
'
' Release Initials  Date        Comment
' 1       LP        31/05/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptEntities
Imports aptDAL
Imports GenericDAL
Imports GenericUtilities
Imports System.Globalization
Imports System.Data.Linq.SqlClient

Public Module AdhocExceptionManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Get Variations"

    Public Function GetAllExceptions(Optional ByVal active As Boolean = True) As List(Of Adhoc)
        modLogManager.Debug("Pre-GetAllExceptions in AdhocExceptionManager")

        Return DAOGetter.AdhocDAO(Context).GetByActive(active)
    End Function

    Public Function GetAllBetweenDates(ByVal startDate As DateTime, ByVal endDate As DateTime, Optional ByVal active As Boolean = True) As List(Of Adhoc)
        modLogManager.Debug("Pre-GetAllBetweenDates in AdhocExceptionManager")

        Return DAOGetter.AdhocDAO(Context).GetAllBetweenDates(startDate, endDate, active)
    End Function

    Public Function GetAdHocException(ByVal id As Integer) As Adhoc
        modLogManager.Debug("Pre-GetAdHocException in AdhocExceptionManager")

        Return DAOGetter.AdhocDAO(Context).GetByID(id)
    End Function

    Public Function GetFutureAndPresentAdHocExceptions() As List(Of Adhoc)
        modLogManager.Debug("Pre-GetFutureAndPresentAdHocExceptions in AdhocExceptionManager")

        Return DAOGetter.AdhocDAO(Context).GetFutureAndPresent()
    End Function

#End Region

#Region "Insertion / Removal (Deactivate)"

    Public Sub SaveAdHocException(ByVal saveAdHoc As Adhoc)
        modLogManager.Debug("Pre-SaveAdHocException in AdhocExceptionManager")

        If saveAdHoc.ID = 0 Then
            DAOGetter.AdhocDAO(Context).Insert(saveAdHoc)
        Else
            DAOGetter.AdhocDAO(Context).Update(saveAdHoc)
        End If

        modLogManager.Debug("Post-SaveAdHocException in AdhocExceptionManager")
    End Sub

    Public Sub RemoveAdHocException(ByVal adhocExceptionId As Integer)
        modLogManager.Debug("Pre-RemoveAdHocException in AdhocExceptionManager")

        Dim adhocException As Adhoc = DAOGetter.AdhocDAO(Context).GetByID(adhocExceptionId)
        adhocException.Active = False
        DAOGetter.AdhocDAO(Context).Update(adhocException)

        modLogManager.Debug("Post-RemoveAdHocException in AdhocExceptionManager")
    End Sub

#End Region

#Region "Queries"

    Public Function IsUsersTimeFree(ByVal userId As Integer, ByVal startDateTime As DateTime, ByVal endDateTime As DateTime) As Boolean
        Dim timeFree As Boolean = True
        Dim userAdhocList As List(Of Adhoc) = (From o In Context.Adhocs
                                               Where o.Active = True And o.User.ID = userId
                                               Select o).ToList

        For Each timeBooked As Adhoc In userAdhocList
            If IsSameDay(startDateTime, timeBooked.StartDate) Then
                If (startDateTime.Hour > timeBooked.StartDate.Hour And startDateTime.Hour < timeBooked.EndDate.Hour) _
                Or (endDateTime.Hour > timeBooked.StartDate.Hour And endDateTime.Hour < timeBooked.EndDate.Hour) _
                Or (startDateTime.Hour < timeBooked.StartDate.Hour And endDateTime.Hour > timeBooked.StartDate.Hour) Then

                    timeFree = False
                End If
            End If
        Next

        Return timeFree
    End Function

    Private Function IsSameDay(ByVal original As DateTime, ByVal comparison As DateTime) As Boolean
        Dim match As Boolean = False

        If original.Day = comparison.Day And original.Month = comparison.Month And original.Year = comparison.Year Then
            match = True
        End If

        Return match
    End Function

    Public Function TotalHoursUnavailable(ByVal weekNumber As Integer,
                                          ByVal year As Integer)
        modLogManager.Debug("Pre-TotalHoursUnavailable in AdhocExceptionManager")

        Dim cal As New GregorianCalendar
        Dim firstDateOfWorkingWeek As DateTime = modDates.FirstDateOfWeek(year, weekNumber, CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)
        Dim lastDateOfWorkingWeek As DateTime = cal.AddDays(firstDateOfWorkingWeek, 6) ' Get to the last day of the week
        Dim scheduledAdhocs As List(Of Adhoc) = GetAllBetweenDates(firstDateOfWorkingWeek, lastDateOfWorkingWeek, True)
        Dim hoursUnavailable As Integer = 0

        For Each tmpAdHoc As Adhoc In scheduledAdhocs

            If tmpAdHoc.IsSingleDay Then
                hoursUnavailable += tmpAdHoc.Hours
            Else
                ' spans across multiple days, so we use the forula
                ' no of dates * working hours
                Dim workingWeekDays As List(Of WorkingWeek) = DAOGetter.WorkingWeekDAO(Context).GetAll()
                Dim daysUnavailable As Integer = SqlMethods.DateDiffDay(tmpAdHoc.StartDate, tmpAdHoc.EndDate)
                daysUnavailable = daysUnavailable + 1 ' need to include the day itself in the difference

                For i As Integer = 0 To daysUnavailable
                    ' for each day get the amount of working hours
                    Dim workDay As DateTime = cal.AddDays(tmpAdHoc.StartDate, i)

                    If workDay <= lastDateOfWorkingWeek Then ' if the day is within the week
                        ' If the day is not sunday or saturday
                        If workDay.DayOfWeek <> DayOfWeek.Saturday AndAlso workDay.DayOfWeek <> DayOfWeek.Sunday Then
                            hoursUnavailable += WorkingWeekManager.GetDayTotalWorkingHours(workDay.DayOfWeek)
                        End If
                    End If

                Next
            End If

        Next

        modLogManager.Debug(String.Format("Post-TotalHoursUnavailable in AdhocExceptionManager, there were {0} hours free.", hoursUnavailable))

        Return hoursUnavailable

    End Function

#End Region

End Module
