'----------------------------------------------------------------------------------------------
' Filename    : PremiumElementDetails.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_premium_element_details")>
Public Class PremiumElementDetails

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    '<Column(Name:="outline_brief", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property OutlineBrief As String

    '<Column(Name:="does_item_exist_on_bbc", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property DoesItemExistOnBBC As Boolean

    <Column(Name:="stock_code", UpdateCheck:=UpdateCheck.Never)> _
    Public Property StockCode As String

    <Column(Name:="budget_per_item", UpdateCheck:=UpdateCheck.Never)> _
    Public Property BudgetPerItem As Decimal

    <Column(Name:="quantity_breaks", UpdateCheck:=UpdateCheck.Never)> _
    Public Property QuantityBreaks As String

    <Column(Name:="colours", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Colours As String

    <Column(Name:="brand", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Brand As String

    <Column(Name:="brand_position", UpdateCheck:=UpdateCheck.Never)> _
    Public Property BrandPosition As String

    <Column(Name:="number_of_branding", UpdateCheck:=UpdateCheck.Never)> _
    Public Property NumberOfBranding As Integer

    <Column(Name:="size_of_item", UpdateCheck:=UpdateCheck.Never)> _
    Public Property SizeOfItem As String

    <Column(Name:="textile", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Textile As String

    <Column(Name:="purpose_of_item", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PurposeOfItem As String

    <Column(Name:="item_packaging", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ItemPackaging As String

    <Column(Name:="weight_restriction", UpdateCheck:=UpdateCheck.Never)> _
    Public Property WeightRestriction As String

    <Column(Name:="sample_required", UpdateCheck:=UpdateCheck.Never)> _
    Public Property SampleRequired As String

    '<Column(Name:="design_required", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property DesignRequired As String

    <Column(Name:="has_bd_designed_item", UpdateCheck:=UpdateCheck.Never)> _
    Public Property HasBDDesignedItem As String

    <Column(Name:="similar_product", UpdateCheck:=UpdateCheck.Never)> _
    Public Property SimilarProducts As String

#Region "New MDA Change Fields"

    <Column(Name:="quote_cost", UpdateCheck:=UpdateCheck.Never)> _
    Public Property QuoteCost As Decimal

    <Column(Name:="supplier", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Supplier As String

    <Column(Name:="eta_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property EtaDate As DateTime

    <Column(Name:="expiry_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ExpiryDate As Nullable(Of DateTime)

    <Column(Name:="image_uploaded", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ImageUploaded As Boolean

    <Column(Name:="final_requirements", UpdateCheck:=UpdateCheck.Never)> _
    Public Property FinalRequirement As String

#End Region

#Region "Even more MDA Changes 2"

    <Column(Name:="brief_submitted", UpdateCheck:=UpdateCheck.Never)> _
    Public Property BriefSubmittedDate As Nullable(Of DateTime)

    <Column(Name:="proposal_required_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ProposalRequiredDate As Nullable(Of DateTime)

    <Column(Name:="quote_provided_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property QuoteProvidedDate As Nullable(Of DateTime)

    <Column(Name:="artwork_available_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ArtworkAvailableDate As Nullable(Of DateTime)

    <Column(Name:="po_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PODate As Nullable(Of DateTime)

    <Column(Name:="delivery_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property DeliveryDate As Nullable(Of DateTime)

    <Column(Name:="delivery_address", UpdateCheck:=UpdateCheck.Never)> _
    Public Property DeliveryAddress As String

#End Region

#End Region

#Region "Foreign Keys"

    <Column(Name:="additional_element_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property AdditionalElementId As Integer

    Private _additionalElement As New EntityRef(Of AdditionalElement)
    <Association(Storage:="_additionalElement", Thiskey:="AdditionalElementId", IsForeignKey:=True)> _
    Public Property AdditionalElement() As AdditionalElement
        Get
            Return Me._additionalElement.Entity
        End Get
        Set(ByVal value As AdditionalElement)
            Me._additionalElement.Entity = value
        End Set
    End Property

#End Region

End Class
