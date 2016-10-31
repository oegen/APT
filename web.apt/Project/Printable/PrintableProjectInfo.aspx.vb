'----------------------------------------------------------------------------------------------
' Filename    : PrintableProjectInfo.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        01/12/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Project_Printable_PrintableProjectInfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If Page.Request.QueryString("projectId") IsNot Nothing Then
                ctrlPrintableProjectInfo.ProjectId = Request.QueryString("projectId")
            End If
        End If

    End Sub

End Class
