$(function () {
    $('.btndelete').off('click').click(function (e) {
        e.preventDefault();
        var name = $(this).data('name');
        if (confirm('Bạn muốn xoá sách ' + name)) {
            var id = $(this).data('id');
            $.ajax({
                url: "/Book/Delete",
                data: JSON.stringify({ id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "POST",
                success: function (data) {
                    if (data.status == "1") {
                        showToast(`Xoá sách ${name} thành công.`);
                        $('#Book_' + id).hide(200);
                        $('#Book_' + id).addClass('check');
                        var check = true;
                        var child = $('.table-book tbody').children('tr');
                        child.each(function () {
                            if(!$(this).hasClass('check'))                            
                            {
                                check=false;
                                return false;
                            }
                        })
                        console.log(check);
                        if (check == true) {
                            $('.table-book').hide(200);
                            $('.noti').show(400);
                            $('.btnadd').hide(200);
                        }
                    }
                    else
                        showToast(`Xoá sách ${name} thất bại`);
                },
                error: function (data) {

                    alert(JSON.stringify(data));
                }
            })
        }
    })
})
