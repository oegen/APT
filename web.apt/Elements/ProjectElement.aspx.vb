'----------------------------------------------------------------------------------------------
' Filename    : ProjectElement.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Elements_ProjectElement
    Inherits System.Web.UI.Page

#Region "Properties"

    Dim _element As Element

    Private Property CurrentElement As Element
        Get
            Return _element
        End Get
        Set(value As Element)
            _element = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            CType(Me.Master, MasterPages_TabPageWithoutAjax).SelectedTab = ProjectTabIndex.ELEMENTS
            CType(Me.Master, MasterPages_TabPageWithoutAjax).SetSubNavSelectedItem(ConstantLibrary.ProjectSubNavItems.EDIT_PROJECT)

            If IsNumeric(Request.QueryString("projectid")) Then
                PermissionCheck()
                InitialiseControls()
            End If
        End If

    End Sub

    Protected Sub ctrlElement_ElementStarted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlElement.ElementStarted
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("This Element has been started")

        ctrlElementKittingDetails.CurrentMode = FormMode.ENABLE_SAVE
        ctrlElementArtworkDetails.CurrentMode = FormMode.ENABLE_SAVE
    End Sub

    Protected Sub ctrlElement_ElementStopped(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlElement.ElementStopped
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("This Element has been stopped")

        ctrlElementKittingDetails.CurrentMode = FormMode.DISABLE_SAVE
        ctrlElementArtworkDetails.CurrentMode = FormMode.DISABLE_SAVE
    End Sub

    Protected Sub ctrlElement_SaveSuccess(ByVal sender As Object, ByVal e As CommandEventArgs) Handles ctrlElement.SaveSuccess

        Dim savedElement As Element = e.CommandArgument
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("Element has been saved successfully")
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("Please ensure all your data is accurate before clicking \'Save\' as this could impede progress & timescales", , 4)
        ctrlElementTitle.Title = savedElement.Name ' savedElement.DisplayString
        ctrlElementListing.SelectedElementId = savedElement.ID
        ctrlElementListing.BindProjectElements()
        EnableKittingArtworkSections()

    End Sub

    Protected Sub ctrlElementArtworkDetails_ElementArtworkInfoSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlElementArtworkDetails.ElementArtworkInfoSaveSuccess
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("Artwork information saved successfully", MessageBoxType.Success)
    End Sub

    Protected Sub ctrlElementKittingDetails_KittingDetailSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlElementKittingDetails.KittingDetailSuccess
        CType(Me.Master, MasterPages_TabPageWithoutAjax).DisplayConfirmationMessage("Kitting Details have been saved")
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub InitialiseControls()

        ctrlElement.ProjectId = Request.QueryString("projectid")
        ctrlElementListing.ProjectId = Request.QueryString("projectid")

        If IsNumeric(Request.QueryString("elementid")) Then

            Dim loadElement As Element = ElementManager.GetElement(Request.QueryString("elementid"))

            If loadElement IsNot Nothing Then

                If loadElement.Project.ID <> Request.QueryString("projectid") Then
                    ' user has changed the elementId to one that does not belong to this project stop doing anything
                    Exit Sub
                End If

                ctrlElementTitle.Title = loadElement.Name 'loadElement.DisplayString
                ctrlElement.ElementId = Request.QueryString("elementid")
                ctrlElementArtworkDetails.ElementId = Request.QueryString("elementid")
                ctrlElementKittingDetails.ElementId = Request.QueryString("elementid")
                ctrlElementListing.SelectedElementId = Request.QueryString("elementid")

                EnableKittingArtworkSections()

            End If

        End If

    End Sub

    Private Sub EnableKittingArtworkSections()
        plcBD.Visible = True
        plcMDA.Visible = True
    End Sub

    Private Sub PermissionCheck()

        If PermissionsManager.CanUserSaveElement(SessionManager.LoggedInUserId, Request.QueryString("projectid")) = False Then
            ctrlElement.CurrentMode = FormMode.DISABLE_SAVE
            ctrlElementKittingDetails.CurrentMode = FormMode.DISABLE_SAVE
            ctrlElementArtworkDetails.CurrentMode = FormMode.DISABLE_SAVE
            ctrlElementListing.CurrentMode = ASP.controls_generic_elementlisting_ascx.Mode.DISABLE_ELEMENT_ADDITION
        End If

    End Sub

#End Region

End Class
