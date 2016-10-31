'----------------------------------------------------------------------------------------------
' Filename    : UserRole.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_user_role")>
Public Class UserRole

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

#End Region

#Region "Foreign Keys"

    <Column(Name:="user_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property UserID As Integer

    Private _user As New EntityRef(Of AptUser)
    <Association(Storage:="_user", Thiskey:="UserID", IsForeignKey:=True)> _
    Public Property User() As AptUser
        Get
            Return Me._user.Entity
        End Get
        Set(ByVal value As AptUser)
            Me._user.Entity = value
        End Set
    End Property

    <Column(Name:="role_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property RoleID As Integer

    Private _role As New EntityRef(Of Role)
    <Association(Storage:="_role", Thiskey:="RoleID", IsForeignKey:=True)> _
    Public Property Role() As Role
        Get
            Return Me._role.Entity
        End Get
        Set(ByVal value As Role)
            Me._role.Entity = value
        End Set
    End Property

#End Region

End Class
