<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotation_CsdTrace.aspx.cs" Inherits="Johnson.Process.Website.ConsultationAndQuotation_CsdTrace" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationDetails.ascx" TagName="ConsultationAndQuotationDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationProductDetails.ascx" TagName="ConsultationAndQuotationProductDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationProductCsdFiceTPDetails.ascx" TagName="ConsultationAndQuotationProductCsdFiceTPDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>技术咨询及报价-CSD工程师</title>
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
    <johnson:Header runat="server" HeaderTitle="技术咨询及报价-CSD工程师" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:ConsultationAndQuotationDetails runat="server" ID="consultationAndQuotationDetails"></johnson:ConsultationAndQuotationDetails>
    </form>
    <table class="formInfo">
        <tr>
            <td class="labelCol" style="width: 200px">
                是否需要其他部门答复<span style="color: Red">*</span>
            </td>
            <td class="textCol" colspan="3">
                <input type="checkbox" name="needEngReply" id="yesNeedEngReply"/><label>技术部答复</label>
                <input type="checkbox" name="needLogReply" id="yesNeedLogReply"/><label>物流部答复</label>
                <input type="checkbox" name="needScmReply" id="yesNeedScmReply"/><label>采购部答复</label>
            </td>
        </tr>
    </table>

    <form id="engForm" style="display:none">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    技术部工程师<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <input type="text" id="txtEngEngineerRealName" class="textInput required" readonly="readonly"/>
                    <input type="text" style="display: none" id="txtEngEngineerAccount" name="engEngineer" value=""/>
                    <%--<input type="text" id="txtEngEngineerRealName" class="textInput required" />
                    <input type="text" id="txtEngEngineerAccount" name="engEngineer" value=""/>--%>
                    <input type="hidden" id="txtEngEngineerId" value="0"/>
                    <input type="button" onclick="selUser('txtEngEngineerId','txtEngEngineerAccount','txtEngEngineerRealName',false);" value="选择" class="btnCommon" />
                </td>
            </tr>
        </table>
    </form>

    <form id="logForm" style="display:none">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    物流部工程师<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <div class="multiSelectUser">
                        <input type="text" class="textInput required userName" readonly="readonly"/>
                        <input type="text" style="display: none" class="userAccount" name="logEngineer" />
                        <input type="button" value="选择" class="btnCommon selectButton" />
                        <input type="button" value="重置" class="btnCommon resetButton" />
                    </div>
                </td>
            </tr>
        </table>
    </form>

    <form id="scmForm" style="display:none">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    采购部工程师<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <input type="text" id="txtScmEngineerRealName" class="textInput required" readonly="readonly"/>
                    <input type="text" style="display: none" id="txtScmEngineerAccount" name="scmEngineer" value=""/>
                    <%--<input type="text" id="txtScmEngineerRealName" class="textInput required" />
                    <input type="text" id="txtScmEngineerAccount" name="scmEngineer" value=""/>--%>
                    <input type="hidden" id="txtScmEngineerId" value="0"/>
                    <input type="button" onclick="selUser('txtScmEngineerId','txtScmEngineerAccount','txtScmEngineerRealName',false);" value="选择" class="btnCommon" />
                </td>
            </tr>
        </table>
    </form>

    <form id="tracerForm">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    答复<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <textarea name="csdReply" class="textInput required" style="width: 500px;" rows="3"></textarea>
                </td>
            </tr>
            <tr>
                <td class="labelCol" style="width: 200px">
                    邮件抄送给
                </td>
                <td class="textCol userEmailMultiSelect" >
                    <input type="text" name="csdTracerEmailTo" class="textInput multiemail" style="width: 500px;"/>
                    <input type="button" value="选择" class="btnCommon" />
                </td>
            </tr>
        </table>
    </form>

    <div id="editProductDialog" title="非标报价">
        <form>
            <johnson:ConsultationAndQuotationProductCsdFiceTPDetails runat="server" ID="editConsultationAndQuotationProductSalesTPDetails"></johnson:ConsultationAndQuotationProductCsdFiceTPDetails>
            <div style="margin-top:1em">
                <input type="submit" value="确定并继续"/>
                <input type="button" value="确定"/>
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
                        <th field="csdWithoutSalesTP" resizable="false" width="155" formatter="priceFormatter">FICE TP/(Without VAT)</th>
                        <th field="csdWithSalesTP" resizable="false" width="140" formatter="priceFormatter">FICE TP/(With VAT)</th>
                        <th field="csdTotalSalesTP" resizable="false" width="120" formatter="priceFormatter">FICE TP/总和</th>
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
    </div>
