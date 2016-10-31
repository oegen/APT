'----------------------------------------------------------------------------------------------
' Filename    : MessagePanel.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Layout_MessagePanel
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
            LoadProject()
        End Set
    End Property

#End Region

#Region "Private Implementation"

    Private Sub LoadProject()

        Dim latestAudit As Audit = AuditTrailManager.GetLatestAuditByProject(ProjectId)

        If latestAudit IsNot Nothing Then
            lblUpdatedSection.Text = latestAudit.Section.Name
            lblAction.Text = latestAudit.ChangeType.Name
            lblUpdatedBy.Text = latestAudit.User.FullName
            lblUpdateDate.Text = latestAudit.AuditDate.ToString("dd/MM/yyyy HH:mm")
            lblDetails.Text = latestAudit.Message
        End If

    End Sub

#End Region

End Class
