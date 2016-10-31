Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Controls_Timesheets_ElementTimeSheetListing
    Inherits System.Web.UI.UserControl

    Public Event TimesheetElementEntrySuccess As EventHandler
    Public Event TimesheetElementEntryFailure As EventHandler


    Public Property ProjectId As Integer
        Get
            Return ViewState("_projectId")
        End Get
        Set(value As Integer)
            ViewState("_projectId") = value
            LoadElements()
        End Set
    End Property

    Public Property WorkDateTime As DateTime 
        Get
            Return ViewState("_workDateTime")
        End Get
        Set(value As DateTime)
            ViewState("_workDateTime") = value
        End Set
    End Property

    Private _reasons As List(Of TimesheetReason) = New List(Of TimesheetReason)

    Public Sub LoadElements()

        rptrElement.Visible = False
        ltlNoElements.Visible = False

        Dim elements As List(Of Element) = ElementManager.GetAllElementsByProject(ProjectId)

        If elements.Count > 0 Then
            _reasons = TimesheetManager.GetActiveTimesheetReasons()
            rptrElement.DataSource = elements
            rptrElement.DataBind()

            rptrElement.Visible = True
        Else
            ltlNoElements.Visible = True
        End If

    End Sub

    Protected Sub rptrElement_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrElement.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim element As Element = DirectCast(e.Item.DataItem, Element)
            Dim ltlElementName As Literal = e.Item.FindControl("ltlElementName")
            Dim txtHours As TextBox = e.Item.FindControl("txtHours")
            Dim txtMinutes As TextBox = e.Item.FindControl("txtMinutes")
            Dim ddlReason As DropDownList = e.Item.FindControl("ddlReason")
            Dim hdnElementId As HiddenField = e.Item.FindControl("hdnElementId")
            Dim lnkAutoFill As LinkButton = e.Item.FindControl("lnkAutoFill")

            hdnElementId.Value = element.ID
            ltlElementName.Text = element.TimesheetsDisplayString

            ' Use some jquery to call an anonymous function to fill in time
            ' should be on the following format:
            '$("#hourId").val(hours); $("#minuteId").val(minutes); return false;
            ' return false simply stops the link button from posting back

            lnkAutoFill.OnClientClick = String.Format("$(""#{0}"").val({1}); $(""#{2}"").val({3}); return false;", _
                                                      txtHours.ClientID, element.SubclassType.TimeHours, _
                                                      txtMinutes.ClientID, element.SubclassType.TimeMinutes)

            modComponent.BindDropDown(ddlReason, _reasons, "ID", "ReasonText", "reason", )

        End If

    End Sub


    Protected Sub lnkSave_Click(sender As Object, e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            If AreEntriesValid() Then
                SaveElementTimesheets()
            End If
        End If

    End Sub

    Private Sub SaveElementTimesheets()

        Dim savedItems As Integer = 0

        For Each repItem As RepeaterItem In rptrElement.Items
            If repItem.ItemType = ListItemType.Item OrElse _
                   repItem.ItemType = ListItemType.AlternatingItem Then

                Dim hours As Integer = 0
                Dim minutes As Integer = 0
                Dim selectedReasonId As Integer = 0
                Dim elementId As Integer = 0

                Dim txtHours As TextBox = DirectCast(repItem.FindControl("txtHours"), TextBox)
                Dim txtMinutes As TextBox = DirectCast(repItem.FindControl("txtMinutes"), TextBox)
                Dim ddlReason As DropDownList = DirectCast(repItem.FindControl("ddlReason"), DropDownList)
                Dim hdnElementId As HiddenField = DirectCast(repItem.FindControl("hdnElementId"), HiddenField)

                Integer.TryParse(txtHours.Text, hours)
                Integer.TryParse(txtMinutes.Text, minutes)
                elementId = hdnElementId.Value
                selectedReasonId = ddlReason.SelectedValue

                If RowCanBeEntered(hours, minutes, selectedReasonId) Then
                    ' User has entered something so let's save it
                    savedItems += 1
                    SaveTimesheet(hours, minutes, elementId, selectedReasonId, WorkDateTime)
                End If
            End If
        Next

        If savedItems > 0 Then
            RaiseEvent TimesheetElementEntrySuccess(Me, New EventArgs)
        End If

    End Sub

    Private Sub SaveTimesheet(hour As Integer, minute As Integer, _
                              elementId As Integer, reasonValue As Integer, timeOfWork As DateTime)

        Dim saveTimesheet As New Timesheet

        With saveTimesheet
            .HourSpent = hour
            .MinutesSpent = minute
            .Reason = TimesheetManager.GetTimesheetReason(reasonValue)
            .User = UserManager.GetUser(SessionManager.LoggedInUserId)
            .DateOfWork = timeOfWork
            .ContextEntityId = AppSettingsGet.EntityElementId
            .EntityParentId = elementId
        End With

        TimesheetManager.SaveTimesheet(saveTimesheet)

    End Sub

    Private Function AreEntriesValid() As Boolean

        ltlMessage.Text = "" ' Reset message

        Dim valid As Boolean = True

        For Each repItem As RepeaterItem In rptrElement.Items

            If repItem.ItemType = ListItemType.Item OrElse _
                   repItem.ItemType = ListItemType.AlternatingItem Then

                Dim hours As Integer = 0
                Dim minutes As Integer = 0
                Dim selectedReasonId As Integer = 0

                Dim txtHours As TextBox = DirectCast(repItem.FindControl("txtHours"), TextBox)
                Dim txtMinutes As TextBox = DirectCast(repItem.FindControl("txtMinutes"), TextBox)
                Dim ddlReason As DropDownList = DirectCast(repItem.FindControl("ddlReason"), DropDownList)
                Dim tdError = DirectCast(repItem.FindControl("tdError"), HtmlControl)

                Integer.TryParse(txtHours.Text, hours)
                Integer.TryParse(txtMinutes.Text, minutes)
                selectedReasonId = ddlReason.SelectedValue
                tdError.Visible = False

                If IsBadEntry(hours, minutes, selectedReasonId) Then
                    tdError.Visible = True
                    valid = False
                End If

            End If
        Next

        If valid = False Then
            ltlMessage.Text = "Please ensure all your entries are correct"
        End If

        Return valid

    End Function

    Private Function RowCanBeEntered(hours As Integer, minutes As Integer, reasonId As Integer) As Boolean
        ' This function is used to see if we can enter an item
        Return ((hours <> 0 OrElse minutes <> 0) AndAlso reasonId <> 0)
    End Function

    Public Function IsBadEntry(hours As Integer, minutes As Integer, selectedReasonId As Integer) As Boolean
        ' This function is used to see if the entry was bad (i.e the user didn't complete it properly)
        If selectedReasonId <> 0 Then
            ' User has selected a reason but not entered in any time?
            If hours = 0 AndAlso minutes = 0 Then
                Return True
            End If
        Else
            ' User has not selected a reason but they have entered time?
            If hours <> 0 OrElse minutes <> 0 Then
                Return True
            End If
        End If

        If minutes > 59 Then
            ' User shoudn't be entering above 59 minutes (should just convert it to an hour really)
            Return True
        End If

        Return False

    End Function

End Class
