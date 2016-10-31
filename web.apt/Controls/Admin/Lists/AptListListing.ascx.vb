Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_Lists_AptListListing
    Inherits System.Web.UI.UserControl

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindAptList()
        End If

    End Sub

    Protected Sub grdvAptListing_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvAptListing.PageIndexChanging

        grdvAptListing.PageIndex = e.NewPageIndex
        BindAptList()

    End Sub

    Protected Sub grdvAptListing_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAptListing.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpAptList As AptList = CType(e.Row.DataItem, AptList)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim hypEditListItem As HyperLink = CType(e.Row.FindControl("hypEditListItem"), HyperLink)

            hypEdit.NavigateUrl = String.Format("~/Admin/Lists/List.aspx?aptListId={0}", tmpAptList.ID)
            hypEditListItem.NavigateUrl = String.Format("~/Admin/Lists/NodeListing.aspx?aptListId={0}", tmpAptList.ID)

        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindAptList()

        Dim aptList As List(Of AptList) = ListManager.GetAllList()

        grdvAptListing.DataSource = aptList
        grdvAptListing.DataBind()

    End Sub

#End Region

End Class
