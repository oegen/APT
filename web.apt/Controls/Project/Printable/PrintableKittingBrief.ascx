<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PrintableKittingBrief.ascx.vb" Inherits="Controls_Project_Printable_PrintableKittingBrief" %>
<%@ Register Src="~/Controls/Kitting/KitContents.ascx" TagName="KitContents" TagPrefix="oegen"  %>

<h1>Final Kitting Brief</h1>
<h2>
    Kitting Brief
</h2>

<oegen:KitContents ID="ctrlKitContents" runat="server" /><br />

Kits to be built by MDA: <asp:Literal ID="ltlKitBuiltByMDA" runat="server"></asp:Literal><br />

Kit Name: <asp:Literal ID="ltlKitName" runat="server"></asp:Literal><br />
Total Kit Quantity: <asp:Literal ID="ltlTotalKitQuantity" runat="server"></asp:Literal><br />
Kitting Costs: <asp:Literal ID="ltlKittingCosts" runat="server"></asp:Literal><br />

Funding: <asp:Literal ID="ltlFunding" runat="server"></asp:Literal><br />
Kit Stock Code: <asp:Literal ID="ltlKitStockCode" runat="server"></asp:Literal><br />
Total No of kits: <asp:Literal ID="ltlNoOfKits" runat="server"></asp:Literal><br />
Instructions: <asp:Literal ID="ltlInstructions" runat="server"></asp:Literal><br />

<h2>
    Dates
</h2>

Kits on Stock Date: <asp:Literal ID="ltlKitsOnStockDate" runat="server"></asp:Literal><br />

<asp:PlaceHolder ID="plcMdaChanges" runat="server" Visible="false">
    Proposal Requirement Date: <asp:Literal ID="ltlProposalReqDate" runat="server"></asp:Literal><br />
    Quote Provided Date: <asp:Literal ID="ltlQuoteProvidedDate" runat="server"></asp:Literal><br />
    Kit to Be Built Date: <asp:Literal ID="ltlKitToBeBuiltDate" runat="server"></asp:Literal><br />
    Delivery Date: <asp:Literal ID="ltlDeliveryDate" runat="server"></asp:Literal><br />
</asp:PlaceHolder>

In-Trade Date: <asp:Literal ID="ltlInTradeDate" runat="server"></asp:Literal><br />
Expiry Date: <asp:Literal ID="ltlExpiryDate" runat="server"></asp:Literal><br />