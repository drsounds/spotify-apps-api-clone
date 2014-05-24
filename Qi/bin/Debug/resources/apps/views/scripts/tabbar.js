require(['$views/views#View'], function (View) {
    /**
     * Tab bar view
     * @class
     * @constructor
     * @this {exports.TabBar}
     **/
    exports.TabBar = function (tabs) {
        this.tabs = tabs;
        this.node = document.createElement('div');
        this.node.classList.add('sp-tabbar');
        this.container = document.createElement('div');
        this.container.classList.add('container');
        this.node.appendChild(this.container);
        this.tabs = [];
        
        window.tabBar = this;
        this.addTabs(tabs);
        
    };
    exports.TabBar.prototype = new View();
    exports.TabBar.prototype.addToDom = function (elm, position) {
        if (position == 'prepend') {
            console.log(this.node);
            console.log(elm);
            elm.insertBefore(this.node, elm.childNodes[0]);
        }
        if (position == 'append') {
            elm.appendChild(this.node);
        }
    }
    /**
     * Returns a new initialized TabBar controller. This can only be created once. 
     */
    exports.TabBar.withTabs = function (tabs) {
       
        return new exports.TabBar(tabs);
    };
    exports.TabBar.removeTab = function (id) {
        // TODO Add this later
    }
    exports.TabBar.removeTabs = function (ids) {
        // TODO Remove tabs
    };
    exports.TabBar.prototype.addTabs = function (tabs) {
        for (var i = 0; i < tabs.length; i++) {
            var tab = tabs[i];
            this.addTab(tab);
        }
    }
    exports.TabBar.prototype.addTab = function (tab) {
        var tabNode = new exports.Tab(tab, this);
        this.container.appendChild(tabNode.node);
        this.tabs.push(tabNode);
    }
    exports.TabBar.prototype.onargumentschanged = function (args) {
        var id = args.length > 1 ? args[args.length-1] : null;
        //alert("A");
        this.setActiveTab(id);
    };
    exports.TabBar.prototype.setActiveTab = function (id) {
    
        var tabs = document.getElementsByClassName('sp-tab');
        var tab = null;

        for (var i = 0; i < tabs.length; i++) {
            var tab = tabs[i];
                console.log("ID", id, tab.getAttribute('id'));
                if (id == tab.getAttribute('id')) {
                    tab.classList.add('sp-tab-active');
                } else {
                    tab.classList.remove('sp-tab-active');
            
                }
            
            
           console.log(tab);
            if (tab.getAttribute('id') == id) {
               
        
            }
        }
        console.log("T");
      
    }
    if (!String.prototype.endsWidth)
    String.prototype.endsWidth = function (str) {
        if (this.indexOf(str) == this.length - str.length) {
            return true;
        }
        return false;
    }
    /**
     * Tab bar tab view
     * @class
     * @constructor
     * @this {exports.Tab}
     **/
    exports.Tab = function (options, tabbar) {
        console.log("Taboptions", options);
        this.options = options;
        this.tabbar = tabbar;
        this.node = document.createElement('a');
        this.node.classList.add('sp-tab');
        this.node.textContent = options.name;
        this.views = options.views;
        this.node.id = options.viewId;
        this.id = options.viewId;
        console.log( options.viewId);
        this.node.setAttribute('id', options.viewId);
        console.log(options);
        var self = this;
        this.node.setAttribute('href', 'javascript:void(0);');
        this.node.addEventListener('mousedown', function (event) {
            if (!('arguments' in window)) {
                window.arguments = [];
            }
            var uri = window.arguments;
            
            var uri = 'spoyler:app:' + window.appName + ':' + window.arguments.join(':');
           
            var foundSection = false;
            for (var i = 0; i < tabbar.tabs.length; i++) {
                var tab = tabbar.tabs[i];
                if (uri.endsWidth(':' + tab.id)) {
                       foundSection = true;
                }
            }
            if (foundSection && window.arguments.length > 1) {
                uri = 'spoyler:app:' + window.appName + ':' + window.arguments.splice(0, window.arguments.length - 2).join(':') + ':' + self.id;
            } else {
                uri = 'spoyler:app:' + window.appName + ':' + window.arguments.join(':') + ':' + self.id;
           
            }
            
            //alert(uri);
            window.location = uri;
            
        });
    };
    exports.TabBar = exports.TabBar;
});