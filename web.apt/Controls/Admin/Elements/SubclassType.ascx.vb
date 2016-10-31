Imports aptBusinessLogic
Imports aptEntities
Imports GenericUtilities
Imports System.Collections.Generic

Partial Class Controls_Admin_Elements_SubclassType
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public Event SubclassTypeSaveSuccess As EventHandler
    Public Event SubclassTypeSaveFailure As EventHandler

    Public Property SubclassTypeId As Integer
        Get
            Return ViewState(Me.UniqueID & "_subclassTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_subclassTypeId") = value
        End Set
    End Property

    Public Property AptTypeId As Integer
        Get
            Return ViewState(Me.UniqueID & "_aptTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_aptTypeId") = value
        End Set
    End Property

#End Region

#Region "Private Implementation"

    Private Sub LoadSubclassTypeDetails()

        If SubclassTypeId <> 0 Then

            ' Need to disable dropdown - user should not be able to change the element schema
            ' ddlElementSchema.Enabled = False

            Dim loadSubclassType As SubclassType = AptTypeManager.GetSubclassType(SubclassTypeId)

            If loadSubclassType IsNot Nothing Then
                txtSubclassTypeName.Text = loadSubclassType.Name
                'ddlElementSchema.SelectedValue = loadSubclassType.ElementSchema.ID
                'chkPrintRequired.Checked = loadSubclassType.PrintRequired
            End If

        End If

    End Sub

    'Private Sub BindElementSchemas()

    '    Dim elementSchemas As List(Of Schema) = SchemaManager.GetSchemasByEntity(AppSettingsGet.SchemaElementEntityID)
    '    ddlElementSchema.BindDataToDropDown(elementSchemas, "ID", "Name", "Schema")

    'End Sub

    Private Sub InitialiseControl()
        ' BindElementSchemas()

        If AptTypeId <> 0 Then
            hypSubclassListing.NavigateUrl = String.Format("~/Admin/Element Manager/SubclassTypeListing.aspx?aptTypeId={0}", AptTypeId)
        End If

        LoadSubclassTypeDetails()
    End Sub

    Private Sub Save()

        If AptTypeId <> 0 Then

            Dim saveSubtype As New SubclassType

            If SubclassTypeId <> 0 Then
                saveSubtype = AptTypeManager.GetSubclassType(SubclassTypeId)
            Else
                'saveSubtype.ElementSchema = SchemaManager.GetSchema(ddlElementSchema.SelectedValue)
                saveSubtype.Type = AptTypeManager.GetAptType(AptTypeId)
            End If

            ' TODO: Workflow - we really need to sort this out
            saveSubtype.Name = txtSubclassTypeName.Text
            ' saveSubtype.PrintRequired = chkPrintRequired.Checked
            saveSubtype.PrintRequired = False

            AptTypeManager.SaveSubclassType(saveSubtype)
            RaiseEvent SubclassTypeSaveSuccess(Me, New EventArgs)

        End If

    End Sub

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            InitialiseControl()
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click

        If Page.IsValid Then
            Save()
        End If

    End Sub

#End Region



End Class
