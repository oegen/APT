'----------------------------------------------------------------------------------------------
' Filename    : Kit.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_kit")>
Public Class Kit

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    '<Column(Name:="quantity", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property Quantity As Integer

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

#Region "Cost"

    '<Column(Name:="cost", UpdateCheck:=UpdateCheck.Never)> _
    'Private Property _cost As Decimal

    'Public Property Cost As Decimal
    '    Get
    '        Return FormatNumber(_Cost)
    '    End Get
    '    Set(ByVal value As Decimal)
    '        _Cost = value
    '    End Set
    'End Property

#End Region

    ' These are gone now (MDA Changes) once client gives ok remove these for good from class and table

    '<Column(Name:="contain_fragile_items", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property ContainFragileItems As Boolean

    '<Column(Name:="contain_high_value", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property ContainHighValue As Boolean

    '<Column(Name:="despatched_to_list_to_be_provided", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property DespatchToListToBeProvided As Boolean

    '<Column(Name:="called_off", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property CalledOff As Boolean

    '<Column(Name:="pre_collated_kit_distribution_only", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property PreCollatedKitDistributionOnly As Boolean


#End Region

    '#Region "New MDA Fields"

    '    <Column(Name:="kit_packing", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property KitPacking As String

    '    <Column(Name:="distribution", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property Distribution As String

    '    <Column(Name:="picking", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property Picking As String

    '    <Column(Name:="manual_order_entry", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property ManualOrderEntry As String

    '    <Column(Name:="comments", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property Comments As String

    '    <Column(Name:="multiple_order_entry", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property MultipleOrderEntry As String

    '    <Column(Name:="sales_funded", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property SalesFunded As Integer

    '    <Column(Name:="brand_funded", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property BrandFunded As Integer

    '    <Column(Name:="budget_code", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property BudgetCode As String

    '    <Column(Name:="cost_centre", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property CostCentre As String

    '    <Column(Name:="project_code", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property ProjectCode As String

    '    <Column(Name:="is_brand_funded", UpdateCheck:=UpdateCheck.Never)> _
    '    Public Property IsBrandFunded As Boolean

    '#End Region

#Region "Foreign Keys"

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

#End Region

End Class
