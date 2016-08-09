<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true"
    CodeBehind="Advertising.aspx.cs" Inherits="HaloBoSach.Advertising" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mt10">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <a href="<%#Eval("LinkURL") %>">
                <img src="/AdvImage/<%#Eval("AdvImagePath") %>" style="margin-right: 7px; margin-bottom: 10px;
                    float: left; width: 233px; height: 330px" /></a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
