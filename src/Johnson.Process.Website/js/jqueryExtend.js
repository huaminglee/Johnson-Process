jQuery(function($){
	$.datepicker.regional['zh-CN'] = {
	    closeText: '关闭',
		prevText: '&#x3c;上月',
		nextText: '下月&#x3e;',
		currentText: '今天',
		monthNames: ['一月','二月','三月','四月','五月','六月',
		'七月','八月','九月','十月','十一月','十二月'],
		monthNamesShort: ['一','二','三','四','五','六',
		'七','八','九','十','十一','十二'],
		dayNames: ['星期日','星期一','星期二','星期三','星期四','星期五','星期六'],
		dayNamesShort: ['周日','周一','周二','周三','周四','周五','周六'],
		dayNamesMin: ['日','一','二','三','四','五','六'],
		dateFormat: 'yy-mm-dd', firstDay: 1,
		isRTL: false
		};
	$.datepicker.setDefaults($.datepicker.regional['zh-CN']);
});

jQuery.extend({
    errorCode: function(code){
        code = parseInt(code);
        switch(code){
            case 1: return "未知错误!";
            case 101: return "选中的人员已经存在!";
        }
    }
});

(function($){
    $.widget("ui.pager", {
            lnkFirst: null,
            lnkPrevious: null,
            lnkNext: null,
            lnkLast: null,
            txtIndex: null,
            lblCount: null,
            index: null,
            count: null,
            
            options: {
                count: 0,
                index: 1,
		        change: null
	        },
	        _create: function(){
	            var children = this.element.children();
	            this.lnkFirst = $(children[0]);
	            this.lnkPrevious = $(children[1]);
	            this.lnkNext = $(children[2]);
	            this.lnkLast = $(children[3]);
	            this.txtIndex = $(children[4]);
	            this.lblCount = $(children[5]).text(this.options.count);
	            var self = this;
	            self.index = this.options.index;
	            this.txtIndex.val(self.index);
	            self.count = this.options.count;
    	        $(children[0]).click(function(){
    	            self._change(1);
    	        });
    	        $(children[1]).click(function(){
    	            self._change(self.index-1);
    	        });
    	        $(children[2]).click(function(){
    	            self._change(self.index+1);
    	        });
    	        $(children[3]).click(function(){
    	            self._change(self.count);
    	        });
    	        $(children[4]).change(function(){
    	            self._change($(this).val());
    	        });
    	        $(children[4]).keydown(function(){
    	            event.keyCode == 13 && self._change($(this).val());
    	        });
    	        
	        },
	        _change: function(index){
	            if(index>0 && index <=this.count){
	                this.index = index;
	                this.txtIndex.val(index);
	                this._trigger("change", null, index);
	            }
	        }
        }
    );
})(jQuery);

