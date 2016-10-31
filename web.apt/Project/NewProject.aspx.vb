'----------------------------------------------------------------------------------------------
' Filename    : NewProject.aspx.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP        25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports aptEntities
Imports aptBusinessLogic

Partial Class Project_NewProject
    Inherits System.Web.UI.Page

    Private property IsBdProject As Boolean
        Get
            Return cbBDProject.Checked
        End Get
        Set(ByVal value As Boolean)
            If value = True Then
                liAvailableBudget.Visible = True
            Else
                liAvailableBudget.Visible = False
            End If
        End Set
    End Property

#Region "Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        plcBindingFix.DataBind()
        If Page.IsPostBack = False Then
            SetupPage()
        End If
    End Sub

    Protected Sub lnkCreateBrief_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCreateBrief.Click
        If Page.IsValid = True Then
            Dim projectId As Integer

            SaveProject(projectId)

            Response.Redirect(String.Format("~/Project/ProjectDetails.aspx?projectId={0}", projectId))
        End If
    End Sub

    Protected Sub lnkSaveProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSaveProject.Click
        If Page.IsValid = True Then
            SaveProject()

            Response.Redirect("~/Project/ProjectListing.aspx")
        End If
    End Sub

    Protected Sub vldOwner_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles vldOwner.ServerValidate
        If ctrlUserSearchListing.SelectedUserId = 0 Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub
#End Region

#Region "Private Implementation"
    Private Sub SetupPage()
        ' Check if user is logged in and user access
        If SessionManager.LoggedInUserId = 0 Then
            Response.Redirect("~/Login.aspx")
        End If

        Initialise()
    End Sub

    Private Sub Initialise()
        IsBdProject = cbBDProject.Checked
    End Sub

    Private Sub SaveProject(Optional ByRef projectId As Integer = 0)
        Dim newProject As New Project()
        Dim userId As Integer = ctrlUserSearchListing.SelectedUserId
        Dim budget As Decimal = 0.0


        ' get userId from user selection control
        newProject.Name = txtProjectName.Text

        newProject.RequiredDate = Date.Parse(txtRequiredDate.Text).ToString("dd/MM/yyyy")

        newProject.RequiredPrintDate = Date.Parse(txtRequiredDate.Text).ToString("dd/MM/yyyy")
        newProject.RequiredPrintDate = newProject.RequiredPrintDate.Value.AddDays(-10) ' Requirement to have the required print day be the required date -10

        newProject.Stopped = False
        newProject.Active = True
        newProject.IsBDProject = cbBDProject.Checked


        If cbBDProject.Checked = True Then
            Decimal.TryParse(txtAvailableBudget.Text, budget)
            newProject.PrintRequired = True
            newProject.AvailableBudget = budget
        End If

        ProjectManager.AddNewProject(newProject, userId, SessionManager.LoggedInUserId)

        projectId = newProject.ID
    End Sub

    Protected Sub cbBDProject_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbBDProject.CheckedChanged

        lblWarning.Text = ""
        IsBdProject = cbBDProject.Checked

        If cbBDProject.Checked Then
            CheckLessThanTwentyWeeks()
        End If

        divSaveProject.Visible = cbBDProject.Checked

    End Sub

    Private Sub CheckLessThanTwentyWeeks()

        If String.IsNullOrEmpty(txtRequiredDate.Text) = False Then
            Dim twentyWeeksInMonths = 5
            Dim latestPossibleDate As Date = Today.AddMonths(twentyWeeksInMonths)
            Dim selectDate As Date = Date.Parse(txtRequiredDate.Text)

            If selectDate < latestPossibleDate Then
                lblWarning.Text = "The In trade date is set to a date that is less than twenty weeks away, completion of this project may not be possible"
            End If
        End If

    End Sub

#End Region

End Class
