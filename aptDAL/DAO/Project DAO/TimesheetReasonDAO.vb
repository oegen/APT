'----------------------------------------------------------------------------------------------
' Filename    : TimesheetDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     15/08/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class TimesheetReasonDAO : Inherits GenericDAO(Of TimesheetReason)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetActive() As List(Of TimesheetReason)

        Return (From o In Context.TimesheetReasons
                Where o.Active = True
                Select o).ToList()

    End Function

End Class
