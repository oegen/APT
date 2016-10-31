'----------------------------------------------------------------------------------------------
' Filename    : AptUser.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_user")>
Public Class AptUser

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="title", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Title As String

    <Column(Name:="forename", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Forename As String

    <Column(Name:="surname", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Surname As String

    '<Column(Name:="username", UpdateCheck:=UpdateCheck.Never)> _
    ' Public Property Username As String

    <Column(Name:="email_address", UpdateCheck:=UpdateCheck.Never)> _
    Public Property EmailAddress As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

    <Column(Name:="ldap_user", UpdateCheck:=UpdateCheck.Never)> _
    Public Property IsLDAPUser As Boolean

    Public ReadOnly Property FullName As String
        Get
            Return String.Format("{0} {1}", Forename, Surname)
        End Get
    End Property


#End Region

End Class
