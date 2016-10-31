Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic

Partial Class Reserve_Time_ReserveTimeListing
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            SetupPage()
        End If
    End Sub

    Protected Sub lnkAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAddNew.Click
        Response.Redirect(String.Format("~/Reserve Time/EditReserveTime.aspx", ctrlReserveTimeListing.projectId))
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPage()
        Dim projectId As Integer

        If Integer.TryParse(Request.QueryString("projectId"), projectId) Then

            ctrlReserveTimeListing.projectId = projectId
            ctrlFreelancerListing.projectId = projectId
            ctrlProjectHeader.ProjectId = projectId
            ctrlSubNavProject.projectId = projectId

        Else

            Response.Redirect("~/Project/ProjectListing.aspx")

        End If

    End Sub

#End Region

End Class
