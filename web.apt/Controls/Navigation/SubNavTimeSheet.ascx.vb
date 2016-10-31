'----------------------------------------------------------------------------------------------
' Filename    : SubNavTimeSheet.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Controls_Navigation_SubNavTimeSheet
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Enum ENavigationItem

        TIMESHEET_ENTRY = 0
        TIMESHEET_LISTING = 2

    End Enum

#End Region

#Region "Properties"

    Public WriteOnly Property SelectedItem As ENavigationItem
        Set(ByVal value As ENavigationItem)
            SetSelectedItemStyle(value)
        End Set
    End Property

#End Region

#Region "Private Implementation"

    Private Sub SetSelectedItemStyle(ByVal selectedItem As ENavigationItem)
        Select Case selectedItem

            Case ENavigationItem.TIMESHEET_ENTRY
                liTimeEntry.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.TIMESHEET_LISTING
                liTimeSheetListing.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

        End Select
    End Sub

#End Region

End Class
