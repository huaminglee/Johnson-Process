﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotation_CID.aspx.cs" Inherits="Johnson.Process.Website.ConsultationAndQuotation_CID" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationDetails.ascx" TagName="ConsultationAndQuotationDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationProductDetails.ascx" TagName="ConsultationAndQuotationProductDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>技术咨询及报价-CID评审</title>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/icon.css" />
	<script type="text/javascript" src="jquery-easyui/jquery.easyui.min.js"></script>
    <script src="js/jquery.tmpl.min.js" type="text/javascript"></script>
    <script src="js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="jqueryui/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="js/processCommon.js" type="text/javascript"></script>
    <script src="js/ConsultationAndQuotation.js" type="text/javascript"></script>
    <script src="js/jqueryExtend.js" type="text/javascript"></script>
    <script src="js/jquery.json-2.3.js" type="text/javascript"></script>
    <link href="jqueryui/cupertino/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/default.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <%--head --%>
    <johnson:Header runat="server" HeaderTitle="技术咨询及报价-CID评审" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:ConsultationAndQuotationDetails runat="server" ID="consultationAndQuotationDetails"></johnson:ConsultationAndQuotationDetails>
    </form>

    <form id="cidForm">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    意见<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <textarea name="cidReply" class="textInput required" style="width: 500px;" rows="3"></textarea>
                </td>
            </tr>
        </table>
    </form>

    <div style="margin-top: 1em;">
        <table id="grid" style="width:900px;height:auto" title="产品信息">
		    <thead>
			    <tr>
				    <th field="productModel" resizable="false" width="300">产品型号</th>
                    <th field="quantity" resizable="false" width="100">数量</th>
                    <th field="remark" resizable="false" width="400">SQ描述</th>
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
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";
    $(function () {
        $.get("ConsultationAndQuotationController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data);
            $("#cidForm").setFormValue(data);
            var products = { "total": data.products.length, "rows": data.products, "footer": [{ "productModel": "合计", "quantity": 0}] };
            $('#grid').datagrid('loadData', products);
            $("#grid").sumProductQuantity();
            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            if (data.remarks) {
                $('#remarks').datagrid('loadData', data.remarks);
            }

            $("#basicInfoForm").setFormReadOnly(true);
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#cidForm").validAndFocus()) {
                return;
            }
            var cidReply = $("#cidForm").getFormValue().cidReply;
            var submitRemark = $("#remarkForm").getFormValue().submitRemark;

            var args = {
                taskId: taskId, cidReply: cidReply, submitRemark: submitRemark
            };
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("ConsultationAndQuotationController.aspx?action=cidSubmit", { formJson: argsJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });

        $("#cidForm").validate();

        $("#grid, #remarks, #attachments").datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false
        });
    })
</script>