//***************OTHER METHODS***************

function RemoveSpecialCharacter(sReturnValue) {
    return sReturnValue.replace(/[^a-zA-Z0-9_]+/g, '');
}

function ClearAll() {
    $('#drpImageList option:eq(0)').attr('selected', 'selected');

    $('#txtCategoryName').val('');
    $('#txtApplicationName').val('');
    $('#txtDescription').val('');
    $('#txtManifest').val('');
}

//***************CATEGORY***************
function GetCategory() {
    PageMethods.getCat(GetCategory_OnSuccess, GetCategory_OnError);
}

function GetCategory_OnSuccess(resultText) {
    var categoryAry = eval(resultText);

    $('#drpCategory').append($('<option></option>').val("0").html("Select Category"));
    $(categoryAry).each(function (i) {
        $('#drpCategory').append($('<option></option>').val(categoryAry[i].Id).html(categoryAry[i].Name));
    });
}

function GetCategory_OnError() {
}

function CategorySelectedIndexChanged() {
    $('#drpImageList').empty()
    $('#btnCreateAppID').attr('disabled', 'disabled');

    if ($("#drpCategory option:selected").val() == "0") {
        $('#txtCategoryName').val('');
        $('#lblSelectedCategory').text('');
    }
    else {
        $('#lblSelectedCategory').text($("#drpCategory option:selected").text());
        GetAppStoreApplication($("#drpCategory option:selected").val(), 0);
        $('#txtCategoryName').val(RemoveSpecialCharacter($("#drpCategory option:selected").text().toLowerCase()));
        var cID = $("#drpCategory option:selected").val();
        PageMethods.getImage(cID, 0, GetImage_onSuccess, GetImage_onFailure);
    }
}

//***************IMAGE***************

function GetImage_onSuccess(resultText) {
    var imageAry = eval(resultText);
    $('#drpImageList').append($('<option></option>').val("0#0").html("Select Image"));
    $(imageAry).each(function (i) {
        $('#drpImageList').append($('<option></option>').val(imageAry[i].Id).html(imageAry[i].Name));
    });
}

function GetImage_onFailure() {
}

function ImageSelectedIndexChanged() {
    if ($("#drpImageList option:selected").val() == "0") {
        $('#btnCreateAppID').attr('disabled', 'disabled');
    }
    else {
        if (IsAppIDCreated($('#drpImageList option:selected').val())) {
            alert('AppID was already created!');
            $('#txtApplicationName').val('');
            $('#btnCreateAppID').attr('disabled', 'disabled');
        }
        else {
            $('#btnCreateAppID').removeAttr('disabled');
            $('#txtApplicationName').val(RemoveSpecialCharacter($("#drpImageList option:selected").text().toLowerCase()));
        }
    }
}

function IsAppIDCreated(imageID) {
    var bValue = false;
    $('#tblApplicationList tbody input:hidden').each(function (i) {
        var obj = $('#tblApplicationList tbody input:hidden')[i];
        if (obj.value == imageID) {
            bValue = true;
            return false;
        }
    });
    return bValue;
}

//***************CREATE APPLICATION***************

function CreateApplication() {
    var CategoryID = $('#drpCategory option:selected').val();
    var ImageID = $('#drpImageList option:selected').val().split('#');
    ImageID = ImageID[0];

    var Name =
    RemoveSpecialCharacter($('#txtCompanyAbbr').val()) + '_' +
    RemoveSpecialCharacter($('#txtCompanyName').val()) + '_' +
    RemoveSpecialCharacter($('#txtCategoryName').val()) + '_' +
    RemoveSpecialCharacter($('#txtApplicationName').val());

    PageMethods.InsertApplication(
                                                                        0,
                                                                        Name,
                                                                        $('#txtDescription').val(),
                                                                        $('#txtManifest').val(),
                                                                        ImageID,
                                                                        $('#drpImageList option:selected').text(),
                                                                        CategoryID,
                                                                       $('#drpCategory option:selected').text(),
                                                                        'N',
                                                                        0,
                                                                        CreateApplication_OnSuccess,
                                                                        CreateApplication_OnError);
}

function CreateApplication_OnSuccess(resultText) {
    var resultAry = eval(resultText);

    if (resultAry[0].Id = "0") {
        ClearAll();
        GetAppStoreApplication($('#drpCategory option:selected').val(), 0);
    }
    alert(resultAry[0].Message);
}

function CreateApplication_OnError(errText) {
}

//***************SHOW CREATED APPLICATION***************

function GetAppStoreApplication(CategoryId, UserID) {
    $('#tblApplicationList tbody').empty();
    PageMethods.getAppSotreApp(CategoryId, UserID, GetAppStoreApplication_OnSuccess, GetAppStoreApplication_OnError);
}

function GetAppStoreApplication_OnSuccess(resultText) {
    var appAry = eval(resultText);

    $(appAry).each(function (i) {
        $('#tblApplicationList  > tbody:last').append(
        '<tr>' +
        '<td>' + appAry[i].CategoryName + '</td>' +
        '<td>' + appAry[i].BeastImageName + '<input type ="hidden"  id="' + "hdfImageID" + i + '" value ="' + appAry[i].SifID + '"></input></td>' +
        '<td>' + appAry[i].Name + '</td>' +
        '<td>' + appAry[i].ManifestUrl + '</td>' +
        '</tr>');
    });
}

function GetAppStoreApplication_OnError() {
}