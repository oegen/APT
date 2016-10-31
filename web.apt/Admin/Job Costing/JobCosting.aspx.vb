Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic
Imports GenericUtilities

Partial Class Admin_Job_Costing_JobCosting
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindAptTypes()
        End If

    End Sub

    Protected Sub ddlAptType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAptType.SelectedIndexChanged
        ctrlJobCostings.AptTypeId = ddlAptType.SelectedValue
    End Sub

    Protected Sub ctrlJobCostings_JobTimesSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlJobCostings.JobTimesSaveSuccess
        CType(Me.Master, MasterPages_Admin).DisplayConfirmationMessage("Job Costings has been saved successfully")
    End Sub

#End Region

#Region "Private Implementation"

    Private Sub BindAptTypes()

        Dim allAptTypes As List(Of ElementType) = AptTypeManager.GetAllAptTypes()
        modComponent.BindDropDown(ddlAptType, allAptTypes, "ID", "Name", "Element Type", "Select an {0}")

    End Sub

#End Region

End Class
