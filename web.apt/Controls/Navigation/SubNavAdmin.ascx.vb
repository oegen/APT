Imports aptBusinessLogic

Partial Class Controls_Navigation_SubNavAdmin
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Enum ENavigationItem

        USER_MANAGER = 0
        TAG_MANAGER = 2
        ARC_RESPONSE_MANAGER = 4
        LIST_MANAGER = 6
        ELEMENT_MANAGER = 8
        JOB_COSTING = 10
        WORKING_WEEK_EXCEPTION = 12
        AD_HOC_EXCEPTION = 14
        BLOCK_MEETING_EXCEPTION = 16
        PROJECT_ROLE_GLOBAL_CHANGE = 18
        FULL_PROJECT_LISTING = 20
        FULL_TIMESHEET_LISTING = 22
        BBC_ITEM_LISTING = 24

    End Enum

#End Region

#Region "Private Fields"

    Private _selectedItem As ENavigationItem

#End Region

#Region "Properties"

    Public Property SelectedItem As ENavigationItem
        Get
            Return _selectedItem
        End Get
        Set(ByVal value As ENavigationItem)
            _selectedItem = value
            SetSelectItemStyle()
        End Set
    End Property

#End Region

#Region "Private Implementation"

    Private Sub SetSelectItemStyle()

        Select Case SelectedItem

            Case ENavigationItem.USER_MANAGER
                liUserManager.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.TAG_MANAGER
                liTagManager.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.ARC_RESPONSE_MANAGER
                liArcResponse.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.LIST_MANAGER
                liList.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.ELEMENT_MANAGER
                liElement.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.JOB_COSTING
                liJobCosting.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.WORKING_WEEK_EXCEPTION
                liWorkingWeekException.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.AD_HOC_EXCEPTION
                liAdHocException.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.BLOCK_MEETING_EXCEPTION
                liBlockMeeting.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.PROJECT_ROLE_GLOBAL_CHANGE
                liProjectRoleGlobalChange.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.FULL_PROJECT_LISTING
                liProjectListing.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.FULL_TIMESHEET_LISTING
                liTimesheetListing.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.BBC_ITEM_LISTING
                liBBCItem.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

        End Select

    End Sub

#End Region

End Class
