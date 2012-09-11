<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotation_ENG.aspx.cs" Inherits="Johnson.Process.Website.ConsultationAndQuotation_ENG" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationDetails.ascx" TagName="ConsultationAndQuotationDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationProductDetails.ascx" TagName="ConsultationAndQuotationProductDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>技术咨询及报价-技术部评审</title>
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
    <johnson:Header runat="server" HeaderTitle="技术咨询及报价-技术部评审" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:ConsultationAndQuotationDetails runat="server" ID="consultationAndQuotationDetails"></johnson:ConsultationAndQuotationDetails>
    </form>
    <table class="formInfo">
        <tr>
            <td class="labelCol" style="width: 200px">
                是否需要CID和QAD评审<span style="color: Red">*</span>
            </td>
            <td class="textCol" colspan="3">
                <input type="checkbox" name="needCidReply" id="yesNeedCidReply"/><label>CID评审</label>
                <input type="checkbox" name="needQadReply" id="yesNeedQadReply"/><label>QAD评审</label>
            </td>
        </tr>
    </table>

    <form id="cidForm" style="display:none">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    CID工程师<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <input type="text" id="txtCidEngineerRealName" class="textInput required" readonly="readonly"/>
                    <input type="text" style="display: none" id="txtCidEngineerAccount" name="cidEngineer" value=""/>
                    <%--<input type="text" id="txtCidEngineerRealName" class="textInput required" />
                    <input type="text" id="txtCidEngineerAccount" name="cidEngineer" value=""/>--%>
                    <input type="hidden" id="txtCidEngineerId" value="0"/>
                    <input type="button" onclick="selUser('txtCidEngineerId','txtCidEngineerAccount','txtCidEngineerRealName',false);" value="选择" class="btnCommon" />
                </td>
            </tr>
        </table>
    </form>

    <form id="qadForm" style="display:none">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    QAD工程师<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <input type="text" id="txtQadEngineerRealName" class="textInput required" readonly="readonly"/>
                    <input type="text" style="display: none" id="txtQadEngineerAccount" name="qadEngineer" value=""/>
                    <%--<input type="text" id="txtQadEngineerRealName" class="textInput required" />
                    <input type="text" id="txtQadEngineerAccount" name="qadEngineer" value=""/>--%>
                    <input type="hidden" id="txtQadEngineerId" value="0"/>
                    <input type="button" onclick="selUser('txtQadEngineerId','txtQadEngineerAccount','txtQadEngineerRealName',false);" value="选择" class="btnCommon" />
                </td>
            </tr>
        </table>
    </form>

    <form id="engForm">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    非标设计工时,涉及物料的SAP NO<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <textarea name="engReply" class="textInput required" style="width: 500px;" rows="3"></textarea>
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
            $("#engForm").setFormValue(data);
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
            var needCidReply = $("#yesNeedCidReply").attr("checked") == "checked";
            var needQadReply = $("#yesNeedQadReply").attr("checked") == "checked";
            if (needCidReply) {
                if (!$("#cidForm").validAndFocus()) {
                    return;
                }
            }
            if (needQadReply) {
                if (!$("#qadForm").validAndFocus()) {
                    return;
                }
            }
            if (!needCidReply && !needQadReply) {
                if (!$("#engForm").validAndFocus()) {
                    return;
                }
            }
            var engReply = $("#engForm").getFormValue().engReply;
            var qadEngineer = $("#qadForm").getFormValue().qadEngineer;
            var cidEngineer = $("#cidForm").getFormValue().cidEngineer;
            var submitRemark = $("#remarkForm").getFormValue().submitRemark;

            var args = {
                taskId: taskId, engReply: engReply, needCidReply: needCidReply, needQadReply: needQadReply, 
                qadEngineer: qadEngineer, cidEngineer: cidEngineer, submitRemark: submitRemark
            };
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("ConsultationAndQuotationController.aspx?action=engSubmit", { formJson: argsJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });

        $("#yesNeedCidReply").click(function () {
            if ($(this).attr("checked") == "checked") {
                $("#engForm").hide();
                $("#cidForm").show();
            }
            else {
                if ($("#yesNeedQadReply").attr("checked") != "checked") {
                    $("#engForm").show();
                }
                $("#cidForm").hide();
            }
        });

        $("#yesNeedQadReply").click(function () {
            if ($(this).attr("checked") == "checked") {
                $("#engForm").hide();
                $("#qadForm").show();
            }
            else {
                if ($("#yesNeedCidReply").attr("checked") != "checked") {
                    $("#engForm").show();
                }
                $("#qadForm").hide();
            }
        });

        $("#engForm, #cidForm, #qadForm").validate();

        $("#grid, #remarks, #attachments").datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false
        });
    })
</script>