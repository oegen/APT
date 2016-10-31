'----------------------------------------------------------------------------------------------
' Filename    : BBCItems.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        15/08/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_timesheet_reason")>
Public Class TimesheetReason

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="reason_text", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ReasonText As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

End Class
