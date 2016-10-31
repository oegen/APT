Imports Microsoft.VisualBasic

Public Structure KitItem

    Private _id As Integer
    Private _quantity As Integer

    Public Property ItemID As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property Quantity As Integer
        Get
            Return _quantity
        End Get
        Set(ByVal value As Integer)
            _quantity = value
        End Set
    End Property

End Structure
