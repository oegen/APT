'----------------------------------------------------------------------------------------------
' Filename    : Place.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_wf_place")>
Public Class Place

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="name", UpdateCheck:=UpdateCheck.Never)>
    Private Property Name As String

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)>
    Private Property Description As String

#End Region

#Region "Foreign Keys"

    <Column(Name:="workflow_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property WorkflowID As Integer

    Private _workflow As New EntityRef(Of Workflow)
    <Association(Storage:="_workflow", Thiskey:="WorkflowID", IsForeignKey:=True)> _
    Public Property Workflow() As Workflow
        Get
            Return Me._workflow.Entity
        End Get
        Set(ByVal value As Workflow)
            Me._workflow.Entity = value
        End Set
    End Property

    <Column(Name:="place_type_id", UpdateCheck:=UpdateCheck.Never)>
    Private Property PlaceTypeID As Integer

    Private _placeType As New EntityRef(Of PlaceType)
    <Association(Storage:="_placeType", Thiskey:="PlaceTypeID", IsForeignKey:=True)> _
    Public Property PlaceType() As PlaceType
        Get
            Return Me._placeType.Entity
        End Get
        Set(ByVal value As PlaceType)
            Me._placeType.Entity = value
        End Set
    End Property

#End Region

End Class
