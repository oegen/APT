Public Class ProjectSearchFilters

    ' Required
    Public Property UserId As Integer
    Public Property PageIndex As Integer
    Public Property PageSize As Integer
    Public Property TotalDataItems As Integer

    'optional
    Public Property BrandId As Integer = 0
    Public Property OwnerFilter As String = ""
    Public Property TradeDate As Nullable(Of DateTime) = Nothing
    Public Property SortBy As ProjectSortBy = ProjectSortBy.Name

End Class
