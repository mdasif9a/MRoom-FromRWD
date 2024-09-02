$(document).ready(function () {
    $('#sidebar ul li a').each(function () {
        var path = location.href;
        if (this.href == location.href) {
            $(this).addClass('active');
            $(this).parent().parent('.collapse').addClass('show');
            $(this).parent().parent('.collapse').parent().children('.nav-link').addClass('active');
            $(this).parent().parent('.collapse').parent().children('.nav-link').removeClass('collapsed');
        }
    });
    $('#example3').DataTable({
        "dom": "<'row'<'col-md-4 mb-2'l><'col-md-4'B><'col-md-4 mb-2'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'mt-2 col-md-5'i><' mt-2 col-md-7'p>>",
        "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
        "pageLength": 10,
        "buttons": [{
            extend: "print",
            exportOptions: {
                columns: ':not(.no-print)'
            },
        }],
        "scrollX": true
    });
});