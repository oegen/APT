'----------------------------------------------------------------------------------------------
' Filename    : PremiumElementDetailsDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class PremiumElementDetailsDAO : Inherits GenericDAO(Of PremiumElementDetails)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByAdditionalElement(ByVal additionalElementId As Integer) As PremiumElementDetails

        Return (From o In Context.PremiumElementDetails
                Where o.AdditionalElement.ID = additionalElementId
                Select o).SingleOrDefault

    End Function

End Class
