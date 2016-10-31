'----------------------------------------------------------------------------------------------
' Filename    : SubclassType.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_subclass_type")>
Public Class SubclassType

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean = True

    <Column(Name:="print_required", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PrintRequired As Nullable(Of Boolean)

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

    <Column(Name:="time_hours", UpdateCheck:=UpdateCheck.Never)> _
    Public Property TimeHours As Integer

    <Column(Name:="time_minutes", UpdateCheck:=UpdateCheck.Never)> _
    Public Property TimeMinutes As Integer

    <Column(Name:="print_lead_time", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PrintLeadTime As Integer

    '<Column(Name:="average_hours", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property AverageHours As Integer

    '<Column(Name:="average_minutes", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property AverageMinutes As Integer

#End Region

#Region "Foreign Keys"

    <Column(Name:="type_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property TypeID As Integer

    Private _type As New EntityRef(Of ElementType)
    <Association(Storage:="_type", Thiskey:="TypeID", IsForeignKey:=True)> _
    Public Property Type() As ElementType
        Get
            Return Me._type.Entity
        End Get
        Set(ByVal value As ElementType)
            Me._type.Entity = value
        End Set
    End Property

    <Column(Name:="element_schema_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ElementSchemaID As Nullable(Of Integer)

    Private _elementSchema As New EntityRef(Of Schema)
    <Association(Storage:="_elementSchema", Thiskey:="ElementSchemaID", IsForeignKey:=True)> _
    Public Property ElementSchema() As Schema
        Get
            Return Me._elementSchema.Entity
        End Get
        Set(ByVal value As Schema)
            Me._elementSchema.Entity = value
        End Set
    End Property

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

#End Region

#Region "Custom Properties"

    Public ReadOnly Property TotalTimeInHours As Integer
        Get
            Return TimeHours + Math.Ceiling((TimeMinutes / 60))
        End Get
    End Property

#End Region


End Class
