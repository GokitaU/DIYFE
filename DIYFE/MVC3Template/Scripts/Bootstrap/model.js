
function LogOnModel(UserName, Password, RememberMe, ReturnURL){
    this.UserName = UserName;
    this.Password = Password;
    this.RememberMe = RememberMe;
    this.ReturnURL = ReturnURL;
    this.SendFormat = sendJsonFormat;
    this.EasyLoad = function (formData) {
        easyLoad(this, formData);
    };
}


function Member(MemberID, Prefix, FirstName, MiddleName, LastName, Suffix, NickName, CompanyName, Title, CreatedDate, UpdatedDate, OptOut, Status, sysattendeeid, contactid) {
    this.MemberID = MemberID;
    this.Prefix = Prefix;
    this.FirstName = FirstName;
    this.MiddleName = MiddleName;
    this.LastName = LastName;
    this.Suffix = Suffix;
    this.NickName = NickName;
    this.CompanyName = CompanyName;
    this.Title = Title;
    this.CreatedDate = CreatedDate;
    this.UpdatedDate = UpdatedDate;
    this.OptOut = OptOut;
    this.Status = Status;
    this.sysattendeeid = sysattendeeid;
    this.contactid = contactid;
    this.MemberAddresses = [new Address()];
    this.MemberPhones = [new Phone()];
    this.MemberEmails = [new Email()];

    this.SendFormat = sendJsonFormat;
    this.EasyLoad = function (formData) {
        easyLoad(this, formData);
    };
}


//Global Function to 'stringigy' the object becuase jqueries .ajax post doesn't honor it's content type setting
//and doesn't send json formated data.
function sendJsonFormat() {
    return JSON.stringify(this);
}

//easy load function for models using form data.  
//For this to work the ID of the inputs must match this  property names in the model.
function easyLoad(obj, formData) {
    for (var i in obj) {
        //the  property has not been set so find it in form data
        if (obj[i] == undefined) {
            //MAYBE CHANGE THIS TO A CASE STATMENT FOR THE TYPE VALUE
            if (formData.find('#' + i).attr('type') == 'checkbox') {
                obj[i] = formData.find('#' + i).is(':checked').toString();
            } else {
                obj[i] = formData.find('#' + i).val();
            }
        }

        //object has another base object so run it's easyload function
        //this could be change to just do some recursion with it's self
        //...but this way mabye better if an object want custom code to be in it's easylaod
        if (obj[i] == 'object') {
            obj[i].EasyLoad(obj[i], formData);
        }
    }
}


//            //Update User Validation
//            if (!$('#fromUpdateMember').valid()) {
//                return;
//            }
//            //            alert($('#memberPhoneBusOptOut').is(':checked') + ' '
//            //            + $('#memberPhoneFaxOptOut').is(':checked') + ' '
//            //            + $('#memberPhoneCellOptOut').is(':checked') + ' '
//            //            + $('#memberEmailOptOut').is(':checked'));

//            var formData = $('#fromUpdateMember');

//            var model = new Member();

//            model.EasyLoad(formData);
//            model.MemberAddresses[0].EasyLoad(formData);
//            var busPhone = new Phone($('#memberPhoneBusID').val(), model.MemberID, 2, $('#memberPhoneBus').val(), $('#memberPhoneBusExt').val(),
//                                        true, $('#memberPhoneBusOptOut').is(':checked'), 1, $('#memberPhoneBusCreateDate').val());

//            var faxPhone = new Phone($('#memberPhoneFaxId').val(), model.MemberID, 12, $('#memberPhoneFax').val(), '',
//                                        true, $('#memberPhoneFaxOptOut').is(':checked'), 3, $('#memberPhoneFaxCreatedDate').val());

//            var cellPhone = new Phone($('#memberPhoneCellId').val(), model.MemberID, 3, $('#memberPhoneCell').val(), '',
//                                        true, $('#memberPhoneCellOptOut').is(':checked'), 4, $('#memberPhoneCellCreatedDate').val());

//            model.MemberPhones[0] = busPhone;
//            model.MemberPhones[1] = faxPhone;
//            model.MemberPhones[2] = cellPhone;

//            if ($('#memberEmailId').val() != '') {
//                model.MemberEmails[0] = new Email($('#memberEmailId').val(), model.MemberID, $('#memeberEmail').val(), 13, true, $('#memberEmailOptOut').is(':checked'), $('#memberEmailCreatedDate').val());

//            } // memberEmailid,          memberId,           emailAddress,         communicationTypeId, isPreferred, optOut
//            //alert(debug_ShowProps(model, 'Member', formData));
//            $.ajax({
//                url: '/Member/Update',
//                type: 'POST',
//                data: model.SendFormat(),
//                contentType: 'application/json; charset=utf-8',
//                success: function (data) {
//                    if (data.success) {
//                        //show Update

//                        if (data.newMember) {
//                            $('.success').html('Member Insert Successful...re-directing');

////                            var hasError = false;
////                            $('#mAddPropect tr').each(function () {
////                                if ($(this).find('input:checked').length > 0) {
////                                    if ($(this).find('select').val() != null) {
////                                        $.ajax({
////                                            url: '/Member/AddMemberRegCode?regCodeId=' + $(this).find('select').val() + '&memberId=' + data.memberId,
////                                            type: 'POST',
////                                            contentType: 'application/json; charset=utf-8',
////                                            success: function (data) {
////                                                if (data.success) {

////                                                } else {
////                                                    //show error
////                                                    alert(data.message);
////                                                }
////                                            },
////                                            error: function () {
////                                                alert("Connection or Return type Error - Load Show Year List");
////                                            }
////                                        });

////                                    }
////                                }

////                            });

//                            window.location = location.protocol + '//' + location.host + '/Member/details?memberid=' + data.memberId;
//                        }
//                        $('.success').show();

//                    } else {
//                        //show error
//                        $('.errorBox').html(data.message).show();
//                    }
//                },
//                error: function () {
//                    $('.errorBox').html('There was an error updating this member.<br>Please contact support').show();
//                }
//                ////You have to format and prepair data like is when not using a model
//                // //,
//                // data: JSON.stringify('{ MemberID: 500, Prefix: "mr", FirstName: "j", MiddleName: "m", LastName: "t", Suffix: "s", NickName: "a", CompanyName: "as", Title: "asdf", CreatedDate: "11/12/2011", UpdatedDate: "11/12/2011", OptOut: true, Status: "stat", sysattendeeid: 34, contactid: 5 }')
//            });
//        });