'----------------------------------------------------------------------------------------------
' Filename    : ProjectDocument.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_project_documents")>
Public Class ProjectDocument

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="path", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Path As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

    <Column(Name:="collection_index", UpdateCheck:=UpdateCheck.Never)> _
    Public Property CollectionIndex As Integer

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

    <Column(Name:="secret_key", UpdateCheck:=UpdateCheck.Never)> _
    Public Property SecretKey As String

#End Region

#Region "Foreign Keys"

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

    <Column(Name:="category_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ProjectCategoryID As Integer

    Private _projectCategory As New EntityRef(Of ProjectDocumentCategory)
    <Association(Storage:="_projectCategory", Thiskey:="ProjectCategoryID", IsForeignKey:=True)> _
    Public Property Category() As ProjectDocumentCategory
        Get
            Return Me._projectCategory.Entity
        End Get
        Set(ByVal value As ProjectDocumentCategory)
            Me._projectCategory.Entity = value
        End Set
    End Property

#End Region

#Region "Custom Properties"

    Public ReadOnly Property FileExtension As String
        Get
            Return System.IO.Path.GetExtension(Path).ToLowerInvariant()
        End Get
    End Property

#End Region
End Class
