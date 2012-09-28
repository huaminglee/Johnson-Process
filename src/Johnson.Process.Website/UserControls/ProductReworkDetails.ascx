<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductReworkDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.ProductReworkDetails" %>
<table class="formInfo">
    <tr>
        <td style="width: 200px" class="labelCol">
            产品类型<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="ProductType" type="radio" value="0" checked="checked"/><label>零部件</label>
            <input name="ProductType" type="radio" value="1" /><label>产品</label>
        </td>
        <td style="width: 200px" class="labelCol">
            系列号<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="XLH" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            名称<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="Name" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            SAP号<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="SapNo" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            数量<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="Quantity" type="text" class="textInput txtwidth required digits " />
        </td>
        <td style="width: 200px" class="labelCol">
            订单号<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="OrderNumber" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            发出部门<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="StartDepartment" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            产品所在地<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="ProductArea" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            要求完成时间<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="CompletedTime" type="text" class="textInput txtwidth required dateISO" />
        </td>
        <td style="width: 200px" class="labelCol">
            
        </td>
        <td style="width: 280px" class="textCol">
            
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            返工品来源<span style="color: Red" >*</span>
        </td>
        <td colspan="3" class="textCol">
            <div>
                <input name="Source" type="radio" value="0" checked="checked"/><label>客户退货</label>
                <input name="Source" type="radio" value="1" /><label>合同更改</label>
                <input name="Source" type="radio" value="2" /><label>样机/小批机</label>
                <label>(样机项目编号)</label><input type="text" class="textInput" style="width:100px;" />
            </div>
            <div>
                <input name="Source" type="radio" value="3" /><label>在线不合格</label>
                <input name="Source" type="radio" value="4" /><label>库存不合格</label>
                <input name="Source" type="radio" value="5" /><label>来料检验不合格</label>
                <input name="Source" type="radio" value="6" /><label>其它</label>
                <input type="text" name="" class="textInput" style="width:80px;" />
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            费用承担(如无需计算费用,则费用承担亦为空白)<span style="color: Red" >*</span>
        </td>
        <td colspan="3" class="textCol">
            <div>
                <input name="FYCD" type="radio" value="0" checked="checked"/><label>客户</label>
                <input name="FYCD" type="radio" value="1" /><label>办事处</label>
                <input name="FYCD" type="radio" value="2" /><label>保险公司</label>
                <input name="FYCD" type="radio" value="3" /><label>供方</label>
                <input name="FYCD" type="radio" value="4" /><label>工厂内部</label>
                <input name="FYCD" type="radio" value="5" /><label>无费用承担</label>
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            承担者
        </td>
        <td class="textCol">
            <div>
                <input name="FYCDZ" type="text" class="textInput txtwidth" />
            </div>
        </td>
        <td style="width: 200px" class="labelCol">
            索赔单号
        </td>
        <td class="textCol">
            <div>
                <input name="SPDH" type="text" class="textInput txtwidth" />
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            确认人
        </td>
        <td class="textCol" colspan="3">
            <div>
                <input name="FYQRR" type="text" class="textInput txtwidth" />
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            原因描述<span style="color: Red" >*</span>
        </td>
        <td colspan="3" class="textCol">
            <textarea name="YYMS" class="textInput required" style="width: 688px;" rows="3"></textarea>
        </td>
    </tr>
</table>
<table id="attachments" style="width:900px;height:auto" title="附件信息">
	<thead>
		<tr> 
			<th field="fileName" resizable="false" width="200">附件名称</th>
            <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
		</tr>
	</thead>
</table>