
(function($) {
$.widget(
    "ui.processDataGrid", {
        options: {
            addDialog: null, 
            editDialog: null,
            addingRow: null,
            addedRow: null,
            editingRow: null,
            editedRow: null, 
            deletedRow: null,
            initingEditForm: null,
            initedEditForm: null
        },
        _create: function(){
            var element = this.element;
            this.eidtableGrid(this.options.addDialog, this.options.editDialog);
        },
        eidtableGrid: function (addDialog, editDialog) {
            var thiz = this;
            var element = this.element;

            var addDialogForm = addDialog.find("form");
            var addDialogFormValue = addDialogForm.getFormValue();
            addDialog.find(".buttons input").button()
            .eq(0).click(function () {
                if (!addDialogForm.valid()) {
                    return;
                }
                var fromValue = addDialogForm.getFormValue();
                var args = { row: fromValue, addDialogForm: addDialogForm };
                thiz._trigger("addingRow", null, args);
                element.datagrid("appendRow", fromValue);
                thiz._trigger("addedRow", null, args);
                addDialogForm.setFormValue(addDialogFormValue);
                addDialogForm.find("input:first").focus();
                return false;
            }).end()
            .eq(1).click(function () {
                if (!addDialogForm.valid()) {
                    return;
                }
                var fromValue = addDialogForm.getFormValue();
                var args = { row: fromValue, addDialogForm: addDialogForm };
                thiz._trigger("addingRow", null, args);
                element.datagrid("appendRow", fromValue);
                thiz._trigger("addedRow", null, args);
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
                var row = element.datagrid('getSelected');
                var index = element.datagrid('getRowIndex', row);
                var args = { index: index, row: fromValue, editDialogForm: editDialogForm };
                thiz._trigger("editingRow", null, args);
                element.datagrid("updateRow", { index: index, row: fromValue });
                thiz._trigger("editedRow", null, args);
                editDialog.dialog("close");
                return false;
            }).end()
            .eq(1).click(function () {
                editDialog.dialog("close");
                return false;
            });

            var editToolButtonId = element.attr("id") + "_editToolButton";
            var deleteToolButtonId = element.attr("id") + "_deleteToolButton";
            function refreshGridToolbar() {
                var rows = element.datagrid('getSelections');
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

            element.datagrid({
                rownumbers: true,
                showFooter: true,
                singleSelect: true,
                nowrap: false,
                onSelect: refreshGridToolbar,
                onUnselect: refreshGridToolbar,
                onDblClickRow: function () {
                    var selected = element.datagrid('getSelected');
                    var args = { row: selected, editDialog: editDialog};
                    thiz._trigger("initingEditForm", null, args);
                    editDialog.find("form").setFormValue(selected);
                    editDialog.dialog("open");
                    thiz._trigger("initedEditForm", null, args);
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
                        var selected = element.datagrid('getSelected');
                        var args = { row: selected};
                        thiz._trigger("initingEditForm", null, args);
                        editDialog.find("form").setFormValue(selected);
                        editDialog.dialog("open");
                        thiz._trigger("initedEditForm", null, args);
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
                        var row = element.datagrid('getSelected');
                        if (row) {
                            var index = element.datagrid('getRowIndex', row);
                            var args = { index: index, row: row};
                            thiz._trigger("deletingRow", null, args);
                            element.datagrid('deleteRow', index);
                            thiz._trigger("deletedRow", null, args);
                        }
                    }
                }]
            });
        }
    }
)
}(jQuery));