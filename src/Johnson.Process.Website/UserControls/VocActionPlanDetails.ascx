<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VocActionPlanDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.VocActionPlanDetails" %>
<table class="formInfo">
    <tr>
        <td style="width: 100px" class="labelCol">
            行动描述<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <textarea name="remark" class="textInput required " style="width: 200px;" rows="3"></textarea>
        </td>
    </tr>
    <tr>
        <td style="width: 100px" class="labelCol">
            计划完成时间<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="endDate" type="text" class="textInput txtwidth required dateISO" />
        </td>
    </tr>
    <tr>
        <td style="width: 100px" class="labelCol">
            负责人<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <div class="singleUserSelect">
                <input type="text" name="userAccount" class="userAccount"/>
                <input type="text" name="userName" class="textInput userName required"/>
                <input type="button" value="选择" class="btnCommon" />
            </div>
        </td>
    </tr>
</table>
