'----------------------------------------------------------------------------------------------
' Filename    : KitQuote.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        21/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_kit_quote")>
Public Class KitQuote

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="number", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Number As Integer

    <Column(Name:="quantity", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Quantity As Integer

    <Column(Name:="kit_packing", UpdateCheck:=UpdateCheck.Never)> _
    Public Property KitPacking As String

    <Column(Name:="distribution", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Distribution As String

    <Column(Name:="picking", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Picking As String

    <Column(Name:="manual_order_entry", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ManualOrderEntry As String

    <Column(Name:="comments", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Comments As String

    <Column(Name:="multiple_order_entry", UpdateCheck:=UpdateCheck.Never)> _
    Public Property MultipleOrderEntry As String

    <Column(Name:="sales_funded", UpdateCheck:=UpdateCheck.Never)> _
    Public Property SalesFunded As Integer

    <Column(Name:="brand_funded", UpdateCheck:=UpdateCheck.Never)> _
    Public Property BrandFunded As Integer

    <Column(Name:="budget_code", UpdateCheck:=UpdateCheck.Never)> _
    Public Property BudgetCode As String

    <Column(Name:="cost_centre", UpdateCheck:=UpdateCheck.Never)> _
    Public Property CostCentre As String

    <Column(Name:="project_code", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ProjectCode As String

    <Column(Name:="is_brand_funded", UpdateCheck:=UpdateCheck.Never)> _
    Public Property IsBrandFunded As Boolean

#End Region

#Region "Foreign Keys"

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
