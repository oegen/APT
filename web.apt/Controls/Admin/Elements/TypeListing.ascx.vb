Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Admin_Elements_TypeListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Public Const ENABLE_TYPE_COMMAND_NAME As String = "EnableType"
    Public Const DISABLE_TYPE_COMMAND_NAME As String = "DisableType"

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindElementTypes()
        End If

    End Sub

    Protected Sub grdvTypes_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvTypes.PageIndexChanging

        grdvTypes.PageIndex = e.NewPageIndex
        BindElementTypes()

    End Sub

    Protected Sub grdvTypes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvTypes.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim tmpAptType As ElementType = CType(e.Row.DataItem, ElementType)
            Dim hypEdit As HyperLink = CType(e.Row.FindControl("hypEdit"), HyperLink)
            Dim hypSubTypes As HyperLink = CType(e.Row.FindControl("hypSubTypes"), HyperLink)
            Dim lnkSetActivity As LinkButton = CType(e.Row.FindControl("lnkSetActivity"), LinkButton)

            lnkSetActivity.CommandArgument = tmpAptType.ID

            If tmpAptType.Active = True Then
                lnkSetActivity.CommandName = DISABLE_TYPE_COMMAND_NAME
                lnkSetActivity.Text = "Disable"
            Else
                lnkSetActivity.CommandName = ENABLE_TYPE_COMMAND_NAME
                lnkSetActivity.Text = "Enable"
            End If

            hypEdit.NavigateUrl = String.Format("~/Admin/Element Manager/ElementType.aspx?aptTypeId={0}", tmpAptType.ID)
            hypSubTypes.NavigateUrl = String.Format("~/Admin/Element Manager/SubclassTypeListing.aspx?aptTypeId={0}", tmpAptType.ID)

        End If

    End Sub

    Protected Sub lnkSetActivity_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim typeId As Integer = e.CommandArgument

        If e.CommandName = ENABLE_TYPE_COMMAND_NAME Then
            AptTypeManager.SetAptTypeActivity(typeId, True)
        Else
            AptTypeManager.SetAptTypeActivity(typeId, False)
        End If

        BindElementTypes()

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindElementTypes()

        Dim elementTypes As List(Of ElementType) = AptTypeManager.GetAllAptTypes()

        grdvTypes.DataSource = elementTypes
        grdvTypes.DataBind()

    End Sub

#End Region

End Class
