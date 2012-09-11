<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Voc_Responsible.aspx.cs" Inherits="Johnson.Process.Website.Voc_Responsible1" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocDetails.ascx" TagName="VocDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocActionCompletedDetails.ascx" TagName="VocActionCompletedDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>VOC-原因分析及纠正预防措施</title>
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
    <johnson:Header runat="server" HeaderTitle="VOC-原因分析及纠正预防措施" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>
    <form id="basicInfoForm">
        <johnson:VocDetails runat="server" ID="vocDetails"></johnson:VocDetails>
    </form>
    <form id="responnsibleUserPreviousForm">
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    负责人上级
                </td>
                <td style="width: 380px" class="textCol" colspan="3">
                    <input type="text" id="txtResponsibleUserRealName" name="responsibleUserPreviousName" class="textInput" readonly="readonly"/>
                    <input type="text" style="display: none" id="txtResponsibleUserAccount" name="responsibleUserPreviousAccount" value=""/>
                    <%--<input type="text" id="txtResponsibleUserRealName" name="responsibleUserPreviousName" class="textInput" />
                    <input type="text" id="txtResponsibleUserAccount" name="responsibleUserPreviousAccount" value=""/>--%>
                    <input type="hidden" id="txtResponsibleUserId" value="0"/>
                    <input type="button" onclick="selUser('txtResponsibleUserId','txtResponsibleUserAccount','txtResponsibleUserRealName',false);" value="选择" class="btnCommon" />
                </td>
            </tr>
        </table>
    </form>
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
        <table id="actionGrid" style="width:900px;height:auto" title="行动">
		    <thead>
			    <tr>
                    <th field="remark" resizable="false" width="170">行动描述</th>
                    <th field="userName" resizable="false" width="80">负责人</th>
                    <th field="endDate" resizable="false" width="80">完成时间</th>
                    <th field="result" resizable="false" width="170">结果</th>
                    <th field="resultFileName" resizable="false" width="110">附件</th>
                    <th field="resultFileId" resizable="false" formatter="fileActionFormater" width="80"></th>
			    </tr>
		    </thead>
	    </table>
    </div>
    <br />
    <form id="reasonAndMeasuresForm">
       <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    原因分析<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <textarea name="reason" class="textInput required" style="width: 688px;" rows="5"></textarea>
                </td>
            </tr>
        </table>
        <table id="reasonAttachments" style="width:900px;height:auto" title="原因分析附件">
		    <thead>
			    <tr> 
				    <th field="fileName" resizable="false" width="200">附件名称</th>
                    <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			    </tr>
		    </thead>
	    </table>
       <br />
       <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    纠正预防措施<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <textarea name="measures" class="textInput required" style="width: 688px;" rows="5"></textarea>
                </td>
            </tr>
        </table>
        <table id="measuresAttachments" style="width:900px;height:auto" title="纠正预防措施附件">
		    <thead>
			    <tr> 
				    <th field="fileName" resizable="false" width="200">附件名称</th>
                    <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			    </tr>
		    </thead>
	    </table>
    </form>
    
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
        $.get("VocController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm, #responnsibleUserPreviousForm, #reasonAndMeasuresForm").setFormValue(data);
            $("#basicInfoForm").setFormReadOnly();
            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            if (data.actions) {
                $("#actionGrid").datagrid("loadData", data.actions)
            }
            if (data.reasonFiles) {
                $("#reasonAttachments").datagrid("loadData", data.reasonFiles)
            }
            if (data.measuresFiles) {
                $("#measuresAttachments").datagrid("loadData", data.measuresFiles)
            }
            $("#remarks").datagrid("loadData", data.remarks);
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#responnsibleUserPreviousForm").validAndFocus()) {
                return;
            }
            if (!$("#reasonAndMeasuresForm").validAndFocus()) {
                return;
            }
            var args = { taskId: taskId };
            var responnsibleUserPrevious = $("#responnsibleUserPreviousForm").getFormValue();
            args.responsibleUserPreviousName = responnsibleUserPrevious.responsibleUserPreviousName;
            args.responsibleUserPreviousAccount = responnsibleUserPrevious.responsibleUserPreviousAccount;
            
            var reasonAndMeasures = $("#reasonAndMeasuresForm").getFormValue();
            args.reason = reasonAndMeasures.reason;
            args.measures = reasonAndMeasures.measures;

            var reasonFiles = $('#reasonAttachments').datagrid('getData');
            args.reasonFiles = reasonFiles.rows;
            var measuresFiles = $('#measuresAttachments').datagrid('getData');
            args.measuresFiles = measuresFiles.rows;

            args.submitRemark = $.getFormValue("#remarkForm").submitRemark;
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("VocController.aspx?action=ResponsibleSubmit", { formJson: argsJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });

        $("#measuresAttachments").attachmentsGrid();
        $("#reasonAttachments").attachmentsGrid();

        $("#responnsibleUserPreviousForm, #reasonAndMeasuresForm").validate();

        $("#remarks, #actionGrid, #attachments").datagrid({
            rownumbers: true
        });
    })
</script>