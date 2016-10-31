<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PDFAnnotateTask.ascx.vb" Inherits="Controls_Tasks_PDFAnnotateTask" %>
<%@ Register Src="~/Controls/Project/DocumentOverview.ascx" TagName="documentOverview" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Project/DocumentListing.ascx" TagName="documentListing" TagPrefix="oegen" %>

<section id="tasks" class="group">
                                   
    <div id="apt-list" class="tasks">
        <ul>
            <li>
                <span class="apt-list-yourtask-title"><asp:Label ID="lblTaskName" runat="server"></asp:Label></span>
                <span class="apt-list-description"><asp:Label ID="lblTaskDesc" runat="server"></asp:Label></span>

                <span class="apt-list-yourtask-title">Comment</span>
                <span class="apt-list-description"><asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox></span>

                <asp:PlaceHolder ID="phError" runat="server" Visible="false">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </asp:PlaceHolder>
            </li>

            <li>
                <ul class="page-subnav task-subnav">
                    <li><asp:LinkButton ID="lnkUploadDocument" runat="server" CssClass="round"><ins><span class="system-buttons upload-document">Upload Document</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkViewDocuments" runat="server" CssClass="round"><ins><span class="system-buttons view-tasks">View Documents</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkComplete" runat="server" CssClass="round"><ins><span class="system-buttons complete">Task Complete</span></ins></asp:LinkButton></li>
                </ul>
            </li>
        </ul>
    </div>

    <!-- Upload Document -->
    <oegen:documentOverview ID="ctrlDocumentUpload" runat="server" Visible="false" />

    <!-- Document Listing -->
    <oegen:documentListing ID="ctrlDocumentListing" runat="server" Visible="false" />

    <asp:Label ID="lblNotification" runat="server" Visible="false">The upload was successful.</asp:Label>
                                        
</section> 

