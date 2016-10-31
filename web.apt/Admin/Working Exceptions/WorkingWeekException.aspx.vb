
Partial Class Admin_Working_Week_Exceptions_WorkingWeekException
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNumeric(Request.QueryString("workingWeekExId")) Then
            ctrlWorkingWeekException.WorkingWeekExceptionId = Request.QueryString("workingWeekExId")
        End If

    End Sub

    Protected Sub ctrlWorkingWeekException_SaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlWorkingWeekException.SaveSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Working Week Exception has been saved")
    End Sub

End Class
