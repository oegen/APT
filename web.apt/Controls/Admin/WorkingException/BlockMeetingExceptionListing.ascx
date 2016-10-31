<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BlockMeetingExceptionListing.ascx.vb" Inherits="Controls_Admin_WorkingException_BlockMeetingExceptionListing" %>

<asp:PlaceHolder ID="plcBlockMeetingException" runat="server">
    <asp:GridView ID="grdvBlockMeetingException" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:TemplateField HeaderText="Start Date">
            <ItemTemplate>
                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Hours" HeaderText="Hours" />
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
<br /><br />
    No Block Meeting Exceptions have been added to the system yet
</asp:PlaceHolder>

