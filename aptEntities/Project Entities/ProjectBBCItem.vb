'----------------------------------------------------------------------------------------------
' Filename    : ProjectBBCItem.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_project_bbc_items")>
Public Class ProjectBBCItem

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="quantity", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Quantity As Integer

    <Column(Name:="pack_quantity", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PackQuantity As Integer

    <Column(Name:="delivery_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property DeliveryDate As DateTime

#End Region

#Region "Foreign Keys"

#Region "Project"

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

#Region "BBC Item"

    <Column(Name:="bbc_item_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property BBCItemId As Integer

    Private _bbcItem As New EntityRef(Of NewBBCItem)
    <Association(Storage:="_bbcItem", Thiskey:="BBCItemId", IsForeignKey:=True)> _
    Public Property BBCItem() As NewBBCItem
        Get
            Return Me._bbcItem.Entity
        End Get
        Set(ByVal value As NewBBCItem)
            Me._bbcItem.Entity = value
        End Set
    End Property

#End Region

#End Region

End Class
