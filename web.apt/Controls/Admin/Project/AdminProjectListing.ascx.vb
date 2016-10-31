'----------------------------------------------------------------------------------------------
' Filename    : AdminProjectListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        28/11/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_Project_AdminProjectListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Public Enum FilterMode
        ALL = 0
        ARCHIVED = 1
        STOPPED = 2
    End Enum

#End Region

#Region "Properties"

    Public Event ArchiveProject As EventHandler
    Public Event UnarchiveProject As EventHandler
    Public Event StopProject As EventHandler
    Public Event StartProject As eventhandler

    Public Property CurrentFilterMode As FilterMode
        Get
            Return ViewState("_filterMode")
        End Get
        Set(ByVal value As FilterMode)
            ViewState("_filterMode") = value
            BindProjectList()
        End Set
    End Property

#End Region

#Region "Private Implementation"

    Private Sub BindProjectList()

        Dim bindProjects As List(Of Project) = GetProjects()
        plcEmpty.Visible = False
        plcProjectListing.Visible = False

        If bindProjects.Count > 0 Then
            grdvProjectListing.DataSource = bindProjects
            grdvProjectListing.DataBind()
            plcProjectListing.Visible = True
        Else
            plcEmpty.Visible = True
        End If

    End Sub

    Private Function GetProjects() As List(Of Project)

        Select Case CurrentFilterMode

            Case FilterMode.ALL
                Return ProjectManager.GetAllProjects()

            Case FilterMode.ARCHIVED
                Return ProjectManager.GetArchivedProjects()

            Case FilterMode.STOPPED
                Return ProjectManager.GetStoppedProjects()

        End Select

    End Function

    Protected Sub grdvProjectListing_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvProjectListing.PageIndexChanging
        grdvProjectListing.PageIndex = e.NewPageIndex
        BindProjectList()
    End Sub

#End Region

    Protected Sub grdvProjectListing_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvProjectListing.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim bindProject As Project = e.Row.DataItem
            Dim lnkStartStop As LinkButton = e.Row.FindControl("lnkStartStop")
            Dim lnkArchive As LinkButton = e.Row.FindControl("lnkArchive")
            Dim hypView As HyperLink = e.Row.FindControl("hypView")
            Dim ltlProject As Literal = e.Row.FindControl("ltlProject")

            ltlProject.Text = bindProject.AINName

            If bindProject.Stopped = True Then
                lnkStartStop.Text = "Start"
                lnkArchive.Visible = False
            Else
                lnkStartStop.Text = "Stop"
            End If

            If bindProject.Active = True Then
                lnkArchive.Text = "Archive"
            Else
                lnkArchive.Text = "Unarchive"
            End If

            hypView.NavigateUrl = String.Format("~/Project/ProjectDetails.aspx?projectId={0}", bindProject.ID)

            lnkStartStop.CommandArgument = bindProject.ID
            lnkArchive.CommandArgument = bindProject.ID

        End If

    End Sub

    Protected Sub lnkStartStop_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Dim projectId As Integer = e.CommandArgument

        ProjectManager.StartStopProject(projectId)

        Dim tmpProj As Project = ProjectManager.GetProject(projectId)

        If tmpProj.Stopped = True Then
            RaiseEvent StopProject(Me, New EventArgs)
        Else
            RaiseEvent StartProject(Me, New EventArgs)
        End If

        BindProjectList()

    End Sub

    Protected Sub lnkArchive_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Dim projectId As Integer = e.CommandArgument
        ProjectManager.ArchiveUnarchiveProject(projectId)

        Dim tmpProj As Project = ProjectManager.GetProject(projectId)

        If tmpProj.Active = True Then
            RaiseEvent ArchiveProject(Me, New EventArgs)
        Else
            RaiseEvent UnarchiveProject(Me, New EventArgs)
        End If

        BindProjectList()
    End Sub

End Class
