function clearPoints() {
    points = [];
    basePoints = [];
    realPoints = [];
    parsedX = [];
}
//TODO: add checking for fools
function getOffset() {
    let offset = $('#offset')[0].value.replace(/,/g , ".");
    if ("" === offset) offset = 0;
    return offset;
}

function getFirstPointX(){
    let fpx = $('#firstX')[0].value.replace(/,/g, ".");
    if ("" === fpx) {
        writeError("Необходимо ввести X первого узла");
        return false;
    }
    //if()
    return fpx;
}

function getStep(){
    let step = $('#step')[0].value;
    if("" === step){
        writeError("Введите шаг");
        return false;
    }
    // if(!Number.isInteger(parseInt(step))){
    //     writeError("Некорректный ввод шага ");
    //     return false;
    // }
    return step;
    
}

function getPointsCount(){
    let count = $('#count')[0].value;
    if("" === count){
        writeError("Введите количество узлов");
        return false;
    }
    // if(isNan(Number.count)){
    //     writeError("Некорректный ввод количества узлов");
    //     return false;
    // }
    // if(!Number.isInteger(count)){
    //     writeError("Количество элементов должно быть целым");
    //     return false;
    // }
    return count;
        
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

function getIndexOfPointToChange(){
    let index = $('#pointtoChange')[0].value;
    if("" === index) return 0;
    
    return index;
}


function Clear(){
    writeError('');
}

function writeError(msg){
    document.getElementById('input-error-label').innerHTML = msg;
}
