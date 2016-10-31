'----------------------------------------------------------------------------------------------
' Filename    : PrintableElement.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 2       TL        23/2/2011   Added pack size field, removed cost per item and print costs fields
' 1       TL        10/11/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Element_Printable_PrintableElement
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Public Property PrintableElementID As Integer
        Get
            Return ViewState(Me.UniqueID & "_printableElementID")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_printableElementID") = value
            LoadAllElementDetails()
        End Set
    End Property

#End Region

#Region "Events"



#End Region

#Region "Public Methods"

#End Region

#Region "Private Implementation"

    Private Sub LoadAllElementDetails()
        LoadElementDetails()
        LoadBDPrintBrief()
        LoadBDKittingBrief()
    End Sub

    Private Sub LoadElementDetails()

        Dim printElement As Element = ElementManager.GetElement(PrintableElementID)

        ' Element Section
        ltlName.Text = printElement.DisplayString
        ltlType.Text = printElement.SubclassType.Type.Name
        ltlSubclassType.Text = printElement.SubclassType.Name
        ltlDescription.Text = printElement.Description
        ltlTrade.Text = printElement.TradeListNode.Name
        ltlTCs.Text = printElement.TCListNode.Name
        ltlBrands.Text = printElement.BrandListNode.Name
        ltlPage.Text = printElement.PageListNode.Name
        'ltlQuantity.Text = printElement.QuantityListNode.Name
        ltlNoAddress.Text = printElement.NumberOfDeliveryAddress
        ltlDeliveryDetails.Text = printElement.DeliveryDetails
        SiteUtils.LoadNullable(printElement.ArtworkDeliveryDate, ltlArtworkDelivery)

        Dim costingDetails As ElementAdditionalDetails = ElementManager.GetElementAdditionalInfoByElement(PrintableElementID)

        ' Costings
        If costingDetails IsNot Nothing Then
            ltlCostingQuantity.Text = costingDetails.Quantity
            ltlArtworkTime.Text = costingDetails.ArtworkTime
            ltlArtworkCost.Text = costingDetails.ArtworkCost
            ltlPrintLeadTimes.Text = costingDetails.PrintLeadTimes
            ltlCostingPrintCost.Text = costingDetails.PrintCost
            ltlCostPerItem.Text = costingDetails.CostPerItem
        End If

    End Sub

    Private Sub LoadBDPrintBrief()

        ' BD Print Brief
        Dim bdPrintBrief As ElementArtworkDetails = ElementManager.GetElementArtworkInfoByElement(PrintableElementID)

        If bdPrintBrief IsNot Nothing Then
            ltlNoOfColours.Text = bdPrintBrief.NoOfColours
            ltlFinishedSize.Text = bdPrintBrief.FinishedSize
            ltlMaterial.Text = bdPrintBrief.Material
            ltlFinishing.Text = bdPrintBrief.Finishing
            ltlDelAdd.Text = bdPrintBrief.NoOfDelAdds
            ltlBDPrintDeliveryDetails.Text = bdPrintBrief.DeliveryDetails
            ltlPackSize.Text = bdPrintBrief.PackSize
        End If

    End Sub

    Private Sub LoadBDKittingBrief()

        ' BD Kitting Brief

        Dim kittingBrief As ElementKittingDetails = ElementManager.GetElementKittingDetailsByElement(PrintableElementID)

        If kittingBrief IsNot Nothing Then
            ltlKittingCostPerItem.Text = kittingBrief.CostPerItem

            If kittingBrief.ExpiryDate.HasValue Then
                ltlExpiryDate.Text = kittingBrief.ExpiryDate.Value
            Else
                ltlExpiryDate.Text = "N/A"
            End If

            ltlPONumber.Text = kittingBrief.PONumber

            If kittingBrief.Existing = True Then
                ltlExistingNew.Text = "Existing"
            Else
                ltlExistingNew.Text = "New"
            End If

            ltlPrintSupplier.Text = kittingBrief.Supplier

            If kittingBrief.DueDateIntoMDA.HasValue Then
                ltlDueDateMDA.Text = kittingBrief.DueDateIntoMDA.Value
            Else
                ltlDueDateMDA.Text = "N/A"
            End If

        End If

    End Sub

#End Region


End Class
