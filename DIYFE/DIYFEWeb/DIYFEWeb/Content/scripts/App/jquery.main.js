//init page
//jQuery.noConflict();



function setupAccordion() {
    $(".open-close").click(function () {
        var parentBlock = $(this).closest(".slideblock");
        var classList = parentBlock.attr('class');
        var allClasses = [];
        allClasses = classList.split(' ');
        var active = false;
        for (i in allClasses) {
            if (allClasses[i] == 'active') {
                active = true
            }
        }
        if (active == true) {
            $(parentBlock).find('.slide').slideUp('fast', function () {
                $(parentBlock).removeClass('active')
            })
        } else {
            $(parentBlock).find('.slide').slideDown('fast', function () {
                $(parentBlock).addClass('active')
            })
        }
        return false
    });
}

// init sub-navigation bar
function initSubnav() {
    var browserVersion = navigator.uaMatch(navigator.userAgent);

    if ($('.subnav').length > 0) {
        var subnavLinks = $(".nav-frame>ul>li>a");
        var activeItem = $(".nav-frame ul .selected").index();
        $('.submenu:eq(' + activeItem + ')').show();
        subnavLinks.each(function () {
            var a = $(this);
            $(a).mouseover(function () {
                $(".nav-frame ul li").removeClass('active');
                var browserVersion = navigator.uaMatch(navigator.userAgent);
                if (browserVersion.browser = 'msie' && parseFloat(browserVersion.version) < 9) {
                    $(".nav-frame ul li a").removeClass('pie_first-child');
                }
                $(this).parent().addClass('active');
                var b = $(a).attr("rel");
                $(".submenu").hide();
                $("#" + b + "").show();
                $('.btn-register').removeClass('active');
                $('body').bind("mousemove", watchPos);
            })
        });
        function watchPos(a) {
            var b = $(".subnav").offset(), d = b.left, c = b.top, b = d + $(".subnav").outerWidth(), c = c + $(".subnav").outerHeight();
            if (a.pageX < d || a.pageX > b || a.pageY > c || a.pageY < $(".nav-frame").offset().top) {
                $(".submenu").hide();
                $('.submenu:eq(' + activeItem + ')').show();
                $(".nav-frame ul li").removeClass('active');
                $(".nav-frame ul .selected").addClass('active');
                var browserVersion = navigator.uaMatch(navigator.userAgent);
                if (browserVersion.browser = 'msie' && parseFloat(browserVersion.version) < 9) {
                    $(".nav-frame ul li:not(.selected) a").removeClass('pie_first-child');
                    $(".nav-frame ul .selected").addClass('active')
                }
                $('body').unbind('mousemove', watchPos);
            }
        }
    }
} 

// accordion function
function initAccordion() {
    jQuery('ul.accordion').multiAccordion({
        activeClass: 'active',
        opener: '>a.opener',
        slider: '>div.slide',
        collapsible: false,
        slideSpeed: 500,
        event: 'click'
    });
}

// tabs init
function initTabs() {
    jQuery('ul.tabset').jqueryTabs({
        addToParent: true,
        holdHeight: false,
        activeClass: 'active',
        tabLinks: 'a.tab',
        fadeSpeed: 0
    })
}

// modal plugin -- http://fancybox.net/api
function initModal() {
    if ($.fancybox) {
        $(".modal-trigger").fancybox({
            'overlayColor': '#fff',
            'transitionIn': 'none',
            'transitionOut': 'none',
            'titleShow': false,
            'centerOnScroll': true
        });
    }
}
/*  ellipsis init -- http://dotdotdot.frebsite.nl/ */

function initEllipsis() {
    // simple ellipsis
    jQuery("#ellipsis1").dotdotdot({
        height: 30 /*	Optionally set a max-height, if null, the height will be measured. */
    });

    // use a 'read more' link to expand the original content
    jQuery("#ellipsis2").dotdotdot({ height: 50 }).append('<a href="#" class="ellipsis-readmore" title="read more">Read more &#187;</a> ');

    // bind and event to 'read more' link
    jQuery(".ellipsis-readmore").bind('click', function () {
        var parentEllipse = jQuery(this).closest(".jq-ellipsis");
        parentEllipse.trigger("destroy");
        parentEllipse.find(".ellipsis-readmore").css({ display: 'none' });
        return false
    });

    // when content is truncated do something, like changing the visibility
    jQuery(".jq-ellipsis").trigger("isTruncated", function (isTruncated) {
        if (isTruncated) {
            jQuery(".jq-ellipsis").css({ 'visibility': 'visible' })
        }
    });
}

// tooltips init
function initTooltips() {
    var links = jQuery('a.tooltip');
    links.each(function () {
        var link = jQuery(this);
        if (link.hasClass('tooltip-onclick')) {
            link.jqueryTooltips({
                clickClass: 'tooltip-onclick',
                event: 'click'
            })
        }
        else {
            link.jqueryTooltips({
                clickClass: 'tooltip-onclick',
                event: 'hover'
            })
        }
    });
}

