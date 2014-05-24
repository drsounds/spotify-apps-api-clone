require(['$views/views#View'], function(View) {
    /**
     * Emulates a playlist. Currently creates a Spotify play button. 
     * @param {Object} resource
     * @param {Object} options
     * @inherits {View}
     ***/
    var List = function (resource, options) {
        this.resource = resource;
        this.node = document.createElement('table');
      
        this.node.style.width = '100%';
    };
    
    
    List.prototype = new View();
    List.prototype.constructor = View;
    
    List.prototype.init = function () {
       /* this.node.setAttribute('src', 'https://embed.spotify.com/?uri=' + this.resource.uri);
        this.node.width = 300;
        this.node.height= 600;
        this.node.setAttribute('frameborder', '0');
        this.node.setAttribute('allowtransparency', 'true');*/
       this.node.classList.add('sp-tracklist');
       this.node.innerHTML = '<thead><th></th><th>Track</th><th>Artist</th><th>Time</th><th>Album</th></thead>';
       var tbody = document.createElement('tbody');
       this.node.appendChild(tbody);
       console.log(this.resource.collection);
       (this.resource['tracks'] || this.resource['objects']).snapshot(0, 100).load('tracks').done(function (resource) {
         resource.objects.forEach(function (track) {
            track.load('name', 'artists', 'album').done(function (track) {
                console.log(track);
                console.log("Track", track);
                var tr = document.createElement('tr');
                tr.innerHTML = '<td></td><td>' + track.name + '</td><td><a href="' + track.artists[0].uri + '">' + track.artists[0].name + '</a></td><td>0:00</td><td><a href="' + track.album.uri + '">' + track.album.name + '</a></td>';
                tbody.appendChild(tr); 
            });
        });
        });
    }
    
    List.forPlaylist = function (playlist, options) {
        return new List(playlist, options);
    };
    List.forAlbum = function (album, options) {
        return new List(album, options);
    };
    
    /**
     * 
     * @param {Object} collection
     * @param {Object} options
     */
    List.forCollection = function (collection, options) {
        return new List(collection, options);
    };
    exports.List = List;
});
