'----------------------------------------------------------------------------------------------
' Filename    : ListNode.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_list_node")>
Public Class ListNode

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

#End Region

#Region "Foreign Keys"

    <Column(Name:="list_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ListID As Integer

    Private _list As New EntityRef(Of AptList)
    <Association(Storage:="_list", Thiskey:="ListID", IsForeignKey:=True)> _
    Public Property List() As AptList
        Get
            Return Me._list.Entity
        End Get
        Set(ByVal value As AptList)
            Me._list.Entity = value
        End Set
    End Property

#End Region

End Class
