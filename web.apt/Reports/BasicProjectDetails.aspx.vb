Imports aptEntities
Imports aptBusinessLogic
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq

Partial Class Reports_BasicProjectDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            DownloadReport()
        End If

    End Sub

    Private Sub DownloadReport()

        Dim header As String = DisplayHeader()
        Dim allProjects As List(Of Project) = ProjectManager.GetAllProjects()
        Dim csv As New StringBuilder

        csv.AppendLine(header)

        For Each project In allProjects

            Dim projectCsvLine = New StringBuilder() ' Just reset

            projectCsvLine.Append(ReturnTextWithDelimiter(project.ID))
            projectCsvLine.Append(ReturnTextWithDelimiter(project.Name))
            projectCsvLine.Append(ReturnTextWithDelimiter(project.BudgetCode))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetProjectCreatedDate(project.ID)))
            projectCsvLine.Append(ReturnTextWithDelimiter(project.RequiredDate.ToString("dd/MM/yyyy")))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetProjectOwner(project.ID)))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetBrand(project.ID)))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetBusinessArea(project.ID)))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetProjectCoordinator(project.ID)))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetProjectElements(project.ID).Count))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetProjectForecastTime(project.ID)))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetProjectActualTime(project.ID)))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetTotalCost(project.ID)))
            projectCsvLine.Append(ReturnTextWithDelimiter(GetProjectCompletedDate(project.ID)))
            projectCsvLine.Append(GetTypeOfWork(project.ID))

            csv.AppendLine(projectCsvLine.ToString)

        Next

        Page.Response.Clear()
        Page.Response.ClearHeaders()
        Page.Response.ContentType = "text/csv"
        Page.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename=BasicProjectDetails.csv"))
        Page.Response.Write(csv)
        Page.Response.End()

    End Sub

    Private Function DisplayHeader() As String

        Dim header As New StringBuilder

        header.Append("AIN,")
        header.Append("Project Name,")
        header.Append("Budget Code,")
        header.Append("Date created,")
        header.Append("In-trade date,")
        header.Append("Project Owner,")
        header.Append("Brand,")
        header.Append("Business Area,")
        header.Append("Co-ordinator,")
        header.Append("Number of Elements,")
        header.Append("Forecast Time,")
        header.Append("Actual Time,")
        header.Append("Total Cost,")
        header.Append("Completed Date,")
        header.Append("Type of Work")

        Return header.ToString

    End Function

    Private Function ReturnTextWithDelimiter(ByVal text As String, Optional ByVal delimiter As String = ",") As String

        Return text & ","

    End Function

    Private Function GetProjectOwner(projectId As Integer) As String

        Dim projectOwner As AptUser = ProjectManager.GetProjectOwner(projectId)

        If projectOwner IsNot Nothing Then
            Return projectOwner.FullName
        Else
            Return "N/A"
        End If

    End Function

    Private Function GetBrand(projectId As String) As String

        Try
            Dim brandId As Integer = ProjectManager.GetProjectBrandId(projectId)
            Dim brandNode As ListNode = ListManager.GetListNode(brandId)

            Return brandNode.Name

        Catch ex As Exception
            Return "N/A"
        End Try

    End Function

    Private Function GetBusinessArea(projectId As String) As String

        Try
            Dim businessAreaId As Integer = ProjectManager.GetProjectBusinessArea(projectId)
            Dim businessArea As ListNode = ListManager.GetListNode(businessAreaId)

            Return businessArea.Name

        Catch ex As Exception
            Return "N/A"
        End Try

    End Function

    Private Function GetProjectCoordinator(projectId As Integer) As String

        Dim coordinator As AptUser = ProjectManager.GetProjectCoordinator(projectId)

        If coordinator IsNot Nothing Then
            Return coordinator.FullName
        Else
            Return "N/A"
        End If

    End Function

    Private Function GetProjectCreatedDate(projectId As Integer) As String

        Try
            Dim firstTokenForProject As Token = WorkflowManager.GetStartTokenByProject(projectId)

            If firstTokenForProject.EnabledDate.HasValue Then
                Return firstTokenForProject.EnabledDate.Value.ToString("dd/MM/yyyy")
            Else
                Return "N/A"
            End If

        Catch ex As Exception
            Return "N/A"
        End Try

    End Function

    Private Function GetProjectCompletedDate(projectId As Integer) As String

        Try
            Dim lastTokenForProject As Token = WorkflowManager.GetLastTokenByProject(projectId)

            If lastTokenForProject.EnabledDate.HasValue Then
                Return lastTokenForProject.EnabledDate.Value.ToString("dd/MM/yyyy")
            Else
                Return "N/A"
            End If

        Catch ex As Exception
            Return "N/A"
        End Try

    End Function

    Private Function GetProjectElements(projectId As Integer) As List(Of Element)

        Dim elementsForProject As List(Of Element) = ElementManager.GetAllElementsByProject(projectId)

        Return elementsForProject

    End Function

    Private Function GetProjectForecastTime(projectId As Integer) As String

        Dim reserveTimes As List(Of ReservedTime) = ReserveTimeManager.GetReservedTimeForProject(projectId)
        Dim totalReservedTime As Integer = 0

        For Each reserveTime As ReservedTime In reserveTimes
            totalReservedTime += reserveTime.Duration
        Next

        Return CStr(totalReservedTime) + " hours"

    End Function

    Private Function GetProjectActualTime(projectId As Integer) As String

        Dim timesheetEntries = GetTimesheetEnteriesByProjectID(projectId)

        Dim totalActualTimeHours As Integer = 0
        Dim totalActualTimeMinutes As Integer = 0

        For Each actualTime As Timesheet In timesheetEntries
            totalActualTimeHours += actualTime.HourSpent
            totalActualTimeMinutes += actualTime.MinutesSpent
        Next

        Dim totalHours = totalActualTimeHours + Fix(totalActualTimeMinutes / 60)
        Dim remainingMinutes = totalActualTimeMinutes Mod 60

        Return CStr(totalHours) + " hours " + CStr(remainingMinutes) + " minutes"

    End Function

    Private Function GetTotalCost(projectId As Integer) As Integer

        Dim timesheetEntries = GetTimesheetEnteriesByProjectID(projectId)

        Dim totalDesignerHours = 0
        Dim totalDesignerMinutes = 0

        Dim totalArtWorkerHours = 0
        Dim totalArtWorkerMinutes = 0

        For Each timesheetEntry As Timesheet In timesheetEntries
            If UserManager.UserHasGlobalRole(timesheetEntry.User.ID, AppSettingsGet.DesignerID) Then
                totalDesignerHours += timesheetEntry.HourSpent
                totalDesignerMinutes += timesheetEntry.MinutesSpent

            ElseIf UserManager.UserHasGlobalRole(timesheetEntry.User.ID, AppSettingsGet.ArtworkerID) Then
                totalArtWorkerHours += timesheetEntry.HourSpent
                totalArtWorkerMinutes += timesheetEntry.MinutesSpent

            End If
        Next

        totalDesignerHours += Math.Round(totalDesignerMinutes / 60)
        totalArtWorkerHours += Math.Round(totalArtWorkerMinutes / 60)

        Return (60 * totalDesignerHours) + (40 * totalArtWorkerHours)

    End Function

    Private Function GetTimesheetEnteriesByProjectID(projectId As Integer) As List(Of Timesheet)

        Dim timesheetEntriesFromProjectId As List(Of Timesheet) = TimesheetManager.GetTimeSheetByProjectId(projectId)
        Dim elementsInProject = GetProjectElements(projectId)
        ' For Each element As Element In elementsInProject
        'Dim timesheetEntriesFromElementId = TimesheetManager.GetTimeSheetByElementId(projectId)
        'If timesheetEntriesFromElementId.Count <> 0 Then
        'timesheetEntriesFromProjectId.AddRange(timesheetEntriesFromElementId)
        'End If
        'Next

        Return timesheetEntriesFromProjectId

    End Function

    Private Function GetDateCompleted(projectId As Integer) As String

        Dim lastToken = WorkflowManager.GetLastTokenByProject(projectId)
        If (lastToken Is Nothing) Then
            Return "N/A"
        Else
            Return CStr(lastToken.EnabledDate)
        End If

    End Function

    Private Function GetTypeOfWork(projectId As Integer) As String

        Dim typeOfWorkId = ProjectManager.GetProjectTypeOfWork(projectId)
        Dim typeOfWorkList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.TypeOfWorkListId)

        Return typeOfWorkList.Where(Function(doc) doc.ID = typeOfWorkId).Select(Function(doc) doc.Name).SingleOrDefault()

    End Function

End Class
