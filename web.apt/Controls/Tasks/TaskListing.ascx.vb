'----------------------------------------------------------------------------------------------
' Filename    : TaskListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports GenericUtilities
Imports System.Collections.Generic

Partial Class Controls_Generic_TaskListing
    Inherits System.Web.UI.UserControl

#Region "Constants"

    Private Const COMPLETE_TASK_COMMAND_NAME As String = "completeTask_click"

#End Region

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
            SetupPage()
        End Set
    End Property

    Public Property OnEmptyTaskListParentVisibleAlteration As Boolean
        Get
            Return ViewState("_OnEmptyTaskListParentVisibleAlteration")
        End Get
        Set(ByVal value As Boolean)
            ViewState("_OnEmptyTaskListParentVisibleAlteration") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub rptrTasks_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrTasks.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim singleToken As Token = CType(e.Item.DataItem, Token)
            Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
            Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)
            Dim lblElement As Label = CType(e.Item.FindControl("lblElement"), Label)
            Dim pnlElement As Panel = CType(e.Item.FindControl("pnlElement"), Panel)
            Dim lblOwner As Label = CType(e.Item.FindControl("lblOwner"), Label)
            Dim lnkCompleteTask As LinkButton = CType(e.Item.FindControl("lnkCompleteTask"), LinkButton)
            Dim singleTransition As Transition = WorkflowManager.GetTransitionByToken(singleToken)
            Dim singleTask As Task = singleTransition.Task

            lblName.Text = singleTask.Name
            lblDescription.Text = singleTask.Description
            lblOwner.Text = ProjectManager.GetProjectOwner(singleToken.Project.ID).FullName
            lnkCompleteTask.CommandName = COMPLETE_TASK_COMMAND_NAME
            lnkCompleteTask.CommandArgument = singleToken.ID

            ' If the token refers to an element token then display the element name
            If singleToken.ContextEntity.ID = AppSettingsGet.EntityElementId Then
                Dim elementAssociated As Element = ElementManager.GetElement(singleToken.ContextParentID)

                If elementAssociated IsNot Nothing Then
                    pnlElement.Visible = True
                    'lblElement.Text = elementAssociated.Name
                    lblElement.Text = elementAssociated.DisplayString
                Else
                    pnlElement.Visible = False
                End If
            Else
                pnlElement.Visible = False
            End If
        End If
    End Sub

    Protected Sub rptrTasks_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptrTasks.ItemCommand
        If COMPLETE_TASK_COMMAND_NAME = e.CommandName Then
            Dim selectedToken As Token = WorkflowManager.GetTokenByID(e.CommandArgument)
            Dim selectedTask As Task = WorkflowManager.GetTransitionByToken(selectedToken).Task
            Dim taskLibraryResponse As TaskLibraryItem = TaskLibraryManager.GetByTask(selectedTask.ID)

            ' if there isn't a task library item available, then play it safe and show the complete task form
            If taskLibraryResponse IsNot Nothing Then
                Response.Redirect(String.Format(taskLibraryResponse.AssociatedPage, e.CommandArgument, ProjectId))
            Else
                Response.Redirect(String.Format("~/Task/CompleteTask.aspx?tokenId={0}&projectId={1}", e.CommandArgument, ProjectId))
            End If
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Public Sub SetupPage()
        Dim tokenList As List(Of Token) = WorkflowManager.GetTokensByUserAndStatus(SessionManager.LoggedInUserId, AppSettingsGet.TokenStatusFree, ProjectId)

        If tokenList.Count = 0 Then
            lblNoResults.Visible = True
            rptrTasks.Visible = False

            If OnEmptyTaskListParentVisibleAlteration Then
                Me.Parent.Visible = False
            End If

        Else
            lblNoResults.Visible = False
            rptrTasks.Visible = True
        End If

        rptrTasks.DataSource = tokenList
        rptrTasks.DataBind()
    End Sub

#End Region

End Class
