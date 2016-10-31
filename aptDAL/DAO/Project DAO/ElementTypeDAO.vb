'----------------------------------------------------------------------------------------------
' Filename    : ElementTypeDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ElementTypeDAO : Inherits GenericDAO(Of ElementType)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of ElementType)
        Dim context As APTContext = CType(_context, APTContext)

        Return (From o In context.AptTypes
                Where o.Active = active
                Order By o.Name
                Select o).ToList()

    End Function

End Class
