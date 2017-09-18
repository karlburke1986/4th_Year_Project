$(function () {

    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {

        // hide dropdown if any
        $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');


        $('#myModalContent').load(this.href, function () {


            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');

            bindForm(this);
        });

        return false;
    });


});

$(function () {
    var submitButton = $("#submitButton");
    var infoForm = $("#infoForm");

    submitButton.click(function () {
        SubmitInfo(infoForm);
    });
});

function SubmitInfo(formContainer) {
    $.ajax({
        url: "Account/Register",
        type: 'post',
        data: formContainer.serialize(),
        success: function (data) {
            if (data.IsSuccess) {
                formContainer.find("input[type='text']").each(function (i, element) {
                    $(this).val('');
                });
            }
            alert(data.Message);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

//function bindForm(dialog) {

//    $('form', dialog).submit(function () {
//        $.ajax({
//            url: this.action,
//            type: this.method,
//            data: $(this).serialize(),
//            success: function (result) {
//                if (result.success) {
//                    $('#myModal').modal('hide');
//                    //Refresh
//                    location.reload();
//                } else {
//                    $('#myModalContent').html(result);
//                    bindForm();
//                }
//            }
//        });
//        return false;
//    });
//}