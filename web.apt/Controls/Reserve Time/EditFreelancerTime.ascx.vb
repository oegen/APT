Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Reserve_Time_EditFreelancerTime
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property freelanceTimeId As Integer
        Get
            Return ViewState("_freelanceTimeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_freelanceTimeId") = value
        End Set
    End Property

    Public Property projectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
        End Set
    End Property

    Public Event FreelanceTimeSaved As EventHandler

#End Region

#Region "Events"

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        If Page.IsValid Then
            SaveReservedItem()
        End If
    End Sub

#End Region

#Region "Public Methods"

    Public Sub SetupControl()
        Dim freelanceTimeItem As FreelancerTime = ReserveTimeManager.GetFreelanceTimeById(freelanceTimeId)

        If freelanceTimeItem IsNot Nothing Then
            txtStartWeek.Text = freelanceTimeItem.StartWeek
            txtNumWeeks.Text = freelanceTimeItem.NumWeeks
            txtStartYear.Text = freelanceTimeItem.Year
            txtDuration.Text = freelanceTimeItem.Duration
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SaveReservedItem()
        Dim freelanceTimeItem As FreelancerTime = ReserveTimeManager.GetFreelanceTimeById(freelanceTimeId)
        Dim update As Boolean = True

        If freelanceTimeItem Is Nothing Then
            freelanceTimeItem = New FreelancerTime
            update = False
        End If

        freelanceTimeItem.StartWeek = txtStartWeek.Text
        freelanceTimeItem.Year = txtStartYear.Text
        freelanceTimeItem.NumWeeks = txtNumWeeks.Text
        freelanceTimeItem.Duration = txtDuration.Text
        freelanceTimeItem.Project = ProjectManager.GetProject(projectId)

        If update Then
            ReserveTimeManager.UpdateFreelanceTime(freelanceTimeItem, SessionManager.LoggedInUserId)
        Else
            ReserveTimeManager.AddFreelanceTime(freelanceTimeItem, SessionManager.LoggedInUserId)
        End If

        RaiseEvent FreelanceTimeSaved(Me, New EventArgs)
    End Sub

#End Region

End Class
