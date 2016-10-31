Imports aptEntities
Imports aptDAL
Imports aptBusinessLogic
Imports System.Collections.Generic
Imports GenericUtilities
Imports System.IO
Imports System.Globalization
Imports System.Data.Linq.SqlClient

Partial Class Test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'PdfWebControl1.LoadDocument(119)

        'Dim openUrl As String = Server.MapPath("~/Documents/80.pdf")

        'Using fStream As New FileStream(openUrl, FileMode.Open)
        '    PdfWebControl1.CreateDocument("final_test", fStream)
        'End Using

        'PdfWebControl1.CreateDocument("asdas", PdfWebControl1.GetOriginalPdf())

        'Dim cal As New GregorianCalendar

        'Dim explicitDate As New DateTime()
        'Dim totalHours As Integer = 0
        'explicitDate = "18/08/2011"

        'Dim weekNumber As Integer = cal.GetWeekOfYear(explicitDate, CalendarWeekRule.FirstDay, AptSettings.FirstDayOfWeek)

        'totalHours += AdhocExceptionManager.TotalHoursUnavailable(weekNumber, 2011)
        'totalHours += BlockMeetingExceptionManager.TotalHoursUnavailable(weekNumber, 2011)
        'totalHours += WorkingWeekManager.TotalHoursUnavailable()

        ' UserManager.GetUserByUsername(AppSettingsGet.DefaultBDCoordinator)


        'If Not IsPostBack Then
        '    'Get PDF as byte array from file (or database, browser upload, remote storage, etc)
        '    Dim pdfData As Byte() = System.IO.File.ReadAllBytes("D:\BarTenderBrochure.pdf")
        '    'Load PDF byte array into RAD PDF
        '    'Me.PdfWebControl1.CreateDocument("Document Name", pdfData)

        '    Dim settings As New RadPdf.Data.Document.PdfDocumentSettings
        '    'settings.

        '    PdfWebControl1.CopyDocument(6, True)

        'End If

        'Dim projectList As List(Of Project) = ProjectManager.GetAllProjects()

        'Dim results As List(Of Project) = modPaging.Page(projectList, 1, 3)

        'Dim weekDate As Date = modDates.FirstDateOfWeek(2011, 1, _
        '                                                   Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday)



    End Sub

    'Protected Sub PdfWebControl1_Saved(ByVal sender As Object, ByVal e As RadPdf.Integration.DocumentSavedEventArgs) Handles PdfWebControl1.Saved
    '    ' We only want to save another copy of the document IF the user has saved the document
    '    Dim saveData As Byte() = PdfWebControl1.GetPdf()
    '    GenericUtilities.modUtilities.SaveFile(saveData, "C:\asdf.pdf")
    'End Sub

    'Private Function GetStartWeekDateTimeFromWeekNumber(ByVal year As Integer, 
    '                                                    ByVal weekNum As Integer, 
    '                                                    ByVal rule As CalendarWeekRule, _
    '                                                    ByVal firstDayOfWeek as DayOfWeek) As DateTime

    '    Dim jan1 As New DateTime(year, 1, 1)

    '    Dim daysOffset As Integer = firstDayOfWeek - jan1.DayOfWeek
    '    Dim firstDayOfTheYearFromSpecifiedDay As DateTime = jan1.AddDays(daysOffset)

    '    Dim cal = CultureInfo.CurrentCulture.Calendar
    '    Dim firstWeek As Integer = cal.GetWeekOfYear(jan1, rule, firstDayOfWeek)

    '    If firstWeek <= 1 Then
    '        weekNum -= 1
    '    End If

    '    Dim result As DateTime = firstDayOfTheYearFromSpecifiedDay.AddDays(weekNum * 7)

    '    Return result

    'End Function



End Class
