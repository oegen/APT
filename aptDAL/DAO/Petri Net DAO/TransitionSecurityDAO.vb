'----------------------------------------------------------------------------------------------
' Filename    : TransitionSecurityDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class TransitionSecurityDAO : Inherits GenericDAO(Of TransitionSecurity)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByTransition(ByVal transitionId As Integer) As List(Of TransitionSecurity)

        Return (From ts In context.TransitionSecurities
                Where ts.Transition.ID = transitionId
                Select ts).ToList

    End Function

End Class

