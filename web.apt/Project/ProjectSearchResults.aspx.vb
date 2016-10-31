'----------------------------------------------------------------------------------------------
' Filename    : ProjectSearchResults.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptBusinessLogic
Imports aptEntities

Partial Class Project_ProjectSearchResults
    Inherits System.Web.UI.Page

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LoadPage()
        End If
    End Sub

#End Region
    
#Region "Private Implementation"

    Private Sub LoadPage()
        Dim searchType As Integer
        Dim searchValue As String

        If Integer.TryParse(CType(Request.QueryString("searchType"), Integer), searchType) Then
            searchValue = Request.QueryString("value")

            ctrlProjectSearchResults.SearchedAttribute = searchType
            ctrlProjectSearchResults.SearchValue = searchValue
        Else
            ' TODO no search provided.
        End If
    End Sub

#End Region

End Class
