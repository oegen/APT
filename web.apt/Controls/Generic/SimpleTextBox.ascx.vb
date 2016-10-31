'----------------------------------------------------------------------------------------------
' Filename    : SimpleTextBox.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI

Partial Class Controls_Generic_SimpleTextBox
    Inherits System.Web.UI.UserControl

    Public property LabelText() As String
        Get
            Return lblAttribute.Text
        End Get
        Set(ByVal value As String)
            lblAttribute.Text = value

            If String.IsNullOrEmpty(ValidationGroup) = False Then
                ' the validation group has been set so add 
                lblAttribute.Text = lblAttribute.Text & "<em>*</em>"
            End If

        End Set
    End Property

    Public Property Text() As String
        Get
            Return txtValue.Text
        End Get
        Set(ByVal value As String)

            If value Is Nothing Then
                value = ""
            End If

            txtValue.Text = value

            If txtValue.MaxLength > 0 Then
                If value.Length > txtValue.MaxLength Then
                    txtValue.Text = value.Substring(0, txtValue.MaxLength)
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

            If String.IsNullOrEmpty(value) Then
                RemoveRequiredAsteriskFromLabel()
                reqValue.Enabled = False
                reqValue.ValidationGroup = value
                txtValue.ValidationGroup = value
            Else
                reqValue.Enabled = True
                reqValue.ValidationGroup = value
                txtValue.ValidationGroup = value
                lblAttribute.Text = lblAttribute.Text & "<em>*</em>"
            End If

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

    Public Property MaxLength As Integer
        Get
            Return txtValue.MaxLength
        End Get
        Set(ByVal value As Integer)
            txtValue.MaxLength = value
        End Set
    End Property

    Private Sub RemoveRequiredAsteriskFromLabel()

        lblAttribute.Text = lblAttribute.Text.Replace("<em>*</em>", "")

    End Sub

End Class
