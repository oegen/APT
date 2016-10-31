'----------------------------------------------------------------------------------------------
' Filename    : ProjectRoleAssociationDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ProjectRoleAssociationDAO : Inherits GenericDAO(Of ProjectRoleAssociation)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public ReadOnly Property ProjectRoleAssociation As IQueryable(Of ProjectRoleAssociation)
        Get
            Return Context.ProjectRoleAssociations.AsQueryable()
        End Get
    End Property

    Public Function GetByProject(ByVal projectId As Integer) As List(Of ProjectRoleAssociation)
        Return (From pra In context.ProjectRoleAssociations
                Where pra.Project.ID = projectId
                Select pra).ToList
    End Function

    Public Function GetByRole(ByVal roleId As Integer) As List(Of ProjectRoleAssociation)
        Return (From pra In Context.ProjectRoleAssociations
                Where pra.Role.ID = roleId
                Select pra).ToList
    End Function

    Public Function GetByUserAndRole(ByVal userId As Integer, ByVal roleId As Integer) As List(Of ProjectRoleAssociation)
        Return (From pra In Context.ProjectRoleAssociations
               Where pra.User.ID = userId _
               AndAlso pra.Role.ID = roleId
               Select pra).ToList
    End Function

    Public Function GetByUserAndProject(ByVal userId As Integer, ByVal projectId As Integer) As List(Of ProjectRoleAssociation)
        Return (From pra In Context.ProjectRoleAssociations
               Where pra.User.ID = userId _
               AndAlso pra.Project.ID = projectId
               Select pra).ToList
    End Function

    Public Function GetAllProjectAndUserAssociations() As List(Of ProjectRoleAssociation)
        Return (From pra In Context.ProjectRoleAssociations
                Select pra).ToList
    End Function

    Public Function GetByProjectsAndRole(ByVal projectIds As List(Of Integer), ByVal roleId As Integer)

        Return (From pra In Context.ProjectRoleAssociations
                Where projectIds.Contains(pra.ID) _
                AndAlso roleId = roleId
                Select pra).ToList()

    End Function

End Class