// flyout init
function initFlyouts() {
    var lis = $('.flyouts>li');
    lis.each(function () {
        var link = $(this);
        $(link).mouseover(function () {
            closeAllFlyouts();
            $('.filter-list-show').removeClass('filter-list-show');
            $(this).addClass('filter-list-show');
            var rel = $(link).attr('rel');
            var target = $("#" + rel + "");
            if ($(this).parent().hasClass('fly-right') == true) {
                var targetLeft = $(this).offset().left + $(this).outerWidth() + 6;
            } else {
                var targetLeft = $(this).offset().left - 6 - $(target).outerWidth();
                $('.flyout-arrow-left').removeClass('flyout-arrow-left').addClass('flyout-arrow-right');
            }
            var targetTop = $(this).offset().top + $(this).outerHeight() / 2 - 32;
            var bodyHeight = $('body').outerHeight();
            var targetHeight = $(target).closest('.flyout-master').outerHeight();
            if (targetTop + targetHeight > bodyHeight) {
                targetTop = bodyHeight - targetHeight;
                $(target).find('.flyout-arrow-left,.flyout-arrow-right').css({
                    top: 'auto',
                    bottom: (bodyHeight - $(this).offset().top) - $(this).outerHeight() + 6
                })
            }

            // .flyout-master
            $(target).css({
                left: targetLeft,
                top: targetTop
            })
            $(target).show();
            $(window).resize(hideFlyer);
            $('body').bind('click', function (e) {
                if (!$(e.target).parents("#" + rel).length) {
                    hideFlyer()
                }
            })
            function hideFlyer() {
                $(target).hide();
                $('.filter-list-show').removeClass('filter-list-show');
            }
            return false
        });
    });
}
function closeAllFlyouts() {
    var lis = $('.flyouts>li');
    lis.each(function () {
        var link = $(this);
        var rel = $(link).attr('rel');
        $("#" + rel + "").hide();
    })
    $('.filter-list-show').removeClass('filter-list-show');
}
// jquery tooltips plugin
jQuery.fn.jqueryTooltips = function (_options) {
    // default options
    var _options = jQuery.extend({
        helperBox: 'tooltip-helper',
        clickClass: 'onclick',
        event: 'click'
    }, _options);

    return this.each(function () {
        var _link = jQuery(this);
        var _helperBox = jQuery('.' + _options.helperBox);
        var _event = _options.event;
        var _delay = 300;
        if (!_helperBox.length) {
            _helperBox = jQuery('<div>').addClass(_options.helperBox).appendTo('body');
            _helperBox.css({
                position: 'absolute',
                top: 0,
                left: 0,
                zIndex: 1000,
                display: 'none'
            })
        }
        var heightArrow = 7;
        var _timer;

        var win = jQuery(window);
        var leftCenterClass = 'left-center-position';
        var rightCenterClass = 'right-center-position';
        var leftTopClass = 'left-top-position';
        var leftBottomClass = 'left-bottom-position';
        var rightTopClass = 'right-top-position';
        var rightBottomClass = 'right-bottom-position';

        var topCenterClass = 'top-center-position';
        var topLeftClass = 'top-left-position';
        var topRightClass = 'top-right-position';
        var bottomCenterClass = 'bottom-center-position';
        var bottomLeftClass = 'bottom-left-position';
        var bottomRightClass = 'bottom-right-position';


        function showTooltip(_obj) {
            _helperBox.empty().show();
            var tooltip = jQuery('#' + _obj.attr('rel'));
            if (tooltip.length) {
                tooltip.clone().appendTo(_helperBox);
                positionTooltip();
            }
        }

        win.resize(function () {
            hideTooltip();
        });

        function positionTooltip() {
            //var _top = _link.offset().top - _helperBox.height() - heightArrow;
            var top = _link.find("img").length > 0 ? _link.find("img").offset().top : _link.offset().top;
            var _top = top - _helperBox.height() - heightArrow;
            var _left = _link.offset().left + (_link.outerWidth() - _helperBox.width()) / 2 + _helperBox.width();            

            var _centerClass = bottomCenterClass;
            var _leftClass = bottomLeftClass;
            var _rightClass = bottomRightClass;

            if (_top < win.scrollTop()) {
                _top = _link.offset().top + _link.outerHeight() + heightArrow;
                _centerClass = topCenterClass;
                _leftClass = topLeftClass;
                _rightClass = topRightClass;
            }

            if (_left - win.scrollLeft() < win.width() && _left - _helperBox.width() - win.scrollLeft() > 0) {
                _helperBox.children().eq(0).addClass(_centerClass);
                _helperBox.css({
                    top: _top,
                    left: _left - _helperBox.width()
                })
            }
            else if (_left - win.scrollLeft() < win.width() && _left - _helperBox.width() - win.scrollLeft() < 0) {
                _helperBox.children().eq(0).addClass(_leftClass);
                _helperBox.css({
                    top: _top,
                    left: win.scrollLeft()
                })
            }
            else {
                _helperBox.children().eq(0).addClass(_rightClass);
                _helperBox.css({
                    top: _top,
                    left: win.scrollLeft() + win.width() - _helperBox.outerWidth()
                })
            }
        }

        function hideTooltip(tooltipId) {
            // Stop processing if tooltipId defined but isn't exists in tooltip container
            if (tooltipId && !isTooltipShown(tooltipId, _helperBox))
                return; 

           	if (_timer) clearTimeout(_timer);
           	_helperBox.hide().empty();
           	jQuery('body').unbind('click');
           	_helperBox.unbind('mouseenter mouseleave');
        }

        function isTooltipShown(tooltipId, $container) {
            var $tooltip = $container.find('#' + tooltipId);
            return $tooltip.length > 0;
        }

        if (_event == 'hover') {
            _link.hover(function () {
                closeAllFlyouts();
                if (_timer) clearTimeout(_timer);
                showTooltip(_link);
                _helperBox.bind('mouseenter', function () {
                    clearTimeout(_timer);
                }).bind('mouseleave', function () {
                    clearTimeout(_timer);
                    // Get current tooltip id
                    var tooltipId = $(this).find('.tooltip-box:first').attr('id');
                    _timer = setTimeout(function() { hideTooltip(tooltipId) }, _delay);
                })
            }, function () {
                clearTimeout(_timer);
                // Get current tooltip id
                var tooltipId = $(this).attr('rel');
                _timer = setTimeout(function() { hideTooltip(tooltipId) }, _delay);
            });

        }
        else if (_event == 'click') {
            _link.bind(_event, function () {
                closeAllFlyouts();
                _helperBox.unbind('mouseenter mouseleave');
                showTooltip(_link);
                var btnsClose = _helperBox.find('a.close-link');
                btnsClose.each(function () {
                    var btn = jQuery(this);
                    btn.bind('click', function () {
                        hideTooltip();
                        return false;
                    })
                });
                jQuery('body').bind('click', function (e) {
                    if (!jQuery(e.target).parents('.' + _options.helperBox).length) {
                        hideTooltip();
                    }
                })
                return false;
            })
        }
    });
}

