Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_ArcReponses_ArcResponseListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"
    Private Const ENABLE_RESPONSE_COMMAND_NAME = "EnableResponse"
    Private Const DISABLE_RESPONSE_COMMAND_NAME = "DisableResponse"
#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindArcResponses()
        End If

    End Sub

    Protected Sub grdvArcResponses_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvArcResponses.PageIndexChanging
        grdvArcResponses.PageIndex = e.NewPageIndex
        BindArcResponses()
    End Sub

    Protected Sub grdvArcResponses_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvArcResponses.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpArcResponse As ArcResponse = CType(e.Row.DataItem, ArcResponse)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim lnkSetActivity As LinkButton = CType(e.Row.FindControl("lnkSetActivity"), LinkButton)

            lnkSetActivity.CommandArgument = tmpArcResponse.ID

            If tmpArcResponse.Active = True Then
                lnkSetActivity.Text = "Delete"
                lnkSetActivity.CommandName = DISABLE_RESPONSE_COMMAND_NAME
            Else
                lnkSetActivity.Text = "Enable"
                lnkSetActivity.CommandName = ENABLE_RESPONSE_COMMAND_NAME
            End If

            hypEdit.NavigateUrl = String.Format("~/Admin/Reject Responses/RejectResponse.aspx?arcResponseId={0}", tmpArcResponse.ID)

        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindArcResponses()

        Dim arcResponseList As List(Of ArcResponse) = WorkflowManager.GetArcResponsesByActive()

        grdvArcResponses.DataSource = arcResponseList
        grdvArcResponses.DataBind()

    End Sub

    Protected Sub lnkSetActivity_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim arcResponseId As Integer = e.CommandArgument

        If e.CommandName = ENABLE_RESPONSE_COMMAND_NAME Then
            WorkflowManager.SetArcResponseActivity(arcResponseId, True)
        Else
            WorkflowManager.SetArcResponseActivity(arcResponseId, False)
        End If

        BindArcResponses()

    End Sub

#End Region



End Class
