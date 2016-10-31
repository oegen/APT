Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_arc_response")>
Public Class ArcResponse

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="response_text", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ResponseText As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As DateTime

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As DateTime

#End Region

End Class
