﻿
$(function () {

    //var timestamp3 = "/Date(15213464464)/".replace("/Date(", "").replace(")/", "");
    //alert(timestamp3);
    //var newDate = new Date();
    //newDate.setTime(timestamp3 * 1000);
    //alert(newDate.toDateString());

    $.extend($.fn.validatebox.defaults.rules, {
        date: {
            validator: function (value, param) {
                return RQcheck(value);
            },
            message: '请输入正确的日期'
        }
    });
     
    //验证日期
    function RQcheck(RQ) {
        var date = RQ;
        var result = date.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
       
        if (result == null)
            return false;
        var d = new Date(result[1], result[3] - 1, result[4]);
        return (d.getFullYear() == result[1] && (d.getMonth() + 1) == result[3] && d.getDate() == result[4]);

    }

    //全局对象
    global = {
        action:"",
        editable:undefined,
        search: function () {
            console.log($("#reg_from").datebox('getValue'))
            $("#table_users").datagrid("load",{
                "user_name": $(".textbox[name='user_name']").val(),
                "reg_from": $("#reg_from").datebox('getValue'),
                "reg_to": $("#reg_to").datebox('getValue'),
            });
        },
        add: function () {
            if (this.editable != undefined)
            {
                return;
            }
            //开启编辑状态
            $("#table_users").datagrid('insertRow', {
                index: 0,
                row: {
                    user_name: "",
                    reg_time :"",
                }
            })
            $("#table_users").datagrid('beginEdit', 0)
            //显示保存和取消按钮
            $("#button_save,#button_cancel").show();
            $("#table_users").datagrid('selectRow', 0);//选中第一行
            this.editable = 0;
            this.action = "add";
        },
        save: function () {
            //结束编辑
            $("#table_users").datagrid("endEdit", this.editable);
        },
        cancel: function () {
            $("#button_save,#button_cancel").hide();
            this.editable = undefined;
            //回滚所有编辑
            $("#table_users").datagrid("rejectChanges");
        },
        update: function () {
            //获取选中的行
            //console.log($("#table_users").datagrid("getSelected"))
            var rows = $("#table_users").datagrid("getRows");
            var selected_row = $("#table_users").datagrid("getRowIndex", $("#table_users").datagrid("getSelected"));
            if (selected_row > -1)
            {
                for (var i = 0; i < rows.length; i++) {
                    $("#table_users").datagrid("endEdit", $("#table_users").datagrid("getRowIndex", rows[i]))
                }
                $("#table_users").datagrid("beginEdit", selected_row);
                $("#button_save,#button_cancel").show();
                this.editable = selected_row;
                this.action = "update";
            }
            else
            {
                $.messager.alert("提示", "请选择一行!", "info");
            }
        },
        delete: function () {
            var list_checked = $("#table_users").datagrid("getChecked");
            if (list_checked.length > 0)
            {
                $.messager.confirm("确认操作", "您确定要删除所有的记录吗?", function (flag) {
                    if(flag)
                    {
                        var list_id = [];
                        for (var i = 0; i < list_checked.length; i++) {
                            list_id.push(list_checked[i].id);
                        }
                        $("#table_users").datagrid("loading")
                        $.post("ashx/UserHandler.ashx",
                            {
                                action: "delete",
                                id:list_id.join(',')
                            },
                            function (data)
                            {
                                var json = eval("(" + data + ")")
                                $("#table_users").datagrid("loaded")
                                if (json.state != "0")
                                {
                                    $("#table_users").datagrid("reload");
                                    $.messager.show({
                                        title: '提示',
                                        msg: '删除了' + json.msg + '条数据',
                                    })
                                }
                                else
                                {
                                    $.messager.show({
                                        title: '警告',
                                        msg: '后台程序发生错误，请联系管理员',
                                    })
                                }
                            });
                    }
                });
                
            }
            else
            {
                $.messager.alert("提示", "请选择要删除的记录", "info");
            }
        }
    }

    $("#table_users").datagrid({
        width: 600,
        url: 'ashx/UserHandler.ashx?action=query',
        title: "用户列表",
        iconCls: "icon-add",
        fitColumns: true,
        //fitColumns: false,//设置冻结列时，fitColumns必须设置为false
        //frozenColumns: [[
        //{
        //    field: 'id',
        //    title: '用户id',
        //    sortable: true,
        //    width: 50,
        //    checkbox: true,
        //},
        //{
        //    field: 'user_name',
        //    title: '用户名',
        //    sortable: true,
        //    width: 200,
        //    editor: {
        //        type: 'validatebox',
        //        options: {
        //            required: true,
        //        }
        //    }
        //}
        //]],
        columns: [[
            {
                field: 'id',
                title: '用户id',
                sortable: true,
                //width: 50,
                checkbox: true,
            },
            {
                field: 'user_name',
                title: '用户名',
                sortable: true,
                //width: 200,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: true,
                    }
                },
                formatter:function(value,rowData,rowIndex)
                {
                    return value + "(" + rowData.id + ")";
                }
            },
            {
                field: 'nick_name',
                title: '昵称',
                sortable: true,
                //width: 100,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: true,
                    }
                }
            },
            {
                field: 'reg_time',
                title: '注册时间',
                sortable: true,
                width: 100,
                editor: {
                    type: 'datebox',
                    options: {
                        required: true,
                        validType: ['date']
                    }
                }
            }
        ]],
        pagination: true,
        pageSize: 10,
        pageList:[10,20,30],
        pageNumber: 1,
        sortName: 'id',
        sortOrder: 'ASC',
        toolbar: "#tb",
        //singleSelect:true,
        onAfterEdit:function(rowIndex, rowData, changes)
        {
            if (global.action == "add")
            {
                //添加，并获取那一行的数据
                var inserted_row = $("#table_users").datagrid('getChanges', 'inserted');
                $.post("../ashx/UserHandler.ashx",
                    {
                        action: "add",
                        user_name: inserted_row[0].user_name,
                        reg_time: inserted_row[0].reg_time,
                    },
                    function (data) {
                        if (data == "1") {
                            $("#table_users").datagrid("reload")
                        }
                        else {
                            $("#table_users").datagrid('rejectChanges');
                        }
                    });
            }
            else if (global.action == "update")
            {
                var updated_row = $("#table_users").datagrid('getChanges', 'updated');
                $.post("../ashx/UserHandler.ashx",
                    {
                        action: "update",
                        id: updated_row[updated_row.length - 1].id,
                        user_name: updated_row[updated_row.length - 1].user_name,
                        reg_time: updated_row[updated_row.length - 1].reg_time,
                    },
                    function (data)
                    {
                        if (data == "1") {
                            $("#table_users").datagrid("reload")
                        }
                        else {
                            $("#table_users").datagrid('rejectChanges');
                        }
                    });
            }
            //编辑结束后
            $("#button_save,#button_cancel").hide();
            global.editable = undefined;
            global.action = "";
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();//阻止默认事件
            console.log(rowData)
        }
        //onDblClickRow: function (rowIndex, rowData)
        //{
            //双击行
            //global.update();
        //},
    })
})