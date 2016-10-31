'----------------------------------------------------------------------------------------------
' Filename    : ProjectBBCItem.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        08/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Element_ProjectBBCItem
    Inherits System.Web.UI.UserControl

    #Region "Constants and Enumerations"

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Public Event SaveSuccess As EventHandler
    Public Event SaveFailure As EventHandler

    Public Property ProjectBBCItemId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectBBCItemId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectBBCItemId") = value
            LoadProjectBBCItemDetails()
        End Set
    End Property

    Public Property IsReadOnly As Boolean
        Get
            Return ViewState(Me.UniqueID & "_isReadOnly")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.UniqueID & "_isReadOnly") = value
            SetReadOnlyMode()
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

#End Region

#Region "Events"

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        If Page.IsValid Then
            If IsBBCItemSelected() = True Then
                SaveProjectBBCItem()
            End If
        End If
    End Sub

#End Region

#Region "Public Methods"

#End Region

#Region "Private Implementation"

    Private Sub LoadProjectBBCItemDetails()

        Dim loadBBCItem As ProjectBBCItem = BBCItemManager.GetProjectBBCItem(ProjectBBCItemId)

        If loadBBCItem IsNot Nothing Then
            ctrlBBCItemSelectAndDisplay.BBCItemId = loadBBCItem.BBCItem.ID
            txtDeliveryDate.SelectedDate = loadBBCItem.DeliveryDate
            txtPackQuantity.Text = loadBBCItem.PackQuantity
            txtQuantity.Text = loadBBCItem.Quantity
        Else
            ' Invalid ID - could be someone playing about with the querystring
            ProjectBBCItemId = 0
        End If

    End Sub

    Private Sub SaveProjectBBCItem()

        Dim saveBBCItem As New ProjectBBCItem

        If ProjectBBCItemId <> 0 Then
            saveBBCItem = BBCItemManager.GetProjectBBCItem(ProjectBBCItemId)
        End If

        saveBBCItem.DeliveryDate = txtDeliveryDate.SelectedDate
        saveBBCItem.PackQuantity = txtPackQuantity.Text
        saveBBCItem.Quantity = txtQuantity.Text
        saveBBCItem.Project = ProjectManager.GetProject(ProjectId)
        saveBBCItem.BBCItem = BBCItemManager.GetNewBBCItem(ctrlBBCItemSelectAndDisplay.BBCItemId)

        BBCItemManager.SaveProjectBBCItem(saveBBCItem, SessionManager.LoggedInUserId)
        ProjectBBCItemId = saveBBCItem.ID
        RaiseEvent SaveSuccess(Me, New EventArgs)

    End Sub

    Private Sub SetReadOnlyMode()

        txtQuantity.Enable = Not IsReadOnly
        txtPackQuantity.Enable = Not IsReadOnly
        txtDeliveryDate.Enable = Not IsReadOnly
        plcSave.Visible = Not IsReadOnly
    End Sub

    Private Function IsBBCItemSelected() As Boolean
        If ctrlBBCItemSelectAndDisplay.BBCItemId = 0 Then
            lblError.Visible = True
            Return False
        End If

        Return True
    End Function

#End Region

End Class
