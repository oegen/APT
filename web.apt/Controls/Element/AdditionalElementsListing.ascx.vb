'----------------------------------------------------------------------------------------------
' Filename    : AdditionalElementsListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Controls_Element_AdditionalElementsListing
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event AdditionalElementDeleted As EventHandler

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

        If Page.IsPostBack = False Then
            BindAdditionalElements()
        End If

    End Sub

    Protected Sub grdvAdditionalElementListing_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvAdditionalElementListing.PageIndexChanging
        grdvAdditionalElementListing.PageIndex = e.NewPageIndex
        BindAdditionalElements()
    End Sub

    Protected Sub grdvAdditionalElementListing_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAdditionalElementListing.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpAdditionalElement As AdditionalElement = CType(e.Row.DataItem, AdditionalElement)
            Dim ltlName As Literal = CType(e.Row.FindControl("ltlName"), Literal)
            Dim ltlCost As Literal = CType(e.Row.FindControl("ltlCost"), Literal)
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            Dim hypPremiumBrief As HyperLink = CType(e.Row.FindControl("hypPremiumBrief"), HyperLink)
            Dim hypAdditionalElements As HyperLink = CType(e.Row.FindControl("hypAdditionalElements"), HyperLink)

            hypAdditionalElements.NavigateUrl = String.Format("~/Elements/AdditionalElement.aspx?additionalElementId={0}&projectid={1}", tmpAdditionalElement.ID, tmpAdditionalElement.Project.ID)
            ltlName.Text = tmpAdditionalElement.Name
            ltlCost.Text = FormatHelper.FormatPrice(tmpAdditionalElement.Cost)

            GenericUtilities.AddConfirmBoxToLinkButton(lnkDelete, "Are you sure you want to delete this premium product?")
            lnkDelete.CommandArgument = tmpAdditionalElement.ID
            hypPremiumBrief.NavigateUrl = String.Format("~/Elements/PremiumElementDetails.aspx?additionalElementId={0}&projectId={1}", tmpAdditionalElement.ID, tmpAdditionalElement.Project.ID)

        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim additionalElementId As Integer = CType(sender, LinkButton).CommandArgument
        ElementManager.SetAdditionalElementInActive(additionalElementId, SessionManager.LoggedInUserId)
        RaiseEvent AdditionalElementDeleted(Me, New EventArgs)
        BindAdditionalElements()

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindAdditionalElements()

        Dim projectAdditionalElements As List(Of AdditionalElement) = ElementManager.GetAdditionalElementsByProject(ProjectId)

        If projectAdditionalElements.Count = 0 Then
            pnlAdditionalElementListing.Visible = False
            pnlNoItems.Visible = True
        End If

        grdvAdditionalElementListing.DataSource = projectAdditionalElements
        grdvAdditionalElementListing.DataBind()

    End Sub

#End Region

End Class
