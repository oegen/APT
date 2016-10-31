<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TopBar.ascx.vb" Inherits="Controls_Navigation_TopBar" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>

<header id="header">

    <div id="toppanel">

        <div id="search-panel">
	        <div class="content clearfix">

		        <div class="left">
			        <!-- Login Form -->
				        <h2>Search for a project</h2>

				        <label class="grey" for="log">Project Name:</label>
                        <asp:TextBox ID="txtProjectName" runat="server" CssClass="field"></asp:TextBox>

                        <ul class="page-subnav task-subnav">
                            <li><asp:LinkButton ID="lnkSearchName" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt search">search</span></ins></asp:LinkButton></li>
                            <li><asp:HyperLink ID="lnkClearName" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt clear">clear</span></ins></asp:HyperLink></li>                            
                        </ul>	

				        <label class="grey" for="pwd">Project Owner Name:</label>
                        <asp:TextBox ID="txtProjectOwner" runat="server" CssClass="field"></asp:TextBox>

				        <!-- <input class="field" type="text" name="pwd" id="Password1" size="23" /> -->
                        <ul class="page-subnav task-subnav">
                            <li><asp:LinkButton ID="lnkSearchOwner" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt search">search</span></ins></asp:LinkButton></li>
                            <li><asp:HyperLink ID="lnkClearOwner" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt clear">clear</span></ins></asp:HyperLink></li>
                        </ul>
		        </div>

		        <div class="left">
			        <!-- Login Form -->
				    <h2>&nbsp;</h2>

				    <label class="grey" for="log">Project Coordinator Name:</label>
                    <asp:TextBox ID="txtCoordinator" runat="server" CssClass="field"></asp:TextBox>
				    <!-- <input class="field" type="text" name="log" id="Text6" value="" size="23" /> -->

                    <ul class="page-subnav task-subnav">
                        <li><asp:LinkButton ID="lnkSearchCoordinator" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt search">search</span></ins></asp:LinkButton></li>
                        <li><asp:HyperLink ID="lnkClearCoordinator" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt clear">clear</span></ins></asp:HyperLink></li>
                    </ul>				            

                    <label class="grey" for="pwd">Project Artworker Name:</label>
                    <asp:TextBox ID="txtArtworker" runat="server" CssClass="field"></asp:TextBox>
				    <!-- <input class="field" type="text" name="pwd" id="Password2" size="23" /> -->

                    <ul class="page-subnav task-subnav">
                        <li><asp:LinkButton ID="lnkSearchArtworker" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt search">search</span></ins></asp:LinkButton></li>
                        <li><asp:HyperLink ID="lnkClearArtworker" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt clear">clear</span></ins></asp:HyperLink></li>
                    </ul>
		        </div>

		        <div class="left right">			
			        <!-- Register Form -->
				        <h2>&nbsp;</h2>
				        <label class="grey" for="log">Brand:</label>
                        <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                        
                        <ul class="page-subnav task-subnav">
                            <li><asp:LinkButton ID="lnkSearchBrand" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt search">search</span></ins></asp:LinkButton></li>
                            <li><asp:HyperLink ID="lnkClearBrand" runat="server" CssClass="round-alt"><ins><span class="system-buttons-alt clear">clear</span></ins></asp:HyperLink></li>
                        </ul>		            
		        </div>
	        </div>
        </div> 
        <!-- / advanced search -->

        <!-- The tab on top -->	
        <div class="tab">
	        <ul class="login">
		        <li class="left">&nbsp;</li>
		        <li><asp:Label ID="lblUser" runat="server" CssClass="user" AssociatedControlID="txtAinNumber">Carl</asp:Label></li>
		        <li><asp:Label ID="Label1" CssClass="ain" runat="server" AssociatedControlID="txtAinNumber">AIN No.</asp:Label>
                    <asp:TextBox ID="txtAinNumber" runat="server" AutoPostBack="true"></asp:TextBox> 
                    <asp:Button ID="lnkSubmit" runat="server" CssClass="input_btn"></asp:Button>
                </li>
		        <li id="search-toggle" class="last-item">
			        <a id="search-open" class="search-open" href="#">[ Advanced Search <span class="open-message">&nbsp;</span> ]</a>
			        <a id="search-close" style="display: none;" class="search-close" href="#">[ Close Search <span class="close-message">&nbsp;</span> ]</a>			
		        </li>
	        </ul> 
        </div>
        <!-- / top -->
    </div>

</header>