$.widget(
    "ui.messager", {
        options: {
            title: "消息",
            content: "",
            type: "succeed"
        },
        _create: function(){
            var container = $("<div></div>");
            this.element.append(container);
            this.element = container;
            var self = this.element;
            self.hide();
            this.element.addClass("msg_box ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
			    .append("<span style='float:right;cursor:pointer;' class='ui-icon ui-icon-closethick'></span>")
                .append("<div style='cursor:move;' class='ui-widget-header ui-corner-all'>" + this.options.title + "</div>")
			    .append("<div><span class='' id='icon'></span><span id='content'>"+this.options.content+"</span></div>")
			    .draggable({handle:".ui-widget-header"})
			    .hide()
			    .find(".ui-icon-closethick")
			    .click(
			        function(){
			            self.hide();
			        }
			    );
        },
        showMessage: function(content, type, title){
            if(!type){
                type = "succeed";
            }
            switch(content){
                case "add":
                    content =  "添加成功！";
                    break;
                case "delete":
                    content =  "删除成功！";
                    break;
                case "update":
                    content =  "修改成功！";
                    break;
                case "noSelect":
                    content =  "请先选择！";
                    break;
                case "error":
                    content =  "操作失败，未知错误！";
                    break;
            }
            var self = this.element;
            var css ;
            switch(type){
                case "succeed":
                    css = "msg_succeed";
                    window.setTimeout(
                        function(){
                            self.hide();
                        },
                        1000
                    );
                    break;
                case "error":
                    css = "msg_error";
                    break;
                case "warning":
                    css = "msg_warning";
                    break;
            }
            
            this.element
            .find("#icon").removeClass().addClass(css).end()
            .find("#content").text(content).end()
            .css({ top: 0, left: 0 }).show();
            
			this.element.position({
			    my: "center top",
			    at: "center top",
				of: window,
				offset: "0 0",
				collision: 'fit'
			});
        }
    }
);

$.widget(
    "ui.tree", {
        options: {
            iconUrl: null,
            iconClass: null,
            select: null
        },
        _create: function(){
            this.element.addClass("ui-tree ui-widget");
            var ul = this.element.children("ul")
            this._createNode(ul);
            var _this = this;
            this.element.find("a").live("click", function(){
                var thisEl = $(this);
                _this.selected && _this.selected.removeClass("selected");
                _this.selected = thisEl;
                thisEl.addClass("selected");
                var a = $(this);
                _this._trigger("select", null, {nodeId: a.attr("nodeId")});
                _this._currentNode = a.parent();
                return false;
            });
            ul.children("li").css("margin", "0");
            
        },
        _createNode: function(el){
            
            var _this = this;
            var children_li = el.children("li");
            children_li.prepend("<span class='tie middle'></span>");
            if(this.options.iconUrl){
                children_li.prepend("<span class='icon' style='background-image:url("+this.options.iconUrl+")'></span>");
            }
            else if(this.options.iconClass){
                children_li.prepend("<span class='icon "+this.options.iconClass+"'></span>");
            }
            children_li.eq(children_li.length-1).find(".middle").removeClass("middle").addClass("bottom");
            children_li.each(function(i){
                var $this = $(this).addClass("node");
                var ul = $this.children("ul");
                if(ul.size()){
                    ul.hide();
                    var expend = $("<span class='state expandable'>expend</span>");
                    expend.click(function(){
                        ul.toggle();
                        if(expend.hasClass("expandable")){
                            expend.removeClass("expandable");
                            expend.addClass("collapsable");
                        }
                        else{
                            expend.removeClass("collapsable");
                            expend.addClass("expandable");
                        }
                        if(!ul.attr("inited")){
                            _this._createNode(ul);
                            ul.attr("inited", 'true')
                        }
                    });
                    $this.prepend(expend);
                }
            });
        },
        select: function(i){
            this.element.children("ul").children("li").eq(i).children("a").click();
        },
        add: function(nodeName, nodeId){
            var _this = this;
            var node = $("<li class='node'><a href='#' nodeId='"+nodeId+"' >"+nodeName+"</a></li>");
            
            node.prepend("<span class='tie bottom'></span>");
            if(this.options.iconUrl){
                node.prepend("<span class='icon' style='background-image:url("+this.options.iconUrl+")'></span>");
            }
            else if(this.options.iconClass){
                node.prepend("<span class='icon "+this.options.iconClass+"'></span>");
            }
            node.prev().find(".bottom").removeClass("bottom").addClass("middle");
            
            if(!this._currentNode.children("ul").size()){
                var ul = $("<ul></ul>").hide();
                this._currentNode.append(ul);
                var expend = $("<span class='state expandable'>expend</span>");
                expend.click(function(){
                    ul.toggle();
                    if(expend.hasClass("expandable")){
                        expend.removeClass("expandable");
                        expend.addClass("collapsable");
                    }
                    else{
                        expend.removeClass("collapsable");
                        expend.addClass("expandable");
                    }
                    if(!ul.attr("inited")){
                        _this._createNode(ul);
                        ul.attr("inited", 'true')
                    }
                });
                this._currentNode.prepend(expend);
            }
            this._currentNode.children("ul").append(node);
            
        },
        currentNode: function(name){
            this._currentNode.find("a").text(name);
        }
    }
);
$.widget(
    "ui.ajaxProgress", {
        options: {
        },
        _create: function(){
            var _this = this;
            var self = this.element;
            self.hide();
            
            this.element.ajaxComplete(function(event,request, settings){
                $(this).hide();
            });
            
            this.element.ajaxSend(function(event,request, settings){
                _this.show();
            });
        },
        show: function(){
            this.element.css({ top: 0, left: 0 }).show();
            
			this.element.position({
			    my: "center center",
			    at: "center center",
				of: window,
				offset: "0 0",
				collision: 'fit'
			});
        }
    }
);

(function($) {
	$.widget("ui.combobox", {
		_create: function() {
			var self = this;
			var select = this.element.hide();
			var input = $("<input>")
				.insertAfter(select)
				.autocomplete({
					source: function(request, response) {
						var matcher = new RegExp(request.term, "i");
						response(select.children("option").map(function() {
							var text = $(this).text();
							if (this.value && (!request.term || matcher.test(text)))
								return {
									id: this.value,
									label: text.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + $.ui.autocomplete.escapeRegex(request.term) + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "<strong>$1</strong>"),
									value: text
								};
						}));
					},
					delay: 0,
					change: function(event, ui) {
						if (!ui.item) {
							// remove invalid value, as it didn't match anything
							$(this).val("");
							return false;
						}
						select.val(ui.item.id);
						self._trigger("selected", event, {
							item: select.find("[value='" + ui.item.id + "']")
						});
						
					},
					minLength: 0
				})
				.addClass("ui-widget ui-widget-content ui-corner-left ui-combobox-text");
			$("<button>&nbsp;</button>")
			.attr("tabIndex", -1)
			.attr("title", "Show All Items")
			.insertAfter(input)
			.button({
				icons: {
					primary: "ui-icon-triangle-1-s"
				},
				text: false
			}).removeClass("ui-corner-all")
			.addClass("ui-corner-right ui-button-icon ui-combobox-button")
			.click(function() {
				// close if already visible
				if (input.autocomplete("widget").is(":visible")) {
					input.autocomplete("close");
					return false;
				}
				// pass empty string as value to search for, displaying all results
				input.autocomplete("search", "");
				input.focus();
				return false;
			});
		}
	});
    

    $.widget(
        "ui.singleSelectUser", {
            options: {
            },
            _create: function(){
                var element = this.element;
                if($.debug){
                    element.find(".userName").removeAttr("readonly");
                    element.find(".userAccount").show();
                }
                else{
                    element.find(".userName").attr("readonly", "readonly");
                    element.find(".userAccount").hide();
                }
                element.find(":button").click(function(){
                    var url = edoc2BaseUrl + "/AppExt/Common/SelectOrgnization.aspx?userTree={show:true,multiSelect:" + false + ",current: true}&deptTree={show:false}";
                        var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
                        if (res != null && res.users != null) {
                            if(res.users.length > 0){
                                var user = res.users[res.users.length-1];
                                element.find(".userName").val(user._data.userRealName).focusout();
                                element.find(".userAccount").val(user._data.loginName);
                            }
                        }
                });
            }
        }
    );

    $.widget(
        "ui.multiSelectUser", {
            options: {
            },
            _create: function(){
                var element = this.element;
                var userAccount = element.find(".userAccount");
                var userName = element.find(".userName");
                if($.debug){
                    userAccount.show();
                    userName.removeAttr("readonly");
                }
                else{
                    userName.attr("readonly", "readonly");
                    userAccount.hide();
                }
                element.find(".selectButton").click(function(){
                    var url = edoc2BaseUrl + "/AppExt/Common/SelectOrgnization.aspx?userTree={show:true,multiSelect:true,current: true}&deptTree={show:false}";
                    var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
                    if (res != null && res.users != null) {
                        if(res.users.length > 0){
                            $.each(res.users, function(i, user){
                                if(userName.val()){
                                    userName.val(userName.val() + "," + user._data.userRealName).focusout();
                                }
                                else{
                                    userName.val(user._data.userRealName).focusout();
                                }
                                if(userAccount.val()){
                                    userAccount.val(userAccount.val() + "," + user._data.loginName);
                                }
                                else{
                                    userAccount.val(user._data.loginName);
                                }
                            })
                        }
                    }
                });
                element.find(".resetButton").click(function(){
                    userName.val("");
                    userAccount.val("");
                });
            }
        }
    );
    
    $.widget(
        "ui.userEmailMultiSelect", {
            options: {
            },
            _create: function(){
                var element = this.element;
                element.find(":button").click(function(){
                    var url = edoc2BaseUrl + "/AppExt/Common/SelectOrgnization.aspx?userTree={show:true,multiSelect:" + true + ",current: true}&deptTree={show:false}";
                        var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
                        var emailElement = element.find(".multiemail");
                        if (res != null && res.users != null) {
                            if(res.users.length > 0){
                                $.each(res.users, function(i, user){
                                    if(emailElement.val()){
                                        emailElement.val(emailElement.val() + ";" + user._data.email);
                                    }
                                    else{
                                        emailElement.val(user._data.email);
                                    }
                                })
                            }
                        }
                });
            }
        }
    );
})(jQuery);

