'----------------------------------------------------------------------------------------------
' Filename    : GenericContentTitle.ascx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       TL        25/07/2011  First release.
'----------------------------------------------------------------------------------------------


Partial Class Controls_Layout_GenericHeader
    Inherits System.Web.UI.UserControl

    Public Enum Setting

        TIMESHEET = 0
        PROJECT_LIST = 2
        TASK_LIST = 4
        ADMIN = 6
        REPORTS = 8
        SCHEDULE = 10

    End Enum

    Public Property Title As String
        Get
            Return ltlTitle.Text
        End Get
        Set(ByVal value As String)
            ltlTitle.Text = value
        End Set
    End Property

    Public Property SubTitle As String
        Get
            Return ltlSubTitle.Text
        End Get
        Set(ByVal value As String)
            ltlSubTitle.Text = value
        End Set
    End Property

    Public WriteOnly Property CurrentSetting As Setting
        Set(ByVal value As Setting)
            SetTitle(value)
        End Set
    End Property

    Private Sub SetTitle(ByVal currentSetting As Setting)

        Select Case currentSetting

            Case Setting.ADMIN
                ltlTitle.Text = "Admin"
                ltlSubTitle.Text = "Keeping you in control..."
            Case Setting.PROJECT_LIST
                ltlTitle.Text = "Project List"
                ltlSubTitle.Text = "An overview of what your working on..."
            Case Setting.REPORTS
                ltlTitle.Text = "Reports"
                ltlSubTitle.Text = "Knowledge is power..."
            Case Setting.SCHEDULE
                ltlTitle.Text = "Schedule"
                ltlSubTitle.Text = "Who's doing what..."
            Case Setting.TASK_LIST
                ltlTitle.Text = "Task List"
                ltlSubTitle.Text = "Pretty much everything you need to do..."
            Case Setting.TIMESHEET
                ltlTitle.Text = "Timesheet"
                ltlSubTitle.Text = "Delivering on time on budget..."
        End Select


    End Sub

End Class
