<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Delivery_Custom.aspx.cs" Inherits="Johnson.Process.Website.Delivery_Custom" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/DeliveryDetails.ascx" TagName="DeliveryDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>货期管理-客服部答复</title>
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
    <johnson:Header runat="server" HeaderTitle="货期管理-客服部答复" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>
    <form id="basicInfoForm">
        <johnson:DeliveryDetails ShowCsdAuditUser="false" runat="server" ID="deliveryDetails"></johnson:DeliveryDetails>
    </form>
    
    <table class="formInfo">
        <tr>
            <td class="labelCol" style="width: 200px">
                是否需要物流部答复<span style="color: Red">*</span>
            </td>
            <td class="textCol" colspan="3">
                <input type="radio" name="needLogisticsReply" value="true" id="yesNeedLogisticsReply"/><label>是</label>
                <input type="radio" checked="checked" value="false" name="needLogisticsReply" id="noNeedLogisticsReply"/><label>否</label>
            </td>
        </tr>
    </table>

    <form id="logisticsForm" style="display:none">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    物流部工程师<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <input type="text" id="txtLogisticsRealName" name="logEngineerName" class="textInput required" readonly="readonly"/>
                    <input type="text" style="display: none" id="hidLogisticsReplyUser" name="logEngineer" value=""/>
                    <%--<input type="text" id="txtLogisticsRealName" name="logEngineerName" class="textInput required" />
                    <input type="text" id="hidLogisticsReplyUser" name="logEngineer" value=""/>--%>
                    <input type="hidden" id="hidLogisticsUserId" value="0"/>
                    <input type="button" onclick="selUser('hidLogisticsUserId','hidLogisticsReplyUser','txtLogisticsRealName',false);" value="选择" class="btnCommon" />
                </td>
            </tr>
        </table>
    </form>

    <form id="replyForm">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    答复<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <textarea name="csdReply" class="textInput required" style="width: 500px;" rows="3"></textarea>
                </td>
            </tr>
        </table>
    </form>

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
                        <textarea name="submitRemark" class="textInput required" style="width: 500px;" rows="3"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>

    <div style="padding: 2em 0 0 30em;">
        <input type="button" id="btnSubmit" value="提交" />
        <input type="button" id="btnReturn" value="退回" />
    </div>
</body>
</html>
<script language="javascript" type="text/javascript">
    var taskId = "<%= this.TaskId %>";
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    function fileActionFormater(fileId, row) {
        var previewLink = $.format("{0}/Preview.aspx?FileId={1}", edoc2BaseUrl, fileId);
        var previewHtml = $.format("<a href='{0}' target='_blank' style='padding: 0.1em;'>预览</a>", previewLink);

        var downloadLink = $.format("{0}/Document/File_Download.aspx?file_Id={1}", edoc2BaseUrl, fileId);
        var downloadHtml = $.format("<a href='{0}' target='_blank' style='padding: 0.1em;'>下载</a>", downloadLink);

        return previewHtml + downloadHtml;
    }

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
            $("#basicInfoForm").setFormValue(data).setFormReadOnly(true);
            
            var materials = { "total": data.materials.length, "rows": data.materials, "footer": [{ "sapNo": "合计", "quantity": 0}] };
            $('#grid').datagrid('loadData', materials);
            sumQuantity();

            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            if (data.remarks) {
                $('#remarks').datagrid('loadData', data.remarks);
            }
            $("#replyForm,#logisticsForm").setFormValue(data);
        });

        $("#logisticsForm, #replyForm, #remarkForm").validate();

        $("#yesNeedLogisticsReply").click(function () {
            $("#logisticsForm").show();
            $("#replyForm").hide();
        });

        $("#noNeedLogisticsReply").click(function () {
            $("#logisticsForm").hide();
            $("#replyForm").show();
        });

        $("#grid").datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false
        });

        $("#attachments").datagrid({
            rownumbers: true
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#basicInfoForm").validAndFocus()) {
                return;
            }
            var needLogisticsReply = $("#yesNeedLogisticsReply").attr("checked") == "checked";
            if (needLogisticsReply) {
                if (!$("#logisticsForm").validAndFocus()) {
                    return;
                }
            }
            else {
                if (!$("#replyForm").validAndFocus()) {
                    return;
                }
            }
            var logisticsObj = $.getFormValue("#logisticsForm");
            var replyObj = $.getFormValue("#replyForm");
            var valueObj = $.getFormValue("#basicInfoForm");
            var gridData = $('#grid').datagrid('getData');
            valueObj.materials = gridData.rows;
            var attachments = $('#attachments').datagrid('getData');
            valueObj.files = attachments.rows;
            var remarkObj = $.getFormValue("#remarkForm");
            valueObj.logEngineer = logisticsObj.logEngineer;
            valueObj.logEngineerName = logisticsObj.logEngineerName;
            valueObj.needLogisticsReply = needLogisticsReply;
            valueObj.csdReply = replyObj.csdReply;
            valueObj.submitRemark = remarkObj.submitRemark;
            var objJson = $.toJSON(valueObj);

            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("DeliveryController.aspx?action=Custom_Service_Submit", { taskId: taskId, delivery: objJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });

        $("#btnReturn").button().click(function () {
            if (!$("#remarkForm").validAndFocus()) {
                return;
            }
            if (!$("#basicInfoForm").validAndFocus()) {
                return;
            }
            var valueObj = $.getFormValue("#basicInfoForm");
            var gridData = $('#grid').datagrid('getData');
            valueObj.materials = gridData.rows;
            var attachments = $('#attachments').datagrid('getData');
            valueObj.files = attachments.rows;
            var remarkObj = $.getFormValue("#remarkForm");
            valueObj.submitRemark = remarkObj.submitRemark;
            var objJson = $.toJSON(valueObj);

            if (!confirm("您确实要退回吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("DeliveryController.aspx?action=return", { taskId: taskId, delivery: objJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("退回成功");
                    closeWindow();
                }
            });
        });

        $("#remarks").datagrid({
            rownumbers: true
        });
    });
</script>


