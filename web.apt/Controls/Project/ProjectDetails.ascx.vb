'----------------------------------------------------------------------------------------------
' Filename    : ProjectDetails.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports GenericUtilities
Imports System.Collections.Generic
Imports System.Data.Linq

Partial Class Controls_Project_ProjectDetails
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event ProjectDetailsChanged As Eventhandler
    Public Event ProjectStopped As EventHandler
    Public Event ProjectArchived As EventHandler

    Public Property ProjectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
        End Set
    End Property

    Public Property ReadOnlyMode As Boolean
        Get
            Return ViewState("_readOnly")
        End Get
        Set(ByVal value As Boolean)
            ViewState("_readOnly") = value
            SetReadOnly()
        End Set
    End Property

    Public Property AllowStop As Boolean
        Get
            Return lnkStop.Visible
        End Get
        Set(ByVal value As Boolean)
            lnkStop.Visible = value
        End Set
    End Property

    Public Property AllowArchive As Boolean
        Get
            Return lnkArchive.Visible
        End Get
        Set(ByVal value As Boolean)
            lnkArchive.Visible = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        plcBindingFix.DataBind()
        If Page.IsPostBack = False Then
            InitialSetup()
        End If
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        SaveProject()
    End Sub

    Protected Sub lnkStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStop.Click
        ProjectManager.StartStopProject(ProjectId)
        RaiseEvent ProjectStopped(Me, New EventArgs)
    End Sub

    Protected Sub lnkArchive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkArchive.Click
        ProjectManager.ArchiveUnarchiveProject(ProjectId)
        RaiseEvent ProjectArchived(Me, New EventArgs)
    End Sub

#End Region

#Region "Private Implementations"

    Public Sub CanUserSeeInActiveProjectCheck()

        Dim currentProject As Project = ProjectManager.GetProject(ProjectId)

        If currentProject IsNot Nothing Then
            If currentProject.Active = False Then
                ' Who should be able to see this?
                ' At the moment we will let only the admin / coordinator of the project see this
                If IsUserProjCoordinatorOrAdmin() = False Then
                    Response.Redirect("~/Project/ProjectListing.aspx")
                End If
            End If
        End If

    End Sub

#Region "Setup"

    Private Sub InitialSetup()
        CanUserSeeInActiveProjectCheck()
        BindDropDowns()
        LoadProject()
        SetProjectActionVisibility()
    End Sub

#End Region

