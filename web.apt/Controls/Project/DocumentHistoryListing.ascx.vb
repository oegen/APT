'----------------------------------------------------------------------------------------------
' Filename    : DocumentHistoryListing.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        04/10/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities
Imports System.Collections.Generic

Partial Class Controls_Project_DocumentHistoryListing
    Inherits System.Web.UI.UserControl

#Region "Constants and Enumerations"

#End Region

#Region "Private Fields"

#End Region

#Region "Properties"

    Public Property DocumentId As Integer
        Get
            Return ViewState(Me.UniqueID & "_DocumentId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.UniqueID & "_DocumentId") = value
            LoadDocumentVersions()
        End Set
    End Property

#End Region

#Region "Private Implementation"

    Private Sub LoadDocumentVersions()
        If DocumentId <> 0 Then
            Dim docVersions As List(Of DocumentVersion) = ProjectDocumentManager.GetDocumentVersions(DocumentId)

            If docVersions.Count > 0 Then
                gvDocVersions.DataSource = docVersions
                gvDocVersions.DataBind()
            Else
                gvDocVersions.Visible = False
                plcEmpty.Visible = True
            End If
        End If
    End Sub

#End Region

    Protected Sub gvDocVersions_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDocVersions.PageIndexChanging
        gvDocVersions.PageIndex = e.NewPageIndex
        LoadDocumentVersions()
    End Sub

    Protected Sub gvDocVersions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocVersions.RowDataBound

        If e.Row.DataItem IsNot Nothing Then
            Dim tmpVersion As DocumentVersion = e.Row.DataItem
            Dim lblCreationTime As Label = e.Row.FindControl("lblCreationTime")
            Dim lblUser As Label = e.Row.FindControl("lblUser")
            Dim hypView As HyperLink = e.Row.FindControl("hypView")

            lblCreationTime.Text = tmpVersion.CreationDateTime
            lblUser.Text = tmpVersion.AptUser.FullName

            hypView.NavigateUrl = String.Format("~/PDFAnnotation/Annotate.aspx?projectId={0}&dockey={2}&radpdfId={1}&readonly=True", _
                                        tmpVersion.Document.Project.ID, tmpVersion.RadPDFDocumentId, tmpVersion.Document.SecretKey)
        End If

    End Sub

End Class
