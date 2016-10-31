<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BBCItemSearch.ascx.vb" Inherits="Controls_Generic_BBCItemSearch" %>


<asp:DropDownList ID="ddlSearchCriteria" runat="server" AutoPostBack="true">
    <asp:ListItem Value="0" Text="Part Number"></asp:ListItem>
    <asp:ListItem Value="1" Text="Brand"></asp:ListItem>
    <asp:ListItem Value="2" Text="Description"></asp:ListItem>
</asp:DropDownList>

<asp:TextBox ID="txtSearch" CssClass="brief-txt-box-last" runat="server"></asp:TextBox>
<asp:DropDownList ID="ddlBrands" runat="server" Visible="false"></asp:DropDownList>

<asp:RequiredFieldValidator ID="reqTextSearch" runat="server" ValidationGroup="vldBBCSearch" 
    ControlToValidate="txtSearch" ErrorMessage="Please enter something into a search field"></asp:RequiredFieldValidator>

<asp:LinkButton ID="lnkSearch" CssClass="lnkSearch" runat="server" ValidationGroup="vldBBCSearch">Search</asp:LinkButton>
<asp:Label ID="lblNoResults" runat="server" ForeColor="Red" Visible="false">There were no results available for the search specified.</asp:Label>

<asp:PlaceHolder ID="plcSearchResults" runat="server" Visible="false">
    <asp:GridView ID="gvSearchResults" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="PartNumber" HeaderText="Part Number" />
            <asp:BoundField DataField="BrandName" HeaderText="Brand Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkSelect" runat="server" OnCommand="lnkSelect_OnCommand">Select</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>    
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcNoResults" runat="server" Visible="false">
    No results could be found from the query
</asp:PlaceHolder>

