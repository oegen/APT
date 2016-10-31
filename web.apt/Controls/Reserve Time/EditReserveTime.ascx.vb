Imports aptEntities
Imports aptBusinessLogic

Partial Class Controls_Reserve_Time_EditReserveTime
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property reservedTimeId As Integer
        Get
            Return ViewState("_reservedTimeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_reservedTimeId") = value
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

    Public Event ReservedTimeItemSave As EventHandler

#End Region

#Region "Public Methods"

    Public Sub SetupControl()
        Dim reservedTimeItem As ReservedTime = ReserveTimeManager.GetReserveTimeByID(reservedTimeID)

        If reservedTimeItem IsNot Nothing Then
            txtWeekNumber.Text = reservedTimeItem.WeekNumber
            txtYearNumber.Text = reservedTimeItem.Year
            txtDuration.Text = reservedTimeItem.Duration
            txtNumberArtworkers.Text = reservedTimeItem.NumArtworkers
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        SaveReservedItem()
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub SaveReservedItem()
        Dim reservedTimeItem As ReservedTime = ReserveTimeManager.GetReserveTimeByID(reservedTimeId)
        Dim update As Boolean = True

        If reservedTimeItem Is Nothing Then
            reservedTimeItem = New ReservedTime
            update = False
        End If

        reservedTimeItem.WeekNumber = txtWeekNumber.Text
        reservedTimeItem.Year = txtYearNumber.Text
        reservedTimeItem.Duration = txtDuration.Text
        reservedTimeItem.NumArtworkers = txtNumberArtworkers.Text
        reservedTimeItem.Project = ProjectManager.GetProject(projectId)

        If update Then
            ReserveTimeManager.UpdateReserveTime(reservedTimeItem, SessionManager.LoggedInUserId)
        Else
            ReserveTimeManager.AddReserveTime(reservedTimeItem, SessionManager.LoggedInUserId)
        End If

        RaiseEvent ReservedTimeItemSave(Me, New EventArgs)
    End Sub

#End Region

End Class
