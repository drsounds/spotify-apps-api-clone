require(['$views/tabbar#TabBar', '$views/views#View'], function (TabBar, View) {
   var UI = function (opt_options) {
       this.tabBar = TabBar.withTabs(opt_options.tabs);
       this.currentView = 'overview';
        console.log("Options", opt_options);
       this.views = opt_options.views;
       this.tabs = opt_options.tabs;
       var self = this;
       this.tabBar.addEventListener('tabchanged', function (event) {
          self.dispatchEvent('viewchanged', {
              id: event.id,
              previousId: self.activeView
          });
          self.activeView = event.id;
           self.dispatchEvent('viewchanged', e);
       });
       this.activeView = 'overview';
       this.options = opt_options;
        this.tabBar.addToDom(document.body, 'prepend');
       __ui = this;
   };
   
   UI.prototype = new Observable();
   UI.prototype.constructor = Observable;
   UI.init = function (options) {
       return new UI(options);
       
   };
   UI.prototype.setActiveView = function (id) {
       var data = {
           id: id,
           previousId: this.activeView
       };
       this.dispatchEvent('viewchange', data);
   }
   exports.UI = UI;
});
