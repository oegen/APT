'----------------------------------------------------------------------------------------------
' Filename    : Timesheet.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_new_timesheet")>
Public Class Timesheet

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="context_entity_id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ContextEntityId As Integer

    <Column(Name:="entity_parent_id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property EntityParentId As Integer

    <Column(Name:="hour_spent", UpdateCheck:=UpdateCheck.Never)> _
    Public Property HourSpent As Integer

    <Column(Name:="minutes_spent", UpdateCheck:=UpdateCheck.Never)> _
    Public Property MinutesSpent As Integer

    <Column(Name:="date_of_work", UpdateCheck:=UpdateCheck.Never)> _
    Public Property DateOfWork As Date

    <Column(Name:="entry_date", UpdateCheck:=UpdateCheck.Never)> _
    Public Property EntryDate As DateTime

#End Region

#Region "Foreign Keys"

    <Column(Name:="reason_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ReasonID As Integer

    Private _reason As New EntityRef(Of TimesheetReason)
    <Association(Storage:="_reason", Thiskey:="ReasonID", IsForeignKey:=True)> _
    Public Property Reason() As TimesheetReason
        Get
            Return Me._reason.Entity
        End Get
        Set(ByVal value As TimesheetReason)
            Me._reason.Entity = value
        End Set
    End Property

    <Column(Name:="user_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property UserID As Integer

    Private _user As New EntityRef(Of AptUser)
    <Association(Storage:="_user", Thiskey:="UserID", IsForeignKey:=True)> _
    Public Property User() As AptUser
        Get
            Return Me._user.Entity
        End Get
        Set(ByVal value As AptUser)
            Me._user.Entity = value
        End Set
    End Property

#End Region

#Region "Custom Properties"

    Public ReadOnly Property TimeSpentShortHand As String
        Get
            Return String.Format("{0}h {1}m", HourSpent, MinutesSpent)
        End Get
    End Property

#End Region

End Class