// jquery tabs plugin
jQuery.fn.jqueryTabs = function (_options) {
    // default options
    var _options = jQuery.extend({
        addToParent: false,
        holdHeight: false,
        activeClass: 'active',
        tabLinks: 'a.tab',
        fadeSpeed: 300,
        event: 'click'
    }, _options);

    return this.each(function () {
        var _holder = jQuery(this);
        var _fadeSpeed = _options.fadeSpeed;
        var _activeClass = _options.activeClass;
        var _addToParent = _options.addToParent;
        var _holdHeight = _options.holdHeight;
        var _tabLinks = jQuery(_options.tabLinks, _holder);
        var _tabset = (_addToParent ? _tabLinks.parent() : _tabLinks);
        var _event = _options.event;
        var _animating = false;

        // tabs init
        _tabLinks.each(function () {
            var _tmpLink = jQuery(this);
            var _tmpTab = jQuery(_tmpLink.attr('href'));
            var _classItem = (_addToParent ? _tmpLink.parent() : _tmpLink);
            if (_tmpTab.length) {
                if (_classItem.hasClass(_activeClass)) _tmpTab.show();
                else _tmpTab.hide();
            }
        });

        // tab switcher
        function switchTab(_switcher) {
            if (!_animating) {
                var _link = jQuery(_switcher);
                var _newItem = (_addToParent ? _link.parent() : _link);
                var _newTab = jQuery(_link.attr('href'));
                if (_newItem.hasClass(_activeClass)) return;

                var _oldItem = jQuery(_addToParent ? _tabset : _tabLinks).filter('.' + _activeClass);
                var _oldTab = jQuery(jQuery(_addToParent ? _oldItem.children('a') : _oldItem).attr('href'));
                if (_newTab.length) {
                    _animating = true;
                    if (_oldItem.length) {
                        _newItem.addClass(_activeClass);
                        _oldItem.removeClass(_activeClass);

                        var _parent = _oldTab.parent();
                        if (_holdHeight) _parent.css({ height: _parent.height() });

                        _oldTab.fadeOut(_fadeSpeed, function () {
                            _newTab.fadeIn(_fadeSpeed, function () {
                                _animating = false;
                            });
                            if (_holdHeight) _parent.css({ height: 'auto' });
                        });
                    } else {
                        _newItem.addClass(_activeClass);
                        _newTab.fadeIn(_fadeSpeed, function () {
                            _animating = false;
                        });
                    }
                }
            }
        }

        // control
        _tabLinks.each(function () {
            jQuery(this).bind(_event, function (e) {
                switchTab(this);
                e.preventDefault();
            });
        });
    });
}

// multilevel accordion plugin
jQuery.fn.multiAccordion = function (_options) {
    // default options
    var _options = jQuery.extend({
        activeClass: 'active',
        opener: '.opener',
        slider: '.slide',
        slideSpeed: 400,
        collapsible: true,
        event: 'click'
    }, _options);

    return this.each(function () {
        // options
        var _event = _options.event;
        var _accordion = jQuery(this);
        var _items = _accordion.find(':has(' + _options.slider + ')');

        _items.each(function () {
            var _holder = jQuery(this);
            var _opener = _holder.find(_options.opener);
            var _slider = _holder.find(_options.slider);
            _opener.bind(_event, function () {
                if (!_slider.is(':animated')) {
                    if (_holder.hasClass(_options.activeClass)) {
                        if (_options.collapsible) {
                            _slider.slideUp(_options.slideSpeed, function () {
                                _holder.removeClass(_options.activeClass);
                            });
                        }
                    } else {
                        var _levelItems = _holder.siblings('.' + _options.activeClass);
                        _holder.addClass(_options.activeClass);
                        _slider.slideDown(_options.slideSpeed);

                        // collapse others
                        _levelItems.find(_options.slider).slideUp(_options.slideSpeed, function () {
                            _levelItems.removeClass(_options.activeClass);
                        })
                    }
                }
                return false;
            });
            if (_holder.hasClass(_options.activeClass)) _slider.show(); else _slider.hide();
        });
    });
}


