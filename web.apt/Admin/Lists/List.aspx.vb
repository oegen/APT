
Partial Class Admin_Lists_List
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("aptListId")) Then
                ctrlAptList.AptListId = Request.QueryString("aptListId")
            End If
        End If

    End Sub

    Protected Sub ctrlAptList_AptListSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlAptList.AptListSaveSuccess
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("List has been saved successfully")
    End Sub

End Class
