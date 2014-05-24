require(['$api/models', '$views/list#List'], function (models, List) {
    models.application.addEventListener('arguments', function (e) {
        console.log("Event", e);
        var playlist = models.Playlist.fromURI(e.arguments.join(':'));
        console.log(playlist);
        var list = List.forPlaylist(playlist);
        list.init();
        playlist.load('tracks', 'name', 'image', 'user').done(function (playlist) {
            console.log(playlist);
            
            document.querySelector('#playlist').appendChild(list.node);
            document.querySelector('#name').innerHTML = playlist.name;
           
            
        });
    });
});
