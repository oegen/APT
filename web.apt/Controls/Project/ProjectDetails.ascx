<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectDetails.ascx.vb" Inherits="Controls_Project_ProjectDetails" %>
<%@ Register Src="~/Controls/Generic/UserDisplayAndSelect.ascx" TagName="UserBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="TextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagName="MultiLineTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagName="DatePicker" TagPrefix="oegen" %>
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


        $('#<%# dtpRequiredDate.DateClientID %>').datepicker('destroy')
        $('#<%# dtpRequiredDate.DateClientID %>').datepicker({
            dateFormat: 'dd/mm/yy',
            onSelect: function () { CheckDate() }
        });

        /*$(".datepicker").datepicker('destroy')
        $(".datepicker").datepicker({
            dateFormat: 'dd/mm/yy',
            onSelect: function () { CheckDate() }
        });*/

    });
</script>
</asp:PlaceHolder>
<div class="tabswrap">

    <div id="tabs-inner" class="group">  

        <span class="tabs-titles">
            <oegen:ContentTitle ID="ctrlGenericContentTitle" runat="server" ToolTipText="Project Details" Title="View / Edit the project details" />
        </span>

        <asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="BulletList" />

            <ul id="ulProjectActions" runat="server" visible="false" class="page-subnav-alt">
                <li><asp:LinkButton ID="lnkArchive" runat="server" CssClass="round-alt"><ins><span class="system-buttons2 archive">archive</span></ins></asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkStop" runat="server" CssClass="round-alt"><ins><span class="system-buttons2 stop">stop</span></ins></asp:LinkButton></li>
            </ul>

            <div class="aptform gen-form">
            <fieldset>
                <legend>Project Details</legend>

                <ol>
                    <li>
                        <oegen:TextBox ID="txtAINNumber" runat="server" LabelText="AIN Number" Enable="false" />
                    </li>
                    <li>
                        <oegen:TextBox ID="txtName" runat="server" LabelText="Project Name" MaxLength="50" 
                            ValidationGroup="vldProject" ErrorMessage="* required field" />
                    </li>
                    <li>
                        <oegen:TextBox ID="txtBudgetCode" runat="server" LabelText="Finance Codes" MaxLength="50" 
                            ValidationGroup="vldProject" ErrorMessage="* required field" />
                    </li>
                    <li id="liAvailableBudget" runat="server" visible="false">
                        <oegen:NumericTextBox ID="txtAvailableBudget" runat="server" LabelText="Available Budget" 
                            ValidationGroup="vldProject" ErrorMessage="* required field" />
                    </li>
                    <li>
                        <oegen:DatePicker ID="dtpRequiredDate" runat="server" LabelText="In-Trade Date" 
                            ErrorMessage="* required field" ValidationGroup="vldProject" CssClass="" />
                        <asp:Label ID="lblWarning" runat="server" ForeColor="Red"></asp:Label>
                    </li>
                    <li>
                        <oegen:DatePicker ID="dtRequiredPrintDate" runat="server" LabelText="Req’d Print Del Date" ValidationGroup="vldProject" />
                    </li>
                    <li>
                        <asp:Label ID="lblOwner" runat="server" AssociatedControlID="ctrlOwner">Project Owner<em>*</em></asp:Label>
                        <oegen:UserBox ID="ctrlOwner" runat="server" ValidationGroup="vldProject" ErrorMessage="* required field" />
                    </li>
                    <li>
                        <asp:Label ID="lblPORaiser" runat="server" AssociatedControlID="ctrlPORaiser">PO Raiser<em>*</em></asp:Label>
                        <oegen:UserBox ID="ctrlPORaiser" runat="server" ErrorMessage="* required field" />
                    </li>
                    <li>
                        <asp:Label ID="lblLegalApprover" runat="server" AssociatedControlID="ctrlLegalApprover">Legal Approver<em>*</em></asp:Label>
                        <oegen:UserBox ID="ctrlLegalApprover" runat="server" ValidationGroup="vldProject" ErrorMessage="* required field" />
                    </li>
                    <li>
                        <asp:Label ID="lblBrandManager" runat="server" AssociatedControlID="ctrlBrandManager">Brand Manager</asp:Label>
                        <oegen:UserBox ID="ctrlBrandManager" runat="server" />
                    </li>
                    <li>
                        <asp:Label ID="lblStudioQA" runat="server" AssociatedControlID="ddlStudioQA">Studio QA<em>*</em></asp:Label>
                        <asp:DropDownList ID="ddlStudioQA" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqStudioQA" runat="server" Display="Dynamic" ValidationGroup="vldProject" 
                            ControlToValidate="ddlStudioQA" ErrorMessage="* required field" InitialValue="0">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <asp:Label ID="lblCoordinator" runat="server" AssociatedControlID="ddlCoord">Coordinator<em>*</em></asp:Label>
                        <asp:DropDownList ID="ddlCoord" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqCoord" runat="server" Display="Dynamic" ValidationGroup="vldProject" 
                            ControlToValidate="ddlCoord" ErrorMessage="* required field" InitialValue="0">
                        </asp:RequiredFieldValidator>
                    </li>

                    <li>
                        <asp:Label ID="lblBrandList" runat="server" AssociatedControlID="ddlBrandList">Brand List<em>*</em></asp:Label>
                        <asp:DropDownList ID="ddlBrandList" runat="server" ValidationGroup="vldProject"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="vldBrandList" runat="server" ErrorMessage="* required field"
                            Display="Dynamic" ValidationGroup="vldProject" InitialValue="0" ControlToValidate="ddlBrandList"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hdnBrandSelected" runat="server" />
                    </li>
                    <li>
                        <asp:Label ID="lblTypeOfWork" runat="server" AssociatedControlID="ddlTypeOfWork">Type of Work<em>*</em></asp:Label>
                        <asp:DropDownList ID="ddlTypeOfWork" runat="server" ValidationGroup="vldProject"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="vldTypeOfWork" runat="server" ErrorMessage="* required field"
                            Display="Dynamic" ValidationGroup="vldProject" InitialValue="0" ControlToValidate="ddlTypeOfWork"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hdnTypeOfWorkSelected" runat="server" />
                    </li>
                    <li>
                        <asp:Label ID="lblBusinessArea" runat="server" AssociatedControlID="ddlBusinessArea">Business Area<em>*</em></asp:Label>
                        <asp:DropDownList ID="ddlBusinessArea" runat="server" ValidationGroup="vldProject"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqBusinessArea" runat="server" ErrorMessage="* required field"
                            Display="Dynamic" ValidationGroup="vldProject" InitialValue="0" ControlToValidate="ddlBusinessArea"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hdnBusinessAreaSelected" runat="server" />
                    </li>
                    <li>
                        <oegen:TextBox ID="txtPrintRefNum" runat="server" LabelText="W/Lea I-Media Number" MaxLength="70" />
                    </li>
                    <li>
                        <asp:Label ID="lblWLeaProjectManager" runat="server" AssociatedControlID="ddlWleaProj">W/Lea Project Manager</asp:Label>
                        <asp:DropDownList ID="ddlWleaProj" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="hdnPreviousWLea" runat="server" />
                    </li>
                </ol>
            </fieldset>

            <asp:PlaceHolder ID="pcSave" runat="server">
                <div id="submit_btn" class="btn btn_orange">
                    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldProject">Save</asp:LinkButton>
                    <span></span>
                </div>

            <div id="right-btn">
                <div class="btn btn_grey">
                    <asp:LinkButton ID="btnPrintable" runat="server" Text="Printable Version" /><span></span>
                </div>
            </div>
    
            </asp:PlaceHolder>

        
        </div>

    </div>
</div>