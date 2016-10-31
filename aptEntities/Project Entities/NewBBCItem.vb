'----------------------------------------------------------------------------------------------
' Filename    : NewBBCItem.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_bbc_item")>
Public Class NewBBCItem

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="part_number", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PartNumber As String

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Description As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean = True

#Region "Foreign Key"

    <Column(Name:="brand_list_item_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property BrandListItemId As Integer

    Private _brandListItem As New EntityRef(Of ListNode)
    <Association(Storage:="_brandListItem", Thiskey:="BrandListItemId", IsForeignKey:=True)> _
    Public Property Brand() As ListNode
        Get
            Return Me._brandListItem.Entity
        End Get
        Set(ByVal value As ListNode)
            Me._brandListItem.Entity = value
        End Set
    End Property

#End Region

#Region "Custom Properties"

    ' Brand Name - only used for binding convenience

    Public ReadOnly Property BrandName() As String
        Get
            If BrandListItemId <> 0 Then
                Return Brand.Name
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property CodePartNumberDisplayString
        Get
            Return String.Format("{0} - {1}", PartNumber, Description)
        End Get
    End Property


#End Region

End Class
