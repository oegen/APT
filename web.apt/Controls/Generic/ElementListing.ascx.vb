'----------------------------------------------------------------------------------------------
' Filename    : ElementListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Controls_Generic_ElementListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

    Public Enum Mode
        DISABLE_ELEMENT_ADDITION = 2
    End Enum

#End Region

#Region "Private Fields"

    Private START_ELEMENT As String
    Private STOP_ELEMENT As String

#End Region

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.ID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_projectId") = value
        End Set
    End Property

    Public Property SelectedElementId As Integer
        Get
            Return ViewState(Me.ID & "_selectedElementId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_selectedElementId") = value
        End Set
    End Property

    Public Property CurrentMode As Mode
        Get
            Return ViewState(Me.ID & "_currentMode")
        End Get
        Set(ByVal value As Mode)
            ViewState(Me.ID & "_currentMode") = value
        End Set
    End Property

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        PermissionCheck()

        If Page.IsPostBack = False Then
            BindProjectElements()
        End If

    End Sub

    Protected Sub rptrElements_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptrElements.ItemDataBound

        If e.Item.DataItem IsNot Nothing Then

            Dim hypName As HyperLink = CType(e.Item.FindControl("hypName"), HyperLink)
            Dim liElement As HtmlGenericControl = CType(e.Item.FindControl("liElement"), HtmlGenericControl)
            Dim lnkStartStopElement As LinkButton = CType(e.Item.FindControl("lnkStartStopElement"), LinkButton)
            Dim currentElement As Element = CType(e.Item.DataItem, Element)

            'hypName.Text = currentElement.Name
            hypName.Text = currentElement.DisplayString
            hypName.NavigateUrl = String.Format("~/Elements/ProjectElement.aspx?projectId={0}&elementId={1}", currentElement.Project.ID, currentElement.ID)

            If currentElement.ElementStopped = True Then
                lnkStartStopElement.Text = "Start Element"
                lnkStartStopElement.CommandName = START_ELEMENT
                liElement.Attributes.Add("class", "stop")
            Else
                lnkStartStopElement.Text = "Stop Element"
                lnkStartStopElement.CommandName = STOP_ELEMENT
            End If

            lnkStartStopElement.CommandArgument = currentElement.ID

            If currentElement.ID = SelectedElementId Then
                liElement.Attributes.Add("class", "selected")
            End If

        End If

    End Sub

#End Region

#Region "Public Methods"

    Public Sub BindProjectElements()

        hypAddNewElement.NavigateUrl = String.Format("~/Elements/ProjectElement.aspx?projectid={0}", ProjectId)

        'rptrElements.DataSource = ElementManager.GetElementsByProject(ProjectId)
        rptrElements.DataSource = ElementManager.GetAllElementsByProject(ProjectId)
        rptrElements.DataBind()

    End Sub

    Public Sub LnkBtnStartStopElement(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim startOrStopElement As Element = ElementManager.GetElement(e.CommandArgument)

        Select Case e.CommandName

            Case START_ELEMENT
                startOrStopElement.ElementStopped = True
            Case STOP_ELEMENT
                startOrStopElement.ElementStopped = False

        End Select

        ElementManager.UpdateElement(startOrStopElement)
        BindProjectElements()

    End Sub

#End Region

#Region "Private Implementation"

    Private Sub PermissionCheck()

        If CurrentMode = Mode.DISABLE_ELEMENT_ADDITION OrElse PermissionsManager.CanUserEditProject(SessionManager.LoggedInUserId, ProjectId) = False Then
            divAddNewElement.Visible = False
        End If

    End Sub

#End Region

End Class
