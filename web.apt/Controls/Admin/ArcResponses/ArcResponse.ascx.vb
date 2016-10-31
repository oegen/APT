Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Admin_ArcResponses_ArcResponse
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event ArcResponseSaveSuccess As EventHandler
    Public Event ArcResponseSaveFailed As EventHandler

    Public Property ArcResponseId As Integer
        Get
            Return ViewState(Me.UniqueID & "_arcResponseId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_arcResponseId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            LoadArcResponseDetails()
        End If

    End Sub

#End Region


#Region "Private Implementation"

    Private Sub LoadArcResponseDetails()


        Dim arcResponses As ArcResponse = WorkflowManager.GetArcResponse(ArcResponseId)

        If arcResponses IsNot Nothing Then
            txtResponseText.Text = arcResponses.ResponseText
        End If

    End Sub

    Private Sub SaveArcResponseDetails()

        Dim saveArcResponse As New ArcResponse

        If ArcResponseId <> 0 Then
            saveArcResponse = WorkflowManager.GetArcResponse(ArcResponseId)
        End If

        saveArcResponse.ResponseText = txtResponseText.Text
        WorkflowManager.SaveArcResponse(saveArcResponse)
        ArcResponseId = saveArcResponse.ID
        RaiseEvent ArcResponseSaveSuccess(Me, New EventArgs)

    End Sub

#End Region

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            SaveArcResponseDetails()
        End If

    End Sub

End Class
