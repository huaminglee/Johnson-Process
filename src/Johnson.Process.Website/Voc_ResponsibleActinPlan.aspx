<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Voc_ResponsibleActinPlan.aspx.cs" Inherits="Johnson.Process.Website.Voc_ResponsibleActinPlan1" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocDetails.ascx" TagName="VocDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocActionPlanDetails.ascx" TagName="VocActionPlanDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>VOC-行动计划</title>
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
    <johnson:Header runat="server" HeaderTitle="VOC-行动计划" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">①基本信息</div></div>
    <form id="basicInfoForm">
        <johnson:VocDetails runat="server" ID="vocDetails"></johnson:VocDetails>
        <div style="margin-top: 1em;">
            <table id="attachments" style="width:900px;height:auto">
		        <thead>
			        <tr> 
				        <th field="fileName" resizable="false" width="200">附件名称</th>
                        <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			        </tr>
		        </thead>
	        </table>
        </div>
    </form>
    <div id="addVocActionPlanDialog" title="添加行动计划">
        <form>
            <johnson:VocActionPlanDetails runat="server" ID="addVocActionPlanDetails"></johnson:VocActionPlanDetails>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定并继续"/>
                <input type="button" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>
    <div id="editVocActionPlanDialog" title="编辑行动计划">
        <form>
            <johnson:VocActionPlanDetails runat="server" ID="editVocActionPlanDetails"></johnson:VocActionPlanDetails>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>

    <div style="margin-top: 1em;">
        <div class="panel-header" ><div class="panel-title">②现场解决方案</div></div>
        <form id="solutionsForm">
           <table class="formInfo">
                <tr >
                    <td class="labelCol" style="width: 200px">
                        现场解决方案<span style="color: Red">*</span>
                    </td>
                    <td class="textCol">
                        <textarea name="solutions" class="textInput required" style="width: 688px;" rows="5"></textarea>
                    </td>
                </tr>
            </table>
            <table id="solutionsAttachments" style="width:900px;height:auto">
		        <thead>
			        <tr> 
				        <th field="fileName" resizable="false" width="200">附件名称</th>
                        <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			        </tr>
		        </thead>
	        </table>
        </form>
    </div>

    <div style="margin-top: 1em;">
        <table id="actionGrid" style="width:900px;height:auto" title="③行动">
		    <thead>
			    <tr>
                    <th field="remark" resizable="false" width="400">行动描述</th>
                    <th field="endDate" resizable="false" width="120">计划完成时间</th>
                    <th field="userName" resizable="false" width="100">负责人</th>
			    </tr>
		    </thead>
	    </table>
    </div>
    
    <br />
    
    <div style="margin-top: 1em;">
        <table id="remarks" style="width:900px;height:auto" title="提交信息">
		    <thead>
			    <tr> 
				    <th field="remarkStepName" resizable="false" width="200">流程步骤</th>
                    <th field="remarkUserName" resizable="false" width="100">提交人</th>
                    <th field="remarkTime" resizable="false" width="130">提交日期</th>
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
    $(function () {
        $.get("VocController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data).setFormReadOnly();
            $("#solutionsForm").setFormValue(data);
            if (data.actions) {
                $("#actionGrid").datagrid("loadData", data.actions)
            }
            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            if (data.solutionsFiles) {
                $('#solutionsAttachments').datagrid('loadData', data.solutionsFiles);
            }
            $("#remarks").datagrid("loadData", data.remarks);
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#solutionsForm").validAndFocus()) {
                return;
            }
            var gridData = $('#actionGrid').datagrid('getData');
            if (gridData.rows.length == 0) {
                alert("请添加行动计划");
                return;
            }
            var gridData = $('#actionGrid').datagrid('getData');
            var args = { taskId: taskId };
            args.actions = gridData.rows;
            args.solutions = $("#solutionsForm").getFormValue().solutions;
            args.solutionsFiles = $("#solutionsAttachments").datagrid('getData').rows;
            args.submitRemark = $.getFormValue("#remarkForm").submitRemark;
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("VocController.aspx?action=ActinPlanSubmit", { formJson: argsJson }, function (data) {
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
            var gridData = $('#actionGrid').datagrid('getData');
            var args = { taskId: taskId };
            args.actions = gridData.rows;
            args.solutions = $("#solutionsForm").getFormValue().solutions;
            args.solutionsFiles = $("#solutionsAttachments").datagrid('getData').rows;
            args.submitRemark = $.getFormValue("#remarkForm").submitRemark;
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要退回吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("VocController.aspx?action=ActinPlanReturn", { formJson: argsJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("退回成功");
                    closeWindow();
                }
            });
        });

        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#remarks, #attachments").datagrid({
            rownumbers: true
        });
        $(".singleUserSelect").singleSelectUser();
        $("#solutionsForm").validate();
        var addVocActionPlanDialog = $("#addVocActionPlanDialog").dialog({ autoOpen: false, modal: true, width: 500 });
        var editVocActionPlanDialog = $("#editVocActionPlanDialog").dialog({ autoOpen: false, modal: true, width: 500 });
        $("#actionGrid").eidtableGrid(addVocActionPlanDialog, editVocActionPlanDialog);
        $("#solutionsAttachments").attachmentsGrid();
    })
</script>