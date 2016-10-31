'----------------------------------------------------------------------------------------------
' Filename    : BBCItems.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_block_meeting")>
Public Class BlockMeetingException

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="start_date", UpdateCheck:=UpdateCheck.Never)>
    Public Property StartDate As DateTime

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)>
    Public Property Description As String

    <Column(Name:="hours", UpdateCheck:=UpdateCheck.Never)>
    Public Property Hours As Integer = 0

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)>
    Public Property Active As Boolean = True

End Class
