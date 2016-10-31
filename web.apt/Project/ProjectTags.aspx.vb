'----------------------------------------------------------------------------------------------
' Filename    : ProjectTags.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------



Partial Class Project_ProjectTags
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LoadPage()
        End If
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadPage()
        ' Set master page tab
        CType(Me.Master, MasterPages_TabPageWithoutAjax).SelectedTab = ProjectTabIndex.TAGS
        CType(Me.Master, MasterPages_TabPageWithoutAjax).SetSubNavSelectedItem(ConstantLibrary.ProjectSubNavItems.EDIT_PROJECT)

        Dim projectId As Integer

        If Integer.TryParse(Request.QueryString("projectId"), projectId) Then
            ctrlProjectTags.ProjectId = projectId
        End If
    End Sub

#End Region

End Class
