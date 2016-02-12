$(document).ready(function () {

    table = $('#myDataTable').dataTable({
        bFilter: false,
        bInfo: false,
        bLengthChange: false,
        "bServerSide": true,
        "bProcessing": true,
        "deferLoading": 0,
        "sAjaxSource": "/Administration/GetPatientListByFilter",
        "fnServerParams": function (aoData) {
            aoData.push({ "name": "FirstName", "value": $("#searchFirstName").val() });
            aoData.push({ "name": "LastName", "value": $("#searchLastName").val() });
            aoData.push({ "name": "Pesel", "value": $("#searchPesel").val() });
        },
        "aoColumns": [
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<input type="checkbox" class="cb" onclick=checkCheckbox(' + o.Id + ') value=' + o.Id + '>';
                            }
                        },
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<span>' + o.FirstName + '</span>';
                            }
                        },
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<span>' + o.LastName + '</span>';
                            }
                        },
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<span>' + o.Pesel + '</span>';
                            }
                        },
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<span>' + o.City + '</span>';
                            }
                        }

        ]
    });
});

function checkCheckbox(currentId) {
    $(".cb:checked").each(function () {
        var id = $(this).val()
        if (id != currentId) {
            $(this).prop("checked", false);
        }
    });
}

$('button#btnSearch').click(function () {
    table.fnDraw();
    $("#tableContainer").show();
});

$('#btnSelect').click(function () {
    if ($("input:checked").length === 0) {
        alert('Please select patient');
        return;
    }

    $("input:checked", table.fnGetNodes()).each(function () {
        $('#selectedPatientId').val($(this).val());
    });

});

