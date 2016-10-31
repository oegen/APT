<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WorkingWeekExceptionListing.ascx.vb" Inherits="Controls_Admin_WorkingException_WorkingWeekExceptionListing" %>

<asp:PlaceHolder ID="plcWorkingWeekException" runat="server">
    <asp:GridView ID="grdvWorkingWeekException" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:TemplateField HeaderText="Username">
            <ItemTemplate>
                <asp:Label ID="lblUsername" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Hours" HeaderText="Hours" />
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:hyperlink ID="hypEdit" runat="server">Edit</asp:hyperlink>
                <asp:LinkButton ID="lnkDelete" runat="server" OnCommand="lnkSetActivity_OnCommand">Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>    
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcEmpty" runat="server">
    <br />
    No Working Week Exceptions have been added in the system yet
</asp:PlaceHolder>

