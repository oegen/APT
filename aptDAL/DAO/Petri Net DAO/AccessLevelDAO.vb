'----------------------------------------------------------------------------------------------
' Filename    : AccessLevelDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class AccessLevelDAO : Inherits GenericDAO(Of AccessLevel)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

End Class
