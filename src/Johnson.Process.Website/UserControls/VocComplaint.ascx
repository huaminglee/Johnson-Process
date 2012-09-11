<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VocComplaint.ascx.cs" Inherits="Johnson.Process.Website.UserControls.VocComplaint" %>
<table class="formInfo">
    <tr>
        <td style="width: 150px" class="labelCol">
            机组型号<span style="color: Red" >*</span>
        </td>
        <td  class="textCol">
            <input  name="machineModel" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 150px" class="labelCol">
            机身编号<span style="color: Red" >*</span>
        </td>
        <td  class="textCol">
            <input  name="machineCode" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 150px" class="labelCol">
            故障数量<span style="color: Red" >*</span>
        </td>
        <td  class="textCol">
            <input  name="faultQuantity" type="text" class="textInput txtwidth required digits" />
        </td>
    </tr>
    <tr>
        <td style="width: 150px" class="labelCol">
            故障类别<span style="color: Red" >*</span>
        </td>
        <td  class="textCol">
            <input name="faultCategory" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 150px" class="labelCol">
            现场临时行动<span style="color: Red" >*</span>
        </td>
        <td  class="textCol">
            <input name="tempMeasure" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 150px" class="labelCol">
            临时行动完成时间<span style="color: Red" >*</span>
        </td>
        <td  class="textCol">
            <input name="needCompleteDate" type="text" class="textInput txtwidth required dateISO" />
        </td>
    </tr>
    <tr>
        <td style="width: 150px" class="labelCol">
            故障描述<span style="color: Red" >*</span>
        </td>
        <td  class="textCol" >
            <textarea name="faultRemark" class="textInput required" style="width: 200px;" rows="3"></textarea>
        </td>
    </tr>
    <tr>
        <td style="width: 150px" class="labelCol">
            投诉处理负责人<span style="color: Red" >*</span>
        </td>
        <td class="textCol" >
            <div class="singleUserSelect">
                <input type="text" name="responsibleUserName" class="textInput required userAccount" readonly="readonly"/>
                <input type="text" style="display: none" name="responsibleUserAccount" class="userName" value=""/>
                <%--<input type="text" name="responsibleUserName" class="textInput required userAccount" />
                <input type="text" name="responsibleUserAccount" class="userName" value=""/>--%>
                <input type="button" value="选择" class="btnCommon" />
            </div>
        </td>
    </tr>
</table>

<table name="attachments" style="width: 350px; height:auto" title="附件">
	<thead>
		<tr> 
			<th field="fileName" resizable="false" width="200">附件名称</th>
            <th field="fileId" resizable="false" formatter="fileActionFormater" width="100">操作</th>
		</tr>
	</thead>
</table>