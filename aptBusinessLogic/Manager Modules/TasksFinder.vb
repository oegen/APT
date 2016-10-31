Imports aptEntities
Imports aptDAL
Imports GenericDAL
Imports System.Data.Linq

Public Class MyTasksFinder

    Private _filters As ProjectSearchFilters

    'We can use a new context here because we're not saving anything here
    Private _context As APTContext = New APTContext(AppSettingsGet.SQLConnectionStr) 'ContextService.GetScopedDataContext(Of APTContext)(AppSettingsGet.ContextKey, AppSettingsGet.SQLConnectionStr)

    Public Sub New(filters As ProjectSearchFilters)
        If filters Is Nothing Then
            Throw New NoNullAllowedException("filters cannot be null")
        End If

        _filters = filters

        'Dim options = New DataLoadOptions()
        'options.LoadWith(Of Project)(Function(p) p.Tokens)
        'options.LoadWith(Of Token)(Function(p) p.Place)
        '_context.LoadOptions = options
    End Sub

    ' May want to change this to another type
    Public Function Find() As List(Of Project)

        Dim filteredProjects As IQueryable(Of Project) = GetInitialQuery()

        ' Remove the fluff early so it doesn't slow down the rest of the query
        filteredProjects = filteredProjects.Where(Function(p) p.Active) _
                                           .Where(Function(p) p.Stopped = False)

        ' filters
        filteredProjects = FilterByBrand(filteredProjects)
        filteredProjects = FilterByOwner(filteredProjects)

        ' distinct here for the joins in the filters above
        filteredProjects = filteredProjects.Distinct()

        ' sort
        filteredProjects = SortProjects(filteredProjects)

        Dim allProjects = filteredProjects.ToList()

        ' get the total item count for paging
        _filters.TotalDataItems = allProjects.Count

        ' Let's add the token and elements as well 


        Return allProjects.Skip((_filters.PageIndex - 1) * _filters.PageSize) _
                          .Take(_filters.PageSize).ToList()

    End Function

    Private Function GetInitialQuery() As IQueryable(Of Project)

        Dim filteredProjects As IQueryable(Of Project)

        If UserManager.DoesUserHaveAGlobalRole(_filters.UserId) Then
            filteredProjects = (From p In DAOGetter.ProjectDAO(_context).Projects
                                Select p).AsQueryable
        Else
            filteredProjects = DAOGetter.ProjectDAO(_context).GetByUserAsQueryable(_filters.UserId)
        End If

        Return filteredProjects

    End Function

    Private Function FilterByBrand(filteredProjects As IQueryable(Of Project)) As IQueryable(Of Project)

        'Dim options As New DataLoadOptions()
        'options.LoadWith(Of Project)(Function (p) p.)

        If (_filters.BrandId <> 0) Then
            filteredProjects = (From p In filteredProjects _
                                Join s In DAOGetter.SchemaDataDAO(_context).SchemaData On p.ID Equals s.ParentID _
                                Where s.SchemaDefinition.ID = AppSettingsGet.BrandListDefinitionID _
                                Where s.SchemaElementValue = _filters.BrandId _
                                Select p)
        End If

        Return filteredProjects

    End Function

    Private Function FilterByOwner(filteredProjects As IQueryable(Of Project))

        If String.IsNullOrEmpty(_filters.OwnerFilter) = False Then
            filteredProjects = (From p In filteredProjects _
                                Join pr In DAOGetter.ProjectRoleAssociationDAO(_context).ProjectRoleAssociation On pr.Project.ID Equals p.ID _
                                Where pr.Role.ID = AppSettingsGet.OwnerRoleID
                                Where pr.User.Forename.ToLower.Contains(_filters.OwnerFilter) OrElse _
                                      pr.User.Surname.ToLower.Contains(_filters.OwnerFilter) _
                                Select p)
        End If

        Return filteredProjects

    End Function

    Private Function FilterByTradeDate(filteredProjects As IQueryable(Of Project))

        If _filters.TradeDate IsNot Nothing Then
            If _filters.TradeDate.HasValue Then
                filteredProjects = filteredProjects.Where(Function(p) p.RequiredDate.Date = _filters.TradeDate)
            End If
        End If

        Return filteredProjects

    End Function

    Private Function SortProjects(filteredProjects As IQueryable(Of Project)) As IQueryable(Of Project)

        Select Case _filters.SortBy
            Case ProjectSortBy.AIN
                filteredProjects = filteredProjects.OrderBy(Function(p) p.ID)
            Case ProjectSortBy.Brand
                ' This isn't implemented for some reason?
            Case ProjectSortBy.Name
                filteredProjects = filteredProjects.OrderBy(Function(p) p.Name)
            Case ProjectSortBy.Owner
                ' projectList.OrderBy(Function(p) p.ID)
            Case ProjectSortBy.TradeInDate
                filteredProjects = filteredProjects.OrderBy(Function(p) p.RequiredDate)
        End Select

        Return filteredProjects

    End Function

    'Private Sub Tokens()

    '    Dim tokens As IQueryable(Of Token) = (From token In DAOGetter.TokenDAO(_context).Tokens
    '                                          Where token.Project.ID = 10001 _
    '                                          Where token.TokenStatus.ID = AppSettingsGet.TokenStatusFree _
    '                                          Where token.Place.ID <> AppSettingsGet.EndPlaceID _
    '                                          Select token)

    '    'FulfillsTransitionSecurityLookup(userId, projectId, GetTransitionByToken(token).ID)
    '    'CheckIfNotElementEndToken(token)



    'End Sub

End Class

