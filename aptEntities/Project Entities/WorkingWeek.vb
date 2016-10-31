'----------------------------------------------------------------------------------------------
' Filename    : WorkingWeek.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_working_week")>
Public Class WorkingWeek

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="weekday", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Weekday As String

    <Column(Name:="start_time", UpdateCheck:=UpdateCheck.Never)> _
    Public Property StartTime As Integer

    <Column(Name:="end_time", UpdateCheck:=UpdateCheck.Never)> _
    Public Property EndTime As Integer

#End Region

End Class
