'----------------------------------------------------------------------------------------------
' Filename    : ProjectDocumentCategory.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_project_document_category")>
Public Class ProjectDocumentCategory

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Name As String

    <Column(Name:="active", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Active As Boolean

#End Region

End Class
