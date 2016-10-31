'----------------------------------------------------------------------------------------------
' Filename    : Arc.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_arc")>
Public Class Arc

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="direction", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Direction As String

    <Column(Name:="pre_condition", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PreCondition As String

    <Column(Name:="pre_condition_text", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PreConditionText As String

    <Column(Name:="alert", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Alert As String

#End Region

#Region "Foreign Keys"

    <Column(Name:="workflow_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property WorkflowID As Integer

    Private _workflow As New EntityRef(Of Workflow)
    <Association(Storage:="_workflow", Thiskey:="WorkflowID", IsForeignKey:=True)> _
    Public Property Workflow() As Workflow
        Get
            Return Me._workflow.Entity
        End Get
        Set(ByVal value As Workflow)
            Me._workflow.Entity = value
        End Set
    End Property

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

    <Column(Name:="arc_type_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ArcTypeID As Integer

    Private _arcType As New EntityRef(Of ArcType)
    <Association(Storage:="_arcType", Thiskey:="ArcTypeID", IsForeignKey:=True)> _
    Public Property ArcType() As ArcType
        Get
            Return Me._arcType.Entity
        End Get
        Set(ByVal value As ArcType)
            Me._arcType.Entity = value
        End Set
    End Property

#End Region

End Class