(function($){$.fn.bgIframe=$.fn.bgiframe=function(s){if($.browser.msie&&/6.0/.test(navigator.userAgent)){s=$.extend({top:'auto',left:'auto',width:'auto',height:'auto',opacity:true,src:'javascript:false;'},s||{});var prop=function(n){return n&&n.constructor==Number?n+'px':n;},html='<iframe class="bgiframe"frameborder="0"tabindex="-1"src="'+s.src+'"'+'style="display:block;position:absolute;z-index:-1;'+(s.opacity!==false?'filter:Alpha(Opacity=\'0\');':'')+'top:'+(s.top=='auto'?'expression(((parseInt(this.parentNode.currentStyle.borderTopWidth)||0)*-1)+\'px\')':prop(s.top))+';'+'left:'+(s.left=='auto'?'expression(((parseInt(this.parentNode.currentStyle.borderLeftWidth)||0)*-1)+\'px\')':prop(s.left))+';'+'width:'+(s.width=='auto'?'expression(this.parentNode.offsetWidth+\'px\')':prop(s.width))+';'+'height:'+(s.height=='auto'?'expression(this.parentNode.offsetHeight+\'px\')':prop(s.height))+';'+'"/>';return this.each(function(){if($('> iframe.bgiframe',this).length==0)this.insertBefore(document.createElement(html),this.firstChild);});}return this;};})(jQuery);

