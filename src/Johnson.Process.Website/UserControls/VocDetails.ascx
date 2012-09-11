<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VocDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.VocDetails" %>
<table class="formInfo">
    <tr>
        <td style="width: 200px" class="labelCol">
            发起人<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="applyUserName" type="text" readonly="readonly" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            办事处<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="applyUserDepartmentName" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            投诉日期<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="applyTime" type="text" class="textInput txtwidth required dateISO" />
        </td>
        <td style="width: 200px" class="labelCol">
            项目名称<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="projectName" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            机组型号<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="machineModel" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            机身编号<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="machineCode" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            故障数量<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="faultQuantity" type="text" class="textInput txtwidth required digits" />
        </td>
        <td style="width: 200px" class="labelCol">
            故障类别<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="faultCategory" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            现场临时行动<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="tempMeasure" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            临时行动完成时间<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="needCompleteDate" type="text" class="textInput txtwidth required dateISO" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            投诉编号<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol" colspan="3">
            <input name="vocCode" type="text" class="textInput txtwidth " />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            故障描述<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol" colspan="3">
            <textarea name="faultRemark" class="textInput required" style="width: 688px;" rows="3"></textarea>
        </td>
    </tr>
</table>
