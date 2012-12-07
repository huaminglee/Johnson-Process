<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.OrderDetails" %>
<table class="formInfo">
    <tr >
        <td style="width: 200px" class="labelCol">
            发起人<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input name="startUserName" type="text" readonly="readonly" class="textInput txtwidth " />
        </td>
        <td style="width: 200px" class="labelCol">
            优先级<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <select name="level" class="textInput required">
                <option>常规</option>
                <option>紧急</option>
            </select>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            交货日期<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="jiaoHuoRiQi" type="text" class="textInput txtwidth required dateISO" />
        </td>
        <td style="width: 200px" class="labelCol">
            SO NO<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="SONO" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            JDS NO<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="JDSNO" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            图纸确认<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <select name="tuZiQueRen" class="textInput required" >
                <option>否</option>
                <option>是</option>
            </select>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            项目名称<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="xiangMingCheng" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            办事处<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="banShiChu" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            办事处联系人
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="banShiChuLianXiRen" type="text"  class="textInput txtwidth " />
        </td>
        <td style="width: 200px" class="labelCol">
            产品类型<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <select name="chanPinLeiXing" class="textInput txtwidth required" >
                <option></option>
                <option>VAV</option>
                <option>YAEP</option>
                <option>YAH</option>
                <option>YAR</option>
                <option>YBDB</option>
                <option>YBOC</option>
                <option>YBOH</option>
                <option>YBW</option>
                <option>YCAB</option>
                <option>YCAE</option>
                <option>YCAG</option>
                <option>YCWE</option>
                <option>YDCC</option>
                <option>YDCK</option>
                <option>YDCP</option>
                <option>YDFC</option>
                <option>YDOC</option>
                <option>YDOH</option>
                <option>YEAS</option>
                <option>YGAS</option>
                <option>YGCC</option>
                <option>YGFC</option>
                <option>YGOC</option>
                <option>YGOH</option>
                <option>YHAC</option>
                <option>YLAA</option>
                <option>YLPA</option>
                <option>YMAC</option>
                <option>YMOC</option>
                <option>YMOH</option>
                <option>YSAC</option>
                <option>YSE</option>
                <option>YSM</option>
                <option>YSM-B</option>
                <option>YSOC</option>
                <option>YSOH</option>
                <option>YVAA</option>
                <option>YVOH</option>
                <option>YWHA</option>
                <option value="OTHER">其它</option>
            </select>
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            ITEM<span style="color: Red" >*</span>
        </td>
        <td style="width: 280px" class="textCol">
            <input  name="sapItem" type="text" class="textInput txtwidth required" />
        </td>
        <td style="width: 200px" class="labelCol">
            MATERIAL<span style="color: Red" >*</span>
        </td>
        <td class="textCol">
            <input name="sapMaterial" type="text" class="textInput txtwidth required" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            数量<span style="color: Red" >*</span>
        </td>
        <td class="textCol">
            <input name="shuLiang" type="text" class="textInput txtwidth digits required" />
        </td>
        <td style="width: 200px" class="labelCol">
            技术要求
        </td>
        <td class="textCol">
            <input name="jiShuYaoQiu" type="text" class="textInput txtwidth" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px" class="labelCol">
            备注
        </td>
        <td class="textCol">
            <input name="beiZhu" type="text" class="textInput txtwidth" />
        </td>
        <td style="width: 200px" class="labelCol">
            其它要求说明
        </td>
        <td class="textCol">
            <input name="qiTaYaoQiuShuoMing" type="text" class="textInput txtwidth" />
        </td>
    </tr>
</table>
