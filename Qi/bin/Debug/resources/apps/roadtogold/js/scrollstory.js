var prevY = 0;
(function ($) {    
    $.fn.parallax = function (y1, y2, callback, data, yield) {
        console.log(arguments);
       var y = $(window).scrollTop() / 1;
       console.log(y);
       console.log(y1, y, y2);
        if (y >= y1 && y <= y2) {
            var delta = (y - y1) / (y2 - y1);
       //     console.log("Delta", delta);
            var power = y - prevY;
            if (delta <= .1 && power > 1) {
            
       //         $('.object').hide();
            }
            callback(delta, y - prevY);
            yield(data);/*
            if (delta >= .9 && power > 0) {
                $('.object').hide();
            }*/
            prevY = y;
        }
    }
    /**
     * Timeline function 
     * @param {Object} scrollY
     **/
    $.fn.story = function (data) {
        $(window).scroll(function () {
            var i = 0;
            $.each(data.items, function (i, epoch) {
                $('ul.timeline li').removeClass('active');
               $.fn.parallax(i * 1000, i * 1000 + 1000, epoch.callback, epoch, function (data2) {
                   $('li[data-number="' + i + '"]').addClass('active');
               }); 
               i++;
            });
        });
    };

})(jQuery);