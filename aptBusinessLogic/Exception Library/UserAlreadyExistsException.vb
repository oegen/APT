'----------------------------------------------------------------------------------------------
' Filename    : UserAlreadyExistsException.vb
' Description : Custom made exception. Thrown under the circumstance as its name states.
'
' Release Initials  Date        Comment
' 1       LP        07/07/2011  First release.
'----------------------------------------------------------------------------------------------

Public Class UserAlreadyExistsException : Inherits Exception

    Public Sub New(ByVal username As String)
        MyBase.New(String.Format("{0} already exists.", username))
    End Sub

End Class
