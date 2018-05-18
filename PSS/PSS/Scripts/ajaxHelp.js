//ConversionObject要转换表单对象
function serializeJson(ConversionObject) {
    //定义一个json对象
    var json = {};
    //表单对象不能为空和未定义
    if (ConversionObject == undefined || ConversionObject == null) {
        return json;
    }
    //为json对象赋值
    $.each(ConversionObject.serializeArray(), function (i, obj) {
        //判断对象是否存在
        if (obj.name in json) {
            json[obj.name] = json[obj.name] + "," + obj.value;
        } else {
            json[obj.name] = obj.value;
        }
    });
    //返回转换好的json对象
    return json;
}
//setForm绑定表单form要绑定的表单json绑定表单的json对象
function setForm(form, json) {
    //遍历json对象
    $.each(json, function (name, value) {
        //根据name获取标签
        var input = $(form).find("[name=" + name + "]");
        //判断标签类型
        if (input.attr("type") == "checkbox") {
            //判断是否有值
            if (value != null && typeof (value) != "boolean") {
                //根据逗号分隔值
                var checkArray = value.split(",");
                //把值绑定到标签上
                for (var i = 0; i < checkArray.length; i++) {
                    for (var j = 0; j < input.length; j++) {
                        if (checkArray[i] == input[j].value) {
                            input[i].click();
                        }
                    }
                }
            }
        } else if (input.attr("type") == "radio") {
            $.each(input, function (i, obj) {
                if (obj.value == value) {
                    obj.click();
                }
            });
        } else if (input.attr("type") == "textarea") {
            input.html(value);
        } else {
            input.val(value);
        }
    });
}
//setTable绑定表格
function setTable(table, items, json) {
    //获取表格对象
    var tbl = $(table);
    tbl.find("tr:gt(0)").remove();
    //循环json对象
    $.each(json, function (i, obj) {
        //创建行对象
        var tr = $("<tr/>");
        //循环项数组
        $.each(items, function (i, item) {
            //web服务返回时间格式正则表达式
            var objRegExp = /^\/Date\(\d*\)\/$/;
            //判断是否是web时间格式
            if (objRegExp.test(obj[item])) {
                tr.append($("<td/>").text(dateFormat(obj[item],"yyyy-MM-dd hh:mm:ss")));
            }
            else {
                tr.append($("<td/>").text(obj[item] == null ? "" : obj[item]));
            }
        });
        tbl.append(tr);
    });
}
//web服务时间转换
function dateFormat(str, fmt) {
    if (str == undefined || str == null)
        return "";
    var time = str.substr(6);
    var date = new Date(parseFloat(time));

    if (fmt == undefined || fmt == null)
        return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDay();
    else
        return fmt.replace("yyyy", date.getFullYear())
            .replace("MM", date.getMonth() + 1)
            .replace("dd", date.getDate())
            .replace("hh", date.getHours())
            .replace("mm", date.getMinutes())
            .replace("ss", date.getSeconds())
}
//设置为jQuery扩展方法
$.extend({
    serializeJson: function (ConversionObject) {
        return serializeJson(ConversionObject);
    },
    setForm: function (form, json) {
        setForm(form, json);
    },
    setTable: function (table, items, json) {
        setTable(table, items, json);
    },
    dateFormat: function (str, fmt) {
        return dateFormat(str, fmt);
    }
});