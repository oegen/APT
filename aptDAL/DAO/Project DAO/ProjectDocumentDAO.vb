'----------------------------------------------------------------------------------------------
' Filename    : ProjectDocumentDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ProjectDocumentDAO : Inherits GenericDAO(Of ProjectDocument)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of ProjectDocument)

        Return (From o In context.ProjectDocuments
                Where o.Active = active
                Select o).ToList()
    End Function

    Public Function GetByProjectAndCategory(ByVal projectId As Integer, ByVal categoryId As Integer) As List(Of ProjectDocument)

        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.ProjectDocuments
                Where o.Project.ID = projectId AndAlso o.Active = True _
                       AndAlso o.Category.ID = categoryId
                Order By o.ID Descending
                Select o).ToList()

    End Function

    Public Function GetByProject(ByVal projectId As Integer) As List(Of ProjectDocument)

        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.ProjectDocuments
                Where o.Project.ID = projectId AndAlso o.Active = True
                Order By o.ID Descending
                Select o).ToList()

    End Function

    Public Function GetProjectDocumentBySecretKey(ByVal secretKey As String)

        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.ProjectDocuments
                Where o.SecretKey = secretKey
                Select o).SingleOrDefault

    End Function

End Class
