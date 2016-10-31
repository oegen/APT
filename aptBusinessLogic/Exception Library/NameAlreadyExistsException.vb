'----------------------------------------------------------------------------------------------
' Filename    : UserAlreadyExistsException.vb
' Description : Custom made exception. Thrown under the circumstance as its name states.
'
' Release Initials  Date        Comment
' 1       TL        08/09/2011  First release.
'----------------------------------------------------------------------------------------------

Public Class NameAlreadyExistsException : Inherits Exception

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

End Class