// clear inputs on focus
function initInputs() {

    PlaceholderInput = function () {
        this.options = {
            element: null,
            showUntilTyping: false,
            wrapWithElement: false,
            getParentByClass: false,
            placeholderAttr: 'value',
            inputFocusClass: 'focus',
            inputActiveClass: 'text-active',
            parentFocusClass: 'parent-focus',
            parentActiveClass: 'parent-active',
            labelFocusClass: 'label-focus',
            labelActiveClass: 'label-active',
            fakeElementClass: 'input-placeholder-text'
        }
        this.init.apply(this, arguments);
    }
    PlaceholderInput.convertToArray = function (collection) {
        var arr = [];
        for (var i = 0, ref = arr.length = collection.length; i < ref; i++) {
            arr[i] = collection[i];
        }
        return arr;
    }
    PlaceholderInput.getInputType = function (input) {
        return (input.type ? input.type : input.tagName).toLowerCase();
    }
    PlaceholderInput.prototype = {
        init: function (opt) {
            this.setOptions(opt);
            if (this.element && this.element.PlaceholderInst) {
                this.element.PlaceholderInst.refreshClasses();
            } else {
                this.element.PlaceholderInst = this;
                if (this.elementType == 'text' || this.elementType == 'password' || this.elementType == 'textarea') {
                    this.initElements();
                    this.attachEvents();
                    this.refreshClasses();
                }
            }
        },
        setOptions: function (opt) {
            for (var p in opt) {
                if (opt.hasOwnProperty(p)) {
                    this.options[p] = opt[p];
                }
            }
            if (this.options.element) {
                this.element = this.options.element;
                this.elementType = PlaceholderInput.getInputType(this.element);
                this.wrapWithElement = (this.elementType === 'password' || this.options.showUntilTyping ? true : this.options.wrapWithElement);
                this.setOrigValue(this.options.placeholderAttr == 'value' ? this.element.defaultValue : this.element.getAttribute(this.options.placeholderAttr));
            }
        },
        setOrigValue: function (value) {
            this.origValue = value;
        },
        initElements: function () {
            // create fake element if needed
            if (this.wrapWithElement) {
                this.element.value = '';
                this.element.removeAttribute(this.options.placeholderAttr);
                this.fakeElement = document.createElement('span');
                this.fakeElement.className = this.options.fakeElementClass;
                this.fakeElement.innerHTML += this.origValue;
                this.fakeElement.style.color = getStyle(this.element, 'color');
                this.fakeElement.style.position = 'absolute';
                this.element.parentNode.insertBefore(this.fakeElement, this.element);
            }
            // get input label
            if (this.element.id) {
                this.labels = document.getElementsByTagName('label');
                for (var i = 0; i < this.labels.length; i++) {
                    if (this.labels[i].htmlFor === this.element.id) {
                        this.labelFor = this.labels[i];
                        break;
                    }
                }
            }
            // get parent node (or parentNode by className)
            this.elementParent = this.element.parentNode;
            if (typeof this.options.parentByClass === 'string') {
                var el = this.element;
                while (el.parentNode) {
                    if (hasClass(el.parentNode, this.options.parentByClass)) {
                        this.elementParent = el.parentNode;
                        break;
                    } else {
                        el = el.parentNode;
                    }
                }
            }
        },
        attachEvents: function () {
            this.element.onfocus = bindScope(this.focusHandler, this);
            this.element.onblur = bindScope(this.blurHandler, this);
            if (this.options.showUntilTyping) {
                this.element.onkeydown = bindScope(this.typingHandler, this);
                this.element.onpaste = bindScope(this.typingHandler, this);
            }
            if (this.wrapWithElement) this.fakeElement.onclick = bindScope(this.focusSetter, this);
        },
        togglePlaceholderText: function (state) {
            if (this.wrapWithElement) {
                this.fakeElement.style.display = state ? '' : 'none';
            } else {
                this.element.value = state ? this.origValue : '';
            }
        },
        focusSetter: function () {
            this.element.focus();
        },
        focusHandler: function () {
            this.focused = true;
            if (!this.element.value.length || this.element.value === this.origValue) {
                if (!this.options.showUntilTyping) {
                    this.togglePlaceholderText(false);
                }
            }
            this.refreshClasses();
        },
        blurHandler: function () {
            this.focused = false;
            if (!this.element.value.length || this.element.value === this.origValue) {
                this.togglePlaceholderText(true);
            }
            this.refreshClasses();
        },
        typingHandler: function () {
            setTimeout(bindScope(function () {
                if (this.element.value.length) {
                    this.togglePlaceholderText(false);
                    this.refreshClasses();
                }
            }, this), 10);
        },
        refreshClasses: function () {
            this.textActive = this.focused || (this.element.value.length && this.element.value !== this.origValue);
            this.setStateClass(this.element, this.options.inputFocusClass, this.focused);
            this.setStateClass(this.elementParent, this.options.parentFocusClass, this.focused);
            this.setStateClass(this.labelFor, this.options.labelFocusClass, this.focused);
            this.setStateClass(this.element, this.options.inputActiveClass, this.textActive);
            this.setStateClass(this.elementParent, this.options.parentActiveClass, this.textActive);
            this.setStateClass(this.labelFor, this.options.labelActiveClass, this.textActive);
        },
        setStateClass: function (el, cls, state) {
            if (!el) return; else if (state) addClass(el, cls); else removeClass(el, cls);
        }
    }

    // utility functions
    function hasClass(el, cls) {
        return el.className ? el.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)')) : false;
    }
    function addClass(el, cls) {
        if (!hasClass(el, cls)) el.className += " " + cls;
    }
    function removeClass(el, cls) {
        if (hasClass(el, cls)) { el.className = el.className.replace(new RegExp('(\\s|^)' + cls + '(\\s|$)'), ' '); }
    }
    function bindScope(f, scope) {
        return function () { return f.apply(scope, arguments) }
    }
    function getStyle(el, prop) {
        if (document.defaultView && document.defaultView.getComputedStyle) {
            return document.defaultView.getComputedStyle(el, null)[prop];
        } else if (el.currentStyle) {
            return el.currentStyle[prop];
        } else {
            return el.style[prop];
        }
    }


    // replace options
    var opt = {
        clearInputs: true,
        clearTextareas: true,
        clearPasswords: true
    }
    // collect all items
    var inputs = [].concat(
		PlaceholderInput.convertToArray(document.getElementsByTagName('input')),
		PlaceholderInput.convertToArray(document.getElementsByTagName('textarea'))
	);
    // apply placeholder class on inputs
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].className.indexOf('default') < 0) {
            var inputType = PlaceholderInput.getInputType(inputs[i]);
            if ((opt.clearInputs && inputType === 'text') ||
				(opt.clearTextareas && inputType === 'textarea') ||
				(opt.clearPasswords && inputType === 'password')
			) {
                new PlaceholderInput({
                    element: inputs[i],
                    wrapWithElement: false,
                    showUntilTyping: false,
                    getParentByClass: false,
                    placeholderAttr: 'value'
                });
            }
        }
    }
}


// custom forms script
var maxVisibleOptions = 20;
var all_selects = false;
var active_select = null;
var selectText = "please select";

function initCustomForms() {
    getElements();
    separateElements();
    replaceRadios();
    replaceCheckboxes();
    replaceSelects();

    // hide drop when scrolling or resizing window
    if (window.addEventListener) {
        window.addEventListener("scroll", hideActiveSelectDrop, false);
        window.addEventListener("resize", hideActiveSelectDrop, false);
    }
    else if (window.attachEvent) {
        window.attachEvent("onscroll", hideActiveSelectDrop);
        window.attachEvent("onresize", hideActiveSelectDrop);
    }
}

function refreshCustomForms() {
    // remove prevously created elements
    if (window.inputs) {
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) { checkboxes[i]._ca.className = "checkboxAreaChecked"; }
            else { checkboxes[i]._ca.className = "checkboxArea"; }
        }
        for (var i = 0; i < radios.length; i++) {
            if (radios[i].checked) { radios[i]._ra.className = "radioAreaChecked"; }
            else { radios[i]._ra.className = "radioArea"; }
        }
        for (var i = 0; i < selects.length; i++) {
            var newText = document.createElement('div');
            if (selects[i].options[selects[i].selectedIndex].title.indexOf('image') != -1) {
                newText.innerHTML = '<img src="' + selects[i].options[selects[i].selectedIndex].title + '" alt="" />';
                newText.innerHTML += '<span>' + selects[i].options[selects[i].selectedIndex].text + '</span>';
            } else {
                newText.innerHTML = selects[i].options[selects[i].selectedIndex].text;
            }
            var elem = document.getElementById("mySelectText" + i);
            if(elem)
                elem.innerHTML = newText.innerHTML;
        }
    }
}

