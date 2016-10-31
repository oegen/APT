'----------------------------------------------------------------------------------------------
' Filename    : ArcDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ArcDAO : Inherits GenericDAO(Of Arc)

    Public Const IN_VALUE As String = "IN"
    Public Const OUT_VALUE As String = "OUT"
    Public Const REJECT_PRECONDITION As String = "r"

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetArcsFromTransition(ByVal transitionId As Integer, Optional ByVal direction As String = ArcDAO.OUT_VALUE) As List(Of Arc)

        Return (From o In context.Arcs
                Where o.Transition.ID = transitionId And o.Direction = direction
                Select o).ToList

    End Function

    Public Function GetArcsFromPlace(ByVal place As Place, Optional ByVal direction As String = OUT_VALUE) As List(Of Arc)

        Return (From o In context.Arcs
                Where (o.Place.ID = place.ID And o.Direction = direction)
                Select o).ToList()

    End Function

    Public Function GetRejectionArcsToPlace(ByVal place As Place, Optional ByVal direction As String = OUT_VALUE) As List(Of Arc)

        Return (From o In context.Arcs
                Where (o.Place.ID = place.ID AndAlso _
                       o.Direction = direction AndAlso _
                       o.PreCondition = REJECT_PRECONDITION)
                Select o).ToList()

    End Function

    Public Function GetTransitionFromStartPlace(ByVal placeId As Integer) As Transition

        Return (From o In context.Arcs
                Where o.Place.ID = placeId And o.Direction = IN_VALUE
                Select o.Transition).SingleOrDefault

    End Function

End Class
