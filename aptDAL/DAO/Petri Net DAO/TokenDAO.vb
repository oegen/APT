'----------------------------------------------------------------------------------------------
' Filename    : TokenDAO.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Configuration
Imports System.Data.Linq
Imports aptEntities
Imports GenericDAL

Public Class TokenDAO : Inherits GenericDAO(Of Token)

    Public Sub New(ByRef pContext As APTContext)
        MyBase._context = pContext
    End Sub

    Private ReadOnly Property context As APTContext
        Get
            Return CType(_context, APTContext)
        End Get
    End Property

    Public ReadOnly Property Tokens As IQueryable(Of Token)
        Get
            Return context.Tokens.AsQueryable
        End Get
    End Property

    Public Function GetTokensAtPlace(ByVal placeId As Integer, ByVal projectId As Integer) As List(Of Token)

        Return (From o In context.Tokens
                Where o.Place.ID = placeId And o.Project.ID = projectId
                Select o).ToList

    End Function

    Public Function GetAllTokensAtPlace(ByVal placeId As Integer) As List(Of Token)
        Return (From o In context.Tokens
                Where o.Place.ID = placeId
                Select o).ToList
    End Function

    Public Function GetTokensByProject(ByVal projectId As Integer) As List(Of Token)

        Return (From o In context.Tokens
                Where o.Project.ID = projectId
                Select o).ToList

    End Function

    Public Function GetTokensByStatus(ByVal tokenStatus As Integer) As List(Of Token)

        Return (From o In context.Tokens
                Where o.TokenStatus.ID = tokenStatus
                Select o).ToList

    End Function

    Public Function GetTokensAtPlaceByStatus(ByVal placeId As Integer, ByVal projectId As Integer, ByVal tokenStatus As Integer) As List(Of Token)

        Return (From o In context.Tokens
                Where o.Place.ID = placeId And o.Project.ID = projectId And o.TokenStatus.ID = tokenStatus
                Select o).ToList

    End Function

    Public Function GetFreeByContextAndEntity(ByVal contextId As Integer, ByVal entityId As Integer, ByVal tokenStatusId As Integer) As List(Of Token)

        Return (From o In context.Tokens
                Where o.ContextEntity.ID = contextId _
                   AndAlso o.ContextParentID = entityId _
                   AndAlso o.TokenStatus.ID = tokenStatusId
                Select o).ToList

    End Function

    Public Function GetFirstByProject(projectId As Integer)

        Return (From o In context.Tokens
                Where o.Project.ID = projectId
                Order By o.EnabledDate Ascending
                Select o).FirstOrDefault()

    End Function

End Class
