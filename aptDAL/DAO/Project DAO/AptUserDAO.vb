'----------------------------------------------------------------------------------------------
' Filename    : AptUserDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class AptUserDAO : Inherits GenericDAO(Of AptUser)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of AptUser)
        Return (From o In context.AptUsers
                Where o.Active = active
                Select o).ToList()
    End Function

    Public Function UserHasAccess(ByVal userId As Integer, ByVal roleId As Integer) As AptUser
        Return (From u In context.UserRoles
                Where u.User.ID = userId And u.Role.ID = roleId
                Select u.User).SingleOrDefault()
    End Function

    Public Function GetUserByUsername(ByVal username As String, Optional ByVal active As Boolean = True) As AptUser
        Return (From u In context.UserLogins
                Where u.Username = username And u.User.Active = active
                Select u.User).SingleOrDefault
    End Function

    Public Function SearchBySurname(ByVal surname As String, Optional ByVal active As Boolean = True) As List(Of AptUser)
        Return (From u In Context.AptUsers
                Where u.Surname.Contains(surname) AndAlso u.Active = active
                Select u).ToList
    End Function

End Class
