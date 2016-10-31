Public Class EntityHistory

    Private _comment As String

    Public Property Comment As String
        Get
            Return _comment
        End Get
        Set(ByVal value As String)
            _comment = value
        End Set
    End Property

    Private _dateTimeCompleted As DateTime
    Public Property DateTimeCompleted As DateTime
        Get
            Return _dateTimeCompleted
        End Get
        Set(ByVal value As DateTime)
            _dateTimeCompleted = value
        End Set
    End Property

    Private _action As String
    Public Property Action As String
        Get
            Return _action
        End Get
        Set(ByVal value As String)
            _action = value
        End Set
    End Property

    Private _user As String
    Public Property User As String
        Get
            Return _user
        End Get
        Set(ByVal value As String)
            _user = value
        End Set
    End Property

    ' TODO: Not sure how we're gonna do this - may not actually need to use it here?
    'Private _rejectResponse As AptUser
    'Public Property RejectResponse As AptUser
    '    Get
    '        Return _rejectResponse
    '    End Get
    '    Set(ByVal value As AptUser)
    '        _rejectResponse = value
    '    End Set
    'End Property

    Public ReadOnly Property FormattedCompleteDate As String
        Get
            Return _dateTimeCompleted.ToString("dd/MM/yyyy HH:mm")
        End Get
    End Property

    Private _projectDocs As List(Of TokenDocument)
    Public Property TasksDocuments As List(Of TokenDocument)
        Get
            Return _projectDocs
        End Get
        Set(ByVal value As List(Of TokenDocument))
            _projectDocs = value
        End Set
    End Property

End Class
