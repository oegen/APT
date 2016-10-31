Imports System.Collections.Generic
Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities

Partial Class Controls_Admin_WorkingException_AdHocListing
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event AdhocExceptionRemoved As EventHandler

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindAdHocExceptions()
        End If

    End Sub

    Protected Sub grdvWorkingWeekException_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvWorkingWeekException.PageIndexChanging

        grdvWorkingWeekException.PageIndex = e.NewPageIndex
        BindAdHocExceptions()

    End Sub

    Protected Sub grdvWorkingWeekException_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvWorkingWeekException.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpAdhoc As Adhoc = CType(e.Row.DataItem, Adhoc)
            Dim tmpLogin As AptLogin = UserManager.GetLoginForUser(tmpAdhoc.User.ID)
            Dim lblUsername As Label = CType(e.Row.FindControl("lblUsername"), Label)
            Dim lblStartDate As Label = CType(e.Row.FindControl("lblStartDate"), Label)
            Dim lblEndDate As Label = CType(e.Row.FindControl("lblEndDate"), Label)
            Dim lblHours As Label = CType(e.Row.FindControl("lblHours"), Label)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)

            lblUsername.Text = tmpLogin.Username
            lblStartDate.Text = tmpAdhoc.StartDate
            lblEndDate.Text = tmpAdhoc.EndDate

            If tmpAdhoc.StartDate <> tmpAdhoc.EndDate Then
                lblHours.Text = "N/A"
            Else
                lblHours.Text = tmpAdhoc.Hours
            End If

            hypEdit.NavigateUrl = String.Format("~/Admin/Working Exceptions/AdHocExceptions.aspx?adhocId={0}", tmpAdhoc.ID)
            lnkDelete.CommandArgument = tmpAdhoc.ID
            modUtilities.AddConfirmBoxToLinkButton(lnkDelete, "Are you sure you want to remove this Ad Hoc Exception")

        End If

    End Sub



#End Region

#Region "Private Implementation"

    Private Sub BindAdHocExceptions()

        plcEmpty.Visible = True
        plcWorkingWeekException.Visible = True

        Dim adHocExceptions As List(Of Adhoc) = AdhocExceptionManager.GetFutureAndPresentAdHocExceptions()

        If adHocExceptions.Count > 0 Then
            grdvWorkingWeekException.DataSource = adHocExceptions
            grdvWorkingWeekException.DataBind()
            plcEmpty.Visible = False
        Else
            plcWorkingWeekException.Visible = False
        End If

    End Sub

    Protected Sub lnkSetActivity_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim adhocId As Integer = e.CommandArgument
        AdhocExceptionManager.RemoveAdHocException(adhocId)
        RaiseEvent AdhocExceptionRemoved(Me, New EventArgs)
        BindAdHocExceptions()

    End Sub

#End Region

End Class
