<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ElementTimeSheetListing.ascx.vb" Inherits="Controls_Timesheets_ElementTimeSheetListing" %>

<asp:Repeater ID="rptrElement" runat="server">
<HeaderTemplate>
<table class="joblisting">
    <tr>
        <th>Name</th>
        <th>Hours</th>
        <th>Mins</th>
        <th>Reason</th>
        <th>Actions</th>
    </tr>
</HeaderTemplate>
<ItemTemplate>
     <tr>
        <td>
            <span><asp:HiddenField ID="hdnElementId" runat="server" />
            <asp:Literal ID="ltlElementName" runat="server"></asp:Literal></span>  
        </td>
        <td>
            <asp:TextBox ID="txtHours" runat="server"></asp:TextBox> 
        </td>
        <td>
            <asp:TextBox ID="txtMinutes" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:DropDownList ID="ddlReason" runat="server" Width="100%"></asp:DropDownList>
        </td>
        <td style="width: 80px">
            <span><asp:LinkButton ID="lnkAutoFill" runat="server">Auto fill</asp:LinkButton></span>
        </td>
        <td id="tdError" runat="server" visible="false" valign="middle" style="">
            <asp:Label ID="lblError" runat="server" ForeColor="Red">*</asp:Label>
        </td>
    </tr>     
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>

<asp:Literal ID="ltlNoElements" runat="server" Visible="false">
    There are no elements in this project
</asp:Literal>

<div id="submit_btn" class="btn btn_orange thinbtn">
    <asp:LinkButton ID="lnkSave" runat="server">Save</asp:LinkButton><span></span>
</div>    

<asp:Label ID="ltlMessage" runat="server" ForeColor="Red"></asp:Label>
