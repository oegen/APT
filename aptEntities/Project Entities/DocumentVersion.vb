'----------------------------------------------------------------------------------------------
' Filename    : DocVersion.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        04/10/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_doc_versions")>
Public Class DocumentVersion

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="radpdf_doc_id", UpdateCheck:=UpdateCheck.Never)>
    Public Property RadPDFDocumentId As Integer

    <Column(Name:="creation_datetime", UpdateCheck:=UpdateCheck.Never)>
    Public Property CreationDateTime As DateTime

#Region "Foreign Keys"

#Region "Document"

    <Column(Name:="doc_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property DocumentId As Integer

    Private _document As New EntityRef(Of ProjectDocument)
    <Association(Storage:="_document", Thiskey:="DocumentId", IsForeignKey:=True)> _
    Public Property Document() As ProjectDocument
        Get
            Return Me._document.Entity
        End Get
        Set(ByVal value As ProjectDocument)
            Me._document.Entity = value
        End Set
    End Property

#End Region

#Region "User"

    <Column(Name:="user_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property UserId As Integer

    Private _user As New EntityRef(Of AptUser)
    <Association(Storage:="_user", Thiskey:="UserId", IsForeignKey:=True)> _
    Public Property AptUser() As AptUser
        Get
            Return Me._user.Entity
        End Get
        Set(ByVal value As AptUser)
            Me._user.Entity = value
        End Set
    End Property

#End Region

#End Region

End Class
