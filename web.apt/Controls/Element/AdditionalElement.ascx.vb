'----------------------------------------------------------------------------------------------
' Filename    : AdditionalElement.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Project_AdditionalElement
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event SaveAdditionalElementSuccess As EventHandler
    Public Event SaveAdditionalElementFail As EventHandler

    Public Property AdditionalElementId As Integer
        Get
            Return ViewState(Me.UniqueID & "_additionalElementId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_additionalElementId") = value
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

    Public Property IsReadOnly As Boolean
        Get
            Return ViewState(Me.UniqueID & "_isReadOnly")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.UniqueID & "_isReadOnly") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            ReadOnlyCheck()
            LoadAdditionalElement()
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then

            Dim saveAdditionalElement As New AdditionalElement
            Dim elementProject As Project = ProjectManager.GetProject(ProjectId)

            If AdditionalElementId <> 0 Then
                saveAdditionalElement = ElementManager.GetAdditionalElement(AdditionalElementId)
            End If

            saveAdditionalElement.Name = txtName.Text
            saveAdditionalElement.Description = txtDescription.Text
            saveAdditionalElement.Cost = txtCost.Text
            saveAdditionalElement.Project = elementProject

            ElementManager.SaveAdditionalElement(saveAdditionalElement, SessionManager.LoggedInUserId)
            AdditionalElementId = saveAdditionalElement.ID

            RaiseEvent SaveAdditionalElementSuccess(Me, New EventArgs())

        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadAdditionalElement()

        If AdditionalElementId <> 0 Then

            Dim loadAdditionalElement As AdditionalElement = ElementManager.GetAdditionalElement(AdditionalElementId)

            txtName.Text = loadAdditionalElement.Name
            txtDescription.Text = loadAdditionalElement.Description
            txtCost.Text = loadAdditionalElement.Cost

        End If

    End Sub

    Private Sub ReadOnlyCheck()

        If IsReadOnly Then
            txtName.Enable = False
            txtDescription.Enable = False
            txtCost.Enable = False
            submit_btn.Visible = False
        End If

    End Sub

#End Region

End Class
