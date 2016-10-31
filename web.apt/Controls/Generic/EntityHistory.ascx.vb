Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Generic_EntityHistory
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public property ProjectId As Integer
        Get
            Return ViewState(Me.UniqueID & "_projectId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_projectId") = value
        End Set
    End Property

    Public Property ElementId As Integer
        Get
            Return ViewState(Me.UniqueID & "_elementId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_elementId") = value
        End Set
    End Property

    Public WriteOnly Property HasHistory As Boolean
        Set(ByVal value As Boolean)
            If value = True Then
                plcHistory.Visible = True
                plcNoHistory.Visible = False
            Else
                plcHistory.Visible = False
                plcNoHistory.Visible = True
            End If
        End Set
    End Property

#End Region


#Region "Private Implementation"

    Private Sub BindHistory()

        If ProjectId <> 0 Then

            Dim history As List(Of EntityHistory) = HistoryGenerator.GetEntityHistory(ProjectId, ElementId)

            If history.Count > 0 Then
                HasHistory = True
            Else
                HasHistory = False
            End If

            grdvHistory.DataSource = history
            grdvHistory.DataBind()

        End If

    End Sub

#End Region

#Region "Events"

    Protected Sub grdvHistory_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvHistory.PageIndexChanging

        grdvHistory.PageIndex = e.NewPageIndex
        BindHistory()

    End Sub

    Protected Sub grdvHistory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvHistory.RowDataBound

        If e.Row.DataItem IsNot Nothing Then

            Dim currentHistory As EntityHistory = CType(e.Row.DataItem, EntityHistory)
            Dim ltlDate As Literal = CType(e.Row.FindControl("ltlDate"), Literal)
            Dim ltlAction As Literal = CType(e.Row.FindControl("ltlAction"), Literal)
            Dim ltlPerformedBy As Literal = CType(e.Row.FindControl("ltlPerformedBy"), Literal)
            Dim ltlComment As Literal = CType(e.Row.FindControl("ltlComment"), Literal)

            ltlDate.Text = currentHistory.FormattedCompleteDate
            ltlAction.Text = currentHistory.Action
            ltlPerformedBy.Text = currentHistory.User
            ltlComment.Text = currentHistory.Comment

            If currentHistory.TasksDocuments.Count > 0 Then

                Dim plcDocuments As PlaceHolder = CType(e.Row.FindControl("plcDocuments"), PlaceHolder)

                For i As Integer = 0 To currentHistory.TasksDocuments.Count - 1

                    If i > 0 Then
                        Dim liBrTag As New Literal
                        liBrTag.Text = "<br />"
                        plcDocuments.Controls.Add(liBrTag)
                    End If

                    Dim hypDocument As New HyperLink
                    Dim tokenDoc As TokenDocument = CType(currentHistory.TasksDocuments(i), TokenDocument)

                    hypDocument.Text = tokenDoc.Document.Name
                    hypDocument.CssClass = "nameLink"
                    hypDocument.NavigateUrl = String.Format("{0}{1}", AppSettingsGet.DocumentsFolderPath, tokenDoc.Document.Path)
                    plcDocuments.Controls.Add(hypDocument)

                Next

            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            BindHistory()
        End If

    End Sub

#End Region

End Class
