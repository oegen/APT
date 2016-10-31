'----------------------------------------------------------------------------------------------
' Filename    : AdditionalElementDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class AdditionalElementDAO : Inherits GenericDAO(Of AdditionalElement)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetAdditionalElementsByProject(ByVal projectId As Integer, Optional ByVal active As Boolean = True)

        Return (From o In Context.AdditionalElements
                Where o.Project.ID = projectId _
                    And o.Active = active
                Select o).ToList()

    End Function

End Class

