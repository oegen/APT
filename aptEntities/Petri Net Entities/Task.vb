'----------------------------------------------------------------------------------------------
' Filename    : Task.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_task")>
Public Class Task

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)>
    Public Property Name As String

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)>
    Public Property Description As String

    <Column(Name:="script", UpdateCheck:=UpdateCheck.Never)>
    Public Property Script As String

#End Region

#Region "Foreign Key"

    <Column(Name:="entity_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property EntityID As Integer

    Private _entity As New EntityRef(Of Entity)
    <Association(Storage:="_entity", Thiskey:="EntityID", IsForeignKey:=True)> _
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
