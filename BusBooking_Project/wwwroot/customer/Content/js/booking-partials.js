function makeUpOption(arr) {
    return $.map(arr, function (o) {
        if (typeof o != "undefined" && o != null) {
            var option = "";
            switch (o.v) {
                //case "ANGIANG":
                //    option = "<option value='{0}'>{1}</option>".format(o.v, "An Giang (Châu Đốc, Long Xuyên)");
                //    break;
                //case "DONGTHAP":
                //    option = "<option value='{0}'>{1}</option>".format(o.v, "Đồng Tháp (Sa Đéc, Cao Lãnh, Hồng Ngự)");
                //    break;
                //case "HAUGIANG":
                //    option = "<option value='{0}'>{1}</option>".format(o.v, "Hậu Giang (Vị Thanh)");
                //    break;
                //case "KIENGIANG":
                //    option = "<option value='{0}'>{1}</option>".format(o.v, "Kiên Giang (Rạch Giá, Hà Tiên)");
                //    break;
                //case "CHAUDOC":
                //case "LONGXUYEN":
                //case "SADEC":
                //case "CAOLANH":
                //case "HONGNGU":
                //case "VITHANH":
                //case "RACHGIA":
                //case "HATIEN":
                //    break;
                default:
                    option = "<option value='{0}'>{1}</option>".format(o.v, o.t);
                    break;
            }
            //console.log(option);
            return option;
        }
        return "";
    });
}
function getRealTextOption(text) {
    text = text.trim();
    switch (text) {
        case "An Giang (Châu Đốc,Long Xuyên)":
            text = "An Giang";
            break;
        case "Đồng Tháp (Sa Đéc,Cao Lãnh,Hồng Ngự)":
            text = "Đồng Tháp";
            break;
        case "Hậu Giang (Vị Thanh)":
            text = "Hậu Giang";
            break;
        case "Kiên Giang (Rạch Giá,Hà Tiên)":
            text = "Kiên Giang";
            break;
        default:
            break;
    }
    //console.log(text);
    return text;
}
$(document).ready(function () {

    var lang = $("html")[0].lang;

    var fromPoints = [];
    //var fromPoints = {};
    var codeAffected = [];


    //for (var i = 0; i < routes.length; i++) {
    //    if (codeAffected.indexOf(routes[i].OriginCode) === -1) {

    //        fromPoints.push({
    //            v: routes[i].OriginCode,
    //            t: routes[i].OriginName
    //        });
    //        codeAffected.push(routes[i].OriginCode);
    //    }
    //}
    for (var i = 0; i < settings.RouteCode.length; i++) {
        if (codeAffected.indexOf(settings.RouteCode[i].OriginCode) === -1) {
            fromPoints.push({
                v: settings.RouteCode[i].OriginCode,
                t: settings.RouteCode[i].OriginName
            });
            codeAffected.push(settings.RouteCode[i].OriginCode);
        }
    }

    fromPoints.sort(function (a, b) {
        if (a.t < b.t)
            return -1;
        if (a.t > b.t)
            return 1;
        return 0;
    });
    //console.log(fromPoints);

    var origin = $("select[name=idOrigin]").empty();
    origin.append(makeUpOption(fromPoints));
    //origin.append(fromPoints);
    

    origin.on("change", function () {
        var vl = origin.val();
        var toPoints = [];
        codeAffected = [];
        //var routeAffected = [];

        for (var i = 0; i < settings.RouteCode.length; i++) {
            if (settings.RouteCode[i].OriginCode === vl) {
                if (codeAffected.indexOf(settings.RouteCode[i].DestCode) === -1) {
                    toPoints.push({
                        v: settings.RouteCode[i].DestCode,
                        t: settings.RouteCode[i].DestName
                    });
                    codeAffected.push(settings.RouteCode[i].DestCode);
                }
            }
        }

        //for (var i = 0; i < routes.length; i++) {
        //    if (routes[i].OriginCode === vl) {

        //        if (codeAffected.indexOf(routes[i].DestCode) === -1) {
        //            if (routeAffected.indexOf(routes[i].Id) === -1) {
        //                toPoints.push({
        //                    v: routes[i].DestCode,
        //                    t: routes[i].DestName
        //                });
        //                routeAffected.push(routes[i].Id);
        //            }
        //            codeAffected.push(routes[i].DestCode);
        //        }
        //    }
        //}

        //console.log(toPoints);

        toPoints.sort(function (a, b) {
            if (a.t < b.t)
                return -1;
            if (a.t > b.t)
                return 1;
            return 0;
        });

        //console.log(toPoints);

        var dest = $("select[name=idDest]").empty();
        dest.append(makeUpOption(toPoints));

        //dest.on("change", function () {
        //    var f = origin.val();
        //    var t = dest.val();
        //    var found = false;
        //    for (var i = 0; i < settings.RouteCode.length && !found; i++) {
        //        if (settings.RouteCode[i].OriginCode === f && settings.RouteCode[i].DestCode === t) {
        //            found = true;
        //            $("input[name=rId]").val(routes[i].Id);
        //        }
        //    }
        //});

        //dest.trigger("change");
        dest.selectpicker("refresh");

    });

    origin.trigger("change");

    $(".selectpicker").selectpicker();

    var startDate = moment(settings["ValidStartDateOffset"], "YYYY-MM-DDTHH:mm:ss.000");


    if (moment().diff(startDate, "day") >= 0) {
        startDate = moment();
        //.add(settings["ValidStartDateOffset"], "day")
    }
    //var startDate = moment().add(settings["ValidStartDateOffset"], "day");
    var endDate = moment(settings["ValidEndDate"], "YYYY-MM-DDTHH:mm:ss.000");

    $('input[name=numOfTicket]').attr('max', settings["MaxBookSeat"]);

    //console.log(startDate, endDate);

    switch (lang) {
        case "en-US":
            $("input[name=dDate]").datepicker({
                language: "en",
                format: "dd-mm-yyyy",
                startDate: startDate.toDate(),
                endDate: endDate.toDate(),
                autoclose: true
            });
            break;
        default:
            $("input[name=dDate]").datepicker({
                language: "vi",
                format: "dd-mm-yyyy",
                startDate: startDate.toDate(),
                endDate: endDate.toDate(),
                autoclose: true,
                weekStart: 1
            });
            break;
    }


    //$("input[name=dDate]").datepicker({
    //    language: "vi",
    //    format: "dd-mm-yyyy",
    //    startDate: startDate.toDate(),
    //    endDate: endDate.toDate(),
    //    weekStart: 1,
    //    autoclose: true
    //});

    $(".btn-booking").on("click", function (e) {
        //e.preventDefault();

        //Get value
        var fromPoint = getRealTextOption($("select[name=idOrigin] > option:selected").text());
        //console.log(fromPoint);
        //switch (fromPoint) {
        //    case "An Giang (Châu Đốc, Long Xuyên)":
        //        fromPoint = "An Giang";
        //        break;
        //    case "Đồng Tháp (Sa Đéc, Cao Lãnh, Hồng Ngự)":
        //        fromPoint = "Đồng Tháp";
        //        break;
        //    case "Hậu Giang (Vị Thanh)":
        //        fromPoint = "Hậu Giang";
        //        break;
        //    case "Kiên Giang (Rạch Giá, Hà Tiên)":
        //        fromPoint = "Kiên Giang";
        //        break;
        //    default:
        //        break;
        //}
        var toPoint = getRealTextOption($("select[name=idDest] > option:selected").text());
        var dDate = $("input[name=dDate]").val();
        var numOfTicket = $("input[name=numOfTicket]").val();
        var url;
        switch (lang) {
            case "en-US":
                url = "/en-US/buy-bus-ticket-from-{0}-to-{1}.html#/step1/{2}/{3}".format(slug(fromPoint), slug(toPoint), dDate, numOfTicket);
                break;
            default:
                url = "/vi-VN/mua-ve-xe-{0}-di-{1}.html#/step1/{2}/{3}".format(slug(fromPoint), slug(toPoint), dDate, numOfTicket);
                break;
        }

        dataLayer.push({
            'event': "productClick",
            'ecommerce': {
                'click': {
                    'actionField': { 'list': "{0} - {1}".format(fromPoint, toPoint) },      // Optional list property.
                    'products': [{
                        'name': "{0}-{1}-{2}".format("All", fromPoint, toPoint),                      // Name or ID is required.
                        'id': "{0}-{1}-{2} ⇒ {3}".format(dDate, "All", fromPoint, toPoint),
                        'brand': "Phương Trang",
                        'category': "{0} ⇒ {1}".format(fromPoint, toPoint)
                    }]
                }
            },
            'eventCallback': function () {
                document.location = url;
            }
        });

        //Redirect
        //location.href = url;
    });
});


