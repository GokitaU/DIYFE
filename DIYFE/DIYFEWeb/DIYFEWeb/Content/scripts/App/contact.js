$(document).ready(function () {
  
    $('#btnSendMail').click(function () {

        var contactMessage = {
            firstName: $('#FirstName').val(),
            lastName: $('#LastName').val(),
            email: $('#Email').val(),
            message: $('#Message').val(),
        }

        $.ajax({
            url: GetRootURL() + 'Service/SendContactEmail',
            data: JSON.stringify(contactMessage),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success) {
                    //loadingDiv.hide();
                    //mediator.publish('BetLoadDone', {});
                } else {
                    //show error
                    
                }
            },
            error: function () {
                mediator.publish('logError', { error: 'Error with service for Insert Article', method: 'Insert Article' });
            }
        });

        return false;
    });
});