#Region "Load Project"

    Private Sub SetProjectActionVisibility()

        If IsUserProjCoordinatorOrAdmin() = True Then

            Dim currentProject As Project = ProjectManager.GetProject(ProjectId)

            If currentProject.Active = True Then
                ulProjectActions.Visible = True
            End If

        End If

    End Sub

    Private Sub LoadProject()
        Dim currentProject As Project = ProjectManager.GetProject(ProjectId)

        If currentProject IsNot Nothing Then
            Dim projectOwner As AptUser = ProjectManager.GetProjectOwner(currentProject.ID)
            Dim poRaiser As AptUser = ProjectManager.GetProjectPORaiser(currentProject.ID)
            Dim legalApprover As AptUser = ProjectManager.GetProjectLegalApprover(currentProject.ID)
            Dim studioQA As AptUser = ProjectManager.GetProjectStudioQA(currentProject.ID)
            Dim coordinator As AptUser = ProjectManager.GetProjectCoordinator(currentProject.ID)
            Dim brandManager As AptUser = ProjectManager.GetProjectBrandManager(currentProject.ID)
            Dim wleaManager As AptUser = ProjectManager.GetWilliamsLeaProjectManager(currentProject.ID)

            txtAINNumber.Text = currentProject.ID
            txtName.Text = currentProject.Name
            ' txtOverview.Text = currentProject.Description
            txtBudgetCode.Text = currentProject.BudgetCode
            dtpRequiredDate.SelectedDate = currentProject.RequiredDate.ToString("dd/MM/yyyy")

            If currentProject.RequiredPrintDate.HasValue Then
                dtRequiredPrintDate.SelectedDate = currentProject.RequiredPrintDate.Value.ToString("dd/MM/yyyy")
            End If

            If projectOwner IsNot Nothing Then
                ctrlOwner.SelectedUserId = projectOwner.ID
            End If

            If poRaiser IsNot Nothing Then
                ctrlPORaiser.SelectedUserId = poRaiser.ID
                'ddlPORaiser.SelectedValue = poRaiser.ID
            End If

            If legalApprover IsNot Nothing Then
                ctrlLegalApprover.SelectedUserId = legalApprover.ID
            End If

            If brandManager IsNot Nothing Then
                ctrlBrandManager.SelectedUserId = brandManager.ID
            End If

            If wleaManager IsNot Nothing Then
                ' ctrlWLeaProjectManager.SelectedUserId = wleaManager.ID
                ddlWleaProj.SelectedValue = wleaManager.ID
                hdnPreviousWLea.Value = wleaManager.ID
            End If

            If studioQA IsNot Nothing Then
                ' ctrlStudioQA.SelectedUserId = studioQA.ID
                ddlStudioQA.SelectedValue = studioQA.ID
            End If

            If coordinator IsNot Nothing Then
                'ctrlCoordinator.SelectedUserId = coordinator.ID
                ddlCoord.SelectedValue = coordinator.ID
            End If

            txtPrintRefNum.Text = ProjectManager.GetProjectReferenceNumber(currentProject.ID)
            ddlBrandList.SelectedValue = ProjectManager.GetProjectBrandId(currentProject.ID)
            ddlTypeOfWork.SelectedValue = ProjectManager.GetProjectTypeOfWork(currentProject.ID)
            ddlBusinessArea.SelectedValue = ProjectManager.GetProjectBusinessArea(currentProject.ID)
            'txtQuote.Text = ProjectManager.GetProjectQuote(currentProject.ID)

            hdnBrandSelected.Value = ddlBrandList.SelectedValue
            hdnBusinessAreaSelected.Value = ddlBusinessArea.SelectedValue
            hdnTypeOfWorkSelected.Value = ddlTypeOfWork.SelectedValue

            If currentProject.IsBDProject Then
                liAvailableBudget.Visible = True
                txtAvailableBudget.Text = currentProject.AvailableBudget
            End If

        End If
    End Sub

#End Region

#Region "Save Project"

    Private Sub SaveProject()
        If Page.IsValid = True Then
            Dim isExisting As Boolean = True
            Dim currentProject As Project = ProjectManager.GetProject(ProjectId)
            Dim oldProject As Project = ProjectManager.GetPinnedProject(ProjectId)

            If currentProject Is Nothing Then
                currentProject = New Project
                oldProject = New Project
                isExisting = False
            End If

            currentProject.Name = txtName.Text
            ' currentProject.Description = txtOverview.Text
            currentProject.BudgetCode = txtBudgetCode.Text
            currentProject.RequiredDate = dtpRequiredDate.SelectedDate
            currentProject.AvailableBudget = txtAvailableBudget.Text

            If dtRequiredPrintDate.SelectedDate.HasValue Then
                currentProject.RequiredPrintDate = dtRequiredPrintDate.SelectedDate.Value
            End If

            'Dim roleChanges As String = GetRoleChangedAudit()
            Dim schemaChanges As String = GetSchemaAttributesChangedAudit()

            ProjectManager.AlterOwner(ProjectId, ctrlOwner.SelectedUserId, SessionManager.LoggedInUserId)
            ProjectManager.AlterPORaiser(ProjectId, ctrlPORaiser.SelectedUserId, SessionManager.LoggedInUserId)
            ProjectManager.AlterLegalApprover(ProjectId, ctrlLegalApprover.SelectedUserId, SessionManager.LoggedInUserId)
            ProjectManager.AlterStudioQA(ProjectId, ddlStudioQA.SelectedValue, SessionManager.LoggedInUserId)
            ProjectManager.AlterCoordinator(ProjectId, ddlCoord.SelectedValue, SessionManager.LoggedInUserId)

            If ctrlBrandManager.SelectedUserId <> 0 Then
                ProjectManager.AlterBrandManager(ProjectId, ctrlBrandManager.SelectedUserId, SessionManager.LoggedInUserId)
            End If

            If ddlWleaProj.SelectedValue <> 0 Then
                ' The user does not have to save W/Lea manager until step G in the workflow
                ProjectManager.AlterWilliamsLeaProjectManager(ProjectId, ddlWleaProj.SelectedValue, SessionManager.LoggedInUserId)
                hdnPreviousWLea.Value = ddlWleaProj.SelectedValue
            ElseIf String.IsNullOrEmpty(hdnPreviousWLea.Value) = False Then
                UserManager.RevokeUserProjectRole(hdnPreviousWLea.Value, AppSettingsGet.WilliamsLeaProjectManagerID, ProjectId)
            End If

            ProjectManager.AlterBrandSelection(ProjectId, ddlBrandList.SelectedValue)
            ProjectManager.AlterTypeOfWorkSelection(ProjectId, ddlTypeOfWork.SelectedValue)
            ProjectManager.AlterBusinessAreaSelection(ProjectId, ddlBusinessArea.SelectedValue)

            ProjectManager.AlterPrintReference(ProjectId, txtPrintRefNum.Text)
            'ProjectManager.AlterQuote(ProjectId, txtQuote.Text)

            If isExisting Then
                ProjectManager.UpdateProject(currentProject, oldProject, SessionManager.LoggedInUserId, schemaChanges)
            Else
                ProjectManager.AddNewProject(currentProject, ctrlOwner.SelectedUserId, SessionManager.LoggedInUserId)
            End If

            RaiseEvent ProjectDetailsChanged(Me, New EventArgs())
        End If
    End Sub

