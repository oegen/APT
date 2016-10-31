'----------------------------------------------------------------------------------------------
' Filename    : TransitionDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class TransitionDAO : Inherits GenericDAO(Of Transition)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetTransitionFromPlace(ByVal place As Place, Optional ByVal direction As String = ArcDAO.IN_VALUE) As Transition

        Return (From o In context.Arcs
                Where o.Place.ID = place.ID And o.Direction = direction
                Select o.Transition).SingleOrDefault

    End Function

End Class
