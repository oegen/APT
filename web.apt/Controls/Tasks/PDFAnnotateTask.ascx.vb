'----------------------------------------------------------------------------------------------
' Filename    : PDFAnnotateTask.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Tasks_PDFAnnotateTask
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

    Protected Sub lnkUploadDocument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUploadDocument.Click
        HideAll()

        ctrlDocumentUpload.Visible = True
        lblNotification.Visible = False
    End Sub

    Protected Sub lnkViewDocuments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewDocuments.Click
        HideAll()

        ctrlDocumentListing.Visible = True
        lblNotification.Visible = False
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

    Protected Sub ctrlDocumentUpload_UploadSuccess(ByVal sender As Object, ByVal e As CommandEventArgs) Handles ctrlDocumentUpload.UploadSuccess
        Dim documentId As Integer

        lblNotification.Text = "Document was successfully uploaded."
        lblNotification.Visible = True
        ctrlDocumentUpload.Visible = False

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

        ctrlDocumentListing.ProjectId = currentToken.Project.ID
        ctrlDocumentUpload.ProjectId = currentToken.Project.ID

        lblTaskName.Text = associatedTask.Name
        lblTaskDesc.Text = associatedTask.Description
    End Sub

    Private Sub HideAll()
        ctrlDocumentUpload.Visible = False
        ctrlDocumentListing.Visible = False
    End Sub

#End Region

End Class
