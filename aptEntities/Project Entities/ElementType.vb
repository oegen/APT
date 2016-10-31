'----------------------------------------------------------------------------------------------
' Filename    : ElementType.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_type")>
Public Class ElementType

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean = True

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

#End Region

End Class
