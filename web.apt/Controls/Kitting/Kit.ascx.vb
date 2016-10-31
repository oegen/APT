'----------------------------------------------------------------------------------------------
' Filename    : Kit.ascx.vb
' Description :
'
' Release Initials  Date        Comment

' 2       TL        21/02/2012  Removed some fields as requested by client
'                               Changed logic so that the kits now save on a quote level rather than on a kit level
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Kit_Kit
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Private SAVE_VALIDATION_GROUP As String = "vldSaveKit"

    Private Const BRAND_FUNDED_INDEX = 1
    Private Const SALES_FUNDED_INDEX = 2

#End Region

#Region "Properties"

    Public Event KitSaveSuccess As EventHandler
    Public Event KitSaveFailure As EventHandler

    Public Property KitId As Integer
        Get
            Return ViewState(Me.UniqueID & "_kitId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_kitId") = value
        End Set
    End Property

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

    Public Property HasKitBeenFinalised As Boolean
        Get
            Return ViewState("_hasKitBeenFinalised")
        End Get
        Set(ByVal value As Boolean)
            ViewState("_hasKitBeenFinalised") = value
        End Set
    End Property

    Public Property NewQuote As Boolean
        Get
            Return ViewState("_newQuote")
        End Get
        Set(ByVal value As Boolean)
            ViewState("_newQuote") = value
        End Set
    End Property

    Public Property QuoteId As Integer
        Get
            Return ViewState("_quoteId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_quoteId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False AndAlso (KitId <> 0 OrElse QuoteId <> 0) Then
            LoadKitDetails()
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid = True Then
            Dim quoteId As Integer = SaveKitDetails()

            ' Once saved we redirect the user to the kit content area
            Response.Redirect(String.Format("~/Kitting/KitElements.aspx?projectId={0}&QuoteId={1}", ProjectId, quoteId))
        End If

    End Sub

    Protected Sub ddlBrandFunded_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBrandFunded.SelectedIndexChanged
        ' Hardcoded to two options sales funded and brand funded
        ' brand = 1
        ' sales = 2

        SetFundingView()
    End Sub

    Protected Sub lnkNewQuote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNewQuote.Click
        Dim currentKit As Kit = KitManager.GetKit(KitId)
        Response.Redirect(String.Format("~/Kitting/Kit.aspx?kitId={0}&newQuote=True&projectId={1}", currentKit.ID, ProjectId))
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadKitDetails()

        ' Details can no longer be changed once saved so set to readonly 

        'Dim loadKit As Kit = KitManager.GetKit(KitId)
        'Dim kitQuote As KitQuote = KitManager.GetLatestKitQuote(KitId)

        Dim loadKit As Kit
        Dim kitQuote As KitQuote

        If KitId <> 0 Then
            loadKit = KitManager.GetKit(KitId)
            kitQuote = KitManager.GetLatestKitQuote(KitId)
        End If

        If QuoteId <> 0 Then
            ' Quote will always override
            kitQuote = KitManager.GetKitQuote(QuoteId)
        End If

        txtKitName.Text = kitQuote.Kit.Name

        If NewQuote = False Then
            SetReadOnly()

            With kitQuote
                txtQuantity.Text = .Quantity

                txtKitPacking.Text = .KitPacking
                txtDistribution.Text = .Distribution
                txtPicking.Text = .Picking
                txtManualOrderEntry.Text = .ManualOrderEntry
                txtComments.Text = .Comments
                txtMultipleOrderEntry.Text = .MultipleOrderEntry

                txtSalesFunded.Text = .SalesFunded
                txtBrandFunded.Text = .BrandFunded
                txtBudgetCode.Text = .BudgetCode
                txtCostCentre.Text = .CostCentre
                txtProjectCode.Text = .ProjectCode

                If .IsBrandFunded Then
                    ddlBrandFunded.SelectedIndex = BRAND_FUNDED_INDEX
                Else
                    ddlBrandFunded.SelectedIndex = SALES_FUNDED_INDEX
                End If

                SetFundingView()
            End With

        Else
            ' New quote - we don't allow the user to change kit name
            txtKitName.Enable = False
        End If

    End Sub

    Private Function SaveKitDetails() As Integer

        Dim saveKit As Kit = New Kit
        saveKit.Name = txtKitName.Text

        If KitId <> 0 Then
            saveKit = KitManager.GetKit(KitId)
        End If

        Dim saveQuote As New KitQuote

        With saveQuote

            .Quantity = txtQuantity.Text

            .KitPacking = txtKitPacking.Text
            .Distribution = txtDistribution.Text
            .Picking = txtPicking.Text
            .ManualOrderEntry = txtManualOrderEntry.Text
            .Comments = txtComments.Text
            .MultipleOrderEntry = txtMultipleOrderEntry.Text

            .SalesFunded = txtSalesFunded.Text
            .BrandFunded = txtBrandFunded.Text
            .BudgetCode = txtBudgetCode.Text
            .CostCentre = txtCostCentre.Text
            .ProjectCode = txtProjectCode.Text

            If ddlBrandFunded.SelectedIndex = BRAND_FUNDED_INDEX Then
                .IsBrandFunded = True
            Else
                .IsBrandFunded = False
            End If

        End With

        saveKit.Project = ProjectManager.GetProject(ProjectId)
        KitManager.SaveKitAndQuote(saveKit, SessionManager.LoggedInUserId, saveQuote)
        KitId = saveKit.ID

        RaiseEvent KitSaveSuccess(Me, New EventArgs)

        Return saveQuote.ID

    End Function

    Private Sub SetValidationGroups()

        If ddlBrandFunded.SelectedIndex = BRAND_FUNDED_INDEX Then
            ' Set all the validators for the brand funded stuff to required
            ' Set required for any numeric textbox otherwise it will only validate the format 
            txtBrandFunded.Required = True
            txtBudgetCode.ValidationGroup = SAVE_VALIDATION_GROUP
            txtCostCentre.ValidationGroup = SAVE_VALIDATION_GROUP
            txtProjectCode.ValidationGroup = SAVE_VALIDATION_GROUP
            txtSalesFunded.Required = False
        ElseIf ddlBrandFunded.SelectedIndex = SALES_FUNDED_INDEX Then
            ' Do the opposite and set the validation for the sales funded stuff
            txtBrandFunded.Required = False
            txtBudgetCode.ValidationGroup = ""
            txtCostCentre.ValidationGroup = ""
            txtProjectCode.ValidationGroup = ""
            txtSalesFunded.Required = True
        End If

    End Sub

    Private Sub SetFundingView()

        plcBrandFundInfo.Visible = False
        liSalesFunded.Visible = False

        If ddlBrandFunded.SelectedIndex = BRAND_FUNDED_INDEX Then
            plcBrandFundInfo.Visible = True
        ElseIf ddlBrandFunded.SelectedIndex = SALES_FUNDED_INDEX Then
            liSalesFunded.Visible = True
        End If

        SetValidationGroups()

    End Sub

    Private Sub SetReadOnly()

        ' Textboxes

        txtKitName.Enable = False
        txtQuantity.Enable = False

        txtKitPacking.Enable = False
        txtDistribution.Enable = False
        txtPicking.Enable = False
        txtManualOrderEntry.Enable = False
        txtComments.Enable = False
        txtMultipleOrderEntry.Enable = False

        txtSalesFunded.Enable = False
        txtBrandFunded.Enable = False
        txtBudgetCode.Enable = False
        txtCostCentre.Enable = False
        txtProjectCode.Enable = False

        ddlBrandFunded.Enabled = False

        plcSaveButton.Visible = False

        If HasKitBeenFinalised = False Then
            plcNewQuote.Visible = True
        End If

    End Sub

#End Region

End Class
