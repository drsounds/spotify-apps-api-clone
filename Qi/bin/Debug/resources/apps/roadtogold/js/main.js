$(window).story({
    'items':[{
        'callback': function (delta, power) {  
             
            $('.object').hide();
            $('#heading').html('The recruitment rocket starts');
            $('#subheading').html('Im selected to the first phase of Spotify recruitment');
             
            $('#rocket').show();
            $('#rocket').css({'bottom': (20 + delta * 800 ) + 'px'});
         }
     },
     {
         'callback': function (delta, power) {  
             
            console.log("DELTA", delta);
            $('.object').hide();
            $('#heading').html('Escaping the journy');
            $('#subheading').html('I\'m landing in Thailand mentally and meet joy');
            $('#meteor').show();
            $('#palm').show();
            $('#palm').css({'bottom': - 500 + ( delta * 500 ) + 'px'});
            $('#sea').show();
            $('#sea').css({'bottom': - 230 + ( delta * 230 ) + 'px'});
            console.log("AT", -200 + (delta * 200));
            $('#meteor').css({'top':  - 500 + ( delta * 800 ) + 'px', 'left': -0 + (delta * 200)});
            console.log("POWER", power);
           
            
         }
     },
     {
         
        'callback': function (delta) {  
            console.log("DELTA", delta);
            $('#meteor').hide();
            $('#heading').html('Began my RESA');
            $('#subheading').html('I begin my RESA');
            $('#airplane').show();
            $('#airplane').css({'left': -244 + (delta * 3100), 'bottom': -91 + (delta * 2000)});
            $('#sea').css({'left': -delta * (1000), 'bottom': -(delta * 2000)});
         
            $('#palm').css({'left': -delta * (1000), 'bottom': -(delta * 2000)});
        }
     }
   ]
});

