Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ElementKittingDetailsDAO : Inherits GenericDAO(Of ElementKittingDetails)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public Function GetByElement(ByVal elementId As Integer) As ElementKittingDetails

        Return (From o In Context.ElementKittingDetails
                Where o.Element.ID = elementId
                Select o).SingleOrDefault

    End Function

End Class
