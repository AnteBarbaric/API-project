$(document).ready(function () {
    initPage();
});

function initPage() {
    onSearchDataChanged('myTable', 'initial', 1);
}

function onSearchSuccess(response) {
    writeTable(response);
    $('#mainAjaxLoader').hide();
}
function onSearchError(response) {
    $('#mainMessage').html(response);
    $('#mainMessage').show();
}

function onSearchDataChanged(tableId, trigger, page) {
    let o = {
        tableId: tableId,
        trigger: trigger,
        page: page,
        rowsPerPage: getRowsPerPage(tableId),
        filterUsername: $('#filterUsername').val(),
        filterLanguageId: $('#filterLanguageId').val(),
        filterUserRoleId: parseInt($('#filterUserRoleId').val()),
        filterStateId: $('#filterStateId').val()
    };
    let data = JSON.stringify(o);
    let url = 'http://localhost:55039/api/account/table';
    getAjax('POST', url, data, onSearchSuccess, onSearchError);
}

function onButtonClick(e, action, id) {
    switch (action) {
        case 'edit':
            editAccount(id);
            break;
        case 'delete':
            deleteAccount(id);
            break;
        default: alert('wrong acction in onButtonClick!');
    }
}

function addNewAccount() {
    $('#userLoginId').val(0);
    $('#username').val('');
    $('#password').val('');
    $('#passwordGroup').show();
    $('#firstName').val('');
    $('#lastName').val('');
    $('#email').val('');
    $('#phone').val('');
    $('#selLanguage').val('0');
    $('#selUserRole').val(0);
    $('#selState').val('0');

    $('#accountModal').modal('show');
}

function editAccount(id) {
    o = getDataObjectFromTable(id);
    $('#userLoginId').val(o.userLoginId);
    $('#username').val(o.username);
    $('#password').val('');
    $('#passwordGroup').hide();
    $('#firstName').val(o.firstName);
    $('#lastName').val(o.lastName);
    $('#email').val(o.email);
    $('#phone').val(o.phone);
    $('#selLanguage').val(o.languageId);
    $('#selUserRole').val(o.userRoleId);
    $('#selState').val(o.stateId);

    $('#accountModal').modal('show');
}

function getDataObjectFromTable(id) {
    let row = getRowByPrimaryKey('myTable', id);
    let cells = $(row).find('td');
    let o = {
        userLoginId: id,
        username: $(cells[0]).text(),
        firstName: $(cells[1]).text(),
        lastName: $(cells[2]).text(),
        email: $(cells[3]).text(),
        phone: $(cells[4]).text(),
        languageId: $(cells[5]).find('select').val(),
        userRoleId: $(cells[6]).find('select').val(),
        stateId: $(cells[7]).find('select').val()
    }
    return o;
}

function getDataObjectFromForm(id) {
    let o = {
        userLoginId: parseInt($('#userLoginId').val()),
        username: $('#username').val(),
        password: $('#password').val(),
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        email: $('#email').val(),
        phone: $('#phone').val(),
        languageId: $('#selLanguage').val(),
        userRoleId: parseInt($('#selUserRole').val()),
        stateId: $('#selState').val()
    }
    return o;
}

function saveData() {
    let o = getDataObjectFromForm();
    if (!checkData(o)) {
        return;
    }
    $('#modalMessage').hide();
    $('#modalAjaxLoader').show();

    let url = 'http://localhost:55039/api/account/';
    let data = JSON.stringify(o);
    let action;
    if (o.userLoginId === 0) {
        action = 'POST';
    }
    else {
        action = 'PUT';
        url += o.userLoginId;
    }
    getAjax(action, url, data, onSaveDataSuccess, onSaveDataError);
}

function checkData(o) {
    if (o.username === '') {
        showModalMessage('Pls. enter username!');
        return false;
    }
    if (o.email === '') {
        showModalMessage('Pls. enter e-mail!');
        return false;
    }
    if (o.userLoginId === 0 && o.password === '') {
        showModalMessage('Pls. enter password!');
        return false;
    }
    if (o.userRoleId === 0) {
        showModalMessage('Pls. select user role!');
        return false;
    }
    if (o.stateId === '0') {
        showModalMessage('Pls. select status!');
        return false;
    }
    return true;
}

function showModalMessage(msg) {
    $('#modalMessage').html(msg);
    $('#modalMessage').show();
}
function deleteAccount(id) {
    let url = 'http://localhost:55039/api/account/' + id;
    getAjax('DELETE', url, null, onAccountOperationSuccess, onAccounOperationError);
}

function onSaveDataSuccess(response) {
    $('#modalAjaxLoader').hide();
    let currentPage = getCurrentPage('accountpager');
    onSearchDataChanged('myTable', 'page', currentPage);
    $('#accountModal').modal('hide');
}

function onSaveDataError(response) {
    $('#modalAjaxLoader').hide();
    showModalMessage(response);
}

function onAccountOperationSuccess(response) {
    let currentPage = getCurrentPage('accountpager');
    onSearchDataChanged('myTable', 'page', currentPage);
}

function onAccounOperationError(response) {
    $('#mainMessage').html(response);
    $('#mainMessage').show();
}

function onSelectChanged(target, e) {
    let row = $(e).closest('tr');
    let id = parseInt(getPrimaryKeyOfRow(row));
    let o = {
        id: id,
        target: target,
        val: $(e).val()
    };
    let url = 'http://localhost:55039/api/account/' + id;
    let data = JSON.stringify(o);
    getAjax('PATCH', url, data, onAccountOperationSuccess, onAccounOperationError);
}

function filterChanged(e) {
    onSearchDataChanged('myTable', 'filter', 1);
}
