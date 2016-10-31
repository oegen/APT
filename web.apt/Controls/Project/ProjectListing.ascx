<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectListing.ascx.vb" Inherits="Controls_Project_ProjectListing" %>
<%@ Register Src="~/Controls/Generic/Paging.ascx" TagPrefix="oegen" TagName="Paging" %>

<div id="apt-list" class="group">

    <asp:Repeater ID="rptrProjectList" runat="server">
    
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>

        <ItemTemplate>
                <li>
                    <span class="apt-list-title"><asp:Label ID="lblProjectName" runat="server"></asp:Label></span>
                    <span><asp:Label ID="lblAINNumber" runat="server"></asp:Label></span>
                    <label> - </label>
                    <span>Owner : <asp:Label ID="lblOwner" runat="server"></asp:Label></span>
                    <span class="apt-list-link"><asp:HyperLink ID="hypViewProject" runat="server">[ Select Project <span class="apt-list-arra">&nbsp;</span> ]</asp:HyperLink>
                </li>
        </ItemTemplate>

        <FooterTemplate>
            </ul>
        </FooterTemplate>

    </asp:Repeater>

    <oegen:Paging ID="ctrlPaging" runat="server" />

    <h2><asp:Label ID="lblNoResults" runat="server" Visible="false">None of your associated projects fulfill the search specifications.</asp:Label></h2>
     

</div>