'----------------------------------------------------------------------------------------------
' Filename    : ProjectHistory.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic

Partial Class Controls_Generic_History
    Inherits System.Web.UI.UserControl

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

    Public Property Title As String
        Get
            Return headerTitle.InnerText
        End Get
        Set(ByVal value As String)
            headerTitle.InnerText = value
        End Set
    End Property
#End Region

#Region "Events"

    Protected Sub gvHistory_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvHistory.PageIndexChanging
        gvHistory.PageIndex = e.NewPageIndex
        SetupPage()
    End Sub

    Protected Sub gvHistory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHistory.RowDataBound
        If e.Row.DataItem IsNot Nothing Then
            Dim currentToken As Token = CType(e.Row.DataItem, Token)
            Dim transitionAssociated As Transition = WorkflowManager.GetTransitionByToken(currentToken)
            Dim taskAssociated As Task = transitionAssociated.Task

            Dim lblDate As Label = CType(e.Row.FindControl("lblDate"), Label)
            Dim lblAction As Label = CType(e.Row.FindControl("lblAction"), Label)
            Dim lblPerformedBy As Label = CType(e.Row.FindControl("lblPerformedBy"), Label)
            Dim lblComment As Label = CType(e.Row.FindControl("lblComment"), Label)
            Dim lblRejectResponse As Label = CType(e.Row.FindControl("lblRejectResponse"), Label)
            Dim lblDocument As Label = CType(e.Row.FindControl("lblDocument"), Label)
            Dim consumedDate As DateTime

            If DateTime.TryParse(currentToken.ConsumedDate, consumedDate) Then
                lblDate.Text = consumedDate.ToString("dd/MM/yyyy hh:mm:ss")
            End If

            lblAction.Text = taskAssociated.Description
            lblPerformedBy.Text = "TODO :" ' currentToken.user
            lblComment.Text = "TODO :" ' currentToken.Detail
            ' TODO lblRejectResponse, lblDocument

        End If
    End Sub



#End Region

#Region "Private Implementation"
    Private Sub SetupPage()
        Dim tokenList As List(Of Token) = HistoryGenerator.GetTokenHistory(ProjectId)

        gvHistory.DataSource = tokenList
        gvHistory.DataBind()
    End Sub
#End Region
    
End Class
