$(document).ready(function () {
    alert('Ovaj dokument je spreman!')
    $('input').keydown(function () {
        $('#message').hide();
    });
    $('#username').focus();

    $('#username').bind('keypress', function (e) {
        if (e.keyCode === 13) {
            $('#password').focus();
        }
    });
    $('#password').bind('keypress', function (e) {
        if (e.keyCode === 13) {
            postLogin();
        }
    });
})

function getData() {
    let o = {
        username: $('#username').val(),
        password: $('#password').val()
    }
    return o;
}
function checkData(o) {
    let ok = true;
    if (o.username == '') {
        alert('Molimo, upišite korisničko ime!');
        ok = false;
    }
    else if (o.password == '') {
        alert('Molimo, upišite password!');
        ok = false;
    }
    return true;
}

function postLoginSuccess(response) {
    $(location).prop('href', '/Home/Index');
}
function postLoginError(response) {
    console.log(response);
}
function postLogin() {
    let o = getData();
    if (!checkData(o)) {
        return;
    }
    let data = JSON.stringify(o);
    let url = 'http://localhost:55039/api/signin';
    //getAjax('POST', url, data, postLoginSuccess, postLoginError);

    
    const options = {
        method: 'POST',
        body: data,
        headers: {
            'Content-Type': 'application/json'
        }
    }

    fetch(url, options)
        .then(res => res.json())
        .then(res => {
            console.log(res);
            if (res.code == 200) {
                $(location).prop('href', '/Home/Index');
            }
            else {
                $('#message').text(res.message);
                $('#message').show();
            }
        })
        .catch(err => console.log(err));
}
