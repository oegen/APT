'----------------------------------------------------------------------------------------------
' Filename    : PremiumBrief.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 2       TL        10/02/2012  Fixed printable version button not hiding properly
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Project_PremiumBrief
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event PremiumBriefSuccess As EventHandler
    Public Event PremiumBriefFail As EventHandler

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

    Public Property PremiumBriefId As Integer
        Get
            Return ViewState(Me.UniqueID & "_premiumBriefId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_premiumBriefId") = value
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

        If Page.IsPostBack = False AndAlso ProjectId <> 0 Then
            ReadOnlyCheck()
            BindMDAManagers()
            LoadPremiumBrief()
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid = True Then
            SavePremiumBrief()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadPremiumBrief()

        If ProjectManager.GetPremiumBriefByProject(ProjectId) IsNot Nothing Then

            Dim loadPremiumBrief As PremiumBrief = ProjectManager.GetPremiumBriefByProject(ProjectId)

            With loadPremiumBrief

                'txtOutline.Text = .OutlineBrief

                'txtBriefSubmittedDate.SelectedDate = .BriefSubmittedDate
                'txtProposalRequiredDate.SelectedDate = .ProposalRequiredDate
                'txtQuoteProvidedDate.SelectedDate = .QuoteProvidedDate
                'txtArtworkAvailableDate.SelectedDate = .ArtworkAvailableDate
                'txtPODate.SelectedDate = .PODate
                'txtDeliveryDate.SelectedDate = .DeliveryDate
                'txtDeliveryAddress.SelectedDate = .DeliveryAddress

                txtTotalCostingEstimate.Text = .TotalCostingEstimate
                txtTotalCostingFinal.Text = .TotalCostingFinal

                txtProductionTimeCostEstimate.Text = .ProductionTimeCostEstimate
                txtProductionTimeCostFinal.Text = .ProductionTimeCostFinal

                txtPreviousCostEstimate.Text = .PreviousCostEstimate
                txtPreviousCostFinal.Text = .PreviousCostFinal

                txtPreviousSupplier.Text = .PreviousSupplier

                ctrlMDAManager.SelectedValue = .MDAManager.ID
                txtBackground.Text = .BackgroundToBrief
                txtPurposeOfProject.Text = .PurposeToProject
                txtTargetMarket.Text = .TargetMarket

                If .OnTrade = True Then
                    radOnTrade.SelectedIndex = 0
                Else
                    radOnTrade.SelectedIndex = 1
                End If

                txtFurtherInfo.Text = .FurtherInfo

                ShowPrintableVersion()
            End With

        End If

    End Sub

    Private Sub SavePremiumBrief()

        Dim savePremiumBrief As New PremiumBrief

        If ProjectId <> 0 Then
            If ProjectManager.GetPremiumBriefByProject(ProjectId) IsNot Nothing Then
                savePremiumBrief = ProjectManager.GetPremiumBriefByProject(ProjectId)
            End If
        End If

        With savePremiumBrief

            '.OutlineBrief = txtOutline.Text

            .TotalCostingEstimate = txtTotalCostingEstimate.Text
            .TotalCostingFinal = txtTotalCostingFinal.Text

            .ProductionTimeCostEstimate = txtProductionTimeCostEstimate.Text
            .ProductionTimeCostFinal = txtProductionTimeCostFinal.Text

            .PreviousCostEstimate = txtPreviousCostEstimate.Text
            .PreviousCostFinal = txtPreviousCostFinal.Text

            .PreviousSupplier = txtPreviousSupplier.Text
            .Project = ProjectManager.GetProject(ProjectId)

            .MDAManager = UserManager.GetUser(ctrlMDAManager.SelectedValue)
            .BackgroundToBrief = txtBackground.Text
            .PurposeToProject = txtPurposeOfProject.Text
            .TargetMarket = txtTargetMarket.Text

            If radOnTrade.SelectedValue = "True" Then
                .OnTrade = True
            Else
                .OnTrade = False
            End If

            .FurtherInfo = txtFurtherInfo.Text

        End With

        ProjectManager.SavePremiumBrief(savePremiumBrief, SessionManager.LoggedInUserId)
        ShowPrintableVersion()
        RaiseEvent PremiumBriefSuccess(Me, New System.EventArgs)

    End Sub

    Private Sub ReadOnlyCheck()
        If IsReadOnly Then
            'txtBriefSubmittedDate.Enable = False
            'txtProposalRequiredDate.Enable = False
            'txtQuoteProvidedDate.Enable = False
            'txtArtworkAvailableDate.Enable = False
            'txtPODate.Enable = False
            'txtDeliveryDate.Enable = False
            'txtDeliveryAddress.Enable = False

            txtTotalCostingEstimate.Enable = False
            txtTotalCostingFinal.Enable = False

            txtProductionTimeCostEstimate.Enable = False
            txtProductionTimeCostFinal.Enable = False

            txtPreviousCostEstimate.Enable = False
            txtPreviousCostFinal.Enable = False
            txtPreviousSupplier.Enable = False

            submit_btn.Visible = False
        End If
    End Sub

    Private Sub ShowPrintableVersion()
        'hypPrintable.Visible = False
        'hypPrintable.NavigateUrl = String.Format("~/Elements/PrintablePremiumBrief.aspx?projectId={0}", ProjectId)

        lnkPrint.PostBackUrl = String.Format("~/Elements/PrintablePremiumBrief.aspx?projectId={0}", ProjectId)
        plcPrintBtn.Visible = True
    End Sub

    Private Sub BindMDAManagers()
        ' ctrlMDAManager.BindDataToDropDown()
        Dim mdaManagers As List(Of AptUser) = UserManager.GetDefaultMDAUsers()

        ctrlMDAManager.BindDataToDropDown(mdaManagers, "ID", "FullName", "user")

    End Sub


#End Region

End Class
