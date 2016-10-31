
Partial Class Elements_ElementHistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            If IsNumeric(Request.QueryString("projectId")) AndAlso IsNumeric(Request.QueryString("elementId")) Then
                ctrlEntityHistory.ProjectId = Request.QueryString("projectId")
                ctrlEntityHistory.ElementId = Request.QueryString("elementId")
                ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
                ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
            End If


        End If

    End Sub

End Class