#End Region

#Region "Bindings"

    Private Sub BindDropDowns()
        BindBrandList()
        BindTypeOfWork()
        BindBusinessAreas()
        BindDefaultUsers(ddlStudioQA, UserManager.GetDefaultStudioQAUsers()) ' QAUsers
        BindDefaultUsers(ddlCoord, UserManager.GetDefaultProjectCoordinators()) ' Coordinators
        BindDefaultUsers(ddlWleaProj, UserManager.GetDefaultWLeaProjManager())
    End Sub

    Private Sub BindBrandList()
        Dim brandList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.BrandListId)

        ddlBrandList.SelectedValue = Nothing

        modComponent.BindDropDown(ddlBrandList, brandList, "ID", "Name", "Brand", "- Select a {0} -")
    End Sub

    Private Sub BindTypeOfWork()
        Dim typeOfWorkList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.TypeOfWorkListId)

        ddlTypeOfWork.SelectedValue = Nothing

        modComponent.BindDropDown(ddlTypeOfWork, typeOfWorkList, "ID", "Name", "Type of Work", "- Select a {0} -")
    End Sub

    Private Sub BindBusinessAreas()
        Dim businessAreaList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.BusinessAreaId)

        ddlBusinessArea.SelectedValue = Nothing

        modComponent.BindDropDown(ddlBusinessArea, businessAreaList, "ID", "Name", "Business Area", "- Select a {0} -")
    End Sub

#End Region

