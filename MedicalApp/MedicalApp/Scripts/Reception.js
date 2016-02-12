$(document).ready(function () {
    $(function () {
        $('#searchPatient').click(function () {
            //var url = $('#modal-searchPatient').data('ur');
            var url = '/Administration/SearchPatient';
            $.get(url, function (data) {
                $('.modal-title').html('Search Patient');
                $('.modal-body').html(data);
                $('#modal').modal('show');
            });
        });

        $('#btnVisitList').click(function () {
            //var url = $('#modal-allVisits').data('url');
            var url = '/Administration/SearchVisit';
            $.get(url, function (data) {
                $('.modal-title').html('Search visits');
                $('.modal-body').html(data);
                $('#modal').modal('show');
            });
        });

        $('#newPatient').click(function () {
            //var url = $('#modal-newPatient').data('url');
            var url = '/Administration/NewPatient'
            $.get(url, function (data) {
                $('.modal-title').html('New Patient');
                $('.modal-body').html(data);
                $('#modal').modal('show');

            });
        });


        var modal = $('#modal');
        modal.on('click', '#btnSelect', function (e) {
            var patientId = $('#selectedPatientId').val();
            if (patientId != null) {
                $.ajax({
                    url: '/Administration/GetPatientById',
                    type: 'GET',
                    dataType: 'JSON',
                    data: { id: patientId },
                    success: function (data) {
                        $('#patientFirstName').val(data.FirstName);
                        $('#patientLastName').val(data.LastName);
                        $('#patientPesel').val(data.Pesel);
                        $('#patientId').val(data.Id),
                        $('#inputMedicalSpecializations').attr("disabled", false);
                    }
                });
            }
        });

        //modal.on('click', '#btnAddVisit', function () {
        //    //       var url = '/Administration/Reception'
        //    debugger;
        //    $.get(function (data) {
        //        $('.modal-title').html('Visit was added');
        //        $('.modal-body').html(data);
        //        $('#modal').modal('show');
        //    });
        //});

        $('#datepicker').datepicker({ minDate: 0 });

        function GetTodayDate() {
            var date = new Date();
            var dd = date.getDate(); //yields day
            var MM = date.getMonth(); //yields month
            var yyyy = date.getFullYear(); //yields year
            var today = (MM + 1) + "/" + dd + "/" + yyyy;
            return today;
        }



        $('#inputMedicalSpecializations').change(function () {

            if ($('#inputMedicalSpecializations').val() != "") {
                if ($('#inputDoctorName').val() != null) {

                    $('#datepicker').val(null);
                    $('#visiteTime option').remove();
                    $('#datepicker').attr("disabled", true);
                    $('#visiteTime').attr("disabled", true);
                }
                var spId = $('#inputMedicalSpecializations').val();
                $('#inputDoctorName').attr("disabled", false);
                $.ajax(
               {
                   url: '/Administration/GetDoctorBySpecializationId',
                   type: 'GET',
                   dataType: 'JSON',
                   data: {
                       specId: spId,
                   },
                   success: function (data) {
                       $('#inputDoctorName option').remove();
                       data = data.sort(function (a, b) {
                           return a.Value > b.Value;
                       });
                       data.forEach(function (doctor) {
                           $('#inputDoctorName').append($("<option></option>").attr("value", doctor.Value).text(doctor.Text));
                       });
                   }
               });
            }

            else {
                $('#inputDoctorName option').remove();
                $('#datepicker').val(null);
                $('#visiteTime option').remove();

                $('#inputDoctorName').attr("disabled", true);
                $('#datepicker').attr("disabled", true);
                $('#visiteTime').attr("disabled", true);
            }
        });



        $('#inputDoctorName').change(function () {
            if ($('#inputDoctorName').val() == "-1") {
                $('#datepicker').attr("disabled", true);
            }
            else {
                $('#datepicker').attr("disabled", false);
            }
            if ($('#datepicker').val() != "") {
                $('#datepicker').val(null);
                $('#visiteTime option').remove();
                $('#visiteTime').attr("disabled", true);
            }
        });



        $('#datepicker').change(function () {
            var date = $('#datepicker').val();
            var docID = $('#inputDoctorName').val();
            $('#visiteTime').attr("disabled", false);

            $.ajax(
                {
                    url: '/Administration/GetAvailbleTimeForDoctor',
                    type: 'GET',
                    dataType: 'JSON',
                    data: {
                        visiteDate: date,
                        doctorId: docID
                    },

                    success: function (data) {
                        $('#visiteTime option').remove();

                        data.forEach(function (time) {
                            $('#visiteTime').append($("<option></option>").attr("value", time.Value).text(time.Text));
                        });
                    }
                });
        });
    });
});