var script = document.createElement('script');
script.src = 'https://code.jquery.com/jquery-3.4.1.min.js';
script.type = 'text/javascript';
document.getElementsByTagName('head')[0].appendChild(script);

let current_version = null;
let filename = null;
let current_actions = [];

document.getElementById('upload').addEventListener('change', readFileAsString)

function readFileAsString() {
    var files = this.files;
    if (files.length === 0) {
        console.log('No file is selected');
        return;
    }

    var reader = new FileReader();
    reader.onload = function (event) {
        console.log(event.target.result.length)
        console.log('500000000')
        if (event.target.result.length > 500000000) {
            document.getElementById('upload').value = null;
            document.getElementsByClassName("error-msg")[0].innerText = 'File is too large!';
        }
        else {
            current_version = event.target.result;
            filename = files[0].name;
            document.getElementsByClassName("error-msg")[0].innerText = '';
        }
    };
    reader.readAsText(files[0]);
}

function replaceItem() {
    let listElement = createGeneralItem('Replace string with string:')
    let box = listElement.getElementsByTagName('div')[0]

    let newAction = {
        Action: 'Replace',
        ParameterFrom: '',
        ParameterTo: '',
        Order: current_actions.length + 1,
        DataResultId: null
    }

    current_actions.push(newAction)

    let fromInput = document.createElement('input')
    fromInput.placeholder = 'Replace this string...'
    fromInput.style.width = '100%'
    fromInput.number = current_actions.length - 1;
    fromInput.addEventListener('input', changeFromParameter)
    let toInput = document.createElement('input')
    toInput.placeholder = 'With this string...'
    toInput.style.width = '100%'
    toInput.number = current_actions.length - 1;
    toInput.addEventListener('input', changeToParameter)
    box.append(fromInput)
    box.append(toInput)

    document.getElementById('toDo').append(listElement);
}

function removeItem() {
    createGeneralItem('Remove:')
}

function insertAItem() {
    createGeneralItem('Insert after:')
}

function insertBItem() {
    createGeneralItem('Insert Before:')
}

function createGeneralItem(name) {
    let listElement = document.createElement('li')
    let newGenItem = document.createElement('div')
    let innerName = document.createElement('h5')
    listElement.appendChild(newGenItem);
    newGenItem.appendChild(innerName);
    innerName.innerText = name;
    newGenItem.className = 'toDo-item'
    return listElement
}

function changeFromParameter(event) {
    current_actions[event.currentTarget.number].ParameterFrom = event.currentTarget.value
}
function changeToParameter(event) {
    current_actions[event.currentTarget.number].ParameterTo = event.currentTarget.value
}


function convert() {
    if (current_version === null) {
        document.getElementsByClassName("error-msg")[0].innerText = 'Upload a text first!';
    }
    else if (current_actions.length === 0) {
        document.getElementsByClassName("error-msg")[0].innerText = 'No actions!';
    }
    else {
        document.getElementsByClassName("error-msg")[0].innerText = '';

        let toSend = {
            Data: current_version,
            ToDo: current_actions
        }
        
        $.ajax({
            type: 'POST',
            data: JSON.stringify(toSend),
            contentType: "application/json",
            url: '/Data/StartJob',
            success: function (data) {
                document.getElementById('download_button').setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(data.after))
                document.getElementById('download_button').setAttribute('download', filename.split('.').slice(0, -1).join('.') + 'Transformed.txt')
                document.getElementById('download_button').style.display = 'block'
            },
            error: function (data) {
                console.log(data)
            }
        });
    }
}