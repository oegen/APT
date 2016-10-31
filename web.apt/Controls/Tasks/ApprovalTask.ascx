<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ApprovalTask.ascx.vb" Inherits="Controls_Tasks_ApprovalTask" %>

<section id="tasks" class="group">

    <div id="apt-list" class="tasks group">
        <ul>
            <li>
                <span class="apt-list-yourtask-title"><asp:Literal ID="ltlTaskName" runat="server"></asp:Literal></span>
                <span class="apt-list-description"><asp:Literal ID="ltlTaskDescription" runat="server"></asp:Literal></span>

                <asp:Panel ID="pnlElementTask" runat="server" Visible="false">

                    <span class="apt-list-yourtask-title"><asp:Literal ID="lblElementName" runat="server"></asp:Literal></span>

                    <span class="apt-list-description"><asp:Literal ID="lblElementDescription" runat="server"></asp:Literal></span>

                </asp:Panel>

                <span class="apt-list-yourtask-title">Comment</span>

                <span class="apt-list-description"><asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox></span>

                <asp:PlaceHolder ID="phError" runat="server" Visible="false">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </asp:PlaceHolder>
            </li>

            <li>
                <ul class="page-subnav task-subnav">
                    <li><asp:LinkButton ID="lnkAccept" runat="server" CssClass="round"><ins><span class="system-buttons yes">Yes</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkReject" onclick="Reject_Click" runat="server" CssClass="round"><ins><span class="system-buttons no">No</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkApproveWithComment" onclick="Reject_Click" Visible="false" runat="server" CssClass="round"><ins><span class="system-buttons yes">Approve with comments</span></ins></asp:LinkButton></li>
                </ul>
            </li>
        </ul>
    </div>
                                        
</section> 
