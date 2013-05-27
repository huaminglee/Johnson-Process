<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Voc_StartCompleted.aspx.cs" Inherits="Johnson.Process.Website.Voc_StartCompleted" %>
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
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>
    <form id="basicInfoForm">
        <johnson:VocDetails runat="server" ID="vocDetails"></johnson:VocDetails>
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
    <br />
    <form id="solutionsForm">
       <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 200px">
                    现场解决方案<span style="color: Red">*</span>
                </td>
                <td class="textCol">
                    <textarea name="solutions" class="textInput required" readonly="readonly" style="width: 688px;" rows="5"></textarea>
                </td>
            </tr>
            <tr>
                <td class="labelCol" style="width: 200px">
                    完成时间<span style="color: Red">*</span>
                </td>
                <td class="textCol" >
                    <input name="solutionsCompleteTime" type="text" class="textInput txtwidth required dateISO" />
                </td>
            </tr>
        </table>
        <table id="solutionsAttachments" style="width:900px;height:auto" title="现场解决方案附件">
		    <thead>
			    <tr> 
				    <th field="fileName" resizable="false" width="200">附件名称</th>
                    <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			    </tr>
		    </thead>
	    </table>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";
    $(function () {
        $.get("VocController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm, #solutionsForm").setFormValue(data).setFormReadOnly();
            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            if (data.solutionsFiles) {
                $('#solutionsAttachments').datagrid('loadData', data.solutionsFiles);
            }
            $("#remarks").datagrid("loadData", data.remarks);
        });

        $("#remarks,#solutionsAttachments, #attachments").datagrid({
            rownumbers: true
        });
    })
</script>