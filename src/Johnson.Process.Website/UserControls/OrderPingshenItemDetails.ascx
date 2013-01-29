<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderPingshenItemDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.OrderPingshenItemDetails" %>
<table class="formInfo">
    <tr>
        <td style="width: 200px" class="labelCol">
            ITEM<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="item" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            MATERIAL<span style="color: Red" >*</span>
        </td>
        <td class="textCol">
            <input name="material" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            数量<span style="color: Red" >*</span>
        </td>
        <td class="textCol">
            <input name="shuliang" type="text" class="textInput txtwidth digits required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            技术要求
        </td>
        <td class="textCol">
            <input name="jishuYaoqiu" type="text" class="textInput txtwidth" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            备注
        </td>
        <td class="textCol">
            <input name="beizhu" type="text" class="textInput txtwidth" />
        </td>
    </tr>
</table>