'----------------------------------------------------------------------------------------------
' Filename    : Element.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities
Imports System.Collections.Generic

Partial Class Controls_Project_Element
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event SaveSuccess As EventHandler
    Public Event SaveFail As EventHandler
    Public Event ElementStopped As EventHandler
    Public Event ElementStarted As EventHandler

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

    Public Property ElementId As Integer
        Get
            Return ViewState(Me.UniqueID & "_elementId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_elementId") = value
        End Set
    End Property

    Private Property ElementAdditionalInfoId As Integer
        Get
            Return ViewState(Me.UniqueID & "_elementAdditionalInfoId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_elementAdditionalInfoId") = value
        End Set
    End Property

    Public Property CurrentMode As FormMode
        Get
            Return ViewState(Me.UniqueID & "_CurrentMode")
        End Get
        Set(ByVal value As FormMode)
            ViewState(Me.UniqueID & "_CurrentMode") = value
            PermissionCheck()
        End Set
    End Property

#End Region

#Region "Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindData()
            FormActiveCheck()

            If ElementId <> 0 Then
                LoadElement()
            Else
                ' if this is a new element then automatically select the business area dropdown to the default project business area
                LoadDefaultFieldValues()
                Dim brand As Integer = ProjectManager.GetProjectBrandId(ProjectId)

                If brand <> 0 Then
                    ddlBrands.SelectedValue = brand
                End If
            End If
        End If

    End Sub

    Public Sub ddlType_ChangedIndex(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        Dim typeId As Integer = ddlType.SelectedValue
        ctlElementInfo.ClearControls()
        BindSubClassTypeDropDownList(typeId)
    End Sub

    Public Sub ddlSubclassType_ChangedIndex(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubclassType.SelectedIndexChanged
        'Dim subclassTypeId As Integer = ddlSubclassType.SelectedValue
        'ctlElementInfo.LoadElementSchemaInformation(subclassTypeId) TEMPO - taken out for now

        ' If this is a new element then automatically set the print lead times and the cost

        Dim selectedSubclassType As SubclassType = AptTypeManager.GetSubclassType(ddlSubclassType.SelectedValue)

        If ElementId = 0 Then
            ' If this is a new element then put in some default data

            txtPrintLeadTimes.Text = selectedSubclassType.PrintLeadTime
            txtArtworkTime.Text = selectedSubclassType.TimeHours
            txtCostArtwork.Text = selectedSubclassType.TimeHours * AppSettingsGet.ArtworkCostPerHour

        End If
    End Sub

    Protected Sub lnkSaveElement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSaveElement.Click
        If Page.IsValid Then
            If PermissionsManager.CanUserSaveElement(SessionManager.LoggedInUserId, ProjectId) Then
                SaveElement()
            End If
        End If
    End Sub

    Protected Sub lnkStartStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStartStop.Click
        If ElementId <> 0 Then
            SetElementActivity()
        End If
    End Sub

    Protected Sub btnPrintable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintable.Click
        Response.Redirect(String.Format("~/Elements/PrintableElement.aspx?projectId={0}&elementId={1}", ProjectId, ElementId))
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadElement()

        Dim loadElement As Element = ElementManager.GetElement(ElementId)

        ' Common element properties 

        txtElementName.Text = loadElement.Name
        txtDescription.Text = loadElement.Description
        ddlTrade.SelectedValue = loadElement.TradeListNode.ID
        ddlTermsCondition.SelectedValue = loadElement.TCListNode.ID

        If loadElement.BrandListNode IsNot Nothing Then
            ddlBrands.SelectedValue = loadElement.BrandListNode.ID
        End If

        ' New requested page info
        If loadElement.PageListNode IsNot Nothing Then
            ddlPage.SelectedValue = loadElement.PageListNode.ID
        End If

        txtNoOfDelAddress.Text = loadElement.NumberOfDeliveryAddress
        txtDeliveryDetails.Text = loadElement.DeliveryDetails

        ' Bind the list here because we want to filter it so that it doesn't show the any inactive types
        ' but we still want the type that was selected even if it is inactive
        Dim typeList As List(Of ElementType) = GetActiveAndSelectedType(loadElement.SubclassType.Type.ID)
        ddlType.BindDataToDropDown(typeList, "ID", "name", "Type")

        ddlType.SelectedValue = loadElement.SubclassType.Type.ID

        Dim subTypeList As List(Of SubclassType) = GetActiveAndSelectedSubclassType(ddlType.SelectedValue, loadElement.SubclassType.ID)

        ddlSubclassType.BindDataToDropDown(subTypeList, "ID", "name", "Subclass")
        liSubclass.Visible = True
        ddlSubclassType.SelectedValue = loadElement.SubclassType.ID

        If loadElement.ArtworkDeliveryDate.HasValue Then
            txtArtworkDeliveryDate.SelectedDate = loadElement.ArtworkDeliveryDate.Value.ToString("dd/MM/yyyy")
        End If

        ' Load Additional Info

        LoadElementAdditionalInfo(ElementId)
        hypViewHistory.NavigateUrl = String.Format("~/Elements/ElementHistory.aspx?elementId={0}&projectId={1}", loadElement.ID, loadElement.Project.ID)
        ulElementActions.Visible = True

        ShowPrintableLink()



    End Sub

    Private Sub LoadElementAdditionalInfo(ByVal elementId As Integer)

        Dim loadElementAdditionalInfo As ElementAdditionalDetails = ElementManager.GetElementAdditionalInfoByElement(elementId)

        If loadElementAdditionalInfo IsNot Nothing Then

            ElementAdditionalInfoId = loadElementAdditionalInfo.ID

            With loadElementAdditionalInfo

                txtQuantity.Text = .Quantity
                txtArtworkTime.Text = .ArtworkTime
                txtCostArtwork.Text = .ArtworkCost
                txtPrintLeadTimes.Text = .PrintLeadTimes
                txtCostPrint.Text = .PrintCost
                txtCostPerItem.Text = .CostPerItem

            End With

        End If

    End Sub

    Private Sub BindData()

        Dim tradeList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.TradeListId)
        Dim tcList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.TCListId)
        Dim brandList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.BrandListId)
        Dim typeList As List(Of ElementType)
        Dim pageList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.ElementPageId)
        Dim quantityList As List(Of ListNode) = ListManager.GetListsNodes(AppSettingsGet.ElementQuantityId)

        If ElementId = 0 Then
            ' this is a new element we - we don't want people adding in types that have been disabled
            typeList = AptTypeManager.GetAptTypes()
            ddlType.BindDataToDropDown(typeList, "ID", "name", "Type")
        End If

        ddlTrade.BindDataToDropDown(tradeList, "ID", "name", "Trade")
        ddlTermsCondition.BindDataToDropDown(tcList, "ID", "name", "T&Cs", "Select {0}")
        ddlBrands.BindDataToDropDown(brandList, "ID", "name", "Brand")
        ddlPage.BindDataToDropDown(pageList, "ID", "name", "Page", "Select {0}")
        'ddlQuantity.BindDataToDropDown(quantityList, "ID", "name", "Quantity", "Select {0}")

    End Sub

    Private Sub BindSubClassTypeDropDownList(ByVal typeId As Integer)

        Dim subTypeList As List(Of SubclassType)

        subTypeList = AptTypeManager.GetSubclassTypesByAptType(typeId)

        ddlSubclassType.BindDataToDropDown(subTypeList, "ID", "name", "Subclass")
        liSubclass.Visible = True
        'ddlSubclassType.Visible = True

    End Sub

    Private Sub SaveElement()

        Dim saveElement As New Element
        Dim saveAdditionalElementInfo As New ElementAdditionalDetails

        If ElementId <> 0 Then

            saveElement = ElementManager.GetElement(ElementId)

            If ElementManager.GetElementAdditionalInfoByElement(ElementId) IsNot Nothing Then
                saveAdditionalElementInfo = ElementManager.GetElementAdditionalInfoByElement(ElementId)
            End If

        End If

        Dim saveElementSchemaInfo As List(Of SchemaData) = ctlElementInfo.ReturnElementAttributes()

        With saveElement

            .Name = txtElementName.Text
            .Description = txtDescription.Text
            .TradeListNode = ListManager.GetListNode(ddlTrade.SelectedValue)
            .TCListNode = ListManager.GetListNode(ddlTermsCondition.SelectedValue)
            .BrandListNode = ListManager.GetListNode(ddlBrands.SelectedValue)
            .PageListNode = ListManager.GetListNode(ddlPage.SelectedValue)

            'If ddlQuantity.SelectedValue <> 0 Then
            '    ' This is no longer a required field so just save if the user has selected something
            '    .QuantityListNode = ListManager.GetListNode(ddlQuantity.SelectedValue)
            'End If

            .NumberOfDeliveryAddress = txtNoOfDelAddress.Text
            .DeliveryDetails = txtDeliveryDetails.Text
            '.AmendTime = txtAmendTime.Text
            '.LoopBack = txtLoopBack.Text
            .SubclassType = AptTypeManager.GetSubclassType(ddlSubclassType.SelectedValue)
            .Active = True
            .Project = ProjectManager.GetProject(ProjectId)

            If txtArtworkDeliveryDate.SelectedDate.HasValue Then
                .ArtworkDeliveryDate = txtArtworkDeliveryDate.SelectedDate.Value
            End If

        End With

        If ElementAdditionalInfoId <> 0 Then
            saveAdditionalElementInfo = ElementManager.GetElementAdditionalInfo(ElementAdditionalInfoId)
        End If

        With saveAdditionalElementInfo

            .Quantity = txtQuantity.Text
            .ArtworkTime = txtArtworkTime.Text
            .ArtworkCost = txtCostArtwork.Text
            .PrintLeadTimes = txtPrintLeadTimes.Text
            .PrintCost = txtCostPrint.Text
            .CostPerItem = txtCostPerItem.Text
            .Element = saveElement

        End With

        ElementManager.SaveElement(saveElement, saveElementSchemaInfo, SessionManager.LoggedInUserId, "<br />", saveAdditionalElementInfo)
        ElementId = saveElement.ID
        ShowPrintableLink()

        RaiseEvent SaveSuccess(Me, New CommandEventArgs("", saveElement))

        ' TODO: save audit trail now

    End Sub

    Private Sub SetElementActivity()

        Dim tmpElement As Element = ElementManager.GetElement(ElementId)

        If tmpElement.ElementStopped = True Then
            ElementManager.SetElementActivity(tmpElement, False, SessionManager.LoggedInUserId)
            CurrentMode = FormMode.ENABLE_SAVE
            ltlStopStartText.Text = "Stop Element"
            spanStartStopButton.Attributes.Add("class", "system-buttons stop")

            RaiseEvent ElementStarted(Me, New EventArgs)

        Else
            ElementManager.SetElementActivity(tmpElement, True, SessionManager.LoggedInUserId)
            ltlStopStartText.Text = "Start Element"
            CurrentMode = FormMode.DISABLE_SAVE
            spanStartStopButton.Attributes.Add("class", "system-buttons start")
            RaiseEvent ElementStopped(Me, New EventArgs)
        End If

    End Sub

    Private Sub PermissionCheck()

        If CurrentMode = FormMode.DISABLE_SAVE Then
            SetFormActivity(False)
        Else
            SetFormActivity(True)
        End If

    End Sub

    Private Sub ShowPrintableLink()
        plcPrintable.Visible = True
    End Sub

    Private Sub LoadDefaultFieldValues()
        txtNoOfDelAddress.Text = AppSettingsGet.NumberOfDelAddress
        txtDeliveryDetails.Text = AppSettingsGet.DefaultCoorsAddress
    End Sub

    Private Sub SetFormActivity(ByVal active As Boolean)

        ' Basic Info
        txtElementName.Enable = active
        txtDescription.Enable = active
        ddlTrade.Enabled = active
        ddlTermsCondition.Enabled = active
        ddlBrands.Enabled = active
        ddlPage.Enabled = active
        txtQuantity.Enable = active
        txtNoOfDelAddress.Enable = active
        txtDeliveryDetails.Enable = active
        txtArtworkDeliveryDate.Enable = active
        divSubmit.Visible = active

        ' Estimated Costings
        txtArtworkTime.Enable = active
        txtCostArtwork.Enable = active
        txtPrintLeadTimes.Enable = active
        txtCostPrint.Enable = active
        txtCostPerItem.Enable = active

        ' If we have the brief has been completed then we need to disable the type and subtype dropdowns

        If WorkflowManager.HasBriefBeenSignedOff(ProjectId) Then
            ddlType.Enabled = False
            ddlSubclassType.Enabled = False
        Else
            ddlType.Enabled = active
            ddlSubclassType.Enabled = active
        End If

    End Sub

    Private Sub FormActiveCheck()

        If ElementId <> 0 Then

            Dim loadElement = ElementManager.GetElement(ElementId)

            ' The user is can only set the element to start or stop IF it has not finished the element workflow
            If ElementManager.HasElementWorkflowFinished(ElementId) Then
                lnkStartStop.Visible = False
                CurrentMode = FormMode.DISABLE_SAVE
            End If

            If PermissionsManager.CanUserSaveElement(SessionManager.LoggedInUserId, ProjectId) = False Then
                ' We need to see if the user has the permission to actually edit elements
                ' At the moment this check is the same as HasElementWorkflowFinished, but once we get who exactly can ch
                lnkStartStop.Enabled = False
                CurrentMode = FormMode.DISABLE_SAVE
            End If

            If loadElement.ElementStopped = True Then
                ltlStopStartText.Text = "Start Element"
                spanStartStopButton.Attributes.Add("class", "system-buttons start")
                CurrentMode = FormMode.DISABLE_SAVE
            Else
                ltlStopStartText.Text = "Stop Element"
                spanStartStopButton.Attributes.Add("class", "system-buttons stop")
            End If

            If WorkflowManager.HasBriefBeenSignedOff(ProjectId) Then
                ddlType.Enabled = False
                ddlSubclassType.Enabled = False
            End If

        End If

    End Sub

#End Region

End Class
