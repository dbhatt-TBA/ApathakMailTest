
function GetCategory() {
    PageMethods.getCat(GetCategory_OnSuccess, GetCategory_OnError);
}

function GetCategory_OnSuccess(resultText) {
    var categoryAry = eval(resultText);

    $(categoryAry).each(function (i) {
        $('#ulCategoryList').append('<li  id=' + "liCategory" + i + ' onclick="Category_OnClick(' + "liCategory" + i + ',' + categoryAry[i].Id + ')"><a  href="javascript:void(0)" >' + categoryAry[i].Name + '</a></li>');
    });
}

function GetCategory_OnError() {
}

//dummy change

function Category_OnClick(lID, cID) {
    $('#tblImageList tbody').empty();

    $('#ulCategoryList li').each(function (i) {
        $('#ulCategoryList li').removeClass('active');
    });

    $(lID).addClass('active');

    GetAppStoreApplication(cID, 0);
}

//***************SHOW CREATED APPLICATION***************

function GetAppStoreApplication(CategoryId, UserID) {
    $('#tblApplicationListView tbody').empty();
    $('#tblApplicationGridView tbody').empty();
    $('#tblApplicationThumbView tbody').empty();

    PageMethods.getAppSotreApp(CategoryId, UserID, GetAppStoreApplication_OnSuccess, GetAppStoreApplication_OnError);
}

function GetAppStoreApplication_OnSuccess(resultText) {
    var appAry = eval(resultText);

    var iNoOfRows = appAry.length / 3;

    if (iNoOfRows.toString().indexOf(".") > 0) {
        iNoOfRows = Math.round(iNoOfRows) + 1;
    }

    for (var i = 0; i <= iNoOfRows - 1; i++) {
        $('#tblApplicationThumbView  > tbody:last').append(
                '<tr>' +
                '<td>&nbsp;</td>' +
                '<td>&nbsp;</td>' +
                '<td>&nbsp;</td>' +
                '</tr>');
    }

    $(appAry).each(function (i) {
        $('#tblApplicationThumbView tbody tr td:eq(' + i + ')').append('<a>' + appAry[i].BeastImageName + '</a><br />&nbsp;' + appAry[i].Description);
    });

    $(appAry).each(function (i) {
        $('#tblApplicationListView  > tbody:last').append(
        '<tr>' +
        '<td><a>' + appAry[i].BeastImageName + '</a></br>' + appAry[i].Description + '<input type ="hidden"  id="' + "hdfImageID" + i + '" value ="' + appAry[i].SifID + '"></input></td>' +
        '</tr>');
    });

    $(appAry).each(function (i) {
        $('#tblApplicationGridView  > tbody:last').append(
        '<tr>' +
        '<td><a>' + appAry[i].BeastImageName + '</a><input type ="hidden"  id="' + "hdfImageID" + i + '" value ="' + appAry[i].SifID + '"></input></td>' +
        '<td>' + appAry[i].Description + '</td>' +
        '</tr>');
    });
}

function GetAppStoreApplication_OnError() {
}