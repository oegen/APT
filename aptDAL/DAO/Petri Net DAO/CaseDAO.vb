﻿'----------------------------------------------------------------------------------------------
' Filename    : CaseDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class CaseDAO : Inherits GenericDAO(Of wfCase)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

End Class
