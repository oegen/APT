'----------------------------------------------------------------------------------------------
' Filename    : TabPageWithoutAjax.master.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL/LP     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class MasterPages_TabPageWithoutAjax
    Inherits System.Web.UI.MasterPage

#Region "Properties"

    Public Property SelectedTab As ProjectTabIndex
        Get
            Return ViewState("_selectedTab")
        End Get
        Set(ByVal value As ProjectTabIndex)
            ViewState("_selectedTab") = value
            SetOpenTab()
        End Set
    End Property

    Public Property ProjectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LoadPage()
        End If
    End Sub

#Region "Link Button Clicks"

    Protected Sub lnkDashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDashboard.Click
        Response.Redirect(String.Format("~/Project/Dashboard.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkInfo.Click
        Response.Redirect(String.Format("~/Project/ProjectDetails.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkTasks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTasks.Click
        Response.Redirect(String.Format("~/Project/ProjectTaskList.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkElements_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkElements.Click

        Dim latestElementId As Integer = GetLatestElementIdInProject(ProjectId)

        If latestElementId <> 0 Then
            Response.Redirect(String.Format("~/Elements/ProjectElement.aspx?projectId={0}&elementId={1}", _
                                            ProjectId, latestElementId))
        Else
            Response.Redirect(String.Format("~/Elements/ProjectElement.aspx?projectId={0}", ProjectId))
        End If

    End Sub

    Protected Sub lnkDocuments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDocuments.Click
        Response.Redirect(String.Format("~/Project/ProjectDocuments.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkTags_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTags.Click
        Response.Redirect(String.Format("~/Project/ProjectTags.aspx?projectId={0}", ProjectId))
    End Sub



#End Region

#End Region

#Region "Public Methods"

    Public Sub DisplayConfirmationMessage(ByVal message As String, Optional ByVal messageBoxType As MessageBoxType = MessageBoxType.Success, _
                                          Optional ByVal timeOut As Integer = 2)

        CType(Me.Master, MasterPage).DisplayConfirmationMessage(message, messageBoxType, timeOut)
    End Sub

    Public Sub DisplayContentTitle(ByVal projectId As Integer)
        Dim currentProject As Project = ProjectManager.GetProject(projectId)

        If currentProject IsNot Nothing Then
            Dim latestAudit As Audit = AuditTrailManager.GetLatestAuditByProject(projectId)

            ctrlMessagePanel.ProjectId = projectId

            ctrlContentHeader.Title = currentProject.Name
            ctrlContentHeader.SubTitle = currentProject.ID

            If latestAudit IsNot Nothing Then
                ctrlContentHeader.Description = String.Format("Last update : {0} by {1} on {2}", _
                                                              latestAudit.Section.Name, _
                                                              latestAudit.User.FullName, _
                                                              latestAudit.AuditDate.ToString("dd/MM/yyyy HH:mm"))
            Else
                ctrlContentHeader.Description = "There have been no updates to the project."
            End If
        End If
    End Sub

    Public Sub SetSubNavSelectedItem(ByVal selectedItem As ConstantLibrary.ProjectSubNavItems)
        ctrlSubNavProject.SelectedItem = selectedItem
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetOpenTab()
        Select Case SelectedTab
            Case ProjectTabIndex.DASHBOARD
                liDashboard.Attributes.Add("class", "ui-state-default ui-corner-top ui-tabs-selected ui-state-active")
            Case ProjectTabIndex.INFO
                liInfo.Attributes.Add("class", "ui-state-default ui-corner-top ui-tabs-selected ui-state-active")
            Case ProjectTabIndex.TASK
                liTasks.Attributes.Add("class", "ui-state-default ui-corner-top ui-tabs-selected ui-state-active")
            Case ProjectTabIndex.ELEMENTS
                liElements.Attributes.Add("class", "ui-state-default ui-corner-top ui-tabs-selected ui-state-active")
            Case ProjectTabIndex.DOCUMENTS
                liDocuments.Attributes.Add("class", "ui-state-default ui-corner-top ui-tabs-selected ui-state-active")
            Case ProjectTabIndex.TAGS
                liTags.Attributes.Add("class", "ui-state-default ui-corner-top ui-tabs-selected ui-state-active")
        End Select
    End Sub

    Private Sub LoadPage()

        If Page.IsPostBack = False Then
            Integer.TryParse(Request.QueryString("projectId"), ProjectId)
            PermissionCheck()
            DisplayContentTitle(ProjectId)
            ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
        End If
    End Sub

    Private Sub PermissionCheck()
        ' Removed through client request
        ' liDocuments.Visible = PermissionsManager.CanAccessDocuments(SessionManager.LoggedInUserId, ProjectId)
    End Sub

#End Region

End Class

