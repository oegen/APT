<%@ Application Language="VB" %>
<%@ Import Namespace="Oegen.Email" %>
<%@ Import Namespace="System.Web.Configuration.WebConfigurationManager" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        Dim instance As System.IO.FileInfo = New System.IO.FileInfo(Server.MapPath("~/web.config"))
        log4net.Config.XmlConfigurator.ConfigureAndWatch(instance)
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        
        ' TODO :- Send fatal error message to log manager
        ' TODO :- Send e-mail with exception information
        
        Dim ErrorMessage As String
        Dim ErrorPath As String = Request.RawUrl
        ErrorMessage = "Error path:" & ErrorPath
        ErrorMessage = ErrorMessage & vbCrLf & vbCrLf & _
          "The error description is as follows : " & Server.GetLastError.ToString
              
        SendEmail(AppSettings("SenderAddress"), AppSettings("ErrorEmailAddress"), _
                             "ERROR - APT", ErrorMessage, , , , , , , AppSettings("SmptServer"))
        
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        ' OK, so what happens is we need to check whether a user is logged in on every single page
        ' but this couldn't be done in the secure folder simply because of the awkwardity that is
        ' the fact that people can be 'auto-logged' in via LDAP.
        
        
    End Sub
    
</script>