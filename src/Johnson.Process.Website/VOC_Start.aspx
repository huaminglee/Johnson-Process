<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VOC_Start.aspx.cs" Inherits="Johnson.Process.Website.VOC_Start" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocDetails.ascx" TagName="VocDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/VocComplaint.ascx" TagName="VocComplaint" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>VOC-ASD</title>
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
    <%--head --%>
    <johnson:Header runat="server" HeaderTitle="VOC-ASD" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    发起人<span style="color: Red" >*</span>
                </td>
                <td style="width: 280px" class="textCol">
                    <input  name="applyUserName" type="text" readonly="readonly" class="textInput txtwidth required" />
                </td>
                <td style="width: 200px" class="labelCol">
                    办事处<span style="color: Red" >*</span>
                </td>
                <td style="width: 280px" class="textCol">
                    <input  name="applyUserDepartmentName" type="text" class="textInput txtwidth required" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    投诉日期<span style="color: Red" >*</span>
                </td>
                <td style="width: 280px" class="textCol">
                    <input  name="applyTime" type="text" class="textInput txtwidth required dateISO" />
                </td>
                <td style="width: 200px" class="labelCol">
                    项目名称<span style="color: Red" >*</span>
                </td>
                <td style="width: 280px" class="textCol">
                    <input name="projectName" type="text" class="textInput txtwidth required" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px" class="labelCol">
                    投诉编号<span style="color: Red" >*</span>
                </td>
                <td style="width: 280px" class="textCol">
                    <input  name="vocCode" readonly="readonly" type="text" class="textInput " />
                </td>
                <td style="width: 200px" >
                    
                </td>
                <td style="width: 280px" >
                    
                </td>
            </tr>
        </table>
    </form>
    
    <div id="addVocComplaint" title="添加投诉信息">
        <form>
            <johnson:VocComplaint runat="server" ID="vocComplaint"></johnson:VocComplaint>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定并继续"/>
                <input type="button" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>

    <div id="editVocComplaint" title="编辑投诉信息">
        <form>
            <johnson:VocComplaint runat="server" ID="vocComplaint1"></johnson:VocComplaint>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>

    <div style="margin-top: 1em;">
        <table id="complaintGrid" style="width:900px;height:auto" title="投诉信息">
		    <thead>
			    <tr>
				    <th field="machineModel" resizable="false" width="90">机组型号</th>
                    <th field="machineCode" resizable="false" width="150">机身编号</th>
                    <th field="faultQuantity" resizable="false" width="90">故障数量</th>
                    <th field="faultCategory" resizable="false" width="90">故障类别</th>
                    <th field="tempMeasure" resizable="false" width="90">现场临时行动</th>
                    <th field="needCompleteDate" resizable="false" width="120">临时行动完成时间</th>
                    <th field="faultRemark" resizable="false" width="120">故障描述</th>
                    <th field="responsibleUserName" resizable="false" width="90">投诉处理负责人</th>
			    </tr>
		    </thead>
	    </table>
    </div>

    <div class="panel-header" style="margin-top: 2em;"><div class="panel-title">备注</div></div>
    <form id="remarkForm">
        <table class="formInfo">
            <tbody >
                <tr>
                    <td class="labelCol" style="width: 200px">
                        备注
                    </td>
                    <td class="textCol">
                        <textarea name="submitRemark" class="textInput" style="width: 500px;" rows="3"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>

    <div style="padding: 2em 0 0 30em;">
        <input type="button" id="btnSubmit" value="提交" />
    </div>
</body>
</html>
<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";
    var tempFolderId = "<%= this.ProcessFolderId %>";
    $(function () {
        $.get("VocController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data);
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#basicInfoForm").validAndFocus()) {
                return;
            }
            if ($("#complaintGrid").datagrid("getData").rows.length == 0) {
                alert("请添加投诉信息!");
                return;
            }
            var args = $.getFormValue("#basicInfoForm");
            args.complaints = $("#complaintGrid").datagrid("getData").rows;
            args.submitRemark = $.getFormValue("#remarkForm").submitRemark;
            var argsJson = $.toJSON(args);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("VocController.aspx?action=start", { taskId: taskId, formJson: argsJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });
        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#basicInfoForm").validate();
        
        $("#addVocComplaint table[name='attachments'], #editVocComplaint table[name='attachments']").attachmentsGrid();
        $(".singleUserSelect").singleSelectUser();

        var addVocComplaint = $("#addVocComplaint").dialog({ autoOpen: false, modal: true, width: 500 });
        var editVocComplaint = $("#editVocComplaint").dialog({ autoOpen: false, modal: true, width: 500 });
        $("#complaintGrid").processDataGrid({
            addDialog: addVocComplaint, 
            editDialog: editVocComplaint, 
            addingRow: function(sender, args){
                args.row.files = args.addDialogForm.find("table[name='attachments']").datagrid("getData").rows;
            },
            addedRow: function(sender, args){
                args.addDialogForm.find("table[name='attachments']").datagrid("loadData", []);
            },
            editingRow: function(sender, args){
                args.row.files = args.editDialogForm.find("table[name='attachments']").datagrid("getData").rows;
            },
            initedEditForm: function(sender, args){
                setTimeout(function(){
                    if(args.row.files){
                        args.editDialog.find("table[name='attachments']").datagrid("loadData", args.row.files);
                    }
                }, 200);
            }
        });
    })
</script>