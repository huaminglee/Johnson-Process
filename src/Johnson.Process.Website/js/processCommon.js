$(function () {
    $("body").ajaxError(function (event, request, settings) {
        alert("提交错误");
    });
});



function selUser(userId, userLoginName, userRealName, multiSelect) {
    var url = edoc2BaseUrl + "/AppExt/Common/SelectOrgnization.aspx?userTree={show:true,multiSelect:" + multiSelect + ",current: true}&deptTree={show:false}";
    var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
    if (res != null && res.users != null) {
        var txtUserId = document.getElementById(userId);
        txtUserId.value = ""; 
        var txtUserRealName = document.getElementById(userRealName);
        txtUserRealName.value = "";
        if (userLoginName != "") {
            var txtUserLoginName = document.getElementById(userLoginName);
            txtUserLoginName.value = "";
        }
        var user;
        if (multiSelect) {
            for (var i = 0; i < res.users.length; i++) {
                user = res.users[i];
                txtUserId.value += "," + user._data.id;
                txtUserRealName.value += "," + user._data.userRealName;
                if (userLoginName != "")
                    txtUserLoginName.value += "," + user._data.loginName;
            }
            txtUserId.value = txtUserId.value != "" ? txtUserId.value.substr(1) : "";
            txtUserRealName.value = txtUserRealName.value != "" ? txtUserRealName.value.substr(1) : "";
            if (userLoginName != "")
                txtUserLoginName.value = txtUserLoginName.value != "" ? txtUserLoginName.value.substr(1) : "";
        }
        else if (res.users.length > 0) {
            user = res.users[res.users.length-1];
            txtUserId.value = user._data.id;
            txtUserRealName.value = user._data.userRealName;
            if (userLoginName != "")
                txtUserLoginName.value = user._data.loginName;
        }
    }
}
function openUploadWnd(url, width, height) {
    return window.open(url, "_blank", "height=" + height + "px, width=" + width + "px, toolbar= no, menubar=no, scrollbars=no, resizable=no, location=no, status=no");
}
function uploadFile(folderId, cb) {
    var win = openUploadWnd(edoc2BaseUrl + "/Document/File_UploadEx.aspx?folderid=" + folderId + "&multifile=true", 520, 360);
    //指定上传完毕的回调函数
    win.uploadCallback = function (filelist) {
        if(cb){
            cb(filelist);
        }
        win.close();
    };
}
function uploadSingleFile(folderId, cb) {
    var win = openUploadWnd(edoc2BaseUrl + "/Document/File_UploadEx.aspx?folderid=" + folderId + "&multifile=false", 520, 360);
    //指定上传完毕的回调函数
    win.uploadCallback = function (filelist) {
        if (cb && filelist && filelist.length) {
            cb(filelist[0]);
        }
        win.close();
    };
}
function deleteEDoc2File(fileId, cb) {
    $.post("EDoc2Controller.aspx?action=DeleteFile", { fileId: fileId }, function (data) {
        if (data.result != 0) {
            alert(data.message);
        }
        else {
            if (cb) {
                cb();
            }
        }
    });
}
function closeWindow() {
    if(!$.debug){
        window.open('', '_self', '');
        window.close();
    }
}
function fileActionFormater(fileId, row) {
    if (parseInt(fileId)) {
        var previewLink = $.format("{0}/Preview.aspx?FileId={1}", edoc2BaseUrl, fileId);
        var previewHtml = $.format("<a href='{0}' target='_blank' style='padding: 0.1em;'>预览</a>", previewLink);

        var downloadLink = $.format("{0}/Document/File_Download.aspx?file_Id={1}", edoc2BaseUrl, fileId);
        var downloadHtml = $.format("<a href='{0}' target='_blank' style='padding: 0.1em;'>下载</a>", downloadLink);

        return previewHtml + downloadHtml;
    }
    return "";
}

function priceFormatter(price) {
    if (price) {
        return $.formatNumber(price, { format: "#,##0.00", locale: "cn" });   
    }
    return "";
}

