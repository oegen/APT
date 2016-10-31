<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Node.aspx.vb" Inherits="Admin_Lists_Node" 
MasterPageFile="~/MasterPages/Admin.master" Title="Add / Edit Node" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Lists/Node.ascx" TagName="Node" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="LIST_MANAGER"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Add/Edit Node" Description="List Item" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <asp:HyperLink CssClass="adminLink" ID="hypAddNode" runat="server">back to list nodes</asp:HyperLink><br />
                    <oegen:Node ID="ctrlNode" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS--> 
            </div>              
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>