<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultationAndQuotationDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.ConsultationAndQuotationDetails" %>
<table class="formInfo">
    <tr>
        <td style="width: 200px" class="labelCol">
            To市场部<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <select name="marketingEngineer" class="textInput txtwidth required" >
                <option value="">请选择</option>
                <asp:Repeater runat="server" ID="marketingEngineerRepeater">
                    <ItemTemplate>
                        <option value="<%# Eval("UserLoginName") %>"><%# Eval("UserRealName")%></option>
                    </ItemTemplate>
                </asp:Repeater>
            </select>
        </td>
        <td style="width: 200px" class="labelCol">
            申请人<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="applyUserName" type="text" readonly="readonly" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            办事处<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="applyUserDepartmentName" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            申请日期
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="applyTime" type="text" class="textInput txtwidth" readonly="readonly"/>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            项目名称<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="projectName" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            成功机会(%)<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="succeedProbability" type="text" class="textInput txtwidth required number" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            预计签合同日期<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="expectSignContact" type="text" class="textInput txtwidth required dateISO" />
        </td>
        <td >
                
        </td>
        <td >
                
        </td>
    </tr>
</table>
