'----------------------------------------------------------------------------------------------
' Filename    : PrintableElement.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        10/11/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Elements_PrintableElement
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            isValidQueryString()
            ctrlPrintElement.PrintableElementID = Request.QueryString("elementId")
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub isValidQueryString()

        If IsNumeric(Request.QueryString("elementId")) Then
            If ElementManager.GetElement(Request.QueryString("elementId")) IsNot Nothing Then
                Exit Sub
            End If
        End If

        Response.Redirect("~/default.aspx")

    End Sub

#End Region

End Class
