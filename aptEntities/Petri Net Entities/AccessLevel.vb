'----------------------------------------------------------------------------------------------
' Filename    : AccessLevel.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_access_level")>
Public Class AccessLevel

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="level", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Level As Integer

    <Column(Name:="short_description", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ShortDescription As String

#End Region

End Class
