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
    if (current_actions.length < 20) {
        let listElement = createGeneralItem((current_actions.length + 1) + '. Replace text with text:')
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
        fromInput.placeholder = 'Replace this string (required)...'
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
    else {
        document.getElementsByClassName("error-msg")[0].innerText = '20 actions is maximum for one transformation!';
    }
}

function removeItem() {
    if (current_actions.length < 20) {
        let listElement = createGeneralItem((current_actions.length + 1) + '. Remove text:')
        let box = listElement.getElementsByTagName('div')[0]

        let newAction = {
            Action: 'Remove',
            ParameterFrom: '',
            ParameterTo: '',
            Order: current_actions.length + 1,
            DataResultId: null
        }

        current_actions.push(newAction)

        let fromInput = document.createElement('input')
        fromInput.placeholder = 'Remove this string (required)...'
        fromInput.style.width = '100%'
        fromInput.number = current_actions.length - 1;
        fromInput.addEventListener('input', changeFromParameter)
        box.append(fromInput)

        document.getElementById('toDo').append(listElement);
    }
    else {
        document.getElementsByClassName("error-msg")[0].innerText = '20 actions is maximum for one transformation!';
    }
}

function insertAItem() {
    if (current_actions.length < 20) {
        let listElement = createGeneralItem((current_actions.length + 1) + '. Insert text after text:')
        let box = listElement.getElementsByTagName('div')[0]

        let newAction = {
            Action: 'Insert after',
            ParameterFrom: '',
            ParameterTo: '',
            Order: current_actions.length + 1,
            DataResultId: null
        }

        current_actions.push(newAction)

        let fromInput = document.createElement('input')
        fromInput.placeholder = 'Insert after (required)...'
        fromInput.style.width = '100%'
        fromInput.number = current_actions.length - 1;
        fromInput.addEventListener('input', changeFromParameterInsertA)
        let toInput = document.createElement('input')
        toInput.placeholder = 'Insert this...'
        toInput.style.width = '100%'
        toInput.number = current_actions.length - 1;
        toInput.addEventListener('input', changeToParameterInsertA)
        box.append(fromInput)
        box.append(toInput)

        document.getElementById('toDo').append(listElement);
    }
    else {
        document.getElementsByClassName("error-msg")[0].innerText = '20 actions is maximum for one transformation!';
    }
}

function insertBItem() {
    if (current_actions.length < 20) {
        let listElement = createGeneralItem((current_actions.length + 1) + '. Insert text before text:')
        let box = listElement.getElementsByTagName('div')[0]

        let newAction = {
            Action: 'Insert after',
            ParameterFrom: '',
            ParameterTo: '',
            Order: current_actions.length + 1,
            DataResultId: null
        }

        current_actions.push(newAction)

        let fromInput = document.createElement('input')
        fromInput.placeholder = 'Insert before (required)...'
        fromInput.style.width = '100%'
        fromInput.number = current_actions.length - 1;
        fromInput.addEventListener('input', changeFromParameterInsertB)
        let toInput = document.createElement('input')
        toInput.placeholder = 'Insert this...'
        toInput.style.width = '100%'
        toInput.number = current_actions.length - 1;
        toInput.addEventListener('input', changeToParameterInsertB)
        box.append(fromInput)
        box.append(toInput)

        document.getElementById('toDo').append(listElement);
    }
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
function changeFromParameterInsertA(event) {
    current_actions[event.currentTarget.number].ParameterFrom = event.currentTarget.value
    current_actions[event.currentTarget.number].ParameterTo = event.currentTarget.value + event.currentTarget.parentElement.getElementsByTagName('input')[1].value
}
function changeToParameterInsertA(event) {
    current_actions[event.currentTarget.number].ParameterTo = event.currentTarget.parentElement.getElementsByTagName('input')[0].value + event.currentTarget.value
}
function changeFromParameterInsertB(event) {
    current_actions[event.currentTarget.number].ParameterFrom = event.currentTarget.value
    current_actions[event.currentTarget.number].ParameterTo = event.currentTarget.parentElement.getElementsByTagName('input')[1].value + event.currentTarget.value
}
function changeToParameterInsertB(event) {
    current_actions[event.currentTarget.number].ParameterTo = event.currentTarget.value + event.currentTarget.parentElement.getElementsByTagName('input')[0].value
}

function convert() {
    if (current_version === null) {
        document.getElementsByClassName("error-msg")[0].innerText = 'Upload a text first!';
    }
    else if (current_actions.length === 0) {
        document.getElementsByClassName("error-msg")[0].innerText = 'No actions!';
    }
    else if (current_actions.some((action) => action.ParameterFrom === "")) {
        document.getElementsByClassName("error-msg")[0].innerText = 'Action with number ' +
            (current_actions.findIndex((action) => action.ParameterFrom === "") + 1) + ' has empty required parameter!';
    }
    else {
        document.getElementsByClassName("error-msg")[0].innerText = '';

        let toSend = {
            Data: current_version,
            ToDo: current_actions,
            FileName: filename
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
                if (data.status === 413) {
                    document.getElementsByClassName("error-msg")[0].innerText = 'File content is too large!';
                }
                else if (data.status === 500) {
                    document.getElementsByClassName("error-msg")[0].innerText = 'Your actions caused the file to become too large to handle!';
                }
                else if (data.status === 503) {
                    document.getElementsByClassName("error-msg")[0].innerText = 'You already have a running job, please wait.';
                }
            }
        });
    }
}