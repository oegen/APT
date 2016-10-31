'----------------------------------------------------------------------------------------------
' Filename    : PrintableKittingBrief.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        10/11/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Project_Printable_PrintableKittingBrief
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Public Property ProjectID As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
            LoadKittingBrief()
        End Set
    End Property

#End Region

#Region "Events"

#End Region

#Region "Public Methods"

#End Region

#Region "Private Implementation"

    Private Sub LoadKittingBrief()

        Dim kitBrief As ProjectKittingBrief = ProjectManager.GetProjectKittingBriefByProject(ProjectID)

        ctrlKitContents.SetAsPrintableVersion()
        ctrlKitContents.QuoteId = KitManager.GetLatestKitQuote(kitBrief.Kit.ID).ID

        If kitBrief IsNot Nothing Then

            If kitBrief.BuiltByMDA = True Then
                ltlKitBuiltByMDA.Text = "Yes"
            Else
                ltlKitBuiltByMDA.Text = "No"
            End If

            ' Brief

            ltlTotalKitQuantity.Text = kitBrief.TotalKitQuantity
            ltlKitName.Text = kitBrief.Kit.Name
            ltlKittingCosts.Text = kitBrief.Costs
            ltlFunding.Text = kitBrief.Funding
            ltlKitStockCode.Text = kitBrief.StockCode
            ltlNoOfKits.Text = kitBrief.TotalNoKits
            ltlInstructions.Text = kitBrief.Instructions

            ' Dates

            ltlKitsOnStockDate.Text = kitBrief.KitsOnStockDate
            'ltlProposalReqDate.Text = kitBrief.ProposalRequiredDate
            'ltlQuoteProvidedDate.Text = kitBrief.QuoteProvidedDate
            'ltlKitToBeBuiltDate.Text = kitBrief.KitBuildDate
            'ltlDeliveryDate.Text = kitBrief.DeliveryDate
            ltlInTradeDate.Text = kitBrief.InTradeDate
            ltlExpiryDate.Text = kitBrief.ExpiryDate

        End If

    End Sub

#End Region

End Class
