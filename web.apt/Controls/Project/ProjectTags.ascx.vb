'----------------------------------------------------------------------------------------------
' Filename    : ProjectTags.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Generic_ProjectTags
    Inherits System.Web.UI.UserControl

#Region "Constants"
    Private Const CHECK_BOX_COMMAND_NAME As String = "checkbox_click"
    Private Const ID_ATTRIBUTE As String = "tagId"
#End Region

#Region "Properties"
    Public Property ProjectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
            SetupPage()
        End Set
    End Property

    Public Property Title As String
        Get
            Return ctrlContentTitle.Title
        End Get
        Set(ByVal value As String)
            ctrlContentTitle.Title = value
        End Set
    End Property
#End Region

#Region "Events"
    Protected Sub rptrProjectTags_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrProjectTags.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim currentTag As Tag = CType(e.Item.DataItem, Tag)
            Dim cbTag As CheckBox = CType(e.Item.FindControl("cbTag"), CheckBox)

            cbTag.Checked = TagManager.ProjectHasTag(currentTag.ID, ProjectId)
            cbTag.Text = currentTag.Name
            cbTag.Attributes.Add(ID_ATTRIBUTE, currentTag.ID)
        End If
    End Sub

    Public Sub checkbox_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cbTag As CheckBox = CType(sender, CheckBox)
        Dim tagId As Integer = cbTag.Attributes(ID_ATTRIBUTE)

        TagManager.AlterProjectTag(tagId, ProjectId, Not cbTag.Checked)





        ' TODO :- Add Audit Trail
        Dim keyword As String = "removed"

        If cbTag.Checked = True Then
            keyword = "added"
        End If

        Dim auditNote As String = String.Format("User {0} {1} the tag {2} to the project {3}.", "TODO USER", keyword, cbTag.Text, ProjectId)

        ' TODO :- INSERT TO AUDIT TRAIL
    End Sub
#End Region

#Region "Private Implementation"
    Private Sub SetupPage()
        Dim allTags As List(Of Tag) = TagManager.GetTags()

        rptrProjectTags.DataSource = allTags
        rptrProjectTags.DataBind()
    End Sub
#End Region

End Class