</body>
</html>
<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";
    function sumProductQuantityAndCsdTP(){
        $("#gridProductSalesTp").sumProductQuantityAndCsdTP();
    }
    $(function () {
        $.get("ConsultationAndQuotationController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm, #tracerForm, #productLeadTimeForm").setFormValue(data);
            var products = { "total": data.products.length, "rows": data.products, "footer": [{ "productModel": "合计", "quantity": 0}] };
            var salesTpProducts = { "total": data.products.length, "rows": data.products, "footer": [{ "productModel": "合计", "quantity": 0, "csdTotalSalesTP": 0}] };
            $('#gridProduct').datagrid('loadData', products);
            $("#gridProductSalesTp").datagrid('loadData', salesTpProducts);
            $("#gridProduct").sumProductQuantity();
            sumProductQuantityAndCsdTP();
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
            var needEngReply = $("#yesNeedEngReply").attr("checked") == "checked";
            var needLogReply = $("#yesNeedLogReply").attr("checked") == "checked";
            var needScmReply = $("#yesNeedScmReply").attr("checked") == "checked";
            if (needEngReply) {
                if (!$("#engForm").validAndFocus()) {
                    return;
                }
            }
            if (needLogReply) {
                if (!$("#logForm").validAndFocus()) {
                    return;
                }
            }
            if (needScmReply) {
                if (!$("#scmForm").validAndFocus()) {
                    return;
                }
            }
            if (!needEngReply && !needLogReply && !needScmReply) {
                if (!$("#tracerForm").validAndFocus()) {
                    return;
                }
                if (!$("#gridProductSalesTp").productCsdSalesTpGridValid()) {
                    return;
                }
                if (!$("#productLeadTimeForm").validAndFocus()) {
                    return;
                }
            }
            var tracerFormValue = $("#tracerForm").getFormValue();
            var csdReply = tracerFormValue.csdReply;
            var logEngineer = $("#logForm").getFormValue().logEngineer;
            var engEngineer = $("#engForm").getFormValue().engEngineer;
            var scmEngineer = $("#scmForm").getFormValue().scmEngineer;
            var submitRemark = $("#remarkForm").getFormValue().submitRemark;
            var productLeadTimeFormValue = $("#productLeadTimeForm").getFormValue();
            var leadTime = null;
            if (parseInt(productLeadTimeFormValue.leadTime)) {
                leadTime = parseInt(productLeadTimeFormValue.leadTime);
            }
            var args = {
                taskId: taskId, csdReply: csdReply, csdTracerEmailTo: tracerFormValue.csdTracerEmailTo, needEngReply: needEngReply, needLogReply: 
                needLogReply, needScmReply: needScmReply, logEngineer: logEngineer, engEngineer: engEngineer, scmEngineer: scmEngineer, 
                submitRemark: submitRemark, leadTime: leadTime, leadTimeRemark: productLeadTimeFormValue.leadTimeRemark
            };
            if (!needEngReply && !needLogReply && !needScmReply) {
                var gridData = $('#gridProductSalesTp').datagrid('getData');
                args.products = gridData.rows;
            }
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("ConsultationAndQuotationController.aspx?action=tracerSubmit", { formJson: argsJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });

        $("#yesNeedEngReply").click(function () {
            if ($(this).attr("checked") == "checked") {
                $("#tracerForm").hide();
                $("#engForm").show();
                $("#yesNeedLogReply, #yesNeedScmReply").removeAttr("checked");
                $("#logForm, #scmForm").hide();
                $("#pnlProducts").show();
                $("#pnlProductsSalesTP").hide();
            }
            else {
                $("#tracerForm").show();
                $("#engForm").hide();
                setTimeout(function () { $("#pnlProducts").hide(); }, 100);
                $("#pnlProductsSalesTP").show();
            }
        });

        $("#yesNeedLogReply").click(function () {
            if ($(this).attr("checked") == "checked") {
                $("#tracerForm").hide();
                $("#logForm").show();
                $("#yesNeedEngReply").removeAttr("checked");
                $("#engForm").hide();
                $("#pnlProducts").show();
                $("#pnlProductsSalesTP").hide();
            }
            else {
                if ($("#yesNeedScmReply").attr("checked") != "checked") {
                    $("#tracerForm").show();
                    setTimeout(function () { $("#pnlProducts").hide(); }, 100);
                    $("#pnlProductsSalesTP").show();
                }
                $("#logForm").hide();
            }
        });

        $("#yesNeedScmReply").click(function () {
            if ($(this).attr("checked") == "checked") {
                $("#tracerForm").hide();
                $("#scmForm").show();
                $("#yesNeedEngReply").removeAttr("checked");
                $("#engForm").hide();
                $("#pnlProducts").show();
                $("#pnlProductsSalesTP").hide();
            }
            else {
                if ($("#yesNeedLogReply").attr("checked") != "checked") {
                    $("#tracerForm").show();
                    setTimeout(function () { $("#pnlProducts").hide(); }, 100);
                    $("#pnlProductsSalesTP").show();
                }
                $("#scmForm").hide();
            }
        });

        $("#tracerForm, #engForm, #logForm, #scmForm, #productLeadTimeForm").validate();

        $(".userEmailMultiSelect").userEmailMultiSelect();
        
        $(".multiSelectUser").multiSelectUser();

        $("#gridProduct, #remarks, #attachments").datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false
        });

        var editDialog = $("#editProductDialog").productCsdSalesTPDialog();
        $("#gridProductSalesTp").productSalesTpGrid(editDialog, sumProductQuantityAndCsdTP);
    })
</script>