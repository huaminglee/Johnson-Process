<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotationProductMarketing2SalesTPDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.ConsultationAndQuotationProductMarketing2SalesTPDetails" %>
<table class="formInfo">
    <tr >
        <td style="width: 200px" class="labelCol">
            <span style="color: Red" >*</span>产品型号
        </td>
        <td class="textCol">
            <input name="productModel" type="text" class="textInput txtwidth" readonly="readonly"/>
        </td>
    </tr>
    <tr>
        <td class="labelCol">
            <span style="color: Red" >*</span>数量
        </td>
        <td class="textCol">
            <input name="quantity" type="text" class="textInput txtwidth" readonly="readonly"/>
        </td>
    </tr>
    <tr>
        <td class="labelCol">
            SQ描述
        </td>
        <td class="textCol">
            <textarea name="remark" class="textInput" style="width: 200px;" rows="3" readonly="readonly"></textarea>
        </td>
    </tr>
</table>
<div class="panel-header" style="margin-top: 1em;"><div class="panel-title">客服部报价</div></div>
<table class="formInfo">
    <tr >
        <td style="width: 200px" class="labelCol">
            <span style="color: Red" >*</span>FICE TP/(Without VAT)
        </td>
        <td class="textCol">
            <input name="csdWithoutSalesTP" type="text" class="textInput txtwidth" readonly="readonly"/>
        </td>
    </tr>
    <tr>
        <td class="labelCol">
            <span style="color: Red" >*</span>FICE TP/(With VAT)
        </td>
        <td class="textCol">
            <input name="csdWithSalesTP" type="text" autocomplete="off" readonly="readonly" class="textInput txtwidth required number" />
        </td>
    </tr>
    <tr>
        <td class="labelCol">
            FICE TP/总和
        </td>
        <td class="textCol">
            <input name="csdTotalSalesTP" type="text" class="textInput txtwidth" readonly="readonly"/>
        </td>
    </tr>
</table>
<div class="panel-header" style="margin-top: 1em;"><div class="panel-title">市场部报价</div></div>
<table class="formInfo">
    <tr >
        <td style="width: 200px" class="labelCol">
            <span style="color: Red" >*</span>Sales TP/(Without VAT)
        </td>
        <td class="textCol">
            <input name="marketingWithoutSalesTP" type="text" class="textInput txtwidth" readonly="readonly"/>
        </td>
    </tr>
    <tr>
        <td class="labelCol">
            <span style="color: Red" >*</span>Sales TP/(With VAT)
        </td>
        <td class="textCol">
            <input name="marketingWithSalesTP" type="text" autocomplete="off" class="textInput txtwidth required number" />
        </td>
    </tr>
    <tr>
        <td class="labelCol">
            Sales TP/总和
        </td>
        <td class="textCol">
            <input name="marketingTotalSalesTP" type="text" class="textInput txtwidth" readonly="readonly"/>
        </td>
    </tr>
</table>