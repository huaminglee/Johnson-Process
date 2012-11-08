<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliveryDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.DeliveryDetails" %>
<table class="formInfo">
    <tbody>
        <tr>
            <td style="width: 200px" class="labelCol">
                订单号
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtOrderNumber" name="orderNumber" type="text" class="textInput txtwidth" />
            </td>
            <td style="width: 200px" class="labelCol">
                项目名称<span style="color: Red" >*</span>
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtProjectName" name="projectName" type="text" class="textInput txtwidth required" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                销售办事处<span style="color: Red" >*</span>
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtSaleOffice" name="saleOffice" type="text" class="textInput txtwidth required" />
            </td>
            <td style="width: 200px" class="labelCol">
                销售组
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtSaleGroup" name="saleGroup" type="text" class="textInput txtwidth" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                销售工程师<span style="color: Red" >*</span>
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtSaleEngineerYT" name="saleEngineerYT" type="text" class="textInput txtwidth required" />
            </td>
            <td style="width: 200px" class="labelCol">
                预计下单日期<span style="color: Red" >*</span>
            </td>
            <td style="width: 280px" class="textCol">
                <input id="txtBookDate" name="bookDate" type="text" class="textInput required dateISO" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" class="labelCol">
                要求出厂日期<span style="color: Red" >*</span>
            </td>
            <td style="width: 280px" class="textCol" colspan="3">
                <input id="txtRequestOutDate" name="requestOutDate" type="text" class="textInput required dateISO" />
            </td>
        </tr>
        <tr id="td_CSDApp" runat="server">
            <td class="labelCol">
                客服部工程师<span style="color: Red" >*</span>
            </td>
            <td class="textCol" colspan="3">
                <div class="singleUserSelect">
                    <input type="text" name="csdEngineer" class="userAccount"/>
                    <input type="text" name="csdEngineerName" class="textInput userName required"/>
                    <input type="button" value="选择" class="btnCommon" />
                </div>
            </td>
        </tr>
    </tbody>
</table>
