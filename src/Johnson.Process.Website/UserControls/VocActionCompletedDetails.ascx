<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VocActionCompletedDetails.ascx.cs" Inherits="Johnson.Process.Website.UserControls.VocActionCompletedDetails" %>
<table class="formInfo">
    <tr>
        <td style="width: 100px" class="labelCol">
            行动描述<span style="color: Red" >*</span>
        </td>
        <td style="width: 350px" class="textCol">
            <textarea name="remark" readonly="readonly" class="textInput" style="width: 200px;" rows="3"></textarea>
        </td>
    </tr>
    <tr>
        <td style="width: 100px" class="labelCol">
            结果<span style="color: Red" >*</span>
        </td>
        <td style="width: 350px" class="textCol">
            <textarea name="result" class="textInput required" style="width: 200px;" rows="3" ></textarea>
        </td>
    </tr>
    <tr>
        <td style="width: 100px" class="labelCol">
            附件
        </td>
        <td style="width: 350px" class="textCol edoc2FileUploader">
            <input  name="resultFileName" readonly="readonly" type="text" class="textInput txtwidth edoc2FileName" />
            <input  name="resultFileId" style="display: none" type="text"  class="edoc2FileId"/>
            <%--<input  name="resultFileName" type="text" class="textInput txtwidth required edoc2FileName" />
            <input  name="resultFileId" type="text"  class="edoc2FileId"/>--%>
            <input type="button" value="上传" class="btnCommon edoc2FileUploadButton" />
            <input type="button" value="删除" class="btnCommon edoc2FileDeleteButton" />
        </td>
    </tr>
</table>
