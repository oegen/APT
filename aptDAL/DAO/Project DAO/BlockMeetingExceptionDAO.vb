'----------------------------------------------------------------------------------------------
' Filename    : AdhocDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        17/8/2011   First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class BlockMeetingExceptionDAO : Inherits GenericDAO(Of BlockMeetingException)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetBlockMeetingsinDateRange(ByVal startDate As DateTime, ByVal endDate As DateTime) As List(Of BlockMeetingException)
        Return (From o In Context.BlockMeetingExceptions
                Where (o.StartDate >= startDate AndAlso o.StartDate <= endDate) _
                AndAlso o.Active = True).ToList
    End Function

    Public Function GetByPresentAndFuture() As List(Of BlockMeetingException)
        Return (From o In Context.BlockMeetingExceptions
                Where o.Active = True _
                AndAlso o.StartDate >= Date.Today).ToList()
    End Function

End Class
