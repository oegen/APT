Imports aptBusinessLogic

Partial Class Controls_Navigation_SubNavReports
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Enum ENavigationItem

        KPI = 0
        CLIENT = 2
        COORDINATOR = 4

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
            Case ENavigationItem.CLIENT
                liClient.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.COORDINATOR
                liCoordinator.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            Case ENavigationItem.KPI
                liKPI.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

        End Select

    End Sub

#End Region

End Class
