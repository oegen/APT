<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SimpleMultiLineTextBox.ascx.vb" Inherits="Controls_Generic_SimpleMultiLineTextBox" %>
<asp:PlaceHolder runat="server" ID="plcBindingFix">
<script type="text/javascript">

    $(function () {
        var maxLength = <%# MaxLength %>;

        $("#<%# txtValue.ClientID %>").keyup(function () {
            var currentCount = $("#<%# txtValue.ClientID %>").val().length;  
            $("#<%# lblCharsRemaining.ClientID %>").text(maxLength - currentCount + " characters left");
        });

        $("#<%# txtValue.ClientID %>").keypress(function () {
            var count = this.value.length;
            if (count >= maxLength)
            {
                return false;
            }
        });
    });

</script>
</asp:PlaceHolder>
<asp:Label ID="lblAttribute" runat="server" AssociatedControlID="txtValue" EnableViewState="false"></asp:Label>

<asp:TextBox ID="txtValue" runat="server" Height="150"  TextMode="MultiLine" CssClass="fixedInputWidth" MaxLength="4000"></asp:TextBox>
<asp:Label ID="lblCharsRemaining" runat="server" ForeColor="Red"></asp:Label>
<asp:RequiredFieldValidator ID="reqValue" runat="server" ControlToValidate="txtValue" Display="Dynamic" Enabled="false" CssClass="">*</asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="regLength" runat="server" ControlToValidate="txtValue" Display="Dynamic" Enabled="false" ValidationExpression="^[\S\s]{{0,4000}}$">Max Length Error</asp:RegularExpressionValidator>



