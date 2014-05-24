require(['$api/models', '$views/list#List', '$views/buttons'], function (models, List, buttons) {
    models.application.addEventListener('arguments', function (e) {
        var playlist = models.Playlist.fromURI('spotify:' + e.data.arguments.join(':'));
        console.log(playlist);
        var list = List.forPlaylist(playlist);
        list.init();
        document.querySelector('#playlist').innerHTML = '';
        document.querySelector('#playlist').appendChild(list.node);
        document.querySelector('#name').textContent = 'Loading';
        document.querySelector('#description').textContent = 'Loading';
        document.querySelector('#toolbar').innerHTML = '';
        
        var playButton = buttons.PlayButton.forPlaylist(playlist);
        document.querySelector('#toolbar').appendChild(playButton.node);
        var shareButton = buttons.ShareButton.forPlaylist(playlist);
        document.querySelector('#toolbar').appendChild(shareButton.node);
        var followButton = buttons.FollowButton.forPlaylist(playlist);
        document.querySelector('#toolbar').appendChild(followButton.node);
        
        playlist.load('tracks', 'name', 'image', 'user').done(function (playlist) {
            console.log(playlist);
            
            document.querySelector('#name').innerHTML = playlist.name;
           
            
        });
    });
});
