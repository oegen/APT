'----------------------------------------------------------------------------------------------
' Filename    : ReserveTimeTask.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports System.Globalization

Partial Class Task_ReserveTimeTask
    Inherits System.Web.UI.Page

#Region "Constants"

    Private Const QUERY_ID As String = "tokenId"

    Public Property TokenId As Integer
        Get
            Return ViewState("_tokenId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_tokenId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            SetupPage()
        End If
    End Sub

    Protected Sub ctrlReserveTime_TaskCompleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlReserveTime.TaskCompleted
        Dim currentToken As Token = WorkflowManager.GetTokenByID(TokenId)

        WorkflowManager.CompleteProcess(TokenId, , SessionManager.LoggedInUserId)

        Response.Redirect(String.Format("~/Project/ProjectTaskList.aspx?projectId={0}", currentToken.Project.ID))
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPage()
        If Integer.TryParse(Request.QueryString(QUERY_ID), TokenId) Then
            Dim currentToken As Token = WorkflowManager.GetTokenByID(TokenId)

            Dim cal As New GregorianCalendar
            Dim weekNumber As Integer = cal.GetWeekOfYear(Date.Now, CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)

            ctrlReserveTime.ProjectId = currentToken.Project.ID
            ctrlReserveTime.WeekNumber = weekNumber
            ctrlReserveTime.YearNumber = Date.Now.Year
            ctrlReserveTime.LoadReserveTime()

            ' Set master page tab
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SelectedTab = ProjectTabIndex.TASK
        Else
            ' TODO : CHANGE TO TASK LISTING OR APPROPRIATE PAGE
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

#End Region

End Class
