'----------------------------------------------------------------------------------------------
' Filename    : SchemaData.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_schema_data")>
Public Class SchemaData

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="schema_element_type", UpdateCheck:=UpdateCheck.Never)> _
    Public Property SchemaElementType As String

    <Column(Name:="schema_element_value", UpdateCheck:=UpdateCheck.Never)> _
    Public Property SchemaElementValue As String

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

    <Column(Name:="schema_entity_id", UpdateCheck:=UpdateCheck.Never)>
    Public Property SchemaEntityID As Integer

    ' Can be a project id or a user id
    <Column(Name:="schema_entity_type_id", UpdateCheck:=UpdateCheck.Never)>
    Public Property ParentID As Integer

#End Region

#Region "Foreign Keys"

    <Column(Name:="schema_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property SchemaDefinitionID As Integer

    Private _schemaDefinition As New EntityRef(Of SchemaDefinition)
    <Association(Storage:="_schemaDefinition", Thiskey:="SchemaDefinitionID", IsForeignKey:=True)> _
    Public Property SchemaDefinition() As SchemaDefinition
        Get
            Return Me._schemaDefinition.Entity
        End Get
        Set(ByVal value As SchemaDefinition)
            Me._schemaDefinition.Entity = value
        End Set
    End Property

#End Region

End Class
