var JSUtilities = {
    TrimByPixel: function (item, width) {
        var str = $(item).text();
        var spn = $('<span style="visibility:hidden"></span>').text(str).appendTo('body');
        spn.css("font-size", $(item).css("font-size"));
        spn.css("font-weight", $(item).css("font-weight"));
        var txt = str;
        while (spn.width() > width) {

            txt = JSUtilities.TrimOneWord(txt);
            spn.text(txt + "...");
        }
        txt = spn.text();
        document.body.removeChild(spn.get(0));
        return txt;
    },
    TrimByHeightPixel: function (item, height) {
        var str = $(item).text();
        var txt = str;
        while ($(item).height() > height) {

            txt = JSUtilities.TrimOneWord(txt);
            $(item).text(txt + "...");
        }

    },
    TrimOneWord: function (str) {
        var txt = str;
        var last = txt.lastIndexOf(" ");
        return txt.substring(0, last);
    },
    ConfirmDialog: function (title, msg, callbackFuntion) {
        var $dialog = $('<div class="modal fade" id="myModal" role="dialog">' +
                            '<div class="modal-dialog modal-sm">' +
                              '<div class="modal-content">' +
                                '<div class="modal-header">' +
                                  '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                                  '<h4 class="modal-title">' + title + '</h4>' +
                                '</div>' +
                                '<div class="modal-body">' +
                                  '<p>' + msg + '</p>' +
                                '</div>' +
                                '<div class="modal-footer">' +
                                  '<button type="button" class="btn btn-default" data-dismiss="modal" id="btnOK">Đồng ý</button>' +
                                  '<button type="button" class="btn btn-default" data-dismiss="modal" id="btnClose">Bỏ qua</button>' +
                                '</div>' +
                              '</div>' +
                            '</div>' +
                          '</div>');
        var confirmDialog = $("#myModal");
        if (confirmDialog.size() > 0) {
            confirmDialog.remove();
        }
        $("body").append($dialog);
        $('#myModal').modal('show').one('click', '#btnOK', function () {
            callbackFuntion();
        });
    },
    AlertMessageDialog: function (title, msg, callbackFuntion) {
        var $dialog = $('<div class="modal fade" id="myModal" role="dialog">' +
                            '<div class="modal-dialog modal-sm">' +
                              '<div class="modal-content">' +
                                '<div class="modal-header">' +
                                  '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                                  '<h4 class="modal-title">' + title + '</h4>' +
                                '</div>' +
                                '<div class="modal-body">' +
                                  '<p>' + msg + '</p>' +
                                '</div>' +
                                '<div class="modal-footer">' +
                                  '<button type="button" class="btn btn-default" data-dismiss="modal" id="btnClose">Close</button>' +
                                '</div>' +
                              '</div>' +
                            '</div>' +
                          '</div>');
        var confirmDialog = $("#myModal");
        if (confirmDialog.size() > 0) {
            confirmDialog.remove();
        }
        $("body").append($dialog);
        $('#myModal').modal('show').one('click', '#btnClose', function () {
            if (callbackFuntion) {
                callbackFuntion();
            }
        });

    },
    ChangePassword: function () {
        var utilities = this;
        var username = $("#hdUserName").val();
        var password = $("#txtPassword").val();
        var Url = "/ajax.aspx?action=changepassword";
        $.ajax({
            type: "POST",
            url: Url,
            dataType: 'text',
            data: {UserName: username, Password: password},
            error: function (msg) {
                this.result = false;
            },
            success: function (data) {
                try {
                    if (data == "True") {
                        utilities.AlertMessageDialog("Thay đổi mật khẩu", "Mật khẩu thay đổi thành công", function () {
                            $("#btnSubmit").removeAttr("disabled");
                        })
                    }
                } catch (e) { }
            }
        });
    },

    ValidationForm: function (id) {
        $(id).bootstrapValidator({
            message: 'This value is not valid',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                require: {
                    validators: {
                        notEmpty: {
                            message: 'Không được để trống'
                        }
                    }
                },
                username: {
                    message: 'The username is not valid',
                    validators: {
                        notEmpty: {
                            message: 'The username is required and can\'t be empty'
                        },
                        stringLength: {
                            min: 6,
                            max: 30,
                            message: 'The username must be more than 6 and less than 30 characters long'
                        },
                        regexp: {
                            regexp: /^[a-zA-Z0-9_\.]+$/,
                            message: 'The username can only consist of alphabetical, number, dot and underscore'
                        }
                    }
                },
                country: {
                    validators: {
                        notEmpty: {
                            message: 'The country is required and can\'t be empty'
                        }
                    }
                },
                email: {
                    validators: {
                        notEmpty: {
                            message: 'Email không được để trống'
                        },
                        emailAddress: {
                            message: 'Email không đúng'
                        }
                    }
                },
                website: {
                    validators: {
                        uri: {
                            allowLocal: true,
                            message: 'The input is not a valid URL'
                        }
                    }
                },
                phone: {
                    validators: {
                        notEmpty: {
                            message: 'Số điện thoại không được để trống'
                        },
                        phone: {
                            message: 'Số điện thoại không đúng',
                            country: 'VN'
                        }

                    }
                },



                password: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required and can\'t be empty'
                        },
                        identical: {
                            field: 'confirmPassword',
                            message: 'The password and its confirm are not the same'
                        }
                    }
                },
                confirmPassword: {
                    validators: {
                        notEmpty: {
                            message: 'The confirm password is required and can\'t be empty'
                        },
                        identical: {
                            field: 'password',
                            message: 'The password and its confirm are not the same'
                        }
                    }
                },
                ages: {
                    validators: {
                        lessThan: {
                            value: 100,
                            inclusive: true,
                            message: 'The ages has to be less than 100'
                        },
                        greaterThan: {
                            value: 10,
                            inclusive: false,
                            message: 'The ages has to be greater than or equals to 10'
                        }
                    }
                },
                number:
                    {
                        validators: {
                            notEmpty: {
                                message: 'Không được để trống'
                            },
                            lessThan: {
                                value: 999999999999,
                                inclusive: true,
                                message: 'Số không hợp lệ'
                            },
                            greaterThan: {
                                value: 0,
                                inclusive: false,
                                message: 'Số phải lớn hơn 0'
                            }
                        }
                    },
                number_womsg:
                    {
                        validators: {
                            notEmpty: {
                                message: 'Không được để trống'
                            },
                            lessThan: {
                                value: 999999999999,
                                inclusive: true,
                                message: 'Số không hợp lệ'
                            },
                            greaterThan: {
                                value: 0,
                                inclusive: false,
                                message: 'Số phải lớn hơn 0'
                            }
                        }
                    },
                password: {
                    validators: {
                        identical: {
                            field: 'confirmPassword',
                            message: 'Xác thực mật khẩu không đúng'
                        }
                    }
                },
                confirmPassword: {
                    validators: {
                        identical: {
                            field: 'password',
                            message: 'Xác thực mật khẩu không đúng'
                        }
                    }
                }
            }
        });
    }

}