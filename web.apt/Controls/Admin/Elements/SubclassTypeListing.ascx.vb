Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_Elements_SubclassTypeListing
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Const ENABLE_SUBTYPE_COMMAND_NAME As String = "EnableSubType"
    Public Const DISABLE_TYPE_COMMAND_NAME As String = "DisableSubType"

    Public Property AptTypeId As Integer
        Get
            Return ViewState(Me.UniqueID & "_aptTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_aptTypeId") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindSubclassTypes()
        End If

    End Sub

    Protected Sub grdvSubclassType_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvSubclassType.PageIndexChanging

        grdvSubclassType.PageIndex = e.NewPageIndex
        BindSubclassTypes()

    End Sub

    Protected Sub grdvSubclassType_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvSubclassType.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpSubclassType As SubclassType = CType(e.Row.DataItem, SubclassType)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim lnkSetActivity As LinkButton = CType(e.Row.FindControl("lnkSetActivity"), LinkButton)

            lnkSetActivity.CommandArgument = tmpSubclassType.ID

            If tmpSubclassType.Active = True Then
                lnkSetActivity.Text = "Disable"
                lnkSetActivity.CommandName = DISABLE_TYPE_COMMAND_NAME
            Else
                lnkSetActivity.Text = "Enable"
                lnkSetActivity.CommandName = ENABLE_SUBTYPE_COMMAND_NAME
            End If

            hypEdit.NavigateUrl = String.Format("~/Admin/Element Manager/SubclassType.aspx?subclassTypeId={0}&aptTypeId={1}", tmpSubclassType.ID, tmpSubclassType.Type.ID)

        End If

    End Sub

    Protected Sub lnkSetActivity_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim typeId As Integer = e.CommandArgument

        If e.CommandName = ENABLE_SUBTYPE_COMMAND_NAME Then
            AptTypeManager.SetSubclassTypeActivity(typeId, True)
        Else
            AptTypeManager.SetSubclassTypeActivity(typeId, False)
        End If

        BindSubclassTypes()

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindSubclassTypes()

        If AptTypeId <> 0 Then

            Dim subclassTypes As List(Of SubclassType) = AptTypeManager.GetAllSubclassTypesByAptType(AptTypeId)

            grdvSubclassType.DataSource = subclassTypes
            grdvSubclassType.DataBind()

        End If

    End Sub

#End Region

End Class
