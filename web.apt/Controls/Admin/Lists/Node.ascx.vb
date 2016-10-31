Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Admin_Lists_Node
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Private NodeListErrorText As String = "<br/><br/><br/><br/>An item with this name already exists in this list"
    Public Event NodeSaveSuccess As EventHandler
    Public Event NodeSaveFailure As EventHandler

    Public Property NodeId As Integer
        Get
            Return ViewState(Me.UniqueID & "_nodeId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_nodeId") = value
        End Set
    End Property

    Public Property ListId As Integer
        Get
            Return ViewState(Me.UniqueID & "_listId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_listId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            LoadNodeDetails()
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            SaveNode()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SaveNode()

        If ListId <> 0 Then

            Dim saveNode As New ListNode
            Dim nodeList As AptList = ListManager.GetList(ListId)

            Try
                If nodeList IsNot Nothing Then
                    If NodeId <> 0 Then
                        saveNode = ListManager.GetListNode(NodeId)
                    Else
                        saveNode.List = ListManager.GetList(ListId)
                    End If

                    saveNode.Name = txtName.Text
                    ListManager.SaveNode(saveNode)
                    NodeId = saveNode.ID
                    RaiseEvent NodeSaveSuccess(Me, New EventArgs)
                Else
                    RaiseEvent NodeSaveFailure(Me, New EventArgs)
                End If
            Catch ex As NameAlreadyExistsException
                lblError.Text = NodeListErrorText
            End Try

        End If

    End Sub

    Private Sub LoadNodeDetails()

        If NodeId <> 0 Then

            Dim loadNode As ListNode = ListManager.GetListNode(NodeId)

            If loadNode IsNot Nothing Then
                txtName.Text = loadNode.Name
            End If

        End If

    End Sub

#End Region

End Class
