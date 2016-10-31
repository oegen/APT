Imports System.Globalization
Imports aptBusinessLogic

Partial Class Schedule_Schedule
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            dtpSelectedWeek.SelectedDate = Date.Now

            LoadPage()
        End If
    End Sub

    'Protected Sub lnkRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefresh.Click
    '    LoadPage()
    'End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        LoadPage()
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadPage()
        Dim gregCal As New GregorianCalendar
        Dim weekNum As Integer = gregCal.GetWeekOfYear(dtpSelectedWeek.SelectedDate, CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)

        ctrlSchedule.WeekNumber = weekNum
        ctrlSchedule.Year = dtpSelectedWeek.SelectedDate.Value.Year
        ctrlSchedule.LoadSchedule()
    End Sub

#End Region

End Class
