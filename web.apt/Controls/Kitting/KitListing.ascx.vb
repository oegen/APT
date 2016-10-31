Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities
Imports System.Collections.Generic

Partial Class Controls_Kitting_KitListing
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False AndAlso ProjectId <> 0 Then
            BindKitListing()
        End If

    End Sub

    Protected Sub grdvKit_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvKit.PageIndexChanging
        grdvKit.PageIndex = e.NewPageIndex
        BindKitListing()
    End Sub

    Protected Sub grdvKit_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvKit.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpKit As Kit = CType(e.Row.DataItem, Kit)
            Dim hypLatestQuote As HyperLink = CType(e.Row.FindControl("hypLatestQuote"), HyperLink)
            Dim hypViewQuotes As HyperLink = CType(e.Row.FindControl("hypViewQuotes"), HyperLink)
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)

            hypLatestQuote.NavigateUrl = String.Format("~/Kitting/Kit.aspx?kitId={0}&projectId={1}", tmpKit.ID, tmpKit.Project.ID)
            hypViewQuotes.NavigateUrl = String.Format("~/Kitting/KitQuotes.aspx?kitId={0}&projectId={1}", tmpKit.ID, tmpKit.Project.ID)
            ' ltlCost.Text = FormatHelper.FormatPrice(tmpKit.Cost)

            If KitManager.HasKitBeenFinalised(Request.QueryString("projectId")) = True Then
                lnkDelete.Enabled = False
            Else
                lnkDelete.CommandArgument = tmpKit.ID
                modUtilities.AddConfirmBoxToLinkButton(lnkDelete, "Are you sure you want to remove this kit?")
            End If

        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim kitId As Integer = CType(sender, LinkButton).CommandArgument
        KitManager.RemoveKit(kitId)
        BindKitListing()

    End Sub

#End Region

#Region "Public Methods"

#End Region

#Region "Private Implementation"

    Private Sub BindKitListing()

        Dim projectKits As List(Of Kit) = KitManager.GetKitsByProject(ProjectId)
        grdvKit.DataSource = projectKits
        grdvKit.DataBind()

    End Sub

#End Region


End Class
