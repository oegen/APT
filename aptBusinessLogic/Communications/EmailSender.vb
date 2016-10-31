'----------------------------------------------------------------------------------------------
' Filename    : EmailSender.vb
' Description : E-mail sender, a module developed as a library for all email sending.
'               Any e-mails that are sent throughout the application should be placed in here.
'
' Release Initials  Date        Comment
' 1       LP        07/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports System.Net.Mail
Imports Oegen.Email
Imports aptEntities
Imports System.Text
Imports GenericUtilities

Public Module EmailSender

#Region "Generic Email Sender"

    Public Function AptSendEmail(ByVal fromAddress As String, ByVal toAddress As String, ByVal message As String, _
                              Optional ByVal subject As String = "", Optional ByVal smtpServer As String = "localhost", _
                              Optional ByVal emailTemplate As String = "", Optional ByVal attachment As String = "", _
                              Optional ByVal cc As String = "", Optional ByVal bcc As String = "") As Boolean

        If (String.IsNullOrEmpty(AppSettingsGet.TestEmail) = False) Then
            ' Use the test email to override (Used for testing)
            toAddress = AppSettingsGet.TestEmail
        End If

        Return Not Oegen.Email.SendEmail(fromAddress, toAddress, subject, message, , , , , , , AppSettingsGet.SmptServer)

    End Function

#End Region

#Region "Project Emails"

    Public Function SendRegistrationEmail(ByVal userRegistered As AptUser) As Boolean
        Dim emailMessage As New StringBuilder

        emailMessage.AppendLine("Hello! I still need to be implemented!")

        Throw New NotImplementedException("SendRegistrationEmail is not implemented")

        ' Return SendEmail(appSetting for from address, users email, message etc etc.)
        Return False
    End Function

    Public Function SendTaskToCompleteEmail(ByVal userToMail As AptUser,
                                            ByVal tokenId As Integer,
                                            Optional ByVal emailAddress As String = "") As Boolean
        Dim emailMessage As New StringBuilder
        Dim newToken As Token = WorkflowManager.GetTokenByID(tokenId)
        Dim transition As Transition = WorkflowManager.GetTransitionByToken(newToken)
        Dim taskToComplete As Task = transition.Task
        Dim subjectStr As String = String.Format("AIN No: {0} - APT - Task Completion - {1}", newToken.Project.ID, taskToComplete.Name)
        Dim requiredPrintDate As String = String.Empty
        If Not newToken.Project.RequiredPrintDate Is Nothing AndAlso _
            (transition.ID = AppSettingsGet.ReserveTimeTransitionBDID OrElse _
             transition.ID = AppSettingsGet.ReserveTimeTransitionNonBDID) Then
            requiredPrintDate = newToken.Project.RequiredPrintDate.Value.ToString("dd/MM/yyyy")
        End If

        If userToMail IsNot Nothing Then
            ' Only add this if we don't email to a specific address as we don't have the information
            emailMessage.AppendLine(String.Format("Dear {0},", userToMail.FullName))
            GetCommonTaskEmailContent(emailMessage, transition, newToken.Project, requiredPrintDate)
            modLogManager.Info(String.Format("E-mail sent to {0} (ID = {1}) with subject {2}", userToMail.FullName, userToMail.ID, subjectStr))

            ' Send email return false when successful and true when un-successful!
            Return AptSendEmail(AppSettingsGet.SenderAddress, userToMail.EmailAddress,
                                emailMessage.ToString(), subjectStr, AppSettingsGet.SmptServer,
                                , , , )

            'Return Not Oegen.Email.SendEmail(AppSettingsGet.SenderAddress, userToMail.EmailAddress, subjectStr, emailMessage.ToString(), , , , , , , AppSettingsGet.SmptServer)
        Else
            GetCommonTaskEmailContent(emailMessage, transition, newToken.Project, requiredPrintDate)
            Return AptSendEmail(AppSettingsGet.SenderAddress, emailAddress,
                                emailMessage.ToString(), subjectStr, AppSettingsGet.SmptServer,
                                , , , )
        End If

        Return False
    End Function

    Public Sub GetCommonTaskEmailContent(ByVal emailMessage As StringBuilder, ByVal transition As Transition, ByRef project As Project, ByVal requiredPrintDate As String)

        emailMessage.AppendLine()
        emailMessage.AppendLine(String.Format("AIN No: {0} - A new task has been added to the project {1} that requires your attention.", project.ID, project.Name))
        emailMessage.AppendLine()
        emailMessage.AppendLine(String.Format("Task Name : {0}", transition.Task.Name))
        emailMessage.AppendLine(String.Format("Task Description : {0}", transition.Task.Description))
        If Not requiredPrintDate = "" Then
            emailMessage.AppendLine(String.Format("Print Required Delivery Date : {0}", requiredPrintDate))
        End If
        emailMessage.AppendLine()
        emailMessage.AppendLine(String.Format("Log in to {0} to see the task and provide the relevant data for completion.", AppSettingsGet.SiteURL))

    End Sub

#End Region

End Module
