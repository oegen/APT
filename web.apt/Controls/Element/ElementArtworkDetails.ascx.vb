'----------------------------------------------------------------------------------------------
' Filename    : ElementArtworkDetails.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 2       TL        23/02/2011  Added new field "pack size" and removed cost per item and print costs fields
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Project_ElementArtworkDetails
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event ElementArtworkInfoSaveSuccess As EventHandler
    Public Event ElementArtworkInfoSaveFailed As EventHandler

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

    Dim _elementArtworkDets As ElementArtworkDetails = Nothing

    Public ReadOnly Property ElementArtworkDets As ElementArtworkDetails
        Get
            If _elementArtworkDets Is Nothing Then
                _elementArtworkDets = ElementManager.GetElementArtworkInfoByElement(ElementId)
            End If

            Return _elementArtworkDets
        End Get
    End Property

#End Region

#Region "Events"


    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then

            Dim saveElementArtworkInfo As New ElementArtworkDetails

            If ElementId <> 0 Then
                If ElementManager.GetElementArtworkInfoByElement(ElementId) IsNot Nothing Then
                    saveElementArtworkInfo = ElementManager.GetElementArtworkInfoByElement(ElementId)
                End If
            End If

            With saveElementArtworkInfo

                .NoOfColours = txtNoOfColour.Text
                .FinishedSize = txtFinishedSize.Text
                .Material = txtMaterial.Text
                .Finishing = txtFinishing.Text
                .NoOfDelAdds = txtNoOfDelAdds.Text
                .DeliveryDetails = txtDeliveryDetails.Text
                .Element = ElementManager.GetElement(ElementId)
                .PackSize = txtPackSize.Text

            End With

            ElementManager.SaveElementArtworkInfo(saveElementArtworkInfo, SessionManager.LoggedInUserId)
            RaiseEvent ElementArtworkInfoSaveSuccess(Me, New EventArgs())

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            If ElementManager.HasElementWorkflowFinished(ElementId) Then
                CurrentMode = FormMode.DISABLE_SAVE
            End If

            If ElementArtworkDets IsNot Nothing Then
                If PermissionsManager.CanUserSaveElement(SessionManager.LoggedInUserId, _
                                                         ElementArtworkDets.Element.Project.ID) = False Then
                    CurrentMode = FormMode.DISABLE_SAVE
                End If
            End If

            LoadElementArtworkInfo()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadElementArtworkInfo()

        If ElementArtworkDets IsNot Nothing Then

            With ElementArtworkDets

                txtNoOfColour.Text = .NoOfColours
                txtFinishedSize.Text = .FinishedSize
                txtMaterial.Text = .Material
                txtFinishing.Text = .Finishing
                txtNoOfDelAdds.Text = .NoOfDelAdds
                txtDeliveryDetails.Text = .DeliveryDetails
                txtPackSize.Text = .PackSize

            End With

        End If

    End Sub

    Private Sub SetFormActivity(ByVal active As Boolean)

        txtNoOfColour.Enable = active
        txtFinishedSize.Enable = active
        txtMaterial.Enable = active
        txtFinishing.Enable = active
        txtNoOfDelAdds.Enable = active
        txtDeliveryDetails.Enable = active
        txtPackSize.Enable = active

        submit_btn.Visible = active

    End Sub

    Private Sub PermissionCheck()

        If CurrentMode = FormMode.DISABLE_SAVE Then
            SetFormActivity(False)
        Else
            SetFormActivity(True)
        End If

    End Sub

#End Region

End Class
