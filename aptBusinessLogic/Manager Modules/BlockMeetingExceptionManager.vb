'----------------------------------------------------------------------------------------------
' Filename    : BlockMeetingExceptionManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        17/08/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptDAL
Imports GenericDAL
Imports GenericUtilities
Imports System.Globalization

Public Module BlockMeetingExceptionManager

    Private ReadOnly Property Context As APTContext
        Get
            Return ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)
        End Get
    End Property

#Region "Gets"

    Public Function GetAllBlockMeetingExceptions() As List(Of BlockMeetingException)
        Return DAOGetter.BlockMeetingExceptionDAO(Context).GetAll()
    End Function

    Public Function GetBlockMeetingException(ByVal id As Integer) As BlockMeetingException
        Return DAOGetter.BlockMeetingExceptionDAO(Context).GetByID(id)
    End Function

    Public Function GetPresentAndFutureExceptions() As List(Of BlockMeetingException)
        Return DAOGetter.BlockMeetingExceptionDAO(Context).GetByPresentAndFuture()

    End Function

#End Region

#Region "Inserts / Updates / Deletion"

    Public Sub SaveBlockMeeting(ByVal saveBlockMeetingException As BlockMeetingException)

        If saveBlockMeetingException.ID = 0 Then
            DAOGetter.BlockMeetingExceptionDAO(Context).Insert(saveBlockMeetingException)
        Else
            DAOGetter.BlockMeetingExceptionDAO(Context).Update(saveBlockMeetingException)
        End If

    End Sub

    Public Sub RemoveBlockMeetingException(ByVal id As Integer)
        Dim removeBlockMeetingException As BlockMeetingException = DAOGetter.BlockMeetingExceptionDAO(Context).GetByID(id)

        removeBlockMeetingException.Active = False
        DAOGetter.BlockMeetingExceptionDAO(Context).Update(removeBlockMeetingException)
    End Sub

#End Region

#Region "Queries"

    Public Function TotalHoursUnavailable(ByVal weekNumber As Integer, ByVal year As Integer) As Integer

        Dim hoursUnavailable As Integer = 0
        Dim cal As New GregorianCalendar
        Dim firstDateOfWorkingWeek As DateTime = modDates.FirstDateOfWeek(year,
                                                                          weekNumber,
                                                                          CalendarWeekRule.FirstDay,
                                                                          AptSettings.FirstDayOfWeek)

        Dim lastDateOfWorkingWeek As DateTime = cal.AddDays(firstDateOfWorkingWeek, 6) ' Get to the last day of the week

        Dim BlockMeetings As List(Of BlockMeetingException) = _
            DAOGetter.BlockMeetingExceptionDAO(Context).GetBlockMeetingsinDateRange(firstDateOfWorkingWeek, _
                                                                                    lastDateOfWorkingWeek)

        For Each tmpBlockMeeting In BlockMeetings
            hoursUnavailable += tmpBlockMeeting.Hours * AppSettingsGet.NumArtworkers 'TODO:are we going to go with this?
        Next

        Return hoursUnavailable

    End Function

#End Region

End Module
