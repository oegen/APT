
Partial Class Admin_Working_Exceptions_AdHocExceptions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNumeric(Request.QueryString("adHocId")) Then
            ctrlAdHocException.AdHocExceptionId = Request.QueryString("adHocId")
        End If

    End Sub

    Protected Sub ctrlAdHocException_SaveAdHocSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAdHocException.SaveAdHocSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Ad Hoc Exception has been saved")
    End Sub

End Class
