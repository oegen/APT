Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Admin_Lists_AptList
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event AptListSaveSuccess As EventHandler
    Public Event AptListSaveFail As EventHandler
    Private DuplicateListNameErrorText As String = "<br/><br/><br/><br/>This list name already exists"

    Public Property AptListId As Integer
        Get
            Return ViewState(Me.UniqueID & "_aptListId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_aptListId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            LoadAptListDetails()
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            SaveAptListDetails()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SaveAptListDetails()

        Dim saveAptList As New AptList

        Try

            If AptListId <> 0 Then
                saveAptList = ListManager.GetList(AptListId)
            End If

            saveAptList.Name = txtName.Text

            ListManager.SaveList(saveAptList)
            AptListId = saveAptList.ID
            RaiseEvent AptListSaveSuccess(Me, New EventArgs)

        Catch ex As NameAlreadyExistsException
            lblError.Text = DuplicateListNameErrorText
        End Try

    End Sub

    Private Sub LoadAptListDetails()

        If AptListId <> 0 Then
            Dim loadAptList = ListManager.GetList(AptListId)

            If loadAptList IsNot Nothing Then
                txtName.Text = loadAptList.Name
            End If

        End If

    End Sub

#End Region

End Class
