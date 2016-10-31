<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectBBCItems.aspx.vb" Inherits="Elements_ProjectBBCItems" Title="Project BBC Items"
MasterPageFile="~/MasterPages/MasterPage.master" %>

<%@ Register Src="~/Controls/Element/ProjectBBCItem.ascx" TagName="BBCItem" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:SubNavProject ID="ctrlSubNavProject" runat="server" SelectedItem="BBC_ITEMS"/>
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
        <!--PROJECT LIST STARTS-->
           
            <hr />

            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" TipTitle="BBC Items" Description="BBC Items......" />
                 
                <!--  GENERIC FORM -->
                                            
 
                    <div class="aptform gen-form">
                        <oegen:BBCItem ID="ctrlBBCItem" runat="server" />
                    </div>               
          
                <!--  END GENERIC FORM --> 
             
           </div>

        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>