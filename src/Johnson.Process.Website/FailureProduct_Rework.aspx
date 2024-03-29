﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FailureProduct_Rework.aspx.cs" Inherits="Johnson.Process.Website.FailureProduct_Rework" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ProductReworkDetails.ascx" TagName="ProductReworkDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ProductReworkMaterialsDetails.ascx" TagName="ProductReworkMaterials" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>返工返修单-QC</title>
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
    <johnson:Header runat="server" HeaderTitle="返工返修单-QC" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:ProductReworkDetails runat="server" ID="productReworkDetails"></johnson:ProductReworkDetails>
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    品质状态描述<span style="color: Red" >*</span>
                </td>
                <td>
                    <textarea name="PZZTMS" class="textInput required" style="width: 688px;" rows="3"></textarea>
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
    var tempFolderId = "<%= this.ProcessFolderId %>";

    $(function () {
        $.get("FailureProductController.aspx?action=GetReworkModel", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data);
            if(data.Files){
                $('#attachments').datagrid('loadData', data.Files);
            }
            $("#remarks").datagrid("loadData", data.Approves);
        });

        $("#btnSubmit").button().click(function () {
            if(!$("#basicInfoForm").validAndFocus()){
                return;
            }
            var valueObj = $("#remarkForm, #basicInfoForm").getFormValue();
            var files = $('#attachments').datagrid('getData');
            valueObj.Files = files.rows;
            var objJson = $.toJSON(valueObj);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("FailureProductController.aspx?action=ReworkSubmit", { taskId: taskId, formJson: objJson, submitRemark: valueObj.submitRemark }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });
        $(".singleUserSelect").singleSelectUser();
        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#remarks").datagrid();
        $("#attachments").attachmentsGrid1();
    })
</script>