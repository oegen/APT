'----------------------------------------------------------------------------------------------
' Filename    : WorkItem.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_workitem")>
Public Class WorkItem

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="enabled_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property EnabledDate As Nullable(Of DateTime)

    <Column(Name:="cancelled_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property CancelledDate As Nullable(Of DateTime)

    <Column(Name:="finished_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property FinishedDate As Nullable(Of DateTime)

    <Column(Name:="deadline", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Deadline As Nullable(Of DateTime)

    <Column(Name:="context_parent_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ContextParentID As Integer

    <Column(Name:="workitem_status_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property WorkItemStatusID As Integer

    <Column(Name:="user_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property UserID As Nullable(Of Integer)

#End Region

#Region "Foreign Keys"

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

    <Column(Name:="trigger_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property TriggerID As Integer

    Private _trigger As New EntityRef(Of Trigger)
    <Association(Storage:="_trigger", Thiskey:="TriggerID", IsForeignKey:=True)> _
    Public Property Trigger() As Trigger
        Get
            Return Me._trigger.Entity
        End Get
        Set(ByVal value As Trigger)
            Me._trigger.Entity = value
        End Set
    End Property

    <Column(Name:="task_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property TaskID As Integer

    Private _task As New EntityRef(Of Task)
    <Association(Storage:="_task", Thiskey:="TaskID", IsForeignKey:=True)> _
    Public Property Task() As Task
        Get
            Return Me._task.Entity
        End Get
        Set(ByVal value As Task)
            Me._task.Entity = value
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

#End Region

End Class
