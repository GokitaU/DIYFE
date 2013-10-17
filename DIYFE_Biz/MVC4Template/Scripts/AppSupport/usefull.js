
//USAGE:
//var bv = navigator.uaMatch();
//alert(bv.browser + " -- " + bv.version);
navigator.uaMatch = function () {

    var ua = navigator.userAgent.toLowerCase();

    var match = /(chrome)[ \/]([\w.]+)/.exec(ua) ||
              /(webkit)[ \/]([\w.]+)/.exec(ua) ||
              /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(ua) ||
              /(msie) ([\w.]+)/.exec(ua) ||
              ua.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(ua) ||
              [];

    return {
        browser: match[1] || "",
        version: parseFloat(match[2]) || 0
    };
};

//USAGE:
//if (dataList[0][columnIndex].tryParseInt() > -1)
//RETURNS:Int- Returns Int value of string, -1 if unable to parse 
String.prototype.tryParseInt = function () {
    var retValue = -1;
    if (this != null) {
        if (this.length > 0) {
            if (!isNaN(this)) {
                retValue = parseInt(this);
            }
        }
    }
    return retValue;
};

//USAGE:
//var tblRow = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</tr>";
//Table.append(tblRow.format('value 0', 'value 1', 'value 2', 'value 3'));
//RETURNS:String- Formated string based on the templates
String.prototype.format = function () {
    var s = this,
        i = arguments.length;
    while (i--) {
        s = s.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
    }
    return s;
};

//USAGE:
//var dateTimeData = jsonData.removeDate();
//RETURNS:String- sometimes json data time data is sent wraped in extra text, this removes the text
String.prototype.removeDate = function () {
    var s = this,
        s = s.replace('/Date(', '');
    s = s.replace(')/', '');
    return s;
};

//USAGE:
//var formatedDateTime = dateTimeVal.formateDate()
//RETURNS:String- formated string from dateTime value
Date.prototype.formatDate = function () {
    var date = this;
    var format = "MM/dd/yyyy";

    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    var day = date.getDate();

    format = format.replace("MM", month.toString());
    format = format.replace("dd", day.toString());
    format = format.replace("yyyy", year.toString());

    return format;
};

//USAGE:
//var savedSearchId = getQuerystring('queryStringId');
//RETURNS:String - Get query string from URL
function GetQueryString(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
};


function GetRootURL() {
  return  location.protocol + '//' + location.host + '/';
}