#Region "Utilities"

    Private Sub SetReadOnly()
        txtName.Enable = Not ReadOnlyMode
        txtBudgetCode.Enable = Not ReadOnlyMode
        txtPrintRefNum.Enable = Not ReadOnlyMode
        'txtQuote.Enable = Not ReadOnlyMode

        ctrlLegalApprover.Enabled = Not ReadOnlyMode
        ctrlOwner.Enabled = Not ReadOnlyMode
        ctrlPORaiser.Enabled = Not ReadOnlyMode
        ctrlBrandManager.Enabled = Not ReadOnlyMode
        ddlWleaProj.Enabled = Not ReadOnlyMode
        'ddlPORaiser.Enabled = Not ReadOnlyMode
        'ctrlStudioQA.Enabled = Not ReadOnlyMode
        ddlStudioQA.Enabled = Not ReadOnlyMode
        ddlCoord.Enabled = Not ReadOnlyMode

        ddlBrandList.Enabled = Not ReadOnlyMode
        ddlTypeOfWork.Enabled = Not ReadOnlyMode
        dtpRequiredDate.Enable = Not ReadOnlyMode

        pcSave.Visible = Not ReadOnlyMode
        lnkArchive.Visible = Not ReadOnlyMode
        'lnkEdit.Visible = Not ReadOnlyMode
        lnkStop.Visible = Not ReadOnlyMode
    End Sub

    Private Function GetRoleChangedAudit() As String
        ' This is done here rather than in the BLL because passing all the roles to the asve function becomes quite a mess...

        Dim projectOwner As AptUser = ProjectManager.GetProjectOwner(ProjectId)
        Dim poRaiser As AptUser = ProjectManager.GetProjectPORaiser(ProjectId)
        Dim legalApprover As AptUser = ProjectManager.GetProjectLegalApprover(ProjectId)
        Dim studioQA As AptUser = ProjectManager.GetProjectStudioQA(ProjectId)
        Dim coordinator As AptUser = ProjectManager.GetProjectCoordinator(ProjectId)
        Dim brandManager As AptUser = ProjectManager.GetProjectBrandManager(ProjectId)
        Dim wleaManager As AptUser = ProjectManager.GetWilliamsLeaProjectManager(ProjectId)
        Dim auditLog As String = ""
        Dim newlineString As String = "<br />"

        If projectOwner.ID <> ctrlOwner.SelectedUserId Then
            Dim newOwner As AptUser = UserManager.GetUser(ctrlOwner.SelectedUserId)
            auditLog += String.Format("The project owner has been changed from {0} to {1}", projectOwner.FullName, newOwner.FullName)
            auditLog += newlineString
        End If

        If poRaiser.ID <> ctrlPORaiser.SelectedUserId Then
            Dim newPORaiser As AptUser = UserManager.GetUser(ctrlPORaiser.SelectedUserId)
            'Dim newPORaiser As AptUser = UserManager.GetUser(ddlPORaiser.SelectedValue)
            auditLog += String.Format("The PO Raiser has been changed from {0} to {1}", poRaiser.FullName, newPORaiser.FullName)
            auditLog += newlineString
        End If

        If legalApprover.ID <> ctrlLegalApprover.SelectedUserId Then
            Dim newLegalApprover As AptUser = UserManager.GetUser(ctrlLegalApprover.SelectedUserId)
            auditLog += String.Format("The legal approver has been changed from {0} to {1}", legalApprover.FullName, newLegalApprover.FullName)
            auditLog += newlineString
        End If

        If brandManager.ID <> ctrlBrandManager.SelectedUserId Then
            Dim newBrandMananger As AptUser = UserManager.GetUser(ctrlBrandManager.SelectedUserId)
            auditLog += String.Format("The brand manager has been changed from {0} to {1}", brandManager.FullName, brandManager.FullName)
            auditLog += newlineString
        End If

        If wleaManager.ID <> ddlWleaProj.SelectedValue Then
            Dim wleaProjectManager As AptUser = UserManager.GetUser(ddlWleaProj.SelectedValue)
            auditLog += String.Format("The Williams Lea project manager has been changed from {0} to {1}", wleaProjectManager.FullName, wleaProjectManager.FullName)
            auditLog += newlineString
        End If

        If studioQA.ID <> ddlStudioQA.SelectedValue Then
            'Dim newStudioQA As AptUser = UserManager.GetUser(ctrlStudioQA.SelectedUserId)
            Dim newStudioQA As AptUser = UserManager.GetUser(ddlStudioQA.SelectedValue)
            auditLog += String.Format("The studio QA has been changed from {0} to {1}", studioQA.FullName, newStudioQA.FullName)
            auditLog += newlineString
        End If

        If coordinator.ID <> ddlCoord.SelectedValue Then
            Dim newCoordinator As AptUser = UserManager.GetUser(ddlCoord.SelectedValue)
            auditLog += String.Format("The coordinator has been changed from {0} to {1}", coordinator.FullName, newCoordinator.FullName)
            auditLog += newlineString
        End If



        Return auditLog

    End Function

    Private Function GetSchemaAttributesChangedAudit() As String

        Dim auditLog As String = ""
        Dim newLine As String = "<br />"
        Dim projectRefNo As String = ProjectManager.GetProjectReferenceNumber(ProjectId)
        Dim quote As Integer = ProjectManager.GetProjectQuote(ProjectId)


        If hdnBrandSelected.Value <> ddlBrandList.SelectedValue Then
            If hdnBrandSelected.Value <> 0 Then
                Dim oldBrand As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.BrandListDefinitionID, ProjectId)
                auditLog += String.Format("Brand List has been changed from {0} to {1}", ListManager.GetListNode(oldBrand.SchemaElementValue).Name, ddlBrandList.SelectedItem.Text)
            Else
                auditLog += String.Format("Brand List has been set to {0}", ddlBrandList.Text)
            End If
        End If

        auditLog += newLine

        If hdnTypeOfWorkSelected.Value <> ddlTypeOfWork.SelectedValue Then
            If hdnTypeOfWorkSelected.Value <> 0 Then
                Dim oldTypeOfWork As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.TypeOfWorkDefinitionID, ProjectId)
                auditLog += String.Format("Type of Work has been changed from {0} to {1}", ListManager.GetListNode(oldTypeOfWork.SchemaElementValue).Name, ddlTypeOfWork.SelectedItem.Text)
            Else
                auditLog += String.Format("Type of Work has been set to {0}", ddlTypeOfWork.SelectedItem.Text)
            End If
            auditLog += newLine
        End If

        If hdnBusinessAreaSelected.Value <> ddlBusinessArea.SelectedValue Then
            If hdnBusinessAreaSelected.Value <> 0 Then
                Dim oldBusinessArea As SchemaData = SchemaManager.GetSchemaDataByProjectIdAndDefinitionId(AppSettingsGet.BusinessAreaDefinitionID, ProjectId)
                auditLog += String.Format("Business Area has been changed from {0} to {1}", ListManager.GetListNode(oldBusinessArea.SchemaElementValue).Name, ddlBusinessArea.SelectedItem.Text)
            Else
                auditLog += String.Format("Business Area has been set to {0}", ddlBusinessArea.SelectedItem.Text)
            End If
            auditLog += newLine
        End If

        If projectRefNo <> txtPrintRefNum.Text Then
            If String.IsNullOrEmpty(projectRefNo) Then
                auditLog += String.Format("Willams Lea I Media No has been changed from {0} to {1}", projectRefNo, txtPrintRefNum.Text)
            Else
                auditLog += String.Format("Willams Lea I Media No has been set to {0}", projectRefNo)
            End If
            auditLog += newLine
        End If

        'If quote <> txtQuote.Text Then
        '    auditLog += String.Format("Print Ref No has been changed from {0} to {1}", quote, txtQuote.Text)
        '    auditLog += newLine
        'End If

        Return auditLog

    End Function

    Private Function IsUserProjCoordinatorOrAdmin() As Boolean
        If UserManager.UserHasProjectRole(SessionManager.LoggedInUserId, AppSettingsGet.GraphicsCoordinatorID, ProjectId) = True OrElse _
            UserManager.UserIsAdmin(SessionManager.LoggedInUserId) = True Then
            Return True
        End If

        Return False
    End Function

#End Region

#End Region

    Protected Sub btnPrintable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintable.Click
        Response.Redirect(String.Format("~/Project/Printable/PrintableProjectInfo.aspx?projectId={0}", ProjectId))
    End Sub

    Private Sub BindDefaultUsers(ByVal bindDropdown As DropDownList, ByVal bindUsers As IList(Of AptUser))

        For Each tmpUser As AptUser In bindUsers
            bindDropdown.Items.Add(New ListItem(tmpUser.FullName, tmpUser.ID))
        Next

        ' Add default select item in 
        bindDropdown.Items.Insert(0, New ListItem("- Select a user -", 0))

    End Sub

End Class