// getting all the required elements
function getElements() {
    // remove prevously created elements
    if (window.inputs) {
        try{
            for (var i = 0; i < inputs.length; i++) {
                inputs[i].className = inputs[i].className.replace('outtaHere', '');
                if (inputs[i]._ca) inputs[i]._ca.parentNode.removeChild(inputs[i]._ca);
                else if (inputs[i]._ra) inputs[i]._ra.parentNode.removeChild(inputs[i]._ra);
            }
        }
        catch(e){
            console.log(e);
        }

        try{
            for (i = 0; i < selects.length; i++) {
                selects[i].replaced = null;
                selects[i].className = selects[i].className.replace('outtaHere', '');
                selects[i]._optionsDiv._parent.parentNode.removeChild(selects[i]._optionsDiv._parent);
                selects[i]._optionsDiv.parentNode.removeChild(selects[i]._optionsDiv);
            }
        }
        catch (e) {
            console.log(e);
        }
    }

    // reset state
    inputs = new Array();
    selects = new Array();
    labels = new Array();
    radios = new Array();
    radioLabels = new Array();
    checkboxes = new Array();
    checkboxLabels = new Array();
    for (var nf = 0; nf < document.getElementsByTagName("form").length; nf++) {
        if (document.forms[nf].className.indexOf("default") < 0) {
            for (var nfi = 0; nfi < document.forms[nf].getElementsByTagName("input").length; nfi++) { inputs.push(document.forms[nf].getElementsByTagName("input")[nfi]); }
            for (var nfl = 0; nfl < document.forms[nf].getElementsByTagName("label").length; nfl++) { labels.push(document.forms[nf].getElementsByTagName("label")[nfl]); }
            for (var nfs = 0; nfs < document.forms[nf].getElementsByTagName("select").length; nfs++) { selects.push(document.forms[nf].getElementsByTagName("select")[nfs]); }
        }
    }
}

// separating all the elements in their respective arrays
function separateElements() {
    var r = 0; var c = 0; var t = 0; var rl = 0; var cl = 0; var tl = 0; var b = 0;
    for (var q = 0; q < inputs.length; q++) {
        if (inputs[q].type == "radio") {
            radios[r] = inputs[q]; ++r;
            for (var w = 0; w < labels.length; w++) {
                if ((inputs[q].id) && labels[w].htmlFor == inputs[q].id) {
                    radioLabels[rl] = labels[w];
                    ++rl;
                }
            }
        }
        if (inputs[q].type == "checkbox") {
            checkboxes[c] = inputs[q]; ++c;
            for (var w = 0; w < labels.length; w++) {
                if ((inputs[q].id) && (labels[w].htmlFor == inputs[q].id)) {
                    checkboxLabels[cl] = labels[w];
                    ++cl;
                }
            }
        }
    }
}
//replacing radio buttons
function replaceRadios() {
    for (var q = 0; q < radios.length; q++) {
        radios[q].className += " outtaHere";
        var radioArea = document.createElement("div");
        if (radios[q].checked) {
            radioArea.className = "radioAreaChecked";
        }
        else {
            radioArea.className = "radioArea";
        }
        radioArea.id = "myRadio" + q;
        radios[q].parentNode.insertBefore(radioArea, radios[q]);
        radios[q]._ra = radioArea;

        radioArea.onclick = new Function('rechangeRadios(' + q + ')');
        if (radioLabels[q]) {
            if (radios[q].checked) {
                radioLabels[q].className += "radioAreaCheckedLabel";
            }
            radioLabels[q].onclick = new Function('rechangeRadios(' + q + ')');
        }
    }
    return true;
}

//checking radios
function checkRadios(who) {
    var what = radios[who]._ra;
    for (var q = 0; q < radios.length; q++) {
        if ((radios[q]._ra.className == "radioAreaChecked") && (radios[q]._ra.nextSibling.name == radios[who].name)) {
            radios[q]._ra.className = "radioArea";
        }
    }
    what.className = "radioAreaChecked";
}

//changing radios
function changeRadios(who) {
    if (radios[who].checked) {
        for (var q = 0; q < radios.length; q++) {
            if (radios[q].name == radios[who].name) {
                radios[q].checked = false;
            }
            radios[who].checked = true;
            checkRadios(who);
        }
    }
}

//rechanging radios
function rechangeRadios(who) {
    if (!radios[who].checked) {
        for (var q = 0; q < radios.length; q++) {
            if (radios[q].name == radios[who].name) {
                radios[q].checked = false;
                if (radioLabels[q]) radioLabels[q].className = radioLabels[q].className.replace("radioAreaCheckedLabel", "");
            }
        }
        radios[who].checked = true;
        if (radioLabels[who] && radioLabels[who].className.indexOf("radioAreaCheckedLabel") < 0) {
            radioLabels[who].className += " radioAreaCheckedLabel";
        }
        checkRadios(who);

        if (window.$ && window.$.fn) {
            $(radios[who]).trigger('change');
        }
    }
}

//replacing checkboxes
function replaceCheckboxes() {
    for (var q = 0; q < checkboxes.length; q++) {
        checkboxes[q].className += " outtaHere";
        var checkboxArea = document.createElement("div");
        if (checkboxes[q].checked) {
            checkboxArea.className = "checkboxAreaChecked";
            if (checkboxLabels[q]) {
                checkboxLabels[q].className += " checkboxAreaCheckedLabel"
            }
        }
        else {
            checkboxArea.className = "checkboxArea";
        }
        checkboxArea.id = "myCheckbox" + q;
        checkboxes[q].parentNode.insertBefore(checkboxArea, checkboxes[q]);
        checkboxes[q]._ca = checkboxArea;
        checkboxArea.onclick = new Function('rechangeCheckboxes(' + q + ')');
        if (checkboxLabels[q]) {
            checkboxLabels[q].onclick = new Function('changeCheckboxes(' + q + ')');
        }
        checkboxes[q].onkeydown = checkEvent;
    }
    return true;
}

//checking checkboxes
function checkCheckboxes(who, action) {
    var what = checkboxes[who]._ca;
    if (action == true) {
        what.className = "checkboxAreaChecked";
        what.checked = true;
    }
    if (action == false) {
        what.className = "checkboxArea";
        what.checked = false;
    }
    if (checkboxLabels[who]) {
        if (checkboxes[who].checked) {
            if (checkboxLabels[who].className.indexOf("checkboxAreaCheckedLabel") < 0) {
                checkboxLabels[who].className += " checkboxAreaCheckedLabel";
            }
        } else {
            checkboxLabels[who].className = checkboxLabels[who].className.replace("checkboxAreaCheckedLabel", "");
        }
    }

}

//changing checkboxes
function changeCheckboxes(who) {
    setTimeout(function () {
        if (checkboxes[who].checked) {
            checkCheckboxes(who, true);
        } else {
            checkCheckboxes(who, false);
        }
    }, 10);
}

