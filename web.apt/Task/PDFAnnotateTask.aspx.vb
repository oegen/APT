'----------------------------------------------------------------------------------------------
' Filename    : PDFAnnotateTask.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Task_PDFAnnotateTask
    Inherits System.Web.UI.Page

#Region "Constants"

    Private Const QUERY_ID As String = "tokenId"

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            SetupPage()
        End If
    End Sub

    Protected Sub ctrlAnnotateTask_TaskComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAnnotateTask.TaskComplete
        Dim currentToken As Token = WorkflowManager.GetTokenByID(ctrlAnnotateTask.TokenId)

        Response.Redirect(String.Format("~/Project/ProjectTaskList.aspx?projectId={0}", currentToken.Project.ID))
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPage()
        Dim tokenId As Integer

        If Integer.TryParse(Request.QueryString(QUERY_ID), tokenId) Then
            ctrlAnnotateTask.TokenId = tokenId

            ' Set master page tab
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SelectedTab = ProjectTabIndex.TASK
        Else
            ' TODO : CHANGE TO TASK LISTING OR APPROPRIATE PAGE
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

#End Region

End Class
