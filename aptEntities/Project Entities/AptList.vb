'----------------------------------------------------------------------------------------------
' Filename    : AptList.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_list")>
Public Class AptList

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

    <Column(Name:="protected", UpdateCheck:=UpdateCheck.Never)> _
    Public Property pProtected As Boolean

    Private _listNodes As New EntitySet(Of ListNode)
    <Association(Storage:="_listNodes", OtherKey:="ListID", ThisKey:="ID")> _
    Public Property ListNodes() As EntitySet(Of ListNode)
        Get
            Return Me._listNodes
        End Get
        Set(ByVal value As EntitySet(Of ListNode))
            Me._listNodes.Assign(value)
        End Set
    End Property

#End Region

End Class
