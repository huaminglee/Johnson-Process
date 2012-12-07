<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPingShen_Start.aspx.cs" Inherits="Johnson.Process.Website.OrderPingShen_Start" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/OrderDetails.ascx" TagName="OrderDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单评审-开始</title>
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
    <johnson:Header runat="server" HeaderTitle="订单评审-开始" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:OrderDetails runat="server" ID="orderDetails"></johnson:OrderDetails>
        
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    是否标准订单
                </td>
                <td style="width: 280px" class="textCol">
                    <input type="checkbox" name="isStandard" value="true"/>
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    PMC工程师<span style="color: Red" >*</span>
                </td>
                <td class="textCol">
                    <div class="singleUserSelect">
                        <input type="text" name="pmcEngineerAccount" class="userAccount"/>
                        <input type="text" name="pmcEngineerName" class="textInput userName required"/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    设计负责人<span style="color: Red" >*</span>
                </td>
                <td class="textCol">
                    <div class="singleUserSelect">
                        <input type="text" name="sheJiFuZeRenAccount" class="userAccount"/>
                        <input type="text" name="sheJiFuZeRenName" class="textInput userName required"/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
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
    var tempFolderId = "<%= this.ProcessFolderId %>";

    $(function () {
        $.get("OrderPingShenController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data);
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#basicInfoForm").validAndFocus()) {
                return;
            }
            var needPingShen = $("#basicInfoForm input[name='needPingShen']").attr("checked") == "checked";
            if (needPingShen && !$("#pingShenYaoQingForm").validAndFocus()) {
                return;
            }
            var valueObj;
            if(needPingShen){
                valueObj = $.getFormValue("#basicInfoForm,#remarkForm");
            }
            else{
                valueObj = $.getFormValue("#basicInfoForm, #remarkForm, #pingShenYaoQingForm");
            }
            var attachments = $('#attachments').datagrid('getData');
            valueObj.files = attachments.rows;
            valueObj.taskId = taskId;
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            var objJson = $.toJSON(valueObj);
            $(this).attr("disabled", "disabled");
            $.post("OrderPingShenController.aspx?action=start", { formJson: objJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });
        $("#basicInfoForm input[name='needPingShen']").click(function(){
            if($(this).attr("checked")){
                $("#pingShenYaoQingForm").show();
            }
            else{
                $("#pingShenYaoQingForm").hide();
            }
        });
        $("#basicInfoForm input[name='isStandard']").click(function(){
            if(!$(this).attr("checked")){
                $("#basicInfoForm input[name='sheJiFuZeRenName']").addClass("required");
            }
            else{
                $("#basicInfoForm input[name='sheJiFuZeRenName']").removeClass("required");
            }
        });
        $(".singleUserSelect").singleSelectUser();
        $(".multiSelectUser").multiSelectUser();
        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#basicInfoForm,#pingShenYaoQingForm").validate();
        $("#attachments").attachmentsGrid1();
    })
</script>