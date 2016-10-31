<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectElement.aspx.vb" Inherits="Elements_ProjectElement" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPages/TabPageWithoutAjax.master" Title="Project Element" %>
<%@ Register Src="~/Controls/Element/Element.ascx" TagPrefix="oegen" TagName="Element" %>
<%@ Register Src="~/Controls/Generic/ElementListing.ascx" TagPrefix="oegen" TagName="ElementListing" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagPrefix="oegen" TagName="ContentTitle" %>
<%@ Register Src="~/Controls/Element/ElementArtworkDetails.ascx" TagPrefix="oegen" TagName="ElementArtworkDetails" %>
<%@ Register Src="~/Controls/Element/ElementKittingDetails.ascx" TagPrefix="oegen" TagName="ElementKittingDetails" %>
<%@ MasterType VirtualPath="~/MasterPages/TabPageWithoutAjax.master" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div class="tabswrap">

    <div id="tabs-inner" class="group">  

        <span class="tabs-titles">
        <oegen:ContentTitle ID="ctrlElementTitle" runat="server" ToolTipText="Enter the required element information" 
            Title="Add your new element" /></span>

        <!-- TODO: Validation Summary
        <div id="error-message">
            <h3>There are some errors with the form, please correct them below.</h3>
            <ul>
                <li><span>Full Name or Organisation can't be empty</span></li>
                <li><span>Full Name or Oragnisation is too short</span></li>
                <li><span>Forum/Display Name can't be empty</span></li>
            </ul>
        </div>
        -->
        
        <!--ACCORDIAN STARTS-->
        <div id="accordion">
           
            <h3><a href="#">Artwork & Printed Item Brief</a></h3>       
            <oegen:Element ID="ctrlElement" runat="server" />
            
            <asp:PlaceHolder ID="plcBD" runat="server" Visible="false">
                <h3><a href="#">BD (Print)</a></h3>
                <div>
                    <oegen:ElementArtworkDetails ID="ctrlElementArtworkDetails" runat="server" />
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="plcMDA" runat="server" Visible="false">
                <h3><a href="#">BD (Kitting)</a></h3>
                <div>
                    <oegen:ElementKittingDetails ID="ctrlElementKittingDetails" runat="server" />
                </div>
            </asp:PlaceHolder>

        </div>
        <!--ACCORDIAN ENDS-->
        
        <!--ELEMENTS NAV STARTS-->                 
        <oegen:ElementListing ID="ctrlElementListing" runat="server" />
        <!--ELEMENTS NAV ENDS--> 
    
    </div>                             
</div>

</asp:Content> 

