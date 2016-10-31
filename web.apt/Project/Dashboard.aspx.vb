Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Project_Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SelectedTab = ProjectTabIndex.DASHBOARD
            LoadDashboard()
        End If

    End Sub

    Public Sub LoadDashboard()

        If IsNumeric(Request.QueryString("projectId")) Then

            Dim loadProjectDetails As Project = aptBusinessLogic.ProjectManager.GetProject(Request.QueryString("projectId"))

            If loadProjectDetails IsNot Nothing Then

                Dim costings As ProjectCostings = aptBusinessLogic.ProjectManager.GetProjectCostings(loadProjectDetails.ID)
                Dim kitBrief As ProjectKittingBrief = ProjectManager.GetProjectKittingBriefByProject(loadProjectDetails.ID)

                With loadProjectDetails

                    ' CORE DETAILS

                    ltlAIN.Text = .ID
                    ltlProjectTitle.Text = .Name
                    ltlBrand.Text = GetProjectBrand(loadProjectDetails.ID)
                    ltlDesc.Text = .Description
                    ' ltlThemeTag.Text =  TODO
                    ltlBudgetCode.Text = .BudgetCode
                    ' ltlStorageCode.Text =  TODO

                    ' DATES
                    ltlBriefSubmitDate.Text = ProjectManager.GetBriefSubmittedDate(loadProjectDetails.ID)
                    ltlInTradeDate.Text = loadProjectDetails.RequiredDate = .RequiredDate
                    ' ltlNoOfWeeks.Text = TODO
                    ' ltlCampaignEndDate.Text = TODO

                    ' REQUIREMENTS 
                    ' ltlPrintRequirement.Text = TODO
                    ' ltlKittingRequirement.Text = TODO
                    ' ltlPremiumRequirement.Text = TODO
                    ' ltlBBCRequirement.Text = TODO

                    ' QUOTES

                    If costings IsNot Nothing Then

                        ltlEstPrint.Text = costings.EstimatePrintCosts
                        ltlFinalPrint.Text = costings.FinalPrintCosts

                        'ltlEstKitting.text = TODO
                        'ltlFinalKitting.text = TODO

                        ltlEstPremium.Text = costings.AddStudioCosts
                        ltlFinalPremium.Text = costings.FinalStudioCosts

                    End If

                    'KEY PLAYERS
                    Dim projectOwner As AptUser = ProjectManager.GetProjectOwner(loadProjectDetails.ID)

                    If projectOwner IsNot Nothing Then
                        ltlProjectOwner.text = projectOwner.FullName
                    End If

                    Dim approver As AptUser = ProjectManager.GetProjectApprover(loadProjectDetails.ID)

                    If approver IsNot Nothing Then
                        ltlProjectApprover.Text = approver.FullName
                    End If

                    Dim studioQA As AptUser = ProjectManager.GetProjectStudioQA(loadProjectDetails.ID)

                    If studioQA IsNot Nothing Then
                        ltlStudioQA.Text = studioQA.FullName
                    End If

                    Dim legalApprover As AptUser = ProjectManager.GetProjectLegalApprover(loadProjectDetails.ID)

                    If legalApprover IsNot Nothing Then
                        ltlLegalApprover.Text = legalApprover.FullName
                    End If

                    Dim PORaiser As AptUser = ProjectManager.GetProjectPORaiser(loadProjectDetails.ID)

                    If PORaiser IsNot Nothing Then
                        ltlPORaiser.Text = PORaiser.FullName
                    End If

                    'ltlWLeaProjectOwner.Text = TODO
                    'ltlMDAProjectOwner.Text = TODO
                    'ltlKittingProjectOwner.Text = TODO

                    'COSTINGS



                End With

            End If

        End If

    End Sub

End Class
