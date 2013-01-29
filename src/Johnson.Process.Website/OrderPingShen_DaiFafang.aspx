<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPingShen_DaiFafang.aspx.cs" Inherits="Johnson.Process.Website.OrderPingShen_DaiFafang" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>待发放文件订单</title>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/icon.css" />
	<script type="text/javascript" src="jquery-easyui/jquery.easyui.min.js"></script>
    <script src="js/jquery.tmpl.min.js" type="text/javascript"></script>
    <script src="js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="jqueryui/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="js/processCommon.js" type="text/javascript"></script>
    <script src="js/processDataGrid.js" type="text/javascript"></script>
    <script src="js/jqueryExtend.js" type="text/javascript"></script>
    <script src="js/jquery.json-2.3.js" type="text/javascript"></script>
    <link href="jqueryui/cupertino/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div style="width: 200px; margin: auto;"><h2>待发放文件订单</h2></div>
    <table id="reportGrid" style="width:1100px;height:auto" title="订单信息">
		<thead>
			<tr>
				<th field="startUserName" resizable="false" width="80">发起人</th>
                <th field="startTime" resizable="false" width="80">发起日期</th>
                <th field="SONO" resizable="false" width="100">SO NO</th>
                <th field="JDSNO" resizable="false" width="100">JDS NO</th>
                <th field="xiangMingCheng" resizable="false" width="100">项目名称</th>
                <th field="chanPinLeiXing" resizable="false" width="80">产品类型</th>
                <th field="incidentNo" formatter="actionRender" resizable="false" width="180">操作</th>
			</tr>
		</thead>
	</table>
</body>
</html>

<script language="javascript" type="text/javascript">
    $(function () {
        $.get("OrderPingshen_DaiFafangController.aspx?action=get", { r: Math.random() }, function (data) {
            if(data){
                $('#reportGrid').datagrid('loadData', data);
            }
        });

        $("#reportGrid").datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false
        });
    })

    function actionRender(incidentNo, row){
        var detailsLink = "OrderPingShen_Completed1.aspx?incNo="+row.incidentNo;
        var shejiTijiaoLink = "OrderWenjianFafang_Start.aspx?incNo="+row.incidentNo + "&taskId="+row.startTaskId;
        return "<a style='padding: 5px;' target='_blank' href='"+shejiTijiaoLink+"'>发放文件</a><a target='_blank' href='"+detailsLink+"'>详细信息</a>";
    }
</script>