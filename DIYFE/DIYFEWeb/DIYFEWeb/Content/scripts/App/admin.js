//$(document).ready(function () {

//    $('#FirstLevCat').change(function () {

//        $('#SecondLevCat .dropDownOption').hide();
//        $('#SecondLevCat [FirstLevCatId="' + $(this).val() + '"]').show();

//        //alert($('#FirstLevCat option:selected').attr('catname'));
//        $('#NewFirstLevCatName').val($('#FirstLevCat option:selected').attr('catname'));
//        $('#NewFirstLevCatUrl').val($('#FirstLevCat option:selected').attr('caturl'));
//    });

//    $('#SecondLevCat').change(function () {

//        $('#ThirdLevCat .dropDownOption').hide();
//        $('#ThirdLevCat [SecondLevCatId="' + $(this).val() + '"]').show();

//        $('#NewSecondLevCatName').val($('#SecondLevCat option:selected').attr('catname'));
//        $('#NewSecondLevCatUrl').val($('#SecondLevCat option:selected').attr('caturl'));
//    });

//    $('#btnAddNewArtical').click(function () {
//        var catRowId = 0;

//        if ($('#ThirdLevCat option:selected').attr('catRowId') != '0') {
//            catRowId = $('#ThirdLevCat option:selected').attr('catRowId')
//        }else if ($('#SecondLevCat option:selected').attr('catRowId') != '0'){
//            catRowId = $('#SecondLevCat option:selected').attr('catRowId')
//        } else if ($('#FirstLevCat option:selected').attr('catRowId') != '0') {
//            catRowId = $('#FirstLevCat option:selected').attr('catRowId')
//        }


//        if (catRowId > 0) {
//            var artical = {
//                ArticleTypeId: $('#artType').val(),
//                CategoryRowId: catRowId,
//                //CategoryId: $('#FirstLevCat').val(),
//                //SecondLevCategoryId: $('#SecondLevCat').val(),
//                //ThirdLevCategoryId: $('#ThirdLevCat').val(),
//                ViewRequests: 0,
//                URLLink: $('#txtUrlLink').val(),
//                NameId: $('#txtNameId').val(),
//                Title: $('#txtTitle').val(),
//                Description: $('#txtDescription').val(),
//                Author: $('#txtAuthor').val(),
//                Keywords: $('#txtKeywords').val(),
//                MainContent: $('#txtMainContent').val(),
//                SideContent: $('#txtSideContent').val(),
//                ListItemContent: $('#txtListItemContent').val(),
//                Name: $('#txtName').val(),
//            }


//            $.ajax({
//                url: GetRootURL() + 'Admin/AddArticle',
//                data: JSON.stringify(artical),
//                type: 'POST',
//                contentType: 'application/json; charset=utf-8',
//                success: function (data) {
//                    if (data.success) {
//                        alert('done');
//                        //loadingDiv.hide();
//                        //mediator.publish('BetLoadDone', {});
//                    } else {
//                        //show error
//                        alert(data.message);
//                        mediator.publish('pageError', { error: data.message, method: 'Insert Article' });
//                    }
//                },
//                error: function () {
//                    mediator.publish('logError', { error: 'Error with service for Insert Article', method: 'Insert Article' });
//                }
//            });
//        } else {
//            alert('DOOOD...pick a cat');
//        }

//    });

//    $('#btnAddNewCategory').click(function () {

//        var category = {
//            CategoryId: $('#FirstLevCat').val(),
//            SecondLevCategoryId: $('#SecondLevCat').val(),
//            ThirdLevCategoryId: $('#ThirdLevCat').val(),
//            CategoryName: $('#NewFirstLevCatName').val(),
//            CategoryUrl: $('#NewFirstLevCatUrl').val().toLowerCase(),
//            SecondLevCategoryName: $('#NewSecondLevCatName').val(),
//            SecondLevCategoryUrl: $('#NewSecondLevCatUrl').val().toLowerCase(),
//            ThirdLevCategoryName: $('#NewThirdLevCatName').val(),
//            ThirdLevCategoryUrl: $('#NewThirdLevCatUrl').val().toLowerCase()
//        }

//        //if (comment.PosterName == '') {
//        //    alert('Post Name is required.');
//        //    return;
//        //}
//        //if (comment.Text == '') {
//        //    alert('Comment Text is required.');
//        //    return;
//        //}

//        $.ajax({
//            url: GetRootURL() + 'Admin/AddCategory',
//            data: JSON.stringify(category),
//            type: 'POST',
//            contentType: 'application/json; charset=utf-8',
//            success: function (data) {
//                if (data.success) {
//                    alert('done');
//                    //loadingDiv.hide();
//                    //mediator.publish('BetLoadDone', {});
//                } else {
//                    //show error
//                    mediator.publish('pageError', { error: data.message, method: 'Insert Category' });
//                }
//            },
//            error: function () {
//                mediator.publish('logError', { error: 'Error with service for adding category', method: 'insert category' });
//            }
//        });

//    });

//});