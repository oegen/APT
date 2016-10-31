<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ReserveTime.ascx.vb" Inherits="Controls_Reserve_Time_ReserveTime" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Reserve Time/ElementEstimations.ascx" TagName="ElementEstimations" TagPrefix="oegen" %>

<section id="tasks" class="group">
    
    <asp:PlaceHolder runat="server" ID="plcBindingFix">    
    <script src="<%#ResolveUrl("~/js/jquery-ui.min.js")%>" type="text/javascript"></script> 
    
    <script type="text/javascript" src="<%#ResolveUrl("~/js/jquery.progressbar.min.js")%>"></script> 
    </asp:PlaceHolder>
    <div id="apt-list">

            <ul id="reserve-time" class="group">

                <asp:PlaceHolder ID="phAlreadyReserved" runat="server" Visible="true">
                <li class="reserve-desc">
                    <asp:Literal ID="ltlDescription" runat="server"></asp:Literal><br /><br />
                    <asp:Literal ID="ltlFreelancersRequired" runat="server"></asp:Literal>
                </li>
                </asp:PlaceHolder>     
                       
                <li class="reservetime-datepicker">
                    <oegen:DateTimePicker ID="dtpStartWeek" runat="server" LabelText="Start Week:" AutoPostBack="true" />
                    <span class="reserve-time-go"><asp:LinkButton ID="lnkRefresh" CssClass="lnkRefresh" runat="server">Go</asp:LinkButton></span>
                </li>          

                <asp:Repeater ID="rptrProgressBars" runat="server">
                    <ItemTemplate>
                        <li>
                            <asp:Label ID="lblWeek" runat="server" CssClass="week"></asp:Label>
                            <asp:Literal ID="ltlProgressBar" runat="server"></asp:Literal>
                            <span class="hours"><asp:Literal ID="ltlRemainingLabel" runat="server"></asp:Literal></span>
                            <span class="overspill"><asp:Literal ID="ltlOverspillLabel" runat="server"></asp:Literal></span>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            


                <li>
                    <label for="txtProjectHours">Estimated Hours:</label>
                    <asp:TextBox ID="txtProjectHours" runat="server" 
                        onKeyUp="SetAllProgressBars(GetProjectHoursTextValue(), GetNumWeeksTextValue(), GetNumArtworkersTextValue());return true;"></asp:TextBox>
                </li>

                <li>
                    <label for="txtNumWeeks">Number of Weeks:</label>
                    <asp:TextBox ID="txtNumWeeks" runat="server" Text="1" AutoPostBack="true"></asp:TextBox>
                </li>

                <li class="noBorder">
                    <label for="txtNumArtworkers">Number of Artworkers:</label>
                    <asp:TextBox ID="txtNumArtworkers" runat="server" Text="1"
                        onKeyUp="SetAllProgressBars(GetProjectHoursTextValue(), GetNumWeeksTextValue(), GetNumArtworkersTextValue());return true;"></asp:TextBox>
                </li>

                <li id="liRequireFreelancers">
                    <asp:CheckBox ID="cbRequireFreelancers" runat="server" Text="Require Freelancer(s)"/><label id="lblFreeLancers"></label>
                </li>

            </ul>
         
            <asp:PlaceHolder ID="pcFinaliseBriefIncomplete" runat="server" Visible="false">
                <ul>
                    <li>Element estimations times will appear if the brief has been finalised, if possible, please complete the finalise brief task before reserving time. 
                    If not then element estimation times cannot be provided and the time reserved may be innacurate.</li>
                </ul>
            </asp:PlaceHolder>

            <!-- ELEMENT ESTIMATIONS -->                 
            <oegen:ElementEstimations ID="ctrlElementEstimations" runat="server" />
            <!-- ELEMENT ESTIMATIONS ENDS -->

            <ul id="ulErrorMessage" style="display : none;">
                <li id="liErrorMessage" style="display:none;"><span id="lblErrorMessage"></span></li>
                <li id="liOverspill" style="display:none; border-bottom:none"><span id="lblOverspill"></span></li>
                <li id="liSaveDenied" style="display:none; color: Red;"><span id="lblSaveDenied"></span></li>
            </ul>

    </div>            
    
    <div id="submit_btn" class="btn btn_orange thinbtn" style="margin-top:20px">
        <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldProject" onclientclick="return IsPageValid();">Reserve Time</asp:LinkButton>
        <span></span>
    </div>

