$(document).ready(function () {

    //  ====================================    Booking     ================================
    $('#booking .controls #cbbRouteFrom').on('change', function () {
        console.log($(this).val());
        if ($(this).val != "") {
            var idRouteFrom = parseInt($(this).val());
            $.ajax({
                type: 'post',
                url: '/home/getrouteto',
                contentType: 'application/json,charset=UTF-8',
                dataType: 'json',
                cache: false,
                data: JSON.stringify(idRouteFrom),
                success: function (data) {
                    if (data != "0") {
                        for (var i = 0; i < data.length; i++) {
                            if (i == 0) {
                                $('#booking .controls #cbbRouteTo').append('<option value="' + data[i].stationTo + '" selected>' + data[i].stationLocationTo + '</option>');
                            } else {
                                $('#booking .controls #cbbRouteTo').append('<option value="' + data[i].stationTo + '">' + data[i].stationLocationTo + '</option>');
                            }
                        }
                    }

                }
            })
        }
    })

    var setDateBooking = function () {
        var date = new Date();
        date.setDate(date.getDate() - 1);
        $('#booking .dDate').datepicker({
            minDate: date
        });
    }
    setDateBooking();

    $('#booking .pageselectSeats').on('click', function () {
        var StationFrom = $('select[name=cbbRouteFrom] > option:selected');
        var StationTo = $('select[name=cbbRouteTo] > option:selected');
        var StartDate = $('input[name=dDate]');
        var QtyTicket = $('input[name=numOfTicket]');
        if (StationFrom.val().trim() != '' && parseInt(StationTo.val().trim()) > 0 && StartDate.val().trim() != "") {
            var url;
            url = 'home/gettimeandroutes?fr=' + StationFrom.val() + '&to=' + StationTo.val() + '&dDate=' + StartDate.val() + '&QtyTicket=' + QtyTicket.val() + '&frname=' + StationFrom.text() + '&toname=' + StationTo.text();
            var currenturl = window.location.href.toLowerCase().trim();

            if (currenturl.indexOf('home') < 0) {
                document.location = url;
            } else {
                var d = document.location;
                d = d.href.replace('home/index', '');
                document.location = d + url;
            }
        }

    })


    $('#form-steps .timego').on('change', function () {
        var info_timego = $(this).children('option:selected').text().trim();
        var change_timego = $('#step-info table tbody tr:eq(0) td:eq(0) span').text(info_timego);

        var info_timerun = $('#step-info table tbody tr:eq(1) td:eq(0) p').text().split(' ')[40].trim();

        var timerun = new Date();
        timerun.setHours(parseInt(info_timego.split(':')[0]) + parseInt(info_timerun))
        timerun.setMinutes(info_timego.split(':')[1]);

        $('#step-info table tbody tr:eq(2) td span').text(timerun.getHours() + ':' + timerun.getMinutes() + ':00');


        var Timevalue = $(this).val();
        $.ajax({
            type: 'POST',
            url: '/selectedSeat/getlistseat',
            contentType: 'application/json,charset=UTF-8',
            dataType: 'json',
            cache: false,
            data: JSON.stringify(Timevalue),
            success: function (data) {

            }
        })
    })


    //======================== Function Booking ========================

})