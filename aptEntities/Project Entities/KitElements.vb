'----------------------------------------------------------------------------------------------
' Filename    : KitElements.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_kit_contents")>
Public Class KitElement

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="quantity", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Quantity As Integer

    <Column(Name:="item_id", UpdateCheck:=UpdateCheck.Never)>
    Public Property ItemId As Integer

    <Column(Name:="item_type", UpdateCheck:=UpdateCheck.Never)>
    Public Property ItemType As Integer

#End Region

#Region "Foreign Keys"

    '<Column(Name:="kit_id", UpdateCheck:=UpdateCheck.Never)>
    'Public Property KitId As Integer

    'Private _kit As New EntityRef(Of Kit)
    '<Association(Storage:="_kit", Thiskey:="KitId", IsForeignKey:=True)> _
    'Public Property Kit() As Kit
    '    Get
    '        Return Me._kit.Entity
    '    End Get
    '    Set(ByVal value As Kit)
    '        Me._kit.Entity = value
    '    End Set
    'End Property

    <Column(Name:="kit_quote_id", UpdateCheck:=UpdateCheck.Never)>
    Public Property KitQuoteId As Integer

    Private _kitQuote As New EntityRef(Of KitQuote)
    <Association(Storage:="_kitQuote", Thiskey:="KitQuoteId", IsForeignKey:=True)> _
    Public Property KitQuote() As KitQuote
        Get
            Return Me._kitQuote.Entity
        End Get
        Set(ByVal value As KitQuote)
            Me._kitQuote.Entity = value
        End Set
    End Property

#End Region

End Class
