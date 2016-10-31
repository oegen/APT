
Partial Class Admin_Reject_Responses_RejectResponse
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("arcResponseId")) Then
                ctrlArcResponse.ArcResponseId = Request.QueryString("arcResponseId")
            End If
        End If

    End Sub

    Protected Sub ctrlArcResponse_ArcResponseSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlArcResponse.ArcResponseSaveSuccess
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("Reject Response has been saved successfully")
    End Sub

End Class
