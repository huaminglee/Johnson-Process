<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductRework_CID.aspx.cs" Inherits="Johnson.Process.Website.ProductRework_CID" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ProductReworkDetails.ascx" TagName="ProductReworkDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ProductReworkCidDetails.ascx" TagName="ProductReworkCidDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>返工返修单-工艺方案</title>
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
    <johnson:Header runat="server" HeaderTitle="返工返修单-工艺方案" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:ProductReworkDetails runat="server" ID="productReworkDetails"></johnson:ProductReworkDetails>
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    品质状态描述<span style="color: Red" >*</span>
                </td>
                <td>
                    <textarea name="PZZTMS" class="textInput required" style="width: 688px;" rows="3"></textarea>
                </td>
            </tr>
        </table>

        <div style="margin-top: 1em;">
            <table id="engRequireGrid" style="width:900px;height:auto" title="技术要求">
		        <thead>
			        <tr>
				        <th field="Name" resizable="false" width="300">所需物料名称</th>
                        <th field="PN" resizable="false" width="100">P.N</th>
                        <th field="Quantity" resizable="false" width="100">数量</th>
			        </tr>
		        </thead>
	        </table>
        </div>
    
        <div style="margin-top: 1em;">
            <table id="engAttachments" style="width:900px;height:auto" title="技术要求附件信息">
		        <thead>
			        <tr> 
				        <th field="FileName" resizable="false" width="200">附件名称</th>
                        <th field="FileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			        </tr>
		        </thead>
	        </table>
        </div>
    </form>

    <div id="addCidDialog" title="添加返工方案">
        <form>
            <johnson:ProductReworkCidDetails runat="server" ID="addProductReworkCidDetails"></johnson:ProductReworkCidDetails>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定并继续"/>
                <input type="button" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>
    <div style="margin-top: 1em;">
        <table id="cidGrid" style="width:900px;height:auto" title="返工方案">
		    <thead>
			    <tr>
                    <th field="FanAn" resizable="false" width="500">返工方案</th>
			    </tr>
		    </thead>
	    </table>
    </div>
    <div id="editCidDialog" title="编辑返工方案">
        <form >
            <johnson:ProductReworkCidDetails runat="server" ID="editProductReworkCidDetails"></johnson:ProductReworkCidDetails>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>
    <br />
    <form id="cidForm">
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    工时类型<span style="color: Red" >*</span>
                </td>
                <td style="width: 280px" class="textCol">
                    <select name="GSLX" class="textInput txtwidth required">
                        <option value="Coil">Coil</option>
                        <option value="钣金">钣金</option>
                        <option value="装配线">装配线</option>
                        <option value="其它">其它</option>
                    </select>
                </td>
                <td style="width: 200px" class="labelCol">
                    工时<span style="color: Red" >*</span>
                </td>
                <td style="width: 280px" class="textCol">
                    <input name="GS" type="text" class="textInput txtwidth required" />
                </td>
            </tr>
        </table>
    </form>
    <div style="margin-top: 1em;">
        <table id="cidFiles" style="width:900px;height:auto" title="工艺附件信息">
		    <thead>
			    <tr> 
				    <th field="FileName" resizable="false" width="200">附件名称</th>
                    <th field="FileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
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
    
    <div class="panel-header" style="margin-top: 2em;"><div class="panel-title">备注</div></div>
    <form id="remarkForm">
        <table class="formInfo">
            <tbody >
                <tr>
                    <td class="labelCol" style="width: 200px">
                        备注
                    </td>
                    <td class="textCol">
                        <textarea name="submitRemark" class="textInput" style="width: 688px;" rows="3"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>

    <div style="padding: 2em 0 0 30em;">
        <input type="button" id="btnSubmit" value="提交" />
        <input type="button" id="btnReturn" value="退回" />
    </div>
</body>
</html>
<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";
    var tempFolderId = "<%= this.ProcessFolderId %>";

    $(function () {
        $.get("ProductReworkController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm").setFormValue(data).setFormReadOnly();
            if(data.Materials){
                $('#engRequireGrid').datagrid('loadData', data.Materials);
            }
            if(data.EngFiles){
                $('#engAttachments').datagrid('loadData', data.EngFiles);
            }
            if(data.GYFA){
                $('#cidGrid').datagrid('loadData', data.GYFA);
            }
            if(data.CidFiles){
                $('#cidFiles').datagrid('loadData', data.CidFiles);
            }
            if(data.Files){
                $('#attachments').datagrid('loadData', data.Files);
            }
            $("#cidForm").setFormValue(data);
            $("#remarks").datagrid("loadData", data.Approves);
        });

        $("#btnSubmit").button().click(function () {
            if(!$("#cidForm").validAndFocus()){
                return;
            }
            var cidGridData = $('#cidGrid').datagrid('getData');
            if (cidGridData.rows.length == 0) {
                alert("请添加返工方案!");
                return;
            }
            var valueObj = $("#remarkForm, #cidForm").getFormValue();
            valueObj.GYFA = cidGridData.rows;
            var cidFiles = $('#cidFiles').datagrid('getData');
            valueObj.CidFiles = cidFiles.rows;
            var objJson = $.toJSON(valueObj);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("ProductReworkController.aspx?action=CidSubmit", { taskId: taskId, formJson: objJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });
        $("#btnReturn").button().click(function () {
            if (!confirm("您确实要退回吗？")) {
                return;
            }
            var valueObj = $("#remarkForm, #cidForm").getFormValue();
            var cidGridData = $('#cidGrid').datagrid('getData');
            valueObj.GYFA = cidGridData.rows;
            var cidFiles = $('#cidFiles').datagrid('getData');
            valueObj.CidFiles = cidFiles.rows;
            var objJson = $.toJSON(valueObj);
            $(this).attr("disabled", "disabled");
            $.post("ProductReworkController.aspx?action=CidReturn", { taskId: taskId, formJson: objJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("退回成功");
                    closeWindow();
                }
            });
        });
        $(".singleUserSelect").singleSelectUser();
        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#attachments, #remarks, #engRequireGrid, #engAttachments").datagrid();
        $("#addCidDialog form,#editCidDialog form, #cidForm").validate();
        var addCidDialog = $("#addCidDialog").dialog({ autoOpen: false, modal: true, width: 500 });
        var editCidDialog = $("#editCidDialog").dialog({ autoOpen: false, modal: true, width: 500 });
        $("#cidGrid").eidtableGrid(addCidDialog,editCidDialog);
        $("#cidFiles").attachmentsGrid1();
    })
</script>