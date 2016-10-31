'----------------------------------------------------------------------------------------------
' Filename    : ProjectAccess.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_project_access")>
Public Class ProjectAccess

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="parent_id", UpdateCheck:=UpdateCheck.Never)>
    Public Property ParentID As Integer

#End Region

#Region "Foreign Keys"
    <Column(Name:="project_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ProjectID As Integer

    Private _project As New EntityRef(Of Project)
    <Association(Storage:="_project", Thiskey:="ProjectID", IsForeignKey:=True)> _
    Public Property Project() As Project
        Get
            Return Me._project.Entity
        End Get
        Set(ByVal value As Project)
            Me._project.Entity = value
        End Set
    End Property

    <Column(Name:="security_level_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property SecurityLevelID As Integer

    Private _securityLevel As New EntityRef(Of ProjectSecurityLevel)
    <Association(Storage:="_securityLevel", Thiskey:="SecurityLevelID", IsForeignKey:=True)> _
    Public Property SecurityLevel() As ProjectSecurityLevel
        Get
            Return Me._securityLevel.Entity
        End Get
        Set(ByVal value As ProjectSecurityLevel)
            Me._securityLevel.Entity = value
        End Set
    End Property

    <Column(Name:="project_access_level_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ProjectAccessLevelID As Integer

    Private _projectAccessLevel As New EntityRef(Of ProjectAccessLevel)
    <Association(Storage:="_projectAccessLevel", Thiskey:="ProjectAccessLevelID", IsForeignKey:=True)> _
    Public Property ProjectAccessLevel() As ProjectAccessLevel
        Get
            Return Me._projectAccessLevel.Entity
        End Get
        Set(ByVal value As ProjectAccessLevel)
            Me._projectAccessLevel.Entity = value
        End Set
    End Property
#End Region

End Class
