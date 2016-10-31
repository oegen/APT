'----------------------------------------------------------------------------------------------
' Filename    : LDAPManager.vb
' Description : Basically just calls the LDAP functionality within generic utilities.
'
' Release Initials  Date        Comment
' 1       LP        20/05/2011  First release.
'----------------------------------------------------------------------------------------------

Imports GenericUtilities

Public Module LDAPManager

#Region "Constants"

    Public Const DEFAULT_ATTRIBUTE As String = "sAMAccountName"
    Public Const SURNAME_ATTRIBUTE As String = "sn"

#End Region

#Region "Get Variations"

    Public Function GetUser(ByVal value As String, Optional ByVal attribute As String = DEFAULT_ATTRIBUTE) As User
        modLogManager.Debug(String.Format("About to get user from LDAP (value = {0}, attribute = {1})", value, attribute))

        Dim userObj As User = LDAPCalls.GetUser(value, attribute)

        modLogManager.Debug(String.Format("Got user with name {0} and username {1}", userObj.FullName, userObj.Username))

        Return userObj
    End Function

    Public Function GetCurrentUser() As User
        Return LDAPCalls.GetUser(LDAPCalls.GetServerVariable())
    End Function

#End Region

#Region "Searching"

    Public Function SearchUsers(ByVal searchValue As String, Optional ByVal searchAttribute As String = SURNAME_ATTRIBUTE, Optional ByVal containsUser As Boolean = True) As List(Of User)
        modLogManager.Debug(String.Format("Searching for attribute '{0}' with search value '{1}'", searchAttribute, searchValue))

        Return LDAPCalls.SearchUsers(searchValue, searchAttribute, containsUser)
    End Function

#End Region

End Module
