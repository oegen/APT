<%@ Control Language="VB" AutoEventWireup="false" CodeFile="KitItemListing.ascx.vb" Inherits="Controls_Kitting_KitItemListing" %>

<asp:PlaceHolder ID="plcItemListing" runat="server" Visible="false">
    <div id="divListing" runat="server" class="elements-contents">
        <table>
                <caption>
                    <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
                </caption>
                <tr>
                    <th>
                        <asp:Literal ID="ltlName" runat="server">Name</asp:Literal>
                    </th>
                    <th class="no-border">Quantity</th>
                </tr>    
            <asp:Repeater ID="rptrItemList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <label id="lblName" runat="server">Name of Element here</label>
                            <asp:HiddenField ID="hidItemId" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hidOldQuantity" runat="server" />
                        </td> 
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:PlaceHolder>

