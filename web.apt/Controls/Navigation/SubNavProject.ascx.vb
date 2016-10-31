'----------------------------------------------------------------------------------------------
' Filename    : SubNavProject.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------
Imports aptBusinessLogic

Partial Class Controls_Navigation_SubNavProject
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public WriteOnly Property SelectedItem As ConstantLibrary.ProjectSubNavItems
        Set(ByVal value As ProjectSubNavItems)

            'RemoveAllItemStyles()

            Select Case value

                Case ProjectSubNavItems.EDIT_PROJECT
                    liEditProject.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.RESERVE_PROJECT_TIME
                    liReserveProjectTime.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.VIEW_RESERVED_TIME
                    liViewReservedTime.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.PROJECT_HISTORY
                    liProjectHistory.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.ADDITIONAL_ELEMENTS
                    liAdditionalElements.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.VIEW_RESERVED_TIME
                    liViewReservedTime.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.AUDIT_TRAIL
                    liAuditTrail.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.BBC_ITEMS
                    liBBCItems.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.PREMIUM_BRIEF
                    liPremiumBrief.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.KITTING
                    liKitting.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

                Case ProjectSubNavItems.COSTINGS
                    liCostings.Attributes.Add("class", AppSettingsGet.NavSelectedItemStyle)

            End Select

        End Set
    End Property

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            CheckPermissions()
        End If
    End Sub

    Protected Sub lnkAdditionalElements_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAdditionalElements.Click
        Response.Redirect(String.Format("~/Elements/AdditionalElementListing.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkBBCList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBBCList.Click
        ' Response.Redirect(String.Format("~/Elements/BBCItemListing.aspx?projectId={0}", ProjectId))
        Response.Redirect(String.Format("~/Elements/ProjectBBCItemsListing.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkEditProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEditProject.Click
        Response.Redirect(String.Format("~/Project/ProjectDetails.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkPremiumBrief_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPremiumBrief.Click
        Response.Redirect(String.Format("~/Project/PremiumBrief.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkKitting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkKitting.Click
        Response.Redirect(String.Format("~/Kitting/KitListing.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkAuditTrail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAuditTrail.Click
        Response.Redirect(String.Format("~/Project/AuditTrail.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkCostings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCostings.Click
        Response.Redirect(String.Format("~/Project/ProjectCostings.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkProjectHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkProjectHistory.Click
        Response.Redirect(String.Format("~/Project/ProjectHistory.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkViewReservedTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewReservedTime.Click
        Response.Redirect(String.Format("~/Reserve Time/ReserveTimeListing.aspx?projectId={0}", ProjectId))
    End Sub

    Protected Sub lnkReserveProjectTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReserveProjectTime.Click
        Response.Redirect(String.Format("~/Project/ReserveTime.aspx?projectId={0}", ProjectId))
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub CheckPermissions()

        Dim showReserveTime As Boolean = PermissionsManager.CanAccessReserveTime(SessionManager.LoggedInUserId, Request.QueryString("projectId"))
        Dim showAudit As Boolean = PermissionsManager.CanAccessAuditTrail(SessionManager.LoggedInUserId)
        Dim showHistory As Boolean = PermissionsManager.CanAccessProjectHistory(SessionManager.LoggedInUserId)
        Dim showCostings As Boolean = PermissionsManager.CanAccessCostings(SessionManager.LoggedInUserId, ProjectId)

        ' No need for reserved time menu items, only required at the task stage.
        liReserveProjectTime.Visible = showReserveTime
        liViewReservedTime.Visible = showReserveTime
        liAuditTrail.Visible = showAudit
        liProjectHistory.Visible = showHistory
        liCostings.Visible = showCostings

    End Sub

    Private Sub RemoveAllItemStyles()

        liEditProject.Attributes.Add("class", "")
        liReserveProjectTime.Attributes.Add("class", "")
        liViewReservedTime.Attributes.Add("class", "")
        liProjectHistory.Attributes.Add("class", "")
        liAdditionalElements.Attributes.Add("class", "")
        liViewReservedTime.Attributes.Add("class", "")
        liAuditTrail.Attributes.Add("class", "")

    End Sub

#End Region

End Class
