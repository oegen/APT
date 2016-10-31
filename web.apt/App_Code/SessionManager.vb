'----------------------------------------------------------------------------------------------
' Filename    : SessionManager.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports Microsoft.VisualBasic
Imports System.Configuration.ConfigurationManager
Imports System.Web.HttpContext
Imports aptBusinessLogic
Imports GenericUtilities
Imports aptEntities
Imports System.Web

Public Class SessionManager

    Public Shared Property LoggedInUserId As Integer
        Get
            If Current.Session("_loggedInUserId") Is Nothing Then

                Dim currentUser As User = UserManager.GetCurrentLDAPUser()
                Dim currentAptUser As AptUser = Nothing

                If currentUser IsNot Nothing Then
                    currentAptUser = UserManager.GetUserByUsername(currentUser.Username)
                End If

                If currentAptUser IsNot Nothing Then
                    If currentAptUser.Active = True Then
                        Current.Session("_loggedInUserId") = currentAptUser.ID
                    End If
                End If

            End If

            If Current.Session("_loggedInUserId") Is Nothing Then
                HttpContext.Current.Response.Redirect("~/Login.aspx")
            End If

            Return Current.Session("_loggedInUserId")

        End Get
        Set(ByVal value As Integer)
            Current.Session("_loggedInUserId") = value
        End Set
    End Property

End Class
