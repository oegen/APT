'----------------------------------------------------------------------------------------------
' Filename    : SchemaDefinition.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_schema_definition")>
Public Class SchemaDefinition

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="schema_element_type", UpdateCheck:=UpdateCheck.Never)> _
    Public Property SchemaElementType As String

    <Column(Name:="schema_element_required", UpdateCheck:=UpdateCheck.Never)> _
    Public Property SchemaElementRequired As Boolean

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

#End Region

#Region "Foreign Keys"

    <Column(Name:="schema_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property SchemaID As Integer

    Private _schema As New EntityRef(Of Schema)
    <Association(Storage:="_schema", Thiskey:="SchemaID", IsForeignKey:=True)> _
    Public Property Schema() As Schema
        Get
            Return Me._schema.Entity
        End Get
        Set(ByVal value As Schema)
            Me._schema.Entity = value
        End Set
    End Property

    <Column(Name:="list_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ListID As Nullable(Of Integer)

    Private _list As New EntityRef(Of AptList)
    <Association(Storage:="_list", Thiskey:="ListID", IsForeignKey:=True)> _
    Public Property List() As AptList
        Get
            Return Me._list.Entity
        End Get
        Set(ByVal value As AptList)
            Me._list.Entity = value
        End Set
    End Property

#End Region

End Class
