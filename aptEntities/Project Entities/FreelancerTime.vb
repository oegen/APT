﻿'----------------------------------------------------------------------------------------------
' Filename    : ReservedTime.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_freelancer_time")>
Public Class FreelancerTime

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="duration", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Duration As Decimal

    <Column(Name:="start_week", UpdateCheck:=UpdateCheck.Never)> _
    Public Property StartWeek As Integer

    <Column(Name:="year", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Year As Integer

    <Column(Name:="num_weeks", UpdateCheck:=UpdateCheck.Never)> _
    Public Property NumWeeks As Integer

#End Region

#Region "Foriegn Keys"

    <Column(Name:="project_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ProjectID As Integer

    Private _project As New EntityRef(Of Project)
    <Association(Storage:="_project", Thiskey:="ProjectID", IsForeignKey:=True)> _
    Public Property Project() As Project
        Get
            Return Me._project.Entity
        End Get
        Set(ByVal value As Project)
            Me._project.Entity = value
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

End Class
