'----------------------------------------------------------------------------------------------
' Filename    : ElementWorkflowState.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Serializable()>
<Table(Name:="apt_tbl_project_elements_workflow_state")>
Public Class ElementWorkflowState

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Description As String

#End Region

End Class
