'----------------------------------------------------------------------------------------------
' Filename    : Element.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_element")>
Public Class Element

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Description As String

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

    '<Column(Name:="business_area", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property BusinessArea As Integer

    <Column(Name:="amend_time", UpdateCheck:=UpdateCheck.Never)> _
    Public Property AmendTime As Integer

    <Column(Name:="loop_back", UpdateCheck:=UpdateCheck.Never)> _
    Public Property LoopBack As Integer

    <Column(Name:="element_stopped", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ElementStopped As Boolean

    <Column(Name:="no_of_delivery_address", UpdateCheck:=UpdateCheck.Never)> _
    Public Property NumberOfDeliveryAddress As String

    <Column(Name:="delivery_details", UpdateCheck:=UpdateCheck.Never)> _
    Public Property DeliveryDetails As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

    <Column(Name:="artwork_delivery_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ArtworkDeliveryDate As Nullable(Of Date)

    <Column(Name:="item_order", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ItemOrder As Integer

#End Region

#Region "Foreign Keys"

#Region "Schema Data"

    <Column(Name:="subclass_type_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property SubclassTypeID As Integer

    Private _subclassType As New EntityRef(Of SubclassType)
    <Association(Storage:="_subclassType", Thiskey:="SubclassTypeID", IsForeignKey:=True)> _
    Public Property SubclassType() As SubclassType
        Get
            Return Me._subclassType.Entity
        End Get
        Set(ByVal value As SubclassType)
            Me._subclassType.Entity = value
        End Set
    End Property

    <Column(Name:="schema_type_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property SchemaTypeID As Nullable(Of Integer)

    Private _schemaType As New EntityRef(Of Schema)
    <Association(Storage:="_schemaType", Thiskey:="SchemaTypeID", IsForeignKey:=True)> _
    Public Property SchemaType() As Schema
        Get
            Return Me._schemaType.Entity
        End Get
        Set(ByVal value As Schema)
            Me._schemaType.Entity = value
        End Set
    End Property

#End Region

    <Column(Name:="project_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ProjectID As Integer

#Region "List Data"

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

    <Column(Name:="trade_list_node_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property TradeListNodeID As Integer

    Private _tradeListNode As New EntityRef(Of ListNode)
    <Association(Storage:="_tradeListNode", Thiskey:="TradeListNodeID", IsForeignKey:=True)> _
    Public Property TradeListNode() As ListNode
        Get
            Return Me._tradeListNode.Entity
        End Get
        Set(ByVal value As ListNode)
            Me._tradeListNode.Entity = value
        End Set
    End Property

    <Column(Name:="TC_list_node_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property TCListNodeID As Integer

    Private _tcListNode As New EntityRef(Of ListNode)
    <Association(Storage:="_tcListNode", Thiskey:="TCListNodeID", IsForeignKey:=True)> _
    Public Property TCListNode() As ListNode
        Get
            Return Me._tcListNode.Entity
        End Get
        Set(ByVal value As ListNode)
            Me._tcListNode.Entity = value
        End Set
    End Property

    <Column(Name:="brand_node_id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property BrandListNodeID As Integer

    Private _brandListAreaNode As New EntityRef(Of ListNode)
    <Association(Storage:="_brandListAreaNode", Thiskey:="BrandListNodeID", IsForeignKey:=True)> _
    Public Property BrandListNode() As ListNode
        Get
            Return Me._brandListAreaNode.Entity
        End Get
        Set(ByVal value As ListNode)
            Me._brandListAreaNode.Entity = value
        End Set
    End Property

    ' Ideally we obviously don't want NULL foreign keys, but because these are new fields
    ' the existing elements won't have any value for these attributes

    <Column(Name:="page", UpdateCheck:=UpdateCheck.Never)> _
    Private Property PageListNodeID As Nullable(Of Integer)

    Private _pageListNode As New EntityRef(Of ListNode)
    <Association(Storage:="_pageListNode", Thiskey:="PageListNodeID", IsForeignKey:=True)> _
    Public Property PageListNode() As ListNode
        Get
            Return Me._pageListNode.Entity
        End Get
        Set(ByVal value As ListNode)
            Me._pageListNode.Entity = value
        End Set
    End Property

    '<Column(Name:="quantity", UpdateCheck:=UpdateCheck.Never)> _
    'Private Property QuantityListNodeID As Nullable(Of Integer)

    'Private _quantityListNode As New EntityRef(Of ListNode)
    '<Association(Storage:="_quantityListNode", Thiskey:="QuantityListNodeID", IsForeignKey:=True)> _
    'Public Property QuantityListNode() As ListNode
    '    Get
    '        Return Me._quantityListNode.Entity
    '    End Get
    '    Set(ByVal value As ListNode)
    '        Me._quantityListNode.Entity = value
    '    End Set
    'End Property

#End Region

#End Region

#Region "Custom Properties"

    Public ReadOnly Property DisplayString As String
        Get
            If SubclassType IsNot Nothing Then
                Return String.Format("{0} - {1} - {2} {3}", ItemOrder, SubclassType.Type.Name, SubclassType.Name, Name)
            End If

            Return ""
        End Get
    End Property

    Public ReadOnly Property TimesheetsDisplayString As String
        Get
            If SubclassType IsNot Nothing AndAlso Project IsNot Nothing Then
                Return String.Format("{0}-{1} - {2} {3}", Project.ID, ItemOrder, SubclassType.Type.Name, SubclassType.Name)
            End If

            Return ""
        End Get
    End Property

    Public ReadOnly Property AINCodeDisplayString
        Get
            If Project IsNot Nothing And ItemOrder <> 0 Then
                Return String.Format("{0} - {1}", Project.ID, ItemOrder)
            End If

            Return ""
        End Get
    End Property

    Public ReadOnly Property AINTypeDisplayString
        Get
            If Project IsNot Nothing AndAlso ItemOrder <> 0 _
                AndAlso SubclassType IsNot Nothing Then
                Return String.Format("{0} - {1} {2}", Project.ID, SubclassType.Type.Name, SubclassType.Type.Name)
            End If

            Return ""
        End Get
    End Property

#End Region
End Class
