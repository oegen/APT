'----------------------------------------------------------------------------------------------
' Filename    : PremiumElementDetails.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Element_PremiumElementDetails
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event ElementPremiumDetailsSaveSuccess As EventHandler
    Public Event ElementPremiumDetailsSaveFail As EventHandler

    Public Property AdditionalElementId As Integer
        Get
            Return ViewState(Me.UniqueID & "_additionalElementId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_additionalElementId") = value
        End Set
    End Property

    Public Property PremiumElementBriefId As Integer
        Get
            Return ViewState(Me.UniqueID & "_premiumElementBriefId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_premiumElementBriefId") = value
        End Set
    End Property

    Public Property IsReadOnly As Boolean
        Get
            Return ViewState(Me.UniqueID & "_isReadOnly")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.UniqueID & "_isReadOnly") = value
        End Set
    End Property
#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False AndAlso AdditionalElementId <> 0 Then
            ReadOnlyCheck()
            LoadPremiumElementInfo()
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            SavePremiumElementDetails()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadPremiumElementInfo()

        Dim loadPremiumElementDetail As PremiumElementDetails = ElementManager.GetPremiumElementDetailsByAdditionalElement(AdditionalElementId)

        If loadPremiumElementDetail IsNot Nothing Then

            With loadPremiumElementDetail

                ' txtOutline.Text = .OutlineBrief
                ' radExistOnBBC.SelectedValue = Convert.ToInt32(.DoesItemExistOnBBC)
                txtStockCode.Text = .StockCode
                txtDescription.Text = loadPremiumElementDetail.AdditionalElement.Description
                txtBudgetPerItem.Text = .BudgetPerItem
                txtQuantity.Text = .QuantityBreaks
                txtColours.Text = .Colours
                txtBrand.Text = .Brand
                txtBrandPosition.Text = .BrandPosition
                txtNumberOfBranding.Text = .NumberOfBranding
                txtSizeOfItem.Text = .SizeOfItem
                txtTextile.Text = .Textile
                txtPurposeOfItem.Text = .PurposeOfItem
                txtItemPackaging.Text = .ItemPackaging
                txtWeightRestrictions.Text = .WeightRestriction
                ' radWeightRestrictions.SelectedValue = Convert.ToInt32(.WeightRestriction)
                txtSampleRequired.Text = .SampleRequired
                ' txtDesignRequired.Text = .DesignRequired
                txtHasBDDesign.Text = .HasBDDesignedItem
                txtSimilarProducts.Text = .SimilarProducts

                ' New MDA Changes 

                txtQuoteCosts.Text = .QuoteCost
                txtSupplier.Text = .Supplier
                dtEtaDate.SelectedDate = .EtaDate
                dtExpiryDate.SelectedDate = .ExpiryDate
                radImageUpload.SelectedValue = .ImageUploaded
                txtFinalRequirements.Text = .FinalRequirement

                'New MDA Changes

                txtBriefSubmittedDate.SelectedDate = .BriefSubmittedDate
                txtProposalRequiredDate.SelectedDate = .ProposalRequiredDate
                txtQuoteProvidedDate.SelectedDate = .QuoteProvidedDate
                txtArtworkAvailableDate.SelectedDate = .ArtworkAvailableDate
                txtPODate.SelectedDate = .PODate
                txtDeliveryDate.SelectedDate = .DeliveryDate
                txtDeliveryAddress.Text = .DeliveryAddress

            End With

        End If

    End Sub

    Private Sub SavePremiumElementDetails()

        Dim savePremElementDetails As New PremiumElementDetails

        If AdditionalElementId <> 0 Then
            If ElementManager.GetPremiumElementDetailsByAdditionalElement(AdditionalElementId) IsNot Nothing Then
                savePremElementDetails = ElementManager.GetPremiumElementDetailsByAdditionalElement(AdditionalElementId)
            End If
        End If

        Dim additionalElement As AdditionalElement = ElementManager.GetAdditionalElement(AdditionalElementId)

        With savePremElementDetails

            ' .OutlineBrief = txtOutline.Text
            .AdditionalElement = additionalElement
            ' .DoesItemExistOnBBC = radExistOnBBC.SelectedValue
            .StockCode = txtStockCode.Text
            .AdditionalElement.Description = txtDescription.Text
            .BudgetPerItem = txtBudgetPerItem.Text
            ' txtQuantity.Text = .
            .QuantityBreaks = txtQuantity.Text
            .Colours = txtColours.Text
            .Brand = txtBrand.Text
            .BrandPosition = txtBrandPosition.Text
            .NumberOfBranding = txtNumberOfBranding.Text
            .SizeOfItem = txtSizeOfItem.Text
            .Textile = txtTextile.Text
            .PurposeOfItem = txtPurposeOfItem.Text
            .ItemPackaging = txtItemPackaging.Text
            '.WeightRestriction = radWeightRestrictions.SelectedValue
            .WeightRestriction = txtWeightRestrictions.Text
            .SampleRequired = txtSampleRequired.Text
            ' .DesignRequired = txtDesignRequired.Text
            .HasBDDesignedItem = txtHasBDDesign.Text
            .SimilarProducts = txtSimilarProducts.Text

            ' New MDA Changes 

            .QuoteCost = txtQuoteCosts.Text
            .Supplier = txtSupplier.Text
            .EtaDate = dtEtaDate.SelectedDate
            .ExpiryDate = dtExpiryDate.SelectedDate
            .ImageUploaded = radImageUpload.SelectedValue
            .FinalRequirement = txtFinalRequirements.Text

            .BriefSubmittedDate = txtBriefSubmittedDate.SelectedDate
            .ProposalRequiredDate = txtProposalRequiredDate.SelectedDate
            .QuoteProvidedDate = txtQuoteProvidedDate.SelectedDate
            .ArtworkAvailableDate = txtArtworkAvailableDate.SelectedDate
            .PODate = txtPODate.SelectedDate
            .DeliveryDate = txtDeliveryDate.SelectedDate
            .DeliveryAddress = txtDeliveryAddress.Text

        End With

        ElementManager.SavePremiumElementDetails(savePremElementDetails, SessionManager.LoggedInUserId)
        RaiseEvent ElementPremiumDetailsSaveSuccess(Me, New EventArgs)


    End Sub

    Private Sub ReadOnlyCheck()

        If IsReadOnly Then
            'txtOutline.Enable = False

            'radExistOnBBC.Enabled = False

            txtStockCode.Enable = False
            txtDescription.Enable = False
            txtBudgetPerItem.Enable = False
            txtQuantity.Enable = False
            txtQuantityBreak.Enable = False
            txtColours.Enable = False
            txtBrand.Enable = False
            txtBrandPosition.Enable = False
            txtNumberOfBranding.Enable = False
            txtSizeOfItem.Enable = False
            txtTextile.Enable = False
            txtPurposeOfItem.Enable = False
            txtItemPackaging.Enable = False

            'radWeightRestrictions.Enabled = False
            txtWeightRestrictions.Enable = False

            txtSampleRequired.Enable = False
            'txtDesignRequired.Enable = False
            txtHasBDDesign.enable = False
            txtSimilarProducts.Enable = False

            ' New MDA Changes 

            txtQuoteCosts.Enable = False
            txtSupplier.Enable = False
            dtEtaDate.Enable = False
            dtExpiryDate.Enable = False
            radImageUpload.Enabled = False
            txtFinalRequirements.Enable = False

            divSave.Visible = False
        End If

    End Sub

#End Region

End Class
