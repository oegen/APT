Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_WorkingWeekException_WorkingWeekException
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event SaveSuccess As EventHandler
    Public Event SaveFailure As EventHandler

    Public Property WorkingWeekExceptionId As Integer
        Get
            Return ViewState(Me.UniqueID & "_workingWeekExceptionId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_workingWeekExceptionId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            GetArtworkers()
            LoadWorkingWeekExceptionDetails()
        End If

    End Sub


    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid = True Then
            SaveWorkingWeekException()
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub GetArtworkers()

        Dim artworkers As List(Of AptUser) = UserManager.GetUsersInGlobalRole(AppSettingsGet.ArtworkerID)
        ddlUser.BindDataToDropDown(artworkers, "ID", "FullName", "User")

    End Sub

    Private Sub SaveWorkingWeekException()

        Dim saveWorkingWeekEx As New WorkingWeekException

        If WorkingWeekExceptionId <> 0 Then
            saveWorkingWeekEx = WorkingWeekManager.GetWorkingWeekException(WorkingWeekExceptionId)
        End If

        saveWorkingWeekEx.Description = txtDescription.Text
        saveWorkingWeekEx.Hours = txtHours.Text
        saveWorkingWeekEx.User = UserManager.GetUser(ddlUser.SelectedValue)

        WorkingWeekManager.SaveWorkingWeekException(saveWorkingWeekEx)
        WorkingWeekExceptionId = saveWorkingWeekEx.ID
        RaiseEvent SaveSuccess(Me, New EventArgs)

    End Sub

    Private Sub LoadWorkingWeekExceptionDetails()

        If WorkingWeekExceptionId <> 0 Then

            Dim loadWorkWeekEx As WorkingWeekException = WorkingWeekManager.GetWorkingWeekException(WorkingWeekExceptionId)

            ddlUser.SelectedValue = loadWorkWeekEx.User.ID
            txtDescription.Text = loadWorkWeekEx.Description
            txtHours.Text = loadWorkWeekEx.Hours

        End If

    End Sub

#End Region

End Class
