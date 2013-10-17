(function ($) {
    $.fn.extend({
        pagerSort: function (options) {
            var settings = $.extend({
                totalrecords: 0,
                recordsperpage: 0,
                length: 10,
                next: 'Next',
                prev: 'Prev',
                first: 'First',
                last: 'Last',
                go: 'Go',
                theme: 'green',
                display: 'double',
                initval: 1,
                model: {},
                sortOrder: 'asc',
                datacontainer: '', //data container id
                dataelement: '', //children elements to be filtered e.g. tr or div
                onchange: null
            }, options);
            return this.each(function () {

                //var dataList = new Array();
                var headerTemplate = '';
                var currentPage = 0;
                var startPage = 0;
                var totalpages = parseInt(settings.totalrecords / settings.recordsperpage);
                if (settings.totalrecords % settings.recordsperpage > 0) totalpages++;
                var initialized = false;
                var container = $(this).addClass('pager').addClass(settings.theme);
                container.find('ul').remove();
                container.find('div').remove();
                container.find('span').remove();
                var dataContainer;
                var dataElements;
                if (settings.datacontainer != '') {
                    dataContainer = $('#' + settings.datacontainer);
                    dataElements = $('' + settings.dataelement + '', dataContainer);
                }
                var list = $('<ul/>');
                var btnPrev = $('<div/>').text(settings.prev).click(function () { currentPage = parseInt(list.find('li a.active').text()) - 1; navigate(--currentPage); }).addClass('btn');
                var btnNext = $('<div/>').text(settings.next).click(function () { currentPage = parseInt(list.find('li a.active').text()); navigate(currentPage); }).addClass('btn');
                var btnFirst = $('<div/>').text(settings.first).click(function () { currentPage = 0; navigate(0); }).addClass('btn');
                var btnLast = $('<div/>').text(settings.last).click(function () { currentPage = totalpages - 1; navigate(currentPage); }).addClass('btn');
                var inputPage = $('<input/>').attr('type', 'text').keydown(function (e) {
                    if (isTextSelected(inputPage)) inputPage.val('');
                    if (e.which >= 48 && e.which < 58) {
                        var value = parseInt(inputPage.val() + (e.which - 48));
                        if (!(value > 0 && value <= totalpages)) e.preventDefault();
                    } else if (!(e.which == 8 || e.which == 46)) e.preventDefault();
                });
                var btnGo = $('<input/>').attr('type', 'button').attr('value', settings.go).addClass('btn').click(function () { if (inputPage.val() == '') return false; else { currentPage = parseInt(inputPage.val()) - 1; navigate(currentPage); } });
                container.append(btnFirst).append(btnPrev).append(list).append(btnNext).append(btnLast).append($('<div/>').addClass('short').append(inputPage).append(btnGo));
                if (settings.display == 'single') {
                    btnGo.css('display', 'none');
                    inputPage.css('display', 'none');
                }

                buildNavigation(startPage);

                if (settings.initval == 0) settings.initval = 1;
                currentPage = settings.initval - 1;

                if (dataContainer != null) {
                    if (dataContainer.length > 0) {
                        // console.log(dataContainer.html());
                        //display elements that need to be displayed
                        if ($(dataElements[0]).find('th').length > 0) {

                            // alert($(dataElements[0]).html());
                            headerTemplate = '<tr>' + $(dataElements[0]).html() + '</tr>';
                            //

                            $(dataElements[0]).find('th').each(function (index) {
                                $(this).append('<div class="LoadingContent"><img src="../Content/Images/ajax-loader.gif" /></div>');

                                $(this).click(function () {

                                    $(this).find('.LoadingContent').show();

                                    SortData($(this).attr('column'));

                                    //                                    var dataRowString = '';

                                    //                                    var localDataItems = dataContainer.find('tr:gt(0)'); //.html('');

                                    //                                    for (var dataRow in dataList) {

                                    //                                        dataRowString = '';
                                    //                                        for (var dataItem in dataList[dataRow]) {
                                    //                                            dataRowString += '<td>' + dataList[dataRow][dataItem] + '</td>';
                                    //                                        }

                                    //                                        $(localDataItems[dataRow]).html(dataRowString);

                                    //                                    }

                                    currentPage = 0;
                                    navigate(currentPage);

                                    $(this).find('.LoadingContent').hide();

                                });
                            });


                        }

                        //                        for (var i = 1; i < settings.totalrecords + 1; i++) {

                        //                            dataList[dataList.length] = new Object();

                        //                            $(dataElements[i]).find('td').each(function (index) {
                        //                                //                                    if ($(this).children().length > 0) {
                        //                                //                                        alert($(this).children().eq(0).html());
                        //                                //                                    }
                        //                                dataList[dataList.length - 1][index] = $(this).html();
                        //                            });
                        //                        }
                    }
                }

                navigate(currentPage);
                initialized = true;

                function showLabels(pageIndex) {
                    container.find('span').remove();
                    var upper = (pageIndex + 1) * settings.recordsperpage;
                    if (upper > settings.totalrecords) upper = settings.totalrecords;
                    container.append($('<span/>').append($('<b/>').text(pageIndex * settings.recordsperpage + 1)))
                                             .append($('<span/>').text('-'))
                                             .append($('<span/>').append($('<b/>').text(upper)))
                                             .append($('<span/>').text('of'))
                                             .append($('<span/>').append($('<b/>').text(settings.totalrecords)));
                }

                function buildNavigation(startPage) {
                    list.find('li').remove();
                    if (settings.totalrecords <= settings.recordsperpage) return;
                    for (var i = startPage; i < startPage + settings.length; i++) {
                        if (i == totalpages) break;
                        list.append($('<li/>')
                                    .append($('<a>').attr('id', (i + 1)).addClass(settings.theme).addClass('normal')
                                    .attr('href', 'javascript:void(0)')
                                    .text(i + 1))
                                    .click(function () {
                                        currentPage = startPage + $(this).closest('li').prevAll().length;
                                        navigate(currentPage);
                                    }));
                    }
                    showLabels(startPage);
                    inputPage.val((startPage + 1));
                    list.find('li a').addClass(settings.theme).removeClass('active');
                    list.find('li:eq(0) a').addClass(settings.theme).addClass('active');
                    showRequiredButtons(startPage);
                }

                function buildPage(startIndexLocal, endIndexLocal) {
                    //                    alert('Current page:' + currentPage +
                    //                          ' Total page:' + settings.totalrecords +
                    //                           ' recordsperpage:' + settings.recordsperpage +
                    //                           ' startIndex: ' + startIndexLocal +
                    //                           ' endIndex: ' + endIndexLocal);
                    //alert(dataContainer.html());

                    dataContainer.find('.row').remove();
                    var pageHtml = '';
                    var columnName = '';
                    for (startIndexLocal; startIndexLocal < endIndexLocal; startIndexLocal++) {
                        pageHtml += '<tr class="row">';
                        $(dataElements[0]).find('th').each(function (index) {
                            columnName = $(this).attr('column');
                            if (columnName == 'FirstName' || columnName == 'LastName') {
                                pageHtml += '<td><a href="../Member/Details?memberId=' + settings.model[startIndexLocal]['MemberID'] + '">' + settings.model[startIndexLocal][columnName] + '</a></td>';                            
                            } else {
                                pageHtml += '<td>' + settings.model[startIndexLocal][columnName] + '</td>';
                            }
                            // alert($(this).attr('column'));
                        });
                        pageHtml += '</tr>';

                    }
                    dataContainer.append(pageHtml);
                }

                function navigate(topage) {
                    //make sure the page in between min and max page count
                    var index = topage;
                    var mid = settings.length / 2;

                    if (settings.length % 2 > 0) mid = (settings.length + 1) / 2;
                    var startIndex = 0;

                    if (topage >= 0 && topage < totalpages) {
                        if (topage >= mid) {
                            if (totalpages - topage > mid)
                                startIndex = topage - (mid - 1);
                            else if (totalpages > settings.length)
                                startIndex = totalpages - settings.length;
                        }
                        buildNavigation(startIndex); showLabels(currentPage);
                        list.find('li a').removeClass('active');
                        inputPage.val(currentPage + 1);
                        list.find('li a[id="' + (index + 1) + '"]').addClass('active');
                        var recordStartIndex = currentPage * settings.recordsperpage;
                        var recordsEndIndex = recordStartIndex + settings.recordsperpage;
                        if (recordsEndIndex > settings.totalrecords)
                            recordsEndIndex = settings.totalrecords % recordsEndIndex;

                        if (initialized) {
                            if (settings.onchange != null) {
                                settings.onchange((currentPage + 1), recordStartIndex, recordsEndIndex);
                            }
                        }

                        buildPage(recordStartIndex, recordsEndIndex);
                        //                        if (dataContainer != null) {
                        //                            // alert('not null');
                        //                            if (dataContainer.length > 0) {
                        //                                // alert('has lenght');
                        //                                //hide all elements first
                        //                                dataElements.css('display', 'none');
                        //                                //display elements that need to be displayed
                        //                                if ($(dataElements[0]).find('th').length > 0) { //if there is a header, keep it visible always

                        //                                    $(dataElements[0]).css('display', '');

                        //                                    recordStartIndex++;
                        //                                    recordsEndIndex++;
                        //                                }
                        //                                // alert(recordStartIndex + ' ' + recordsEndIndex + ' total:' + $(dataElements).length);
                        //                                for (var i = recordStartIndex; i < recordsEndIndex; i++) {

                        //                                    $(dataElements[i]).css('display', '');
                        //                                }
                        //                            }
                        //                        }

                        showRequiredButtons();
                    }
                }

                function SortData(column) {

                    // alert(dataList[0][columnIndex]);

                    var ascSort = true;
                    if (settings.sortOrder == 'asc') {
                        settings.sortOrder = 'decs';
                    } else {
                        settings.sortOrder = 'asc';
                        ascSort = false;
                    }
                    //                    if ($(dataList[0][columnIndex]).find().children().length > 0) {
                    //                        alert($(dataList[0][columnIndex]).html());
                    //                    } else {
                    //                        alert('not working');
                    //                    }

                    //SORT BY INT
                    if (settings.model[0][column].tryParseInt() > -1) {

                        settings.model.sort(function compareTotals(a, b) {
                            if (ascSort) {
                                return b[column] - a[column];
                            } else {
                                return a[column] - b[column];
                            }
                        });

                    } else {
//                        var isHtml = false;
//                        if ($(dataList[0][columnIndex]).html() != null) {
//                            isHtml = true;
//                        }
                        settings.model.sort(function compareString(a, b) {
//                            if (isHtml) {
//                                var nameA = $(a[column]).html().toLowerCase();
//                                var nameB = $(b[column]).html().toLowerCase();
//                            } else {
                                var nameA = a[column].toLowerCase();
                                var nameB = b[column].toLowerCase();
                            //}
                            if (ascSort) {
                                if (nameA < nameB) { return -1 }
                                if (nameA > nameB) { return 1 }
                            } else {
                                if (nameA < nameB) { return 1 }
                                if (nameA > nameB) { return -1 }
                            }
                            return 0;
                        });
                    }

//                    if (dataList[0][columnIndex].tryParseInt() > -1) {

//                        dataList.sort(function compareTotals(a, b) {
//                            if (ascSort) {
//                                return b[columnIndex] - a[columnIndex];
//                            } else {
//                                return a[columnIndex] - b[columnIndex];
//                            }
//                        });

//                    } else {
//                        var isHtml = false;
//                        if ($(dataList[0][columnIndex]).html() != null) {
//                            isHtml = true;
//                        }
//                        dataList.sort(function compareString(a, b) {
//                            if (isHtml) {
//                                var nameA = $(a[columnIndex]).html().toLowerCase();
//                                var nameB = $(b[columnIndex]).html().toLowerCase();
//                            } else {
//                                var nameA = a[columnIndex].toLowerCase();
//                                var nameB = b[columnIndex].toLowerCase();
//                            }
//                            if (ascSort) {
//                                if (nameA < nameB) { return -1 }
//                                if (nameA > nameB) { return 1 }
//                            } else {
//                                if (nameA < nameB) { return 1 }
//                                if (nameA > nameB) { return -1 }
//                            }
//                            return 0;
//                        });
//                    }

                }

                //                function compareTotals(a, b) {
                //                    if (settings.sortOrder == 'asc') {
                //                        return b.total - a.total;
                //                    } else {
                //                        return a.to
                //                    }
                //                }

                //                function compareDates(a, b) {
                //                    var dateA = new Date(a.year, a.month, a.date);
                //                    var dateB = new Date(b.year, b.month, b.date);
                //                    return dateA - dateB;
                //                }

                //                function compareString(a, b) {
                //                    var nameA = a[0].toLowerCase();
                //                    var nameB = b[0].toLowerCase();

                //                    if (settings.sortOrder == 'asc') {
                //                        if (nameA < nameB) { return -1 }
                //                        if (nameA > nameB) { return 1 }
                //                    } else {
                //                        if (nameA < nameB) { return 1 }
                //                        if (nameA > nameB) { return -1 }
                //                    }
                //                    return 0;
                //                }

                function showRequiredButtons() {
                    if (totalpages > settings.length) {
                        if (currentPage > 0) { btnPrev.css('display', ''); }
                        else { btnPrev.css('display', 'none'); }
                        if (currentPage > settings.length / 2 - 1) { btnFirst.css('display', ''); }
                        else { btnFirst.css('display', 'none'); }

                        if (currentPage == totalpages - 1) { btnNext.css('display', 'none'); }
                        else btnNext.css('display', '');
                        if (totalpages > settings.length && currentPage < (totalpages - (settings.length / 2)) - 1) { btnLast.css('display', ''); }
                        else { btnLast.css('display', 'none'); };
                    }
                    else {
                        btnFirst.css('display', 'none');
                        btnPrev.css('display', 'none');
                        btnNext.css('display', 'none');
                        btnLast.css('display', 'none');
                    }
                }
                function isTextSelected(el) {
                    var startPos = el.get(0).selectionStart;
                    var endPos = el.get(0).selectionEnd;
                    var doc = document.selection;
                    if (doc && doc.createRange().text.length != 0) {
                        return true;
                    } else if (!doc && el.val().substring(startPos, endPos).length != 0) {
                        return true;
                    }
                    return false;
                }
            });
        }
    });
})(jQuery);