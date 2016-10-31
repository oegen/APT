'----------------------------------------------------------------------------------------------
' Filename    : Role.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Data.Linq
Imports System.Data.Linq.Mapping

<Table(Name:="apt_tbl_role")>
Public Class Role
    Implements IComparable

#Region "Properties"

    <Column(IsPrimaryKey:=True, IsDbGenerated:=True, Name:="id", UpdateCheck:=UpdateCheck.Never)> _
    Public Property ID As Integer

    <Column(Name:="title", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Title As String

    <Column(Name:="description", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Description As String

    <Column(Name:="created", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Created As Nullable(Of DateTime)

    <Column(Name:="modified", UpdateCheck:=UpdateCheck.Never)> _
    Public Property Modified As Nullable(Of DateTime)

#End Region

    Public Overloads Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
        Dim otherSupplier As Role = CType(obj, Role)
        Return Me.ID.CompareTo(otherSupplier.ID)
    End Function

End Class
