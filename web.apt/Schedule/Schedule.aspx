<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Schedule.aspx.vb" Inherits="Schedule_Schedule"  MasterPageFile="~/MasterPages/MasterPage.master" Title="Schedule" %>
<%@ Register Src="~/Controls/Navigation/SubNavEmpty.ascx" TagName="SubNav" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Schedule/Schedule.ascx" TagName="Schedule" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagName="DatePicker" TagPrefix="oegen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="maincontent" class="group menu-closed">
    
        <!-- Sub Nav -->
        <oegen:SubNav ID="ctrlSubNav" runat="server" />
     
        <!--CONTENT STARTS-->
        <div id="content" class="group">
                    
            <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="SCHEDULE" Title="Weekly Schedule" />
                          
            <!--LIST STARTS-->
            <div id="main-content-area" class="group">

                <div id="section-wrap">

                    <hr />

                    <div id="gen-inner">

                        <oegen:DatePicker ID="dtpSelectedWeek" runat="server" LabelText="Week Date" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" />

                        <!--<div class="refresh">
                            <asp:LinkButton ID="lnkRefresh" runat="server"></asp:LinkButton>
                        </div>-->

                        <!-- need more space here -->

                        <div class="aptform gen-form">
                        
                            <oegen:Schedule ID="ctrlSchedule" runat="server" />

                        </div>

                    </div>

                </div>
                            
            </div>
            <!--LIST ENDS-->
                    
        </div>
        <!--CONTENT ENDS-->
                    
    </div>

</asp:Content>