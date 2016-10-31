'----------------------------------------------------------------------------------------------
' Filename    : ReservedTimeDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ReservedTimeDAO : Inherits GenericDAO(Of ReservedTime)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public ReadOnly Property context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByProject(ByVal projectId As Integer, Optional ByVal active As Boolean = True) As List(Of ReservedTime)
        Return (From rt In context.ReservedTimes
                Where rt.Active = active AndAlso rt.Project.ID = projectId
                Select rt
                Order By rt.Year, rt.WeekNumber).ToList
    End Function

    Public Function GetAllWithinWeekAndYear(ByVal weekNumber As Integer, ByVal yearNumber As Integer) As List(Of ReservedTime)

        Return (From rt In context.ReservedTimes
                Where rt.WeekNumber = weekNumber _
                AndAlso rt.Year = yearNumber _
                AndAlso rt.Active = True
                Select rt).ToList

    End Function

    Public Function GetSpecific(ByVal projectId As Integer, ByVal userId As Integer,
                                ByVal weekNumber As Integer, ByVal year As Integer) As ReservedTime

        Return (From rt In context.ReservedTimes
                Where rt.Project.ID = projectId AndAlso rt.User.ID = userId _
                    AndAlso rt.WeekNumber = weekNumber AndAlso rt.Year = year
                Select rt).SingleOrDefault

    End Function

    Public Function GetProjectReservedTimeInWeek(ByVal projectId As Integer, ByVal weekNumber As Integer _
                                                 , ByVal yearNumber As Integer) As List(Of ReservedTime)

        Return (From rt In context.ReservedTimes
                Where rt.WeekNumber = weekNumber AndAlso rt.Project.ID = projectId _
                    AndAlso rt.Year = yearNumber
                Select rt).ToList

    End Function

End Class
