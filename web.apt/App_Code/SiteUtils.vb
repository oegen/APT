Imports System.Web.UI.WebControls
Imports System
Imports aptEntities
Imports aptBusinessLogic

Public Module SiteUtils

    Public Sub LoadNullable(ByRef nullable As Nullable(Of Date), ByRef literalDate As Literal)

        If nullable.HasValue Then
            literalDate.Text = nullable.Value
        Else
            literalDate.Text = ""
        End If

    End Sub

    Public Function GetFilePath(ByRef document As ProjectDocument) As String

        With document
            Return String.Format("{0}{1}", AppSettingsGet.DocumentsFolderPath, .Path)
        End With

    End Function

End Module
