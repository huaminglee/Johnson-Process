<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotationProductCsdFiceTPDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.ConsultationAndQuotationProductCsdFiceTPDetails" %>
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
    <tr >
        <td style="width: 150px" class="labelCol">
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
            <input name="csdWithSalesTP" type="text" autocomplete="off" class="textInput txtwidth required number" />
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