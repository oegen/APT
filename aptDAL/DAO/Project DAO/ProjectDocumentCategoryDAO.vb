'----------------------------------------------------------------------------------------------
' Filename    : ProjectDocumentCategoryDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ProjectDocumentCategoryDAO : Inherits GenericDAO(Of ProjectDocumentCategory)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of ProjectDocumentCategory)
        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.ProjectCategories
                Where o.Active = active
                Select o).ToList()
    End Function

End Class
