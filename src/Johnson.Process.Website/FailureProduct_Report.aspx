<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FailureProduct_Report.aspx.cs" Inherits="Johnson.Process.Website.FailureProduct_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>不合格品流程报表</title>
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
    <div style="width: 200px; margin: auto;"><h2>不合格品流程报表</h2></div>
    <form id="searchForm" runat="server">
    <table class="formInfo">
        <tr>
            <td style="width: 200px" class="labelCol">
                发起人
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="startUserName" type="text" class="textInput txtwidth " />
            </td>
            <td style="width: 200px" class="labelCol">
                发起日期
            </td>
            <td style="width: 280px" class="textCol dateRange">
                <input name="startTimeStart" type="text" style="width: 85px" value="<%=DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") %>" class="textInput txtwidth  dateISO" />到
                <input name="startTimeEnd" type="text" style="width: 85px" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" class="textInput txtwidth  dateISO" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                不合格编号
            </td>
            <td style="width: 280px" class="textCol">
                <input name="No" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                零件号
            </td>
            <td style="width: 280px" class="textCol">
                <input name="ComponentCode" type="text" class="textInput txtwidth required" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                零件名称
            </td>
            <td style="width: 280px" class="textCol">
                <input name="ComponentName" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                部件系列号
            </td>
            <td style="width: 280px" class="textCol">
                <input name="BJXLH" type="text" class="textInput txtwidth required" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                机组系列号
            </td>
            <td style="width: 280px" class="textCol">
                <input name="JZXLH" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                供应商名称
            </td>
            <td style="width: 280px" class="textCol">
                <input name="GYSMC" type="text" class="textInput txtwidth required" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                处理结果
            </td>
            <td style="width: 280px" class="textCol">
                <input name="Result" type="radio" value="1"/><label>退回供应商</label>
                <input name="Result" type="radio" value="2" /><label>让步接收</label>
                <input name="Result" type="radio" value="3" /><label>返工/返修</label>
                <input name="Result" type="radio" value="4" /><label>报废</label>
                <input name="Result" type="radio" value="5" /><label>挑选</label>
                <input name="Result" type="radio" value="7" /><label>其它</label>
            </td>
            <td style="width: 200px" >
                <input type="button" name="btnSearch" value="查询"/>
                <input type="reset" name="btnReset" value="重置"/>
                <input type="button" name="btnDaochu" value="导出Excel"/>
            </td>
            <td >
                
            </td>
        </tr>
    </table>

    </form>
    <table id="reportGrid" style="width:1100px;height:auto" title="不合格信息">
		<thead>
			<tr>
				<th field="startUserName" resizable="false" width="80">发起人</th>
                <th field="startTime" resizable="false" width="80">发起日期</th>
				<th field="No" resizable="false" width="100">不合格编号</th>
                <th field="ComponentCode" resizable="false" width="100">零件号</th>
                <th field="ComponentName" resizable="false" width="100">零件名称</th>
                <th field="BJXLH" resizable="false" width="100">部件系列号</th>
				<th field="JZXLH" resizable="false" width="100">机组系列号</th>
                <th field="GYSMC" resizable="false" width="100">供应商名称</th>
                <th field="ZRBM" resizable="false" width="80">责任部门</th>
                <th field="Result" resizable="false" width="80">处理结果</th>
                <th field="incidentNo" formatter="actionRender" resizable="false" width="120">操作</th>
			</tr>
		</thead>
	</table>
</body>
</html>

<script language="javascript" type="text/javascript">
    $(function () {
        var formValue = $("#searchForm").getFormValue();
        var formJson = $.toJSON(formValue);
        $.get("FailureProduct_ReportController.aspx?action=search", {formJson: formJson, r: Math.random() }, function (data) {
             $('#reportGrid').datagrid('loadData', data);
        });

        $(".dateRange").dateRange();

        $("#searchForm input[name='btnSearch']").button().click(function(){
            var formValue = $("#searchForm").getFormValue();
            var formJson = $.toJSON(formValue);

            $.post("FailureProduct_ReportController.aspx?action=search", { formJson: formJson, r: Math.random() }, function (data) {
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
        $("#searchForm input[name='btnDaochu']").button().click(function(){
            var self = this;
            $(this).attr("disabled", "disabled");
            var formValue = $("#searchForm").getFormValue();
            var formJson = $.toJSON(formValue);

            $.post("FailureProduct_ReportController.aspx?action=Daochu", { formJson: formJson }, function (data) {
                $(self).removeAttr("disabled");
                if(data.result != 0){
                    alert(data.message);
                    return;
                }
                window.open("ProductReworkReportExcelDownload.aspx?file="+data.data);
            });
            return false;
        });
    })

    function actionRender(incidentNo, row){
        var processLink = "/EDoc2v4/Workflow/Common/UltimusWfTxStatus.aspx?pIncidentNo="+row.incidentNo+"&pProcessName="+encodeURIComponent("不合格品处理");
        var detailsLink = "FailureProduct_Completed.aspx?incNo="+incidentNo;
        return "<a style='padding: 5px;' target='_blank' href='"+processLink+"'>流程信息</a><a target='_blank' href='"+detailsLink+"'>详细信息</a>";
    }
</script>