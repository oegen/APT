'----------------------------------------------------------------------------------------------
' Filename    : Paging.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/11/2011  First release.
'----------------------------------------------------------------------------------------------

Partial Class Controls_Generic_Paging
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event PageChanged As CommandEventHandler

    Public Property CurrentPage As Integer
        Get
            Return ViewState(Me.UniqueID & "_CurrentPage")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_CurrentPage") = value
            ltlCurrentPage.Text = value
            SetButtonVisibility()
        End Set
    End Property

    Private Property LastPage As Integer
        Get
            Return ViewState(Me.UniqueID & "_LastPage")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_LastPage") = value
            ltlLastPage.Text = value
            SetButtonVisibility()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub lnkEndPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEndPage.Click
        CurrentPage = LastPage
        RaiseEvent PageChanged(Me, New CommandEventArgs("", CurrentPage))
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        CurrentPage += 1
        RaiseEvent PageChanged(Me, New CommandEventArgs("", CurrentPage))
    End Sub

    Protected Sub lnkPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrevious.Click
        CurrentPage -= 1
        RaiseEvent PageChanged(Me, New CommandEventArgs("", CurrentPage))
    End Sub

    Protected Sub lnkStartPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStartPage.Click
        CurrentPage = 1
        RaiseEvent PageChanged(Me, New CommandEventArgs("", CurrentPage))
    End Sub

#End Region

#Region "Public Methods"

    Public Sub InitPaging(ByVal pageNumber As Integer, ByVal sourceTotal As Integer, ByVal pageSize As Integer)
        CurrentPage = pageNumber
        SetLastPage(sourceTotal, pageSize)
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetLastPage(ByVal sourceTotal As Integer, ByVal pageSize As Integer)
        ' Finds out how many pages we should have 

        Dim remainder As Integer = sourceTotal Mod pageSize

        If remainder <> 0 Then
            LastPage = Math.Floor(sourceTotal / pageSize) + 1
        Else
            ' No remainder so return the whole number 
            LastPage = sourceTotal / pageSize
        End If

    End Sub

    Private Sub SetButtonVisibility()
        lnkStartPage.Visible = True
        lnkEndPage.Visible = True
        lnkNext.Visible = True
        lnkPrevious.Visible = True

        If LastPage = 1 Then
            ' There is only one page so hide everything
            lnkStartPage.Visible = False
            lnkEndPage.Visible = False
            lnkNext.Visible = False
            lnkPrevious.Visible = False
        Else
            If CurrentPage = 1 Then
                ' first page so hide the previous button
                lnkPrevious.Visible = False
                lnkStartPage.Visible = False
            ElseIf CurrentPage = LastPage Then
                lnkNext.Visible = False
                lnkEndPage.Visible = False
            End If
        End If

    End Sub
#End Region

End Class
