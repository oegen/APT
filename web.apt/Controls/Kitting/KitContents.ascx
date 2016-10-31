<%@ Control Language="VB" AutoEventWireup="false" CodeFile="KitContents.ascx.vb" Inherits="Controls_Kitting_KitContents" %>

<asp:PlaceHolder ID="plcKit" runat="server">

    <asp:PlaceHolder ID="plcKitDivs" runat="server" EnableViewState="false">
        <div class="kit-contents">                        
            <div class="kit-column-scroller">  
    </asp:PlaceHolder>

    <table>
        <caption id="captionKit" runat="server">Kit Contents</caption>
        <tr>
            <th>AIN / Code</th>
            <th>Element Type</th>
            <th>Subclass / Size</th>
            <th>Pack Size</th>
            <th>Supplier</th>
            <th>Cost / Item</th>
            <th>Expiry Date</th>
            <th id="thAction" runat="server">Action</th>
        </tr>   

    <asp:Repeater ID="rptrKitContent" runat="server">
    <ItemTemplate>
    <tr>
        <td>
            <asp:Literal ID="ltlAIN" runat="server"></asp:Literal>
        </td>
        <td>
            <asp:Literal ID="ltlType" runat="server"></asp:Literal>
        </td>
        <td>
            <asp:Literal ID="ltlSubclass" runat="server"></asp:Literal>
        </td>
        <td>
            <asp:Literal ID="ltlPackSize" runat="server"></asp:Literal>
        </td>
        <td>
            <asp:Literal ID="ltlSupplier" runat="server"></asp:Literal>
        </td>
        <td>
            <asp:Literal ID="ltlCost" runat="server"></asp:Literal>
        </td>
        <td>
            <asp:Literal ID="ltlExpiryDate" runat="server"></asp:Literal>
        </td>
        <td id="tdRemove" runat="server">
            <asp:LinkButton ID="lnkRemove" runat="server" OnCommand="lnkSetActivity_OnCommand">Remove</asp:LinkButton>
        </td>
    </tr> 
    </ItemTemplate>

    <FooterTemplate>
            </table>
    </FooterTemplate>
    </asp:Repeater>

    <asp:PlaceHolder ID="plcEndKitDivs" runat="server" EnableViewState="false">
                </div>
        </div>  
    </asp:PlaceHolder>

</asp:PlaceHolder>

<asp:PlaceHolder ID="plcEmpty" runat="server">
    No items have been added to this kit yet <br /><br />
</asp:PlaceHolder>




