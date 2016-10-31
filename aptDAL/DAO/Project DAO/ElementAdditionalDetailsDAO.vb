Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class ElementAdditionalDetailsDAO : Inherits GenericDAO(Of ElementAdditionalDetails)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property Context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property


    Public Function GetByElement(ByVal elementId As Integer) As ElementAdditionalDetails

        Return (From o In Context.ElementAdditionalDetails
                Where o.Element.ID = elementId
                Select o).SingleOrDefault

    End Function

End Class
