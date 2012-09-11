<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="Johnson.Process.Website.UserControls.Header" %>

<table class="header">
    <tr>
        <td rowspan="2" style="width: 143px;">
            <div style="float: left; margin-left: 10px;">
                <img alt="" src="images/logo.gif" />
            </div>
        </td>
        <td style="padding-left:270px; vertical-align: bottom; font-weight: bold; font-size:1.5em;">
                <%= this.HeaderTitle %>
        </td>
    </tr>
    <tr>
        <td style="text-align: left; font-weight: bold;">
            GZF ORDER FORM
        </td>
    </tr>
</table>