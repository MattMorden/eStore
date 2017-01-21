$(function () {
    //click on tab disable click if errors - register popup
    $('#myTabs .nav, nav-tabs').click(function (e) {
        $('#form0').validate();
        if ($('#form0').valid()) {
            return true;
        }
        else {
            return false;
        }
    }); //tabs

    $('#login_popup').modal('show'); //allow the login modal to appear

    $('#register_popup').modal('show'); //allow the register modal to appear

   // $('#logout').on('click', function(e) {del})

    //details anchor click - to load popup on catalogue
    $('a.btn-danger').on('click', function (e) {
        pcd = $(this).attr("data-prodcd");
        $('#qty').val('0');
        $('#ajaxMsg').text('');
        $('#detailsGraphic').attr('src', $('#Graphic' + pcd).attr('src'));
        $('#detailsProductName').text($('#Name' + pcd).text());
        $('#detailsDescription').text($('#Descr' + pcd).data('description'));
        $('#detailsProductcode').val(pcd);
        $('#detailsPrice').text($('#Price' + pcd).text());

    });
}); //main JQuery function

function logout() {
    document.getElementById('logoutForm').submit();
}//logout()






