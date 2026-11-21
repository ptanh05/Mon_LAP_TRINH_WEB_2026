// AssetHub - hiệu ứng giao diện cơ bản (nút "lên đầu trang")
$(function () {
    $(window).on('scroll', function () {
        $('.to-top').toggle($(this).scrollTop() > 200);
    });
    $('.to-top').on('click', function () {
        $('html, body').animate({ scrollTop: 0 }, 400);
    });
    $('.search-box input').on('focus', function () { $(this).select(); });

    $(document).on('click', '.nav-loai', function (e) {
        if ($('#main-content').length === 0) return;

        e.preventDefault();
        var id = $(this).data('id');

        $('.menu li').removeClass('active');
        $(this).closest('li').addClass('active');

        $.ajax({
            url: '/Home/FilterByLoai',
            type: 'GET',
            data: id ? { maLoai: id } : {},
            success: function (res) {
                $('#main-content').html(res);
            }
        });
    });
});
