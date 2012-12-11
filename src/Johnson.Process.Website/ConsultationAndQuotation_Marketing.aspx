﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotation_Marketing.aspx.cs" Inherits="Johnson.Process.Website.ConsultationAndQuotation_Marketing" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationDetails.ascx" TagName="ConsultationAndQuotationDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationProductDetails.ascx" TagName="ConsultationAndQuotationProductDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationProductMarketingSalesTPDetails.ascx" TagName="ConsultationAndQuotationProductSalesTPDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>技术咨询及报价-市场部答复</title>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/icon.css" />
	<script type="text/javascript" src="jquery-easyui/jquery.easyui.min.js"></script>
    <script src="js/jquery.tmpl.min.js" type="text/javascript"></script>
    <script src="js/jshashtable.js" type="text/javascript"></script>
    <script src="js/jquery.numberformatter-1.2.3.min.js" type="text/javascript"></script>
    <script src="js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="jqueryui/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="js/processCommon.js" type="text/javascript"></script>
    <script src="js/ConsultationAndQuotation.js" type="text/javascript"></script>
    <script src="js/jqueryExtend.js" type="text/javascript"></script>
    <script src="js/jquery.json-2.3.js" type="text/javascript"></script>
    <link href="jqueryui/cupertino/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .datagrid-cell { font-size: 1.2em; }
    </style>
</head>
<body>
    <%--head --%>
    <johnson:Header runat="server" HeaderTitle="技术咨询及报价-市场部答复" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:ConsultationAndQuotationDetails runat="server" ID="consultationAndQuotationDetails"></johnson:ConsultationAndQuotationDetails>
    </form>
    <table class="formInfo">
        <tr>
            <td class="labelCol" style="width: 200px">
                是否需要CSD答复<span style="color: Red">*</span>
            </td>
            <td class="textCol" colspan="3">
                <input type="radio" name="needCsdReply" value="true" id="yesNeedCsdReply"/><label>是</label>
                <input type="radio" checked="checked" value="false" name="needCsdReply" id="noNeedCsdReply"/><label>否</label>
            </td>
        </tr>
    </table>

    <form id="csdForm" style="display:none">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    CSD联系人<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <input type="text" id="txtCsdRealName" name="csdEngineerName" class="textInput required" readonly="readonly"/>
                    <input type="text" style="display: none" id="hidCsdReplyUserAccount" name="csdEngineerAccount" value=""/>
                    <%--<input type="text" id="txtCsdRealName" name="csdEngineerName" class="textInput required" />
                    <input type="text" id="hidCsdReplyUserAccount" name="csdEngineerAccount" value=""/>--%>
                    <input type="hidden" id="hidCsdUserId" value="0"/>
                    <input type="button" onclick="selUser('hidCsdUserId','hidCsdReplyUserAccount','txtCsdRealName',false);" value="选择" class="btnCommon" />
                </td>
            </tr>
        </table>
    </form>

    <form id="marketingForm">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    答复
                </td>
                <td class="textCol" >
                    <textarea name="marketingReply" class="textInput" style="width: 500px;" rows="3"></textarea>
                </td>
            </tr>
            <tr>
                <td class="labelCol" style="width: 200px">
                    邮件抄送给
                </td>
                <td class="textCol userEmailMultiSelect" >
                    <input type="text" name="toCsdEmailAddress" class="textInput multiemail" style="width: 500px;"/>
                    <input type="button" value="选择" class="btnCommon" />
                </td>
            </tr>
        </table>
    </form>
    
    <div id="editProductDialog" title="非标报价">
        <form>
            <johnson:ConsultationAndQuotationProductSalesTPDetails runat="server" ID="editConsultationAndQuotationProductSalesTPDetails"></johnson:ConsultationAndQuotationProductSalesTPDetails>
            <div style="margin-top:1em">
                <input type="submit" value="确定并继续"/>
                <input type="submit" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>

    <div id="pnlProducts" style="margin-top: 1em;">
        <table id="gridProduct" style="width:900px;height:auto" title="产品信息">
		    <thead>
		        <tr>
				    <th field="productModel" resizable="false" width="150">产品型号</th>
                    <th field="quantity" resizable="false" width="50">数量</th>
                    <th field="remark" resizable="false" width="200">SQ描述</th>
			    </tr>
            </thead>
	    </table>
    </div>

    <div id="pnlProductsSalesTP">
        <div style="margin-top: 1em;">
            <table id="gridProductSalesTp" style="width:900px;height:auto" title="(第一部分) 非标报价">
		        <thead>
			        <tr>
				        <th field="productModel" resizable="false" width="150">产品型号</th>
                        <th field="quantity" resizable="false" width="50">数量</th>
                        <th field="remark" resizable="false" width="200">SQ描述</th>
                        <th field="marketingWithoutSalesTP" resizable="false" width="155" formatter="priceFormatter">Sales TP/(Without VAT)</th>
                        <th field="marketingWithSalesTP" resizable="false" width="140" formatter="priceFormatter">Sales TP/(With VAT)</th>
                        <th field="marketingTotalSalesTP" resizable="false" width="120" formatter="priceFormatter">Sales TP/总和</th>
			        </tr>
		        </thead>
	        </table>
        </div>
        <div class="panel-header" ><div class="panel-title">(第二部分) 非标货期</div></div>
        <form id="productLeadTimeForm">
            <table class="formInfo">
                <tr >
                    <td class="labelCol" style="width: 200px" >
                        在对应机组Lead Time基础上增加<span style="color: Red">*</span>
                    </td>
                    <td >
                        <input name="leadTime" class="textInput required digits" style="width: 200px;"/>天
                    </td>
                </tr>
                <tr >
                    <td class="labelCol" style="width: 200px">
                        备注
                    </td>
                    <td >
                        <textarea name="leadTimeRemark" class="textInput" style="width: 500px;" rows="3"></textarea>
                    </td>
                </tr>
            </table>
        </form>
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
        <input type="button" id="btnReturn" value="退回" />
    </div>
