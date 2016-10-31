'----------------------------------------------------------------------------------------------
' Filename    : ContentTitleWithToolTip.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Controls_Layout_ContentTitle
    Inherits System.Web.UI.UserControl

    Public Property Title As String
        Get
            Return ltlTitle.Text
        End Get
        Set(ByVal value As String)
            ltlTitle.Text = value
        End Set
    End Property

    Public Property ToolTipText As String
        Get
            Return hypToolTip.ToolTip
        End Get
        Set(ByVal value As String)
            hypToolTip.ToolTip = value

            If value.Trim <> "" Then
                plcToolTip.Visible = True
            Else
                plcToolTip.Visible = False
            End If

        End Set
    End Property

End Class
