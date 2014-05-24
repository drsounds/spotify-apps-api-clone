require([], function () {
   var ScrollAgent = function (view, options) {
       this.container = options.container;
       this.id = options.id;
       this.height = 0;
       this.tagName = 'div';
       this.scroller = null;
       this.view = view;
       var self = this;
       this.view.addEventListener('scroll', function (event) {
          var scrollY = window.pageYOffset; 
          var bouncer = document.querySelector('sp_bouncer_' + self.id).getBoundingClientRect();
          var bouncerRect = bouncer.getBoundingClientRect();
          var bodyRect = document.body.getBoundingClientRect();
          if (rect.top < bodyRect.bottom && rect.style.display == 'block') {
              // Eg in the scrolling bounds
              bouncer.style.display = 'none';
              var evt = {
                  start: 0,
                  end: 0,
                  done: function (elements) {
                      for (var i = elements.length; i > -1; i--) {
                          self.view.insertBefore(bouncer.parentNode, elements[i]);
                      }
                      bouncer.style.display = 'block';
                  }
              };
              self.dispatchEvent('request', evt);
          }
       });
       var bouncer = document.createElement('div');
       bouncer.addClass('sp-bouncer');
       bouncer.setAttribute('id', 'sp_bouncer_' + this.id);
       this.view.appendChild(bouncer);
       
   };
   ScrollAgent.prototype = new Observable();
   ScrollAgent.prototype.constructor = Observable;
   exports.ScrollAgent = ScrollAgent;
   
});
