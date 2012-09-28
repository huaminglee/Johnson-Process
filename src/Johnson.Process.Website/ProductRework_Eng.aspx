<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductRework_Eng.aspx.cs" Inherits="Johnson.Process.Website.ProductRework_Eng" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ProductReworkDetails.ascx" TagName="ProductReworkDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ProductReworkMaterialsDetails.ascx" TagName="ProductReworkMaterials" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>返工返修单-ENG</title>
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
    <johnson:Header runat="server" HeaderTitle="返工返修单-ENG" ID="header"></johnson:Header>
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
    </form>
    
    <div id="addMaterialsDialog" title="添加物料信息">
        <form>
            <johnson:ProductReworkMaterials runat="server" ID="addProductReworkMaterials"></johnson:ProductReworkMaterials>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定并继续"/>
                <input type="button" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>
    <div id="editMaterialsDialog" title="编辑物料信息">
        <form >
            <johnson:ProductReworkMaterials runat="server" ID="editProductReworkMaterials"></johnson:ProductReworkMaterials>
            <div style="margin-top:1em" class="buttons">
                <input type="submit" value="确定"/>
                <input type="button" value="关闭"/>
            </div>
        </form>
    </div>
    <form id="engForm">
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
            <table id="engAttachments" style="width:900px;height:auto" title="附件信息">
		        <thead>
			        <tr> 
				        <th field="FileName" resizable="false" width="200">附件名称</th>
                        <th field="FileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			        </tr>
		        </thead>
	        </table>
        </div>
    </form>

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
            if(data.Files){
                $('#attachments').datagrid('loadData', data.Files);
            }
            $("#remarks").datagrid("loadData", data.Approves);
        });

        $("#btnSubmit").button().click(function () {
            var engRequireData = $('#engRequireGrid').datagrid('getData');

            var valueObj = $("#remarkForm").getFormValue();
            valueObj.Materials = engRequireData.rows;
            var engFiles = $('#engAttachments').datagrid('getData');
            valueObj.EngFiles = engFiles.rows;
            var objJson = $.toJSON(valueObj);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            $(this).attr("disabled", "disabled");
            $.post("ProductReworkController.aspx?action=EngSubmit", { taskId: taskId, formJson: objJson }, function (data) {
                if (data.result != 0) {
                    alert(data.message);
                }
                else {
                    alert("提交成功");
                    closeWindow();
                }
            });
        });
        $(".singleUserSelect").singleSelectUser();
        $(".dateISO").datepicker({ changeMonth: true, changeYear: true });
        $("#attachments, #remarks").datagrid();
        $("#addMaterialsDialog form,#editMaterialsDialog form").validate();
        var addMaterialsDialog = $("#addMaterialsDialog").dialog({ autoOpen: false, modal: true, width: 500 });
        var editMaterialsDialog = $("#editMaterialsDialog").dialog({ autoOpen: false, modal: true, width: 500 });
        $("#engRequireGrid").eidtableGrid(addMaterialsDialog,editMaterialsDialog);
        $("#engAttachments").attachmentsGrid();
    })
</script>