var points = [];
var realPoints = [];
var funcName = "sin(x)";
var basePoints = [];


window.onload = function () {
    Draw(points);
};

var pointsCount;
var step;
var firstPointX;
function interpolate() {
    clearPoints();
    
    firstPointX = getFirstPointX();
    step = getStep();
    pointsCount = getPointsCount();
    alert(pointsCount);
    let number = getFuncNumber();
    let offset = getOffset();
    let pointToChange = getIndexOfPointToChange();

    doRequest(offset, pointToChange, number);
}


function doRequest(offset, pointToChange, funcNumber) {
    $.ajax({
        type: 'GET',
        url: "Interpolate",
        data: { 'firstPointX': firstPointX,'step':step, 'pointsCount':pointsCount, 'funcNumber':funcNumber, 'offset': offset, 'pointToChange':pointToChange },
        success: function (data,textStatus, xhr) {
            onSuccess(data);
        },
        error: function (a, jqXHR, exception) {
            onError();
        }
    });
}

function getFuncNumber() {
    let e = $('#e');
    let square = $('#square');
    let sin = $('#sin');

    if (square[0].checked){
        return 0;
    } else {
        if (sin[0].checked){
            return 1;
        } else {
            return 2;
        }
    }
}

function onSuccess(data) {
    // for linux need to replace . to ,
    let stringValues = data.replace(/,/g, ".").split(" ");
    parseResponse(stringValues);
    Draw();
}

function onError() {
    writeError("Не удалось получить ответ от сервера");
}