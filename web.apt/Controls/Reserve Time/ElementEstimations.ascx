<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ElementEstimations.ascx.vb" Inherits="Controls_Reserve_Time_ElementEstimations" %>
        
        
<div id="estimations-nav">

    <h2>Element Estimations</h2>
    <ul>
    <asp:Repeater ID="rptrElementEstimations" runat="server">

    <ItemTemplate>
    
        <li id="liElement" runat="server">
            <asp:Label ID="lblName" runat="server"></asp:Label>
            <asp:Label ID="lblDuration" runat="server"></asp:Label>
        </li>

    </ItemTemplate>

    </asp:Repeater>

        <li>
            <asp:Label ID="Label1" runat="server">Total Time</asp:Label>
            <asp:Label ID="lblTotalTime" runat="server"></asp:Label>
        </li>
    </ul>

    <div id="divAddNewElement" runat="server" class="right-btn btn_orange">
        <asp:LinkButton ID="lnkUseTotalTime" runat="server" CssClass="total-time">Use total time</asp:LinkButton><span></span>
    </div>

</div>
