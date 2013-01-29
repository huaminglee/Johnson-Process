<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FailureProduct_QC.aspx.cs" Inherits="Johnson.Process.Website.FailureProduct_QC" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/FailureProductDetails.ascx" TagName="FailureProductDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>不合格品处理单-QC</title>
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
    <johnson:Header runat="server" HeaderTitle="不合格品处理单-QC" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:FailureProductDetails runat="server" ID="failureProductDetails"></johnson:FailureProductDetails>
        <div class="panel-header" style="margin-top: 2em;"><div class="panel-title">LOG生产计划具体情况说明(时间及数量)或责任部门主管建议的处理意见</div></div>
        <textarea name="PmcOpinion" class="textInput required" style="width: 890px;" rows="3"></textarea>
        <div class="panel-header" style="margin-top: 2em;"><div class="panel-title">QAD判定</div></div>
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    缺陷评级<span style="color: Red" >*</span>
                </td>
                <td class="textCol">
                    <div>
                        <input name="Level" type="radio" value="Critical" checked="checked"/><label>Critical</label>
                        <input name="Level" type="radio" value="Major" /><label>Major</label>
                        <input name="Level" type="radio" value="Minor" /><label>Minor</label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    处理结果<span style="color: Red" >*</span>
                </td>
                <td class="textCol">
                    <div>
                        <input name="QEResult" type="radio" value="1" checked="checked"/><label>退回供应商</label>
                        <input name="QEResult" type="radio" value="2" /><label>让步接收</label>
                        <input name="QEResult" type="radio" value="3" /><label>返工/返修</label>
                        <input name="QEResult" type="radio" value="4" /><label>报废</label>
                        <input name="QEResult" type="radio" value="5" /><label>挑选</label>
                        <input name="QEResult" type="radio" value="6" /><label>MRB会议</label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    来料
                </td>
                <td class="textCol">
                    <div>
                        <input name="SupplierDeal" type="radio" value="8D报告" /><label>8D报告</label>
                        <input name="SupplierDeal" type="radio" value="索赔单" /><label>索赔单</label>
                        <label>单号</label><input name="SupplierDealBillNumber" type="text" class="textInput txtwidth " style="width:80px;"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    制程
                </td>
                <td class="textCol">                    
                    <input name="ProduceDeal" type="radio" value="产品返工/返修单" /><label>产品返工/返修单</label>
                    <input name="ProduceDeal" type="radio" value="8D报告" /><label>8D报告</label>
                    <label>单号</label><input name="ProduceDealNumber" type="text" class="textInput txtwidth " style="width:80px;"/>
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    具体分析说明<span style="color: Red" >*</span>
                </td>
                <td class="textCol">
                    <textarea name="Analysis" class="textInput required" style="width: 688px;" rows="3"></textarea>
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    让步接收意见<span style="color: Red" >*</span>
                </td>
                <td class="textCol">
                    <textarea name="ReceiveQARemark" class="textInput required" style="width: 688px;" rows="3"></textarea>
                </td>
            </tr>
        </table>
    </form>
    <form id="qcForm">
        <div class="panel-header" style="margin-top: 2em;"><div class="panel-title">跟踪验证<span style="color: Red" >*</span></div></div>
        <textarea name="QCValidateResult" class="textInput required" style="width: 890px;" rows="3"></textarea>
    </form>
    
    <div style="margin-top: 1em;">
        <table id="remarks" style="width:900px;height:auto" title="提交信息">
		    <thead>
			    <tr> 
				    <th field="StepName" resizable="false" width="200">流程步骤</th>
                    <th field="ApproveUserName" resizable="false" width="100">提交人</th>
                    <th field="ApproveTime" resizable="false" width="130">提交日期</th>
                    <th field="Remark" resizable="false" width="300">备注</th>
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
                        <textarea name="submitRemark" class="textInput" style="width: 688px;" rows="3"></textarea>
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
        $.get("FailureProductController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data).setFormReadOnly();
            $("#basicInfoForm input[name='QEResult']:checked + label").after().css("color", "red");
            $("#qcForm").setFormValue(data);
            $("#remarks").datagrid("loadData", data.Approves);
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#qcForm").validAndFocus()) {
                return;
            }
            var valueObj = $.getFormValue("#remarkForm, #qcForm");
            
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("FailureProductController.aspx?action=QCSubmit", { taskId: taskId, QCValidateResult: valueObj.QCValidateResult, submitRemark: valueObj.submitRemark  }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });
        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#qcForm").validate();
        $("#remarks").datagrid({
            rownumbers: true
        });
    })
</script>