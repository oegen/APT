Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_element_kitting_details")>
Public Class ElementKittingDetails

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="cost_per_item", UpdateCheck:=UpdateCheck.Never)> _
    Public Property CostPerItem As Decimal

    <Column(Name:="expiry_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ExpiryDate As Nullable(Of Date)

    <Column(Name:="po_number", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PONumber As String

    <Column(Name:="existing", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Existing As Boolean

    <Column(Name:="supplier", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Supplier As String

    <Column(Name:="due_date_into_mda", UpdateCheck:=UpdateCheck.Never)> _
    Public Property DueDateIntoMDA As Nullable(Of Date)

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
