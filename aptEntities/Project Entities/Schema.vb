'----------------------------------------------------------------------------------------------
' Filename    : Schema.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_schema")>
Public Class Schema

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

#End Region

#Region "Foreign Keys"

    <Column(Name:="type_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property EntityId As Nullable(Of Integer)

    Private _entity As New EntityRef(Of Entity)
    <Association(Storage:="_entity", Thiskey:="EntityId", IsForeignKey:=True)> _
    Public Property Entity() As Entity
        Get
            Return Me._entity.Entity
        End Get
        Set(ByVal value As Entity)
            Me._entity.Entity = value
        End Set
    End Property

#End Region

End Class
