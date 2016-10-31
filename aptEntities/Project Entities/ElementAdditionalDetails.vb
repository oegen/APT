Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_element_additional_details")>
Public Class ElementAdditionalDetails

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="quantity", UpdateCheck:=UpdateCheck.Never)>
    Public Property Quantity As Integer

    <Column(Name:="artwork_time", UpdateCheck:=UpdateCheck.Never)>
    Public Property ArtworkTime As Integer

#Region "Artwork Cost"

    <Column(Name:="artwork_cost", UpdateCheck:=UpdateCheck.Never)>
    Private Property _artworkCost As Decimal

    Public Property ArtworkCost As Decimal
        Get
            Return FormatNumber(_artworkCost, 2)
        End Get
        Set(ByVal value As Decimal)
            _artworkCost = value
        End Set
    End Property

#End Region

    <Column(Name:="print_lead_times", UpdateCheck:=UpdateCheck.Never)>
    Public Property PrintLeadTimes As String

#Region "Print Cost"

    <Column(Name:="print_cost", UpdateCheck:=UpdateCheck.Never)>
    Private Property _printCost As Decimal

    Public Property PrintCost As Decimal
        Get
            Return FormatNumber(_printCost)
        End Get
        Set(ByVal value As Decimal)
            _printCost = value
        End Set
    End Property

#End Region

    <Column(Name:="cost_per_item", UpdateCheck:=UpdateCheck.Never)>
    Public Property CostPerItem As Decimal



#End Region

#Region "Foreign Keys"

    <Column(Name:="element_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ElementID As Integer

    Private _element As New EntityRef(Of Element)
    <Association(Storage:="_element", Thiskey:="ElementID", IsForeignKey:=True)> _
    Public Property Element() As Element
        Get
            Return Me._element.Entity
        End Get
        Set(ByVal value As Element)
            Me._element.Entity = value
        End Set
    End Property

#End Region

End Class
