'----------------------------------------------------------------------------------------------
' Filename    : SimpleDropDownList.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports GenericUtilities
Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Generic_SimpleDropDownList
    Inherits System.Web.UI.UserControl

    Public Event SelectedIndexChanged As EventHandler

    Public ReadOnly Property SelectedIndex As Integer
        Get
            Return ddlValue.SelectedIndex
        End Get
    End Property

    Public Property LabelText As String
        Get
            Return lblAttribute.Text
        End Get
        Set(ByVal value As String)
            lblAttribute.Text = value
        End Set
    End Property

    Public Property SelectedValue As Integer
        Get
            Return ddlValue.SelectedValue
        End Get
        Set(ByVal value As Integer)
            ddlValue.SelectedValue = value
        End Set
    End Property

    Public Property AutoPostBack As Boolean
        Get
            Return ddlValue.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlValue.AutoPostBack = value
        End Set
    End Property

    Public Property ValidationGroup As String
        Get
            Return reqValue.ValidationGroup
        End Get
        Set(ByVal value As String)

            If String.IsNullOrEmpty(value) Then
                reqValue.Enabled = False
                reqValue.ValidationGroup = value
                ddlValue.ValidationGroup = value
            Else
                lblAttribute.Text = lblAttribute.Text & "<em>*</em>"
                reqValue.Enabled = True
                reqValue.ValidationGroup = value
                ddlValue.ValidationGroup = value
            End If

        End Set
    End Property

    Public Property Enabled As Boolean
        Get
            Return ddlValue.Enabled
        End Get
        Set(ByVal value As Boolean)
            ddlValue.Enabled = value
        End Set
    End Property

    Public ReadOnly Property DropDownList As DropDownList
        Get
            Return ddlValue
        End Get
    End Property

    Public Sub BindDataToDropDown(ByVal listId As Integer, ByVal itemString As String, Optional ByVal selectItemText As String = "")

        ddlValue.Items.Clear()
        Dim listItems As List(Of ListNode) = ListManager.GetListsNodes(listId)
        modComponent.BindDropDown(ddlValue, listItems, "ID", "Name", itemString, selectItemText)

    End Sub

    Public Sub BindDataToDropDown(ByVal datasource As IList, _
                              ByVal idField As String, ByVal textField As String, _
                              ByVal itemString As String, Optional ByVal selectItemText As String = "- Select a {0} -")

        ddlValue.Items.Clear()
        modComponent.BindDropDown(ddlValue, datasource, idField, textField, itemString, selectItemText)

    End Sub

    Protected Sub ddlValue_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlValue.SelectedIndexChanged
        RaiseEvent SelectedIndexChanged(sender, e)
    End Sub

End Class
