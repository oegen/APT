<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectSchedule.ascx.vb" Inherits="Controls_Schedule_ProjectSchedule" %>

<asp:PlaceHolder ID="plcReservedTime" runat="server">
   <asp:GridView ID="gvReservedTimes" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:TemplateField HeaderText="Project">
            <ItemTemplate>
                <asp:Literal ID="ltlAIN" runat="server"></asp:Literal> -
                <asp:HyperLink ID="lnkProject" runat="server" CssClass="nameLink"></asp:HyperLink>  
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
        <asp:TemplateField HeaderText="Avg. Hours per Artworker">
            <ItemTemplate>
                <asp:Literal ID="ltlArtworkerAvg" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Reserved By">
            <ItemTemplate>
                <asp:Literal ID="ltlReservedBy" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView> 
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcErrorMessage" runat="server" Visible="false">
    There is no time reserved for this project.
</asp:PlaceHolder>
