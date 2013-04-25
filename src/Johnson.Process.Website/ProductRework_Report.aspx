<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductRework_Report.aspx.cs" Inherits="Johnson.Process.Website.ProductRework_Report" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>返工返修流程报表</title>
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
    <div style="width: 200px; margin: auto;"><h2>返工返修流程报表</h2></div>
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
                <input name="startTimeStart" type="text" style="width: 85px" value="<%=DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd") %>" class="textInput txtwidth  dateISO" />到
                <input name="startTimeEnd" type="text" style="width: 85px" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" class="textInput txtwidth  dateISO" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                不合格编号
            </td>
            <td style="width: 280px" class="textCol">
                <input name="FailureNo" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                产品类型
            </td>
            <td style="width: 280px" class="textCol">
                <select name="ProductType">
                    <option></option>
                    <option value="0">零部件</option>
                    <option value="1">产品</option>
                </select>
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                系列号
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="XLH" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                名称
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="Name" type="text" class="textInput txtwidth required" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                SAP号
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="SapNo" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                订单号
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="OrderNumber" type="text" class="textInput txtwidth" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                发出部门
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="StartDepartment" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                
            </td>
            <td style="width: 280px" class="textCol">
                
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
                <input type="button" name="btnDaochu" value="导出Excel"/>
            </td>
            <td >
                
            </td>
        </tr>
    </table>

    </form>
    <table id="reportGrid" style="width:auto;height:auto" title="返工返修信息">
		<thead>
			<tr>
				<th field="startUserName" resizable="false" width="80">发起人</th>
                <th field="startTime" resizable="false" width="80">发起日期</th>
				<th field="FailureNo" resizable="false" width="100">不合格编号</th>
                <th field="ProductType" resizable="false" width="100">产品类型</th>
                <th field="XLH" resizable="false" width="100">系列号</th>
                <th field="Name" resizable="false" width="100">名称</th>
				<th field="SapNo" resizable="false" width="100">SAP号</th>
                <th field="Quantity" resizable="false" width="100">数量</th>
                <th field="OrderNumber" resizable="false" width="100">订单号</th>
                <th field="StartDepartment" resizable="false" width="80">发出部门</th>
                <th field="GS" resizable="false" width="80">工时</th>
                <th field="GSFY" resizable="false" width="80">工时费用</th>
                <th field="WLFY" resizable="false" width="80">物料费用</th>
                <th field="ZFY" resizable="false" width="80">总费用</th>
                <th field="incidentNo" formatter="actionRender" resizable="false" width="120">操作</th>
			</tr>
		</thead>
	</table>
</body>
</html>

<script language="javascript" type="text/javascript">
    $(function () {

        $(".dateRange").dateRange();

        $("#searchForm input[name='btnSearch']").button().click(function(){
            var formValue = $("#searchForm").getFormValue();
            var formJson = $.toJSON(formValue);

            $.post("ProductRework_ReportController.aspx?action=search", { formJson: formJson }, function (data) {
                $('#reportGrid').datagrid('loadData', data);
            });
        }).click();

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

            $.post("ProductRework_ReportController.aspx?action=Daochu", { formJson: formJson }, function (data) {
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
        var detailsLink = "ProductRework_Completed.aspx?incNo="+incidentNo;
        return "<a style='padding: 5px;' target='_blank' href='"+processLink+"'>流程信息</a><a target='_blank' href='"+detailsLink+"'>详细信息</a>";
    }
</script>