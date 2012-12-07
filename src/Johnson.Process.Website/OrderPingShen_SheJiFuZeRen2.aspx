<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPingShen_SheJiFuZeRen2.aspx.cs" Inherits="Johnson.Process.Website.OrderPingShen_SheJiFuZeRen2" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/OrderDetails.ascx" TagName="OrderDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单评审-设计负责人</title>
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
    <johnson:Header runat="server" HeaderTitle="订单评审-设计负责人" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:OrderDetails runat="server" ID="orderDetails"></johnson:OrderDetails>
    </form>
    
    <form id="pingShenForm">
        <table class="formInfo">
            <tr>
                <td class="labelCol" style="width: 200px">
                    电气完成时间
                </td>
                <td colspan="3" class="textCol">
                    <input  name="dianQiWanChengRiQi" type="text" class="textInput txtwidth" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td class="labelCol" style="width: 200px">
                    设计完成时间<span style="color: Red" >*</span>
                </td>
                <td colspan="3" class="textCol">
                    <input  name="sheJiWanChengRiQi" type="text" class="textInput txtwidth required dateISO" />
                </td>
            </tr>
            <tr>
                <td class="labelCol" style="width: 200px">
                    外购清单完成日期
                </td>
                <td colspan="3" class="textCol">
                    <input  name="waiGouWanChengRiQi" type="text" class="textInput txtwidth dateISO" />
                </td>
            </tr>
        </table>
    </form>
    <div style="margin-top: 1em;">
        <table id="attachments" style="width:900px;height:auto" title="评审资料">
		    <thead>
			    <tr> 
				    <th field="FileName" resizable="false" width="200">文件名称</th>
                    <th field="FileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			    </tr>
		    </thead>
	    </table>
    </div>
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
        $.get("OrderPingShenController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data).setFormReadOnly();
            $("#pingShenForm").setFormValue(data);
            if(data.files){
                $("#attachments").datagrid('loadData', data.files);
            }
            $("#remarks").datagrid("loadData", data.approves);
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#pingShenForm").validAndFocus()) {
                return;
            }
            var valueObj = $.getFormValue("#pingShenForm, #remarkForm");

            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("OrderPingShenController.aspx?action=EngFuZeRen2PingShenSubmit", { taskId: taskId, submitRemark: valueObj.submitRemark, sheJiWanChengRiQi: valueObj.sheJiWanChengRiQi, waiGouWanChengRiQi: valueObj.waiGouWanChengRiQi }, function (data) {
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
        $("#pingShenForm").validate();
        $("#attachments, #remarks").datagrid();
    })
</script>