</section>
<asp:PlaceHolder runat="server" ID="plcBindingFix2">
<script type="text/javascript">
    function GetProjectHoursTextValue() {
        return document.getElementById('<%#txtProjectHours.ClientID%>').value;
    }

    function GetNumWeeksTextValue() {
        return document.getElementById('<%#txtNumWeeks.ClientID%>').value;
    }

    function GetNumArtworkersTextValue() {
        return document.getElementById('<%#txtNumArtworkers.ClientID%>').value;
    }

    function GetFreelancersChecked(){
        return document.getElementById('<%#cbRequireFreelancers.ClientID%>').checked;
    }

    function CheckSectionDisplay() {
        if (document.getElementById("liErrorMessage").style.display == "none"
            && document.getElementById("liOverspill").style.display == "none"
            && document.getElementById("liSaveDenied").style.display == "none") {

            document.getElementById("ulErrorMessage").style.display = "none";
            document.getElementById("liRequireFreelancers").style.display = "none";
        }
        else {
            document.getElementById("ulErrorMessage").style.display = "block";
            document.getElementById("liRequireFreelancers").style.display = "block";
        }
    }

    function SetAllProgressBars(hours, numWeeks, numArtworkers) {
        // todo - if some aren't mumbers then don't do anything

        var runningHourCount = 0;
        var artworkerCapabilities = GetNumArtworkersTextValue() * GetWorkingWeekHours();

        for (i = 1; i <= numWeeks; i++) {
            var usedHours = eval("GetHoursUsed" + i + "()");
            var availableHours = eval("GetTotalAvailableHours" + i + "()") - usedHours;
            var remainingHours = hours - runningHourCount;
            var potentialHours = availableHours;
            var actualHours = 0;

            if (availableHours > artworkerCapabilities) {
                potentialHours = artworkerCapabilities;
            }

            if (potentialHours > remainingHours) {
                actualHours = remainingHours;
            }
            else {
                actualHours = potentialHours;
            }

            // Get the progress bar id
            var functionCall = '$("#pb' + i + '").progressBar(' + GetPercentageValue(actualHours + usedHours, i, actualHours) + ');';

            eval(functionCall);

            runningHourCount += actualHours;
        }

        if (runningHourCount < hours) {
            document.getElementById("liOverspill").style.display = "block";

            document.getElementById("lblOverspill").innerHTML = "<p>With the number of weeks and artworkers in which you have selected, only " + runningHourCount
                                                                + " hours could be accounted for.</p><br /><p>There are still " + (hours - runningHourCount)
                                                                + " hours remaining " + "(which at " + GetWorkingWeekHours()
                                                                + " hours per week would work out at approximately "
                                                                + Math.ceil((hours - runningHourCount) / GetWorkingWeekHours() / numWeeks) 
                                                                + " freelancer(s) for the " + numWeeks + " week(s)).</p><br />"
                                                                + "<p>You will be required to either highlight that you wish to use freelancers to account for the additional time, or to increase the number of artworkers or weeks.</p>";
        }
        else {
            document.getElementById("liOverspill").style.display = "none";
            document.getElementById("liSaveDenied").style.display == "none";
        }

        CheckArtworkers();
        CheckSectionDisplay();
    }

    function CheckArtworkers() {
        var numArtworkers = GetNumArtworkersTextValue();
        var availableArtworkers = GetTotalArtworkers();

        if (numArtworkers > availableArtworkers) {
            document.getElementById("lblErrorMessage").innerHTML = "You have requested " + numArtworkers + " artworkers." +
                                                                   " There are only " + availableArtworkers + " artworkers available.";

            document.getElementById("liErrorMessage").style.display = "block";
        }
        else {
            document.getElementById("liErrorMessage").style.display = "none";
            document.getElementById("liSaveDenied").style.display = "none";
        }

        CheckSectionDisplay();
    }

    function ConvertToPercentage(textValue, weekNum) {
        var hoursUsed = Math.ceil(textValue);
        var hoursAvailable = eval("GetTotalAvailableHours" + weekNum + "()");
        var percentageValue = Math.round(GetUsedPercentage(weekNum) + (parseInt(textValue) / GetSinglePercentage(weekNum)));

        if (percentageValue > 100) {
            percentageValue = 100;
        }

        return percentageValue;
    }

    function GetPercentageValue(textValue, weekNum, projectsHours) {
        if (textValue != null && textValue != "" && !isNaN(textValue)) {
            var hoursUsed = Math.ceil(textValue);
            var hoursAvailable = eval("GetTotalAvailableHours" + weekNum + "()");
            var percentageValue = Math.round(parseInt(textValue) / GetSinglePercentage(weekNum));

            if (hoursUsed > hoursAvailable) {
                document.getElementById("lblOverspill" + weekNum).innerHTML = "Overspill - " + (hoursUsed - hoursAvailable) + " hours "
                                                                              + " (approx. " + Math.round((hoursUsed - hoursAvailable) / GetWorkingWeekHours()) 
                                                                              + " freelancers working at " + GetWorkingWeekHours() + " hours a week)";

                document.getElementById("lblOverspill" + weekNum).style.display = "block";

                hoursUsed = hoursAvailable;

                // Make 100 so the graphic doesn't mess up (anything over 100 makes it go funny)
                percentageValue = 100;
            }
            else {
                document.getElementById("lblOverspill" + weekNum).style.display = "none";
            }

            document.getElementById("lblRemainingHours" + weekNum).innerHTML = hoursUsed + " hours used out of " + hoursAvailable + ". <br />"
                                                                               + projectsHours + " hours for this project.";

            return percentageValue;
        }

        document.getElementById("lblRemainingHours" + weekNum).innerHTML = "0 hours used out of " + eval("GetTotalAvailableHours" + weekNum + "()");

        return GetUsedPercentage(weekNum);
    }

    function GetPercentageByWeeks(hours, availableHours, hoursUsedInWeek) {
        if (hours != null && hours != "" && !isNaN(hours)) {
            // Get the total hours we can fulfill in that week


            // Divide the number of hours by weeks so we can roughly average it out
            return GetPercentageValue(hours / numWeeks, weekNum);
        }

        return GetUsedPercentage(weekNum);
    }

    function GetUsedPercentage(weekNum) {
        var hoursUsed = eval("GetHoursUsed" + weekNum + "();");  // set this in the code behind (page load)
        var totalHours = eval("GetTotalAvailableHours" + weekNum + "();");  // set this in the code behind (page load)
        var singlePercent = totalHours / 100;

        return hoursUsed / singlePercent;
    }

    function GetSinglePercentage(weekNum) {
        var totalHours = eval("GetTotalAvailableHours" + weekNum + "();");  // Set this on page load

        return totalHours / 100;
    }

    function IsPageValid() {
        // If there is an error, the page cannot be submitted
        if (document.getElementById("liErrorMessage").style.display == "block") {
            document.getElementById("liSaveDenied").innerHTML = "You must address the errors before the time can be reserved.";
            document.getElementById("liSaveDenied").style.display = "block";

            return false;
        }

        // If there is an overspill and freelancers aren't required
        if (document.getElementById("liOverspill").style.display == "block" && GetFreelancersChecked() == false) {
            document.getElementById("liSaveDenied").innerHTML = "You must address the errors before the time can be reserved.";
            document.getElementById("liSaveDenied").style.display = "block";

            return false;
        }

        return true;
    }

    function HasEnoughArtworkerTime(numArtworkers, totalTime, numWeeks) {
        var workingWeekHours = GetWorkingWeekHours();
        var totalArtworkers = GetTotalArtworkers();

        if (numArtworkers > totalArtworkers) {
            document.getElementById("lblErrorMessage").innerHTML = "There are only " + totalArtworkers +
                                                                    " artworkers available, you have opted for " + numArtworkers + 
                                                                    " artworker(s).";

            return false;
        }

        if (((totalTime / numArtworkers) / numWeeks) > workingWeekHours) {
            document.getElementById("lblErrorMessage").innerHTML = numArtworkers + " artworker(s) can work a total of " + (workingWeekHours * numArtworkers) +
                                                                    " hours a week. You requested for " + totalTime + 
                                                                    " hours for all artworker(s) involved which is invalid. If freelancers are required please select the check box to continue.";

            return false;
        }

        return true;
    }

</script>
</asp:PlaceHolder>
<asp:Literal ID="ltlArtworkersTotalAndHoursScript" runat="server"></asp:Literal>

<!-- Javascript Function Writing Repeater for each week -->
<asp:Repeater ID="rptrJavascript" runat="server">
    <ItemTemplate>
        <asp:Literal ID="ltlGetHoursScript" runat="server"></asp:Literal>
        <asp:Literal ID="ltlGetAvailableHoursScript" runat="server"></asp:Literal>
        <asp:Literal ID="ltlProgressBarDefinition" runat="server"></asp:Literal>
    </ItemTemplate>
</asp:Repeater>

<script type="text/javascript">

    var t = setTimeout("checkTheTimer()", 1000);

    $(window).load(checkTheTimer());

    function checkTheTimer() {
        SetAllProgressBars(GetProjectHoursTextValue(), GetNumWeeksTextValue(), GetNumArtworkersTextValue());
    }

</script>
