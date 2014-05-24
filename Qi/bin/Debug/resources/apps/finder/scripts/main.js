var data = {
    'objects':[
        {
            'name': 'Time Machine',
            'uri': 'spotify:app:timemachine',
            'description': ''
        }
    ]
};

$('#apps').html("");
$.each(data, function (i, object) {
   var html = '<div class="entry" cell-padding="5" padding="2"><div class="entry-content">"<a href="' + object.uri + '">' + object.name + '</a><p>' + object.description + '</p></div>';
    var div = document.createElement('div');
    $(div).addClass('col-md-6');
    $(div).html(html);
    $('#apps').append(div);
});
