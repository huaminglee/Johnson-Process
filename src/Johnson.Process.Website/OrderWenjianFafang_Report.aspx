<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderWenjianFafang_Report.aspx.cs" Inherits="Johnson.Process.Website.OrderWenjianFafang_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单评审报表</title>
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
    <div style="width: 200px; margin: auto;"><h2>订单评审报表</h2></div>
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
                <input name="startTimeStart" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />到
                <input name="startTimeEnd" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                交货日期
            </td>
            <td style="width: 280px" class="textCol dateRange">
                <input name="jiaoHuoRiQiStart" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />到
                <input name="jiaoHuoRiQiEnd" type="text" style="width: 85px" class="textInput txtwidth  dateISO" />
            </td>
            <td style="width: 200px" class="labelCol">
                SO NO
            </td>
            <td style="width: 280px" class="textCol">
                <input name="SONO" type="text" class="textInput txtwidth " />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                JDS NO
            </td>
            <td style="width: 280px" class="textCol">
                <input name="JDSNO" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                项目名称
            </td>
            <td style="width: 280px" class="textCol">
                <input name="xiangMingCheng" type="text" class="textInput txtwidth required" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                办事处
            </td>
            <td style="width: 280px" class="textCol">
                <input name="banShiChu" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                办事处联系人
            </td>
            <td style="width: 280px" class="textCol">
                <input name="banShiChuLianXiRen" type="text" class="textInput txtwidth " />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                产品类型
            </td>
            <td style="width: 280px" class="textCol">
                <select name="chanPinLeiXing" class="textInput txtwidth " >
                    <option></option>
                    <option>VAV</option>
                    <option>YAEP</option>
                    <option>YAH</option>
                    <option>YAR</option>
                    <option>YBDB</option>
                    <option>YBOC</option>
                    <option>YBOH</option>
                    <option>YBW</option>
                    <option>YCAB</option>
                    <option>YCAE</option>
                    <option>YCAG</option>
                    <option>YCWE</option>
                    <option>YDCC</option>
                    <option>YDCK</option>
                    <option>YDCP</option>
                    <option>YDFC</option>
                    <option>YDOC</option>
                    <option>YDOH</option>
                    <option>YEAS</option>
                    <option>YGAS</option>
                    <option>YGCC</option>
                    <option>YGFC</option>
                    <option>YGOC</option>
                    <option>YGOH</option>
                    <option>YHAC</option>
                    <option>YLAA</option>
                    <option>YLPA</option>
                    <option>YMAC</option>
                    <option>YMOC</option>
                    <option>YMOH</option>
                    <option>YSAC</option>
                    <option>YSE</option>
                    <option>YSM</option>
                    <option>YSM-B</option>
                    <option>YSOC</option>
                    <option>YSOH</option>
                    <option>YVAA</option>
                    <option>YVOH</option>
                    <option>YWHA</option>
                    <option value="OTHER">其它</option>
                </select>
            </td>
            <td style="width: 200px" class="labelCol">
                ITEM
            </td>
            <td style="width: 280px" class="textCol">
                <input name="sapItem" type="text" class="textInput txtwidth " />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                是否标准订单
            </td>
            <td style="width: 280px" class="textCol">
                <select name="isStandard">
                    <option></option>
                    <option value="true">是</option>
                    <option value="false">否</option>
                </select>
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
    <table id="reportGrid" style="width:1100px;height:auto" title="订单信息">
		<thead>
			<tr>
				<th field="startUserName" resizable="false" width="80">发起人</th>
                <th field="startTime" resizable="false" width="80">发起日期</th>
				<th field="isStandard" resizable="false" width="70">标准订单</th>
				<th field="jiaoHuoRiQi" resizable="false" width="80">交货日期</th>
                <th field="SONO" resizable="false" width="100">SO NO</th>
                <th field="JDSNO" resizable="false" width="100">JDS NO</th>
                <th field="xiangMingCheng" resizable="false" width="100">项目名称</th>
				<th field="banShiChu" resizable="false" width="80">办事处</th>
                <th field="banShiChuLianXiRen" resizable="false" width="80">办事处联系人</th>
                <th field="chanPinLeiXing" resizable="false" width="80">产品类型</th>
                <th field="sapItem" resizable="false" width="80">ITEM</th>
                <th field="taskId" formatter="actionRender" resizable="false" width="120">操作</th>
			</tr>
		</thead>
	</table>
</body>
</html>

<script language="javascript" type="text/javascript">
    $(function () {
        $.get("OrderPingshen_ReportController.aspx?action=get", { r: Math.random() }, function (data) {
             $('#reportGrid').datagrid('loadData', data);
        });

        $(".dateRange").dateRange();

        $("#searchForm input[name='btnSearch']").button().click(function(){
            var formValue = $("#searchForm").getFormValue();
            var formJson = $.toJSON(formValue);

            $.post("OrderPingshen_ReportController.aspx?action=search", { formJson: formJson }, function (data) {
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
        var processLink = "/EDoc2v4/Common/UltimusWfTxStatus.aspx?pIncidentNo="+row.incidentNo+"&pProcessName="+encodeURIComponent("广州订单评审")+"&taskid=" + takId;
        var detailsLink = "OrderPingShen_Completed1.aspx?taskId="+takId;
        return "<a style='padding: 5px;' target='_blank' href='"+processLink+"'>流程信息</a><a target='_blank' href='"+detailsLink+"'>详细信息</a>";
    }
</script>
