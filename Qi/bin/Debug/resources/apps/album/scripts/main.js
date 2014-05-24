require(['$api/models', '$views/image#Image', '$views/list#List', '$views/buttons'], function (models, Image, List, buttons) {
    models.application.addEventListener('arguments', function (e) {
        var album = models.Album.fromURI('spotify:' + e.data.arguments.join(':'));
        console.log(album);
        var list = List.forAlbum(album);
        console.log("List", list);
        list.init();
        document.querySelector('#album').innerHTML = '';
        document.querySelector('#album').appendChild(list.node);
        document.querySelector('#name').textContent = 'Loading';
        document.querySelector('#description').textContent = '';
        document.querySelector('#toolbar').innerHTML = '';
        
        var playButton = buttons.PlayButton.forAlbum(album);
        document.querySelector('#toolbar').appendChild(playButton.node);
        var shareButton = buttons.ShareButton.forAlbum(album);
        document.querySelector('#toolbar').appendChild(shareButton.node);
        var followButton = buttons.FollowButton.forAlbum(album);
        document.querySelector('#toolbar').appendChild(followButton.node);
        var cover = Image.forAlbum(album);
        console.log(cover);
        document.querySelector('#cover').innerHTML = '';
        document.querySelector('#cover').appendChild(cover.node);
        album.load('tracks', 'name', 'image', 'user').done(function (album) {
            console.log(album);
            
            document.querySelector('#name').innerHTML = album.name;
           
            
        });
    });
});
