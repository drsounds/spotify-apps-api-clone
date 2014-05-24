var __application = null;
var __ui = null;

require(['$api/models#Application'], function (Application) {
    console.log(__application);
    var linkStyle = document.createElement('link'); linkStyle.setAttribute('href', '$views/css/adam.css'); linkStyle.setAttribute('rel', 'stylesheet'); linkStyle.setAttribute('type', 'text/css'); document.head.appendChild(linkStyle);
    
    __application = new Application([], null, null, null, null); 
    window.addEventListener('message', function (event) {
        var data = event.data;
        console.log("Event", event);
        if (data.type == 'argumentschanged') {
            if ('arguments' in data) {
                console.log(__application);
                console.log("DATA", data);
                __application.dispatchEvent('arguments', { 'arguments' : data.arguments});
                console.log(__ui);
                var id = data.arguments.length > 0 ? data.arguments[data.arguments.length - 1] : null;
               if (__ui != null) {
                    __ui.tabBar.onargumentschanged(data.arguments);
                    __ui.setActiveView(id);
                    
                  var views = __ui.views;
                    for (var i = 0; i < __ui.options.views.length; i++) {
                        var view = __ui.options.views[i];
                        if (view.id == id) {
                            console.log("Element", view.element);
                            view.element.style.display = 'block';
                        } else {
                            view.element.style.display = 'none';
                        }
                    }
            }
                   
            }
        }
    }, '*');
   
});