// rechanging checkboxes
function rechangeCheckboxes(who) {
    var tester = false;
    if (checkboxes[who].checked == true) {
        tester = false;
    }
    else {
        tester = true;
    }
    checkboxes[who].checked = tester;
    checkCheckboxes(who, tester);
    if (window.$ && window.$.fn) {
        $(checkboxes[who]).trigger('change');
    }
}

// check event
function checkEvent(e) {
    if (!e) var e = window.event;
    if (e.keyCode == 32) { for (var q = 0; q < checkboxes.length; q++) { if (this == checkboxes[q]) { changeCheckboxes(q); } } } //check if space is pressed
}

// replace selects
function replaceSelects() {
    for (var q = 0; q < selects.length; q++) {
        if (!selects[q].replaced && selects[q].offsetWidth) {
            selects[q]._number = q;
            //create and build div structure
            var selectArea = document.createElement("div");
            var left = document.createElement("span");
            left.className = "left";
            selectArea.appendChild(left);

            var disabled = document.createElement("span");
            disabled.className = "disabled";
            selectArea.appendChild(disabled);

            selects[q]._disabled = disabled;
            var center = document.createElement("span");
            var button = document.createElement("a");
            var text = document.createTextNode(selectText);
            center.id = "mySelectText" + q;

            var stWidth = selects[q].offsetWidth;
            selectArea.style.width = stWidth + "px";
            if (selects[q].parentNode.className.indexOf("type2") != -1) {
                button.href = "javascript:showOptions(" + q + ",true)";
            } else {
                button.href = "javascript:showOptions(" + q + ",false)";
            }
            button.className = "selectButton";
            selectArea.className = "selectArea";
            selectArea.className += " " + selects[q].className;
            selectArea.id = "sarea" + q;
            center.className = "center";
            center.appendChild(text);
            selectArea.appendChild(center);
            selectArea.appendChild(button);

            //insert select div
            selects[q].parentNode.insertBefore(selectArea, selects[q]);
            //build & place options div

            var optionsDiv = document.createElement("div");
            var optionsList = document.createElement("ul");
            var optionsListHolder = document.createElement("div");

            optionsListHolder.className = "select-center";
            optionsListHolder.innerHTML = "<div class='select-center-right'></div>";
            optionsDiv.innerHTML += "<div class='select-top'><div class='select-top-left'></div><div class='select-top-right'></div></div>";

            optionsListHolder.appendChild(optionsList);
            optionsDiv.appendChild(optionsListHolder);

            selects[q]._optionsDiv = optionsDiv;
            selects[q]._options = optionsList;

            optionsDiv.style.width = stWidth + "px";
            optionsDiv._parent = selectArea;

            optionsDiv.className = "optionsDivInvisible";
            optionsDiv.id = "optionsDiv" + q;

            if (selects[q].className.length) {
                optionsDiv.className += ' drop-' + selects[q].className;
            }

            populateSelectOptions(selects[q]);
            optionsDiv.innerHTML += "<div class='select-bottom'><div class='select-bottom-left'></div><div class='select-bottom-right'></div></div>";
            document.body.appendChild(optionsDiv);
            selects[q].replaced = true;

            // too many options
            if (selects[q].options.length > maxVisibleOptions) {
                optionsDiv.className += ' optionsDivScroll';
            }

            //hide the select field
            if (selects[q].className.indexOf('default') != -1) {
                selectArea.style.display = 'none';
            } else {
                selects[q].className += " outtaHere";
            }
        }
    }
    all_selects = true;
}

//collecting select options
function populateSelectOptions(me) {
    me._options.innerHTML = "";
    for (var w = 0; w < me.options.length; w++) {
        var optionHolder = document.createElement('li');
        var optionLink = document.createElement('a');
        var optionTxt;
        if (me.options[w].title.indexOf('image') != -1) {
            optionTxt = document.createElement('img');
            optionSpan = document.createElement('span');
            optionTxt.src = me.options[w].title;
            optionSpan = document.createTextNode(me.options[w].text);
        } else {
            optionTxt = document.createTextNode(me.options[w].text);
        }

        // hidden default option
        if (me.options[w].className.indexOf('default') != -1) {
            optionHolder.style.display = 'none'
        }

        optionLink.href = "javascript:showOptions(" + me._number + "); selectMe('" + me.id + "'," + w + "," + me._number + ");";
        if (me.options[w].title.indexOf('image') != -1) {
            optionLink.appendChild(optionTxt);
            optionLink.appendChild(optionSpan);
        } else {
            optionLink.appendChild(optionTxt);
        }
        optionHolder.appendChild(optionLink);
        me._options.appendChild(optionHolder);
        //check for pre-selected items
        if (me.options[w].selected) {
            selectMe(me.id, w, me._number, true);
        }
    }
    if (me.disabled) {
        me._disabled.style.display = "block";
    } else {
        me._disabled.style.display = "none";
    }
}

//selecting me
function selectMe(selectFieldId, linkNo, selectNo, quiet) {
    selectField = selects[selectNo];
    for (var k = 0; k < selectField.options.length; k++) {
        if (k == linkNo) {
            selectField.options[k].selected = true;
        }
        else {
            selectField.options[k].selected = false;
        }
    }

    //show selected option
    textVar = document.getElementById("mySelectText" + selectNo);
    var newText;
    var optionSpan;
    if (selectField.options[linkNo].title.indexOf('image') != -1) {
        newText = document.createElement('img');
        newText.src = selectField.options[linkNo].title;
        optionSpan = document.createElement('span');
        optionSpan = document.createTextNode(selectField.options[linkNo].text);
    } else {
        newText = document.createTextNode(selectField.options[linkNo].text);
    }
    if (selectField.options[linkNo].title.indexOf('image') != -1) {
        if (textVar.childNodes.length > 1) textVar.removeChild(textVar.childNodes[0]);
        textVar.replaceChild(newText, textVar.childNodes[0]);
        textVar.appendChild(optionSpan);
    } else {
        if (textVar.childNodes.length > 1) textVar.removeChild(textVar.childNodes[0]);
        textVar.replaceChild(newText, textVar.childNodes[0]);
    }
    if (!quiet && all_selects) {
        if (typeof selectField.onchange === 'function') {
            selectField.onchange();
        }
        if (window.$ && window.$.fn) {
            $(selectField).trigger('change');
        }
    }
}

