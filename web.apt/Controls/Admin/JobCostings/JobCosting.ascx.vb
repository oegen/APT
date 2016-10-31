Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_JobCostings_JobCosting
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event JobTimesSaveSuccess As EventHandler
    Public Event JobTimesSaveFailure As EventHandler

    Public Property AptTypeId As Integer
        Get
            Return ViewState(Me.UniqueID & "_aptTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_aptTypeId") = value
            BindSubclassTypes()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindSubclassTypes()
        End If

    End Sub

    Protected Sub rptTypes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptTypes.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim tmpAptType As ElementType = CType(e.Item.DataItem, ElementType)

            Dim l As Label = CType(e.Item.FindControl("lblTypeName"), Label)
            If l IsNot Nothing Then
                l.Text = "<b>" & tmpAptType.Name & "</b>"
            End If

            Dim r As Repeater = CType(e.Item.FindControl("rptSubclassTypes"), Repeater)
            If r IsNot Nothing Then
                ' Bind Data
                r.DataSource = AptTypeManager.GetAllSubclassTypesByAptType(tmpAptType.ID)
                r.DataBind()
            End If
        End If
    End Sub

    Protected Sub rptSubclassTypes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        ' This function should populate subclass type name, hours, minutes and id

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim tmpSubclassType As SubclassType = CType(e.Item.DataItem, SubclassType)

            Dim lblSubclassTypesName As Label = CType(e.Item.FindControl("lblSubclassTypesName"), Label)
            lblSubclassTypesName.Text = tmpSubclassType.Name

            Dim txtPrintLeadTime As TextBox = CType(e.Item.FindControl("txtPrintLeadTime"), TextBox)
            txtPrintLeadTime.Text = tmpSubclassType.PrintLeadTime

            Dim hdnPrintLeadTime As HiddenField = CType(e.Item.FindControl("hdnPrintLeadTime"), HiddenField)
            hdnPrintLeadTime.Value = tmpSubclassType.PrintLeadTime

            Dim txtTimeHours As TextBox = CType(e.Item.FindControl("txtSubclassTypesTimeHours"), TextBox)
            txtTimeHours.Text = tmpSubclassType.TimeHours

            Dim hdnTimeHours As HiddenField = CType(e.Item.FindControl("hdnSubclassTypesTimeHours"), HiddenField)
            hdnTimeHours.Value = tmpSubclassType.TimeHours

            Dim txtTimeMinutes As TextBox = CType(e.Item.FindControl("txtSubclassTypesTimeMinutes"), TextBox)
            txtTimeMinutes.Text = tmpSubclassType.TimeMinutes

            Dim hdnTimeMinutes As HiddenField = CType(e.Item.FindControl("hdnSubclassTypesTimeMinutes"), HiddenField)
            hdnTimeMinutes.Value = tmpSubclassType.TimeMinutes

            Dim hdnSubclassTypesID As HiddenField = CType(e.Item.FindControl("hdnSubclassTypesID"), HiddenField)
            hdnSubclassTypesID.Value = tmpSubclassType.ID

            Dim hours As Integer
            Dim minutes As Integer

            TimesheetManager.GetAverageTime(hours, minutes, tmpSubclassType.ID)

            CType(e.Item.FindControl("lblSubclassTypesAverage"), Label).Text = _
                String.Format("{0} hour(s):{1} min(s)", hours, minutes)

        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            SaveJobCostingTimes()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindSubclassTypes()

        If AptTypeId <> 0 Then
            Dim loadAptType As ElementType = AptTypeManager.GetAptType(AptTypeId)
            Dim bindList As New List(Of ElementType)
            bindList.Add(loadAptType)

            rptTypes.DataSource = bindList
            rptTypes.DataBind()
        Else
            ' Assume they want all of the types
            rptTypes.DataSource = AptTypeManager.GetAllAptTypes()
            rptTypes.DataBind()
        End If

    End Sub

    Private Sub SaveJobCostingTimes()

        For Each i As RepeaterItem In rptTypes.Items

            ' Bind to nested repeater
            Dim r As Repeater = CType(i.FindControl("rptSubclassTypes"), Repeater)
            ' Loop inner repeater (subclass type level)
            For Each currentSubclassRepeaterItem As RepeaterItem In r.Items

                If currentSubclassRepeaterItem.ItemType = ListItemType.Item Or _
                   currentSubclassRepeaterItem.ItemType = ListItemType.AlternatingItem Then
                    ' Id
                    Dim id As Integer = 0

                    ' Default Hours
                    Dim hours As Integer = 0
                    Dim hoursBefore As Integer = 0

                    ' Default Minutes
                    Dim minutes As Integer = 0
                    Dim minutesBefore As Integer = 0

                    ' Default Print Lead Time

                    Dim printLeadTime As Integer = 0
                    Dim printLeadTimeBefore As Integer = 0

                    ' Get id from hidden field
                    Dim hdnSubclassTypesID As HiddenField = _
                        CType(currentSubclassRepeaterItem.FindControl("hdnSubclassTypesID"), HiddenField)
                    id = hdnSubclassTypesID.Value

                    ' Get print lead time from hidden field
                    Dim hdnPrintLeadTime As HiddenField = CType(currentSubclassRepeaterItem.FindControl("hdnPrintLeadTime"), HiddenField)
                    printLeadTimeBefore = hdnPrintLeadTime.Value

                    ' Get print lead time from text box
                    Dim txtPrintLeadTime As TextBox = CType(currentSubclassRepeaterItem.FindControl("txtPrintLeadTime"), TextBox)
                    If Integer.TryParse(txtPrintLeadTime.Text, printLeadTime) = False Then
                        printLeadTime = hdnPrintLeadTime.Value
                    End If

                    ' Get hours from hidden field
                    Dim hdnSubclassTypesTimeHours As HiddenField = _
                        CType(currentSubclassRepeaterItem.FindControl("hdnSubclassTypesTimeHours"), HiddenField)
                    hoursBefore = hdnSubclassTypesTimeHours.Value

                    ' Get hours from text box
                    Dim txtSubclassTypesTimeHours As TextBox = _
                        CType(currentSubclassRepeaterItem.FindControl("txtSubclassTypesTimeHours"), TextBox)

                    If Integer.TryParse(txtSubclassTypesTimeHours.Text, hours) = False Then
                        hours = hdnSubclassTypesTimeHours.Value
                    End If

                    ' Get minutes from hidden field
                    Dim hdnSubclassTypesTimeMinutes As HiddenField = _
                        CType(currentSubclassRepeaterItem.FindControl("hdnSubclassTypesTimeMinutes"), HiddenField)
                    minutesBefore = hdnSubclassTypesTimeMinutes.Value

                    ' Get minutes from text box
                    Dim txtSubclassTypesTimeMinutes As TextBox = _
                        CType(currentSubclassRepeaterItem.FindControl("txtSubclassTypesTimeMinutes"), TextBox)

                    If Integer.TryParse(txtSubclassTypesTimeMinutes.Text, minutes) = False Then
                        minutes = hdnSubclassTypesTimeMinutes.Value
                    ElseIf minutes > 59 Then
                        ' if the user has put more than  59 mins in then convert minutes and hours accordingly
                        hours += Math.Floor(minutes / 60)
                        minutes = minutes Mod 60
                    End If

                    ' Detect if users have changed hours
                    If hours <> hoursBefore _
                        OrElse minutes <> minutesBefore _
                        OrElse printLeadTime <> printLeadTimeBefore Then

                        Dim saveSubclassType As SubclassType = AptTypeManager.GetSubclassType(id)

                        If hours <> hoursBefore Then saveSubclassType.TimeHours = hours
                        If minutes <> minutesBefore Then saveSubclassType.TimeMinutes = minutes
                        If printLeadTime <> printLeadTimeBefore Then saveSubclassType.PrintLeadTime = printLeadTime

                        AptTypeManager.SaveSubclassType(saveSubclassType)
                    End If
                End If
            Next
        Next

        RaiseEvent JobTimesSaveSuccess(Me, New EventArgs)
        BindSubclassTypes()

    End Sub

#End Region

End Class

