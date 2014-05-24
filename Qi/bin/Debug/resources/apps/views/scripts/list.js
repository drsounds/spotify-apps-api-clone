require(['$views/views#View'], function(View) {
    /**
     * Emulates a playlist. Currently creates a Spotify play button. 
     * @param {Object} resource
     * @param {Object} options
     * @inherits {View}
     ***/
    var List = function (resource, options) {
        this.resource = resource;
        this.node = document.createElement('iframe');
    };
    
    
    List.prototype = new View();
    List.prototype.constructor = View;
    
    List.prototype.init = function () {
        this.node.setAttribute('src', 'https://embed.spotify.com/?uri=' + this.resource.uri);
        this.node.width = 300;
        this.node.height= 600;
        this.node.setAttribute('frameborder', '0');
        this.node.setAttribute('allowtransparency', 'true');
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