//showing options
function showOptions(g) {
    _elem = document.getElementById("optionsDiv" + g);
    var divArea = document.getElementById("sarea" + g);
    if (active_select && active_select != _elem) {
        active_select.className = active_select.className.replace('optionsDivVisible', ' optionsDivInvisible');
        active_select.style.height = "auto";
    }
    if (_elem.className.indexOf("optionsDivInvisible") != -1) {
        _elem.style.left = "-9999px";
        _elem.style.top = findPosY(divArea) + divArea.offsetHeight + 'px';
        _elem.className = _elem.className.replace('optionsDivInvisible', '');
        _elem.className += " optionsDivVisible";
        /*if (_elem.offsetHeight > 200)
        {
        _elem.style.height = "200px";
        }*/
        _elem.style.left = findPosX(divArea) + 'px';

        active_select = _elem;
        if (_elem._parent.className.indexOf('selectAreaActive') < 0) {
            _elem._parent.className += ' selectAreaActive';
        }

        if (document.documentElement) {
            document.documentElement.onclick = hideSelectOptions;
        } else {
            window.onclick = hideSelectOptions;
        }
    }
    else if (_elem.className.indexOf("optionsDivVisible") != -1) {
        hideActiveSelectDrop();
    }

    // for mouseout
    /*_elem.timer = false;
    _elem.onmouseover = function() {
    if (this.timer) clearTimeout(this.timer);
    }
    _elem.onmouseout = function() {
    var _this = this;
    this.timer = setTimeout(function(){
    _this.style.height = "auto";
    _this.className = _this.className.replace('optionsDivVisible','');
    if (_elem.className.indexOf('optionsDivInvisible') == -1)
    _this.className += " optionsDivInvisible";
    },200);
    }*/
}

function hideActiveSelectDrop() {
    if (active_select) {
        active_select.style.height = "auto";
        active_select.className = active_select.className.replace('optionsDivVisible', '');
        active_select.className = active_select.className.replace('optionsDivInvisible', '');
        active_select._parent.className = active_select._parent.className.replace('selectAreaActive', '')
        active_select.className += " optionsDivInvisible";
        active_select = false;
    }
}

function hideSelectOptions(e) {
    if (active_select) {
        if (!e) e = window.event;
        var _target = (e.target || e.srcElement);
        if (!isElementBefore(_target, 'selectArea') && !isElementBefore(_target, 'optionsDiv')) {
            hideActiveSelectDrop();
            if (document.documentElement) {
                document.documentElement.onclick = function () { };
            }
            else {
                window.onclick = null;
            }
        }
    }
}

function isElementBefore(_el, _class) {
    var _parent = _el;
    do {
        _parent = _parent.parentNode;
    }
    while (_parent && _parent.className != null && _parent.className.indexOf(_class) == -1)
    return _parent.className && _parent.className.indexOf(_class) != -1;
}

function findPosY(obj) {
    if (obj.getBoundingClientRect) {
        var scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop;
        var clientTop = document.documentElement.clientTop || document.body.clientTop || 0;
        return Math.round(obj.getBoundingClientRect().top + scrollTop - clientTop);
    } else {
        var posTop = 0;
        while (obj.offsetParent) { posTop += obj.offsetTop; obj = obj.offsetParent; }
        return posTop;
    }
}

function findPosX(obj) {
    if (obj.getBoundingClientRect) {
        var scrollLeft = window.pageXOffset || document.documentElement.scrollLeft || document.body.scrollLeft;
        var clientLeft = document.documentElement.clientLeft || document.body.clientLeft || 0;
        return Math.round(obj.getBoundingClientRect().left + scrollLeft - clientLeft);
    } else {
        var posLeft = 0;
        while (obj.offsetParent) { posLeft += obj.offsetLeft; obj = obj.offsetParent; }
        return posLeft;
    }
}

// iepp v2.1pre @jon_neal & @aFarkas github.com/aFarkas/iepp
// html5shiv @rem remysharp.com/html5-enabling-script
// Dual licensed under the MIT or GPL Version 2 licenses
; (function () {
    /*@cc_on(function (a, b) { function r(a) { var b = -1; while (++b < f) a.createElement(e[b]) } if (!window.attachEvent || !b.createStyleSheet || !function () { var a = document.createElement("div"); return a.innerHTML = "<elem></elem>", a.childNodes.length !== 1 } ()) return; a.iepp = a.iepp || {}; var c = a.iepp, d = c.html5elements || "abbr|article|aside|audio|bdi|canvas|data|datalist|details|figcaption|figure|footer|header|hgroup|mark|meter|nav|output|progress|section|subline|summary|time|video", e = d.split("|"), f = e.length, g = new RegExp("(^|\\s)(" + d + ")", "gi"), h = new RegExp("<(/*)(" + d + ")", "gi"), i = /^\s*[\{\}]\s*$/, j = new RegExp("(^|[^\\n]*?\\s)(" + d + ")([^\\n]*)({[\\n\\w\\W]*?})", "gi"), k = b.createDocumentFragment(), l = b.documentElement, m = b.getElementsByTagName("script")[0].parentNode, n = b.createElement("body"), o = b.createElement("style"), p = /print|all/, q; c.getCSS = function (a, b) { try { if (a + "" === undefined) return "" } catch (d) { return "" } var e = -1, f = a.length, g, h = []; while (++e < f) { g = a[e]; if (g.disabled) continue; b = g.media || b, p.test(b) && h.push(c.getCSS(g.imports, b), g.cssText), b = "all" } return h.join("") }, c.parseCSS = function (a) { var b = [], c; while ((c = j.exec(a)) != null) b.push(((i.exec(c[1]) ? "\n" : c[1]) + c[2] + c[3]).replace(g, "$1.iepp-$2") + c[4]); return b.join("\n") }, c.writeHTML = function () { var a = -1; q = q || b.body; while (++a < f) { var c = b.getElementsByTagName(e[a]), d = c.length, g = -1; while (++g < d) c[g].className.indexOf("iepp-") < 0 && (c[g].className += " iepp-" + e[a]) } k.appendChild(q), l.appendChild(n), n.className = q.className, n.id = q.id, n.innerHTML = q.innerHTML.replace(h, "<$1font") }, c._beforePrint = function () { if (c.disablePP) return; o.styleSheet.cssText = c.parseCSS(c.getCSS(b.styleSheets, "all")), c.writeHTML() }, c.restoreHTML = function () { if (c.disablePP) return; n.swapNode(q) }, c._afterPrint = function () { c.restoreHTML(), o.styleSheet.cssText = "" }, r(b), r(k); if (c.disablePP) return; m.insertBefore(o, m.firstChild), o.media = "print", o.className = "iepp-printshim", a.attachEvent("onbeforeprint", c._beforePrint), a.attachEvent("onafterprint", c._afterPrint) })(this, document)@*/
})();

