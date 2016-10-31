'----------------------------------------------------------------------------------------------
' Filename    : ContentTitle.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------


Partial Class Controls_Layout_ContentHeader
    Inherits System.Web.UI.UserControl

    Public Property Title As String
        Get
            Return lblTitle.Text
        End Get
        Set(ByVal value As String)
            lblTitle.Text = value
        End Set
    End Property

    Public Property SubTitle As String
        Get
            Return lblSubTitle.Text
        End Get
        Set(ByVal value As String)
            lblSubTitle.Text = value
        End Set
    End Property

    Public Property Description As String
        Get
            Return lblDescription.Text
        End Get
        Set(ByVal value As String)
            lblDescription.Text = value
        End Set
    End Property

    Public Property ShowOpenCloseMessage As Boolean
        Get
            Return pnlMessageDisplay.Visible
        End Get
        Set(ByVal value As Boolean)
            pnlMessageDisplay.Visible = True
        End Set
    End Property

End Class
