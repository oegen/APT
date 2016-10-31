'----------------------------------------------------------------------------------------------
' Filename    : NumericTextBox.ascx.vb
' Description : 
' The max length has been set to the default of 10 as it is assumed anything put into this will be an integer (32bit)
' the max value is 2147483647 
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------



Partial Class Controls_Generic_NumericTextBox
    Inherits System.Web.UI.UserControl

    Public Property LabelText() As String
        Get
            Return lblAttribute.Text
        End Get
        Set(ByVal value As String)
            lblAttribute.Text = value

            If Required = True Then
                lblAttribute.Text = lblAttribute.Text & "<em>*</em>"
            End If

        End Set
    End Property

    Public ReadOnly Property TextBoxClientID As String
        Get
            Return txtValue.ClientID
        End Get
    End Property

    ' This could be any number type
    Public Property Text() As Object
        Get
            If IsNumeric(txtValue.Text) Then
                Return txtValue.Text
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Object)

            If value Is Nothing Then
                value = ""
            End If

            txtValue.Text = value

            If txtValue.MaxLength > 0 Then
                If Convert.ToString(value).Length > txtValue.MaxLength Then
                    txtValue.Text = value.ToString.Substring(0, txtValue.MaxLength)
                End If
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

    Public Property TextMode As TextBoxMode
        Get
            Return txtValue.TextMode
        End Get
        Set(ByVal value As TextBoxMode)
            txtValue.TextMode = value
        End Set
    End Property

    Public Property ValidationGroup As String
        Get
            Return reqValue.ValidationGroup
        End Get
        Set(ByVal value As String)
            reqValue.ValidationGroup = value
            txtValue.ValidationGroup = value
            regNumber.ValidationGroup = value
        End Set
    End Property

    Public Property SkinID As String
        Get
            Return txtValue.SkinID
        End Get
        Set(ByVal value As String)
            txtValue.SkinID = value
        End Set
    End Property

    Public Property Enable() As Boolean
        Get
            Return txtValue.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtValue.Enabled = value
        End Set
    End Property

    Public Property Required As Boolean
        Get
            Return reqValue.Enabled
        End Get
        Set(ByVal value As Boolean)
            AddRequiredText()
            reqValue.Enabled = value
        End Set
    End Property

    Public Property MaxLength As Integer
        Get
            Return txtValue.MaxLength
        End Get
        Set(ByVal value As Integer)
            txtValue.MaxLength = value
        End Set
    End Property

    Public Property EnableLabelViewState As Boolean
        Get
            Return lblAttribute.EnableViewState
        End Get
        Set(ByVal value As Boolean)
            lblAttribute.EnableViewState = value
        End Set
    End Property

    Private Sub AddRequiredText()

        If lblAttribute.Text.Contains("<em>*</em>") = False Then
            lblAttribute.Text = lblAttribute.Text & "<em>*</em>"
        End If

    End Sub


End Class
