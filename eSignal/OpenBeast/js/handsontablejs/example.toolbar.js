$.example = {
    exampleDialogs: {}
}

$.example.toolbar = {};

$.example.toolbar.loadDialog = function (name, title, width, height) {
    if (!$.example.exampleDialogs[name]) {
        if (width) {
            $.example.exampleDialogs[name] = $('<div class="' + name + '"></div>')
	            .html('')
	            .dialog({
	                autoOpen: false,
	                title: title,
	                modal: true,
	                width: width,
	                height: height,
	                minWidth: width,
	                minHeight: height,
	                resizable: false,
	                close: function (event, ui) {
	                }
	            });

        }else {
            $.example.exampleDialogs[name] = $('<div style="z-index :1000;" class="' + name + '"></div>')
            .html('')
            .dialog({
                autoOpen: false,
                title: title,
                modal: true,
                close: function (event, ui) {
                }
            });
        }
        $.example.exampleDialogs[name].dialog('open');
        $('.' + name).dform($.example.toolbar.forms[name]);
    } else {
        $.example.exampleDialogs[name].dialog('open');
    }
};

//Data
$.example.toolbar.data = {
    'example': function () {
        $.example.toolbar.loadDialog('example', 'Format Cells', 315, 390);
    }
};

//Forms
$.example.toolbar.forms = {
    'example': {
        "action": "",
        "method": "get",
        "html": [
             {
                 'type': 'div',
                 'id': 'dialog_hot',
                 'class': 'tabbable-panel'
                 //'style': 'width: 260px; height: 360px; overflow: auto;'
             }
           
        ]
    }
}
