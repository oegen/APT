Public Class ReserveWeek

    Private m_weekNumber As Integer
    Private m_year As Integer
    Private m_availableHours As Decimal
    Private m_usedHours As Decimal

    Public Property WeekNumber As Integer
        Get
            Return m_weekNumber
        End Get
        Set(ByVal value As Integer)
            m_weekNumber = value
        End Set
    End Property

    Public Property Year As Integer
        Get
            Return m_year
        End Get
        Set(ByVal value As Integer)
            m_year = value
        End Set
    End Property

    Public Property AvailableHours As Decimal
        Get
            Return m_availableHours
        End Get
        Set(ByVal value As Decimal)
            m_availableHours = value
        End Set
    End Property

    Public Property UsedHours As Decimal
        Get
            Return m_usedHours
        End Get
        Set(ByVal value As Decimal)
            m_usedHours = value
        End Set
    End Property

End Class
