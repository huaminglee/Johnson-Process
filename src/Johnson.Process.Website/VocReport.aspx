<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VocReport.aspx.cs" Inherits="Johnson.Process.Website.VocReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VOC流程报表</title>
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
    <div style="width: 200px; margin: auto;"><h2>VOC流程报表</h2></div>
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
                办事处
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="applyUserDepartmentName" type="text" class="textInput txtwidth " />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                投诉日期
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="applyTimeStart" type="text" style="width: 85px" value="<%=DateTime.Now.AddDays(-20).ToString("yyyy-MM-dd") %>" class="textInput txtwidth  dateISO" />到
                <input  name="applyTimeEnd" type="text" style="width: 85px" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" class="textInput txtwidth  dateISO" />
            </td>
            <td style="width: 200px" class="labelCol">
                项目名称
            </td>
            <td style="width: 280px" class="textCol">
                <input name="projectName" type="text" class="textInput txtwidth " />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                机组型号
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="machineModel" type="text" class="textInput txtwidth " />
            </td>
            <td style="width: 200px" class="labelCol">
                机身编号
            </td>
            <td style="width: 280px" class="textCol">
                <input  name="machineCode" type="text" class="textInput txtwidth " />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                投诉编号
            </td>
            <td style="width: 280px" class="textCol">
                <input name="vocCode" type="text" class="textInput txtwidth " />
            </td>
            <td style="width: 200px" class="labelCol">
                故障描述
            </td>
            <td style="width: 280px" class="textCol">
                <input name="faultRemark" type="text" class="textInput txtwidth " />
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
            <td style="width: 280px" >
                
            </td>
        </tr>
    </table>

    </form>
    <table id="complaintGrid" style="width:1100px;height:auto" title="投诉信息">
		<thead>
			<tr>
				<th field="applyUserName" resizable="false" width="90">发起人</th>
                <th field="applyUserDepartmentName" resizable="false" width="150">办事处</th>
                <th field="applyTime" resizable="false" width="80">投诉日期</th>
				<th field="machineModel" resizable="false" width="90">机组型号</th>
                <th field="machineCode" resizable="false" width="150">机身编号</th>
                <th field="tempMeasure" resizable="false" width="90">现场临时行动</th>
                <th field="needCompleteDate" resizable="false" width="120">临时行动完成时间</th>
                <th field="faultRemark" resizable="false" width="120">故障描述</th>
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

        $.get("VocReportController.aspx?action=search", { formJson: formJson, r: Math.random() }, function (data) {
             $('#complaintGrid').datagrid('loadData', data);
        });

        //$(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#searchForm input[name='applyTimeStart']").datepicker({ changeMonth: true, changeYear: true, onSelect: function(selectedDate){
            $( "#searchForm input[name='applyTimeEnd']" ).datepicker( "option", "minDate", selectedDate );
        }});

        $("#searchForm input[name='applyTimeEnd']").datepicker({ changeMonth: true, changeYear: true, onSelect: function(selectedDate){
            $( "#searchForm input[name='applyTimeStart']" ).datepicker( "option", "maxDate", selectedDate );
        }});

        $("#searchForm input[name='btnSearch']").button().click(function(){
            var formValue = $("#searchForm").getFormValue();
            var formJson = $.toJSON(formValue);

            $.post("VocReportController.aspx?action=search", { formJson: formJson }, function (data) {
                $('#complaintGrid').datagrid('loadData', data);
            });
            return false;
        });

        $("#searchForm input[name='btnReset']").button();

        $("#searchForm input[name='btnDaochu']").button().click(function(){
            var self = this;
            $(this).attr("disabled", "disabled");
            var formValue = $("#searchForm").getFormValue();
            var formJson = $.toJSON(formValue);

            $.post("VocReportController.aspx?action=Daochu", { formJson: formJson }, function (data) {
                $(self).removeAttr("disabled");
                if(data.result != 0){
                    alert(data.message);
                    return;
                }
                window.open("VocReportExcelDownload.aspx?file="+data.data);
            });
            return false;
        });

        $("#complaintGrid").datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false
        });
    })

    function actionRender(incidentNo, row){
        var processLink = "../WorkFlow/Common/UltimusWfTxStatus.aspx?pIncidentNo="+row.incidentNo+"&pProcessName=VOC";
        var detailsLink = "Voc_Completed2.aspx?incidentNo="+incidentNo;
        return "<a style='padding: 5px;' target='_blank' href='"+processLink+"'>流程信息</a><a target='_blank' href='"+detailsLink+"'>详细信息</a>";
    }
</script>