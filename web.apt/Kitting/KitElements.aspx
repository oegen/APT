<%@ Page Language="VB" AutoEventWireup="false" CodeFile="KitElements.aspx.vb" Inherits="Kitting_KitElements" 
MasterPageFile="~/MasterPages/MasterPage.master" Title="Kit Contents" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Kitting/KitContents.ascx" TagName="KitContent" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Kitting/KitItemListing.ascx" TagName="KitItemListing" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:subnavproject ID="ctrlSubNavProject" runat="server" SelectedItem="KITTING" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
               
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
                  
        <!--PROJECT LIST STARTS-->           
            <hr />

            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" 
                    TipTitle="Your Kit Contents" Description="Kit Contents..." />
                 
            <!--  GENERIC FORM -->                               
                <div class="aptform gen-form">

                    <div id="kit-contents-wrap">

                        <oegen:KitContent ID="ctrlKitContent" runat="server" />
                        <!-- kit content -->

                        <div id="kit-contents-group">
                            <oegen:KitItemListing ID="ctrlElementItemListing" runat="server" CurrentMode="ELEMENTS" />
                            <oegen:KitItemListing ID="ctrlBBCItemsListing" runat="server" CurrentMode="BBC_ITEM" />  
                            <oegen:KitItemListing ID="ctrlPremiumElementsListing" runat="server" CurrentMode="PREMIUM_ELEMENTS" LastColumn="true" />  
                        </div>
                                             
                        <div class="btn btn_orange thinbtn">
                            <asp:LinkButton ID="lnkSaveKitContents" runat="server" ValidationGroup="vldUser">Add selection to the kit contents table</asp:LinkButton><span></span>
                        </div>

                    </div>


                </div>                        
            <!--  END GENERIC FORM -->            
        <!--PROJECT LIST ENDS-->
        </div>             
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
