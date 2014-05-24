
require([], function () {
  
  
  
  var Loadable = function () {
      
  }
  Loadable.prototype = new Observer();
  Loadable.prototype.constructor = Observer;
  
  Loadable.define = function (clazz, names, opt_func) {
      // TODO fix this soon
  };
  Loadable.prototype.resolve = function (name, value, opt_silent) {
      this[name] = value;
  };
  
  Loadable.prototype.load = function (properties) {
     // TODO Abstract function 
  };
  Loadable.fromURI = function (uri) {
      return new Loadable(uri);
  }
  
  var Artist = function (uri) {
      this.uri = uri;
  };
  
  
  var BridgeLoadable = function (uri) {
      this.backend = 'https://api.spotify.com/v1';
      this.images = {};
  if (typeof(uri) === 'undefined') {
          return;
      }
      this.uri = uri;
  }
  BridgeLoadable.prototype = new Loadable();
  BridgeLoadable.prototype.constructor = Loadable;
  
  /**
   * Mock bridge loading by spotify ws 
   * @param {Object} properties
   */
  BridgeLoadable.prototype.load = function (properties) {
      var self = this;
      this.images = {};
      var promise = new Promise();
      if ('uri' in this) {
      var xmlHttp = new XMLHttpRequest();
      xmlHttp.onreadystatechange = function () {
          if (xmlHttp.readyState == 4) {
              if (xmlHttp.status == 200) {
                  
                  //var obj = JSON.parse(xmlHttp.responseText);
                  //promise.setDone(obj);
                  var data = JSON.parse(xmlHttp.responseText);
                  if (self.uri.split(':')[1] == 'track' || self.uri.split(':')[1] == 'album'  || self.uri.split(':')[1] == 'artist') {
                      if (self.uri.split(':')[1] == 'track')
                      
                          self.resolve('album', Album.fromURI(data.album.uri), true);
                      self.resolve('uri', self.uri);
                      if (self.uri.split(':')[1] == 'track' || self.uri.split(':')[1] == 'album' ) {
                          self.resolve('artists', Artist.fromURIs(data.artists.map(function (a) { return a.uri;})), true);
                      }
                      console.log(self.uri);
                      if (self.uri.split(':')[1] == 'album') {
                          var images = data.images;
                          console.log("Images", data.images);
                          for (var i = 0; i < images.length; i++) {
                              var image = images[i];
                              console.log("Images", self);
                              self.images[image.height] = image.url;
                          }
                      }
                      self.resolve('name', data.name, true);
                      self.resolve('popularity', data.popularity, true);
                      self.resolve('type', self.uri[1]);
                      self.resolve('explicit', data.explicit);
                      promise.setDone(self);
                  } else {
                      promise.setFail();
                  }
              } else {
                  promise.setFail();
              }
          }
      }
        
      
      var url = this.backend + '/' + ((this.uri.split(':').length > 3 && this.uri.split(':')[3]) == 'playlist' ? this.uri.split(':')[3] + 's/' + this.uri.split(':')[4] : this.uri.split(':')[1] + 's/' + this.uri.split(':')[2]);
      console.log("URL", this.uri);
      console.log(url);
      xmlHttp.open('GET', url, true);
          console.log(url);
          xmlHttp.send(null);
      } else {
          setTimeout(function () {
              promise.setDone(self);
          }, 100);
      }
      return promise;
  };
  
  var Snapshot = function (collection, opt_start, opt_end, opt_raw) {
      this.objects = [];
      this.collection = collection;
      this.uri = this.collection.uri;
      this.start = opt_start ? opt_start : 0;
      this.end = opt_end ? opt_end : 100;
      this.backend = 'https://api.spotify.com/v1'
  };
  Snapshot.prototype = new Loadable();
  Snapshot.prototype.constructor = Loadable;
  Snapshot.prototype.toArray = function () {
      return this.objects;
  }
  Snapshot.prototype.toURIs = function () {
      return this.objects.map(function (a) { return a.uri; });
  }
  Snapshot.prototype.load = function (properties) {
      var self = this;
      var promise = new Promise();
     
      if ('uri' in this) {
      if (this.uri.indexOf('spotify:trackset:') == 0) {
          return;
      }
      var xmlHttp = new XMLHttpRequest();
      xmlHttp.onreadystatechange = function () {
          if (xmlHttp.readyState == 4) {
              if (xmlHttp.status == 200) {
                  
                  //var obj = JSON.parse(xmlHttp.responseText);
                  //promise.setDone(obj);
                  
                // console.log(xmlHttp.responseText);
                  var data = JSON.parse(xmlHttp.responseText);
                 
                      console.log("SELF", self);
                  if (self.uri.split(':')[1] == 'playlist' || self.uri.split(':')[1] == 'album') {
                      var items = 0;
                      
                      var tracks = data.tracks.items;
                      for (var i = 0; i < tracks.length; i++) {
                          var track = Track.fromURI(tracks[i].uri);
                          self.objects.push(track);
                      }
                      promise.setDone(self);
                  }
                  if (self.uri.split(':')[1] == 'album') {
                      var images = data.images;
                      console.log("Images", data.images);
                      for (var i = 0; i < images.length; i++) {
                          var image = images[i];
                          self.images[image.height] = image.url;
                      }
                  }
                  promise.setFail();
              } else {
                  promise.setFail();
              }              
          }
      }
      var url = this.backend + '/' + ((this.uri.split(':').length > 3 && this.uri.split(':')[3]) == 'playlist' ? this.uri.split(':')[3] + 's/' + this.uri.split(':')[4] : this.uri.split(':')[1] + 's/' + this.uri.split(':')[2]);
      console.log(url);
      xmlHttp.open('GET', url, true);
          xmlHttp.send(null);
      } else {
          setTimeout(function () {
              promise.setDone(self);
          }, 100);
      }
      return promise;
  }
  
  var MdL = function () {
      
  };
  MdL.prototype = new BridgeLoadable();
  MdL.prototype.constructor = BridgeLoadable;
  MdL.init = function (uri) {
      return new MdL(uri);  
  };
  MdL.prototype.imageForSize = function (size) {
      return "";
  };
  Artist.prototype = new MdL();
  Artist.prototype.constructor = MdL;
  Artist.fromURI = function (uri) {
      return new Artist(uri);
  }
  Artist.fromURIs = function (uris) {
      return uris.map(function (uri) { 
          return Artist.fromURI(uri);
      });
  }
  
  var ListDescriptor = function (type, opt_params) {
        this.type = type;  
  };
  ListDescriptor.Types = {
      LIST: 'list',
  LISTS: 'lists',
  SORT: 'sort',
  FILTER: 'filter',
  RANGE: 'range',
  SHUFFLE: 'shuffle'
  };
  ListDescriptor.compare = function (a, b) {
      return a.type == b.type;
  }
  ListDescriptor.create = function (uri) {
       return new ListDescriptor(uri);  
  };
  ListDescriptor.createConcatenated = function (lists) {
      // TODO fix it soon
  };
  ListDescriptor.prototype.filter = function (operation, field, value) {
      
  };
  
  var Collection = function (itemClass, uri, snapshot, opt_descriptor, opt_itemFactory) {
      if (arguments.length < 3) {
          return;
      }
      console.log(uri);
      this.uri = uri;
      this.descriptor = opt_descriptor;
      this.type = SP.bind(itemClass.fromURI, uri);  
  }
 
  Collection.prototype = new MdL();
  Collection.prototype.constructor = MdL;
  
  Collection.prototype.range = function (offset, length) {
      
  };
   Collection.prototype.snapshot = function (start, end) {
      start = start ? start : 0;
      end = end ? end : 100;
      var snapshot = new Snapshot(this, start, end, false);
      return snapshot;
  }
  var Album = function (uri) {
      console.log(Album);
      var collection = new Collection(Album, uri, Snapshot.prototype.load);
      this.tracks = collection;
      this.uri = uri;
  }
  Album.fromURI = function (uri) {
      return new Album(uri);
  }
  Album.prototype = new MdL();
  Album.prototype.constructor = MdL;
  
  var Playlist = function (uri) {
      //alert(uri);
      this.uri = uri;
      var tracks = new Collection(Playlist, uri, Snapshot.prototype.load);
      this.tracks = tracks;
  }
  Playlist.fromURI = function (uri) {
      return new Playlist(uri);
  }
  Playlist.prototype = new MdL();
  Playlist.prototype.constructor = MdL;
  
  var Track = function (uri) {
    this.uri = uri;
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
  exports.Loadable = Loadable;
  exports.BridgeLoadable = BridgeLoadable;
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
  Session.prototype = new BridgeLoadable();
  Session.prototype.constructor = BridgeLoadable;
  exports.Session = Session;
  
  var Track = function (uri) {
      this.uri = uri;
  }
  Track.prototype = new MdL();
  Track.prototype.constructor = MdL;
  Track.fromURI = function (uri) {
      return new Track(uri);
  }
  exports.Track = Track;
  exports.ListDescriptor = ListDescriptor;
  
  var Context = function (uri) {
      this.uri = uri;
  }
  Context.prototype = new Loadable();
  Context.prototype.constructor = Loadable;
  
  var Player = function () {
    this.contexts = [];
    this.context = null;
    this.index = 0;
    this,playing = false;
    this.position = 0;
    this.repeat = false;
    this.shuffle = false;
    this.track = null;    
  };
  Player.prototype = new BridgeLoadable();
  Player.prototype.constructor = BridgeLoadable;
  
  
  
  exports.Player = Player;
})
