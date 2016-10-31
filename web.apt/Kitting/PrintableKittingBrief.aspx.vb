'----------------------------------------------------------------------------------------------
' Filename    : PrintableKittingBrief.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        10/11/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic

Partial Class Kitting_PrintableKittingBrief
    Inherits System.Web.UI.Page

#Region "Private Implementation"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            ValidQstringCheck()
            ctrlPrintableKittingBrief.projectId = Request.QueryString("ProjectId")
        End If

    End Sub

    Private Sub ValidQstringCheck()

        If IsNumeric(Request.QueryString("ProjectId")) Then
            If ProjectManager.GetProjectKittingBriefByProject(Request.QueryString("ProjectId")) IsNot Nothing Then
                Exit Sub
            End If
        End If

        Response.Redirect("~/Default.aspx")

    End Sub

#End Region

End Class
