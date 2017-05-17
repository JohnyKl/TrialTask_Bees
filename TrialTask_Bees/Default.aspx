<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TrialTask_Bees.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 39px">
    
        <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="DRONE BEES" Width="206px"></asp:Label>
        <hr />
        <div style="border: 1px solid #000000; height: 132px" title="Settings">
            SETTINGS:<br /><br />
        <asp:Label runat="server" Text="Types of Bees" Width="125px"></asp:Label>        
        <asp:Label ID="Label3" runat="server" Text="Number" Width="102px"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="Health points" Width="130px"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Hit points" Width="90px"></asp:Label>
        <br />
        <asp:TextBox ID="tbQueenBeeName" runat="server" ReadOnly="True" Width="110px">Queen Bee</asp:TextBox>
        <asp:TextBox ID="tbQueenBeeNumber" runat="server" Width="92px" OnTextChanged="tb_TextChanged" MaxLength="4" ReadOnly="True" TextMode="Number">1</asp:TextBox>
        <asp:TextBox ID="tbQueenBeeHealth" runat="server" Width="108px" OnTextChanged="tb_TextChanged" TextMode="Number">100</asp:TextBox>
        <asp:TextBox ID="tbQueenBeeHitPoints" runat="server" Width="108px" OnTextChanged="tb_TextChanged" TextMode="Number">8</asp:TextBox>
        <br />
        <asp:TextBox ID="tbWorkerBeeName" runat="server" ReadOnly="True" Width="110px">Worker Bee</asp:TextBox>
        <asp:TextBox ID="tbWorkerBeeNumber" runat="server" Width="92px" OnTextChanged="tb_TextChanged" TextMode="Number">5</asp:TextBox>
        <asp:TextBox ID="tbWorkerBeeHealth" runat="server" Width="108px" OnTextChanged="tb_TextChanged" TextMode="Number">75</asp:TextBox>
        <asp:TextBox ID="tbWorkerBeeHitPoints" runat="server" Width="108px" OnTextChanged="tb_TextChanged" TextMode="Number">10</asp:TextBox>
        <br />
        <asp:TextBox ID="tbDroneBeeName" runat="server" ReadOnly="True" Width="110px">Drone Bee</asp:TextBox>
        <asp:TextBox ID="tbDroneBeeNumber" runat="server" Width="92px" OnTextChanged="tb_TextChanged" TextMode="Number">8</asp:TextBox>
        <asp:TextBox ID="tbDroneBeeHealth" runat="server" Width="108px" OnTextChanged="tb_TextChanged" TextMode="Number">50</asp:TextBox>
        <asp:TextBox ID="tbDroneBeeHitPoints" runat="server" Width="108px" OnTextChanged="tb_TextChanged" TextMode="Number">12</asp:TextBox>
        <asp:Button ID="btnStart" runat="server" Text="Start " OnClick="btnStart_Click" Width="50px" />      
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="50px" />
        </div> 
        <div style="border: 1px solid #000000; height: 400px" title="Settings">

            <asp:Panel ID="Panel1" runat="server" Height="364px">
                <asp:TextBox ID="tbBeesInfo" runat="server" Height="350px" ReadOnly="True" TextMode="MultiLine" Width="492px"></asp:TextBox>
            </asp:Panel>
            <asp:Label ID="lblHitCounter" runat="server" Text="Current Hits: 0; Total killed: 0" Width="280px"></asp:Label>
            <asp:Button ID="btnHit" runat="server" Text="Hit" OnClick="btnHit_Click" Width="50px" Enabled="False" />
            <asp:Label ID="lblMessage" runat="server" Text="Message: " Width="600px"></asp:Label>

        </div> 
    </div>
        </form>
    </body>
</html>
