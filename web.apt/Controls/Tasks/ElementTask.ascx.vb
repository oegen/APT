'----------------------------------------------------------------------------------------------
' Filename    : ElementTask.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Tasks_ElementTask
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property TokenId As Integer
        Get
            Return ViewState("_tokenId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_tokenId") = value
            SetupPage()
        End Set
    End Property

    Public Event TaskComplete As EventHandler

#End Region

#Region "Events"

    Protected Sub lnkElement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkElement.Click
        Dim currentToken As Token = WorkflowManager.GetTokenByID(TokenId)

        Response.Redirect(String.Format("~/Project/ProjectElement.aspx?projectId={0}", currentToken.Project.ID))
    End Sub

    Protected Sub lnkProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkProject.Click
        Dim currentToken As Token = WorkflowManager.GetTokenByID(TokenId)

        Response.Redirect(String.Format("~/Project/ProjectDetails.aspx?projectId={0}", currentToken.Project.ID))
    End Sub

    Protected Sub lnkTimesheet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTimesheet.Click
        Dim currentToken As Token = WorkflowManager.GetTokenByID(TokenId)

        Response.Redirect(String.Format("~/Timesheet/TimesheetListing.aspx?projectId={0}", currentToken.Project.ID))
    End Sub

    Protected Sub lnkComplete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkComplete.Click
        Dim errorMessage As String = ""

        If TaskLibraryManager.CanTaskBeCompleted(TokenId, errorMessage) Then
            WorkflowManager.CompleteProcess(TokenId, , SessionManager.LoggedInUserId, txtComment.Text)

            RaiseEvent TaskComplete(Me, New EventArgs())
        Else
            phError.Visible = True
            lblErrorMessage.Text = errorMessage
        End If
    End Sub

    Protected Sub lnkDocumentUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDocumentUpload.Click
        ctrlUploadDocument.Visible = True
        lblUploadSuccess.Visible = False
    End Sub

    Protected Sub ctrlUploadDocument_UploadSuccess(ByVal sender As Object, ByVal e As CommandEventArgs) Handles ctrlUploadDocument.UploadSuccess
        Dim documentId As Integer

        ctrlUploadDocument.Visible = False
        lblUploadSuccess.Visible = True

        ' Associate the document with the token that is to be completed
        If Integer.TryParse(e.CommandArgument, documentId) Then
            WorkflowManager.AssociatedDocumentWithToken(documentId, TokenId)
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPage()
        Dim currentToken As Token = WorkflowManager.GetTokenByID(TokenId)
        Dim associatedTransition As Transition = WorkflowManager.GetTransitionByToken(currentToken)
        Dim associatedTask As Task = associatedTransition.Task
        Dim currentElement As Element = ElementManager.GetElement(currentToken.ContextParentID)

        lblTaskName.Text = associatedTask.Name
        lblTaskDescription.Text = associatedTask.Description
        lblElementName.Text = currentElement.DisplayString
        lblElementDescription.Text = currentElement.Description
        ctrlUploadDocument.ProjectId = currentToken.Project.ID
    End Sub

#End Region
       
End Class
