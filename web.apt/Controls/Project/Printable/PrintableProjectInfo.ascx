<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PrintableProjectInfo.ascx.vb" Inherits="Controls_Project_Printable_PrintableProjectInfo" %>
<%@ Register Src="~/Controls/Element/Printable/PrintableElement.ascx" TagPrefix="oegen" TagName="PrintableElement" %>

<h1>
    <asp:Literal ID="ltlProject" runat="server"></asp:Literal>
</h1>

SAP Codes: <asp:Literal ID="ltlSapCodes" runat="server"></asp:Literal><br />
In-trade Date: <asp:Literal ID="ltlInTradeDate" runat="server"></asp:Literal><br />
Required Print Delivery Date: <asp:Literal ID="ltlReqPrintDelDate" runat="server"></asp:Literal><br />
Project Owner: <asp:Literal ID="ltlProjectOwner" runat="server"></asp:Literal><br />
PO Raiser: <asp:Literal ID="ltlPoRaiser" runat="server"></asp:Literal><br />
Legal Approver <asp:Literal ID="ltlLegalApprover" runat="server"></asp:Literal><br />
Studio QA: <asp:Literal ID="ltlStudioQA" runat="server"></asp:Literal><br />
Coordinator: <asp:Literal ID="ltlCoordinator" runat="server"></asp:Literal><br />
Brand List: <asp:Literal ID="ltlBrand" runat="server"></asp:Literal><br />
Type of Work: <asp:Literal ID="ltlTypeOfWork" runat="server"></asp:Literal><br />
Business Area: <asp:Literal ID="ltlBusinessArea" runat="server"></asp:Literal><br />
W/Lea I - Media Number <asp:Literal ID="ltlWLeaMediaNumber" runat="server"></asp:Literal><br />

<asp:PlaceHolder ID="plcElements" runat="server">

    <h2>Element Details</h2>

    <asp:Repeater ID="rptrElements" runat="server">
        <ItemTemplate>
            <oegen:PrintableElement ID="ctrlPrintableElement" runat="server" /> 
        </ItemTemplate>
    </asp:Repeater>
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcNoElements" runat="server">
    No elements have been added to this project yet
</asp:PlaceHolder>
