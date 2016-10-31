'----------------------------------------------------------------------------------------------
' Filename    : ElementSchemaBuilder.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic
Imports System.Collections.Generic

Partial Class Controls_Project_ElementSchemaBuilder
    Inherits System.Web.UI.UserControl

#Region "Private Fields"

    Private _elementTypeTextField As String = "textfield"
    Private _elementTypeList As String = "list"
    Private _elementEntityId As Integer = AppSettingsGet.SchemaElementEntityID ' TODO: either set this in the web config or get by DAO
    Private _loadedSchemaDefinitions As List(Of SchemaDefinition)
    Private _hasLoadedEntitySchema As Boolean = False
    Private Const PAGE_VALIDATION_GROUP As String = "saveElement" ' TODO Allow this to be set via property
    Private Const GENERIC_CONTROL_PREFIX As String = "genericCtrl_{0}"

#End Region

#Region "Properties"

    Public Property CurrentSubclassTypeId As Integer
        Get
            Return ViewState(Me.UniqueID & "_SubclassTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_SubclassTypeId") = value
        End Set
    End Property

    Public Property CurrentElementId As Integer
        Get
            Return ViewState(Me.UniqueID & "_ElementId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_ElementId") = value
        End Set
    End Property

    Private Property LoadedSchemaDefinitions As List(Of SchemaDefinition)
        Get
            Return _loadedSchemaDefinitions
        End Get
        Set(ByVal value As List(Of SchemaDefinition))
            _loadedSchemaDefinitions = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' We need to create the dynamically binded controls each time

        If CurrentSubclassTypeId <> 0 AndAlso plcElementSchemaInfo.Controls.Count = 0 Then
            LoadElementSchemaInformation(CurrentSubclassTypeId)
        End If

    End Sub

#End Region

