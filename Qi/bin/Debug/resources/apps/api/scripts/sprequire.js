var __context = this;
var depth = 0;
var LOCALE = 'en'; // TODO Add support for more locales later
Array.prototype.insert = function(index) {
    this.splice.apply(this, [index, 0].concat(
        Array.prototype.slice.call(arguments, 1)));
    return this;
};
var sprequirejs = function (exports) {
    var Spotify = function () {
        
    };
    exports.Spotify = Spotify;
    exports.Spotify.prototype.bind = function (func, obj) {
      return function (args) {
          return func.apply(obj, [args]);
      };
    };
    exports.SP = new Spotify();
    exports.LangModule = function (stringFile) {
        this.strings = {};
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open('GET', stringFile, false);
        xmlHttp.send(null);
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            var data = JSON.parse(xmlHttp.responseText);
            this.strings = data; 
        }
    };
    exports.LangModule.prototype.get = function (key, var_args) {
        if (typeof(this.strings[key]) != 'undefined') {
            return this.strings[key];
        } else {
            return typeof(var_args) !== 'undefined' ? var_args : key;
        }
    };
    
    exports.require = function () {
        var modules = arguments[0];
        var callback = arguments[1];
        var items = [];
        for (var i = 0; i < modules.length; i++) {
            var xmlHttp = new XMLHttpRequest();
            var module = modules[i];
            console.log(module);
            var _class = module.indexOf('#') > 0 ? module.split('#')[1] : null;
            if (_class != null) {
                module = module.split('#')[0];
            }
            if (module.indexOf('.lang') > 0) {
                // Emulate the lang module
                var parts = module.split('/');
                parts.insert(parts.length - 2, LOCALE + '.loc');
                parts.slice(1);
                var url = parts.join('/');
                if (url.indexOf('/') == 0) {
                    url = url.substr(1);
                }
                if (url.indexOf('$') != 0 && url.indexOf('sp') != 0 && url.indexOf('http') != 0) {
               //     url = '$' + window.appName + '/' + url;
                }
                var code = eval('(function () { var exports = {}; exports = new LangModule("' + url + '"); console.log("exports", exports); return exports;  })();');
                console.log(code);
                var mod = eval(code);
                console.log("Mod", mod);
                if (_class != null) {
                    console.log(mod);
                    mod = mod[_class];
                }
                items.push(mod);
            } else {
                module = module + '.js';
                if (module.indexOf('$') == 0) {
                var parts = module.split('/');
                
                // Append scripts like in the real spotify apps api
                parts.insert(parts.length - 1, 'scripts');
                module = parts.join('/');
                if (module.indexOf('/') == 0)
                module = module.substr(1);
                }
                xmlHttp.open('GET', module, false);
                xmlHttp.send(null);
            
                if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
                    console.log(xmlHttp.responseText);
                    var code = ("(function () { var exports = {}; " + xmlHttp.responseText + "; console.log('exports" + depth + "', exports); return exports;  })();");
                    console.log(code);
                    console.log(url);
                        var mod = eval(code);
                        console.log("Mod", mod);
                        if (_class != null) {
                            console.log(mod);
                            mod = mod[_class];
                        }
                        items.push(mod);
                
                }
            }
        }
        callback.apply(__context, items);
    };
    var Promise = function () {
       var self = this;
       this._done = function () { return self; };
       this._fail = function () { return self; };
       this._always = function () { return self; };
   };
   Promise.prototype = {
       done: function (callback) {
           this._done = callback;
           return this;
       },
       fail: function (callback) {
           this._fail = callback;
           return this;
       },
       always: function (callback) {
           this._always = callback;
           return this;
       },
       setDone: function (data) {
           this._done.call(this, data);
           this._always.call(this, data);
       },
       setFail: function (data) {
           this._fail.call(this, data);
           this._always.call(this, data);
       },
   };
   exports.Promise = Promise;
   var Observable = function() {
    };
    Observable.prototype = {
        addEventListener: function(type, method) {
            var listeners, handlers, scope;
            if (!(listeners = this.listeners)) {
                listeners = this.listeners = {};
            }
            if (!(handlers = listeners[type])){
                handlers = listeners[type] = [];
            }
            scope = (scope ? scope : window);
            handlers.push({
                method: method,
                scope: scope,
               // context: (context ? context : scope)
            });
        },
        dispatchEvent: function(type, data) {
            var listeners, handlers, i, n, handler, scope;
            if (!(listeners = this.listeners)) {
                return;
            }
            if (!(handlers = listeners[type])){
                return;
            }
            for (i = 0, n = handlers.length; i < n; i++){
                handler = handlers[i];
                //if (typeof(context)!=="undefined" && context !== handler.context) continue;
                if (handler.method.call(
                    handler.scope, this, type, data
                )===false) {
                    return false;
                }
            }
            return true;
        }
    };
    exports.Observable = Observable;
    exports.Observer = Observable;
    exports.Promise = Promise;
   
};

sprequirejs(window);
