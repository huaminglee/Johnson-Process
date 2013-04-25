<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPingShen_Completed2.aspx.cs" Inherits="Johnson.Process.Website.OrderPingShen_Completed2" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/OrderDetails.ascx" TagName="OrderDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单评审</title>
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
    <johnson:Header runat="server" HeaderTitle="订单评审" ID="header"></johnson:Header>
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
    <div style="margin-top: 1em;">
        <table id="fafangLiuchengGrid" style="width:900px;height:auto" title="文件发放流程">
		    <thead>
			    <tr> 
				    <th field="startUserName" resizable="false" width="80">发起人</th>
                    <th field="startTime" resizable="false" width="80">发起日期</th>
                    <th field="fafangWancheng" resizable="false" width="80">资料完整度</th>
                    <th field="sheJiShuoMing" resizable="false" width="180">设计说明</th>
                    <th field="incidentNo" formatter="actionRender" resizable="false" width="120">操作</th>
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
</body>
</html>
<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";

    $(function () {
        $.get("OrderPingShenController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data).setFormReadOnly();
            if(data.files){
                $("#attachments").datagrid('loadData', data.files);
            }
            if(data.items){
                $("#items").datagrid('loadData', data.items);
            }
            if(data.wanjianFafangList){
                $("#fafangLiuchengGrid").datagrid('loadData', data.wanjianFafangList);
            }
            if(data.sheJiZiLiao){
                $("#sheJiZiLiao").datagrid('loadData', data.sheJiZiLiao);
            }
            $("#remarks").datagrid("loadData", data.approves);
        });
        $("#attachments, #remarks, #sheJiZiLiao, #items, #fafangLiuchengGrid").datagrid();
    })

    function actionRender(incidentNo, row){
        var processLink = "/EDoc2v4/Workflow/Common/UltimusWfTxStatus.aspx?pIncidentNo="+row.incidentNo+"&pProcessName="+encodeURIComponent("广州订单文件发放");
        var detailsLink = "OrderWenjianFafang_Completed.aspx?incNo="+incidentNo;
        return "<a style='padding: 5px;' target='_blank' href='"+processLink+"'>流程信息</a><a target='_blank' href='"+detailsLink+"'>详细信息</a>";
    }
</script>

