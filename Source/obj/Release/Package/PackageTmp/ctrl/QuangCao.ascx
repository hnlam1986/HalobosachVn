<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuangCao.ascx.cs" Inherits="HaloBoSach.ctrl.QuangCao" %>
<div id="demo3" style="width:233px ">
   
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <a href="<%#Eval("LinkURL") %>" target="_blank">
                    <img src="/AdvImage/<%#Eval("AdvImagePath") %>" height="330px" width="233px" style="margin-bottom: 10px"></a>
            </ItemTemplate>
        </asp:Repeater>

</div>
