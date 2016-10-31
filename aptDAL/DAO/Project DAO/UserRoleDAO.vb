'----------------------------------------------------------------------------------------------
' Filename    : UserRoleDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class UserRoleDAO : Inherits GenericDAO(Of UserRole)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetAllByUser(ByVal userId As Integer) As List(Of UserRole)
        Return (From ur In context.UserRoles
                Where ur.User.ID = userId
                Select ur).ToList
    End Function

    Public Function GetByUserAndRole(ByVal userId As Integer, ByVal roleId As Integer) As UserRole
        Return (From ur In context.UserRoles
                Where ur.User.ID = userId And ur.Role.ID = roleId
                Select ur).SingleOrDefault
    End Function

    Public Function GetByRole(ByVal roleId As Integer) As List(Of UserRole)
        Return (From ur In Context.UserRoles
               Where ur.Role.ID = roleId AndAlso ur.User.Active = True
               Select ur).ToList
    End Function

End Class
