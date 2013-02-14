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



});