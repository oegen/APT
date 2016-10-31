'----------------------------------------------------------------------------------------------
' Filename    : WorkingWeekException.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_working_week_exception")>
Public Class WorkingWeekException

#Region "Constructor"

    Public Sub New()
        Active = True
    End Sub

#End Region

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    '<Column(Name:="start_time", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property StartTime As Integer

    '<Column(Name:="end_time", UpdateCheck:=UpdateCheck.Never)> _
    'Public Property EndTime As Integer

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Description As String

    <Column(Name:="hours", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Hours As Integer

#End Region

#Region "Foreign Keys"

    <Column(Name:="user_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property UserID As Integer

    Private _userObj As New EntityRef(Of AptUser)
    <Association(Storage:="_userObj", Thiskey:="UserID", IsForeignKey:=True)> _
    Public Property User() As AptUser
        Get
            Return Me._userObj.Entity
        End Get
        Set(ByVal value As AptUser)
            Me._userObj.Entity = value
        End Set
    End Property

    '<Column(Name:="weekday_id", UpdateCheck:=UpdateCheck.Never)>
    'Private Property WeekdayID As Integer

    'Private _weekday As New EntityRef(Of WorkingWeek)
    '<Association(Storage:="_weekday", Thiskey:="WeekdayID", IsForeignKey:=True)> _
    'Public Property Weekday() As WorkingWeek
    '    Get
    '        Return Me._weekday.Entity
    '    End Get
    '    Set(ByVal value As WorkingWeek)
    '        Me._weekday.Entity = value
    '    End Set
    'End Property

#End Region

End Class
