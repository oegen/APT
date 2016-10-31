Imports Microsoft.VisualBasic
Imports System.Web.UI
Imports System.Web

Public Module ProjectFilterManager

    Enum FilterUse
        Task
        Project
    End Enum


    Public Function GetProjectFilterFromCookie(ByVal page As Page, ByVal filterUse As FilterUse) As ProjectFilter
        Dim cookieString As String = GetCookieString(filterUse)
        Dim returnFilter As ProjectFilter = New ProjectFilter
        Dim cookie As HttpCookie = page.Request.Cookies(cookieString)
        If cookie IsNot Nothing Then
            Date.TryParse(cookie.Values.Item("TradeDate"), returnFilter.TradeDate)
            Integer.TryParse(cookie.Values.Item("Brand"), returnFilter.Brand)
            returnFilter.Owner = cookie.Values.Item("Owner")
            Integer.TryParse(cookie.Values.Item("SortBy"), returnFilter.SortBy)
        Else : Return Nothing
        End If
        Return returnFilter
    End Function

    Public Sub AddEditProjectFilter(ByVal page As Page, ByVal filter As ProjectFilter, ByVal filterUse As FilterUse)
        Dim cookieString As String = GetCookieString(filterUse)
        Dim cookie As HttpCookie
        If Not page.Request.Cookies(cookieString) Is Nothing Then
            cookie = page.Request.Cookies(cookieString)
            cookie.Values.Set("TradeDate", filter.TradeDate.ToString("yyyy-MM-dd"))
            cookie.Values.Set("Brand", filter.Brand.ToString())
            cookie.Values.Set("Owner", filter.Owner)
            cookie.Values.Set("SortBy", filter.SortBy.ToString())
            cookie.Expires = Now.AddHours(2)
            page.Response.Cookies.Set(cookie)
        Else
            cookie = New HttpCookie(cookieString)
            cookie.Values.Set("TradeDate", filter.TradeDate.ToString("yyyy-MM-dd"))
            cookie.Values.Set("Brand", filter.Brand.ToString())
            cookie.Values.Set("Owner", filter.Owner)
            cookie.Values.Set("SortBy", filter.SortBy.ToString())
            cookie.Expires = Now.AddHours(2)
            page.Response.Cookies.Add(cookie)
        End If
    End Sub

    Public Sub ClearProjectFilter(ByVal page As Page, ByVal filterUse As FilterUse)
        Dim cookieString As String = GetCookieString(filterUse)
        Dim cookie As HttpCookie = page.Request.Cookies(cookieString)
        If Not cookie Is Nothing Then
            cookie.Expires = Now.AddHours(-1)
            page.Response.Cookies.Set(cookie)
        End If
    End Sub

    Private Function GetCookieString(ByVal filterUse As FilterUse) As String
        Select Case filterUse
            Case ProjectFilterManager.FilterUse.Task
                Return "APTProjectFilter"
            Case ProjectFilterManager.FilterUse.Project
                Return "APTTaskFilter"
            Case Else
                Return "APTProjectFilter"
        End Select
    End Function
End Module
