Imports System.Globalization
Imports aptBusinessLogic

Partial Class Project_ProjectReserveTime
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LoadPage()
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadPage()
        Dim projectId As Integer

        If Integer.TryParse(Request.QueryString("projectId"), projectId) Then
            Dim cal As New GregorianCalendar
            Dim weekNumber As Integer = cal.GetWeekOfYear(Date.Now, CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)

            ctrlReserveTime.ProjectId = projectId
            ctrlReserveTime.WeekNumber = weekNumber
            ctrlReserveTime.YearNumber = Date.Now.Year
            ctrlReserveTime.LoadReserveTime()
        End If
    End Sub

#End Region

End Class
