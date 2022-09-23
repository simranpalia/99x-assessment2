$("body").on("click", "*[data-action='edit'], *[data-action='add'],[data-action='view']", function (e) {
    if (e.target.type == 'checkbox') return;
    e.preventDefault();
    var that = this;
    if (e.target.type == 'checkbox') return;
    e.preventDefault();

    var modalClass = $(this).data("class");
    var large = $(this).data("large");
    var disableBackgroundClose = $(this).data("disablebackgroundclose");

    if (!$('#edit_modal').length) {
        //create modal
        $('body').append('<div id="edit_modal" class="modal fade"><div class="modal-dialog ' + modalClass + '"><div class="modal-content"><div class="modal-header"><button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button><h4 class="modal-title">Edit</h4></div><div class="modal-body"><div id="modal_loader"><div class="alert alert-success"><strong>Loading...</strong></div></div><div id="modal_body"></div></div></div></div></div>');
    } else {
        var md = $("#edit_modal").find(".modal-dialog");
        if (md && !md.hasClass(modalClass))
            md.addClass(modalClass);
    }

    var url = $(this).data("target"), mb = $("#modal_body"), ml = $("#modal_loader"), em = $("#edit_modal");

    if ($(this).data("title"))
        em.find(".modal-title").empty().html($(this).data("title"));

    if (large) {
        var $md = $(em.find(".modal-dialog"));
        if ($md) $md.addClass("modal-lg");
    } else {
        var $md = $(em.find(".modal-dialog"));
        if ($md) $md.removeClass("modal-lg");
    }

    em.modal({ backdrop: 'static' });

    if ($(this).data("endparam")) {
        url = url + $(this).attr("data-endparam");
    }

    ml.show();
    mb.empty();

    $.ajax({
        type: "GET",
        url: url,
        cache: false,
        contentType: 'application/html;charset=utf-8',
        dataType: 'html',
        success: function (data) {
            mb.html(data);
        },
        complete: function () {
            ml.hide();
        },
        error: function (data) {
            mb.append("<div class='alert alert-danger'>" + data.status + " - " + data.statusText + "</div>");
        }
    });

});