Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Admin_Elements_AptType
    Inherits System.Web.UI.UserControl

    #Region "Constants and Enumerations"

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Public Event AptTypeSaveSuccess As EventHandler
    Public Event AptTypeSaveFailure As EventHandler

    Public Property AptTypeId As Integer
        Get
            Return ViewState(Me.UniqueID & "_aptTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_aptTypeId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            LoadAptTypeDetails()
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            SaveAptType()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadAptTypeDetails()

        If AptTypeId <> 0 Then

            Dim loadAptType As ElementType = AptTypeManager.GetAptType(AptTypeId)

            If loadAptType IsNot Nothing Then
                txtTypeName.Text = loadAptType.Name
            End If

        End If

    End Sub

    Private Sub SaveAptType()

        Dim saveAptType As New ElementType

        If AptTypeId <> 0 Then
            saveAptType = AptTypeManager.GetAptType(AptTypeId)
        End If

        saveAptType.Name = txtTypeName.Text

        AptTypeManager.SaveType(saveAptType)
        AptTypeId = saveAptType.ID
        RaiseEvent AptTypeSaveSuccess(Me, New EventArgs)

    End Sub

#End Region

End Class
