'----------------------------------------------------------------------------------------------
' Filename    : AuditTrail.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        09/08/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Project_AuditTrail
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
            LoadAuditTrail()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub gvAuditTrail_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAuditTrail.PageIndexChanging
        gvAuditTrail.PageIndex = e.NewPageIndex
        LoadAuditTrail()
    End Sub

    Protected Sub gvAuditTrail_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAuditTrail.RowDataBound
        If e.Row.DataItem IsNot Nothing Then
            Dim auditItem As Audit = CType(e.Row.DataItem, Audit)
            Dim lblDateTime As Label = CType(e.Row.FindControl("lblDateTime"), Label)
            Dim lblSection As Label = CType(e.Row.FindControl("lblSection"), Label)
            Dim lblChangeType As Label = CType(e.Row.FindControl("lblChangeType"), Label)
            Dim lblMessage As Label = CType(e.Row.FindControl("lblMessage"), Label)
            Dim lblUser As Label = CType(e.Row.FindControl("lblUser"), Label)

            lblDateTime.Text = auditItem.AuditDate.ToString("dd/MM/yyyy HH:mm:ss")
            lblSection.Text = auditItem.Section.Name
            lblChangeType.Text = auditItem.ChangeType.Name
            lblMessage.Text = auditItem.Message
            lblUser.Text = auditItem.User.FullName
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadAuditTrail()
        Dim auditList As List(Of Audit) = AuditTrailManager.GetAllByProject(ProjectId)

        gvAuditTrail.DataSource = auditList
        gvAuditTrail.DataBind()
    End Sub

#End Region

End Class
