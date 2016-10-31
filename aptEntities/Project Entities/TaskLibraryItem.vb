'----------------------------------------------------------------------------------------------
' Filename    : TaskLibraryItem.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_task_library_item")>
Public Class TaskLibraryItem

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="associated_page", UpdateCheck:=UpdateCheck.Never)>
    Public Property AssociatedPage As String

#End Region

#Region "Foreign Keys"

    <Column(Name:="task_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property TaskID As Integer

    Private _task As New EntityRef(Of Task)
    <Association(Storage:="_task", Thiskey:="TaskID", IsForeignKey:=True)> _
    Public Property Task() As Task
        Get
            Return Me._task.Entity
        End Get
        Set(ByVal value As Task)
            Me._task.Entity = value
        End Set
    End Property

#End Region


End Class
