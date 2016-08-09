var NewsCategoryManagementEvent = {
    isCreateNode: false,
    treeView: "NewsCate",
    CustomMenu: function ($node) {
        this.treeView = this.element.get(0).id;
        var tree = $("#" + this.treeView).jstree(true);
        var items = {
            createItem: {
                label: "Thêm",
                "action": function (obj) {
                    $node = tree.create_node($node);
                    tree.edit($node);
                }
            },
            renameItem: { // The "rename" menu item
                label: "Đổi tên",
                "action": function (obj) { tree.edit($node); }
            },
            deleteItem: { // The "delete" menu item
                label: "Xóa",
                "action": function (obj) {
                    tree.delete_node($node);
                }
            },
            displayItem: { // The "delete" menu item
                label: "Hiện Menu",

                "action": function (obj) {
                    var Url = "/ajax.aspx?action=changestatus&id=" + $node.id;
                    $.ajax({
                        type: "POST",
                        url: Url,
                        dataType: 'text',
                        error: function (msg) {
                            this.result = false;
                        }
                    });
                }
            },
            hideItem: { // The "delete" menu item
                label: "Ẩn Menu",

                "action": function (obj) {
                    var Url = "/ajax.aspx?action=changestatus&id=" + $node.id;
                    $.ajax({
                        type: "POST",
                        url: Url,
                        dataType: 'text',
                        error: function (msg) {
                            this.result = false;
                        }
                    });
                }
            },
            displayAsCateItem: { // The "delete" menu item
                label: "Hiện danh mục SP",

                "action": function (obj) {
                    var Url = "/ajax.aspx?action=changecategorystatus&id=" + $node.id;
                    $.ajax({
                        type: "POST",
                        url: Url,
                        dataType: 'text',
                        error: function (msg) {
                            this.result = false;
                        }
                    });
                }
            },
            hideAsCateItem: { // The "delete" menu item
                label: "Ẩn danh mục SP",

                "action": function (obj) {
                    var Url = "/ajax.aspx?action=changecategorystatus&id=" + $node.id;
                    $.ajax({
                        type: "POST",
                        url: Url,
                        dataType: 'text',
                        error: function (msg) {
                            this.result = false;
                        }
                    });
                }
            },
            changelink: { // The "delete" menu item
                label: "Chuyển link",

                "action": function (obj) {
                  

                    //-----------------
                    var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px; " src="UpdateNaviLink.aspx?id=' + $node.id + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 230,
                    width: 550,
                    title: "Chuyển hướng link"
                });
                $dialog.dialog('open');
                    //--------------------
                }
            },
            changeImage: { // The "delete" menu item
                label: "Thiết lập giao diện",

                "action": function (obj) {


                    //-----------------
                    var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px; " src="ProductCategoryImage.aspx?id=' + $node.id + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 250,
                    width: 550,
                    title: "Thiết lập giao diện"
                });
                    $dialog.dialog('open');
                    //--------------------
                }
            }

        };
        if ($node.parents.length == 1) {
            delete items.deleteItem;
            delete items.renameItem;
        } else if ($node.parents.length == 3) {
            delete items.createItem;
        }
        if ($("#" + $node.id).attr("data-visible") == "True") {
            delete items.displayItem;
        } else {
            delete items.hideItem;
        }
        if ($("#" + $node.id).attr("data-is-category") == "True") {
            delete items.displayAsCateItem;
        } else {
            delete items.hideAsCateItem;
            delete items.changeImage;
        }

        return items;
    },
    InitJSTreeView: function (tree) {
        this.treeView = tree;
        $('#' + tree).on('changed.jstree', function (e, data) {
            var id = undefined;
            if (data.node && data.node.id) {
                id = data.node.id;
            }
            if (id) {
                var level = data.node.parents.length;
                var Url = "";
                if (level >= 3) {
                    Url = "/ajax.aspx?action=getnewslist&isLeaf=true&id=" + id;
                    if (data.node.data.isCategory == "True") {
                        Url = "/ajax.aspx?action=getproductlist&isLeaf=true&id=" + id;
                    }
                } else {
                    Url = "/ajax.aspx?action=getnewslist&isLeaf=false&id=" + id;
                    if (data.node.data.isCategory == "True") {
                        Url = "/ajax.aspx?action=getproductlist&isLeaf=false&id=" + id;
                    }
                }
                $.ajax({
                    type: "POST",
                    url: Url,
                    dataType: 'text',
                    success: function (data) {
                        if (data != 'False') {
                            $("#NewsListFrame").get(0).innerHTML = data;
                        }

                    },
                    error: function (msg) {
                        this.result = false;
                    }
                });
            }
        }).jstree({
            "plugins": ["contextmenu", "dnd", "state"], contextmenu: { items: this.CustomMenu },

            'core': {
                "check_callback": function (operation, node, node_parent, node_name, move_to) {
                    if (operation == 'rename_node') {
                        var action = "rename";
                        var isleaf = false;
                        if (NewsCategoryManagementEvent.isCreateNode) {
                            action = "create";
                        }
                        if (node.parents.length >= 3) {
                            isleaf = true;
                        }
                        $.ajax({
                            type: "POST",
                            url: "/ajax.aspx?action=" + action,
                            dataType: 'text',
                            data: { id: NewsCategoryManagementEvent.isCreateNode ? node.parent : node.id, name: node_name, leaf: isleaf },
                            success: function (data) {
                                if (data == 'True' && action == "rename") {
                                    return true;
                                }
                                else {
                                    NewsCategoryManagementEvent.isCreateNode = false;
                                    $('#' + tree).jstree('refresh');
                                }
                            },
                            error: function (msg) {
                                this.result = false;
                            }
                        });

                    } else if (operation == 'delete_node') {
                        $.ajax({
                            type: "POST",
                            url: "/ajax.aspx?action=delete",
                            dataType: 'text',
                            data: { id: node.id },
                            success: function (data) {
                                if (data == 'True') {
                                    return true;
                                }
                                else {
                                    $('#' + tree).jstree('refresh');
                                }
                            },
                            error: function (msg) {
                                this.result = false;
                            }
                        });
                    } else if (operation == 'create_node') {
                        NewsCategoryManagementEvent.isCreateNode = true;
                    } else if (operation == 'move_node') {
                        if (!move_to) {
                            if (arguments[1].children.length > 0 && arguments[2].parents.length == 2 || arguments[2].parents.length > 2) {
                                alert('Vượt quá số cấp quy định!');
                                //$('#' + tree).jstree('refresh');
                                return false;
                            }
                            var isLeaf = false;
                            if (arguments[2].parents.length == 2) {
                                isLeaf = true;
                            }
                            $.ajax({
                                type: "POST",
                                url: "/ajax.aspx?action=order",
                                dataType: 'text',
                                data: { selected: arguments[1].id, children: arguments[2].children.toString(), children_sort: arguments[2].children_d.toString(),
                                    parent_node: arguments[2].id, order: arguments[3], is_leaf: isLeaf
                                },
                                success: function (data) {
                                    if (data == 'True') {
                                        return true;
                                    }
                                    else {
                                        $('#' + tree).jstree('refresh');
                                    }
                                },
                                error: function (msg) {
                                    this.result = false;
                                }
                            });
                        } else {

                        }
                    }
                },
                'data': {
                    'url': '/ajax.aspx',
                    'data': function (node) {
                        return {
                            'action': 'get',
                            'id': node.id
                        };
                    }
                }
            }

        });
    },
    DeleteNewsItem: function (id, image) {
        if (id) {
            $("#news_indicator_" + id).css("display", "");
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=deletenews",
                dataType: 'text',
                data: { "id": id, img: image },
                success: function (data) {
                    if (data == 'True') {
                        $("#news_" + id).remove();
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    },
    DeleteProductItem: function (id, image) {
        if (id) {
            $("#news_indicator_" + id).css("display", "");
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=deleteproduct",
                dataType: 'text',
                data: { "id": id, img: image },
                success: function (data) {
                    if (data == 'True') {
                        $("#news_" + id).remove();
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    },
    SelectedPage: function (btn, page) {
        $(".custom-table").hide();
        $(".custom-table[data-paging=" + page + "]").show();
        $(".pgGrid li a").removeClass("selected");
        $(btn).addClass("selected");
    },
    UpdateDirectUrl: function (id, url) {
        if (id) {
            $("#news_indicator_" + id).css("display", "");
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=applydirect",
                dataType: 'text',
                data: { "id": id, url: url },
                success: function (data) {
                    if (data == 'True') {
                        $("#news_indicator_" + id).css("display", "none");
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    }
}