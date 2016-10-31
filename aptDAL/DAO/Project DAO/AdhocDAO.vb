'----------------------------------------------------------------------------------------------
' Filename    : AdhocDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class AdhocDAO : Inherits GenericDAO(Of Adhoc)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByActive(Optional ByVal active As Boolean = True) As List(Of Adhoc)
        Return (From o In context.Adhocs
                Where o.Active = active
                Select o).ToList()
    End Function

    Public Function GetAllBetweenDates(ByVal startDate As DateTime, ByVal endDate As DateTime, Optional ByVal active As Boolean = True) As List(Of Adhoc)
        Return (From o In Context.Adhocs
                Where ((o.StartDate >= startDate And o.StartDate <= endDate) OrElse
                (o.EndDate > startDate And o.EndDate <= endDate)) AndAlso o.Active = active _
                Select o).ToList
    End Function

    Public Function GetFutureAndPresent() As List(Of Adhoc)

        Return (From o In Context.Adhocs
                Where (o.StartDate >= DateTime.Today OrElse o.EndDate >= DateTime.Today) _
                    AndAlso o.Active = True
                Select o).ToList()

    End Function

End Class

