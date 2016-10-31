<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectTags.ascx.vb" Inherits="Controls_Generic_ProjectTags" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen"  %>

<div class="tabswrap">

    <div id="tabs-inner" class="group">  

        <oegen:ContentTitle ID="ctrlContentTitle" runat="server" Title="Tags" ToolTipText="Project Tag Associations"></oegen:ContentTitle>
                 
            <div class="aptform gen-form">

                <asp:Repeater ID="rptrProjectTags" runat="server">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                            <li><asp:CheckBox ID="cbTag" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="checkbox_click" /></li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>

            </div>

    </div>
</div>
