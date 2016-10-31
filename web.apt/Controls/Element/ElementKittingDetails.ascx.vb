'----------------------------------------------------------------------------------------------
' Filename    : ElementKittingDetails.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Project_ElementKittingDetails
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event KittingDetailSuccess As EventHandler
    Public Event KittingDetailFail As EventHandler

    Public Property ElementId As Integer
        Get
            Return ViewState(Me.UniqueID & "_elementId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_elementId") = value
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

    Dim _elementKittingDets As ElementKittingDetails = Nothing

    Public ReadOnly Property ElementKittingDets As ElementKittingDetails
        Get
            If _elementKittingDets Is Nothing Then
                _elementKittingDets = ElementManager.GetElementKittingDetailsByElement(ElementId)
            End If

            Return _elementKittingDets
        End Get
    End Property

#End Region

#Region "Events"

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            SaveElementKittingInfo()
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False AndAlso ElementId <> 0 Then

            If ElementManager.HasElementWorkflowFinished(ElementId) Then
                CurrentMode = FormMode.DISABLE_SAVE
            End If

            If ElementKittingDets IsNot Nothing Then
                If PermissionsManager.CanUserSaveElement(SessionManager.LoggedInUserId, _
                                                         ElementKittingDets.Element.Project.ID) = False Then
                    CurrentMode = FormMode.DISABLE_SAVE
                End If
            End If

            LoadElementKittingInfo()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadElementKittingInfo()

        If ElementKittingDets IsNot Nothing Then
            txtCostPerItem.Text = ElementKittingDets.CostPerItem
            txtDateIntoMDA.SelectedDate = ElementKittingDets.DueDateIntoMDA
            radListExistNew.SelectedValue = Convert.ToInt32(ElementKittingDets.Existing)
            txtExpiryDate.SelectedDate = ElementKittingDets.ExpiryDate
            txtPONumber.Text = ElementKittingDets.PONumber
            txtSupplier.Text = ElementKittingDets.Supplier
        End If

    End Sub

    Private Sub SaveElementKittingInfo()

        Dim saveKitInfo As New ElementKittingDetails

        If ElementId <> 0 Then
            If ElementManager.GetElementKittingDetailsByElement(ElementId) IsNot Nothing Then
                saveKitInfo = ElementManager.GetElementKittingDetailsByElement(ElementId)
            End If
        End If

        With saveKitInfo

            .CostPerItem = txtCostPerItem.Text
            .DueDateIntoMDA = txtDateIntoMDA.SelectedDate
            .Element = ElementManager.GetElement(ElementId)
            .Existing = radListExistNew.SelectedValue
            .ExpiryDate = txtExpiryDate.SelectedDate
            .PONumber = txtPONumber.Text
            .Supplier = txtSupplier.Text

        End With

        ElementManager.SaveElementKittingInfo(saveKitInfo, SessionManager.LoggedInUserId)
        RaiseEvent KittingDetailSuccess(Me, New EventArgs)

    End Sub

    Private Sub PermissionCheck()

        If CurrentMode = FormMode.DISABLE_SAVE Then
            SetFormActivity(False)
        Else
            SetFormActivity(True)
        End If

    End Sub

    Private Sub SetFormActivity(ByVal active As Boolean)

        txtCostPerItem.Enable = active
        txtExpiryDate.Enable = active
        txtPONumber.Enable = active
        radListExistNew.Enabled = active
        txtSupplier.Enable = active
        txtDateIntoMDA.Enable = active
        submit_btn.Visible = active

    End Sub

#End Region

End Class
