Imports System.Globalization
Imports aptBusinessLogic

Partial Class NewReserveTime
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            SetupPage()
        End If
    End Sub

    Protected Sub ctrlReserveTime_TaskCompleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlReserveTime.TaskCompleted
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Reserve time was saved successfully!")
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPage()
        Dim projectId As Integer

        If Integer.TryParse(Request.QueryString("projectId"), projectId) Then
            Dim gregCal As New GregorianCalendar

            ctrlReserveTime.ProjectId = projectId
            ctrlReserveTime.WeekNumber = gregCal.GetWeekOfYear(Date.Now, CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)
            ctrlReserveTime.YearNumber = Date.Now.Year
            ctrlReserveTime.LoadReserveTime()

            ctrlProjectHeader.ProjectId = projectId
            ctrlSubNavProject.projectId = projectId
        End If

    End Sub

#End Region

End Class
