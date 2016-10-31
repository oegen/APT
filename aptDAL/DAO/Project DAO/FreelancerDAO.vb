Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class FreelancerDAO : Inherits GenericDAO(Of FreelancerTime)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetTimesByProject(ByVal projectId As Integer) As List(Of FreelancerTime)

        Return (From ft In Context.FreelancerTime
                Where ft.Project.ID = projectId
                Select ft).ToList

    End Function

    Public Function GetSpecific(ByVal projectId As Integer, ByVal userId As Integer,
                                ByVal numWeeks As Integer, ByVal year As Integer) As FreelancerTime

        Return (From ft In Context.FreelancerTime
                Where ft.Project.ID = projectId AndAlso ft.User.ID = userId _
                    AndAlso ft.NumWeeks = numWeeks AndAlso ft.Year = year
                Select ft).SingleOrDefault

    End Function

    Public Function GetByWeek(ByVal weekNumber As Integer, ByVal yearNumber As Integer) As List(Of FreelancerTime)

        Dim freelanceList As List(Of FreelancerTime) = GetAll()

        ' We need to get all the freelancers times that contain the current week and year within their ranges
        ' If the times fall within the ranges from freelancerTime.StartWeek and totalRunningWeeks.
        'Return (From ft In freelanceList
        '        Where (yearNumber > ft.Year) OrElse (weekNumber >= ft.StartWeek AndAlso yearNumber = ft.Year) _
        '            AndAlso _
        '              (yearNumber < GetEndYear(ft.Year, ft.StartWeek, ft.NumWeeks) _
        '                 OrElse (weekNumber < GetEndWeek(ft.StartWeek, ft.NumWeeks) AndAlso yearNumber = GetEndYear(ft.Year, ft.StartWeek, ft.NumWeeks))
        '              )
        '        Select ft).ToList

        Return (From ft In freelanceList
                Where (ft.StartWeek = weekNumber AndAlso yearNumber = ft.Year) _
                OrElse (GetEndWeek(ft.StartWeek, ft.NumWeeks) = weekNumber AndAlso yearNumber = GetEndYear(ft.Year, ft.StartWeek, ft.NumWeeks))
                Select ft).ToList()

    End Function

    Private Function GetEndYear(ByVal year As Integer, ByVal startWeek As Integer, ByVal numWeeks As Integer) As Integer
        Dim endWeek As Integer = startWeek + numWeeks

        ' Remove the remainder weeks then divide by the total weeks in a year.
        ' E.G. week 100 will be year + 1 (as 48 remainder would be removed)
        ' E.G. week 50 will be year + 0 (as 50 remainder would be removed)
        Return year + (endWeek - (endWeek Mod 52)) / 52

    End Function

    Private Function GetEndWeek(ByVal startWeek As Integer, ByVal numWeeks As Integer) As Integer
        Dim endWeek As Integer = startWeek + numWeeks

        Return (endWeek Mod 52) - 1
    End Function

End Class
