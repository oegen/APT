'----------------------------------------------------------------------------------------------
' Filename    : PremiumBrief.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_premium_brief")>
Public Class PremiumBrief

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="total_costing_estimate", UpdateCheck:=UpdateCheck.Never)> _
    Public Property TotalCostingEstimate As Decimal

    <Column(Name:="total_costing_final", UpdateCheck:=UpdateCheck.Never)> _
    Public Property TotalCostingFinal As Decimal

    <Column(Name:="production_time_cost_estimate", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ProductionTimeCostEstimate As Decimal

    <Column(Name:="production_time_cost_final", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ProductionTimeCostFinal As Decimal

    <Column(Name:="previous_cost_estimate", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PreviousCostEstimate As Decimal

    <Column(Name:="previous_cost_final", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PreviousCostFinal As Decimal

    <Column(Name:="previous_supplier", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PreviousSupplier As String

#Region "New MDA Fields"

    <Column(Name:="background_to_brief", UpdateCheck:=UpdateCheck.Never)> _
    Public Property BackgroundToBrief As String

    <Column(Name:="purpose_to_project", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PurposeToProject As String

    <Column(Name:="target_market", UpdateCheck:=UpdateCheck.Never)> _
    Public Property TargetMarket As String

    <Column(Name:="on_trade", UpdateCheck:=UpdateCheck.Never)> _
    Public Property OnTrade As Boolean

    <Column(Name:="further_info", UpdateCheck:=UpdateCheck.Never)> _
    Public Property FurtherInfo As String

#End Region

#End Region

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

    <Column(Name:="mda_manager_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property MDAManagerId As Integer

    Private _mdaManager As New EntityRef(Of AptUser)
    <Association(Storage:="_mdaManager", Thiskey:="MDAManagerId", IsForeignKey:=True)> _
    Public Property MDAManager() As AptUser
        Get
            Return Me._mdaManager.Entity
        End Get
        Set(ByVal value As AptUser)
            Me._mdaManager.Entity = value
        End Set
    End Property

#End Region

End Class
