<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="Project_Dashboard" Title="Dashboard"
MasterPageFile="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>

<asp:Content ID="content" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div class="tabswrap">

    <div id="tabs-inner" class="group">  

        <span class="tabs-titles">
            <oegen:ContentTitle ID="ctrlGenericContentTitle" runat="server" ToolTipText="Project Details" Title="Project summary" />
        </span>

        <div class="dashboard">
        
            <table>
                <caption>Core details</caption>
                <tbody>
                    <tr>
                        <td class="detail">AIN No:</td>
                        <td>
                            <asp:Literal ID="ltlAIN" runat="server">100552268</asp:Literal>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="detail">Project title:</td>
                        <td>
                            <asp:Literal ID="ltlProjectTitle" runat="server"> Magners IOT Distribution drive</asp:Literal>
                       </td>
                    </tr>                    
                    <tr>
                        <td class="detail">Brand</td>
                        <td>
                            <asp:Literal ID="ltlBrand" runat="server">Magners draught</asp:Literal>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="detail">Description/Outline of campaign:</td>
                        <td>
                            <asp:Literal ID="ltlDesc" runat="server">3 kits - install and special prices. 2,500 outlets</asp:Literal>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="detail">Theme/Tag:</td>
                        <td>
                            <asp:Literal ID="ltlThemeTag" runat="server">Magners Draught</asp:Literal>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="detail">Budget code:</td>
                        <td>
                            <asp:Literal ID="ltlBudgetCode" runat="server">4254545453 354564 4531</asp:Literal>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="detail">Storage code (if different from budget code):</td>
                        <td>
                            <asp:Literal ID="ltlStorageCode" runat="server">4254545453 354564 4531</asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </table>            
            
            <table class="dashboard">
                <caption>Dates</caption>
                <tbody>
                    <tr>
                        <td class="detail">Brief submitted date:</td>
                        <td>
                            <asp:Literal ID="ltlBriefSubmitDate" runat="server">25/06/2010</asp:Literal>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="detail">In-trade date:</td>
                        <td>
                            <asp:Literal ID="ltlInTradeDate" runat="server">24/10/2010</asp:Literal>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="detail">No. of weeks</td>
                        <td>
                            <asp:Literal ID="ltlNoOfWeeks" runat="server">17</asp:Literal>
                        </td>
                    </tr>                
                    <tr>
                        <td class="detail">Campaign end date:</td>
                        <td>
                            <asp:Literal ID="ltlCampaignEndDate" runat="server">17</asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </table>            
            
            <table class="dashboard">
                <caption>Requirements</caption>
                <thead>
                    <tr>
                        <th colspan="2">Studio (type of work envisaged):</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="detail">Print:</td>
                        <td><asp:Literal ID="ltlPrintRequirement" runat="server">Yes</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">Kitting:</td>
                        <td><asp:Literal ID="ltlKittingRequirement" runat="server">No</asp:Literal></td>
                    </tr>                   
                    <tr>
                        <td class="detail">Premiums:</td>
                        <td><asp:Literal ID="ltlPremiumRequirement" runat="server">No</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">BBC items:</td>
                        <td><asp:Literal ID="ltlBBCRequirement" runat="server">Yes</asp:Literal></td>
                    </tr>
                </tbody>
            </table>     
            
            <table class="dashboard">
                <caption>Quotes</caption>
                <thead>
                    <tr>
                        <th></th>
                        <th>Estimate</th>
                        <th>Final</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="detail">Print:</td>
                        <td><asp:Literal ID="ltlEstPrint" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlFinalPrint" runat="server">£500.00</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">Kitting:</td>
                        <td><asp:Literal ID="ltlEstKitting" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlFinalKitting" runat="server">£500.00</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">Premiums:</td>
                        <td><asp:Literal ID="ltlEstPremium" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlFinalPremium" runat="server">£500.00</asp:Literal></td>
                    </tr>
                </tbody>
            </table>            


            <table class="dashboard">
                <caption>Key players</caption>
                <tbody>
                    <tr>
                        <td class="detail">Project Owner:</td>
                        <td><asp:Literal ID="ltlProjectOwner" runat="server">Cooling Brewery</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">Project Approver:</td>
                        <td><asp:Literal ID="ltlProjectApprover" runat="server">Tommy Lied</asp:Literal></td>
                    </tr>                   
                    <tr>
                        <td class="detail">Studio QA:</td>
                        <td><asp:Literal ID="ltlStudioQA" runat="server">Lloydy P</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">Legal Approver:</td>
                        <td><asp:Literal ID="ltlLegalApprover" runat="server">Robin 'the Judge' Smith</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">PO Raiser:</td>
                        <td><asp:Literal ID="ltlPORaiser" runat="server">Paul Verdon</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">Williams Lea Project Owner:</td>
                        <td><asp:Literal ID="ltlWLeaProjectOwner" runat="server">William Lea</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">MDA Procurement Project Owner:</td>
                        <td><asp:Literal ID="ltlMDAProjectOwner" runat="server">Fred Funk</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">MDA Kitting Project Owner:</td>
                        <td><asp:Literal ID="ltlKittingProjectOwner" runat="server">Kitman Do</asp:Literal></td>
                    </tr>
                </tbody>
            </table>

            <table class="dashboard">
                <caption>Key Timings</caption>
                <thead>
                    <tr>
                        <th></th>
                        <th>Projected</th>
                        <th>Actual</th>
                        <th>Deviance/Indicator</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="detail">Partners provide costs</td>
                        <td><asp:Literal ID="ltlPartnerProvProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlPartnerProvEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlPartnerProvDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>                    
                    <tr>
                        <td class="detail">Present final costs & final scope of work</td>
                        <td><asp:Literal ID="ltlFinalCostsProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlFinalCostsEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlFinalCostsDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>                      
                    <tr>
                        <td class="detail">Client approves costs and scope of work</td>
                        <td><asp:Literal ID="ltlClientCostsProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlClientCostsEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlClientCostsDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>                      
                    <tr>
                        <td class="detail">Finalised design templates to studio</td>
                        <td><asp:Literal ID="ltlDesignTempProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlDesignTempEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlDesignTempDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>  
                    <tr>
                        <td class="detail">Artwork time in Graphics Studio</td>
                        <td><asp:Literal ID="ltlArtworkStudioProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlArtworkStudioEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlArtworkStudioDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>  
                    <tr>
                        <td class="detail">Artwork final approval</td>
                        <td><asp:Literal ID="ltlArtworkApprovalProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlArtworkApprovalEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlArtworkApprovalDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>
                    <tr>
                        <td class="detail">Print production</td>
                        <td><asp:Literal ID="ltlPrintProductionProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlPrintProductionEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlPrintProductionDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>
                    <tr>
                        <td class="detail">Booking In</td>
                        <td><asp:Literal ID="ltlBookingInProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlBookingInEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlBookingInDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>
                    <tr>
                        <td class="detail">Kitting / Packing/ Despatch</td>
                        <td><asp:Literal ID="ltlKittingProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlKittingEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlKittingDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>
                    <tr>
                        <td class="detail">Live</td>
                        <td><asp:Literal ID="ltlLiveProjected" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlLiveEstimated" runat="server">£500.00</asp:Literal></td>
                        <td><asp:Literal ID="ltlLiveDeviance" runat="server">£500.00</asp:Literal></td>
                    </tr>
                </tbody>
            </table>


        </div>

    </div>
</div>
</asp:Content>
