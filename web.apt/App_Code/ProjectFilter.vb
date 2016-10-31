Imports Microsoft.VisualBasic

Public Class ProjectFilter
    Private _Brand As Integer
    Public Property Brand() As Integer
        Get
            Return _Brand
        End Get
        Set(ByVal value As Integer)
            _Brand = value
        End Set
    End Property

    Private _Owner As String
    Public Property Owner() As String
        Get
            Return _Owner
        End Get
        Set(ByVal value As String)
            _Owner = value
        End Set
    End Property

    Private _SortBy As Integer
    Public Property SortBy() As Integer
        Get
            Return _SortBy
        End Get
        Set(ByVal value As Integer)
            _SortBy = value
        End Set
    End Property

    Private _TradeDate As Date
    Public Property TradeDate() As Date
        Get
            Return _TradeDate
        End Get
        Set(ByVal value As Date)
            _TradeDate = value
        End Set
    End Property

    Public Sub New(Optional ByVal pBrand As Integer = -1, _
                   Optional ByVal pOwner As String = "", _
                   Optional ByVal pSortBy As Integer = -1, _
                   Optional ByVal pTradeDate As Date = Nothing)
        Brand = pBrand
        Owner = pOwner
        SortBy = pSortBy
        If pTradeDate = Nothing Then
            TradeDate = Date.MinValue
        Else
            TradeDate = pTradeDate
        End If
    End Sub
End Class
