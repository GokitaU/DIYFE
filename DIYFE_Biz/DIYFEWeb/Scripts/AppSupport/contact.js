$(document).ready(function () {
  
    $('#btnSendMail').click(function () {

        //field_error

        var contactMessage = {
            FullName: $('#FirstName').val() + ' ' + $('#LastName').val(),
            Email: $('#Email').val(),
            WebSite: $('#WebSite').val(),
            Phone: $('#Phone').val(),
            Subject: $('#Subject').val(),
            Message: $('#Message').val()
        }
        var hasError = false;
        var errorText ="The form has a incorrect values.  Please review";
        if ($('#FirstName').val() == '') {
            $('#FirstName').parent().parent().addClass('field_error');
            hasError = true;
        } else {
            $('#FirstName').parent().parent().removeClass('field_error');
        }

        if ($('#LastName').val() == '') {
            $('#LastName').parent().parent().addClass('field_error');
            hasError = true;
        } else {
            $('#LastName').parent().parent().removeClass('field_error');
        }

        if (contactMessage.Subject == '') {
            $('#Subject').parent().parent().addClass('field_error');
            hasError = true;
        } else {
            $('#Subject').parent().parent().removeClass('field_error');
        }


        if (contactMessage.Email == '') {
            $('#Email').parent().parent().addClass('field_error');
            hasError = true;
        } else {
            $('#Email').parent().parent().removeClass('field_error');
        }

        if (contactMessage.Subject == '') {
            $('#Subject').parent().parent().addClass('field_error');
            hasError = true;
        } else {
            $('#Subject').parent().parent().removeClass('field_error');
        }

        if (contactMessage.Message == '') {
            $('#Message').parent().parent().addClass('field_error');
            hasError = true;
        } else {
            $('#Message').parent().parent().removeClass('field_error');
        }

        if ($('#Newsletter:checked').length > 0) {
            contactMessage.NewsLetter = "true";
        }

        if (hasError) {
            $('#formErrorDiv').show();
            $('#formErrorDiv').attr("tabindex", -1).focus();
            $('#formErrorDiv').html('<p>' + errorText + '</p>');

            return;
        } else {
            $('#formErrorDiv').hide();
            $('#formSuccess').show();
            $('#formSuccess').attr("tabindex", -1).focus();

        }


        $.ajax({
            url: GetRootURL() + 'Home/SendContactEmail',
            data: JSON.stringify(contactMessage),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success) {
                    //loadingDiv.hide();
                    $('.success').show();
                    //mediator.publish('BetLoadDone', {});
                } else {
                    //show error
                    $('#formErrorDiv').show();
                    $('#formErrorDiv').attr("tabindex", -1).focus();
                    $('#formErrorDiv').html('<p>' + data.message + '</p>');
                }
            },
            error: function () {
                mediator.publish('logError', { error: 'Error with service for Insert Article', method: 'Insert Article' });
            }
        });

        return false;
    });
});