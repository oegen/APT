<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PrintableElement.ascx.vb" Inherits="Controls_Element_Printable_PrintableElement" %>

<h1>
    <asp:Literal ID="ltlName" runat="server"></asp:Literal> Details
</h1>
Type: <asp:Literal ID="ltlType" runat="server"></asp:Literal><br />
Subclass Type: <asp:Literal ID="ltlSubclassType" runat="server"></asp:Literal><br />
Description: <asp:Literal ID="ltlDescription" runat="server"></asp:Literal><br />
Trade: <asp:Literal ID="ltlTrade" runat="server"></asp:Literal><br />
T&Cs: <asp:Literal ID="ltlTCs" runat="server"></asp:Literal><br />
Brands: <asp:Literal ID="ltlBrands" runat="server"></asp:Literal><br />
Page: <asp:Literal ID="ltlPage" runat="server"></asp:Literal><br />
<!--
Quantity: <asp:Literal ID="ltlQuantity" runat="server"></asp:Literal><br />
-->
No of Del Addresses: <asp:Literal ID="ltlNoAddress" runat="server"></asp:Literal><br />
Delivery Details: <asp:Literal ID="ltlDeliveryDetails" runat="server"></asp:Literal><br />
Artwork Req'd Date: <asp:Literal ID="ltlArtworkDelivery" runat="server"></asp:Literal><br />

<h2>
    Element Estimated Costings
</h2>

Quantity: <asp:Literal ID="ltlCostingQuantity" runat="server"></asp:Literal><br />
Artwork Time: <asp:Literal ID="ltlArtworkTime" runat="server"></asp:Literal><br />
Artwork Cost: <asp:Literal ID="ltlArtworkCost" runat="server"></asp:Literal><br />
Print Lead Times: <asp:Literal ID="ltlPrintLeadTimes" runat="server"></asp:Literal><br />
Print Cost: <asp:Literal ID="ltlCostingPrintCost" runat="server"></asp:Literal><br />
Cost per Item: <asp:Literal ID="ltlCostPerItem" runat="server"></asp:Literal><br />

<h2>
    BD Print Brief
</h2>

No of Colours: <asp:Literal ID="ltlNoOfColours" runat="server"></asp:Literal><br />
Finished Size: <asp:Literal ID="ltlFinishedSize" runat="server"></asp:Literal><br />
Material: <asp:Literal ID="ltlMaterial" runat="server"></asp:Literal><br />
Finishing: <asp:Literal ID="ltlFinishing" runat="server"></asp:Literal><br />
No Of Del adds: <asp:Literal ID="ltlDelAdd" runat="server"></asp:Literal><br />
Delivery Details: <asp:Literal ID="ltlBDPrintDeliveryDetails" runat="server"></asp:Literal><br />
Pack Size: <asp:Literal ID="ltlPackSize" runat="server"></asp:Literal><br />
<h2>
    BD Kitting Brief
</h2>

Cost per item: <asp:Literal ID="ltlKittingCostPerItem" runat="server"></asp:Literal><br />
Expiry Date: <asp:Literal ID="ltlExpiryDate" runat="server"></asp:Literal><br />
PO Number: <asp:Literal ID="ltlPONumber" runat="server"></asp:Literal><br />
Existing or New: <asp:Literal ID="ltlExistingNew" runat="server"></asp:Literal><br />
Print Supplier: <asp:Literal ID="ltlPrintSupplier" runat="server"></asp:Literal><br />
Due Date into MDA: <asp:Literal ID="ltlDueDateMDA" runat="server"></asp:Literal>