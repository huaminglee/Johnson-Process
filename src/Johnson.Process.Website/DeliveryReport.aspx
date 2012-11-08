<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliveryReport.aspx.cs" Inherits="Johnson.Process.Website.DeliveryReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>货期管理流程报表</title>
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
    <div style="width: 200px; margin: auto;"><h2>货期管理流程报表</h2></div>
    <form id="searchForm" runat="server">
    <table class="formInfo">
        <tr>
            <td style="width: 200px" class="labelCol">
                发起人
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="applyUserName" type="text" class="textInput txtwidth " />
            </td>
            <td style="width: 200px" class="labelCol">
                发起日期
            </td>
            <td style="width: 280px" class="textCol dateRange">
                <input name="applyTimeStart" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />到
                <input name="applyTimeEnd" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                订单号
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtOrderNumber" name="orderNumber" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                项目名称
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtProjectName" name="projectName" type="text" class="textInput txtwidth required" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                销售办事处
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtSaleOffice" name="saleOffice" type="text" class="textInput txtwidth required" />
            </td>
            <td style="width: 200px" class="labelCol">
                销售组
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtSaleGroup" name="saleGroup" type="text" class="textInput txtwidth" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                销售工程师
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtSaleEngineerYT" name="saleEngineerYT" type="text" class="textInput txtwidth required" />
            </td>
            <td style="width: 200px" class="labelCol">
                预计下单日期
            </td>
            <td style="width: 280px" class="textCol dateRange">
                <input name="bookDateStart" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />到
                <input name="bookDateEnd" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                要求出厂日期
            </td>
            <td style="width: 280px" class="textCol dateRange">
                <input name="requestOutDateStart" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />到
                <input name="requestOutDateEnd" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />
            </td>
            <td style="width: 200px" class="labelCol">
                
            </td>
            <td style="width: 280px" class="textCol">
                
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                流程状态
            </td>
            <td style="width: 280px" class="textCol">
                <select name="taskStatus">
                    <option value="1">处理中</option>
                    <option value="2">已完成</option>
                </select>
            </td>
            <td style="width: 200px" >
                <input type="button" name="btnSearch" value="查询"/>
                <input type="reset" name="btnReset" value="重置"/>
            </td>
            <td style="width: 280px" >
                
            </td>
        </tr>
    </table>

    </form>
    <table id="reportGrid" style="width:1100px;height:auto" title="货期信息">
		<thead>
			<tr>
				<th field="applyUserName" resizable="false" width="90">发起人</th>
                <th field="applyTime" resizable="false" width="120">发起日期</th>
                <th field="orderNumber" resizable="false" width="80">订单号</th>
                <th field="projectName" resizable="false" width="150">项目名称</th>
				<th field="saleOffice" resizable="false" width="90">销售办事处</th>
                <th field="saleGroup" resizable="false" width="80">销售组</th>
                <th field="saleEngineerYT" resizable="false" width="80">销售工程师</th>
                <th field="bookDate" resizable="false" width="120">预计下单日期</th>
                <th field="requestOutDate" resizable="false" width="120">要求出厂日期</th>
                <th field="taskId" formatter="actionRender" resizable="false" width="120">操作</th>
			</tr>
		</thead>
	</table>
</body>
</html>

<script language="javascript" type="text/javascript">
    $(function () {
        $.get("DeliveryReportController.aspx?action=get", { r: Math.random() }, function (data) {
             $('#reportGrid').datagrid('loadData', data);
        });

        $(".dateRange").dateRange();

        $("#searchForm input[name='btnSearch']").button().click(function(){
            var formValue = $("#searchForm").getFormValue();
            var formJson = $.toJSON(formValue);

            $.post("DeliveryReportController.aspx?action=search", { formJson: formJson }, function (data) {
                $('#reportGrid').datagrid('loadData', data);
            });
        });

        $("#searchForm input[name='btnReset']").button();

        $("#reportGrid").datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false
        });
    })

    function actionRender(takId, row){
        var processLink = "../WorkFlow/Common/UltimusWfTxStatus.aspx?pIncidentNo="+row.incidentNo+"&pProcessName=VOC&taskid=" + takId;
        var detailsLink = "Delivery_Completed.aspx?taskId="+takId;
        return "<a style='padding: 5px;' target='_blank' href='"+processLink+"'>流程信息</a><a target='_blank' href='"+detailsLink+"'>详细信息</a>";
    }
</script>