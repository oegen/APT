<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintableElement.aspx.vb" Inherits="Elements_PrintableElement" %>
<%@ Register Src="~/Controls/Element/Printable/PrintableElement.ascx" TagPrefix="oegen" TagName="PrintableElement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <oegen:PrintableElement ID="ctrlPrintElement" runat="server" />
    </div>
    </form>
</body>
</html>
