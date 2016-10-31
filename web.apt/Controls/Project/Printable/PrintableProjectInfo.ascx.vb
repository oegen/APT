'----------------------------------------------------------------------------------------------
' Filename    : PrintableProjectInfo.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        01/12/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Project_Printable_PrintableProjectInfo
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
            LoadProjectInfo()
        End Set
    End Property

#End Region

#Region "Events"

#End Region

#Region "Public Methods"

#End Region

#Region "Private Implementation"

    Private Sub LoadProjectInfo()
        Dim loadProject As Project = ProjectManager.GetProject(ProjectId)

        If loadProject IsNot Nothing Then

            Dim projectOwner As AptUser = ProjectManager.GetProjectOwner(loadProject.ID)
            Dim poRaiser As AptUser = ProjectManager.GetProjectPORaiser(loadProject.ID)
            Dim legalApprover As AptUser = ProjectManager.GetProjectLegalApprover(loadProject.ID)
            Dim studioQA As AptUser = ProjectManager.GetProjectStudioQA(loadProject.ID)
            Dim coordinator As AptUser = ProjectManager.GetProjectCoordinator(loadProject.ID)

            ltlProject.Text = loadProject.AINName
            ltlSapCodes.Text = loadProject.BudgetCode
            ltlInTradeDate.Text = loadProject.RequiredDate

            If loadProject.RequiredPrintDate.HasValue Then
                ltlReqPrintDelDate.Text = loadProject.RequiredPrintDate.Value.ToString("dd/MM/yyyy")
            End If

            If projectOwner IsNot Nothing Then
                ltlProjectOwner.Text = projectOwner.FullName
            End If

            If poRaiser IsNot Nothing Then
                ltlPoRaiser.Text = poRaiser.FullName
            End If

            If legalApprover IsNot Nothing Then
                ltlLegalApprover.Text = legalApprover.FullName
            End If

            If studioQA IsNot Nothing Then
                ltlStudioQA.Text = studioQA.FullName
            End If

            If coordinator IsNot Nothing Then
                ltlCoordinator.Text = coordinator.FullName
            End If

            Dim brandNode As ListNode = GetListNode(ProjectManager.GetProjectBrandId(loadProject.ID))
            ltlBrand.Text = brandNode.Name

            Dim typeOfWorkNode As ListNode = GetListNode(ProjectManager.GetProjectTypeOfWork(loadProject.ID))
            ltlTypeOfWork.Text = typeOfWorkNode.Name

            Dim busAreaNode As ListNode = GetListNode(ProjectManager.GetProjectBusinessArea(loadProject.ID))
            ltlBusinessArea.Text = busAreaNode.Name

            ltlWLeaMediaNumber.Text = ProjectManager.GetProjectReferenceNumber(loadProject.ID)

            ' Now load the element information

            Dim elementsInProject As List(Of Element) = ElementManager.GetElementsByProject(ProjectId)

            If elementsInProject.Count > 0 Then
                rptrElements.DataSource = elementsInProject
                rptrElements.DataBind()
            Else
                plcElements.Visible = False
                plcNoElements.Visible = True
            End If

        End If
    End Sub

#End Region

    Protected Sub rptrElements_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrElements.ItemDataBound

        If e.Item.DataItem IsNot Nothing Then
            Dim tmpElement As Element = e.Item.DataItem
            Dim printableElement As ASP.controls_element_printable_printableelement_ascx = e.Item.FindControl("ctrlPrintableElement")

            printableElement.PrintableElementID = tmpElement.ID
        End If
    End Sub

End Class
