
require([], function () {
  
  var MdL = function (uri) {
      if (typeof(uri) === 'undefined') {
          return;
      }
      this.uri = uri;
      this.backend = 'http://api.spotify.com/v1/' + uri.split[1] + 's';
  }
  MdL.prototype = new Observer();
  MdL.prototype.constructor = Observer;
  MdL.prototype.load = function (properties) {
      var self = this;
      var promise = new Promise();
      if ('uri' in this) {
      var xmlHttp = new XMLHttpRequest();
      xmlHttp.onreadystatechange = function () {
          if (xmlHttp.readyState == 4) {
              if (xmlHttp.status == 200) {
                  var obj = JSON.parse(xmlHttp.responseText);
                  promise.setDone(obj);
              }
          }
      }
    
      xmlHttp.open('GET', this.backend + '/' + this.uri.slice[2], true);
      } else {
          setTimeout(function () {
              promise.setDone(self);
          }, 100);
      }
      return promise;
  };
  MdL.fromURI = function (uri) {
      return new MdL(uri);
  }
  
  var Artist = function (arguments) {
      MdL.call(this, arguments);
  }
  Artist.prototype = new MdL();
  Artist.prototype.constructor = MdL;
  Artist.fromURI = function (uri) {
      return new Artist(uri);
  }
  
  var Collection = function (arguments) {
      MdL.call(this, arguments);
  }
  Collection.prototype = new MdL();
  Collection.prototype.constructor = MdL;
  Collection.fromURI = function (uri) {
      return new Collection(uri);
  }
  var Album = function (arguments) {
      MdL.call(this, arguments);
  }
  Album.fromURI = function (uri) {
      return new Album(uri);
  }
  Album.prototype = new Collection();
  Album.prototype.constructor = Collection;
  
  var Playlist = function (arguments) {
      MdL.call(this, arguments);
  }
  Playlist.fromURI = function (uri) {
      return new Playlist(uri);
  }
  Playlist.prototype = new Collection();
  Playlist.prototype.constructor = Collection;
  
  var Track = function (arguments) {
      MdL.call(this, arguments);
  }
  Track.prototype = new MdL();
  Track.prototype.constructor = MdL;
  Track.fromURI = function (uri) {
      return new Track(uri);
  }
  exports.Artist = Artist;
  exports.Playlist = Playlist;
  exports.Album = Album;
  exports.Track = Track;
  exports.MdL = MdL;
  exports.Collection = Collection;
  
  var Application = function (arguments, dropped, identifier, name, uri) {
      this.arguments = arguments;
      this.dropped = dropped;
      this.identifier = identifier;
      this.name = name;
      this.uri = uri;
  };
  Application.prototype = new Observer();
  Application.prototype.constructor = Observer;
  Application.prototype.activate = function () {
      // TODO Mock function
  };
  Application.prototype.deactivate = function () {
      // TODO Mock function
  };
  Application.prototype.setTitle = function (title) {
      // TODO Mock function
  };
  
  exports.Application = Application;
  exports.application = __application;
  
  var Session = function () {
    this.offline = false;
    this.connecting = false;
    this.country = 'SE';
    this.developer = true,
    this.incognito = false;
    this.online = true;
    this.partner = 'Bungalow Doctrine';
    this.product = 'Spotify';
    this.resolution = 1;
    this.streaming = 'disabled'; // TODO Fix this
    this.testGroup = 'LGH1102';
    this.user = null;
  }
  Session.prototype = new MdL();
  Session.prototype.constructor = MdL;
  exports.Session = Session;
})
