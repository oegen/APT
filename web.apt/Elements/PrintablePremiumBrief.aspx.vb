'----------------------------------------------------------------------------------------------
' Filename    : PrintablePremiumBrief.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        10/11/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic 

Partial Class Elements_PrintablePremiumBrief
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            ValidQstringCheck()
            ctrlPrintablePremiumBrief.ProjectId = Request.QueryString("projectId")
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub ValidQstringCheck()

        If IsNumeric(Request.QueryString("projectId")) Then
            ' Just need to check that it's there, other checks are made in the masterpage
            Exit Sub
        End If

        Response.Redirect("~/Default.aspx")
    End Sub

#End Region

End Class
