require([], function () {
    var View = function () {
        this.node = document.createElement('div');
        
    };
    View.prototype = new Observable();
    View.prototype.constructor = Observable;
    exports.View = View;
})
