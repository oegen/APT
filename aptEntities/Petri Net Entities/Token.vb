'----------------------------------------------------------------------------------------------
' Filename    : Token.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_token")>
Public Class Token

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="enabled_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property EnabledDate As Nullable(Of DateTime)

    <Column(Name:="cancelled_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property CancelledDate As Nullable(Of DateTime)

    <Column(Name:="consumed_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ConsumedDate As Nullable(Of DateTime)

    <Column(Name:="comment", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Comment As String

    <Column(Name:="rejected", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Rejected As Boolean

#End Region

#Region "Foreign Keys"

    <Column(Name:="context_parent_id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ContextParentID As Integer

    <Column(Name:="project_id", UpdateCheck:=UpdateCheck.Never)> _
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

    <Column(Name:="case_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property CaseID As Integer

    Private _case As New EntityRef(Of wfCase)
    <Association(Storage:="_case", Thiskey:="CaseID", IsForeignKey:=True)> _
    Public Property cCase() As wfCase
        Get
            Return Me._case.Entity
        End Get
        Set(ByVal value As wfCase)
            Me._case.Entity = value
        End Set
    End Property

    <Column(Name:="place_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property PlaceID As Integer

    Private _place As New EntityRef(Of Place)
    <Association(Storage:="_place", Thiskey:="PlaceID", IsForeignKey:=True)> _
    Public Property Place() As Place
        Get
            Return Me._place.Entity
        End Get
        Set(ByVal value As Place)
            Me._place.Entity = value
        End Set
    End Property

    <Column(Name:="context_entity_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ContextEntityID As Integer

    Private _contextEntity As New EntityRef(Of Entity)
    <Association(Storage:="_contextEntity", Thiskey:="ContextEntityID", IsForeignKey:=True)> _
    Public Property ContextEntity() As Entity
        Get
            Return Me._contextEntity.Entity
        End Get
        Set(ByVal value As Entity)
            Me._contextEntity.Entity = value
        End Set
    End Property

    <Column(Name:="token_status_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property TokenStatusID As Integer

    Private _tokenStatus As New EntityRef(Of TokenStatus)
    <Association(Storage:="_tokenStatus", Thiskey:="TokenStatusID", IsForeignKey:=True)> _
    Public Property TokenStatus() As TokenStatus
        Get
            Return Me._tokenStatus.Entity
        End Get
        Set(ByVal value As TokenStatus)
            Me._tokenStatus.Entity = value
        End Set
    End Property

    <Column(Name:="user_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property UserID As Integer

    Private _user As New EntityRef(Of AptUser)
    <Association(Storage:="_user", Thiskey:="UserID", IsForeignKey:=True)> _
    Public Property AptUser() As AptUser
        Get
            Return Me._user.Entity
        End Get
        Set(ByVal value As AptUser)
            Me._user.Entity = value
        End Set
    End Property

#End Region

#Region "Constructor"
    Public Sub New()
        Rejected = False
    End Sub
#End Region

End Class
