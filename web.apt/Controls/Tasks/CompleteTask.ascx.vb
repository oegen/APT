'----------------------------------------------------------------------------------------------
' Filename    : CompleteTask.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports GenericUtilities

Partial Class Controls_Tasks_CompleteTask
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

        lblTaskName.Text = associatedTask.Name
        lblDescription.Text = associatedTask.Description
        ctrlUploadDocument.ProjectId = currentToken.Project.ID
    End Sub

#End Region
    
End Class
