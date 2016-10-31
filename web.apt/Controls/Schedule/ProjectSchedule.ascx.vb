Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Schedule_ProjectSchedule
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property CurrentWeek As Integer
        Get
            Return ViewState("_currentWeek")
        End Get
        Set(ByVal value As Integer)
            ViewState("_currentWeek") = value
        End Set
    End Property

    Public Property Year As Integer
        Get
            Return ViewState("_year")
        End Get
        Set(ByVal value As Integer)
            ViewState("_year") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub gvReservedTimes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvReservedTimes.RowDataBound
        If e.Row.DataItem IsNot Nothing Then
            Dim reservedTime As ReservedTime = CType(e.Row.DataItem, ReservedTime)
            Dim lnkProject As HyperLink = CType(e.Row.FindControl("lnkProject"), HyperLink)
            Dim ltlAIN As Literal = CType(e.Row.FindControl("ltlAIN"), Literal)
            Dim ltlDuration As Literal = CType(e.Row.FindControl("ltlDuration"), Literal)
            Dim ltlNumArtworkers As Literal = CType(e.Row.FindControl("ltlNumArtworkers"), Literal)
            Dim ltlArtworkerAvg As Literal = CType(e.Row.FindControl("ltlArtworkerAvg"), Literal)
            Dim ltlReservedBy As Literal = CType(e.Row.FindControl("ltlReservedBy"), Literal)

            lnkProject.Text = reservedTime.Project.Name
            lnkProject.NavigateUrl = String.Format("~/Project/ProjectDetails.aspx?projectId={0}", reservedTime.Project.ID)
            ltlAIN.Text = reservedTime.Project.ID
            ltlDuration.Text = String.Format("{0} hours", reservedTime.Duration)
            ltlNumArtworkers.Text = reservedTime.NumArtworkers

            If reservedTime.Duration <> 0 AndAlso reservedTime.NumArtworkers <> 0 Then
                ltlArtworkerAvg.Text = String.Format("{0} hours", reservedTime.Duration / reservedTime.NumArtworkers)
            Else
                ltlArtworkerAvg.Text = "N/A"
            End If

            ltlReservedBy.Text = reservedTime.User.FullName
        End If
    End Sub

#End Region

#Region "Public Methods"

    Public Sub SetupControl()

        plcReservedTime.Visible = False
        plcErrorMessage.Visible = False

        Dim projectReservedTimes As List(Of ReservedTime) = ReserveTimeManager.GetAllWithinWeekAndYear(CurrentWeek, Year)

        If projectReservedTimes.Count > 0 Then
            gvReservedTimes.DataSource = projectReservedTimes
            gvReservedTimes.DataBind()
            plcReservedTime.Visible = True
        Else
            plcErrorMessage.Visible = True
        End If

    End Sub

#End Region

End Class
