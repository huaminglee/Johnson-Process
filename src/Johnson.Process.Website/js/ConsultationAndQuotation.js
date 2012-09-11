
jQuery.fn.extend({
    productMarketingSalesTPDialog: function () {
        this.dialog(({ autoOpen: false, modal: true, width: 500 }));
        var editDialogForm = this.find("form");
        editDialogForm.find("input[name='marketingWithSalesTP']").keyup(function () {
            var val = parseFloat($(this).val());
            if (val) {
                editDialogForm.find("input[name='marketingWithoutSalesTP']").val((val / 1.17).toFixed(2));
                var quantity = parseInt(editDialogForm.find("input[name='quantity']").val());
                editDialogForm.find("input[name='marketingTotalSalesTP']").val((val * quantity).toFixed(2));
            }
            else {
                editDialogForm.find("input[name='marketingTotalSalesTP']").val("");
                editDialogForm.find("input[name='marketingWithoutSalesTP']").val("");
            }
        });
        return this;
    },
    productCsdSalesTPDialog: function () {
        this.dialog(({ autoOpen: false, modal: true, width: 500 }));
        var editDialogForm = this.find("form");
        editDialogForm.find("input[name='csdWithSalesTP']").keyup(function () {
            var val = parseFloat($(this).val());
            if (val) {
                editDialogForm.find("input[name='csdWithoutSalesTP']").val((val / 1.17).toFixed(2));
                var quantity = parseInt(editDialogForm.find("input[name='quantity']").val());
                editDialogForm.find("input[name='csdTotalSalesTP']").val((val * quantity).toFixed(2));
            }
            else {
                editDialogForm.find("input[name='csdTotalSalesTP']").val("");
                editDialogForm.find("input[name='csdWithoutSalesTP']").val("");
            }
        });
        return this;
    },
    productSalesTpGrid: function (editDialog, editedRow) {
        var thiz = this;
        var editRowIndex;

        var editDialogForm = editDialog.find("form");
        editDialog.find(":submit, :button").button()
        .eq(0).click(function () {
            if (!editDialogForm.valid()) {
                return;
            }
            var fromValue = editDialogForm.getFormValue();
            var rows = thiz.datagrid("getData").rows;
            var row = rows[editRowIndex];
            for (var valueName in fromValue) {
                if (typeof (row[valueName]) != "undefined") {
                    row[valueName] = fromValue[valueName];
                }
            }
            thiz.datagrid("updateRow", { index: editRowIndex, row: row });
            if (typeof (editedRow) != "undefined") {
                editedRow(fromValue);
            }
            if (rows.length - 1 <= editRowIndex) {
                editDialog.dialog("close");
                return false;
            }
            editRowIndex++;
            editDialog.find("form").setFormValue(rows[editRowIndex]);
            editDialog.find("input[readonly!='readonly']").first().focus();
            return false;
        }).end()
        .eq(1).click(function () {
            if (!editDialogForm.valid()) {
                return;
            }
            var fromValue = editDialogForm.getFormValue();
            var rows = thiz.datagrid("getData").rows;
            var row = rows[editRowIndex];
            for (var valueName in fromValue) {
                if (typeof (row[valueName]) != "undefined") {
                    row[valueName] = fromValue[valueName];
                }
            }
            thiz.datagrid("updateRow", { index: editRowIndex, row: row });
            if (typeof (editedRow) != "undefined") {
                editedRow(fromValue);
            }
            editDialog.dialog("close");
            return false;
        }).end()
        .eq(2).click(function () {
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

        function eidtSelectedRow() {
            var selected = thiz.datagrid('getSelected');
            editRowIndex = thiz.datagrid('getRowIndex', selected);
            editDialog.find("form").setFormValue(selected);
            editDialog.dialog("open");
            editDialog.find("input[readonly!='readonly']").first().focus();
        }

        thiz.datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false,
            onSelect: refreshGridToolbar,
            onUnselect: refreshGridToolbar,
            onDblClickRow: eidtSelectedRow,
            toolbar: [{
                id: editToolButtonId,
                text: '编辑',
                disabled: true,
                iconCls: 'icon-edit',
                handler: eidtSelectedRow
            }]
        });
    },
    productMarketingSalesTpGridValid: function () {
        var invalidSalesTpProductModels = "";
        var firstAppend = true;
        var productsSalesTp = this.datagrid("getData");
        $.each(productsSalesTp.rows, function (i, salesTP) {
            if (!salesTP.marketingWithSalesTP) {
                if (firstAppend) {
                    invalidSalesTpProductModels = salesTP.productModel;
                    firstAppend = false;
                }
                else {
                    invalidSalesTpProductModels += "," + salesTP.productModel;
                }
            }
        });
        if (invalidSalesTpProductModels) {
            alert($.format("请正确填写{0}的报价信息", invalidSalesTpProductModels));
            return false;
        }
        return true;
    },
    productCsdSalesTpGridValid: function () {
        var invalidSalesTpProductModels = "";
        var firstAppend = true;
        var productsSalesTp = this.datagrid("getData");
        $.each(productsSalesTp.rows, function (i, salesTP) {
            if (!salesTP.csdWithSalesTP) {
                if (firstAppend) {
                    invalidSalesTpProductModels = salesTP.productModel;
                    firstAppend = false;
                }
                else {
                    invalidSalesTpProductModels += "," + salesTP.productModel;
                }
            }
        });
        if (invalidSalesTpProductModels) {
            alert($.format("请正确填写{0}的报价信息", invalidSalesTpProductModels));
            return false;
        }
        return true;
    },
    sumProductQuantity: function () {
        var gridData = this.datagrid('getData');
        var quantity = 0;
        $.each(gridData.rows, function (i, data) {
            var parsedInt = parseInt(data.quantity);
            quantity += parsedInt;
        });
        var rows = this.datagrid('getFooterRows');
        rows[0]['productModel'] = '合计';
        rows[0]['quantity'] = quantity;
        this.datagrid('reloadFooter');
    },
    sumProductQuantityAndTP: function () {
        var gridData = this.datagrid('getData');
        var quantity = 0;
        var marketingTotalSalesTP = 0;
        $.each(gridData.rows, function (i, data) {
            quantity += parseInt(data.quantity);
            marketingTotalSalesTP += parseInt(data.marketingTotalSalesTP);
        });
        var rows = this.datagrid('getFooterRows');
        rows[0]['productModel'] = '合计';
        rows[0]['quantity'] = quantity;
        rows[0]['marketingTotalSalesTP'] = marketingTotalSalesTP;
        this.datagrid('reloadFooter');
    },
    sumProductQuantityAndCsdTP: function () {
        var gridData = this.datagrid('getData');
        var quantity = 0;
        var csdTotalSalesTP = 0;
        $.each(gridData.rows, function (i, data) {
            quantity += parseInt(data.quantity);
            csdTotalSalesTP += parseInt(data.csdTotalSalesTP);
        });
        var rows = this.datagrid('getFooterRows');
        rows[0]['productModel'] = '合计';
        rows[0]['quantity'] = quantity;
        rows[0]['csdTotalSalesTP'] = csdTotalSalesTP;
        this.datagrid('reloadFooter');
    }
});
