Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_WorkingException_BlockMeetingExceptionListing
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event WorkingExceptionRemoved As eventhandler

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindBlockMeetingExceptions()
        End If

    End Sub

    Protected Sub grdvBlockMeetingException_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvBlockMeetingException.PageIndexChanging

        grdvBlockMeetingException.PageIndex = e.NewPageIndex
        BindBlockMeetingExceptions()

    End Sub

    Protected Sub grdvBlockMeetingException_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBlockMeetingException.RowDataBound

        If e.Row.DataItem IsNot Nothing Then
            Dim tmpBlockMeetingException As BlockMeetingException = CType(e.Row.DataItem, BlockMeetingException)
            Dim lblStartDate As Label = CType(e.Row.FindControl("lblStartDate"), Label)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)

            hypEdit.NavigateUrl = String.Format("~/Admin/Working Exceptions/BlockMeetingException.aspx?blockMeetingExId={0}", tmpBlockMeetingException.ID)
            lblStartDate.Text = tmpBlockMeetingException.StartDate
            lnkDelete.CommandArgument = tmpBlockMeetingException.ID
        End If

    End Sub


    Protected Sub lnkSetActivity_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim workingWeekExId As Integer = e.CommandArgument
        BlockMeetingExceptionManager.RemoveBlockMeetingException(workingWeekExId)
        RaiseEvent WorkingExceptionRemoved(Me, New EventArgs)
        BindBlockMeetingExceptions()
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindBlockMeetingExceptions()

        plcBlockMeetingException.Visible = True
        plcEmpty.Visible = True

        Dim blockMeetingExceptions As List(Of BlockMeetingException) = BlockMeetingExceptionManager.GetPresentAndFutureExceptions()

        If blockMeetingExceptions.Count > 0 Then
            grdvBlockMeetingException.DataSource = blockMeetingExceptions
            grdvBlockMeetingException.DataBind()
            plcEmpty.Visible = False
        Else
            plcBlockMeetingException.Visible = False
        End If
    End Sub

#End Region

End Class
