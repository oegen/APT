'----------------------------------------------------------------------------------------------
' Filename    : DateTimePicker.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------


Partial Class Controls_Generic_DateTimePicker
    Inherits System.Web.UI.UserControl

    Public Property LabelText As String
        Get
            Return lblAttribute.Text
        End Get
        Set(ByVal value As String)
            lblAttribute.Text = value
        End Set
    End Property

    Public ReadOnly Property DateClientID As String
        Get
            Return txtDate.ClientID
        End Get
    End Property

    Public Property CssClass As String 
        Get
            Return txtDate.CssClass
        End Get
        Set(ByVal value As String)
            txtDate.CssClass = value
        End Set
    End Property


    Public Property SelectedDate As Nullable(Of Date)
        Get
            If String.IsNullOrEmpty(txtDate.Text) Then
                Dim sDate As Nullable(Of Date)
                Return sDate
            Else
                Dim sDate As Date
                Date.TryParse(txtDate.Text, sDate)
                Return sDate
            End If
        End Get
        Set(ByVal value As Nullable(Of Date))
            If value Is Nothing Then
                txtDate.Text = ""
            Else
                txtDate.Text = value.Value.ToString("dd/MM/yyyy")
            End If
        End Set
    End Property

    Public Property ErrorMessage() As String
        Get
            Return reqValue.ErrorMessage
        End Get
        Set(ByVal value As String)
            reqValue.Text = value
        End Set
    End Property

    Public Property ValidationGroup As String
        Get
            Return reqValue.ValidationGroup
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value) Then
                reqValue.Enabled = False
            Else
                reqValue.ValidationGroup = value
                txtDate.ValidationGroup = value
                reqValue.Enabled = True
                lblAttribute.Text = lblAttribute.Text & "<em>*</em>"
            End If
        End Set
    End Property

    Public Property SkinIDValue As String
        Get
            Return txtDate.SkinID
        End Get
        Set(ByVal value As String)
            txtDate.SkinID = value
        End Set
    End Property

    Public Property Enable() As Boolean
        Get
            Return txtDate.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtDate.Enabled = value
        End Set
    End Property

    Public Property StylingClass As String
        Get
            Return txtDate.CssClass
        End Get
        Set(ByVal value As String)
            txtDate.CssClass = String.Format("{0} {1}", "datepicker", value)
        End Set
    End Property

    Public Property AutoPostBack As Boolean
        Get
            Return txtDate.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            txtDate.AutoPostBack = value
        End Set
    End Property

    Public Event DateSelectionChanged As EventHandler

    Protected Sub txtDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate.TextChanged
        RaiseEvent DateSelectionChanged(sender, e)
    End Sub
End Class
