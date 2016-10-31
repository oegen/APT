Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If SessionManager.LoggedInUserId <> 0 Then
            Response.Redirect("~/Project/ProjectListing.aspx")
        Else
            Response.Redirect("~/login.aspx")
        End If

    End Sub

End Class
