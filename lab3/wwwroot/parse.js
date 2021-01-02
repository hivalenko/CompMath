function parseX() {

    let xData = $('#xData')[0].value.replace(/,/g , ".");
    if(!parseXValues(xData,parsedX)){
        return;
    }

    if (parsedX.length < 3) {
        writeError("Введите по крайней мере 3 уникальные точки");
        return false;
    }
    parsedX.sort(function(x, y) {
        return x - y;
    });

    let xHandledData = [];
    for (let index = 0; index < parsedX.length; index++) {
        xHandledData.push(parsedX[index].toString());
    }
    return xHandledData;
}

function parseResponse(stringValues){
    funcName = stringValues[0];
    let gotenStep = parseFloat(stringValues[1]);
    let index = 2;
    let pcount = parseInt(pointsCount);
    let fpx = Number.parseFloat(firstPointX);
    for (; index <= pcount+1; index++) {
        
        let y = parseFloat(stringValues[index]);
        let point3 = {'x': fpx, 'y': y};
        fpx = fpx + gotenStep;
        basePoints.push(point3);
    }
    alert(index -2);
    //alert(parsedX[0] + " " + parseFloat(stringValues[1]));
    //alert(parseFloat(stringValues[index] + " " + parseFloat(stringValues[index+1])));
    for (index; index < stringValues.length; index++) {
        let x = parseFloat(stringValues[index]);
        index++;
        let y = parseFloat(stringValues[index]);
        index++;
        let realY = parseFloat(stringValues[index]);
        let point = {'x': x, 'y':y};
        points.push(point);
        let point2 = {'x': x, 'y': realY};
        realPoints.push(point2);
        
    }
}

function parseXValues(xData, parsedX) {
    let splitXdata = xData.split(' ');
    for (let index = 0; index < splitXdata.length; index++) {
        if (isStringNumber(splitXdata[index])){
            let newNumber = parseFloat(splitXdata[index]);
            if (isNaN(newNumber)){
                writeError("Необходимо вводить только вещественные числа");
                return false;
            }
            if (!parsedX.includes(newNumber)){
                parsedX.push(parseFloat(splitXdata[index]));
            }
        }
    }
    return true;
}

function isStringNumber(stringNumber) {
    return stringNumber !== " " && stringNumber !== "" &&
        stringNumber !== undefined && stringNumber != null &&
        stringNumber !== "\n" && stringNumber !== "\t";
}