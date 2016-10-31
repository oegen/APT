'----------------------------------------------------------------------------------------------
' Filename    : ProjectDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ProjectDAO : Inherits GenericDAO(Of Project)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public ReadOnly Property Projects As IQueryable(Of Project)
        Get
            Return context.Projects.AsQueryable()
        End Get
    End Property


    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of Project)
        Return (From o In context.Projects
                Where o.Active = active
                Select o).ToList()
    End Function

    Public Function GetByUserAsQueryable(ByVal userId As Integer) As IQueryable(Of Project)
        ' Get all projects associated with a user
        Return (From u In context.ProjectRoleAssociations
                Where u.User.ID = userId
                Select u.Project)
    End Function

    Public Function GetByUser(ByVal userId As Integer) As List(Of Project)
        ' Get all projects associated with a user
        Return GetByUserAsQueryable(userId).ToList()
    End Function

    Public Function GetProjectWithReservedTimeInWeek(ByVal week As Integer, ByVal year As Integer) As List(Of Project)

        Dim projectList As List(Of Project) = GetByActive()

        Return (From proj In projectList
                Where GetProjectReservedTimeInWeek(proj.ID, week, year) = True
                Select proj).ToList

    End Function

    Public Function GetProjectReservedTimeInWeek(ByVal projectId As Integer, ByVal weekNumber As Integer, ByVal yearNumber As Integer) As Boolean
        If DAOGetter.ReservedTimeDAO(context).GetProjectReservedTimeInWeek(projectId, weekNumber, yearNumber).Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
