'----------------------------------------------------------------------------------------------
' Filename    : Project.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Serializable()>
<Table(Name:="apt_tbl_project")>
Public Class Project

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="[desc]", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Description As String

    <Column(Name:="budget_code", UpdateCheck:=UpdateCheck.Never)> _
    Public Property BudgetCode As String

    <Column(Name:="required_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property RequiredDate As Date

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

    <Column(Name:="alert_email_count", UpdateCheck:=UpdateCheck.Never)> _
    Public Property AlertEmailCount As Nullable(Of Integer)

    <Column(Name:="accepted", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Accepted As Boolean

    <Column(Name:="stopped", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Stopped As Boolean

    <Column(Name:="schema_type_id", UpdateCheck:=UpdateCheck.Never)>
    Public Property SchemaTypeID As Integer

    <Column(Name:="project_status_id", UpdateCheck:=UpdateCheck.Never)>
    Public Property ProjectStatusID As Integer

    <Column(Name:="print_required", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PrintRequired As Boolean

    <Column(Name:="required_print_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property RequiredPrintDate As Nullable(Of Date)

    <Column(Name:="is_bd_project", UpdateCheck:=UpdateCheck.Never)>
    Private Property _IsBdProject As Nullable(Of Boolean)

    <Column(Name:="available_budget", UpdateCheck:=UpdateCheck.Never)>
    Private Property _availableBudget As Decimal = 0.0

    Public Property IsBDProject As Boolean
        Get
            If _IsBdProject Is Nothing Then
                Return False
            End If

            Return CType(_IsBdProject, Boolean)
        End Get
        Set(ByVal value As Boolean)
            _IsBdProject = value
        End Set
    End Property

#End Region

#Region "Foreign Keys"

    '<Column(Name:="elements_workflow_state_id", UpdateCheck:=UpdateCheck.Never)>
    'Private Property ElementsWorkflowStateID As Integer

    'Private _elementsWorkflowState As New EntityRef(Of ElementWorkflowState)
    '<Association(Storage:="_elementsWorkflowState", Thiskey:="ElementsWorkflowStateID", IsForeignKey:=True)> _
    'Public Property ElementWorkflowState() As ElementWorkflowState
    '    Get
    '        Return Me._elementsWorkflowState.Entity
    '    End Get
    '    Set(ByVal value As ElementWorkflowState)
    '        Me._elementsWorkflowState.Entity = value
    '    End Set
    'End Property

#End Region

#Region "Custom Properties"

    Public ReadOnly Property AINName As String
        Get
            Return String.Format("{0} - {1}", ID, Name)
        End Get
    End Property

    Public Property AvailableBudget As Decimal
        Get
            Return FormatNumber(_availableBudget, 2)
        End Get
        Set(ByVal value As Decimal)
            _availableBudget = FormatNumber(value, 2)
        End Set
    End Property

#End Region

End Class
