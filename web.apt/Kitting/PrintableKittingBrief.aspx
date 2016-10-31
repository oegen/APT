<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintableKittingBrief.aspx.vb" Inherits="Kitting_PrintableKittingBrief" %>
<%@ Register Src="~/Controls/Project/Printable/PrintableKittingBrief.ascx" TagPrefix="oegen" TagName="PrintableKittingBrief" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <oegen:PrintableKittingBrief ID="ctrlPrintableKittingBrief" runat="server"></oegen:PrintableKittingBrief>
    </div>
    </form>
</body>
</html>
