'----------------------------------------------------------------------------------------------
' Filename    : WorkitemSecurity.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_workitem_security")>
Public Class WorkitemSecurity

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="parent_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property ParentID As Integer

#End Region

#Region "Foreign Keys"

    <Column(Name:="workitem_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property WorkitemID As Integer

    Private _workitem As New EntityRef(Of WorkItem)
    <Association(Storage:="_workitem", Thiskey:="WorkitemID", IsForeignKey:=True)> _
    Public Property Workitem() As WorkItem
        Get
            Return Me._workitem.Entity
        End Get
        Set(ByVal value As WorkItem)
            Me._workitem.Entity = value
        End Set
    End Property

    <Column(Name:="security_lookup_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property SecurityLookupID As Integer

    Private _securityLookup As New EntityRef(Of SecurityLookup)
    <Association(Storage:="_securityLookup", Thiskey:="WorkitemID", IsForeignKey:=True)> _
    Public Property SecurityLookup() As SecurityLookup
        Get
            Return Me._securityLookup.Entity
        End Get
        Set(ByVal value As SecurityLookup)
            Me._securityLookup.Entity = value
        End Set
    End Property

#End Region

End Class
