'----------------------------------------------------------------------------------------------
' Filename    : PlaceDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class PlaceDAO : Inherits GenericDAO(Of Place)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetPlacesFromTransition(ByVal transition As Transition, Optional ByVal direction As String = ArcDAO.OUT_VALUE) As List(Of Place)

        Return (From o In context.Arcs
                Where o.Transition.ID = transition.ID And o.Direction = direction
                Select o.Place).ToList

    End Function

    Public Function GetPlaceFromTransition(ByVal transitionId As Integer) As Place

        Return (From a In context.Arcs
                Where a.Transition.ID = transitionId And a.Direction = ArcDAO.IN_VALUE
                Select a.Place).SingleOrDefault

    End Function

    Public Function GetAllPlacesByToken(ByVal token As Token) As List(Of Place)

        Return (From o In context.Tokens
                Where o.ID = token.ID
                Select o.Place).ToList()

    End Function

End Class
