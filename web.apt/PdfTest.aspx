<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PdfTest.aspx.vb" Inherits="PdfTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PlaceHolder runat="server" ID="plcBindingFix">
        <script type="text/javascript" language="javascript" src="<%#ResolveUrl("~/js/deployJava.js")%>"></script>
        <script type="text/javascript" language="javascript">  
              
            var parameters = {
                OpenURL: "<%#OpenURL%>",
                SaveURL: "SavePdf.aspx",
                PrintVisible: "false",
                OpenVisible: "false", 
                java_arguments: "-Xmx256m"
            };

            var attributes = {
                archive: "<%#ResolveUrl("~/pdfnotes/webnotes.jar")%>,<%#ResolveUrl("~/pdfnotes/jPDFNotesS.jar")%>,<%#ResolveUrl("~/pdfnotes/cmykProfileS.jar")%>",
                code: "qoppa.webNotes.SaveToWeb",
                width: "100%",
                Height: "850", 
                name: "jPDFNotes"
            };

            var version = "1.6.0_10";
            deployJava.runApplet(attributes, parameters, version);
       
        </script>
        </asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
