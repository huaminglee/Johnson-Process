<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Voc_Completed2.aspx.cs" Inherits="Johnson.Process.Website.Voc_Completed2" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocDetails.ascx" TagName="VocDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocActionCompletedDetails.ascx" TagName="VocActionCompletedDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>VOC</title>
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
    <johnson:Header runat="server" HeaderTitle="VOC" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">①基本信息</div></div>
    <form id="basicInfoForm">
        <johnson:VocDetails runat="server" ID="vocDetails"></johnson:VocDetails>
    <table id="attachments" style="width:900px;height:auto" >
		<thead>
			<tr> 
				<th field="fileName" resizable="false" width="200">附件名称</th>
                <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			</tr>
		</thead>
	</table>
    <div style="margin-top: 1em;">
        <div class="panel-header" ><div class="panel-title">②现场解决方案</div></div>
        <textarea name="solutions" class="textInput" style="width: 688px;" rows="5"></textarea>
    </div>
    <div style="margin-top: 1em;">
        <div class="panel-header" ><div class="panel-title">③行动</div></div>
        <table id="actionGrid" style="width:900px;height:auto">
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
    <div class="panel-header" ><div class="panel-title">④原因分析</div></div>
        <textarea name="reason" class="textInput required" style="width: 688px;" rows="5"></textarea>
        <table id="reasonAttachments" style="width:900px;height:auto">
		    <thead>
			    <tr> 
				    <th field="fileName" resizable="false" width="200">附件名称</th>
                    <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			    </tr>
		    </thead>
	    </table>
        <br />
        <div class="panel-header" ><div class="panel-title">⑤纠正预防措施</div></div>
        <textarea name="measures" class="textInput required" style="width: 688px;" rows="5"></textarea>
        <table id="measuresAttachments" style="width:900px;height:auto">
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
</body>
</html>
<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";
    var incidentNo = '<%= Request["incidentNo"] %>';
    $(function () {
        $.get("VocController.aspx?action=get", { taskId: taskId, incidentNo: incidentNo, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data).setFormReadOnly();
            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            if (data.reasonFiles) {
                $("#reasonAttachments").datagrid("loadData", data.reasonFiles)
            }
            if (data.measuresFiles) {
                $("#measuresAttachments").datagrid("loadData", data.measuresFiles)
            }
            if (data.actions) {
                $("#actionGrid").datagrid("loadData", data.actions)
            }
            $("#remarks").datagrid("loadData", data.remarks);
        });

        $("#remarks,#measuresAttachments,#reasonAttachments,#actionGrid, #attachments").datagrid({
            rownumbers: true
        });
    })
</script>