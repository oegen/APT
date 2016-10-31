<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintableProjectInfo.aspx.vb" Inherits="Project_Printable_PrintableProjectInfo" %>
<%@ Register Src="~/Controls/Project/Printable/PrintableProjectInfo.ascx" TagPrefix="oegen" TagName="PrintableProjectInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <oegen:PrintableProjectInfo ID="ctrlPrintableProjectInfo" runat="server" />
    </div>
    </form>
</body>
</html>
