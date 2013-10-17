$(document).ready(function () {

    var commentTemplate =
                    '<li class="sfcommentDetails" id="{0}">' +
                            '<span class="sfcommentNumber">0</span>'
    '<strong class="sfcommentAuthor">{1}</strong>' +
    '<em class="sfcommentDate">{2}</em>' +
    '<div class="sfcommentText">{3}</div>' +
    '</li>';

    $('#btnSubmitComment').click(function () {

        var comment = {
            ArticleId: $('#articleId').val(),
            PosterName: $('#txtPosterName').val(),
            PosterEmail: $('#txtPosterEmail').val(),
            PosterWebSite: $('#txtPosterWebSite').val(),
            Text: $('#txtCommentText').val()
        }

        if (comment.PosterName == '') {
            alert('Post Name is required.');
            return;
        }
        if (comment.Text == '') {
            alert('Comment Text is required.');
            return;
        }

        $.ajax({
            url: GetRootURL() + 'Service/PostComment',
            data: JSON.stringify(comment),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success) {
                    //loadingDiv.hide();
                    //mediator.publish('BetLoadDone', {});
                    var commentList = $('#commentsList');
                    var addHtml = commentTemplate.format(data.obj.Id, data.obj.PosterName, data.obj.CreatedDate, data.obj.Text);
                    commentList.prepend(addHtml);
                    commentList.find('li [id=' + data.obj.Id + ']').focus();
                    alert('remimber to add comment to page - tried to fix to to return the added object but enity framework doesnt return the id and the jquery framework doesnt support focus or it ist working correctly');
                } else {
                    //show error
                    mediator.publish('pageError', { error: data.message, method: 'Insert Comment' });
                }
            },
            error: function () {
                mediator.publish('logError', { error: 'Error with service for InsertNBABetting', method: 'NBA Load Bets' });
            }
        });

    });

});