</body>
</html>
<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";
    var tempFolderId = "<%= this.ProcessFolderId %>";

    function sumProductQuantityAndTP(){
        $("#gridProductSalesTp").sumProductQuantityAndTP();
    }
    
    $(function () {
        $.get("ConsultationAndQuotationController.aspx?action=marketingGet", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm, #marketingForm, #productLeadTimeForm, #csdForm").setFormValue(data);
            var products = { "total": data.products.length, "rows": data.products, "footer": [{ "productModel": "合计", "quantity": 0}] };
            var salesTpProducts = { "total": data.products.length, "rows": data.products, "footer": [{ "productModel": "合计", "quantity": 0, "marketingTotalSalesTP": 0}] };
            $('#gridProduct').datagrid('loadData', products);
            $("#gridProductSalesTp").datagrid('loadData', salesTpProducts);
            $("#gridProduct").sumProductQuantity();
            sumProductQuantityAndTP();
            setTimeout(function () { $("#pnlProducts").hide(); }, 100);
            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            if (data.remarks) {
                $('#remarks').datagrid('loadData', data.remarks);
            }

            $("#basicInfoForm").setFormReadOnly(true);
        });

        $("#btnSubmit").button().click(function () {
            var needCsdReply = $("#yesNeedCsdReply").attr("checked") == "checked";
            if (needCsdReply) {
                if (!$("#csdForm").validAndFocus()) {
                    return;
                }
            }
            else {
                if (!$("#marketingForm").validAndFocus()) {
                    return;
                }
                if (!$("#gridProductSalesTp").productMarketingSalesTpGridValid()) {
                    return;
                }
                if (!$("#productLeadTimeForm").validAndFocus()) {
                    return;
                }
            }
            var csdEngineerFormValue = $("#csdForm").getFormValue();
            var marketingFormValue = $("#marketingForm").getFormValue();
            var submitRemark = $("#remarkForm").getFormValue().submitRemark;
            var productLeadTimeFormValue = $("#productLeadTimeForm").getFormValue();
            var leadTime = null;
            if (parseInt(productLeadTimeFormValue.leadTime)) {
                leadTime = parseInt(productLeadTimeFormValue.leadTime);
            }

            var args = {
                taskId: taskId, needCsdReply: needCsdReply, csdEngineerAccount: csdEngineerFormValue.csdEngineerAccount,
                csdEngineerName: csdEngineerFormValue.csdEngineerName, marketingReply: marketingFormValue.marketingReply,
                toCsdEmailAddress: marketingFormValue.toCsdEmailAddress, submitRemark: submitRemark,
                leadTime: leadTime, leadTimeRemark: productLeadTimeFormValue.leadTimeRemark
            };
            var attachments = $('#attachments').datagrid('getData');
            args.files = attachments.rows;
            if (!needCsdReply) {
                var gridData = $('#gridProductSalesTp').datagrid('getData');
                args.products = gridData.rows;
            }
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("ConsultationAndQuotationController.aspx?action=MarketingSubmit", { formJson: argsJson }, function (data) {
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
            var needCsdReply = $("#yesNeedCsdReply").attr("checked") == "checked";
            var csdEngineerFormValue = $("#csdForm").getFormValue();
            var marketingFormValue = $("#marketingForm").getFormValue();
            var submitRemark = $("#remarkForm").getFormValue().submitRemark;
            var productLeadTimeFormValue = $("#productLeadTimeForm").getFormValue();
            var leadTime = null;
            if (parseInt(productLeadTimeFormValue.leadTime)) {
                leadTime = parseInt(productLeadTimeFormValue.leadTime);
            }

            var args = {
                taskId: taskId, needCsdReply: needCsdReply, csdEngineerAccount: csdEngineerFormValue.csdEngineerAccount,
                csdEngineerName: csdEngineerFormValue.csdEngineerName, marketingReply: marketingFormValue.marketingReply, 
                toCsdEmailAddress: marketingFormValue.toCsdEmailAddress, submitRemark: submitRemark,
                leadTime: leadTime, leadTimeRemark: productLeadTimeFormValue.leadTimeRemark
            };
            if (!needCsdReply) {
                var gridData = $('#gridProductSalesTp').datagrid('getData');
                args.products = gridData.rows;
            }
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要退回吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("ConsultationAndQuotationController.aspx?action=MarketingReturn", { formJson: argsJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("退回成功");
                    closeWindow();
                }
            });
        });

        $("#yesNeedCsdReply").click(function () {
            $("#csdForm").show();
            $("#pnlProducts").show();
            $("#pnlProductsSalesTP").hide();
            $("#marketingForm").hide();
        });

        $("#noNeedCsdReply").click(function () {
            $("#csdForm").hide();
            setTimeout(function () { $("#pnlProducts").hide(); }, 100);
            $("#pnlProductsSalesTP").show();
            $("#marketingForm").show();
        });

        $("#csdForm, #marketingForm, #productLeadTimeForm").validate();

        $(".userEmailMultiSelect").userEmailMultiSelect();

        $("#gridProduct, #remarks").datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false
        });
        $("#attachments").attachmentsGrid();
        var editDialog = $("#editProductDialog").productMarketingSalesTPDialog();
        $("#gridProductSalesTp").productSalesTpGrid(editDialog, sumProductQuantityAndTP);
    })
</script>