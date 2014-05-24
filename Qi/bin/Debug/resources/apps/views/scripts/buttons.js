require(['$views/views#View'], function (View) {
    console.log(View);
    var BaseButton = function (options) {
        this.node = document.createElement('a');
        this.node.classList.add('btn');
        
       
    };
    BaseButton.prototype = new View();
    BaseButton.prototype.constructor = View;
    exports.BaseButton = BaseButton;
    
    var PlayButton = function (resource, options) {
        /*this.node = document.createElement('iframe');
        this.node.src = 'https://embed.spotify.com/?uri=' + resource.uri;
        this.node.width = 128;
        this.node.height= 128;
        this.node.setAttribute('frameborder', '0');
        this.node.setAttribute('allowtransparency', 'true');*/
       this.node = document.createElement('a');
       this.node.classList.add('btn');
       this.node.classList.add('btn-primary');
       this.node.innerHTML = '<span class="glyphicon glyphicon-play"></span>Play';
    };
    PlayButton.forArtist = function (item, options) {
        return new PlayButton(item, options);
    }
    PlayButton.forItem = function (item, options) {
        return new PlayButton(item, options);
    }
    PlayButton.forAlbum = function (item, options) {
        return new PlayButton(item, options);
    }
    PlayButton.forTrack = function (item, options) {
        return new PlayButton(item, options);
    }
    PlayButton.forPlaylist = function (item, options) {
        return new PlayButton(item, options);
    }
    exports.PlayButton = PlayButton;
    
    var ShareButton = function (resource, options) {
      /*  this.node = document.createElement('iframe');
        this.node.src = 'https://embed.spotify.com/follow/1/?uri=' + resource.uri;
        this.node.width = 300;
        this.node.height= 600;
        this.node.setAttribute('frameborder', '0');
        this.node.setAttribute('allowtransparency', 'true');*/
       // TODO This is only a mock for now
       this.node.classList.add('btn');
       this.node.innerHTML = '<span class="glyphicon glyphicon-share"></span>Share';
       
    };
    ShareButton.prototype = new BaseButton();
    ShareButton.prototype.constructor = BaseButton;
    ShareButton.forArtist = function (item, options) {
        return new ShareButton(item, options);
    }
    ShareButton.forPlaylist = function (item, options) {
        return new ShareButton(item, options);
    }
    ShareButton.forItem = function (item, options) {
        return new ShareButton(item, options);
    }
    ShareButton.forAlbum = function (item, options) {
        return new ShareButton(item, options);
    }
    ShareButton.forTrack = function (item, options) {
        return new ShareButton(item, options);
    }
    exports.ShareButton = ShareButton;
    
    
    var FollowButton = function (resource, options) {
      /*  this.node = document.createElement('iframe');
        this.node.src = 'https://embed.spotify.com/follow/1/?uri=' + resource.uri;
        this.node.width = 300;
        this.node.height= 600;
        this.node.setAttribute('frameborder', '0');
        this.node.setAttribute('allowtransparency', 'true');*/
       // TODO This is only a mock for now
       this.node.classList.add('btn');
       this.node.classList.add('btn');
       this.node.innerHTML = '<span class="glyphicon glyphicon-share"></span>Follow';
       
    };
    FollowButton.prototype = new BaseButton();
    FollowButton.prototype.constructor = BaseButton;
    FollowButton.forArtist = function (item, options) {
        return new FollowButton(item, options);
    }
    FollowButton.forPlaylist = function (item, options) {
        return new FollowButton(item, options);
    }
    FollowButton.forItem = function (item, options) {
        return new FollowButton(item, options);
    }
    FollowButton.forAlbum = function (item, options) {
        return new FollowButton(item, options);
    }
    FollowButton.forTrack = function (item, options) {
        return new FollowButton(item, options);
    }
    exports.FollowButton = FollowButton;
    
})
