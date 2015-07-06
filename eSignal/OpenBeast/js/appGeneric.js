var webMethod = "http://localhost:3748/AppStoreService.asmx/";
//var webMethod = "http://beasttest4/OpenF2WebService/Services/Service.asmx/";
//var webMethod = "http://hprajapati/AppStoreService/AppStoreService.asmx/";
//var webMethod = "http://beasttest4/appstoreservices/AppStoreService.asmx/";

var enumWebMethod =
{
    "Category": "GetAppStoreCategory",
    "SubCategory": "GetAppStoreCategory",
    "BeastApplication": "GetBeastApplication",
    "AppStoreApplication": "GetAppStoreApplication",
    "AssignApplicaitonIntoCategory": "NewApplicationInSubCategory",
    "CreateAppID": "NewAppStoreApplication",
    "SearchApplication": "SearchAppStoreApplication"
};

//$.jmsajaxurl = function (options) {
//    var url = options.url;
//    url += options.method;
//    if (options.data) {
//        var data = "";
//        for (var i in options.data) {
//            if (data != "")
//                data += "&"; data += i + "=" + JSON.stringify(options.data[i]);
//        }
//        url += "?" + data; data = null; options.data = "{}";
//    }
//    return url;
//};

var generateCDURL = function (options) {
    var url = options.url;
    url += options.method;
    if (options.data) {
        var data = "";
        for (var i in options.data) {
            if (data != "")
                data += "&";
            //data += i + "=" + JSON.stringify(options.data[i]);
            data += i + "=" + options.data[i];
        }
        url += "?" + data; data = null; options.data = "{}";
    }
    return url;
};

function RemoveSpecialCharacter(sReturnValue) {
    return sReturnValue.replace(/[^a-zA-Z0-9_]+/g, '');
}