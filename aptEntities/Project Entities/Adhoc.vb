'----------------------------------------------------------------------------------------------
' Filename    : Adhoc.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_adhoc")>
Public Class Adhoc

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="start_date", UpdateCheck:=UpdateCheck.Never)>
    Public Property StartDate As DateTime

    <Column(Name:="end_date", UpdateCheck:=UpdateCheck.Never)>
    Public Property EndDate As DateTime

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)>
    Public Property Description As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)>
    Public Property Active As Boolean = True

    <Column(Name:="hours", UpdateCheck:=UpdateCheck.Never)>
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

#End Region

#Region "Custom Properties"

    Public ReadOnly Property IsSingleDay As Boolean
        Get
            If StartDate.Date = EndDate.Date Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

#End Region

End Class
