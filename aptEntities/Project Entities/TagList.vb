'----------------------------------------------------------------------------------------------
' Filename    : TagList.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_project_tags")>
Public Class TagList

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

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

    <Column(Name:="tag_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property TagID As Integer

    Private _tag As New EntityRef(Of Tag)
    <Association(Storage:="_tag", Thiskey:="TagID", IsForeignKey:=True)> _
    Public Property Tag() As Tag
        Get
            Return Me._tag.Entity
        End Get
        Set(ByVal value As Tag)
            Me._tag.Entity = value
        End Set
    End Property

#End Region

End Class
