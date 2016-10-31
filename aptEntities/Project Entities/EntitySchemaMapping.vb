'----------------------------------------------------------------------------------------------
' Filename    : EntitySchemaMapping.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_entity_schema_mapping")>
Public Class EntitySchemaMapping

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

#End Region

#Region "Foreign Keys"

    <Column(Name:="parent_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ParentID As Integer

    ' TODO - Find association
    'Private _parent As New EntityRef(Of Parent)
    '<Association(Storage:="_parent", Thiskey:="ParentID", IsForeignKey:=True)> _
    'Public Property Parent() As Parent
    '    Get
    '        Return Me._parent.Entity
    '    End Get
    '    Set(ByVal value As Parent)
    '        Me._parent.Entity = value
    '    End Set
    'End Property

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

#End Region

End Class
