require(['$views/views#View'], function (View) {
    var Throbber = function (elm, opt_delay) {
      this.elm = elm;
      this.delay = opt_delay;  
      this.node.classList.add('sp-throbber');
      this.node.innerHTML = "Loading"; // TODO Prettify the throbber
    };
    Throbber.prototype = new View;
    Throbber.prototype.constructor = new View();
    
    Throbber.forElement = function (elm, opt_delay) {
        return new Throbber(elm, opt_delay);
    };
    Throbber.prototype.hide = function () {
        this.node.style.display = 'none';
    }
    Throbber.prototype.hideContent = function () {
        this.node.parentNode.style.display = 'none';
    }
    Throbber.prototype.setPosition = function (x, y) {
        
    }
    Throbber.prototype.setSize = function (width, height) {
        
    }
    Throbber.prototype.showContent = function () {
        this.node.parentNode.style.display = 'block';
    }
    Throbber.prototype.show = function () {
        this.node.style.display = 'block';
    }
    exports.Throbber = Throbber;
});
