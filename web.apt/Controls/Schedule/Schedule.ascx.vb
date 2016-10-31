Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Schedule_Schedule
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property WeekNumber As Integer
        Get
            Return ViewState("_weekNumber")
        End Get
        Set(ByVal value As Integer)
            ViewState("_weekNumber") = value
        End Set
    End Property

    Public Property Year As Integer
        Get
            Return ViewState("_year")
        End Get
        Set(ByVal value As Integer)
            ViewState("_year") = value
        End Set
    End Property

#End Region

#Region "Public Methods"

    Public Sub LoadSchedule()
        Dim projectList As List(Of Project) = ProjectManager.GetProjectsWithReservedTimeInWeek(WeekNumber, Year)

        ctrlFreelanceShedule.WeekNumber = WeekNumber
        ctrlFreelanceShedule.YearNumber = Year
        ctrlFreelanceShedule.SetupFreelanceTime()

        ctrlProjectSchedule.CurrentWeek = WeekNumber
        ctrlProjectSchedule.Year = Year
        ctrlProjectSchedule.SetupControl()
    End Sub

#End Region

End Class
