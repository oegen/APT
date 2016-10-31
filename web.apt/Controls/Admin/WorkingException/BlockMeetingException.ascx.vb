Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Admin_WorkingException_BlockMeetingException
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event SaveBlockMeetingSuccess As EventHandler
    Public Event SaveBlockMeetingFailure As EventHandler

    Public Property BlockMeetingExceptionId As Integer
        Get
            Return ViewState(Me.UniqueID & "_blockMeetingExceptionId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_blockMeetingExceptionId") = value
            LoadBlockMeetingException()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid = True Then
            SaveBlockMeetingException()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub LoadBlockMeetingException()

        If BlockMeetingExceptionId <> 0 Then

            Dim saveBlockMeetingEx As BlockMeetingException = _
                BlockMeetingExceptionManager.GetBlockMeetingException(BlockMeetingExceptionId)

            If saveBlockMeetingEx IsNot Nothing Then
                txtDate.SelectedDate = saveBlockMeetingEx.StartDate
                txtDescription.Text = saveBlockMeetingEx.Description
                txtHours.Text = saveBlockMeetingEx.Hours
            Else
                BlockMeetingExceptionId = 0
            End If

        End If

    End Sub

    Private Sub SaveBlockMeetingException()

        Dim saveBlockMeetingEx As New BlockMeetingException

        If BlockMeetingExceptionId <> 0 Then
            saveBlockMeetingEx = BlockMeetingExceptionManager.GetBlockMeetingException(BlockMeetingExceptionId)
        End If

        saveBlockMeetingEx.StartDate = txtDate.SelectedDate
        saveBlockMeetingEx.Description = txtDescription.Text
        saveBlockMeetingEx.Hours = txtHours.Text

        BlockMeetingExceptionManager.SaveBlockMeeting(saveBlockMeetingEx)
        BlockMeetingExceptionId = saveBlockMeetingEx.ID

        RaiseEvent SaveBlockMeetingSuccess(Me, New EventArgs)

    End Sub

#End Region


End Class
