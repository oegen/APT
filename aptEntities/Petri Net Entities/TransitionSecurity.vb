'----------------------------------------------------------------------------------------------
' Filename    : TransitionSecurity.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_transition_security")>
Public Class TransitionSecurity

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

#End Region

#Region "Foreign Keys"

    <Column(Name:="parent_id", UpdateCheck:=UpdateCheck.Never)>
    Public Property ParentID As Integer

    <Column(Name:="transition_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property TransitionID As Integer

    Private _transition As New EntityRef(Of Transition)
    <Association(Storage:="_transition", Thiskey:="TransitionID", IsForeignKey:=True)> _
    Public Property Transition() As Transition
        Get
            Return Me._transition.Entity
        End Get
        Set(ByVal value As Transition)
            Me._transition.Entity = value
        End Set
    End Property

    <Column(Name:="security_lookup_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property SecurityLookupID As Integer

    Private _securityLookup As New EntityRef(Of SecurityLookup)
    <Association(Storage:="_securityLookup", Thiskey:="SecurityLookupID", IsForeignKey:=True)> _
    Public Property SecurityLookup() As SecurityLookup
        Get
            Return Me._securityLookup.Entity
        End Get
        Set(ByVal value As SecurityLookup)
            Me._securityLookup.Entity = value
        End Set
    End Property

    <Column(Name:="access_level_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property AccessLevelID As Integer

    Private _accessLevel As New EntityRef(Of AccessLevel)
    <Association(Storage:="_accessLevel", Thiskey:="AccessLevelID", IsForeignKey:=True)> _
    Public Property Department() As AccessLevel
        Get
            Return Me._accessLevel.Entity
        End Get
        Set(ByVal value As AccessLevel)
            Me._accessLevel.Entity = value
        End Set
    End Property

#End Region

End Class
