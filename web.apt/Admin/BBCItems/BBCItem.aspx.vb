'----------------------------------------------------------------------------------------------
' Filename    : BBCItem.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Partial Class Admin_BBCItems_BBCItem
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("BBCItemId")) Then
                ctrlBBCItem.BBCItemId = Request.QueryString("BBCItemId")
            End If
        End If

    End Sub

    Protected Sub ctrlBBCItem_BBCItemSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlBBCItem.BBCItemSaveSuccess
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("BBC Item has been saved successfully")
    End Sub

End Class
