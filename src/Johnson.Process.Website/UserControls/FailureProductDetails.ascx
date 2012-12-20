<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FailureProductDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.FailureProductDetails" %>
<table class="formInfo">
    <tr class="trNo">
        <td style="width: 200px" class="labelCol">
            不合格编号
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="No" type="text" readonly="readonly" class="textInput txtwidth " />
        </td>
        <td style="width: 200px" >
            
        </td>
        <td style="width: 280px" >
            
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            零件号
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="ComponentCode" type="text" readonly="readonly" class="textInput txtwidth " />
            <input name="ProductType" type="hidden" />
        </td>
        <td style="width: 200px" class="labelCol">
            零件名称
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="ComponentName" readonly="readonly" type="text" class="textInput txtwidth " />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            部件系列号
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="BJXLH" type="text" class="textInput txtwidth" />
        </td>
        <td style="width: 200px" class="labelCol">
            机组系列号
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="JZXLH" type="text" class="textInput txtwidth" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            供应商代码
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="GYSDM" type="text" readonly="readonly" class="textInput txtwidth " />
        </td>
        <td style="width: 200px" class="labelCol">
            供应商名称
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="GYSMC" type="text" readonly="readonly" class="textInput txtwidth " />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            MO
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="MO" type="text" readonly="readonly" class="textInput txtwidth " />
        </td>
        <td style="width: 200px" class="labelCol">
            UM
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="UM" type="text" readonly="readonly" class="textInput txtwidth " />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            订单号
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="OrderCode" type="text" class="textInput txtwidth" readonly="readonly" />
        </td>
        <td style="width: 200px" class="labelCol">
            发现地点
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="FailurePlace" type="text" readonly="readonly" class="textInput txtwidth " />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            责任部门
        </td>
        <td class="textCol">
            <input name="ZRBM" type="text" readonly="readonly" class="textInput txtwidth " />
        </td>
        <td style="width: 200px" class="labelCol">
            不合品数量<span style="color: Red" >*</span>
        </td>
        <td class="textCol">
            <input name="Quantity" type="text" class="textInput txtwidth digits required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            不合格现象描述
        </td>
        <td colspan="3" class="textCol">
            <textarea name="Remark" class="textInput " style="width: 688px;" rows="3"></textarea>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            不合品来源
        </td>
        <td colspan="3" class="textCol">
            <input name="Source" type="radio" value="制程不合格品" checked="checked"/><label>制程不合格品</label>
            <input name="Source" type="radio" value="来料不合格品" /><label>来料不合格品</label>
            <input name="Source" type="radio" value="成品仓不合格品" /><label>成品仓不合格品</label>
            <input name="Source" type="radio" value="客户退回不合格品" /><label>客户退回不合格品(费用已计入warranty,无需计算金额)</label>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            主要产生原因
        </td>
        <td colspan="3" class="textCol">
            <div>
                <input name="Reason" type="radio" value="设计问题" checked="checked"/><label>设计问题</label>
                <input name="Reason" type="radio" value="工艺问题" /><label>工艺问题</label>
                <input name="Reason" type="radio" value="合同更改" /><label>合同更改</label>
                <input name="Reason" type="radio" value="采购计划" /><label>采购计划</label>
                <input name="Reason" type="radio" value="生产计划" /><label>生产计划</label>
                <input name="Reason" type="radio" value="图纸版本" /><label>图纸版本</label>
            </div>
            
            <div>
                <input name="Reason" type="radio" value="来料质量" /><label>来料质量</label>
                <input name="Reason" type="radio" value="工装设备" /><label>工装设备</label>
                <input name="Reason" type="radio" value="操作失误" /><label>操作失误</label>
                <input name="Reason" type="radio" value="厂内搬运贮存" /><label>厂内搬运贮存</label>
                <input name="Reason" type="radio" value="场外搬运" /><label>场外搬运</label>
                <input name="Reason" type="radio" value="场外运输" /><label>场外运输</label>
                <input name="Reason" type="radio" value="其它" /><label>其它</label>
                <input name="ReasonRemark" type="text" class="textInput txtwidth " style="width:80px;"/>
            </div>
        </td>
    </tr>
</table>
