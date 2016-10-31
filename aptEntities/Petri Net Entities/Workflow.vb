'----------------------------------------------------------------------------------------------
' Filename    : Workflow.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_workflow")>
Public Class Workflow

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As Integer

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Description As Integer

#End Region

#Region "Foreign Keys"

    <Column(Name:="start_task_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property StartTaskID As Integer

    Private _startTask As New EntityRef(Of Task)
    <Association(Storage:="_startTask", Thiskey:="StartTaskID", IsForeignKey:=True)> _
    Public Property StartTask() As Task
        Get
            Return Me._startTask.Entity
        End Get
        Set(ByVal value As Task)
            Me._startTask.Entity = value
        End Set
    End Property

#End Region

End Class
