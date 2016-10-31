Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_element_artwork_details")>
Public Class ElementArtworkDetails

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="no_of_colours", UpdateCheck:=UpdateCheck.Never)> _
    Public Property NoOfColours As Integer

    <Column(Name:="finished_size", UpdateCheck:=UpdateCheck.Never)> _
    Public Property FinishedSize As String

    <Column(Name:="material", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Material As String

    <Column(Name:="finishing", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Finishing As String

    <Column(Name:="no_of_del_adds", UpdateCheck:=UpdateCheck.Never)> _
    Public Property NoOfDelAdds As Integer

    <Column(Name:="delivery_details", UpdateCheck:=UpdateCheck.Never)> _
    Public Property DeliveryDetails As String

    <Column(Name:="pack_size", UpdateCheck:=UpdateCheck.Never)> _
    Public Property PackSize As Integer

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
