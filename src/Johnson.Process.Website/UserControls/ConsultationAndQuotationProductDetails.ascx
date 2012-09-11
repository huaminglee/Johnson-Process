<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotationProductDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.ConsultationAndQuotationProductDetails" %>
<table class="formInfo">
    <tr >
        <td style="width: 150px" class="labelCol">
            <span style="color: Red" >*</span>产品型号
        </td>
        <td class="textCol">
            <input name="productModel" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td class="labelCol">
            <span style="color: Red" >*</span>数量
        </td>
        <td class="textCol">
            <input name="quantity" type="text" class="textInput txtwidth required digits" />
        </td>
    </tr>
    <tr>
        <td class="labelCol">
            SQ描述
        </td>
        <td class="textCol">
            <textarea name="remark" class="textInput" style="width: 200px;" rows="3"></textarea>
        </td>
    </tr>
</table>