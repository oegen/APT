'----------------------------------------------------------------------------------------------
' Filename    : ProjectTaskList.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------


Partial Class Project_ProjectTaskList
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
        Dim projectId As Integer

        If Integer.TryParse(Request.QueryString("projectId"), projectId) Then
            ctrlTaskListing.ProjectId = projectId

            ' Set master page tab
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SelectedTab = ProjectTabIndex.TASK
            ' set master page project
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SetSubNavSelectedItem(ConstantLibrary.ProjectSubNavItems.EDIT_PROJECT)
        End If
    End Sub
#End Region

End Class
