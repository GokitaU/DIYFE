$(document).ready(function () {

    //****************************** START CORE APPLIACTION ERROR LOGGING *********************************//
    
    //ERROR LOGGING - this must be first so i can respond to subscribed events
    var errorLogging = {};
    mediator.installTo(errorLogging);
    errorLogging.subscribe('masterPageError', function (arg) {
       // alert('In responce to the masterPageError event.  This message is genrated by the errorLogging object in core.js.  This object is used to make ajax calls to record javascript and other page errors. Here are the extra parms passed -- -- ' + arg.error + ' ' + arg.method);
        //console.log('json post to record error ' + arg.error + ' ' + arg.method);
        //this.name = arg;
        //console.log(this.name + ' obj bttom');
        //alert('obj eveent sub change ' + this.name);
    });
    errorLogging.subscribe('pageError', function (arg) {
     //   alert('In responce to the pageError event.  This message is genrated by the errorLogging object in core.js.  This object is used to make ajax calls to record javascript and other page errors. Here are the extra parms passed -- -- ' + arg.error + ' ' + arg.method);
        //this.name = arg;
        //console.log(this.name + ' obj bttom');
        //alert('obj eveent sub change ' + this.name);
    });
    errorLogging.subscribe('logError', function (arg) {
      //  alert('In responce to the logError event.  Use this to only log error but not display it to user. -- -- ' + arg.error + ' ' + arg.method);
        //this.name = arg;
        //console.log(this.name + ' obj bttom');
        //alert('obj eveent sub change ' + this.name);
    });


    //****************************** START CORE APPLIACTION SCRIPTS *********************************//

    try {
        //topNavSelectedId is set in _Layout using the viewbag set in the controler.  PageBaseModel.ActiveTopNavLink
        $(topNavSelectedId).addClass('active');
    } catch (err) {
       
        mediator.publish('logError', { error: err.message, method: 'core.js, on document ready' });
    }

    //Global Function to 'stringigy' the object becuase jqueries .ajax post doesn't honor it's content type setting
    //and doesn't send json formated data.
    function sendJsonFormat() {
        return JSON.stringify(this);
    }

    //easy load function for models using form data.  
    //For this to work the ID of the inputs must match this  property names in the model.
    function easyLoad(obj, formData) {
        for (var i in obj) {
            //the  property has not been set so find it in form data
            if (obj[i] == undefined) {
                //MAYBE CHANGE THIS TO A CASE STATMENT FOR THE TYPE VALUE
                if (formData.find('#' + i).attr('type') == 'checkbox') {
                    obj[i] = formData.find('#' + i).is(':checked').toString();
                } else {
                    obj[i] = formData.find('#' + i).val();
                }
            }

            //object has another base object so run it's easyload function
            //this could be change to just do some recursion with it's self
            //...but this way mabye better if an object want custom code to be in it's easylaod
            if (obj[i] == 'object') {
                obj[i].EasyLoad(obj[i], formData);
            }
        }
    }

    //USAGE:
    //var savedSearchId = GetQueryString('queryStringId');
    //RETURNS:String - Get query string from URL
    function GetQueryString(key, default_) {
        if (default_ == null) default_ = "";
        key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
        var qs = regex.exec(window.location.href);
        if (qs == null)
            return default_;
        else

            return decodeURIComponent(qs[1].replace(/\+/g, " "));

    };



});