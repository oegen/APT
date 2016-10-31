'----------------------------------------------------------------------------------------------
' Filename    : BBCItemSelectAndDisplay.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        09/02/2012  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Generic_BBCItemSelectAndDisplay
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public property BBCItemId As integer
        Get
            Return ViewState(Me.UniqueID & "_bbcItemId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_bbcItemId") = value
            SetBBCItem()
        End Set
    End Property

    Public Property IsReadOnly As Boolean
        Get
            Return ViewState(Me.UniqueID & "_isReadOnly")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.UniqueID & "_isReadOnly") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub lnkChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkChange.Click
        ctrlBBCItemSearch.Visible = True
        plcBBCItemViewer.Visible = False
    End Sub

    Protected Sub ctrlBBCItemSearch_BBCItemSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlBBCItemSearch.BBCItemSelected
        ctrlBBCItemSearch.Visible = False
        plcBBCItemViewer.Visible = True

        BBCItemId = ctrlBBCItemSearch.SelectedBBCItemId
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetBBCItem()
        Dim loadBBCItem As NewBBCItem = BBCItemManager.GetNewBBCItem(BBCItemId)
        ltlBBCItem.Text = loadBBCItem.Description
    End Sub

    Private Sub SetReadOnly()
        lnkChange.Visible = Not IsReadOnly
    End Sub

#End Region

End Class
