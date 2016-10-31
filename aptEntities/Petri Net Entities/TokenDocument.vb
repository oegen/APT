'----------------------------------------------------------------------------------------------
' Filename    : TokenDocument.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        08/08/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_token_document")>
Public Class TokenDocument

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

#End Region

#Region "Foreign Key"

    <Column(Name:="token_id", UpdateCheck:=UpdateCheck.Never)> _
    Private Property TokenID As Integer

    Private _token As New EntityRef(Of Token)
    <Association(Storage:="_token", Thiskey:="TokenID", IsForeignKey:=True)> _
    Public Property Token() As Token
        Get
            Return Me._token.Entity
        End Get
        Set(ByVal value As Token)
            Me._token.Entity = value
        End Set
    End Property

    <Column(Name:="document_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property DocumentID As Integer

    Private _document As New EntityRef(Of ProjectDocument)
    <Association(Storage:="_document", Thiskey:="DocumentID", IsForeignKey:=True)> _
    Public Property Document() As ProjectDocument
        Get
            Return Me._document.Entity
        End Get
        Set(ByVal value As ProjectDocument)
            Me._document.Entity = value
        End Set
    End Property

#End Region

End Class
