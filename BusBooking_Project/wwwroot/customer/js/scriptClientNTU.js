﻿$(document).ready(function () {

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

        var url = 'home/gettimeandroutes?fr=' + StationFrom.val() + '&to=' + StationTo.val() + '&dDate=' + StartDate.val() + '&QtyTicket=' + QtyTicket.val();
        document.location = url;
    })
})