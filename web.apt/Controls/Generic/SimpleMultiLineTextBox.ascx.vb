'----------------------------------------------------------------------------------------------
' Filename    : SimpleMultiLineTextBox.ascx.vb
' Description :
' Max Length has been set to the default of 4000 (same as the max length of a nvarchar and varchar in SQL)

' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Controls_Generic_SimpleMultiLineTextBox
    Inherits System.Web.UI.UserControl

    Private _maxLengthValidExpression As String = "^[\S\s]{{0,{0}}}$"

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

    Public Property ValidationGroup As String
        Get
            Return reqValue.ValidationGroup
        End Get
        Set(ByVal value As String)
            reqValue.ValidationGroup = value
            txtValue.ValidationGroup = value
            regLength.ValidationGroup = value
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
            regLength.Enabled = True
            regLength.ValidationExpression = String.Format(_maxLengthValidExpression, value)
        End Set
    End Property

    Public Property Required As Boolean
        Get
            Return reqValue.Enabled
        End Get
        Set(ByVal value As Boolean)
            lblAttribute.Text = lblAttribute.Text & "<em>*</em>"
            reqValue.Enabled = value
        End Set
    End Property

    Public Property Width As Unit
        Get
            Return txtValue.Width
        End Get
        Set(ByVal value As Unit)
            txtValue.Width = value
        End Set
    End Property

    Public Property Height As Unit
        Get
            Return txtValue.Height
        End Get
        Set(ByVal value As Unit)
            txtValue.Height = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        plcBindingFix.DataBind()
    End Sub
End Class
