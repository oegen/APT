<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ReservedTimeListing.ascx.vb" Inherits="Controls_Reserve_Time_ViewReservedTime" %>

<asp:PlaceHolder ID="plcReserveTime" runat="server">
    <asp:GridView ID="gvReservedTimes" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Week Number">
                <ItemTemplate>
                    <asp:Label ID="lblWeekNum" runat="server" CssClass="nameLink"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Duration">
                <ItemTemplate>
                    <asp:Literal ID="ltlDuration" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Number of Artworkers">
                <ItemTemplate>
                    <asp:Literal ID="ltlNumArtworkers" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Reserved By">
                <ItemTemplate>
                    <asp:Literal ID="ltlReservedBy" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcError" runat="server" Visible="false">
    There is no time reserved for this project.
</asp:PlaceHolder>
