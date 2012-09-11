<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Delivery_Start_Return.aspx.cs" Inherits="Johnson.Process.Website.Delivery_Start_Return" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/DeliveryDetails.ascx" TagName="DeliveryDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/MaterialDetails.ascx" TagName="MaterialDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>货期管理-办事处</title>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/icon.css" />
	<script type="text/javascript" src="jquery-easyui/jquery.easyui.min.js"></script>

    <script src="js/jquery.tmpl.min.js" type="text/javascript"></script>
    <script src="js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="jqueryui/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="js/processCommon.js" type="text/javascript"></script>
    <script src="js/jqueryExtend.js" type="text/javascript"></script>
    <script src="js/jquery.json-2.3.js" type="text/javascript"></script>
    <link href="jqueryui/cupertino/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/default.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <%--head --%>
    <johnson:Header runat="server" HeaderTitle="货期管理-办事处" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>
    <form id="basicInfoForm">
        <johnson:DeliveryDetails runat="server" ID="deliveryDetails"></johnson:DeliveryDetails>
    </form>
    <div id="addMaterialInfoFormDialog" title="添加机组信息">
        <form>
            <johnson:MaterialDetails runat="server" ID="materialDetails1"></johnson:MaterialDetails>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定并继续"/>
                <input type="button" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>
    <div id="editMaterialInfoFormDialog" title="编辑机组信息">
        <form>
            <johnson:MaterialDetails runat="server" ID="materialDetails"></johnson:MaterialDetails>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>

    <div style="margin-top: 1em;">
        <table id="grid" style="width:900px;height:auto" title="机组信息">
		    <thead>
			    <tr>
				    <th field="sapNo" resizable="false" width="300">SAP Materials No</th>
                    <th field="quantity" resizable="false" width="100">数量</th>
                    <th field="remark" resizable="false" width="400">备注</th>
			    </tr>
		    </thead>
	    </table>
    </div>
    
    <div style="margin-top: 1em;">
        <table id="attachments" style="width:900px;height:auto" title="附件信息">
		    <thead>
			    <tr> 
				    <th field="fileName" resizable="false" width="200">附件名称</th>
                    <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			    </tr>
		    </thead>
	    </table>
    </div>

    <div style="margin-top: 1em;">
        <table id="remarks" style="width:900px;height:auto" title="备注信息">
		    <thead>
			    <tr> 
				    <th field="remarkStepName" resizable="false" width="200">流程步骤</th>
                    <th field="remarkUserName" resizable="false" width="100">备注人</th>
                    <th field="remarkTime" resizable="false" width="130">备注日期</th>
                    <th field="remark" resizable="false" width="300">备注</th>
			    </tr>
		    </thead>
	    </table>
    </div>

    <div class="panel-header" style="margin-top: 2em;"><div class="panel-title">备注</div></div>
    <form id="remarkForm">
        <table class="formInfo">
            <tbody >
                <tr>
                    <td class="labelCol" style="width: 200px">
                        备注
                    </td>
                    <td class="textCol">
                        <textarea name="submitRemark" class="textInput" style="width: 500px;" rows="3"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>

    <div style="padding: 2em 0 0 30em;">
        <input type="button" id="btnSubmit" value="提交" />
    </div>
</body>
</html>
<script language="javascript" type="text/javascript">
    var taskId = "<%= this.TaskId %>";
    var tempFolderId = "<%= this.ProcessFolderId %>";
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    
    function sumQuantity() {
        var gridData = $('#grid').datagrid('getData');
        var quantity = 0;
        $.each(gridData.rows, function (i, data) {
            var parsedInt = parseInt(data.quantity);
            quantity += parsedInt;
        });
        var rows = $('#grid').datagrid('getFooterRows');
        rows[0]['sapNo'] = '合计';
        rows[0]['quantity'] = quantity;
        $('#grid').datagrid('reloadFooter');
    }

    $(function () {
        //初始化
        $.get("DeliveryController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data);
            $("#txtBookDate,#txtRequestOutDate").datepicker({ changeMonth: true, changeYear: true });

            var materials = { "total": data.materials.length, "rows": data.materials, "footer": [{ "sapNo": "合计", "quantity": 0}] };
            $('#grid').datagrid('loadData', materials);
            sumQuantity();

            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            if (data.remarks) {
                $('#remarks').datagrid('loadData', data.remarks);
            }
        });

        $("#basicInfoForm").validate();
        var addDialog = $("#addMaterialInfoFormDialog").dialog({ autoOpen: false, modal: true, width: 500 });
        var editDialog = $("#editMaterialInfoFormDialog").dialog({ autoOpen: false, modal: true, width: 500 });
        $("#grid").eidtableGrid(addDialog, editDialog, sumQuantity, sumQuantity, sumQuantity);

        $("#btnSubmit").button().click(function () {
            if (!$("#basicInfoForm").validAndFocus()) {
                return;
            }
            var gridData = $('#grid').datagrid('getData');
            if (gridData.rows.length == 0) {
                alert("请添加机组信息");
                return;
            }
            var valueObj = $.getFormValue("#basicInfoForm");
            valueObj.materials = gridData.rows;
            var attachments = $('#attachments').datagrid('getData');
            valueObj.files = attachments.rows;
            var remarkObj = $.getFormValue("#remarkForm");
            valueObj.submitRemark = remarkObj.submitRemark;
            var objJson = $.toJSON(valueObj);

            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("DeliveryController.aspx?action=submit", { taskId: taskId, delivery: objJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });


        $("#attachments").attachmentsGrid();

        $("#remarks").datagrid({
            rownumbers: true
        });
    });
</script>