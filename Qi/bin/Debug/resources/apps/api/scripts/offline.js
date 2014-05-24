require([], function () {
   var Offline = function () {
       
   };
   Offline.prototype = new Observer();
   Offline.prototype.constructor = Observer;
   Offline.forCurrentUser = function () {
       return new Offline();
   }
   Offline.prototype.enableSync = function (item) {
       // TODO This is only a mock function right now
       var promise = new Promise();
       setTimeout(function () {
           promise.setDone();
       }, 100);
       return promise;
   };
   Offline.prototype.enableSync = function (item) {
       // TODO This is only a mock function right now
       var promise = new Promise();
       setTimeout(function () {
           promise.setDone({
               enabled: false,
               status: 'synced'});
       }, 100);
       return promise;
   };
   Offline.prototype.load = function (properties) {
       var promise = new Promise();
       setTimeout(function () {
           promise.setDone({});
       }, 100);
       return promise;
   };
   exports.Offline = Offline;
});
