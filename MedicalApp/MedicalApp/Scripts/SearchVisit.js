$(document).ready(function () {
    $('#selectDate').datepicker();

    visitTable = $('#visitTable').dataTable({
        bFilter: false,
        bInfo: false,
        bLengthChange: false,
        "bServerSide": true,
        "bProcessing": true,
        "deferLoading": 0,
        "sAjaxSource": "/Administration/GetVisitsByFilter",
        "fnServerParams": function (aoData) {
            aoData.push({ "name": "DoctorName", "value": $("#selectDoctor").val()});
            aoData.push({ "name": "VisitDate", "value": $("#selectDate").val() });
        },
        "aoColumns": [
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<span>' + o.DoctorName + '</span>';
                            }
                        },
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<span>' + o.DoctorSpecialization + '</span>';
                            }
                        },
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<span>' + o.VisitDate + '</span>';
                            }
                        },
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<span>' + o.Description + '</span>';
                            }
                        },
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<span>' + o.PatientName + '</span>';
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


    $('#btnSearchVisit').click(function () {
        visitTable.fnDraw();
        $('#visitTableContainer').show();
    });

});





