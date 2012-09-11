<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaterialDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.MaterialDetails" %>
<table class="formInfo">
    <tbody >
        <tr >
            <td style="width: 150px" class="labelCol">
                SAP Materials No<span style="color: Red" >*</span>
            </td>
            <td class="textCol">
                <input name="sapNo" type="text" class="textInput txtwidth required" />
            </td>
        </tr>
        <tr>
            <td class="labelCol">
                数量<span style="color: Red" >*</span>
            </td>
            <td class="textCol">
                <input name="quantity" type="text" class="textInput txtwidth required digits" />
            </td>
        </tr>
        <tr>
            <td class="labelCol">
                备注
            </td>
            <td class="textCol">
                <textarea name="remark" class="textInput" style="width: 200px;" rows="3"></textarea>
            </td>
        </tr>
    </tbody>
</table>