
Partial Class Project_Project
    Inherits System.Web.UI.Page

#Region "Constants and Enumerations"

    Public Const NUMBER_OF_TABS = 6
    Public tabJavascript As String = "<script type=""text/javascript"">""$(function () {{$(""#tabs"").tabs(); $(""#tabs"").tabs(""select"", {0});}});</script>"""

#End Region

#Region "Properties"

    Public Property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

    Public ReadOnly Property TabIndex As Integer
        Get

            Dim returnTab As ConstantLibrary.ProjectTabIndex = ConstantLibrary.ProjectTabIndex.DASHBOARD

            If IsNumeric(Request.QueryString("tabIndex")) AndAlso _
               CType(Request.QueryString("tabIndex"), Integer) < NUMBER_OF_TABS Then

                Return CType(Request.QueryString("tabIndex"), Integer)
            Else
                Return CType(ConstantLibrary.ProjectTabIndex.DASHBOARD, Integer)
            End If

        End Get
    End Property

#End Region

#Region "Private Implementation"

    Private Sub SetActiveTabIndex()
        ltlTabJavascript.Text = String.Format(tabJavascript, TabIndex)
    End Sub

#End Region

End Class
