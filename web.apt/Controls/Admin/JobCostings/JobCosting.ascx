<%@ Control Language="VB" AutoEventWireup="false" CodeFile="JobCosting.ascx.vb" Inherits="Controls_Admin_JobCostings_JobCosting" %>

<asp:Repeater runat="server" ID="rptTypes">
    <HeaderTemplate>
        <table class="joblisting">
            <tr>
                <th>Name</th>
                <th>Print Lead Time</th>
                <th>Hours</th>
                <th>Mins</th>
                <th>Avg. Time</th>
            </tr>
    </HeaderTemplate>
                                                            
    <ItemTemplate>
        <tr>
            <td class="table-subtitle" colspan="5"><asp:Label runat="server" ID="lblTypeName"></asp:Label></td>
        </tr>
        <asp:Repeater runat="server" ID="rptSubclassTypes" OnItemDataBound="rptSubclassTypes_ItemDataBound">                                                                       
            <ItemTemplate>
                <tr>
                    <td><asp:Label runat="server" ID="lblSubclassTypesName"></asp:Label></td>
                    <td>
                       <asp:TextBox ID="txtPrintLeadTime" runat="server"></asp:TextBox> 
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSubclassTypesTimeHours" Width="20"></asp:TextBox>  
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSubclassTypesTimeMinutes" Width="20"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblSubclassTypesAverage"></asp:Label>
                        <asp:HiddenField runat="server" ID="hdnSubclassTypesID" />
                        <asp:HiddenField runat="server" ID="hdnPrintLeadTime" />
                        <asp:HiddenField runat="server" ID="hdnSubclassTypesTimeHours" />
                        <asp:HiddenField runat="server" ID="hdnSubclassTypesTimeMinutes" />
                    </td>
                </tr>     
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
                                                                
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>

<div class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="jobcosting">Save</asp:LinkButton><span></span>
</div> 