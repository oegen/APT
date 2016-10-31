
Partial Class Admin_Element_Manager_SubclassTypeListing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("aptTypeId")) Then
                ctrlSubclassTypeListing.AptTypeId = Request.QueryString("aptTypeId")
                hypAddSubclassType.NavigateUrl = _
                    String.Format("~/Admin/Element Manager/SubclassType.aspx?aptTypeId={0}", Request.QueryString("aptTypeId"))
            End If
        End If

    End Sub

End Class