/* dotdotdot - ellipsis plugin */
(function (e) {
    function s(a, c, k, b) { var d = a.contents(), h = !1; a.empty(); for (var f = 0, i = d.length; f < i && !h; f++) { var g = d[f], j = e(g); if ("undefined" != typeof g) { a.append(j); if (b) { var t = a.is("table, thead, tbody, tfoot, tr, col, colgroup, object, embed, param, ol, ul, dl, select, optgroup, option, textarea, script, style") ? "after" : "append"; a[t](b) } 3 == g.nodeType ? c.innerHeight() > k.maxHeight && (h = r(j, c, k, b)) : h = s(j, c, k, b); h || b && b.remove() } } return h } function r(a, c, e, b) {
        var d = !1, h = a[0]; if ("undefined" == typeof h) return !1;
        var f = "letter" == e.wrap ? "" : " ", i = o(h).split(f); q(h, i.join(f) + e.ellipsis); for (var g = i.length - 1; 0 <= g; g--) if (c.innerHeight() > e.maxHeight) { var j = o(h).length - (i[g].length + f.length + e.ellipsis.length), j = 0 < j ? o(h).substring(0, j) : ""; q(h, j + e.ellipsis) } else { d = !0; break } d || (d = a.parent(), a.remove(), $n = d.contents().eq(-1), d = r($n, c, e, b)); return d
    } function n(a) { return { width: a.innerWidth(), height: a.innerHeight()} } function q(a, c) { a.innerText ? a.innerText = c : a.nodeValue ? a.nodeValue = c : a.textContent && (a.textContent = c) } function o(a) {
        return a.innerText ?
a.innerText : a.nodeValue ? a.nodeValue : a.textContent ? a.textContent : ""
    } function u(a, c) { return "undefined" == typeof a || !a ? !1 : "string" == typeof a ? (a = e(a, c), a.length ? a : !1) : "object" == typeof a ? "undefined" == typeof a.jquery ? !1 : a : !1 } function v(a, c) { if (!a) return !1; c = "string" == typeof c ? "dotdotdot: " + c : ["dotdotdot:", c]; window.console && window.console.log && window.console.log(c); return !1 } if (!e.fn.dotdotdot) {
        e.fn.dotdotdot = function (a) {
            var c, k; if (0 == this.length) return v(!0, 'No element found for "' + this.selector + '".'), this;
            if (1 < this.length) return this.each(function () { e(this).dotdotdot(a) }); var b = this, d = this[0]; b.data("dotdotdot") && b.trigger("destroy.dot"); b.bind_events = function () {
                b.bind("update.dot", function (a, g) {
                    a.preventDefault(); a.stopPropagation(); var d = f, i; if ("number" == typeof f.height) i = f.height; else { i = b.innerHeight(); var m = ["paddingTop", "paddingBottom"]; z = 0; for (l = m.length; z < l; z++) { var p = parseInt(b.css(m[z])); isNaN(p) && (p = 0); i -= p } } d.maxHeight = i; f.maxHeight += f.tolerance; if ("undefined" != typeof g) {
                        if ("string" == typeof g ||
g instanceof HTMLElement) g = e("<div />").append(g).contents(); g instanceof e && (h = g)
                    } j.empty(); j.append(h.clone(!0)); d = m = !1; c && (m = c.clone(!0), c.remove()); if (j.innerHeight() > f.maxHeight) if ("children" == f.wrap) { d = j; i = f; var p = d.children(), o = !1; d.empty(); for (var n = 0, r = p.length; n < r; n++) { var q = p.eq(n); d.append(q); m && d.append(m); if (d.innerHeight() > i.maxHeight) { q.remove(); o = !0; break } else m && m.remove() } d = o } else d = s(j, j, f, m); return k = d
                }); b.bind("isTruncated.dot", function (a, b) {
                    a.preventDefault(); a.stopPropagation();
                    "function" == typeof b && b.call(d, k); return k
                }); b.bind("originalContent.dot", function (a, b) { a.preventDefault(); a.stopPropagation(); "function" == typeof b && b.call(d, h); return h }); b.bind("destroy.dot", function (a) { a.preventDefault(); a.stopPropagation(); b.unwatch(); b.unbind_events(); b.empty(); b.append(h); b.data("dotdotdot", !1) })
            }; b.unbind_events = function () { b.unbind(".dot") }; b.watch = function () {
                b.unwatch(); "window" == f.watch ? e(window).bind("resize.dot", function () {
                    g && clearInterval(g); g = setTimeout(function () { b.trigger("update.dot") },
10)
                }) : (i = n(b), g = setInterval(function () { var a = n(b); if (i.width != a.width || i.height != a.height) b.trigger("update.dot"), i = n(b) }, 100))
            }; b.unwatch = function () { g && clearInterval(g) }; var h = b.contents(), f = e.extend(!0, {}, e.fn.dotdotdot.defaults, a); k = c = void 0; var i = {}, g = null, j = b.wrapInner("<" + f.wrapper + ' class="dotdotdot" />').children(); c = u(f.after, j); k = !1; j.css({ height: "auto", width: "auto" }); b.data("dotdotdot", !0); b.bind_events(); b.trigger("update.dot"); f.watch && b.watch(); return b
        }; e.fn.dotdotdot.defaults = { wrapper: "div",
            ellipsis: "... ", wrap: "word", tolerance: 0, after: null, height: null, watch: !1, debug: !1
        }; var w = e.fn.html; e.fn.html = function (a) { return "string" == typeof a && this.data("dotdotdot") ? (this.trigger("update", a), this) : w.call(this, a) }; var x = e.fn.text; e.fn.text = function (a) { if ("string" == typeof a && this.data("dotdotdot")) { var c = e("<div />"); c.text(a); a = c.html(); c.remove(); this.trigger("update", a); return this } return x.call(this, a) } 
    }
})(jQuery);

jQuery(function () {
    initSubnav();
    initInputs();
    initCustomForms();
    initFlyouts();
    initTooltips();
    initAccordion();
    initTabs();
    initModal();
    initEllipsis();
});
