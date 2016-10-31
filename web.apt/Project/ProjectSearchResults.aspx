<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectSearchResults.aspx.vb" Inherits="Project_ProjectSearchResults" Title="Project Search Results" MasterPageFile="~/MasterPages/MasterPage.master" %>
<%@ Register Src="~/Controls/Project/ProjectSearchResults.ascx" TagName="ProjectSearchResults" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="maincontent" class="group menu-closed">
                
        <!--CONTENT STARTS-->                  
        <div id="content" class="group">
         
            <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="PROJECT_LIST" />
          
            <!--PROJECT LIST STARTS-->
            <div id="main-content-area" class="group">

                <div id="section_wrap">

                    <div id="gen-inner">
                        <oegen:ContentTitle ID="ctrlContentTitle" runat="server" Title="Your Projects..." ToolTipText="Your Projects form description..."></oegen:ContentTitle>
                        <oegen:ProjectSearchResults ID="ctrlProjectSearchResults" runat="server" />
                    </div>
                    
                </div>
          
            </div>  
            <!--PROJECT LIST ENDS-->         
        </div>
        <!--CONTENT ENDS-->
        
    </div>

</asp:Content>