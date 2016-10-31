'----------------------------------------------------------------------------------------------
' Filename    : AptLoginDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class AptLoginDAO : Inherits GenericDAO(Of AptLogin)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetLoginByUser(ByVal userId As Integer) As AptLogin
        Return (From o In context.UserLogins
                Where o.User.ID = userId
                Select o).SingleOrDefault
    End Function

    Public Function GetLoginByUsername(ByVal username As String) As AptLogin
        Return (From o In context.UserLogins
                Where o.Username = username
                Select o).SingleOrDefault
    End Function

    Public Function SearchByUsername(ByVal username As String, Optional ByVal active As Boolean = True) As List(Of AptUser)
        Return (From u In Context.UserLogins
                Where u.Username.Contains(username) AndAlso u.User.Active = active
                Select u.User).ToList
    End Function

End Class
