'----------------------------------------------------------------------------------------------
' Filename    : ProjectKittingBrief.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_project_kitting_brief")>
Public Class ProjectKittingBrief

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="built_by_mda", UpdateCheck:=UpdateCheck.Never)> _
    Public Property BuiltByMDA As Boolean

    '<Column(Name:="collation_budget_code", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property CollationBudgetCode As String

    <Column(Name:="collation_budget_code", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Funding As String

    <Column(Name:="stock_code", UpdateCheck:=UpdateCheck.Never)> _
    Public Property StockCode As String

    ' This has been taken out
    '<Column(Name:="no_part_per_kit", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property NoPartsPerKit As Integer

    <Column(Name:="total_no_kits", UpdateCheck:=UpdateCheck.Never)> _
    Public Property TotalNoKits As Integer

    <Column(Name:="instructions", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Instructions As String

    <Column(Name:="brief_submitted_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property KitsOnStockDate As DateTime ' was BriefSubmittedDate

    ' ALL BELOW HAVE BEEN COMMENTED DUE TO MDA CHANGES

    '<Column(Name:="proposal_required_date", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property ProposalRequiredDate As DateTime

    '<Column(Name:="quote_provided_date", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property QuoteProvidedDate As DateTime

    '<Column(Name:="kits_build_date", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property KitBuildDate As DateTime

    '<Column(Name:="delivery_date", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property DeliveryDate As DateTime

    <Column(Name:="in_house_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property InTradeDate As DateTime ' was in house

    <Column(Name:="expiry_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ExpiryDate As DateTime

    ' New MDA Fields

    <Column(Name:="total_kit_quantity", UpdateCheck:=UpdateCheck.Never)> _
    Public Property TotalKitQuantity As Integer

    <Column(Name:="costs", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Costs As Decimal

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

    <Column(Name:="kit_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property KitID As Integer

    Private _kit As New EntityRef(Of Kit)
    <Association(Storage:="_kit", Thiskey:="KitID", IsForeignKey:=True)> _
    Public Property Kit() As Kit
        Get
            Return Me._kit.Entity
        End Get
        Set(ByVal value As Kit)
            Me._kit.Entity = value
        End Set
    End Property

#End Region

End Class
