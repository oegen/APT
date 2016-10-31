Imports System.Collections.Generic
Imports System.Linq
Imports aptEntities
Imports aptBusinessLogic

Partial Class Reports_Client_Report
    Inherits System.Web.UI.Page

    Private StartTokens As List(Of Token) = WorkflowManager.GetAllStartTokens()
    Private ProjectUserAssociations As List(Of ProjectRoleAssociation)
    Private TotalSpendOnProjects As Integer
    Private TotalTimeOnProjects As Integer

    Private _projectsList As List(Of Project)

    Private Property ProjectsList As List(Of Project)
        Get
            Return _projectsList
        End Get
        Set(value As List(Of Project))
            _projectsList = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            ProjectsList = ProjectManager.GetAllProjects().Where(Function(doc) GetStartTokenByProjectId(doc.ID) IsNot Nothing).ToList
            populateRepeaters()

            ltlTotalProjects.Text = ProjectsList.Count
            ltlTotalSpend.Text = GetTotalSpendForListOfProjects(ProjectsList)
            ltlTotalTime.Text = GetTotalTimeForList(ProjectsList)
        End If

    End Sub

    Protected Sub rptrBusinessArea_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrBusinessArea.ItemDataBound

        RepeaterItemDataBound(AppSettingsGet.BusinessAreaDefinitionID, e)

    End Sub

    Protected Sub rptrBrand_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrBrand.ItemDataBound

        RepeaterItemDataBound(AppSettingsGet.BrandListDefinitionID, e)

    End Sub

    Protected Sub rptrProjectOwner_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrProjectOwner.ItemDataBound

        Dim ltlName As Literal = CType(e.Item.FindControl("ltlName"), Literal)
        Dim ltlNoOfProjects As Literal = CType(e.Item.FindControl("ltlNoOfProjects"), Literal)
        Dim ltlPercentShareProjects As Literal = CType(e.Item.FindControl("ltlPercentShareProjects"), Literal)
        Dim ltlActualTime As Literal = CType(e.Item.FindControl("ltlActualTime"), Literal)
        Dim ltlPercentShareTimeAccuracy As Literal = CType(e.Item.FindControl("ltlPercentShareTimeAccuracy"), Literal)
        Dim ltlSpend As Literal = CType(e.Item.FindControl("ltlSpend"), Literal)
        Dim ltlPercentShareSpend As Literal = CType(e.Item.FindControl("ltlPercentShareSpend"), Literal)

        Dim dataItem As AptUser = CType(e.Item.DataItem, AptUser)
        ltlName.Text = dataItem.FullName

        Dim projectAssociations = ProjectManager.GetProjectsByUserAndRole(dataItem.ID, AppSettingsGet.OwnerRoleID).Select(Function(doc) doc.Project.ID).ToList
        Dim listOfProjects = ProjectsList.Where(Function(doc) projectAssociations.Contains(doc.ID)).ToList

        ltlNoOfProjects.Text = listOfProjects.Count
        ltlPercentShareProjects.Text = Math.Round((listOfProjects.Count / ProjectsList.Count) * 100, 2)

        Dim totalTime = GetTotalTimeForList(listOfProjects)
        ltlActualTime.Text = totalTime
        ltlPercentShareTimeAccuracy.Text = GetShare(totalTime, TotalTimeOnProjects)

        Dim totalSpend = GetTotalSpendForListOfProjects(listOfProjects)
        ltlSpend.Text = totalSpend
        ltlPercentShareSpend.Text = GetShare(totalSpend, TotalSpendOnProjects)
    End Sub
    
    Protected Sub rptrTypeOfWork_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrTypeOfWork.ItemDataBound

        RepeaterItemDataBound(AppSettingsGet.TypeOfWorkDefinitionID, e)

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click

        ProjectsList = ProjectManager.GetAllProjects().Where(Function(doc) GetStartTokenByProjectId(doc.ID) IsNot Nothing).ToList

        Select rlstDateFilter.SelectedValue
            Case "YTD"
                Dim beginningOfCurrentYear As DateTime = Date.Parse("01/01/" + CStr(DateTime.Now.Year))
                ProjectsList = ProjectsList.Where(Function(doc) WorkflowManager.GetStartTokenByProject(doc.ID).EnabledDate > beginningOfCurrentYear).ToList
            Case "MAT"
                Dim dateOneYearAgo As DateTime = DateTime.Now.AddYears(-1)
                ProjectsList = ProjectsList.Where(Function(doc) WorkflowManager.GetStartTokenByProject(doc.ID).EnabledDate > dateOneYearAgo).ToList
            Case "Date Range"
                ProjectsList = ProjectsList.Where(Function(doc) WorkflowManager.GetStartTokenByProject(doc.ID).EnabledDate > dtpBegin.SelectedDate _
                                              And WorkflowManager.GetStartTokenByProject(doc.ID).EnabledDate < dtpEnd.SelectedDate).ToList
        End Select

        Select Case ddlProjectStatus.SelectedValue
            Case "Completed"
                Dim completed As List(Of Project) = New List(Of Project)
                Dim projectId As Integer = 0
                For Each project As Project In ProjectsList
                    projectId = project.ID
                    If WorkflowManager.GetLastTokenByProject(projectId) IsNot Nothing Then
                        completed.Add(project)
                    End If
                Next

                ProjectsList = completed

            Case "Archived"
                Dim archived As List(Of Project) = New List(Of Project)
                Dim projectId As Integer = 0
                For Each project In ProjectsList
                    projectId = project.ID
                    If ProjectManager.GetProject(projectId).Active Then
                        archived.Add(project)
                    End If
                Next

                ProjectsList = archived

            Case "In Progress"
                Dim inProgress As List(Of Project) = New List(Of Project)
                Dim projectId As Integer = 0
                For Each project As Project In ProjectsList
                    projectId = project.ID
                    If (Not ProjectManager.GetProject(projectId).Active) And _
                       (WorkflowManager.GetLastTokenByProject(projectId) Is Nothing) And _
                       (Not ProjectManager.GetProject(projectId).Stopped) Then

                        inProgress.Add(project)

                    End If
                Next

                ProjectsList = inProgress

            Case "Stopped & Cancelled"
                Dim stopped As List(Of Project) = New List(Of Project)
                Dim projectId As Integer = 0
                For Each project As Project In ProjectsList
                    projectId = project.ID
                    If ProjectManager.GetProject(projectId).Stopped Then
                        stopped.Add(project)
                    End If
                Next

                ProjectsList = stopped

        End Select

        populateRepeaters()
        ltlTotalProjects.Text = ProjectsList.Count
        ltlTotalSpend.Text = GetTotalSpendForListOfProjects(ProjectsList)
        ltlTotalTime.Text = GetTotalTimeForList(ProjectsList)

    End Sub

    Private Sub populateRepeaters()
        TotalTimeOnProjects = GetTotalTimeForList(ProjectsList)
        TotalSpendOnProjects = GetTotalSpendForListOfProjects(ProjectsList)

        rptrBusinessArea.DataSource = ListManager.GetListsNodes(AppSettingsGet.BusinessAreaId)
        rptrBrand.DataSource = ListManager.GetListsNodes(AppSettingsGet.BrandListId)
        rptrTypeOfWork.DataSource = ListManager.GetListsNodes(AppSettingsGet.TypeOfWorkListId)
        rptrBusinessArea.DataBind()
        rptrBrand.DataBind()
        rptrTypeOfWork.DataBind()

        rptrProjectOwner.DataSource = ProjectManager.GetAllProjectAndUserAssociations.Select(Function(doc) doc.User).Distinct
        rptrProjectOwner.DataBind()

    End Sub

    Private Function GetStartTokenByProjectId(projectId As Integer) As Token
        Return StartTokens.Where(Function(doc) doc.Project.ID = projectId).FirstOrDefault
    End Function

    Private Sub RepeaterItemDataBound(listId As Integer, e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        Dim ltlName As Literal = CType(e.Item.FindControl("ltlName"), Literal)
        Dim ltlNoOfProjects As Literal = CType(e.Item.FindControl("ltlNoOfProjects"), Literal)
        Dim ltlPercentShareProjects As Literal = CType(e.Item.FindControl("ltlPercentShareProjects"), Literal)
        Dim ltlActualTime As Literal = CType(e.Item.FindControl("ltlActualTime"), Literal)
        Dim ltlPercentShareTimeAccuracy As Literal = CType(e.Item.FindControl("ltlPercentShareTimeAccuracy"), Literal)
        Dim ltlSpend As Literal = CType(e.Item.FindControl("ltlSpend"), Literal)
        Dim ltlPercentShareSpend As Literal = CType(e.Item.FindControl("ltlPercentShareSpend"), Literal)
        Dim dataItem As ListNode = CType(e.Item.DataItem, ListNode)

        ltlName.Text = dataItem.Name

        Dim projectIds = SchemaManager.GetParentIdBySchemaAndValue(listId, dataItem.ID)
        Dim listOfProjects = ProjectsList.Where(Function(doc) projectIds.Contains(doc.ID)).ToList()

        ltlNoOfProjects.Text = listOfProjects.Count
        ltlPercentShareProjects.Text = Math.Round((listOfProjects.Count / ProjectsList.Count) * 100, 2)

        Dim totalTime = GetTotalTimeForList(listOfProjects)
        ltlActualTime.Text = totalTime
        ltlPercentShareTimeAccuracy.Text = GetShare(totalTime, TotalTimeOnProjects)

        Dim totalSpend = GetTotalSpendForListOfProjects(listOfProjects)
        ltlSpend.Text = totalSpend
        ltlPercentShareSpend.Text = GetShare(totalSpend, TotalSpendOnProjects)
    End Sub

    Private Function GetTotalTimeForProject(projectId As Integer) As Integer
        Dim total = 0
        Dim timesheetsForProject As List(Of Timesheet) = TimesheetManager.GetTimeSheetByProjectId(projectId)
        For Each timesheet As Timesheet In timesheetsForProject
            total += (timesheet.HourSpent * 60)
            total += timesheet.MinutesSpent
        Next
        Return Math.Round((total / 60), 0)
    End Function

    Private Function GetTotalTimeForList(projectList As List(Of Project)) As Integer
        Dim total = 0
        For Each project In projectList
            total += GetTotalTimeForProject(project.ID)
        Next

        Return total
    End Function

    Private Function GetSpendForProject(projectId As Integer) As Integer
        Dim timesheets = TimesheetManager.GetTimeSheetByProjectId(projectId)

        Dim totalTimeDesigner = 0
        Dim totalTimeArtWorker = 0

        For Each timesheet As Timesheet In timesheets
            If (UserManager.UserHasGlobalRole(timesheet.User.ID, AppSettingsGet.ArtworkerID)) Then
                totalTimeArtWorker += (timesheet.HourSpent * 60)
                totalTimeArtWorker += (timesheet.MinutesSpent)
            ElseIf (UserManager.UserHasGlobalRole(timesheet.User.ID, AppSettingsGet.DesignerID)) Then
                totalTimeDesigner += (timesheet.HourSpent * 60)
                totalTimeDesigner += (timesheet.MinutesSpent * 60)
            End If
        Next

        Return Math.Round((totalTimeDesigner / 60), 0) * 60 + Math.Round((totalTimeArtWorker / 60), 0) * 40
    End Function

    Private Function GetTotalSpendForListOfProjects(projectList As List(Of Project)) As Integer
        Dim total = 0
        For Each project As Project In projectList
            total += GetSpendForProject(project.ID)
        Next

        Return total
    End Function

    Private Function GetShare(actual As Integer, total As Integer) As Double
        If total = 0 Then
            Return 0
        Else
            Return Math.Round((actual / total) * 100, 2)
        End If
    End Function

End Class