jQuery.fn.extend({
    edoc2SingleFile: function (folderId) {
        var thiz = this;
        thiz.find(".edoc2FileUploadButton").click(function () {
            uploadSingleFile(folderId, function (file) {
                thiz.find(".edoc2FileId").val(file.fileId);
                thiz.find(".edoc2FileName").val(file.fileName);
            });
            return false;
        });
        thiz.find(".edoc2FileDeleteButton").click(function () {
            var fileId = thiz.find(".edoc2FileId").val();
            deleteEDoc2File(fileId, function () {
                thiz.find(".edoc2FileId").val("");
                thiz.find(".edoc2FileName").val("");
            });
            return false;
        });
    },
    attachmentsGrid: function () {
        var thiz = this;
        this.datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            toolbar: [{
                text: '上传',
                iconCls: 'icon-add',
                handler: function () {
                    uploadFile(tempFolderId, function (files) {
                        $.each(files, function (i, file) {
                            thiz.datagrid("appendRow", { fileName: file.fileName, fileId: file.fileId });
                        });
                    });
                }
            }, {
                text: '删除',
                iconCls: 'icon-remove',
                handler: function () {
                    if (!confirm("确实要删除吗?")) {
                        return;
                    }
                    var row = thiz.datagrid('getSelected');
                    if (row) {
                        $.post("EDoc2Controller.aspx?action=DeleteFile", { fileId: row.fileId }, function (data) {
                            if (data.result != 0) {
                                alert(data.message);
                            }
                            else {
                                var index = thiz.datagrid('getRowIndex', row);
                                thiz.datagrid('deleteRow', index);
                            }
                        });
                    }
                }
            }]
        });
    },
    eidtableGrid: function (addDialog, editDialog, addedRow, editedRow, deletedRow) {
        var thiz = this;

        var addDialogForm = addDialog.find("form");
        var addDialogFormValue = addDialogForm.getFormValue();
        addDialog.find(".buttons input").button()
        .eq(0).click(function () {
            if (!addDialogForm.valid()) {
                return;
            }
            var fromValue = addDialogForm.getFormValue();
            thiz.datagrid("appendRow", fromValue);
            if (addedRow) {
                addedRow(fromValue);
            }
            addDialogForm.setFormValue(addDialogFormValue);
            addDialogForm.find("input:first").focus();
            return false;
        }).end()
        .eq(1).click(function () {
            if (!addDialogForm.valid()) {
                return;
            }
            var fromValue = addDialogForm.getFormValue();
            thiz.datagrid("appendRow", fromValue);
            if (addedRow) {
                addedRow(fromValue);
            }
            addDialogForm.setFormValue(addDialogFormValue);
            addDialog.dialog("close");
            return false;
        }).end()
        .eq(2).click(function () {
            addDialog.dialog("close");
            return false;
        });

        var editDialogForm = editDialog.find("form");
        editDialog.find(".buttons input").button()
        .eq(0).click(function () {
            if (!editDialogForm.valid()) {
                return;
            }
            var fromValue = editDialogForm.getFormValue();
            var row = thiz.datagrid('getSelected');
            var index = thiz.datagrid('getRowIndex', row);
            thiz.datagrid("updateRow", { index: index, row: fromValue });
            if (editedRow) {
                editedRow(fromValue);
            }
            editDialog.dialog("close");
            return false;
        }).end()
        .eq(1).click(function () {
            editDialog.dialog("close");
            return false;
        });

        var editToolButtonId = this.attr("id") + "_editToolButton";
        var deleteToolButtonId = this.attr("id") + "_deleteToolButton";
        function refreshGridToolbar() {
            var rows = thiz.datagrid('getSelections');
            if (rows.length == 1) {
                $("#" + editToolButtonId).linkbutton('enable');
            }
            else {
                $("#" + editToolButtonId).linkbutton('disable');
            }
            if (rows.length) {
                $("#" + deleteToolButtonId).linkbutton('enable');
            }
            else {
                $("#" + deleteToolButtonId).linkbutton('disable');
            }
        }

        thiz.datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false,
            onSelect: refreshGridToolbar,
            onUnselect: refreshGridToolbar,
            onDblClickRow: function () {
                var selected = thiz.datagrid('getSelected');
                editDialog.find("form").setFormValue(selected);
                editDialog.dialog("open");
            },
            toolbar: [{
                text: '添加',
                iconCls: 'icon-add',
                handler: function () {
                    addDialog.find("form").setFormValue(addDialogFormValue);
                    addDialog.dialog("open");
                }
            }, {
                id: editToolButtonId,
                text: '编辑',
                disabled: true,
                iconCls: 'icon-edit',
                handler: function () {
                    var selected = thiz.datagrid('getSelected');
                    editDialog.find("form").setFormValue(selected);
                    editDialog.dialog("open");
                }
            }, {
                id: deleteToolButtonId,
                text: '删除',
                disabled: true,
                iconCls: 'icon-remove',
                handler: function () {
                    if (!confirm("确实要删除吗?")) {
                        return;
                    }
                    var row = thiz.datagrid('getSelected');
                    if (row) {
                        var index = thiz.datagrid('getRowIndex', row);
                        thiz.datagrid('deleteRow', index);
                        if (deletedRow) {
                            deletedRow(row);
                        }
                    }
                }
            }]
        });
    },
    eidtableOnlyGrid: function (editDialog, editedRow) {
        var thiz = this;

        var editDialogForm = editDialog.find("form");
        editDialog.find(".buttons input").button()
        .eq(0).click(function () {
            if (!editDialogForm.valid()) {
                return;
            }
            var fromValue = editDialogForm.getFormValue();
            var row = thiz.datagrid('getSelected');
            var index = thiz.datagrid('getRowIndex', row);
            for (var valueName in fromValue) {
                if (typeof (row[valueName]) != "undefined") {
                    row[valueName] = fromValue[valueName];
                }
            }
            thiz.datagrid("updateRow", { index: index, row: row });
            if (editedRow) {
                editedRow(fromValue);
            }
            editDialog.dialog("close");
            return false;
        }).end()
        .eq(1).click(function () {
            editDialog.dialog("close");
            return false;
        });

        var editToolButtonId = this.attr("id") + "_editToolButton";
        function refreshGridToolbar() {
            var rows = thiz.datagrid('getSelections');
            if (rows.length == 1) {
                $("#" + editToolButtonId).linkbutton('enable');
            }
            else {
                $("#" + editToolButtonId).linkbutton('disable');
            }
        }

        thiz.datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false,
            onSelect: refreshGridToolbar,
            onUnselect: refreshGridToolbar,
            onDblClickRow: function () {
                var selected = thiz.datagrid('getSelected');
                editDialog.find("form").setFormValue(selected);
                editDialog.dialog("open");
            },
            toolbar: [{
                id: editToolButtonId,
                text: '编辑',
                disabled: true,
                iconCls: 'icon-edit',
                handler: function () {
                    var selected = thiz.datagrid('getSelected');
                    editDialog.find("form").setFormValue(selected);
                    editDialog.dialog("open");
                }
            }]
        });
    }
});

