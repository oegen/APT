'----------------------------------------------------------------------------------------------
' Filename    : MasterPage.master.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

#Region "Constants and Enumerations"

    Private Const POPUP_SCRIPT As String = "<script type=""text/javascript"">javascript:$.modaldialog.{0}('{1}', {{ timeout: {2} }});</script>"
    Private Const TEST_SCRIPT As String = "javascript:$.modaldialog.{0}('{1}', {{ timeout: {2} }});"

#End Region

#Region "Properties"

    Public Property SelectedLeftNavPage As Controls_Navigation_LeftNav.Pages
        Get
            Return ctrlLeftNav.ActivePage
        End Get
        Set(ByVal value As Controls_Navigation_LeftNav.Pages)
            ctrlLeftNav.ActivePage = value
        End Set
    End Property

#End Region

#Region "Public Methods"

    Public Sub DisplayConfirmationMessage(ByVal message As String, Optional ByVal messageBoxType As MessageBoxType = MessageBoxType.Success, _
                                          Optional ByVal timeOut As Integer = 2)

        ltlConfirmationmessage.Text = String.Format(POPUP_SCRIPT, GetMessageBoxString(messageBoxType), message, timeOut)
    End Sub

    Private Function GetMessageBoxString(ByVal pMessageBoxType As MessageBoxType) As String

        Select Case pMessageBoxType
            Case MessageBoxType.Success
                Return "success"
            Case MessageBoxType.ErrorMessage
                Return "error"
        End Select

        Return "success"

    End Function

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        plcBindingFix.DataBind()
        Page.Header.DataBind()
        If Page.IsPostBack = False Then
            If IsNumeric(Request.QueryString("projectId")) Then
                ' check that the project is actually valid
                If ProjectManager.GetProject(Request.QueryString("projectId")) IsNot Nothing Then
                    ' Check if user has any access to the project
                    If PermissionsManager.CanAccessProject(SessionManager.LoggedInUserId, Request.QueryString("projectId")) = False Then
                        Response.Redirect("~/Default.aspx")
                    End If
                Else
                    Response.Redirect("~/Default.aspx")
                End If
            End If
        End If

    End Sub

#End Region

End Class

