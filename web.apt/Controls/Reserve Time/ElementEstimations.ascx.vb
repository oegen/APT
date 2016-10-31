Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic

Partial Class Controls_Reserve_Time_ElementEstimations
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("_projectId") = value
            SetupElements()
        End Set
    End Property

    Public Property TotalTime As Decimal
        Get
            Return ViewState("_totalTime")
        End Get
        Set(ByVal value As Decimal)
            ViewState("_totalTime") = value
        End Set
    End Property

    Public Event UseTotalTime As EventHandler

#End Region

#Region "Events"

    Protected Sub rptrElementEstimation_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrElementEstimations.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim element As Element = CType(e.Item.DataItem, Element)
            Dim elementAdditionalInfo As ElementAdditionalDetails = ElementManager.GetElementAdditionalInfoByElement(element.ID)
            Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
            Dim lblDuration As Label = CType(e.Item.FindControl("lblDuration"), Label)

            lblName.Text = element.Name
            lblDuration.Text = String.Format("{0} hour(s)", elementAdditionalInfo.ArtworkTime)
        End If
    End Sub

    Protected Sub lnkUseTotalTIme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUseTotalTIme.Click
        RaiseEvent UseTotalTime(Me, New EventArgs)
    End Sub

#End Region

#Region "Public Methods"

    Private Sub SetupElements()

        Dim elementList As List(Of Element) = ElementManager.GetElementsByProject(ProjectId)

        rptrElementEstimations.DataSource = elementList
        rptrElementEstimations.DataBind()

        CalculateTotalTime()

    End Sub

    Private Sub CalculateTotalTime()
        TotalTime = ElementManager.GetElementTotalEstimationTime(ProjectId)
        lblTotalTime.Text = String.Format("{0} hours", TotalTime)
    End Sub

#End Region

End Class
