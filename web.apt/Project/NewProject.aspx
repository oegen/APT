<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewProject.aspx.vb" Inherits="Project_NewProject" Title="New Project" MasterPageFile="~/MasterPages/MasterPage.master" EnableViewState="true" %>
<%@ Register Src="~/Controls/Generic/UserSearchListing.ascx" TagName="UserSearchListing" TagPrefix="oegen"  %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen"  %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen"  %>
<%@ MasterType VirtualPath="~/MasterPages/MasterPage.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<script src="../js/jquery.horizontal.scroll.js" type="text/javascript"></script>
<asp:PlaceHolder runat="server" ID="plcBindingFix">
<script type="text/javascript">
    $(function () {

        function CheckDate() {

            var selectedDate = $(".datepicker").datepicker("getDate");
            var latestPossibleDate = new Date;
            latestPossibleDate.setMonth(latestPossibleDate.getMonth() + 5);

            if (selectedDate > latestPossibleDate) {
                $('#<%# lblWarning.ClientID %>').text("");
            }
            else {
                $('#<%# lblWarning.ClientID %>').text("The In trade date is set to a date that is less than twenty weeks away, completion of this project may not be possible");
            }
        }

        $(".datepicker").datepicker('destroy')
        $(".datepicker").datepicker({
            dateFormat: 'dd/mm/yy',
            onSelect: function () {

                $('#<%# lblWarning.ClientID %>').text("");

                if ($('#<%# cbBDProject.ClientID %>').attr('checked')) {
                    CheckDate();
                }
            }
        });

    });
</script>
</asp:PlaceHolder>


<script type="text/javascript">

    $(document).ready(function () {
        $('#horiz_container_outer').horizontalScroll();

    });
		
</script>


<div id="maincontent" class="group menu-closed">

                             
    <!--CONTENT STARTS-->
    <div id="content" class="new-brief group">
                    
 


       <div class="title">
            <div class="title-nav">
                <h1>New Project Brief</h1>
                <h2>Create the vital info needed right here...</h2>
            </div>
        </div>
            

                            
            <div id="section-wrap">
                            
                    <hr />

	                <ul id="horiz_container_outer">
		                <li id="horiz_container_inner">
			                <ul id="horiz_container">
				                <li><img src="../App_Themes/_default/images/oneway01.jpg" width="241px" height="411px" alt="One way of working" /></li>
                                <li><img src="../App_Themes/_default/images/oneway02.jpg" width="241px" height="411px" alt="One way of working" /></li>
                                <li><img src="../App_Themes/_default/images/oneway03.jpg" width="241px" height="411px" alt="One way of working" /></li>
                                <li><img src="../App_Themes/_default/images/oneway04.jpg" width="241px" height="411px" alt="One way of working" /></li>
                                <li><img src="../App_Themes/_default/images/oneway05.jpg" width="241px" height="411px" alt="One way of working" /></li>
                                <li><img src="../App_Themes/_default/images/oneway06.jpg" width="241px" height="411px" alt="One way of working" /></li>
                                <li><img src="../App_Themes/_default/images/oneway07.jpg" width="241px" height="411px" alt="One way of working" /></li>
                                <li><img src="../App_Themes/_default/images/oneway08.jpg" width="241px" height="411px" alt="One way of working" /></li>
			                </ul>
		                </li>		
	                </ul>
						
	                <div id="scrollbar">
		                <a id="left_scroll" class="mouseover_left" href="#"></a>
		                <div id="track">
                            <div id="dragBar"></div>
		                </div>
		                <a id="right_scroll" class="mouseover_right" href="#"></a>
                    </div>

            
                    <div class="welcome">
                        <h2>Welcome to the power of One!</h2>
                        <p>One team, using one system, following one process &amp; timeline – all working one way, to achieve one result...</p>
                        <p>...Brilliant project execution!</p>
                    </div>

                    <div class="oneteam"><img src="../App_Themes/_default/images/oneteam_badge.gif" alt="one team" /></div>
            </div>


            <div class="form-white-box group">
                                
                <div class="aptform group">

                    <!--  NEW PROJECT BRIEF FORM -->
                               
                    <div class="col1">                                
                        <ul class="new-brief">
                            <li>
                                <span class="heading"><label for="name">1. Name of Project <em>*</em></label></span>
                                <span class="brief-txt-box"><asp:TextBox ID="txtProjectName" runat="server" CssClass="new-brief-txtBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldProjectName" runat="server" Display="Dynamic" 
                                        ControlToValidate="txtProjectName" ErrorMessage="* required" ValidationGroup="vldNewProject"></asp:RequiredFieldValidator></span>
                            </li>
                            <li>
                                <span class="heading"><label for="">2. In-trade Date <em>*</em></label></span>
                                <span class="brief-txt-box"><asp:TextBox ID="txtRequiredDate" runat="server" CssClass="datepicker new-brief-txtBox" Width="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vldRequiredDate" runat="server" Display="Dynamic"
                                        ControlToValidate="txtRequiredDate" ErrorMessage="* required" ValidationGroup="vldNewProject"></asp:RequiredFieldValidator>
                                </span>
                            </li> 
                            <li>
                                <span class="heading"><label for="">3. Owner <em>*</em></label></span>
                                <span class="brief-txt-box"><oegen:UserSearchListing ID="ctrlUserSearchListing" runat="server" LabelText=""></oegen:UserSearchListing>
                                <asp:CustomValidator ID="vldOwner" runat="server" Display="Dynamic"
                                    ErrorMessage="* required" ValidationGroup="vldNewProject"></asp:CustomValidator></span>
                            </li>
                            <li id="liAvailableBudget" runat="server" visible="false">
                                <span class="heading"><label for="">4. Budget Available </label></span>
                                <span class="brief-txt-box">
                                    <asp:TextBox ID="txtAvailableBudget" runat="server" Width="30"></asp:TextBox>
                                </span>
                            </li>

                            <li> 
                                <fieldset>
	                                <label for="cbBDProject"><asp:CheckBox ID="cbBDProject" runat="server" CssClass="" 
                                        Checked="false" AutoPostBack="true" Text=" Is this a BD/Agency led project?" /></label>
                                </fieldset> 
                            </li>
                            <li>
                                <span class="heading"><label for="">
                                    <b>Please note this does not mean artwork or design time, has been reserved in the Studio. <br /> To reserve time,  please contact a member of the Studio co-ordination team</b>
                                </label></span>
                            </li>
                            <li> 
                                <fieldset>
	                                <asp:Label ID="lblWarning" runat="server" ForeColor="Red"></asp:Label>
                                </fieldset> 
                            </li>
                        </ul>
                    </div>
                                                 
                    <div class="colFull"> 
                        <div class="btn btn_grey"><asp:LinkButton ID="lnkCreateBrief" runat="server" ValidationGroup="vldNewProject">Create New Brief</asp:LinkButton><span></span></div>
                        <div id="divSaveProject" class="btn btn_orange" runat="server" visible="false"><asp:LinkButton ID="lnkSaveProject" runat="server" ValidationGroup="vldNewProject">Save New Project</asp:LinkButton><span></span></div>
                    </div>

                </div> 
                               
            </div>


                 
    </div>
    <!--CONTENT ENDS-->
                     
</div>

</asp:Content>
