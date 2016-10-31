'----------------------------------------------------------------------------------------------
' Filename    : Audit.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_audit")>
Public Class Audit

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="datetime", UpdateCheck:=UpdateCheck.Never)> _
    Public Property AuditDate As DateTime

    <Column(Name:="message", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Message As String

#End Region

#Region "Foreign Keys"
    <Column(Name:="user_id", UpdateCheck:=UpdateCheck.Never)> _
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

    <Column(Name:="change_type_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ChangeTypeID As Integer

    Private _changeType As New EntityRef(Of AuditChangeType)
    <Association(Storage:="_changeType", Thiskey:="ChangeTypeID", IsForeignKey:=True)> _
    Public Property ChangeType() As AuditChangeType
        Get
            Return Me._changeType.Entity
        End Get
        Set(ByVal value As AuditChangeType)
            Me._changeType.Entity = value
        End Set
    End Property

    <Column(Name:="section_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property SectionID As Integer

    Private _section As New EntityRef(Of AuditSection)
    <Association(Storage:="_section", Thiskey:="SectionID", IsForeignKey:=True)> _
    Public Property Section() As AuditSection
        Get
            Return Me._section.Entity
        End Get
        Set(ByVal value As AuditSection)
            Me._section.Entity = value
        End Set
    End Property
#End Region

End Class
