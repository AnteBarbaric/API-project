// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// dodatni kod za submenu
$('.dropdown-menu a.dropdown-toggle').on('click', function (e) {
    if (!$(this).next().hasClass('show')) {
        $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
    }
    var $subMenu = $(this).next(".dropdown-menu");
    $subMenu.toggleClass('show');


    $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
        $('.dropdown-submenu .show').removeClass("show");
    });

    return false;
});

function modalLogin() {
    function showMessage(text) {
        $('#waitingSpinner').hide();
        $('#loginMessage').text(text);
        $('#loginMessage').show();
    }
    function getData() {
        let o = {
            username: $('#username').val(),
            password: $('#password').val()
        }
        return o;
    }
    function checkData(o) {
        if (o.username === '') {
            showMessage('Molimo, upišite korisničko ime!');
           return false;
        }
        else if (o.password === '') {
            showMessage('Molimo, upišite password!');
           return false;
        }
        return true;
    }
    
    $('#loginMessage').hide();
    
    let o = getData();
    if (!checkData(o)) {
        return;
    }
    $('#waitingSpinner').show();
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
            if (res.code === 200) {
                $('#waitingSpinner').hide();
                $('#loginModal').modal('hide');
            }
            else {
               showMessage(res.message);
            }
        })
        .catch(err => showMessage(err));
}