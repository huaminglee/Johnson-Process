<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotation_Completed.aspx.cs" Inherits="Johnson.Process.Website.ConsultationAndQuotation_Completed" %>
<%@ Register Src="UserControls/Header.ascx" TagName="Header" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationDetails.ascx" TagName="ConsultationAndQuotationDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationProductDetails.ascx" TagName="ConsultationAndQuotationProductDetails" TagPrefix="johnson" %>
<%@ Register Src="UserControls/ConsultationAndQuotationProductCsdSalesTPDetails.ascx" TagName="ConsultationAndQuotationProductSalesTPDetails" TagPrefix="johnson" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>技术咨询及报价</title>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/icon.css" />
	<script type="text/javascript" src="jquery-easyui/jquery.easyui.min.js"></script>
    <script src="js/jquery.tmpl.min.js" type="text/javascript"></script>
    <script src="js/jshashtable.js" type="text/javascript"></script>
    <script src="js/jquery.numberformatter-1.2.3.min.js" type="text/javascript"></script>
    <script src="js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="jqueryui/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="js/processCommon.js" type="text/javascript"></script>
    <script src="js/ConsultationAndQuotation.js" type="text/javascript"></script>
    <script src="js/jqueryExtend.js" type="text/javascript"></script>
    <script src="js/jquery.json-2.3.js" type="text/javascript"></script>
    <link href="jqueryui/cupertino/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .datagrid-cell { font-size: 1.2em; }
    </style>
