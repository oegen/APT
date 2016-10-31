Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Admin_Tags_Tag
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event TagSaveSuccess As EventHandler
    Public Event TagSaveFailure As EventHandler
    Private DuplicateTagNameErrorText As String = "<br/><br/><br/><br/>This tag name already exists"

    Public Property TagId As Integer
        Get
            Return ViewState(Me.UniqueID & "_tagId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_tagId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            LoadTagDetails()
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        If Page.IsValid Then
            SaveTag()
        End If
    End Sub

#End Region

#Region "Public Methods"

#End Region

#Region "Private Implementation"

    Private Sub LoadTagDetails()

        Dim loadTag As Tag = TagManager.GetTag(TagId)

        If loadTag IsNot Nothing Then
            txtName.Text = loadTag.Name
        End If

    End Sub

    Private Sub SaveTag()

        Dim saveTag As New Tag

        If TagId <> 0 Then
            saveTag = TagManager.GetTag(TagId)
        End If

        saveTag.Name = txtName.Text

        Try
            TagManager.SaveTag(saveTag)
            TagId = saveTag.ID
            RaiseEvent TagSaveSuccess(Me, New EventArgs)
        Catch ex As NameAlreadyExistsException
            lblError.Text = DuplicateTagNameErrorText
        End Try

    End Sub

#End Region

End Class