$(function(){if($.browser.msie && $.browser.version == "6.0")document.execCommand("BackgroundImageCache", false, true);});


$.validator.messages.required = "必填";
$.validator.messages.number = "只能输入数字";
$.validator.messages.digits = "只能输入整数";
$.validator.messages.date = "日期格式不正确";
$.validator.messages.dateISO = "日期格式不正确";
$.validator.messages.email = "邮件格式不正确";

jQuery.extend({
    rselect: /^(?:select)/i,
    rtextarea: /^(?:textarea)/i,
    rinput: /^(?:color|date|datetime|email|hidden|month|number|password|range|search|tel|text|time|url|week)$/i,
    rradio: /^(?:radio)$/i,
    getFormValue: function (form) {
        return $(form).getFormValue();
    }
});

jQuery.fn.extend({
    getFormValue: function () {
        var valueArray = this.map(function () {
            return this.elements ? jQuery.makeArray(this.elements) : this;
        })
		.filter(function () {
		    return this.name &&
				(this.checked || $.rselect.test(this.nodeName) || $.rtextarea.test(this.nodeName) ||
					$.rinput.test(this.type));
		})
		.map(function (i, elem) {
		    var val = jQuery(this).val();

		    return val == null ?
				null :
				jQuery.isArray(val) ?
					jQuery.map(val, function (val, i) {
					    return { name: elem.name, value: val.replace(/\r?\n/g, "\r\n") };
					}) :
					{ name: elem.name, value: val.replace(/\r?\n/g, "\r\n") };
		}).get();

        var valueObj = {};
        $.each(valueArray, function (i, value) {
            valueObj[value.name] = value.value;
        });
        return valueObj;
    },
    setFormValue: function (obj) {
        this.map(function () {
            return this.elements ? jQuery.makeArray(this.elements) : this;
        })
        .filter(function () {
            return this.name &&
				($.rradio.test(this.type) || $.rselect.test(this.nodeName) || $.rtextarea.test(this.nodeName) ||
					$.rinput.test(this.type));
        })
        .each(function () {
            if($.rradio.test(this.type)){
                if($(this).val() == obj[this.name]){
                    $(this).attr("checked", "checked");
                }
            }
            else{
                $(this).val(obj[this.name]);
            }
        });
        return this;
    },
    setFormReadOnly: function () {
        this.map(function () {
            return this.elements ? jQuery.makeArray(this.elements) : this;
        })
        .filter(function () {
            return this.name &&
				($.rradio.test(this.type) || $.rselect.test(this.nodeName) || $.rtextarea.test(this.nodeName) ||
					$.rinput.test(this.type));
        })
        .each(function () {
            if ($.rradio.test(this.type) || $.rselect.test(this.nodeName)) {
                $(this).attr("disabled", "disabled");
            }
            else {
                $(this).attr("readonly", "readonly");
            }
        });
        return this;
    },
    validAndFocus: function () {
        var validValue = this.valid();
        if (!validValue) {
            this.validate().focusInvalid();
        }
        return validValue;
    },
    dateRange: function(){
        this.each(function(){
            var inputs = $(this).find("input");
            inputs.eq(0).datepicker({ changeMonth: true, changeYear: true, onSelect: function(selectedDate){
                inputs.eq(1).datepicker( "option", "minDate", selectedDate );
            }});
            inputs.eq(1).datepicker({ changeMonth: true, changeYear: true, onSelect: function(selectedDate){
                inputs.eq(0).datepicker( "option", "maxDate", selectedDate );
            }});
        }); 
    }
});

jQuery.validator.addMethod(
    "multiemail",
     function(value, element) {
         if (this.optional(element)) // return true on optional element 
             return true;
         var emails = value.split(/[;]+/); // split element by , and ;
         valid = true;
         for (var i in emails) {
             value = emails[i];
             valid = valid &&
                     jQuery.validator.methods.email.call(this, $.trim(value), element);
         }
         return valid;
     },

    "邮件格式不正确,多个邮件之间请用\";\"隔开"
);