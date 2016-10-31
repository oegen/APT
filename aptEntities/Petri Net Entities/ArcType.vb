'----------------------------------------------------------------------------------------------
' Filename    : ArcType.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_arc_type")>
Public Class ArcType

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

#End Region

End Class
