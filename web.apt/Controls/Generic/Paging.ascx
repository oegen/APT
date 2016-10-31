<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Paging.ascx.vb" Inherits="Controls_Generic_Paging" %>

<table border="0" cellpadding="0" cellspacing="0" id="paging-table">
	<tr>
		<td>
            <asp:LinkButton ID="lnkStartPage" runat="server" CssClass="page-far-left">Start Page</asp:LinkButton>
            <asp:LinkButton ID="lnkPrevious" runat="server" CssClass="page-left">Previous</asp:LinkButton>

			<div id="page-info">
                <strong>
                    <asp:Literal ID="ltlCurrentPage" runat="server">1</asp:Literal>
                </strong> /
                
                <asp:Literal ID="ltlLastPage" runat="server">15</asp:Literal>   
            </div>

            <asp:LinkButton ID="lnkNext" runat="server" CssClass="page-right">Next</asp:LinkButton>
            <asp:LinkButton ID="lnkEndPage" runat="server" CssClass="page-far-right">End Page</asp:LinkButton>
		</td>
	</tr>
</table>