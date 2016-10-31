<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ContentHeader.ascx.vb" Inherits="Controls_Layout_ContentHeader" %>

<div class="title">
    <div class="title-nav">
        <h1><asp:Label ID="lblTitle" runat="server"></asp:Label> <span class="colorit"><asp:Label ID="lblSubTitle" runat="server"></asp:Label></span></h1>
        <p>
            <asp:Label ID="lblDescription" runat="server"></asp:Label>
            
            <asp:PlaceHolder ID="pnlMessageDisplay" runat="server" Visible="false">
                <span id="toggle"><a id="open" class="open" href="#" style="display: inline; ">[ Display Message <span class="open-message">&nbsp;</span> ]</a> 
			    <a id="close" style="display: none;" class="close" href="#">[ Close Message <span class="close-message">&nbsp;</span> ]</a></span>
            </asp:PlaceHolder>
        </p>
    </div>
</div>

