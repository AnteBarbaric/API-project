function getRowsPerPage(tableId) {
    var table = $('#' + tableId);
    var panel = $(table).parent();
    var holder = $(panel).find('.table-rows-per-page');

    var select = $(holder).find('select:first');
    var sn = $(select).val();
    var n = parseInt(sn);
    return n;
}

function getTableId(e) {
    var table = $(e).closest('.card-body').find('table');
    var tableId = $(table).prop('id');

    return tableId;
}

function pageClick(e) {
    var page = $(e).parent().text();
    var tableId = getTableId(e);
    var npage = parseInt(page);
    onSearchDataChanged(tableId, 'page', npage);
}

function onLeftPagerClick(e, current) {
    var tableId = getTableId(e);
    onSearchDataChanged(tableId, 'page', current - 1);
}

function onRightPagerClick(e, current) {
    var tableId = getTableId(e);
    onSearchDataChanged(tableId, 'page', current + 1);
}
function getPagerClassActive(i, current) {
    return i === current ? ' active' : '';
}

function getLeftPagerClassDisabled(current, total) {
    return current === 1 ? ' disabled' : '';
}

function getRightPagerClassDisabled(current, total) {
    return current === total ? ' disabled' : '';
}

function getPagerHtml(current, total) {
    if (total === 1) {
        return '&nbsp;'
    }
    var html = '<nav aria-label="Page navigation">';
    html += '<ul class="pagination">';
    
    /*for (var i = 0; i < total; i++) {
        html += '<li class="page-item"><a class="page-link" href="#_" onclick="pageClick(this)">' + (i + 1) + '</a></li>';
    }
    */
    html += '<li class="page-item' + getLeftPagerClassDisabled(current, total) + '"><a class="page-link" href="#_" onclick="onLeftPagerClick(this, ' + current + ')"><span aria-hidden="true">&laquo;</span><span class="sr-only">Previous</span></a></li>';

    if (total < 10) {
        for (var i = 1; i <= total; i++) {
            html += '<li class="page-item' + getPagerClassActive(i, current) + '"><a class="page-link" href="#_" onclick="pageClick(this)">' + i + '</a></li>';
        }
    }
    else {
        if (current < 6) {
            for (var i = 1; i < 6; i++) {
                html += '<li  class="page-item' + getPagerClassActive(i, current) + '><a class="page-link" href="#_" onclick="pageClick(this)">' + i + '</a></li>';
            }
            html += '<li class="disabled"><a href="#_">...</a></li>';
            html += '<li><a href="#_" onclick="pageClick(this)">' + total + '</a></li>';
        }
        else if (current > total - 5) {
            html += '<li><a href="#_" onclick="pageClick(this)">' + 1 + '</a></li>';
            html += '<li class="disabled"><a href="#_">...</a></li>';
            for (var i = total - 4; i <= total; i++) {
                html += '<li' + getPagerClassActive(i, current) + '><a href="#_" onclick="pageClick(this)">' + i + '</a></li>';
            }
        }
        else {
            html += '<li><a href="#_" onclick="pageClick(this)">' + 1 + '</a></li>';
            html += '<li class="disabled"><a href="#_">...</a></li>';
            for (var i = current - 2; i <= current + 2; i++) {
                html += '<li' + getPagerClassActive(i, current) + '><a href="#_" onclick="pageClick(this)">' + i + '</a></li>';
            }
            html += '<li class="disabled"><a href="#_">...</a></li>';
            html += '<li><a href="#_" onclick="pageClick(this)">' + total + '</a></li>';
        }
    }

    html += '<li class="page-item' + getRightPagerClassDisabled(current, total) + '"><a class="page-link" href="#_" onclick="onRightPagerClick(this, ' + current + ')"><span aria-hidden="true">&raquo;</span><span class="sr-only">Next</span></a></li>';

    html += '</ul>';
    html += '</nav>';

    return html;
}
function getPagerHtmlOLD(current, total) {
    if (total === 1) {
        return '&nbsp;'
    }
    var html = '<ul class="pagination pagination-xs" style="margin: 0px;">';
    html += '<li' + getLeftPagerClassDisabled(current, total) + '><a href="#_" onclick="onLeftPagerClick(this, ' + current + ')">&laquo;</a></li>';
    if (total < 10) {
        for (var i = 1; i <= total; i++) {
            html += '<li' + getPagerClassActive(i, current) + '><a href="#_" onclick="pageClick(this)">' + i + '</a></li>';
        }
    }
    else {
        if (current < 6) {
            for (var i = 1; i < 6; i++) {
                html += '<li' + getPagerClassActive(i, current) + '><a href="#_" onclick="pageClick(this)">' + i + '</a></li>';
            }
            html += '<li class="disabled"><a href="#_">...</a></li>';
            html += '<li><a href="#_" onclick="pageClick(this)">' + total + '</a></li>';
        }
        else if (current > total - 5) {
            html += '<li><a href="#_" onclick="pageClick(this)">' + 1 + '</a></li>';
            html += '<li class="disabled"><a href="#_">...</a></li>';
            for (var i = total - 4; i <= total; i++) {
                html += '<li' + getPagerClassActive(i, current) + '><a href="#_" onclick="pageClick(this)">' + i + '</a></li>';
            }
        }
        else {
            html += '<li><a href="#_" onclick="pageClick(this)">' + 1 + '</a></li>';
            html += '<li class="disabled"><a href="#_">...</a></li>';
            for (var i = current - 2; i <= current + 2; i++) {
                html += '<li' + getPagerClassActive(i, current) + '><a href="#_" onclick="pageClick(this)">' + i + '</a></li>';
            }
            html += '<li class="disabled"><a href="#_">...</a></li>';
            html += '<li><a href="#_" onclick="pageClick(this)">' + total + '</a></li>';
        }
    }

    html += '<li' + getRightPagerClassDisabled(current, total) + '><a href="#_" onclick="onRightPagerClick(this, ' + current + ')">&raquo;</a></li>';
    html += '</ul>';

    return html;
}

