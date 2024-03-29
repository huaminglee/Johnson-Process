﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Johnson.Process.Website._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.json-2.3.js" type="text/javascript"></script>
    <link href="ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="ligerUI/js/ligerui.min.js" type="text/javascript"></script>
    <script src="js/EmployeeData.js" type="text/javascript"></script>
    <script src="js/DepartmentData.js" type="text/javascript"></script>
    <script type="text/javascript">

        var DepartmentList = DepartmentData.Rows;
        var sexData = [{ Sex: 1, text: '男' }, { Sex: 2, text: '女'}];
        $(f_initGrid);
        var manager, g;
        function f_initGrid()
        { 
           g =  manager = $("#maingrid").ligerGrid({
                columns: [
                { display: '主键', name: 'ID', width: 50, type: 'int',frozen:true },
                { display: '名字', name: 'RealName',
                    editor: { type: 'text' }
                },
                { display: '性别', width: 50, name: 'Sex',type:'int',
                    editor: { type: 'select', data: sexData, valueColumnName: 'Sex' },
                    render: function (item)
                    {
                        if (parseInt(item.Sex) == 1) return '男';
                        return '女';
                    }
                },
                { display: '年龄', name: 'Age', width: 50, type: 'int', editor: { type: 'int'} }, 
                { display: '部门', name: 'DepartmentID', width: 120, isSort: false,
                    editor: { type: 'select', data: DepartmentList, valueColumnName: 'DepartmentID', displayColumnName: 'DepartmentName' }, render: function (item)
                    {
                        for (var i = 0; i < DepartmentList.length; i++)
                        {
                            if (DepartmentList[i]['DepartmentID'] == item.DepartmentID)
                                return DepartmentList[i]['DepartmentName']
                        }
                        return item.DepartmentName;
                    }
                } 
                ],
                onSelectRow: function (rowdata, rowindex)
                {
                    $("#txtrowindex").val(rowindex);
                },
                enabledEdit: true, clickToEdit: false, isScroll: false,  
                data:EmployeeData,
                width: '100%'
            });   
        }
        function beginEdit() {
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            manager.beginEdit(row);
        }
        function cancelEdit() {
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            manager.cancelEdit(row);
        }
        function cancelAllEdit()
        {
            manager.cancelEdit();
        }
        function endEdit()
        {
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            manager.endEdit(row);
        }
        function endAllEdit()
        {
            manager.endEdit();
        }
        function deleteRow()
        { 
            manager.deleteSelectedRow();
        }
        var newrowid = 100;
        function addNewRow()
        {
            manager.addEditRow();
        } 
         
        function getSelected()
        { 
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            alert(JSON.stringify(row));
        }
        function getData()
        { 
            var data = manager.getData();
            alert(JSON.stringify(data));
        } 
    </script>
</head>
<body  style="padding:10px">
 <a class="l-button" style="width:80px;float:left; margin-left:6px;" onclick="beginEdit()">修改行</a>
  <a class="l-button" style="width:80px;float:left; margin-left:6px;" onclick="endEdit()">提交修改</a>
    <a class="l-button" style="width:80px;float:left; margin-left:6px;" onclick="endAllEdit()">提交全部</a>
 <a class="l-button" style="width:80px;float:left; margin-left:6px;" onclick="cancelEdit()">取消修改</a>
  <a class="l-button" style="width:80px;float:left; margin-left:6px;" onclick="cancelAllEdit()">取消全部</a>


<a class="l-button" style="width:120px;float:left; margin-left:6px;" onclick="deleteRow()">删除选择的行</a>

<a class="l-button" style="width:100px;float:left; margin-left:6px;" onclick="addNewRow()">添加行</a>


 

 <div class="l-clear"></div>
    <div id="maingrid" style="margin-top:20px"></div> <br />
       <br />
   <a class="l-button" style="width:120px" onclick="getSelected()">获取选中的值(选择行)</a>
 
   <br />
   <a class="l-button" style="width:120px" onclick="getData()">获取当前的值</a>
  <div style="display:none;">
  <!-- g data total ttt -->
</div>
</body>
</html>