</head>
<body>
    <%--head --%>
    <johnson:Header runat="server" HeaderTitle="技术咨询及报价" ID="header"></johnson:Header>
    <div class="panel-header" ><div class="panel-title">基本信息</div></div>
    <form id="basicInfoForm">
        <johnson:ConsultationAndQuotationDetails runat="server" ID="consultationAndQuotationDetails"></johnson:ConsultationAndQuotationDetails>
    </form>
    <div class="panel-header" style="margin-top: 1em;"><div class="panel-title">Sales Marketing答复</div></div>
    <form id="marketingForm">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 100px">
                    Sales Marketing答复<span style="color: Red">*</span>
                </td>
                <td class="textCol" >
                    <textarea name="marketingReply" class="textInput required" style="width: 700px;" rows="3"></textarea>
                </td>
            </tr>
        </table>
    </form>
    <form id="otherReplyForm">
        <div class="panel-header" style="margin-top: 1em;"><div class="panel-title">广州厂相关部门评审</div></div>
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 100px">
                    ENG
                </td>
                <td class="textCol">
                    <textarea name="engReply" class="textInput" readonly="readonly" style="width: 300px;" rows="3"></textarea>
                </td>
                <td class="labelCol" style="width: 100px">
                    SCM
                </td>
                <td class="textCol">
                    <textarea name="scmReply" class="textInput" readonly="readonly" style="width: 300px;" rows="3"></textarea>
                </td>
            </tr>
            <tr >
                <td class="labelCol" style="width: 100px">
                    LOG
                </td>
                <td class="textCol" >
                    <textarea name="logReply" class="textInput" readonly="readonly" style="width: 300px;" rows="3"></textarea>
                </td>
                <td class="labelCol" style="width: 100px">
                    QAD
                </td>
                <td class="textCol" colspan="3" >
                    <textarea name="qadReply" class="textInput" readonly="readonly" style="width: 300px;" rows="3"></textarea>
                </td>
            </tr>
            <tr >
                <td class="labelCol" style="width: 100px">
                    CID
                </td>
                <td class="textCol" colspan="3" >
                    <textarea name="cidReply" class="textInput" readonly="readonly" style="width: 300px;" rows="3"></textarea>
                </td>
            </tr>
        </table>
    </form>
    <form id="tracerForm">
        <table class="formInfo">
            <tr >
                <td class="labelCol" style="width: 100px">
                    CSD答复<span style="color: Red">*</span>
                </td>
                <td class="textCol" colspan="3" >
                    <textarea name="csdReply" class="textInput required" style="width: 700px;" rows="3"></textarea>
                </td>
            </tr>
        </table>
    </form>

    <div id="pnlProductsSalesTP">
        <div style="margin-top: 1em;">
            <table id="gridProductSalesTp" style="width:auto;height:auto" title="(第一部分) 非标报价">
		        <thead>
			        <tr>
				        <th field="productModel" resizable="false" width="150">产品型号</th>
                        <th field="quantity" resizable="false" width="50">数量</th>
                        <th field="remark" resizable="false" width="100">SQ描述</th>
                        <th field="csdWithoutSalesTP" resizable="false" width="155" formatter="priceFormatter">FICE TP/(Without VAT)</th>
                        <th field="csdWithSalesTP" resizable="false" width="140" formatter="priceFormatter">FICE TP/(With VAT)</th>
                        <th field="csdTotalSalesTP" resizable="false" width="100" formatter="priceFormatter">FICE TP/总和</th>
                        <th field="marketingWithoutSalesTP" resizable="false" width="155" formatter="priceFormatter">Sales TP/(Without VAT)</th>
                        <th field="marketingWithSalesTP" resizable="false" width="140" formatter="priceFormatter">Sales TP/(With VAT)</th>
                        <th field="marketingTotalSalesTP" resizable="false" width="120" formatter="priceFormatter">Sales TP/总和</th>
			        </tr>
		        </thead>
	        </table>
        </div>
        <div class="panel-header" ><div class="panel-title">(第二部分) 非标货期</div></div>
        <form id="productLeadTimeForm">
            <table class="formInfo">
                <tr >
                    <td class="labelCol" style="width: 200px" >
                        在对应机组Lead Time基础上增加<span style="color: Red">*</span>
                    </td>
                    <td >
                        <input name="leadTime" class="textInput required digits" style="width: 200px;"/>天
                    </td>
                </tr>
                <tr >
                    <td class="labelCol" style="width: 200px">
                        备注
                    </td>
                    <td >
                        <textarea name="leadTimeRemark" class="textInput" style="width: 500px;" rows="3"></textarea>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    
    
    <div style="margin-top: 1em;">
        <table id="attachments" style="width:900px;height:auto" title="附件信息">
		    <thead>
			    <tr> 
				    <th field="fileName" resizable="false" width="200">附件名称</th>
                    <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
			    </tr>
		    </thead>
	    </table>
    </div>
    
    <div style="margin-top: 1em;">
        <table id="remarks" style="width:900px;height:auto" title="备注信息">
		    <thead>
			    <tr> 
				    <th field="remarkStepName" resizable="false" width="200">流程步骤</th>
                    <th field="remarkUserName" resizable="false" width="100">备注人</th>
                    <th field="remarkTime" resizable="false" width="130">备注日期</th>
                    <th field="remark" resizable="false" width="300">备注</th>
			    </tr>
		    </thead>
	    </table>
    </div>

</body>
</html>
<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "<%= Johnson.Process.Website.WebHelper.EDoc2BaseUrl %>";
    var taskId = "<%= this.TaskId %>";
    $(function () {
        $.get("ConsultationAndQuotationController.aspx?action=get", { taskId: taskId, r: Math.random() }, function (data) {
            $("#basicInfoForm, #marketingForm, #otherReplyForm, #tracerForm, #productLeadTimeForm").setFormValue(data);
            var products = { "total": data.products.length, "rows": data.products, "footer": [{ "productModel": "合计", "quantity": 0, "marketingTotalSalesTP": 0}] };
            $('#gridProductSalesTp').datagrid('loadData', products);
            $("#gridProductSalesTp").sumProductQuantityAndTP();
            if (data.files) {
                $('#attachments').datagrid('loadData', data.files);
            }
            if (data.remarks) {
                $('#remarks').datagrid('loadData', data.remarks);
            }

            $("#basicInfoForm").setFormReadOnly(true);
        });

        $("#gridProductSalesTp, #remarks, #attachments").datagrid({
            rownumbers: true,
            showFooter: true,
            singleSelect: true,
            nowrap: false
        });
    })
</script>