function getPerformanceHtml(rowsOnPage, totalRows, timeMs) {
    var html = rowsOnPage + ' of ' + totalRows + ' rows (time ' + timeMs + ' ms)';

    return html;
}

function writeTable(o) {
    o.rowsOnPage = o.rows.length;
    var table = $('#' + o.tableId);
    writeRows(table, o.columnDefinitions, o.rows);
    var panel = $(table).closest('.card-body');
    var html = getPagerHtml(o.currentPage, o.totalPages);
    $('#accountpager').html(html);
    $(table).parent().find('.performance').html(o.rowsOnPage + ' of ' + o.totalRows + ' rows (db time is ' + o.elapsedMs + ' ms )' );
}

function writeRows(table, columnDefinitions, rows) {
    var tbody = $(table).find('tbody');
    $(tbody).empty();
    for (var i = 0; i < rows.length; i++) {
        writeRow(tbody, columnDefinitions, rows[i]);
    }
}
function writeRow(tbody, columnDefinitions, row) {
    var html = getRowHtml(columnDefinitions, row);
    $(tbody).append(html);
}

function getRowStyle(color) {
    if (color !== '') {
        return ' style="color: ' + color + ';"';
    }
    return '';
}

function getRowHtml(columnDefinitions, row) {
    var html = '<tr' + getRowStyle(row.color) + '>';

    for (var i = 0; i < columnDefinitions.length && i < row.cells.length; i++) {
        html += getElement(columnDefinitions[i], row.cells[i]);
    }

    html += '</tr>';
    return html;
}

function getElement(columnDefinition, text) {
    if (columnDefinition === null || columnDefinition.columnType === null) {
        return getTextElement(columnDefinition, text);
    }
    switch (columnDefinition.columnType) {
        case 'text':
            return getTextElement(columnDefinition, text);
        case 'textbox':
            return getTextBox(columnDefinition, text);
        case 'select':
            return getSelectElement(columnDefinition, text);
        case 'button':
            return getButtonElement(columnDefinition, text);
        case "checkbox":
            return getCheckboxElement(columnDefinition, text);
        case 'primaryKey':
            return getPrimaryKey(columnDefinition, text);
        default:
            return 'unknown column type';
    }
}

