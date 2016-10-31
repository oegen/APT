Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Controls_Admin_WorkingException_WorkingWeekExceptionListing
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event WorkingWeekExceptionRemoved As EventHandler

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindWorkWeekExceptions()
        End If

    End Sub

    Protected Sub grdvWorkingWeekException_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvWorkingWeekException.PageIndexChanging

        grdvWorkingWeekException.PageIndex = e.NewPageIndex
        BindWorkWeekExceptions()

    End Sub

    Protected Sub grdvWorkingWeekException_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvWorkingWeekException.RowDataBound

        If e.Row.DataItem IsNot Nothing Then
            Dim tmpWorkWeekEx As WorkingWeekException = CType(e.Row.DataItem, WorkingWeekException)
            Dim userLogin As AptLogin = UserManager.GetLoginForUser(tmpWorkWeekEx.User.ID)
            Dim lblUsername As Label = CType(e.Row.FindControl("lblUsername"), Label)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)

            lblUsername.Text = userLogin.Username
            hypEdit.NavigateUrl = String.Format("~/Admin/Working Exceptions/WorkingWeekException.aspx?workingWeekExId={0}", tmpWorkWeekEx.ID)
            lnkDelete.CommandArgument = tmpWorkWeekEx.ID
            modUtilities.AddConfirmBoxToLinkButton(lnkDelete, "Are you sure you want to remove this working week exception?")
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindWorkWeekExceptions()

        Dim allWorkingWeekExceptions As List(Of WorkingWeekException) = WorkingWeekManager.GetAllActiveWorkingWeekExceptions()

        grdvWorkingWeekException.DataSource = allWorkingWeekExceptions
        grdvWorkingWeekException.DataBind()

    End Sub

    Protected Sub lnkSetActivity_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim workingWeekExId As Integer = e.CommandArgument
        WorkingWeekManager.RemoveWorkingWeekException(workingWeekExId)
        RaiseEvent WorkingWeekExceptionRemoved(Me, New EventArgs)
        BindWorkWeekExceptions()

    End Sub

#End Region

End Class
