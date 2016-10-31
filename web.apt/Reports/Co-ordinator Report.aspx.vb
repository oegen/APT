Imports System.Collections.Generic
Imports System.Linq
Imports aptEntities
Imports aptBusinessLogic

Partial Class Reports_Co_ordinator_Report
    Inherits System.Web.UI.Page

    Private StartTokens As List(Of Token) = WorkflowManager.GetAllStartTokens()

    Private _userData As List(Of ProjectRoleAssociation)

    Private Property UserData As List(Of ProjectRoleAssociation)
        Get
            Return _userData
        End Get
        Set(value As List(Of ProjectRoleAssociation))
            _userData = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            UserData = UserManager.GetUserDataByRole(AppSettingsGet.GraphicsCoordinatorID).Where(Function(doc) GetStartTokenByProjectId(doc.Project.ID) IsNot Nothing).ToList()
            PopulateRepeater()
            ltlTotalProjectsTable1.Text = GetTotalAmountOfProjects()
            ltlTotalProjectsTable2.Text = GetTotalOverrunProjects()

            'For Date Range Filter
            Dim earilestProject As ProjectRoleAssociation = UserData.OrderBy(Function(doc) GetStartTokenByProjectId(doc.Project.ID).EnabledDate).First
            dtpBegin.SelectedDate = GetStartTokenByProjectId(earilestProject.Project.ID).EnabledDate
            dtpEnd.SelectedDate = DateTime.Now.ToShortDateString
        End If

    End Sub

    Protected Sub rptrCoordinatorTable1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrCoordinatorTable1.ItemDataBound

        Dim ltlName As Literal = CType(e.Item.FindControl("ltlName"), Literal)
        Dim ltlNoOfProjects As Literal = CType(e.Item.FindControl("ltlNoOfProjects"), Literal)
        Dim ltlPercentShareProjects As Literal = CType(e.Item.FindControl("ltlPercentShareProjects"), Literal)
        Dim ltlEstimatedTime As Literal = CType(e.Item.FindControl("ltlEstimatedTime"), Literal)
        Dim ltlActualTime As Literal = CType(e.Item.FindControl("ltlActualTime"), Literal)
        Dim ltlAccuracy As Literal = CType(e.Item.FindControl("ltlAccuracy"), Literal)

        Dim coordinator = CType(e.Item.DataItem, AptUser)
        Dim coordinatorProjectIds = UserData.Where(Function(doc) doc.User.ID = coordinator.ID).Select(Function(doc) doc.Project.ID).ToList

        ltlName.Text = coordinator.FullName
        ltlNoOfProjects.Text = coordinatorProjectIds.Count

        Dim totalAmountOfProjects As Integer = GetTotalAmountOfProjects()
        ltlPercentShareProjects.Text = Math.Round((coordinatorProjectIds.Count / totalAmountOfProjects) * 100, 2)

        Dim totalEstimated As Integer = GetTotalEstimatedTime(coordinatorProjectIds)
        Dim totalActual As Integer = GetTotalActualTimes(coordinatorProjectIds)
        ltlEstimatedTime.Text = totalEstimated
        ltlActualTime.Text = totalActual
        ltlAccuracy.Text = GetTimeAccuracy(totalEstimated, totalActual)

    End Sub

    Protected Sub rptrCoordinatorTable2_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrCoordinatorTable2.ItemDataBound

        Dim ltlName As Literal = CType(e.Item.FindControl("ltlName"), Literal)
        Dim ltlNoOfProjects As Literal = CType(e.Item.FindControl("ltlNoOfProjects"), Literal)
        Dim ltlOverrunProjects As Literal = CType(e.Item.FindControl("ltlOverrunProjects"), Literal)
        Dim ltlPercentAccuracy As Literal = CType(e.Item.FindControl("ltlPercentAccuracy"), Literal)
        Dim ltlOverrunTime As Literal = CType(e.Item.FindControl("ltlOverrunTime"), Literal)

        Dim coordinator = CType(e.Item.DataItem, AptUser)
        Dim coordinatorProjectIds = UserData.Where(Function(doc) doc.User.ID = coordinator.ID).Select(Function(doc) doc.Project.ID).ToList

        ltlName.Text = coordinator.FullName
        ltlNoOfProjects.Text = coordinatorProjectIds.Count
        Dim overrunProjects = GetOverrunProjects(coordinatorProjectIds)
        ltlOverrunProjects.Text = overrunProjects.Count
        ltlPercentAccuracy.Text = GetOverrunAccuracy(overrunProjects.Count, coordinatorProjectIds.Count)
        ltlOverrunTime.Text = GetTotalOverrunTime(coordinatorProjectIds)

    End Sub

    Private Sub PopulateRepeater()

        Dim distinctUsers As List(Of AptUser) = UserData.Select(Function(e) e.User).Distinct().ToList
        rptrCoordinatorTable1.DataSource = distinctUsers
        rptrCoordinatorTable2.DataSource = distinctUsers
        rptrCoordinatorTable1.DataBind()
        rptrCoordinatorTable2.DataBind()

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click

        UserData = UserManager.GetUserDataByRole(AppSettingsGet.GraphicsCoordinatorID).Where(Function(doc) GetStartTokenByProjectId(doc.Project.ID) IsNot Nothing).ToList()

        Select Case rlstDateFilter.SelectedValue
            Case "YTD"
                Dim beginningOfCurrentYear As DateTime = Date.Parse("01/01/" + CStr(DateTime.Now.Year))
                UserData = UserData.Where(Function(doc) WorkflowManager.GetStartTokenByProject(doc.Project.ID).EnabledDate > beginningOfCurrentYear).ToList
            Case "MAT"
                Dim dateOneYearAgo As DateTime = DateTime.Now.AddYears(-1)
                UserData = UserData.Where(Function(doc) WorkflowManager.GetStartTokenByProject(doc.Project.ID).EnabledDate > dateOneYearAgo).ToList
            Case "Date Range"
                UserData = UserData.Where(Function(doc) WorkflowManager.GetStartTokenByProject(doc.Project.ID).EnabledDate > dtpBegin.SelectedDate _
                                              And WorkflowManager.GetStartTokenByProject(doc.Project.ID).EnabledDate < dtpEnd.SelectedDate).ToList

        End Select

        Select Case ddlProjectStatus.SelectedValue
            Case "Completed"
                Dim completed As List(Of ProjectRoleAssociation) = New List(Of ProjectRoleAssociation)
                Dim projectId As Integer = 0
                For Each uData As ProjectRoleAssociation In UserData
                    projectId = uData.Project.ID
                    If WorkflowManager.GetLastTokenByProject(projectId) IsNot Nothing Then
                        completed.Add(uData)
                    End If
                Next

                UserData = completed

            Case "Archived"
                Dim archived As List(Of ProjectRoleAssociation) = New List(Of ProjectRoleAssociation)
                Dim projectId As Integer = 0
                For Each uData In UserData
                    projectId = uData.Project.ID
                    If ProjectManager.GetProject(projectId).Active Then
                        archived.Add(uData)
                    End If
                Next

                UserData = archived

            Case "In Progress"
                Dim inProgress As List(Of ProjectRoleAssociation) = New List(Of ProjectRoleAssociation)
                Dim projectId As Integer = 0
                For Each uData As ProjectRoleAssociation In UserData
                    projectId = uData.Project.ID
                    If (Not ProjectManager.GetProject(projectId).Active) And _
                       (WorkflowManager.GetLastTokenByProject(projectId) Is Nothing) And _
                       (Not ProjectManager.GetProject(projectId).Stopped) Then

                        inProgress.Add(uData)

                    End If
                Next

                UserData = inProgress

            Case "Stopped & Cancelled"
                Dim stopped As List(Of ProjectRoleAssociation) = New List(Of ProjectRoleAssociation)
                Dim projectId As Integer = 0
                For Each uData As ProjectRoleAssociation In UserData
                    projectId = uData.Project.ID
                    If ProjectManager.GetProject(projectId).Stopped Then
                        stopped.Add(uData)
                    End If
                Next

                UserData = stopped

        End Select

        PopulateRepeater()
        ltlTotalProjectsTable1.Text = GetTotalAmountOfProjects()
        ltlTotalProjectsTable2.Text = GetTotalOverrunProjects()

    End Sub

    Private Function GetStartTokenByProjectId(projectId As Integer) As Token
        Return StartTokens.Where(Function(doc) doc.Project.ID = projectId).FirstOrDefault
    End Function

    Private Function GetTotalAmountOfProjects() As Integer
        Return UserData.Select(Function(e) e.Project).Distinct().Count
    End Function

    Private Function GetEstimatedTimeForProject(projectId As Integer) As Integer
        Dim total = 0
        Dim estimatedTimes As List(Of ReservedTime) = ReserveTimeManager.GetReservedTimeForProject(projectId)
        For Each estimatedTime As ReservedTime In estimatedTimes
            total += estimatedTime.Duration
        Next
        Return total
    End Function

    Private Function GetTotalEstimatedTime(projectIdList As List(Of Integer)) As Integer
        Dim total = 0
        For Each projectId As Integer In projectIdList
            total += GetEstimatedTimeForProject(projectId)
        Next

        Return total
    End Function

    Private Function GetActualTimeForProject(projectId As Integer) As Integer
        Dim total = 0
        Dim actualTimes As List(Of Timesheet) = TimesheetManager.GetTimeSheetByProjectId(projectId)
        For Each actualTime As Timesheet In actualTimes
            total += (actualTime.HourSpent * 60)
            total += (actualTime.MinutesSpent)
        Next
        Return Math.Round((total / 60), 0)
    End Function

    Private Function GetTotalActualTimes(projectIdList As List(Of Integer)) As Integer
        Dim total = 0
        For Each projectId As Integer In projectIdList
            total += GetActualTimeForProject(projectId)
        Next

        Return total
    End Function

    Private Function GetOverrunProjects(projectIdList As List(Of Integer)) As List(Of Project)
        Dim overrunProjects As List(Of Project) = New List(Of Project)
        For Each projectId As Integer In projectIdList
            Dim estimatedTime = GetEstimatedTimeForProject(projectId)
            Dim actualTime = GetActualTimeForProject(projectId)
            If actualTime > estimatedTime Then
                overrunProjects.Add(ProjectManager.GetProject(projectId))
            End If
        Next
        Return overrunProjects
    End Function

    Private Function GetTotalOverrunProjects() As Integer
        Return GetOverrunProjects(UserData.Select(Function(doc) doc.Project.ID).ToList).Count
    End Function

    Private Function GetTimeAccuracy(estimatedTime As Integer, actualTime As Integer) As Double
        If actualTime = 0 Then
            Dim answer As Integer = If(estimatedTime = 0, 0, 1)
            Return answer
        Else
            Return Math.Round((CType(estimatedTime, Double) / actualTime) - 1, 2)
        End If
    End Function

    Private Function GetOverrunAccuracy(overrunProjects As Integer, overallProjects As Integer) As Double
        Dim onTimeProjects = overallProjects - overrunProjects
        Dim answer = Math.Round((onTimeProjects / overallProjects * 100), 2)
        Return answer
    End Function

    Private Function GetTotalOverrunTime(projectIdList As List(Of Integer)) As Integer
        Dim total = 0
        For Each projectId As Integer In projectIdList
            Dim estimated = GetEstimatedTimeForProject(projectId)
            Dim actual = GetActualTimeForProject(projectId)
            If (estimated < actual) Then
                total += (actual - estimated)
            End If
        Next
        Return total
    End Function

End Class
