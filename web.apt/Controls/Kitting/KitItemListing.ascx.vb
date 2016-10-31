Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Kitting_KitItemListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Public Enum Mode
        ELEMENTS = 0
        PREMIUM_ELEMENTS = 2
        BBC_ITEM = 4
    End Enum

    Private Const LAST_LIST_STYLE As String = "elements-contents last-column"

#End Region

#Region "Properties"

    Private Property CurrentMode As Mode
        Get
            Return ViewState(Me.UniqueID & "_currentMode")
        End Get
        Set(ByVal value As Mode)
            ViewState(Me.UniqueID & "_currentMode") = value
            SetTitle()
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

    Public Property QuoteId As Integer
        Get
            Return ViewState(Me.UniqueID & "_quoteId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_quoteId") = value
        End Set
    End Property

    Public WriteOnly Property LastColumn As Boolean
        Set(ByVal value As Boolean)
            If value = True Then
                divListing.Attributes.Add("class", LAST_LIST_STYLE)
            End If
        End Set
    End Property

    Private Property IsReadOnly As Boolean
        Get
            Return ViewState(Me.UniqueID & "_isReadOnly")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.UniqueID & "_isReadOnly") = value
        End Set
    End Property

#End Region

#Region "Private Implementation"

    Private Sub BindItems()

        Dim itemList As IList = GetDataSource()

        If itemList.Count > 0 Then
            plcItemListing.Visible = True
            rptrItemList.DataSource = itemList
            rptrItemList.DataBind()
        End If

    End Sub

    Private Function GetDataSource() As IList

        Select Case CurrentMode

            Case Mode.BBC_ITEM
                ' Return BBCItemManager.GetBBCItemByProject(ProjectId)
                Return BBCItemManager.GetProjectBBCItems(ProjectId)
            Case Mode.ELEMENTS
                Return ElementManager.GetElementsByProject(ProjectId)
            Case Mode.PREMIUM_ELEMENTS
                Return ElementManager.GetAdditionalElementsByProject(ProjectId)
            Case Else
                Return New ArrayList

        End Select

    End Function

    Private Sub SetTitle()

        Select Case CurrentMode

            Case Mode.ELEMENTS
                ltlTitle.Text = "Elements"
                ltlName.Text = "Elements"
            Case Mode.BBC_ITEM
                ltlTitle.Text = "BBC Items"
                ltlName.Text = "BBC Items"
            Case Mode.PREMIUM_ELEMENTS
                ltlTitle.Text = "Premium Elements"
                ltlName.Text = "Premium Products"

        End Select

    End Sub

    Private Sub GetItemDetails(ByRef itemId As Integer, ByRef name As String, ByRef quantity As Integer, ByVal dataItem As Object)

        Dim tmpKitItem As KitElement = New KitElement

        Select Case CurrentMode

            Case Mode.BBC_ITEM

                Dim bindBBCItem As ProjectBBCItem = CType(dataItem, ProjectBBCItem)
                name = bindBBCItem.BBCItem.CodePartNumberDisplayString
                itemId = bindBBCItem.ID
                tmpKitItem = KitManager.GetKitElementByTypeAndKit(bindBBCItem.ID, QuoteId, _
                                                                  KitManager.GetKitElementTypeId(KitItemType.BBC_ITEM))
            Case Mode.ELEMENTS
                Dim bindElement As Element = CType(dataItem, Element)
                name = bindElement.AINTypeDisplayString
                itemId = bindElement.ID
                tmpKitItem = KitManager.GetKitElementByTypeAndKit(bindElement.ID, QuoteId, _
                                                                  KitManager.GetKitElementTypeId(KitItemType.ELEMENT))
            Case Mode.PREMIUM_ELEMENTS
                Dim bindPremiumElement As AdditionalElement = CType(dataItem, AdditionalElement)
                Dim preElementDetails As PremiumElementDetails = ElementManager.GetPremiumElementDetailsByAdditionalElement(bindPremiumElement.ID)

                If preElementDetails IsNot Nothing Then
                    name = String.Format("{0} - {1} - {2}", preElementDetails.StockCode, bindPremiumElement.Name, preElementDetails.SizeOfItem)
                Else
                    name = bindPremiumElement.Name
                End If

                itemId = bindPremiumElement.ID
                tmpKitItem = KitManager.GetKitElementByTypeAndKit(bindPremiumElement.ID, QuoteId, _
                                                                  KitManager.GetKitElementTypeId(KitItemType.PREMIUM_ELEMENT))

        End Select

        If tmpKitItem IsNot Nothing Then
            quantity = tmpKitItem.Quantity
        Else
            quantity = 0
        End If

    End Sub

#End Region

#Region "Events"

    Protected Sub rptrItemList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrItemList.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or _
           e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim itemName As String = ""
            Dim itemQuantity As Integer = 0
            Dim itemId As Integer = 0
            Dim lblName As HtmlGenericControl = CType(e.Item.FindControl("lblName"), HtmlGenericControl)
            Dim txtQuantity As TextBox = CType(e.Item.FindControl("txtQuantity"), TextBox)
            Dim hidOldQuantity As HiddenField = CType(e.Item.FindControl("hidOldQuantity"), HiddenField)
            Dim hidItemId As HiddenField = CType(e.Item.FindControl("hidItemId"), HiddenField)

            If IsReadOnly = True Then
                txtQuantity.Enabled = False
            End If

            GetItemDetails(itemId, itemName, itemQuantity, e.Item.DataItem)

            lblName.InnerText = itemName
            txtQuantity.Text = itemQuantity
            hidOldQuantity.Value = itemQuantity
            hidItemId.Value = itemId

        End If

    End Sub

#End Region

#Region "Public Methods"

    Public Function GetChangedItems() As List(Of KitItem)

        Dim changedItems As New List(Of KitItem)

        For Each item As RepeaterItem In rptrItemList.Items

            Dim hidOldQuantity As HiddenField = CType(item.FindControl("hidOldQuantity"), HiddenField)
            Dim newQuantity As Integer

            Integer.TryParse(CType(item.FindControl("txtQuantity"), TextBox).Text, newQuantity)

            If hidOldQuantity.Value <> newQuantity Then

                Dim addKitItem As KitItem
                addKitItem.ItemID = CType(item.FindControl("hidItemId"), HiddenField).Value
                addKitItem.Quantity = newQuantity
                hidOldQuantity.Value = newQuantity
                changedItems.Add(addKitItem)

            End If
        Next

        Return changedItems

    End Function

    Public Sub SetCurrentMode(ByVal currentMode As Mode, ByVal isReadOnly As Boolean)

        Me.IsReadOnly = isReadOnly
        Me.CurrentMode = currentMode
        BindItems()

    End Sub

#End Region

End Class
