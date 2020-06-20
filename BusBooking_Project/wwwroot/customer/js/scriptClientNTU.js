$(document).ready(function () {

    //  ====================================    Booking     ================================

    //  Event change Station From
    $('#booking .controls #cbbRouteFrom').on('change', function () {

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
                        $('#booking .controls #cbbRouteTo option').remove();
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
    var eventSelectSeat = function () {
        var seats = [];
        var selectedSeat = [];
        $('.seat').click(function (e) {
            if ($(this).hasClass('selected') == false && $(this).hasClass('choosed') == false && $('#content-steps > div > div.col-sm-8.col-xs-12.col-ms-12 > div > div:nth-child(2) > div > table > tbody tr').find('.selected').length == 0) {
                $(this).addClass("selected");
                e.preventDefault();
                seats.push($(this).attr('data-id'));
                selectedSeat.push($(this).text());
                $('.ng-show').text(selectedSeat.toString());

            }
            else if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');

                seats.splice($.inArray($(this).attr('data-id'), seats), 1);
                selectedSeat.splice($.inArray($(this).text(), selectedSeat), 1);
                $('.ng-show').text(selectedSeat.toString());

            }
        });
    }
    //  Event load page select Seat
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


    eventSelectSeat();

    //  Event change time go in page select Seat
    $('#form-steps .timego').on('change', function () {

        // thoi gian di duoc chon
        var info_timego = $(this).children('option:selected').text().trim();

        // thay doi thoi gian di cho trip info
        var change_timego = $('#step-info table tbody tr:eq(0) td:eq(0) span').text(info_timego);

        // thoi gian chay
        var info_timerun = $('#step-info table tbody tr:eq(1) td:eq(0) p').text().split(' ')[52].trim();

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
                var contain = $('#content-steps > div > div.col-sm-8.col-xs-12.col-ms-12 > div > div:nth-child(2) > div > table > tbody');
                contain.children('tr').remove();
                for (var i = 0; data.length != 0 || i <= data.length; i++) {
                    var arr = []
                    var str = '';
                    str += '<tr>';

                    if (data.length > 4) {
                        arr = data.splice(0, 4);
                    } else if (data.length <= 4) {
                        arr = data.splice(0, data.length);
                    }

                    for (var j = 0; j < arr.length; j++) {
                        str += '<td>';
                        str += '<div class="seat" id="' + arr[j].id + '">' + arr[j].code + '</div>';
                        str += '</td>';
                    }
                    str += '</tr>';
                    contain.append(str);
                }
                eventSelectSeat();
            }
        })
    })

    //  Event choose seat in page select seat
    //var eventSelectSeat = function () {
    //    var seats = [];
    //    var selectedSeat = [];
    //    $('.seat').click(function (e) {
    //        if ($(this).hasClass('selected') == false && $(this).hasClass('choosed') == false) {
    //            $(this).addClass("selected");
    //            e.preventDefault();
    //            seats.push($(this).attr('data-id'));
    //            selectedSeat.push($(this).text());
    //            $('.ng-show').text(selectedSeat.toString());
    //            console.log(seats);
    //        }
    //        else if ($(this).hasClass('selected')) {
    //            $(this).removeClass('selected');

    //            seats.splice($.inArray($(this).attr('data-id'), seats), 1);
    //            selectedSeat.splice($.inArray($(this).text(), selectedSeat), 1);
    //            $('.ng-show').text(selectedSeat.toString());
    //            console.log(seats);
    //        }
    //    });
    //}
    //eventSelectSeat();

    $('#customerPage').on('click', function () {
        if ($('#content-steps > div > div.col-sm-8.col-xs-12.col-ms-12 > div > div:nth-child(2) > div > table > tbody tr').find('.selected').length > 0) {
            var seatid = $('#content-steps > div > div.col-sm-8.col-xs-12.col-ms-12 > div > div:nth-child(2) > div > table > tbody tr').find('.selected').attr('id');
            var CatePrice = $("#form-steps > fieldset > div:nth-child(3) > div > div > div > select option:selected").html().split(' - ')[1].replace('VND', '').trim();
            var RoutePrice = $("#form-steps > fieldset > div:nth-child(2) > div > div > div > p > span.f-right > span").text().replace('₫', '').trim();
            var RouteId = $('#form-steps .timego').val();

            var origin = window.location.origin;

            window.location.assign(origin + '/customerinfo/index?seat=' + seatid + '&cateprice=' + CatePrice + '&routeprice=' + RoutePrice + '&routeid=' + RouteId);
        }

    });


})