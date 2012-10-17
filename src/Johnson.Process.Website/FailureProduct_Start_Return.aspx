<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FailureProduct_Start_Return.aspx.cs" Inherits="Johnson.Process.Website.FailureProduct_Start_Return" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/FailureProductDetails.ascx" TagName="FailureProductDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>不合格品处理单-开始</title>
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
    
    <table class="header">
        <tr>
            <td rowspan="2" style="width: 143px;">
                <div style="float: left; margin-left: 10px;">
                    <img alt="" src="images/logo.gif" />
                </div>
            </td>
            <td style="padding-left:270px; width:350px; vertical-align: bottom; font-weight: bold; font-size:1.5em;">
                    不合格品处理单-开始
            </td>
            <td style="padding-left:100px;">
                <a href="ProductRework_Start.aspx?taskId=<%= Request["taskId"] %>">返工返修</a>
            </td>
            <td >
            
            </td>
        </tr>
        <tr>
            <td style="text-align: left; font-weight: bold;">
                GZF ORDER FORM
            </td>
            <td >
            
            </td>
            <td >
            
            </td>
        </tr>
    </table>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>
    <form id="searchForm">
        <div>
            <table class="formInfo">
                <tr>
                    <td style="width: 200px" class="labelCol">
                        不合格编号<span style="color: Red" >*</span>
                    </td>
                    <td class="textCol" >
                        <input type="text" name="id" class="textInput required"/>
                        <input id="btnGetFailureProduct" type="button" value="获取不合品信息" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <form id="basicInfoForm">
        <johnson:FailureProductDetails runat="server" ID="failureProductDetails"></johnson:FailureProductDetails>
    </form>
    
    <form id="usersForm">
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    PMC<span style="color: Red" >*</span>
                </td>
                <td class="textCol" >
                    <div class="singleUserSelect">
                        <input type="text" name="pmcUserAccount" class="userAccount"/>
                        <input type="text" name="pmcUserName" class="textInput userName required"/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    QE<span style="color: Red" >*</span>
                </td>
                <td class="textCol">
                    <div class="singleUserSelect">
                        <input type="text" name="qeUserAccount" class="userAccount"/>
                        <input type="text" name="qeUserName" class="textInput userName required"/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
        </table>
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
            $("#remarks").datagrid("loadData", data.Approves);
            $("#usersForm").setFormValue({pmcUserAccount: data.PmcUserAccount, pmcUserName: data.PmcUserName, qeUserAccount: data.QEUserAccount, qeUserName: data.QEUserName});
            $("#searchForm input[name='id']").val(data.No);
        });
        $(".trNo").hide();
        $("#btnSubmit").button().click(function () {
            if (!$("#searchForm").validAndFocus()) {
                return;
            }
            var formValue = $("#searchForm").getFormValue();
            $.get("FailureProductController.aspx?action=GetFromThirdDatabase", { id: formValue.id }, function (data) {
                if(!data){
                    alert("找不到编号的不合品信息!");
                }
                else{
                    $("#basicInfoForm").setFormValue(data);
                    if (!$("#basicInfoForm").validAndFocus()) {
                        return;
                    }
                    if (!$("#usersForm").validAndFocus()) {
                        return;
                    }
                    var valueObj = $("#basicInfoForm").getFormValue();
                    var remarkObj = $.getFormValue("#remarkForm");
                    valueObj.submitRemark = remarkObj.submitRemark;
                    var users = $("#usersForm").getFormValue();
                    valueObj.PmcUserAccount = users.pmcUserAccount;
                    valueObj.PmcUserName = users.pmcUserName;
                    valueObj.QEUserAccount = users.qeUserAccount;
                    valueObj.QEUserName = users.qeUserName;
                    var objJson = $.toJSON(valueObj);
                    if (!confirm("您确实要提交吗？")) {
                        return;
                    }
                    $(this).attr("disabled", "disabled");
                    $.post("FailureProductController.aspx?action=startResubmit", { taskId: taskId, formJson: objJson }, function (data) {
                        if (data.result != 0) {
                            alert(data.message);
                        }
                        else {
                            alert("提交成功");
                            closeWindow();
                        }
                    });
                }
            });
        });
        $(".singleUserSelect").singleSelectUser();
        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#basicInfoForm, #searchForm").validate();
        $("#btnGetFailureProduct").button().click(function(){
            if (!$("#searchForm").validAndFocus()) {
                return;
            }
            var formValue = $("#searchForm").getFormValue();
            $.get("FailureProductController.aspx?action=GetFromThirdDatabase", { id: formValue.id }, function (data) {
                if(!data){
                    alert("找不到编号的不合品信息!");
                }
                else{
                    $("#basicInfoForm").setFormValue(data);
                }
            });
        });

        $("#remarks").datagrid({
            rownumbers: true
        });
    })
</script>