using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using CefSharp;
using CefSharp.WinForms;
using System.Threading;
using System.Xml;

namespace Qi.Views
{
    public partial class AppView : View
    {
        public AppView()
        {
            InitializeComponent();
        }
        public Form1 MainForm;
        public AppView(Form1 mainForm) : base(mainForm)
        {
            this.MainForm = mainForm;
            InitializeComponent();
          
        }
        public Dictionary<String, WebView> Apps = new Dictionary<string, WebView>();
        public override bool AcceptsUri(string uri)
        {
            return new Regex("spoyler:app:(.*)").IsMatch(uri);
        }
        public class RequestHandler : IRequestHandler
        {
            private AppView host;
            public RequestHandler(AppView host)
            {
                this.host = host;

            }

            public bool GetAuthCredentials(IWebBrowser browser, bool isProxy, string host, int port, string realm, string scheme, ref string username, ref string password)
            {
                return false;
            }

            public bool GetDownloadHandler(IWebBrowser browser, string mimeType, string fileName, long contentLength, ref IDownloadHandler handler)
            {
                return false;
            }

            public bool OnBeforeBrowse(IWebBrowser browser, IRequest request, NavigationType naigationvType, bool isRedirect)
            {
                if (request.Url.StartsWith("spoyler:") /*|| request.Url.StartsWith("spotify:")*/ || request.Url.StartsWith("mashcast:"))
                {
                    host.MainForm.BeginInvoke(new Action(() => 
                        host.MainForm.Navigate(request.Url)
                    ), null);
                    return true;
                }
                return false;
            }

            public bool OnBeforeResourceLoad(IWebBrowser browser, IRequestResponse requestResponse)
            {
                return false;
            }

            public void OnResourceResponse(IWebBrowser browser, string url, int status, string statusText, string mimeType, System.Net.WebHeaderCollection headers)
            {
               
            }
        }
        public class MenuHandler : IMenuHandler
        {
            private AppView appView;
            public MenuHandler(AppView appView)
            {
                this.appView = appView;
            }
            public bool OnBeforeMenu(IWebBrowser browser)
            {

                return true;
            }
        }
        public class LoadHander : ILoadHandler
        {

            public bool OnLoadError(IWebBrowser browser, string url, int errorCode, ref string errorText)
            {
                throw new NotImplementedException();
            }
        }
        public override void Navigate(string uri)
        {
            
            string app = uri.Split(':')[2];
            WebView wv = null;
            foreach (WebView ww in this.Apps.Values)
            {
                ww.Hide();
            }
            if (!Apps.ContainsKey(app))
            {
                BrowserSettings browserSettings = new BrowserSettings();
                browserSettings.ApplicationCacheDisabled = true;
                browserSettings.PageCacheDisabled = true;
                browserSettings.WebSecurityDisabled = true;
                wv = new WebView("sp://" + app + "/index.html", browserSettings);
               
                wv.Dock = DockStyle.Fill;
                Thread.Sleep(10);
                this.Controls.Add(wv);
              
                wv.RequestHandler = new RequestHandler(this);
                wv.MenuHandler = new MenuHandler(this);
                if (Properties.Settings.Default.Developer)
                {
                    wv.ContextMenu = new ContextMenu();
                    MenuItem showInspector = wv.ContextMenu.MenuItems.Add("Show Inspector");
                    MenuItem reloadPage = wv.ContextMenu.MenuItems.Add("Reload page");
                    showInspector.Select += showInspector_Select;
                    reloadPage.Select += reloadPage_Select;
                }
                this.Apps.Add(app, wv);
                wv.Show();
                this.Refresh(); 
               
            }
            else
            {
                wv = this.Apps[app];
                String args = "";
                IEnumerable<String> arguments = uri.Split(':').Skip(3);
             
                foreach (String argument in arguments)
                {
                    args += "'" + argument + "',";
                }
                
                wv.Show();
            }
            if (wv != null)
            {
                if (!wv.IsBrowserInitialized)
                {
                    wv.Tag = uri;
                    wv.PropertyChanged += wv_PropertyChanged;
                    
                    return;
                }
                else
                {
                    try
                    {
                      
                        String args = "";
                        foreach (String argument in uri.Split(':').Skip(3))
                        {
                            args += "'" + argument.Replace("'", "\\'") + "',";
                        }
                        args.TrimEnd(',');

                        
                        wv.ExecuteScript("window.appName = '" + uri.Split(':')[2] + "'; window.arguments = [" + args + "]; if (window.tabBar) window.tabBar.onargumentschanged([ " + args + "]); window.postMessage({'type': 'argumentschanged', 'arguments': [" + args + "]}, '*');");
                    }
                    catch (Exception e)
                    {

                    }
                }
                
            }
        }
        public WebView MashcastWebView;
        /// <summary>
        /// Used to pass callbacks when radio is finished
        /// </summary>
        public void ExecuteScript(string script)
        {
            if (MashcastWebView != null)
            {
                MashcastWebView.ExecuteScript(script);

            }
        }
        public WebView GetActiveWebView()
        {
            foreach(WebView app in Apps.Values) {
                if (app.Visible)
                    return app;
            }
            return null;
        }
        void showInspector_Select(object sender, EventArgs e)
        {
            WebView webView = (WebView)GetActiveWebView();
           
            webView.ShowDevTools();
        }

        void reloadPage_Select(object sender, EventArgs e)
        {
            WebView wv = (WebView)GetActiveWebView();
            wv.Reload();
        }
        private bool webViewIsReady;
        void wv_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsBrowserInitialized")
            {
                foreach (WebView webView in Apps.Values)
                {
                    if (webView.IsBrowserInitialized)
                    {
                        try
                        {
                            String uri = (String)webView.Tag;
                            String args = "";
                            foreach (String argument in uri.Split(':').Skip(3))
                            {
                                args += "'" + argument.Replace("'", "\\'") + "',";
                            }
                            args.TrimEnd(',');
                            webView.LoadCompleted += webView_LoadCompleted;
                            webView.Tag = (object)new WebViewArgs() { args = args, webView = webView, uri = uri, app = uri.Split(':')[2] };
                            //webView.BringToFront();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }
        public class WebViewArgs
        {
            public string uri;
            public string app;
            public string args;
            public WebView webView;
        }

        void webView_LoadCompleted(object sender, LoadCompletedEventArgs url)
        {
            WebView webView = null;
            foreach (WebView wv in this.Apps.Values)
            {
                if (wv.Address == url.Url)
                {
                    webView = wv;
                }

            }
            WebViewArgs args = (WebViewArgs)webView.Tag;
            webView.RegisterJsObject("appName", args.app);
            webView.ExecuteScript("window.appName = '" + args.app + "'; setTimeout( function () {  console.log('Applying general style'); var linkStyle = document.createElement('link'); linkStyle.setAttribute('href', '$views/css/adam.css'); linkStyle.setAttribute('rel', 'stylesheet'); linkStyle.setAttribute('type', 'text/css'); document.head.appendChild(linkStyle); window.arguments = [" + args.args + "]; if (window.tabBar) window.tabBar.onargumentschanged([ " + args.args + "]);  window.postMessage({'type': 'argumentschanged', 'arguments':[" + args.args + "]}, '*'); }, 100);");
            webView.LoadCompleted -= webView_LoadCompleted;             
            if (args.uri.StartsWith("spoyler:app:debug")) 
            {
                this.MashcastWebView = webView;
                this.MainForm.MashcastAppView = this;
            }
        }
       
        private void AppView_Load(object sender, EventArgs e)
        {

        }
    }
    
    
}
