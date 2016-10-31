Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic

Partial Class Controls_Admin_Users_UserListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enum"

    Private ENABLE_USER_COMMANDNAME As String = "EnableUser"
    Private DISABLE_USER_COMMANDNAME As String = "DisableUser"

    Private Enum FilterMode
        NONE = 0
        USERNAME = 1
        SURNAME = 2
    End Enum

#End Region

#Region "Properties"

    Public Event DisabledUser As EventHandler
    Public Event EnabledUser As EventHandler

    Public Property Username As String 
        Get
            Return ViewState(Me.UniqueID & "_username")
        End Get
        Set(ByVal value As String)
            ViewState(Me.UniqueID & "_username") = value
            CurrentFilterMode = FilterMode.USERNAME
            BindUsers()
        End Set
    End Property

    Public Property Surname As String
        Get
            Return ViewState(Me.UniqueID & "_surname")
        End Get
        Set(ByVal value As String)
            ViewState(Me.UniqueID & "_surname") = value
            CurrentFilterMode = FilterMode.SURNAME
            BindUsers()
        End Set
    End Property

    Private Property CurrentFilterMode As FilterMode
        Get
            Return ViewState(Me.UniqueID & "_currentFilterMode")
        End Get
        Set(ByVal value As FilterMode)
            ViewState(Me.UniqueID & "_currentFilterMode") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindUsers()
        End If

    End Sub

#End Region

#Region "Public Methods"

    Public Sub BindUsers()

        Dim userList As List(Of AptUser)

        Select Case CurrentFilterMode
            Case FilterMode.SURNAME
                userList = UserManager.SearchUserBySurname(Surname)
            Case FilterMode.USERNAME
                userList = UserManager.SearchUserByUserName(Username)
            Case Else
                userList = UserManager.GetAllUsers()
        End Select

        If userList.Count > 0 Then
            grdvUsers.Visible = True
            pnlEmpty.Visible = False
            grdvUsers.DataSource = userList
            grdvUsers.DataBind()
        Else
            grdvUsers.Visible = False
            pnlEmpty.Visible = True
        End If

    End Sub

    Protected Sub grdvUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvUsers.PageIndexChanging
        grdvUsers.PageIndex = e.NewPageIndex
        BindUsers()
    End Sub

    Protected Sub grdvUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvUsers.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim currentAptUser As AptUser = CType(e.Row.DataItem, AptUser)
            Dim userLogin As AptLogin = UserManager.GetLoginForUser(currentAptUser.ID)
            Dim ltlName As Literal = CType(e.Row.FindControl("ltlName"), Literal)
            Dim ltlUsername As Literal = CType(e.Row.FindControl("ltlUsername"), Literal)
            Dim hypViewDetails As HyperLink = CType(e.Row.FindControl("hypViewDetails"), HyperLink)
            Dim hypViewLogin As HyperLink = CType(e.Row.FindControl("hypViewLogin"), HyperLink)
            Dim lnkSetUserActivity As LinkButton = CType(e.Row.FindControl("lnkSetUserActivity"), LinkButton)

            ltlName.Text = currentAptUser.FullName

            If userLogin IsNot Nothing Then
                ltlUsername.Text = userLogin.Username
            Else
                ltlUsername.Text = "N/A"
            End If

            hypViewDetails.NavigateUrl = String.Format("~/Admin/Users/User.aspx?userId={0}", currentAptUser.ID)
            hypViewLogin.NavigateUrl = String.Format("~/Admin/Users/AddEditLogin.aspx?userId={0}", currentAptUser.ID)
            lnkSetUserActivity.CommandArgument = currentAptUser.ID

            If currentAptUser.Active Then
                lnkSetUserActivity.Text = "Disable User"
                lnkSetUserActivity.CommandName = DISABLE_USER_COMMANDNAME
            Else
                lnkSetUserActivity.Text = "Enable User"
                lnkSetUserActivity.CommandName = ENABLE_USER_COMMANDNAME
            End If

        End If

    End Sub

    Protected Sub lnkSetUserActivity_Command(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim userId As Integer = e.CommandArgument

        If e.CommandName = ENABLE_USER_COMMANDNAME Then
            EnableUser(userId)
        Else
            DisableUser(userId)
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub DisableUser(ByVal userId As Integer)

        UserManager.DisableUser(userId)
        BindUsers()
        RaiseEvent DisabledUser(Me, New EventArgs)

    End Sub

    Private Sub EnableUser(ByVal userId As Integer)

        UserManager.EnableUser(userId)
        BindUsers()
        RaiseEvent EnabledUser(Me, New EventArgs)

    End Sub

    Private Sub ResetFilters()
        Username = ""
        Surname = ""
    End Sub

#End Region

End Class