function getClassName(className) {
    if (className !== '') {
        return ' class="' + className + '"';
    }
    else {
        return '';
    }
}
function getTextBox(columnDefinition, text) {
    var html = '<td' + getClassName(columnDefinition.className) + '>';
    html += '<input type="text" name="' + columnDefinition.name + '" value="';
    html += text !== 'Blank' ? text : '';
    html += '" onchange="onTextBoxChanged(\'' + columnDefinition.name + '\', this);" ';
    html += 'style="width: 100%;"/>';
    html += '</td>'
    return html;
}
function getTextElement(columnDefinition, text) {
    var html = '<td';
    if (columnDefinition != null) {
        html += getClassName(columnDefinition.className);
    }
    html += '>';
    html += text !== 'Blank' ? text : '';
    html += '</td>'
    return html;
}
function getSelectElement(columnDefinition, selectedText) {
    var html = '<td' + getClassName(columnDefinition.className) + '>';
    html += '<div class="input-group input-group-sm">';
    html += '<select class="selectpicker form-control" style="height: 22px; padding-top: 2px; padding-bottom: 0px;" onchange="onSelectChanged(\'' + columnDefinition.name + '\', this)"' + '>';
    if (columnDefinition.items) {
        for (var i = 0; i < columnDefinition.items.length; i++) {
            var value = columnDefinition.items[i].value;
            var text = columnDefinition.items[i].text;
            var selectedPhrase = (text == selectedText || value == selectedText) ? ' selected="selected"' : '';
            html += '<option value="' + value + '"' + selectedPhrase + '>' + text + '</option>';
        }
    }
    html += '</select>';
    html += '</div>';
    html += '</td>';

    return html;
}

function getButtonElement(columnDefinition, id) {
    var html = '<td' + getClassName(columnDefinition.className) + '>';
    var classColor = columnDefinition.action == 'delete' ? 'btn-outline-danger' : 'btn-outline-success';
    html += '<button type="button" class="btn ' + classColor + ' btn-sm ' + columnDefinition.buttonClassName + '" ';
    html += 'onclick = "onButtonClick(this, \'' + columnDefinition.action + '\', \'' + id + '\')" >' + columnDefinition.title + '</button>';

    html += '</td>';

    return html;
}

function getChecked(text) {
    if (text === 'yes') {
        return 'checked="checked" ';
    }
    return '';
}

function getCheckboxElement(columnDefinition, text) {
    var items = text.split(':');
    var html = '<td' + getClassName(columnDefinition.className) + '>';
    html += '<input type="checkbox" name="selector" class="form-control" ';
    html += 'onclick="onCheckboxClick(this, \'' + columnDefinition.action + '\', \'' + items[0] + '\')" ';
    html += 'value = "' + items[2] + '" ';
    html += getChecked(items[1]) + ' />';
    html += '</td>';
    return html;
}
function getPrimaryKey(columnDefinition, text) {
    var html = '<td' + getClassName(columnDefinition.className) + ' style="display: none;">';
    html += '<input type="checkbox" name="primaryKey" ';
    html += 'value = "' + text + '" /> ';
    html += '</td>';
    return html;
}

function onRowsPerPageChanged(e) {
    var panel = $(e).closest('.card-body');
    if (panel.length > 0) {
        var table = $(panel).find('table');
        var tableId = $(table).prop('id');
    }
    else {
        tableId = 'myTable';
    }
    onSearchDataChanged(tableId, 'page', 1);
}

function getCurrentPage(pagerId) {
    let li = $('#' + pagerId).find('li.active');
    let pagetxt = $(li).find('a').text();
    let page = parseInt(pagetxt);
    return page;
}

function getPrimaryKeyOfRow(row) {
    let pk = $(row).find('input[name="primaryKey"]');
    return $(pk).val();
}
function getRowByPrimaryKey(tableId, pk) {
    var pkInput = $('#' + tableId + ' > tbody').find('input[name="primaryKey"][value="' + pk + '"]');
    if (pkInput.length > 0) {
        return $(pkInput).parent().parent();
    }
    return null;
}

function setSelectedByText(selectId, text) {
    if (text === '' || text === null) {
        $('#' + selectId).val(0);
    }
    else {
        let opt = $('#' + selectId).find('option[text="' + text + '"]');
        $(opt).prop('selected', true);
    }
}

