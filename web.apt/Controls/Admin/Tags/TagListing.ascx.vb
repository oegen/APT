Imports System.Collections.Generic
Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Admin_Tags_TagListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Private Const ENABLE_TAG_COMMAND_NAME As String = "EnableTag"
    Private Const DISABLE_TAG_COMMAND_NAME As String = "DisableTag"

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindTags()
        End If

    End Sub

    Protected Sub grdvTags_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvTags.PageIndexChanging

        grdvTags.PageIndex = e.NewPageIndex
        BindTags()

    End Sub

    Protected Sub grdvTags_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvTags.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpTag As Tag = CType(e.Row.DataItem, Tag)
            Dim hypTag As HyperLink = CType(e.Row.FindControl("hypTag"), HyperLink)
            Dim lnkSetActivity As LinkButton = CType(e.Row.FindControl("lnkSetActivity"), LinkButton)

            lnkSetActivity.CommandArgument = tmpTag.ID

            If tmpTag.Active = True Then
                lnkSetActivity.CommandName = DISABLE_TAG_COMMAND_NAME
                lnkSetActivity.Text = "Disable"
            Else
                lnkSetActivity.CommandName = ENABLE_TAG_COMMAND_NAME
                lnkSetActivity.Text = "Enable"
            End If

            hypTag.NavigateUrl = String.Format("~/Admin/Tags/Tag.aspx?tagId={0}", tmpTag.ID)

        End If

    End Sub

    Protected Sub lnkSetActivity_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim tagId As Integer = e.CommandArgument

        If e.CommandName = ENABLE_TAG_COMMAND_NAME Then
            TagManager.SetTagActivity(tagId, True)
        Else
            TagManager.SetTagActivity(tagId, False)
        End If

        BindTags()

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindTags()

        Dim tagList As List(Of Tag) = TagManager.GetAllTags()

        grdvTags.DataSource = tagList
        grdvTags.DataBind()

    End Sub

#End Region

End Class