#Region "Private Implementation"

    Public Sub LoadElementSchemaInformation(ByVal subclassTypeId As Integer, Optional ByVal elementId As Integer = 0)

        ClearControls()

        CurrentSubclassTypeId = subclassTypeId
        If CurrentElementId = 0 Then
            CurrentElementId = elementId
        End If

        Dim elementSchema As Schema = ElementManager.GetElementSchemaByElementSubType(subclassTypeId)
        Dim schemaAttributeValues As List(Of SchemaData) = Nothing
        LoadedSchemaDefinitions = ElementManager.GetSchemaDefinitionByElementSchemas(elementSchema.ID)

        If elementId <> 0 Then
            ' user wants to load in a previously saved element so get the values
            schemaAttributeValues = SchemaManager.GetSchemaDataByEntityId(elementId)
        End If

        For i As Integer = 0 To LoadedSchemaDefinitions.Count - 1
            Dim tmpSchemaDefinition As SchemaDefinition = LoadedSchemaDefinitions(i)

            If schemaAttributeValues IsNot Nothing Then
                CreateControl(tmpSchemaDefinition, schemaAttributeValues(i).SchemaElementValue)
            Else
                CreateControl(tmpSchemaDefinition)
            End If
        Next

    End Sub

    Private Sub CreateControl(ByRef elementSchemaDef As SchemaDefinition, Optional ByVal value As String = "")

        ' First create a label for the schema definition property

        plcElementSchemaInfo.Controls.Add(New LiteralControl("<li>"))

        Select Case elementSchemaDef.SchemaElementType

            Case _elementTypeTextField
                SetupElementAttributeTextField(elementSchemaDef, value)
            Case _elementTypeList

                If value <> "" Then
                    SetupElementAttributeList(elementSchemaDef, CType(value, Integer))
                Else
                    SetupElementAttributeList(elementSchemaDef)
                End If

        End Select

        plcElementSchemaInfo.Controls.Add(New LiteralControl("</li>"))

    End Sub

    Private Sub SetupElementAttributeTextField(ByRef elementSchemaDef As SchemaDefinition, Optional ByVal value As String = "")

        Dim elementAttributeRow As New ASP.controls_generic_simpletextbox_ascx
        plcElementSchemaInfo.Controls.Add(elementAttributeRow)

        elementAttributeRow.ID = String.Format(GENERIC_CONTROL_PREFIX, elementSchemaDef.ID)
        elementAttributeRow.LabelText = elementSchemaDef.Name
        elementAttributeRow.Text = value

        ' Taken out because Karl no longer wants required fields for the schema values
        'If elementSchemaDef.SchemaElementRequired Then
        '    ' TODO: Set up validators
        '    elementAttributeRow.ValidationGroup = PAGE_VALIDATION_GROUP
        'End If

    End Sub

    Private Sub SetupElementAttributeList(ByRef elementSchemaDef As SchemaDefinition, Optional ByVal value As Integer = 0)

        Dim elementAttributeRow As New ASP.controls_generic_simpledropdownlist_ascx
        plcElementSchemaInfo.Controls.Add(elementAttributeRow)

        elementAttributeRow.ID = String.Format(GENERIC_CONTROL_PREFIX, elementSchemaDef.ID)
        elementAttributeRow.LabelText = elementSchemaDef.Name

        ' Taken out because Karl no longer wants required fields for the schema values
        'If elementSchemaDef.SchemaElementRequired Then
        '    ' TODO: Set up validators
        '    elementAttributeRow.ValidationGroup = PAGE_VALIDATION_GROUP
        'End If

        Dim datasource As List(Of ListNode) = ListManager.GetListsNodes(elementSchemaDef.List.ID)
        elementAttributeRow.BindDataToDropDown(datasource, "ID", "Name", elementSchemaDef.Name, "- Select {0} -")
        elementAttributeRow.SelectedValue = value

    End Sub

    Public Function ReturnElementAttributes() As List(Of SchemaData)

        Dim returnSchemaDefinitions As New List(Of SchemaData)
        Dim schemaAttributeValues As List(Of SchemaData) = Nothing

        If CurrentElementId <> 0 Then
            ' user wants to load in a previously saved element so get the values
            schemaAttributeValues = SchemaManager.GetSchemaDataByEntityId(CurrentElementId)
        End If

        Dim currentLoadedSchemaDefinition As Integer = 0

        For i As Integer = 0 To plcElementSchemaInfo.Controls.Count - 1

            If TypeOf (plcElementSchemaInfo.Controls(i)) Is LiteralControl = False Then

                Dim SchemaDefinition As SchemaDefinition = LoadedSchemaDefinitions.Item(currentLoadedSchemaDefinition)
                Dim currentSchemaData As New SchemaData

                If CurrentElementId <> 0 Then
                    currentSchemaData = schemaAttributeValues(currentLoadedSchemaDefinition)
                Else
                    currentSchemaData.SchemaDefinition = SchemaDefinition
                    currentSchemaData.SchemaElementType = SchemaDefinition.SchemaElementType
                    currentSchemaData.SchemaEntityID = _elementEntityId
                End If

                If SchemaDefinition.SchemaElementType = _elementTypeTextField Then
                    Dim elementSchemaDefControlTextField As ASP.controls_generic_simpletextbox_ascx = _
                        CType(plcElementSchemaInfo.Controls(i), ASP.controls_generic_simpletextbox_ascx)

                    currentSchemaData.SchemaElementValue = elementSchemaDefControlTextField.Text

                Else
                    Dim elementSchemaDefControlDropDownList As ASP.controls_generic_simpledropdownlist_ascx = _
                        CType(plcElementSchemaInfo.Controls(i), ASP.controls_generic_simpledropdownlist_ascx)

                    currentSchemaData.SchemaElementValue = elementSchemaDefControlDropDownList.SelectedValue

                End If

                returnSchemaDefinitions.Add(currentSchemaData)
                currentLoadedSchemaDefinition += 1

            End If

        Next

        Return returnSchemaDefinitions

    End Function

    Public Sub ClearControls()

        If plcElementSchemaInfo.Controls.Count > 0 Then
            ' Don't want duplicate controls to be on there so we need to clear the previous ones
            plcElementSchemaInfo.Controls.Clear()
        End If

    End Sub

#End Region

End Class
