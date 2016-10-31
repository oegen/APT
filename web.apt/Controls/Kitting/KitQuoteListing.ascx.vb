'----------------------------------------------------------------------------------------------
' Filename    : KitQuoteListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        21/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic

Partial Class Controls_Kitting_KitQuoteListing
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property KitId As Integer
        Get
            Return ViewState(Me.UniqueID & "_kitId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_kitId") = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            GetKitQuotes()
        End If

    End Sub

    Private Sub GetKitQuotes()

        If KitId <> 0 Then
            Dim kitQuotes As List(Of KitQuote) = KitManager.GetQuotesByKit(KitId)

            If kitQuotes.Count > 0 Then
                grdvKitQuote.DataSource = kitQuotes
                grdvKitQuote.DataBind()
            End If
        End If

    End Sub

    Protected Sub grdvKitQuote_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvKitQuote.RowDataBound
        If e.Row.DataItem IsNot Nothing Then
            Dim quote As KitQuote = CType(e.Row.DataItem, KitQuote)
            Dim hypView As HyperLink = CType(e.Row.FindControl("hypView"), HyperLink)
            Dim hypViewContents As HyperLink = CType(e.Row.FindControl("hypViewContents"), HyperLink)

            hypViewContents.NavigateUrl = String.Format("~/Kitting/KitElements.aspx?ProjectId={0}&QuoteId={1}", quote.Kit.Project.ID, quote.ID)
            hypView.NavigateUrl = String.Format("~/Kitting/Kit.aspx?ProjectId={0}&QuoteId={1}", quote.Kit.Project.ID, quote.ID)
        End If
    End Sub

End Class
