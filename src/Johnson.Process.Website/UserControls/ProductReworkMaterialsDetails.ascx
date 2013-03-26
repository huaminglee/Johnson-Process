<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductReworkMaterialsDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.ProductReworkMaterialsDetails" %>
<table class="formInfo">
    <tr>
        <td style="width: 200px" class="labelCol">
            所需物料名称<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="Name" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            P.N<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="PN" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            数量<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="Quantity" type="text" class="textInput txtwidth required number" />
        </td>
    </tr>
</table>