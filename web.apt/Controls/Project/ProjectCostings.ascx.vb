Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities

Partial Class Controls_Project_ProjectCostings
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event SaveProjectCostingsSuccess As EventHandler
    Public Event SaveProjectCostingsFailure As EventHandler

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
            LoadCostingDetails()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadCostingDetails()

        If ProjectId <> 0 Then

            lblArtworkCosting.Text = FormatHelper.FormatPrice(ProjectManager.GetArtworkCostings(ProjectId))
            lblKitting.Text = FormatHelper.FormatPrice(ProjectManager.GetKittingCosts(ProjectId))
            lblPremiums.Text = FormatHelper.FormatPrice(ProjectManager.GetPremiumCosts(ProjectId))
            lblPrint.Text = FormatHelper.FormatPrice(ProjectManager.GetPrintCostings(ProjectId))

            Dim loadProjectCostings As ProjectCostings = ProjectManager.GetProjectCostings(ProjectId)

            If loadProjectCostings IsNot Nothing Then
                txtAddStudioCosts.Text = loadProjectCostings.AddStudioCosts
                txtEstimatePrintCost.Text = loadProjectCostings.EstimatePrintCosts
                txtFinalPrintCosts.Text = loadProjectCostings.FinalPrintCosts
                txtFinalStudioCosts.Text = loadProjectCostings.FinalStudioCosts
            End If

        End If

    End Sub



#End Region


    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then

            Dim loadProjectCostings As ProjectCostings = ProjectManager.GetProjectCostings(ProjectId)

            If loadProjectCostings Is Nothing Then
                loadProjectCostings = New ProjectCostings
            End If

            loadProjectCostings.Project = ProjectManager.GetProject(projectId)
            loadProjectCostings.AddStudioCosts = txtAddStudioCosts.Text
            loadProjectCostings.EstimatePrintCosts = txtEstimatePrintCost.Text
            loadProjectCostings.FinalPrintCosts = txtFinalPrintCosts.Text
            loadProjectCostings.FinalStudioCosts = txtFinalStudioCosts.Text

            ProjectManager.SaveProjectCostings(loadProjectCostings)

            RaiseEvent SaveProjectCostingsSuccess(Me, New EventArgs)

        End If

    End Sub


End Class
