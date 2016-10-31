<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CompleteTask.ascx.vb" Inherits="Controls_Tasks_CompleteTask" %>
<%@ Register Src="~/Controls/Project/DocumentOverview.ascx" TagName="documentOverview" TagPrefix="oegen" %>

<section id="tasks" class="group">
                                   
    <div id="apt-list" class="tasks">
        <ul>
            <li>
                <span class="apt-list-yourtask-title"><asp:Label ID="lblTaskName" runat="server"></asp:Label></span>
                <span class="apt-list-description"><asp:Label ID="lblDescription" runat="server">Has this process been completed?</asp:Label></span>

                <asp:Panel ID="pnlElement" runat="server" Visible="false">

                    <span class="apt-list-yourtask-title"><asp:Label ID="lblElementName" runat="server"></asp:Label></span>

                    <span class="apt-list-description"><asp:Label ID="lblElementDescription" runat="server"></asp:Label></span>

                </asp:Panel>

                <span class="apt-list-yourtask-title">Comment</span>

                <span class="apt-list-description"><asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox></span>

                <asp:PlaceHolder ID="phError" runat="server" Visible="false">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </asp:PlaceHolder>
            </li>

            <li>
                <ul class="page-subnav task-subnav">
                    <li><asp:LinkButton ID="lnkDocumentUpload" runat="server" CssClass="round"><ins><span class="system-buttons upload-document">Upload Document</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkComplete" runat="server" CssClass="round"><ins><span class="system-buttons complete">Task Complete</span></ins></asp:LinkButton></li>
                </ul>
            </li>
        </ul>
    </div>

    <oegen:documentOverview ID="ctrlUploadDocument" runat="server" Visible="false" />

    <asp:Label ID="lblUploadSuccess" runat="server" Visible="false">File uploaded successfully.</asp:Label>
                                        
</section> 









