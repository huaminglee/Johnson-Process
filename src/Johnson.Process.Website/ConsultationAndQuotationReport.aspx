<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotationReport.aspx.cs" Inherits="Johnson.Process.Website.ConsultationAndQuotationReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>技术咨询及报价流程报表</title>
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
    <div style="width: 230px; margin: auto;"><h2>技术咨询及报价流程报表</h2></div>
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
                申请日期
            </td>
            <td style="width: 280px" class="textCol dateRange">
                <input name="applyTimeStart" type="text" style="width: 85px" value="<%=DateTime.Now.AddDays(-20).ToString("yyyy-MM-dd") %>" class="textInput txtwidth  dateISO" />到
                <input name="applyTimeEnd" type="text" style="width: 85px" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" class="textInput txtwidth  dateISO" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                To市场部
            </td>
            <td style="width: 280px" class="textCol">
                <input name="marketingEngineer" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                办事处
            </td>
            <td style="width: 280px" class="textCol">
                <input name="applyUserDepartmentName" type="text" class="textInput txtwidth" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                项目名称
            </td>
            <td style="width: 280px" class="textCol">
                <input name="projectName" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                预计签合同日期
            </td>
            <td style="width: 280px" class="textCol dateRange">
                <input name="expectSignContactDateStart" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />到
                <input name="expectSignContactDateEnd" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                
            </td>
            <td style="width: 280px" class="textCol">
                
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
    <table id="reportGrid" style="width:1100px;height:auto" title="技术咨询及报价信息">
		<thead>
			<tr>
				<th field="applyUserName" resizable="false" width="90">发起人</th>
				<th field="applyTime" resizable="false" width="120">发起时间</th>
                <th field="marketingEngineer" resizable="false" width="80">To市场部</th>
                <th field="applyUserDepartmentName" resizable="false" width="150">办事处</th>
				<th field="projectName" resizable="false" width="100">项目名称</th>
                <th field="succeedProbability" resizable="false" width="80">成功机会(%)</th>
                <th field="expectSignContact" resizable="false" width="120">预计签合同日期</th>
                <th field="incidentNo" formatter="actionRender" resizable="false" width="120">操作</th>
			</tr>
		</thead>
	</table>
</body>
</html>

<script language="javascript" type="text/javascript">
    $(function () {
        $.get("ConsultationAndQuotationReportController.aspx?action=get", { r: Math.random() }, function (data) {
             $('#reportGrid').datagrid('loadData', data);
        });

        $(".dateRange").dateRange();

        $("#searchForm input[name='btnSearch']").button().click(function(){
            var formValue = $("#searchForm").getFormValue();
            var formJson = $.toJSON(formValue);

            $.post("ConsultationAndQuotationReportController.aspx?action=search", { formJson: formJson }, function (data) {
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

    function actionRender(incidentNo, row){
        var processLink = "../WorkFlow/Common/UltimusWfTxStatus.aspx?pIncidentNo="+incidentNo+"&pProcessName=" + encodeURIComponent("技术咨询及报价");
        var detailsLink = "ConsultationAndQuotation_Completed2.aspx?incidentNo="+incidentNo;
        return "<a style='padding: 5px;' target='_blank' href='"+processLink+"'>流程信息</a><a target='_blank' href='"+detailsLink+"'>详细信息</a>";
    }
</script>