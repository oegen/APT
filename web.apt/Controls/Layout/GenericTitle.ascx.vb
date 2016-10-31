'----------------------------------------------------------------------------------------------
' Filename    : MagicalToolTip.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Controls_Generic_GenericTitle
    Inherits System.Web.UI.UserControl

    Private Const TITLE_ATTR As String = "title"

    Public Property TipTitle As String
        Get
            Return ltlTitle.Text
        End Get
        Set(ByVal value As String)
            ltlTitle.Text = value
        End Set
    End Property

    Public Property Description As String
        Get
            Return lnkHover.Attributes(TITLE_ATTR)
        End Get
        Set(ByVal value As String)
            lnkHover.Attributes(TITLE_ATTR) = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lnkHover.Attributes.Add(TITLE_ATTR, Description)
    End Sub
End Class
