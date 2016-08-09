<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="NewsCategoryManagement.aspx.cs" Inherits="HaloBoSach.admin.NewsCategoryManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jtree/themes/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jtree/jstree.js"></script>
    <script type="text/javascript" src="Scripts/NewsCategoryManagement.Event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="movenode"></div>
    <div class="mainDiv">
        <div class="treeViewFrame">
            <div id="treeViewCate">
            </div>
            <script type="text/javascript">
                $(function () {
                    NewsCategoryManagementEvent.InitJSTreeView("treeViewCate");
                });
            </script>
        </div>
        <div class="ListNewsFrame" id="NewsListFrame">
            
        </div>
    </div>
    <div id="dialog-modal" title="Basic modal dialog">
</div>

</asp:Content>
