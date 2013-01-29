<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPingShen_SheJiFuZeRen.aspx.cs" Inherits="Johnson.Process.Website.OrderPingShen_SheJiFuZeRen" %>
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
                <td style="width: 200px" class="labelCol">
                    是否标准订单
                </td>
                <td style="width: 280px" class="textCol">
                    <input type="checkbox" name="isStandard" value="true"/>
                </td>
            </tr>
        </table>

        <table class="formInfo" id="pingShenTable">
            <tr>
                <td style="width: 200px" class="labelCol">
                    是否需要工程师评审
                </td>
                <td style="width: 280px" class="textCol">
                    <input type="checkbox" name="needEngineerPingShen" value="true"/>
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    外购清单完成日期
                </td>
                <td style="width: 280px" class="textCol">
                    <input  name="waiGouQingDanRiQi" type="text" class="textInput txtwidth dateISO" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    设计完成日期<span style="color: Red" >*</span>
                </td>
                <td style="width: 280px" class="textCol">
                    <input  name="sheJiWanChengRiQi" type="text" class="textInput txtwidth required dateISO" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    设计工程师<span style="color: Red" >*</span>
                </td>
                <td class="textCol">
                    <div class="singleUserSelect">
                        <input type="text" name="engEngineerAccount" class="userAccount"/>
                        <input type="text" name="engEngineerName" class="textInput userName required"/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
        </table>

        <table class="formInfo" id="needPingShenTable">
            <tr>
                <td style="width: 200px" class="labelCol">
                    需要组织评审
                </td>
                <td class="textCol">
                    <input type="checkbox" name="needPingShen" value="true"/>
                </td>
            </tr>
        </table>
    </form>
    <form id="engineerPingShenForm" style="display: none;">
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    电气工程师<span style="color: Red" >*</span>
                </td>
                <td class="textCol">
                    <div class="singleUserSelect">
                        <input type="text" name="dianQiEngineerAccount" class="userAccount"/>
                        <input type="text" name="dianQiEngineerName" class="textInput userName required"/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
        </table>
    </form>
    <form id="pingShenYaoQingForm" style="display: none">
        <table>
            <tr>
                <td align="center" colspan="4">
                    <h3>项目评审邀请</h3>
                </td>
            </tr>
            <tr>
                <td rowspan="2">
                    参与者<span style="color: Red" >*</span>
                </td>
                <td class="textCol" style="width: 280px">
                    <div class="multiSelectUser">
                    CSD:
                        <input type="text" name="csdPingShenRenAccounts" class="userAccount"/>
                        <input type="text" name="csdPingShenRenNames" style="width: 140px;" class="textInput userName"/>
                        <input type="button" value="选择" class="btnCommon selectButton" />
                        <input type="button" value="重置" class="btnCommon resetButton" />
                    </div>
                </td>
                <td class="textCol" style="width: 280px">
                    <div class="multiSelectUser">
                        ENG:
                        <input type="text" name="engPingShenRenAccounts" class="userAccount"/>
                        <input type="text" name="engPingShenRenNames" style="width: 140px;" class="textInput userName"/>
                        <input type="button" value="选择" class="btnCommon selectButton" />
                        <input type="button" value="重置" class="btnCommon resetButton" />
                    </div>
                </td>
                <td class="textCol" style="width: 280px">
                    <div class="multiSelectUser">
                        SCM:
                        <input type="text" name="scmPingShenRenAccounts" class="userAccount"/>
                        <input type="text" name="scmPingShenRenNames" style="width: 140px;" class="textInput userName"/>
                        <input type="button" value="选择" class="btnCommon selectButton" />
                        <input type="button" value="重置" class="btnCommon resetButton" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="textCol" style="width: 280px">
                    <div class="multiSelectUser">
                    QAD:
                        <input type="text" name="qadPingShenRenAccounts" class="userAccount"/>
                        <input type="text" name="qadPingShenRenNames" style="width: 140px;" class="textInput userName"/>
                        <input type="button" value="选择" class="btnCommon selectButton" />
                        <input type="button" value="重置" class="btnCommon resetButton" />
                    </div>
                </td>
                <td class="textCol" style="width: 280px">
                    <div class="multiSelectUser">
                    CID:
                        <input type="text" name="cidPingShenRenAccounts" class="userAccount"/>
                        <input type="text" name="cidPingShenRenNames" style="width: 140px;" class="textInput userName"/>
                        <input type="button" value="选择" class="btnCommon selectButton" />
                        <input type="button" value="重置" class="btnCommon resetButton" />
                    </div>
                </td>
                <td class="textCol" style="width: 280px">
                    <div class="multiSelectUser">
                    PMC:
                        <input type="text" name="pmcPingShenRenAccounts" class="userAccount"/>
                        <input type="text" name="pmcPingShenRenNames" style="width: 140px;" class="textInput userName"/>
                        <input type="button" value="选择" class="btnCommon selectButton" />
                        <input type="button" value="重置" class="btnCommon resetButton" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    开始时间<span style="color: Red" >*</span>
                </td>
                <td>
                    <input name="pingShenStartDate" type="text" style="width: 120px" class="textInput txtwidth required dateISO" />日
                    <input name="pingShenStartHours" type="text" style="width: 20px" class="textInput txtwidth required digits" />点
                    <input name="pingShenStartMinutes" type="text" style="width: 20px" value="0" class="textInput txtwidth required digits" />分
                </td>
                <td colspan="2">
                    结束时间<span style="color: Red" >*</span>
                    <input name="pingShenEndDate" type="text" style="width: 120px" class="textInput txtwidth required dateISO" />日
                    <input name="pingShenEndHours" type="text" style="width: 20px" class="textInput txtwidth required digits" />点
                    <input name="pingShenEndMinutes" type="text" style="width: 20px" value="0" class="textInput txtwidth required digits" />分
                </td>
            </tr>
            <tr>
                <td>
                    地点<span style="color: Red" >*</span>
                </td>
                <td colspan="3">
                    <input name="pingShenPalce" type="text" style="width: 600px;" class="textInput txtwidth required" />
                </td>
            </tr>
            <tr>
                <td>
                    会议讨论内容<span style="color: Red" >*</span>
                </td>
                <td colspan="3">
                    <textarea name="pingShenContent" class="textInput required" style="width: 600px;" rows="3"></textarea>
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
            $("#pingShenForm, #engineerPingShenForm").setFormValue(data);
            if(data.files){
                $("#attachments").datagrid('loadData', data.files);
            }
            if(data.items){
                $("#items").datagrid('loadData', data.items);
            }
            $("#remarks").datagrid("loadData", data.approves);
        });

        $("#btnSubmit").button().click(function () {
            var needPingShen = $("#pingShenForm input[name='needPingShen']").attr("checked") == "checked";
            if (needPingShen && !$("#pingShenYaoQingForm").validAndFocus()) {
                return;
            }
            var isStandard = $("#pingShenForm input[name='isStandard']").attr("checked") == "checked";
            if(!isStandard){
                if (!$("#pingShenForm").validAndFocus()) {
                    return;
                }
                var needEngineerPingShen = $("#pingShenForm input[name='needEngineerPingShen']").attr("checked") == "checked";
                if(needEngineerPingShen && !$("#engineerPingShenForm").validAndFocus()){
                    return;
                }
            }
            var valueObj = $.getFormValue("#pingShenForm, #engineerPingShenForm, #remarkForm, #pingShenYaoQingForm");
            valueObj.taskId = taskId;
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            var objJson = $.toJSON(valueObj);
            $(this).attr("disabled", "disabled");
            $.post("OrderPingShenController.aspx?action=EngFuZeRenPingShenSubmit", { formJson: objJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });
        $("#pingShenForm input[name='needEngineerPingShen']").click(function(){
            if($(this).attr("checked")){
                $("#pingShenForm input[name='waiGouQingDanRiQi'], #pingShenForm input[name='sheJiWanChengRiQi']").removeClass("required");
                $("#engineerPingShenForm").show();
            }
            else{
                $("#pingShenForm input[name='waiGouQingDanRiQi'], #pingShenForm input[name='sheJiWanChengRiQi']").addClass("required");
                $("#engineerPingShenForm").hide();
            }
        });
        $("#pingShenForm input[name='needPingShen']").click(function(){
            if($(this).attr("checked")){
                $("#pingShenYaoQingForm").show();
            }
            else{
                $("#pingShenYaoQingForm").hide();
            }
        });
        $("#pingShenForm input[name='isStandard']").click(function(){
            if(!$(this).attr("checked")){
                $("#pingShenTable, #needPingShenTable").show();
                var needEngineerPingShen = $("#pingShenForm input[name='needEngineerPingShen']").attr("checked") == "checked";
                if(needEngineerPingShen){
                    $("#engineerPingShenForm").show();
                }
            }
            else{
                $("#pingShenForm input[name='needPingShen'], #pingShenForm input[name='needEngineerPingShen']").removeAttr("checked");
                $("#pingShenTable, #needPingShenTable, #engineerPingShenForm, #pingShenYaoQingForm").hide();
            }
        });
        $(".singleUserSelect").singleSelectUser();
        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#pingShenForm, #engineerPingShenForm").validate();
        $("#attachments, #remarks, #items").datagrid({rownumbers: true});
    })
</script>