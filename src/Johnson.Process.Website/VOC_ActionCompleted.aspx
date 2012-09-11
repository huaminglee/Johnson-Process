<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VOC_ActionCompleted.aspx.cs" Inherits="Johnson.Process.Website.VOC_ActionCompleted" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocDetails.ascx" TagName="VocDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocActionCompletedDetails.ascx" TagName="VocActionCompletedDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>VOC-行动完成</title>
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
    <johnson:Header runat="server" HeaderTitle="VOC-行动完成" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>
    <form id="basicInfoForm">
        <johnson:VocDetails runat="server" ID="vocDetails"></johnson:VocDetails>
    </form>

    <div id="editVocActionPlanDialog" title="编辑行动计划">
        <form>
            <johnson:VocActionCompletedDetails runat="server" ID="editVocActionPlanDetails"></johnson:VocActionCompletedDetails>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
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
        <table id="actionGrid" style="width:900px;height:auto" title="行动计划">
		    <thead>
			    <tr>
                    <th field="remark" resizable="false" width="150">行动描述</th>
                    <th field="endDate" resizable="false" width="120">计划完成时间</th>
                    <th field="result" resizable="false" width="150">结果</th>
                    <th field="resultFileName" resizable="false" width="150">附件</th>
                    <th field="resultFileId" resizable="false" formatter="fileActionFormater" width="80"></th>
			    </tr>
		    </thead>
	    </table>
    </div>
    
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
    </div>
</body>
</html>
<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";
    var tempFolderId = "<%= this.ProcessFolderId %>";
    $(function () {
        $.get("VocController.aspx?action=ActionCompletedGet", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data).setFormReadOnly();
            if (data.actions) {
                $("#actionGrid").datagrid("loadData", data.actions)
            }
            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            $("#remarks").datagrid("loadData", data.remarks);
        });

        $("#btnSubmit").button().click(function () {
            var gridData = $('#actionGrid').datagrid('getData');
            var invalidActionRemarks = "";
            var firstAppend = true;
            $.each(gridData.rows, function (i, action) {
                if (!action.result) {
                    if (firstAppend) {
                        invalidActionRemarks = action.remark;
                        firstAppend = false;
                    }
                    else {
                        invalidActionRemarks += "," + action.remark;
                    }
                }
            });
            if (invalidActionRemarks) {
                alert($.format("请填写{0}的行动结果", invalidActionRemarks));
                return false;
            }
            var args = { taskId: taskId };
            args.actions = gridData.rows;
            args.submitRemark = $.getFormValue("#remarkForm").submitRemark;
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("VocController.aspx?action=ActionCompletedSubmit", { formJson: argsJson }, function (data) {
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

        $(".edoc2FileUploader").edoc2SingleFile(tempFolderId);

        $("#remarks, #attachments").datagrid({
            rownumbers: true
        });

        var editVocActionPlanDialog = $("#editVocActionPlanDialog").dialog({ autoOpen: false, modal: true, width: 500 });
        $("#actionGrid").eidtableOnlyGrid(editVocActionPlanDialog);
    })
</script>