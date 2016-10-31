Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Layout_ProjectHeader
    Inherits System.Web.UI.UserControl

    Public WriteOnly Property ProjectId As Integer
        Set(ByVal value As Integer)
            DisplayContentTitle(value)
        End Set
    End Property

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

End Class
