Imports System.Collections.Generic
Imports aptBusinessLogic
Imports aptEntities

Partial Class Kitting_KitElements
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            If IsNumeric(Request.QueryString("QuoteId")) Then
                ctrlKitContent.QuoteId = Request.QueryString("QuoteId")
                ctrlBBCItemsListing.QuoteId = Request.QueryString("QuoteId")
                ctrlElementItemListing.QuoteId = Request.QueryString("QuoteId")
                ctrlPremiumElementsListing.QuoteId = Request.QueryString("QuoteId")

                Dim tmpKit As Kit = KitManager.GetKit(Request.QueryString("kitId"))

                If tmpKit IsNot Nothing Then
                    ctrlMagicalToolTip.TipTitle = String.Format("{0} - Contents", tmpKit.Name)
                End If

            End If

            If IsNumeric(Request.QueryString("projectId")) Then
                ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
                ctrlBBCItemsListing.ProjectId = Request.QueryString("projectId")
                ctrlElementItemListing.ProjectId = Request.QueryString("projectId")
                ctrlPremiumElementsListing.ProjectId = Request.QueryString("projectId")
                ctrlSubNavProject.ProjectId = Request.QueryString("projectId")
            End If

            Dim readOnlyMode As Boolean = KitManager.HasKitBeenFinalised(Request.QueryString("projectId"))

            ctrlBBCItemsListing.SetCurrentMode(Controls_Kitting_KitItemListing.Mode.BBC_ITEM, readOnlyMode)
            ctrlElementItemListing.SetCurrentMode(Controls_Kitting_KitItemListing.Mode.ELEMENTS, readOnlyMode)
            ctrlPremiumElementsListing.SetCurrentMode(Controls_Kitting_KitItemListing.Mode.PREMIUM_ELEMENTS, readOnlyMode)

        End If

    End Sub

    Protected Sub lnkSaveKitContents_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSaveKitContents.Click

        If Page.IsValid Then

            Dim changedElements As List(Of KitItem) = ctrlElementItemListing.GetChangedItems()
            Dim changedBBCItems As List(Of KitItem) = ctrlBBCItemsListing.GetChangedItems()
            Dim changedPremiums As List(Of KitItem) = ctrlPremiumElementsListing.GetChangedItems()

            KitManager.SaveKitContents(changedElements, Request.QueryString("QuoteId"), KitItemType.ELEMENT)
            KitManager.SaveKitContents(changedPremiums, Request.QueryString("QuoteId"), KitItemType.PREMIUM_ELEMENT)
            KitManager.SaveKitContents(changedBBCItems, Request.QueryString("QuoteId"), KitItemType.BBC_ITEM)

            ctrlKitContent.BindKitContents()
            CType(Me.Master, MasterPage).DisplayConfirmationMessage("Kit Contents have been saved successfully")

        End If

    End Sub

    'Private Function ctrlKitContent() As Object
    '    Throw New NotImplementedException
    'End Function

End Class
