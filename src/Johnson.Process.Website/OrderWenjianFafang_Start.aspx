﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderWenjianFafang_Start.aspx.cs" Inherits="Johnson.Process.Website.OrderWenjianFafang_Start" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/OrderDetails.ascx" TagName="OrderDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单评审-提交设计</title>
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
    <johnson:Header runat="server" HeaderTitle="订单评审-提交设计" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:OrderDetails runat="server" ID="orderDetails"></johnson:OrderDetails>
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
    <form id="pingShenForm" style="margin-top: 1em;">
        <table class="formInfo">
            <tr>
                <td class="labelCol" style="width: 200px">
                    设计说明
                </td>
                <td colspan="3" class="textCol">
                    <input type="text" name="sheJiShuoMing" class="textInput userName"/>
                </td>
            </tr>
            <tr>
                <td class="labelCol" style="width: 200px">
                    资料完整性<span style="color: Red" >*</span>
                </td>
                <td colspan="3" class="textCol">
                    <select name="fafangWancheng" class="required">
                        <option></option>
                        <option value="true">全部完成</option>
                        <option value="false">部分完成</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="labelCol" style="width: 200px">
                    是否有新物料
                </td>
                <td colspan="3" class="textCol">
                    <input type="checkbox" name="hasXinWuLiao" value="true"/>
                </td>
            </tr>
            <tr id="trScmEngineer" style="display: none;">
                <td class="labelCol" style="width: 200px">
                    物料计划员<span style="color: Red" >*</span>
                </td>
                <td colspan="3" class="textCol">
                    <div class="singleUserSelect">
                        <input type="text" name="scmEngineerAccount" class="userAccount"/>
                        <input type="text" name="scmEngineerName" class="textInput userName required"/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="labelCol" style="width: 200px">
                    技术检查工程师<span style="color: Red" >*</span>
                </td>
                <td colspan="3" class="textCol">
                    <div class="singleUserSelect">
                        <input type="text" name="jianChaEngineerAccount" class="userAccount"/>
                        <input type="text" name="jianChaEngineerName" class="textInput userName required"/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="labelCol" style="width: 200px">
                    主管<span style="color: Red" >*</span>
                </td>
                <td colspan="3" class="textCol">
                    <div class="singleUserSelect">
                        <input type="text" name="zhuGuanAccount" class="userAccount"/>
                        <input type="text" name="zhuGuanName" class="textInput userName required"/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
        </table>
    </form>
    <div style="margin-top: 1em;">
        <table id="sheJiZiLiao" style="width:900px;height:auto" title="设计资料">
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
    var pingshenIncNo = '<%= Request["incNo"] %>';
    if(!pingshenIncNo){
        alert("该流程只能从待发放文件订单中发起")
        $("#btnSubmit").hide();
    }
    $(function () {
        $.get("OrderWenjianFafang_Controller.aspx?action=get", { taskId: taskId, pingshenIncNo: pingshenIncNo, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data).setFormReadOnly();
            $("#pingShenForm").setFormValue(data);
            if(data.files){
                $("#attachments").datagrid('loadData', data.files);
            }
            if(data.items){
                $("#items").datagrid('loadData', data.items);
            }
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#pingShenForm").validAndFocus()) {
                return;
            }
            var valueObj = $.getFormValue("#pingShenForm, #remarkForm");
            var sheJiZiLiao = $('#sheJiZiLiao').datagrid('getData');
            valueObj.sheJiZiLiao = sheJiZiLiao.rows;
            valueObj.taskId = taskId;
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            valueObj.pingshenIncNo = pingshenIncNo;
            var objJson = $.toJSON(valueObj);
            $(this).attr("disabled", "disabled");
            $.post("OrderWenjianFafang_Controller.aspx?action=Start", { formJson: objJson}, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });
        $("#pingShenForm input[name='hasXinWuLiao']").click(function(){
            if($(this).attr("checked")){
                $("#pingShenForm input[name='scmEngineerName']").addClass("required");
                $("#trScmEngineer").show();
            }
            else{
                $("#pingShenForm input[name='scmEngineerName']").removeClass("required");
                $("#trScmEngineer").hide();
            }
        });
        $(".singleUserSelect").singleSelectUser();
        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#pingShenForm").validate();
        $("#attachments, #remarks, #items").datagrid();
        $("#sheJiZiLiao").attachmentsGrid1();
    })
</script>
