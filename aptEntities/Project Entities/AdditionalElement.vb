'----------------------------------------------------------------------------------------------
' Filename    : AdditionalElement.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_additional_element")>
Public Class AdditionalElement

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Description As String

#Region "Cost"

    <Column(Name:="cost", UpdateCheck:=UpdateCheck.Never)> _
    Private Property _cost As Decimal

    Public Property Cost As Decimal
        Get
            Return FormatNumber(_cost, 2)
        End Get
        Set(ByVal value As Decimal)
            _cost = value
        End Set
    End Property

#End Region

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

#End Region

#Region "Foreign Keys"

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

#End Region

End Class


