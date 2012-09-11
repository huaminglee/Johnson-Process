<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductRework_Start.aspx.cs" Inherits="Johnson.Process.Website.ProductRework_Start" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ProductReworkDetails.ascx" TagName="ProductReworkDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>返工返修单-开始</title>
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
    
    <table class="header">
        <tr>
            <td rowspan="2" style="width: 143px;">
                <div style="float: left; margin-left: 10px;">
                    <img alt="" src="images/logo.gif" />
                </div>
            </td>
            <td style="padding-left:270px; width:350px; vertical-align: bottom; font-weight: bold; font-size:1.5em;">
                    返工返修单-开始
            </td>
            <td style="padding-left:100px;">
                <a href="FailureProduct_Start.aspx?taskId=<%= Request["taskId"] %>">不合格品处理</a>
            </td>
            <td >
            
            </td>
        </tr>
        <tr>
            <td style="text-align: left; font-weight: bold;">
                GZF ORDER FORM
            </td>
            <td >
            
            </td>
            <td >
            
            </td>
        </tr>
    </table>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>

    <form id="basicInfoForm">
        <johnson:ProductReworkDetails runat="server" ID="productReworkDetails"></johnson:ProductReworkDetails>
        
        <table class="formInfo">
            <tr>
                <td style="width: 200px" class="labelCol">
                    QC<span style="color: Red" >*</span>
                </td>
                <td class="textCol" style="width: 280px" >
                    <div class="singleUserSelect">
                        <input type="text" name="QCUserName" class="textInput required userAccount" readonly="readonly"/>
                        <input type="text" style="display: none" name="QCUserAccount" class="userName" value=""/>
                        <input type="button" value="选择" class="btnCommon" />
                    </div>
                </td>
            </tr>
        </table>
    </form>
    
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
            $("#basicInfoForm").setFormValue(data);
        });

        $("#btnSubmit").button().click(function () {
            if (!$("#basicInfoForm").validAndFocus()) {
                return;
            }
            var valueObj = $("#basicInfoForm,#remarkForm").getFormValue();
            var objJson = $.toJSON(valueObj);
            if (!confirm("您确实要提交吗？")) {
                return;
            }
            var files = $('#attachments').datagrid('getData');
            valueObj.Files = files.rows;
            $(this).attr("disabled", "disabled");
            $.post("ProductReworkController.aspx?action=start", { taskId: taskId, formJson: objJson, submitRemark: objJson.submitRemark }, function (data) {
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
        $("#basicInfoForm").validate();
        $("#attachments").attachmentsGrid();
    })
</script>