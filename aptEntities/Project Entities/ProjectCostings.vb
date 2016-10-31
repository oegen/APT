'----------------------------------------------------------------------------------------------
' Filename    : Costings.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        22/08/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_project_costings")>
Public Class ProjectCostings

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="add_studio_costs", UpdateCheck:=UpdateCheck.Never)> _
    Public Property AddStudioCosts As Decimal

    <Column(Name:="final_studio_costs", UpdateCheck:=UpdateCheck.Never)> _
    Public Property FinalStudioCosts As Decimal

    <Column(Name:="estimate_print_costs", UpdateCheck:=UpdateCheck.Never)> _
    Public Property EstimatePrintCosts As Decimal

    <Column(Name:="final_print_costs", UpdateCheck:=UpdateCheck.Never)> _
    Public Property FinalPrintCosts As Decimal

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

#End Region

End Class
