'----------------------------------------------------------------------------------------------
' Filename    : ApprovalTask.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Tasks_ApprovalTask
    Inherits System.Web.UI.UserControl

#Region "Constants"

    Private Const ACCEPT_PRE_CONDITION As String = "a"
    Private Const REJECT_PRE_CONDITION As String = "r"

#End Region

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

    Public Event TaskApproved As EventHandler
    Public Event TaskRejected As EventHandler
#End Region

#Region "Events"

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAccept.Click
        Dim errorMessage As String = ""

        If TaskLibraryManager.CanTaskBeCompleted(TokenId, errorMessage) Then
            CompleteFunction(ACCEPT_PRE_CONDITION)

            RaiseEvent TaskApproved(Me, New EventArgs())
        Else
            phError.Visible = True
            lblErrorMessage.Text = errorMessage
        End If
       
    End Sub

    Protected Sub Reject_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CompleteFunction(REJECT_PRE_CONDITION)

        RaiseEvent TaskRejected(Me, New EventArgs())
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPage()
        Dim currentToken As Token = WorkflowManager.GetTokenByID(TokenId)
        Dim associatedTransition As Transition = WorkflowManager.GetTransitionByToken(currentToken)
        Dim associatedTask As Task = associatedTransition.Task

        ltlTaskName.Text = associatedTask.Name
        ltlTaskDescription.Text = associatedTask.Description

        SetupElement()

        ' We only want to show this button if we're at the designer step
        If currentToken.Place.ID = AppSettingsGet.StudioQAPlaceID Then
            lnkApproveWithComment.Visible = True
        End If

    End Sub

    Private Sub SetupElement()
        Dim currentToken As Token = WorkflowManager.GetTokenByID(TokenId)

        If currentToken.ContextEntity.ID = AppSettingsGet.EntityElementId Then
            Dim element As Element = ElementManager.GetElement(currentToken.ContextParentID)

            pnlElementTask.Visible = True
            lblElementName.Text = element.DisplayString
            lblElementDescription.Text = element.Description
        End If
    End Sub

    Private Sub CompleteFunction(ByVal preCondition As String)
        WorkflowManager.CompleteProcess(TokenId, preCondition, SessionManager.LoggedInUserId, txtComment.Text)
    End Sub

#End Region

End Class
