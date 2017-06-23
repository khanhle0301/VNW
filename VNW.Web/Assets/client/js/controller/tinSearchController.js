Mustache.Formatters = {
    "date": function (dt) {
        var lpad = Mustache.Formatters.lpad,
            day = lpad(dt.getDate(), 2, "0"),
            month = lpad(dt.getMonth() + 1, 2, "0");
        return day + "/" + month + "/" + dt.getFullYear();
    }
};

var timConfig = {
    pageSize: 2,
    pageIndex: 1,
}
var timSearchController = {
    init: function () {
        timSearchController.loadData();
        timSearchController.registerEvent();
    },
    registerEvent: function () {

        $('.nganhnghe').off('click').on('click', function () {
            timSearchController.loadData(true);
        });

        $('.diadiem').off('click').on('click', function () {
            timSearchController.loadData(true);
        });

        $('.kynang').off('click').on('click', function () {
            timSearchController.loadData(true);
        });

        $('.capbac').off('click').on('click', function () {
            timSearchController.loadData(true);
        });

        $("input[name=jobSalary]:radio").change(function () {
            timSearchController.loadData(true);
        });

        $(".clickable").change(function () {
            $(".clickable").removeClass('active');
            $(this).addClass('active');
            //alert(this.value);
        });

    },

    getDate: function () {
        var date = new Date(),
          year = date.getFullYear(),
          month = (date.getMonth() + 1).toString(),
          formatedMonth = (month.length === 1) ? ("0" + month) : month,
          day = date.getDate().toString(),
          formatedDay = (day.length === 1) ? ("0" + day) : day;
        return formatedDay + "/" + formatedMonth + "/" + year;
    },

    loadData: function (changePageSize) {

        var keyword = $("#txtKeyword").val();

        var industry = $("#cateListMainSearch").val();

        var location = $("#locationMainSearch").val();

        var sort = 'date';

        var nganhnghe = '';
        $(".nganhnghe:checked").each(function () {
            nganhnghe += $(this).val() + ",";
        });

        nganhnghe = nganhnghe.slice(0, -1);

        var diadiem = '';
        $(".diadiem:checked").each(function () {
            diadiem += $(this).val() + ",";
        });
        diadiem = diadiem.slice(0, -1);

        var kynang = '';
        $(".kynang:checked").each(function () {
            kynang += $(this).val() + ",";
        });
        kynang = kynang.slice(0, -1);

        var capbac = '';
        $(".capbac:checked").each(function () {
            capbac += $(this).val() + ",";
        });
        capbac = capbac.slice(0, -1);

        var mucluong = $('input:radio[name=jobSalary]:checked').val();

        var isAuthenticated = $('#isAuthenticated').val();

        var dateNow = timSearchController.getDate();

        $.ajax({
            url: '/TinTuyenDung/LoadData',
            type: 'GET',
            data: {
                keyword: keyword,
                industry: industry,
                location: location,
                sort: sort,
                nganhnghe: nganhnghe,
                diadiem: diadiem,
                kynang: kynang,
                capbac: capbac,
                mucluong: mucluong,
                page: timConfig.pageIndex,
                pageSize: timConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            Id: item.Id,
                            Url: '/' + item.Alias + '-' + item.Id + '.html',
                            TenCongTy: item.TenCongTy,
                            Logo: item.Logo,
                            ChucDanh: item.ChucDanh,
                            DiaChi: '#',
                            NgayDang: item.NgayDang,
                            isAuthenticated: (isAuthenticated != 1) ? true : false,
                            thuongluong: (item.HienThiLuong == true) ? true : false,
                            TuLuong: item.TuLuong,
                            DenLuong: item.DenLuong,
                            homnay: (item.NgayDang == dateNow) ? true : false,
                        });

                    });
                    if (html == '') {
                        $('#countTin').hide();
                        $('#pagination-container').hide();
                        $('#tblData').html('<div id="no-results-message"><h2 class="text-center text-lg">Hiện không có công việc nào theo tiêu chí này. Bạn hãy thử tìm lại với từ khoá rộng hơn.</h2></div>');
                    }
                    else {
                        $('#pagination-container').show();
                        $('#countTin').html('<strong class="text-primary">Có ' + response.total + '</strong> công việc');
                        $('#tblData').html(html);

                        timSearchController.paging(response.total, function () {
                            timSearchController.loadData();

                        }, changePageSize);
                    }
                    //$("#top").trigger("click");
                    timSearchController.registerEvent();
                }
            }
        })
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / timConfig.pageSize);
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "«",
            next: "›",
            last: "»",
            prev: "‹",
            visiblePages: 10,
            onPageClick: function (event, page) {
                timConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
timSearchController.init();

