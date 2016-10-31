Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Controls_Kitting_KitContents
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property QuoteId As Integer
        Get
            Return ViewState(Me.UniqueID & "_quoteId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_quoteId") = value
            BindKitContents()
        End Set
    End Property

    Public WriteOnly Property ShowKitContents As Boolean
        Set(ByVal value As Boolean)

            If value = True Then
                plcKit.Visible = True
                plcEmpty.Visible = False
            Else
                plcKit.Visible = False
                plcEmpty.Visible = True
            End If

        End Set
    End Property

    Public Property IsReadOnly As Boolean
        Get
            Return ViewState(Me.UniqueID & "_isReadOnly")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.UniqueID & "_isReadOnly") = value
            thAction.Visible = False
            captionKit.Visible = False
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub rptrKitContent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrKitContent.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or _
            e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim bindKitContent As KitElement = CType(e.Item.DataItem, KitElement)
            Dim ltlAIN As Literal = CType(e.Item.FindControl("ltlAIN"), Literal)
            Dim ltlType As Literal = CType(e.Item.FindControl("ltlType"), Literal)
            Dim ltlSubclass As Literal = CType(e.Item.FindControl("ltlSubclass"), Literal)
            Dim ltlPackSize As Literal = CType(e.Item.FindControl("ltlPackSize"), Literal)
            Dim ltlSupplier As Literal = CType(e.Item.FindControl("ltlSupplier"), Literal)
            Dim ltlCost As Literal = CType(e.Item.FindControl("ltlCost"), Literal)
            Dim ltlExpiryDate As Literal = CType(e.Item.FindControl("ltlExpiryDate"), Literal)

            Dim lnkRemove As LinkButton = CType(e.Item.FindControl("lnkRemove"), LinkButton)
            Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)

            ltlType.Text = KitManager.GetKitElementName(bindKitContent.ItemId, bindKitContent.ItemType)

            ltlPackSize.Text = "N/A"
            ltlSupplier.Text = "N/A"
            ltlCost.Text = "N/A"
            ltlExpiryDate.Text = "N/A"
            ltlSubclass.Text = "N/A"

            Select Case bindKitContent.ItemType

                Case AppSettingsGet.ElementTypeID
                    Dim tmpElement As Element = ElementManager.GetElement(bindKitContent.ItemId)
                    Dim tmpArtworkAdditionalCosts As ElementArtworkDetails = ElementManager.GetElementArtworkInfoByElement(tmpElement.ID)
                    Dim tmpAdditionalElementInfo As ElementAdditionalDetails = ElementManager.GetElementAdditionalInfoByElement(tmpElement.ID)
                    Dim tmpKittingInfo As ElementKittingDetails = ElementManager.GetElementKittingDetailsByElement(tmpElement.ID)

                    ltlAIN.Text = tmpElement.AINCodeDisplayString
                    ltlType.Text = tmpElement.SubclassType.Type.Name
                    ltlSubclass.Text = tmpElement.SubclassType.Name

                    If tmpArtworkAdditionalCosts IsNot Nothing Then
                        ltlPackSize.Text = tmpArtworkAdditionalCosts.PackSize
                    End If

                    If tmpKittingInfo IsNot Nothing Then
                        ltlSupplier.Text = tmpKittingInfo.Supplier
                        ltlCost.Text = tmpKittingInfo.CostPerItem
                        ltlExpiryDate.Text = If(tmpKittingInfo.ExpiryDate.HasValue, tmpKittingInfo.ExpiryDate.Value.ToString("dd/MM/yyyy"), "N/A")
                    End If

                Case AppSettingsGet.PremiumElementTypeID
                    Dim tmpPremElement As AdditionalElement = ElementManager.GetAdditionalElement(bindKitContent.ItemId)
                    Dim premInfo As PremiumElementDetails = ElementManager.GetPremiumElementDetailsByAdditionalElement(tmpPremElement.ID)
                    ltlAIN.Text = tmpPremElement.Name
                    ltlType.Text = "Premium"

                    If premInfo IsNot Nothing Then
                        ltlSupplier.Text = premInfo.Supplier
                        ltlCost.Text = premInfo.QuoteCost
                        ltlExpiryDate.Text = If(premInfo.ExpiryDate.HasValue, premInfo.ExpiryDate.Value.ToString("dd/MM/yyyy"), "N/A")
                    End If

                Case AppSettingsGet.BBCItemTypeID
                    Dim tmpProjectBBCItem As ProjectBBCItem = BBCItemManager.GetProjectBBCItem(bindKitContent.ItemId)
                    ltlAIN.Text = tmpProjectBBCItem.BBCItem.PartNumber
                    ltlType.Text = "BBC"
                    ltlPackSize.Text = tmpProjectBBCItem.PackQuantity

            End Select

            If IsReadOnly = True Then
                Dim tdRemove As HtmlTableCell = CType(e.Item.FindControl("tdRemove"), HtmlTableCell)
                tdRemove.Visible = False
                lnkRemove.Enabled = False
            Else
                lnkRemove.CommandArgument = bindKitContent.ID
                modUtilities.AddConfirmBoxToLinkButton(lnkRemove, "Are you sure you want to remove this item from kitting")
            End If

            'lblType.Text = KitManager.GetKitElementTypeString(bindKitContent.ItemType)

        End If

    End Sub

    Protected Sub lnkSetActivity_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim kitElementId As Integer = e.CommandArgument
        KitManager.RemoveKitElement(KitManager.GetKitElement(kitElementId))
        BindKitContents()

    End Sub

#End Region

#Region "Private Implementation"

    Public Sub BindKitContents()

        If QuoteId <> 0 Then

            Dim kitContents As List(Of KitElement) = KitManager.GetKitElementsByQuote(QuoteId)

            ShowKitContents = False

            If kitContents.Count > 0 Then
                ShowKitContents = True
                rptrKitContent.DataSource = kitContents
                rptrKitContent.DataBind()
            End If

        End If

    End Sub

    Public Sub SetAsPrintableVersion()

        plcKitDivs.Visible = False
        plcEndKitDivs.Visible = False
        IsReadOnly = True

    End Sub

#End Region

End Class
