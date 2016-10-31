'----------------------------------------------------------------------------------------------
' Filename    : Case.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_case")>
Public Class wfCase

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

#End Region

#Region "Foreign Keys"

    <Column(Name:="context_parent_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ContextParentID As Integer

    Private _contextParent As New EntityRef(Of Project)
    <Association(Storage:="_contextParent", Thiskey:="ContextParentID", IsForeignKey:=True)> _
    Public Property Project() As Project
        Get
            Return Me._contextParent.Entity
        End Get
        Set(ByVal value As Project)
            Me._contextParent.Entity = value
        End Set
    End Property

    <Column(Name:="workflow_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property workflowID As Integer

    Private _workflow As New EntityRef(Of Workflow)
    <Association(Storage:="_workflow", Thiskey:="workflowID", IsForeignKey:=True)> _
    Public Property Workflow() As Workflow
        Get
            Return Me._workflow.Entity
        End Get
        Set(ByVal value As Workflow)
            Me._workflow.Entity = value
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

    <Column(Name:="case_status_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property CaseStatusID As Integer

    Private _caseStatus As New EntityRef(Of CaseStatus)
    <Association(Storage:="_caseStatus", Thiskey:="CaseStatusID", IsForeignKey:=True)> _
    Public Property CaseStatus() As CaseStatus
        Get
            Return Me._caseStatus.Entity
        End Get
        Set(ByVal value As CaseStatus)
            Me._caseStatus.Entity = value
        End Set
    End Property

#End Region

End Class
