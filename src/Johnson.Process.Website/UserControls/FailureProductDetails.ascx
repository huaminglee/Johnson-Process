<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FailureProductDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.FailureProductDetails" %>
<table class="formInfo">
    <tr>
        <td style="width: 200px" class="labelCol">
            零件号/系列号<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="ComponentCode" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            零件名<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="ComponentName" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            订单号<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="OrderCode" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            发现地点<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="FailurePlace" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            责任部门/供应商<span style="color: Red" >*</span>
        </td>
        <td colspan="3" class="textCol">
            <input name="Supplier" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            不合品来源<span style="color: Red" >*</span>
        </td>
        <td colspan="3" class="textCol">
            <input name="Source" type="radio" value="0" checked="checked"/><label>制程不合格品</label>
            <input name="Source" type="radio" value="1" /><label>来料不合格品</label>
            <input name="Source" type="radio" value="2" /><label>成品仓不合格品</label>
            <input name="Source" type="radio" value="3" /><label>客户退回不合格品(费用已计入warranty,无需计算金额)</label>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            不合品数量<span style="color: Red" >*</span>
        </td>
        <td colspan="3" class="textCol">
            <input name="Quantity" type="text" class="textInput txtwidth digits required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            主要产生原因<span style="color: Red" >*</span>
        </td>
        <td colspan="3" class="textCol">
            <div>
                <input name="Reason" type="radio" value="0" checked="checked"/><label>设计问题</label>
                <input name="Reason" type="radio" value="1" /><label>工艺问题</label>
                <input name="Reason" type="radio" value="2" /><label>合同更改</label>
                <input name="Reason" type="radio" value="3" /><label>采购计划</label>
                <input name="Reason" type="radio" value="4" /><label>生产计划</label>
                <input name="Reason" type="radio" value="5" /><label>图纸版本</label>
            </div>
            
            <div>
                <input name="Reason" type="radio" value="6" /><label>来料质量</label>
                <input name="Reason" type="radio" value="7" /><label>工装设备</label>
                <input name="Reason" type="radio" value="8" /><label>操作失误</label>
                <input name="Reason" type="radio" value="9" /><label>厂内搬运贮存</label>
                <input name="Reason" type="radio" value="10" /><label>场外搬运</label>
                <input name="Reason" type="radio" value="11" /><label>场外运输</label>
                <input name="Reason" type="radio" value="12" /><label>其它</label>
                <input name="ReasonRemark" type="text" class="textInput txtwidth " style="width:80px;"/>
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            不合格现象描述<span style="color: Red" >*</span>
        </td>
        <td colspan="3" class="textCol">
            <textarea name="Remark" class="textInput required" style="width: 688px;" rows="3"></textarea>
        </td>
    </tr>
</table>
