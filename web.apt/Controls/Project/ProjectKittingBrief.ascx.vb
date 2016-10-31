'----------------------------------------------------------------------------------------------
' Filename    : ProjectKittingBrief.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic

Partial Class Controls_Project_ProjectKittingBrief
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event KittingBriefSaveSuccess As Eventhandler
    Public Event KittingBriefSaveFailed As Eventhandler

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
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

        If Page.IsPostBack = False Then
            BindAvailableKits()
            ReadOnlyCheck()
            LoadProjectKittingBrief()
        End If

    End Sub



#End Region

#Region "Public Methods"

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            SaveProjectKittingBrief()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadProjectKittingBrief()

        If ProjectId <> 0 Then

            Dim loadProjectKitBrief As ProjectKittingBrief = ProjectManager.GetProjectKittingBriefByProject(ProjectId)

            If loadProjectKitBrief IsNot Nothing Then

                With loadProjectKitBrief

                    radListKitsBuiltMDA.SelectedValue = Convert.ToInt32(.BuiltByMDA)
                    txtBudgetCode.Text = .Funding
                    txtKitStockCode.Text = .StockCode
                    txtTotalKits.Text = .TotalNoKits
                    txtInstructions.Text = .Instructions

                    BriefSubmitDate.SelectedDate = .KitsOnStockDate
                    'ProposalReqDate.SelectedDate = .ProposalRequiredDate
                    'QuoteProvidedDate.SelectedDate = .QuoteProvidedDate
                    'KitToBeBuiltDate.SelectedDate = .KitBuildDate
                    'DeliveryDate.SelectedDate = .DeliveryDate
                    InHouseDate.SelectedDate = .InTradeDate
                    ExpiryDate.SelectedDate = .ExpiryDate

                    txtTotalKitQuantity.Text = .TotalKitQuantity
                    txtKittingCosts.Text = .Costs

                    If .Kit IsNot Nothing Then
                        ddlKits.SelectedValue = .Kit.ID
                        Dim tmpQuote As KitQuote = KitManager.GetLatestKitQuote(ddlKits.SelectedValue)
                        ctrlKitContents.IsReadOnly = True
                        ctrlKitContents.QuoteId = tmpQuote.ID
                        plcKitContents.Visible = True
                        ShowPrintableVersionURL()
                    End If

                End With

            End If

        End If

    End Sub

    Private Sub SaveProjectKittingBrief()

        Dim saveProjKitBrief As ProjectKittingBrief

        If ProjectId <> 0 Then
            saveProjKitBrief = ProjectManager.GetProjectKittingBriefByProject(ProjectId)

            If saveProjKitBrief Is Nothing Then
                saveProjKitBrief = New ProjectKittingBrief
            End If
        End If

        saveProjKitBrief.Project = ProjectManager.GetProject(ProjectId)

        With saveProjKitBrief

            .BuiltByMDA = radListKitsBuiltMDA.SelectedValue

            .TotalKitQuantity = txtTotalKitQuantity.Text
            .Costs = txtKittingCosts.Text

            .Funding = txtBudgetCode.Text
            .StockCode = txtKitStockCode.Text
            .TotalNoKits = txtTotalKits.Text
            .Instructions = txtInstructions.Text

            .KitsOnStockDate = BriefSubmitDate.SelectedDate
            '.ProposalRequiredDate = ProposalReqDate.SelectedDate
            '.QuoteProvidedDate = QuoteProvidedDate.SelectedDate
            '.KitBuildDate = KitToBeBuiltDate.SelectedDate
            '.DeliveryDate = DeliveryDate.SelectedDate
            .InTradeDate = InHouseDate.SelectedDate
            .ExpiryDate = ExpiryDate.SelectedDate
            .Kit = KitManager.GetKit(ddlKits.SelectedValue)

        End With

        ProjectManager.SaveProjectKittingBrief(saveProjKitBrief, SessionManager.LoggedInUserId)
        RaiseEvent KittingBriefSaveSuccess(Me, New EventArgs)
        ShowPrintableVersionURL()

    End Sub

    Private Sub ReadOnlyCheck()

        If IsReadOnly = True Then
            'Brief 

            radListKitsBuiltMDA.Enabled = False
            txtBudgetCode.Enable = False
            txtKitStockCode.Enable = False
            txtTotalKits.Enable = False
            txtInstructions.Enable = False

            ' Dates

            BriefSubmitDate.Enable = False
            ProposalReqDate.Enable = False
            QuoteProvidedDate.Enable = False
            KitToBeBuiltDate.Enable = False
            DeliveryDate.Enable = False
            InHouseDate.Enable = False
            ExpiryDate.Enable = False

            plcSave.Visible = False
        End If

    End Sub

    Private Sub ShowPrintableVersionURL()
        hypPrintable.Visible = False
        hypPrintable.NavigateUrl = String.Format("~/Kitting/PrintableKittingBrief.aspx?projectId={0}", ProjectId)

        lnkPrintable.Visible = True
        lnkPrintable.PostBackUrl = String.Format("~/Kitting/PrintableKittingBrief.aspx?projectId={0}", ProjectId)
    End Sub

    Private Sub BindAvailableKits()
        Dim projectKits As List(Of Kit) = KitManager.GetKitsByProject(ProjectId)
        ddlKits.BindDataToDropDown(projectKits, "ID", "Name", "Kit")

        If projectKits.Count = 0 Then
            plcSave.Visible = False
        End If
    End Sub

#End Region

    Protected Sub ddlKits_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlKits.SelectedIndexChanged

        If ddlKits.SelectedIndex > 0 Then
            ' make sure they've actually selected a kit
            Dim tmpQuote As KitQuote = KitManager.GetLatestKitQuote(ddlKits.SelectedValue)

            If tmpQuote IsNot Nothing Then
                plcKitContents.Visible = True
                ctrlKitContents.IsReadOnly = True
                ctrlKitContents.QuoteId = tmpQuote.ID
            Else
                ltlKitContents.Visible = True
            End If
        Else
            ctrlKitContents.Visible = False
        End If

    End Sub

End Class
