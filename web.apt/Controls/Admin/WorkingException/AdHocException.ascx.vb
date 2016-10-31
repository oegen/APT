Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_WorkingException_AdHocException
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Private Enum AdHocType

        SINGLE_DAY = 0
        MULTIPLE_DAY = 1

    End Enum

    Private Const FORM_VALDIATION_GROUP As String = "vldSave"

#End Region

#Region "Properties"

    Public Event SaveAdHocSuccess As EventHandler
    Public Event SaveAdHocFailure As EventHandler

    Public Property AdHocExceptionId As Integer
        Get
            Return ViewState(Me.UniqueID & "_AdHocExceptionId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_AdHocExceptionId") = value
        End Set
    End Property

    Private Property CurrentAdHocType As AdHocType
        Get
            Return ViewState(Me.UniqueID & "_AdHocType")
        End Get
        Set(ByVal value As AdHocType)
            ViewState(Me.UniqueID & "_AdHocType") = value
            ProcessAdHocTypeView()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindArtworkers()
            LoadAdHocExceptionDetails()
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid = True Then
            If ValidateForm() = True Then
                SaveAdHocExceptionDetails()
            End If
        End If

    End Sub

    Protected Sub ddlAdHocOptions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAdHocOptions.SelectedIndexChanged
        CurrentAdHocType = ddlAdHocOptions.SelectedValue

        If ddlAdHocOptions.SelectedValue = AdHocType.MULTIPLE_DAY Then
            liHours.Visible = False
        Else
            liHours.Visible = True
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindArtworkers()

        Dim artworkers As List(Of AptUser) = UserManager.GetUsersInGlobalRole(AppSettingsGet.ArtworkerID)
        ddlUser.BindDataToDropDown(artworkers, "ID", "FullName", "User")

    End Sub

    Private Sub LoadAdHocExceptionDetails()

        CurrentAdHocType = AdHocType.SINGLE_DAY

        If AdHocExceptionId <> 0 Then

            Dim loadAdHocEx As Adhoc = AdhocExceptionManager.GetAdHocException(AdHocExceptionId)

            If loadAdHocEx.IsSingleDay = False Then
                CurrentAdHocType = AdHocType.MULTIPLE_DAY
            End If

            ddlUser.SelectedValue = loadAdHocEx.User.ID
            txtDescription.Text = loadAdHocEx.Description
            txtHours.Text = loadAdHocEx.Hours
            dpStartDate.SelectedDate = loadAdHocEx.StartDate
            dpEndDate.SelectedDate = loadAdHocEx.EndDate

        End If

    End Sub

    Private Sub SaveAdHocExceptionDetails()

        Dim saveAdHocEx As New Adhoc

        If AdHocExceptionId <> 0 Then
            saveAdHocEx = AdhocExceptionManager.GetAdHocException(AdHocExceptionId)
        End If

        saveAdHocEx.User = UserManager.GetUser(ddlUser.SelectedValue)
        saveAdHocEx.Description = txtDescription.Text

        saveAdHocEx.StartDate = dpStartDate.SelectedDate

        If CurrentAdHocType = AdHocType.SINGLE_DAY Then
            saveAdHocEx.EndDate = dpStartDate.SelectedDate
            saveAdHocEx.Hours = txtHours.Text
        Else
            saveAdHocEx.EndDate = dpEndDate.SelectedDate
            saveAdHocEx.Hours = 0
        End If

        AdhocExceptionManager.SaveAdHocException(saveAdHocEx)
        AdHocExceptionId = saveAdHocEx.ID
        RaiseEvent SaveAdHocSuccess(Me, New EventArgs)

    End Sub

    Private Sub ProcessAdHocTypeView()

        If CurrentAdHocType = AdHocType.SINGLE_DAY Then
            liEndDate.Visible = False
            txtHours.Visible = True
            dpEndDate.ValidationGroup = ""
            txtHours.Required = True
            txtHours.ValidationGroup = FORM_VALDIATION_GROUP
        Else
            liEndDate.Visible = True
            txtHours.Visible = False
            txtHours.Required = False
            txtHours.ValidationGroup = ""
            dpEndDate.ValidationGroup = FORM_VALDIATION_GROUP
        End If

    End Sub

    Private Function ValidateForm() As Boolean

        ltlEndDateError.Visible = False

        If CurrentAdHocType = AdHocType.MULTIPLE_DAY Then

            If dpEndDate.SelectedDate <= dpStartDate.SelectedDate Then
                ltlEndDateError.Visible = True
                ltlEndDateError.Text = "The end date must be after the start date"
                Return False
            ElseIf dpEndDate.SelectedDate = dpStartDate.SelectedDate Then
                ltlEndDateError.Visible = True
                ltlEndDateError.Text = "The selected dates are on the same, please select a single day as the adhoc type"
            End If

            Return True

        End If

        Return True

    End Function

#End Region

End Class
