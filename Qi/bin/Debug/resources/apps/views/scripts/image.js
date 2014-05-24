require(['$views/views#View'], function (View) {
    var Image = function (item, options) {
        this.options = options;
        this.item = item;
        this.node = document.createElement('div');
        this.node.classList.add('sp-image');
        if (typeof(options) !== 'undefined') {
            if ('width' in options) {
                this.node.style.width = options.width + 'px';
                this.node.style.height = options.height + 'px';
                
            }
        }
        var self = this;
        if (typeof(item) == 'String') {
            this.node.style.backgroundImage = 'url("' + item + '")';
        }
        item.load('image').done(function (item) {
            console.log(item);
            self.node.style.backgroundImage = 'url("' + item.images['640'] + '")';
        })
    };
    
    
    
    Image.forAlbum = function (album, options) {
       
        return new Image(album, options);
    }
    
    Image.forArtist = function (artist, options) {
        return new Image(artist, options);
    }
    
    Image.forAlbum = function (album, options) {
        return new Image(album, options);
    }
    
    Image.forProfile = function (profile, options) {
        return new Image(album, options);
    }
    
    Image.forTrack = function (track, options) {
        return new Image(track, options);
    }
    
    
    Image.forUser = function (user, options) {
        return new Image(user, options);
    }
    
    Image.fromSource = function (url, options) {
        return new Image(url, options);
    }
    
    Image.prototype = new View();
    Image.prototype.constructor = View;
    exports.Image = Image;
});
