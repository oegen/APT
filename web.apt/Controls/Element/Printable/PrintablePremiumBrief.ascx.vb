'----------------------------------------------------------------------------------------------
' Filename    : PrintablePremiumBrief.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        10/11/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Element_Printable_PrintablePremiumBrief
    Inherits System.Web.UI.UserControl

    #Region "Constants and Enumerations"

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_ProjectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_ProjectId") = value
            LoadPremiumBrief()
        End Set
    End Property

#End Region

#Region "Private Implementation"

    Private Sub LoadPremiumBrief()

        Dim premBrief As PremiumBrief = ProjectManager.GetPremiumBriefByProject(ProjectId)

        If premBrief IsNot Nothing Then
            'SiteUtils.LoadNullable(premBrief.BriefSubmittedDate, ltlDateBriefSubmitted)
            'SiteUtils.LoadNullable(premBrief.ProposalRequiredDate, ltlProposalRequiredDate)
            'SiteUtils.LoadNullable(premBrief.QuoteProvidedDate, ltlQuoteProvidedDate)
            'SiteUtils.LoadNullable(premBrief.ArtworkAvailableDate, ltlArtworkAvailableFrom)
            'SiteUtils.LoadNullable(premBrief.PODate, ltlPODate)
            'SiteUtils.LoadNullable(premBrief.DeliveryDate, ltlDeliveryDate)
            'SiteUtils.LoadNullable(premBrief.DeliveryAddress, ltlAddress)

            ltlTotalCostEst.Text = premBrief.TotalCostingEstimate
            ltlTotalCostFinal.Text = premBrief.TotalCostingFinal

            ltlProductionEst.Text = premBrief.ProductionTimeCostEstimate
            ltlProductionFinal.Text = premBrief.ProductionTimeCostFinal

            ltlPrevCostEst.Text = premBrief.PreviousCostEstimate
            ltlPrevCostFinal.Text = premBrief.PreviousCostFinal
            ltlPreviousSupplier.Text = premBrief.PreviousSupplier

        End If

    End Sub

#End Region

End Class

