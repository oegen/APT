
Partial Class Reserve_Time_EditReserveTime
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            SetupPage()
        End If
    End Sub

    Protected Sub ctrlReserveTime_ReservedTimeItemSave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlReserveTime.ReservedTimeItemSave
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Reserve time was saved successfully!")
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SetupPage()
        Dim reserveTimeId As Integer
        Dim projectId As Integer

        If Integer.TryParse(Request.QueryString("projectId"), projectId) Then
            ctrlProjectHeader.ProjectId = projectId
            ctrlReserveTime.projectId = projectId
            ctrlSubNavProject.ProjectId = projectId
        End If

        If Integer.TryParse(Request.QueryString("reserveTimeId"), reserveTimeId) Then
            ctrlReserveTime.reservedTimeId = reserveTimeId
        End If

        ctrlReserveTime.SetupControl()
    End Sub

#End Region
    
End Class
