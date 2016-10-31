<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ContentTitle.ascx.vb" Inherits="Controls_Layout_ContentTitle" %>

<div id="form-title-wrap">
    <div class="form-title">
        <h2>
            <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
        </h2>
    </div>
    <asp:PlaceHolder ID="plcToolTip" runat="server" Visible="false">
        <div class="form-info-btn">
            <asp:HyperLink ID="hypToolTip" runat="server" CssClass="someClass">
                <asp:Image ID="imgToolTip" runat="server" ImageUrl="~/App_Themes/_default/images/form-info-btn.png" AlternateText="" />
            </asp:HyperLink>
        </div>
    </asp:PlaceHolder>
</div>