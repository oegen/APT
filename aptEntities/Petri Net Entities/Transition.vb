'----------------------------------------------------------------------------------------------
' Filename    : Transition.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_transition")>
Public Class Transition

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Description As String

    <Column(Name:="days_from_start", UpdateCheck:=UpdateCheck.Never)> _
    Public Property DaysFromStart As Integer

    <Column(Name:="enable_email", UpdateCheck:=UpdateCheck.Never)> _
    Public Property EnableEmail As Boolean

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

#End Region

End Class
