using CefSharp;
using Parse;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qi
{
    static class Program
    {
        class SchemeHandlerFactory : ISchemeHandlerFactory
        {
            public ISchemeHandler Create()
            {
                return new SchemeHandler();
            }
        }

        public class SchemeHandler : CefSharp.ISchemeHandler
        {

            public bool ProcessRequest(IRequest request, ref string mimeType, ref Stream stream)
            {
                if (request.Url.StartsWith("$"))
                {
                    request.Url = request.Url.TrimStart('$');
                    request.Url = "sp://" + request.Url;
                }
                if (request.Url.StartsWith("sp://")) {
                    if (request.Url.Contains("?"))
                    {
                        request.Url = request.Url.Split(':')[0];
                    }
                    var uri = new Uri(request.Url);
                    var segments = uri.Segments;
                    var file = segments[segments.Length - 1];

                    var bytes = File.ReadAllBytes(Assembly.GetEntryAssembly().Location.Replace("Qi.exe", "") + "\\" + request.Url.Replace("sp://","").Replace("\\", "/"));
                        stream = new MemoryStream(bytes);
                    mimeType = "text/html";
                    if(request.Url.EndsWith(".css")) 
                    {
                        mimeType = "text/css";
                        using(StreamReader sr = new StreamReader(stream)) {
                            String t = sr.ReadToEnd();
                            t = t.Replace("@primary-color", ColorTranslator.ToHtml(MainForm.PrimaryColor));
                            t = t.Replace("@fore-color", ColorTranslator.ToHtml(MainForm.ForeColor));
                            t = t.Replace("@back-color", ColorTranslator.ToHtml(MainForm.BackColor));
                            t = dotless.Core.Less.Parse(t);
                            stream = new MemoryStream(Encoding.GetEncoding("UTF-8").GetBytes(t));
                        }
                    }
                    if(request.Url.EndsWith(".html")) {
                        mimeType = "text/html";
                    }
                    if(request.Url.EndsWith(".js")) {
                        mimeType = "text/javascript";
                    }
                    if(request.Url.EndsWith(".png")) {
                        mimeType = "image/png";
                    }

                    return true;
               }
               return false;
            }

            public bool ProcessRequestAsync(IRequest request, SchemeHandlerResponse response, OnRequestCompletedHandler requestCompletedCallback)
            {
               
                // Add compability with Spotify's internal resource protocol
                // 
                if (request.Url.Contains("$"))
                {
                    request.Url = request.Url.Split('$')[1];
                    request.Url = request.Url.TrimStart('$');
                    request.Url = "sp://" + request.Url;
                }
                if (request.Url.StartsWith("sp://"))
                {
                    if (request.Url.Contains("?"))
                    {
                        request.Url = request.Url.Split('?')[0];
                    }
                    var uri = new Uri(request.Url);
                    var segments = uri.Segments;
                    var file = segments[segments.Length - 1];
                    string filePath = Assembly.GetEntryAssembly().Location.Replace("Qi.exe", "") +  "\\resources\\apps\\" + request.Url.Replace("sp://", "").Replace("\\", "/");
                    // If developer mode
                    if (Properties.Settings.Default.Developer && !File.Exists(filePath))
                    {
                        filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Spotify\\" + request.Url.Replace("sp://", "").Replace("\\", "/");
                       
                    }
                    // If developer mode
                    if (Properties.Settings.Default.Developer && !File.Exists(filePath))
                    {
                        filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Spoyler\\" + request.Url.Replace("sp://", "").Replace("\\", "/");

                    }
                    // If developer mode
                    if (Properties.Settings.Default.Developer && !File.Exists(filePath))
                    {
                        filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Bungalow\\" + request.Url.Replace("sp://", "").Replace("\\", "/");

                    }
                    if (!File.Exists(filePath))
                    {
                           
                        request.Url = "sp://" + (Properties.Settings.Default.Developer ? "notfound" : "notfound") + "/index.html";
                        filePath = Assembly.GetEntryAssembly().Location.Replace("Qi.exe", "") + "\\resources\\apps\\" + request.Url.Replace("sp://", "").Replace("\\", "/");
                        response.ResponseHeaders = new Dictionary<string,string>();
                        response.ResponseHeaders.Add(new KeyValuePair<string, string>("Status", "404 Not Found"));
                    }
                    
                    
                    var bytes = File.ReadAllBytes(filePath);
                    response.ResponseStream = new MemoryStream(bytes);
                    response.MimeType = "text/html";
                    if (request.Url.EndsWith(".css"))
                    {
                        response.MimeType = "text/css";
                        using (StreamReader sr = new StreamReader(response.ResponseStream))
                        {
                            String t = sr.ReadToEnd();
                            t = t.Replace("@primary-color", ColorTranslator.ToHtml(Properties.Settings.Default.PrimaryColor));
                            t = t.Replace("@fore-color", ColorTranslator.ToHtml(Properties.Settings.Default.ForeColor));
                            t = t.Replace("@back-color", ColorTranslator.ToHtml(MainForm.ViewStackColor));
                            t = dotless.Core.Less.Parse(t);
                            response.ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(t));
                        }
                    }
                    if (request.Url.EndsWith(".html"))
                    {
                        response.MimeType = "text/html";
                        using (StreamReader sr = new StreamReader(response.ResponseStream))
                        {
                            String t = sr.ReadToEnd();
                            t = "<script src=\"$api/scripts/sprequire.js\" type=\"text/javascript\"></script><script src=\"$api/scripts/core.js\" type=\"text/javascript\"></script>"  + t;
                            response.ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(t));
                        }
                    }
                    if (request.Url.EndsWith(".html"))
                    {
                        response.MimeType = "text/html";
                    }
                    if (request.Url.EndsWith(".js"))
                    {
                        response.MimeType = "text/javascript";
                    }
                    if (request.Url.EndsWith(".png"))
                    {
                        response.MimeType = "image/png";
                    }
                    requestCompletedCallback();
                     return true;


                }
                return false;
            }
        }
        public static String ParseKey = "BcaoTgqLReDYciFUEBzCrkd05RUFaD0yQmO97SRl";
        public static String ParseAppId = "VTlPniIM8oOw3DvzOGR9jyIzeF4CwI6aNT9klJbe";
       
        public static Form1 MainForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Set the unhandled exception mode to force all Windows Forms errors to go through 
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            Parse.ParseClient.Initialize(ParseAppId, ParseKey);
            ParseFacebookUtils.Initialize("554643974572103");
            bool t = CefSharp.CEF.Initialize(new CefSharp.Settings()
            {
                PackLoadingDisabled = false,
                
            });
            CefSharp.CEF.RegisterScheme("sp", new SchemeHandlerFactory());
            CefSharp.CEF.RegisterScheme("$", new SchemeHandlerFactory());
            CefSharp.CEF.RegisterJsObject("__mashcast", new Qi.Mashcast());
            CefSharp.CEF.RegisterJsObject("localSettings", new LocalStorage()); // We use LocalSettings for now, intent to use LocalStorage
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            try
            {
                if (args.Length > 1)
                {
                    Console.Write(String.Join(" ", args));
                    if (args[0] == "/uri")
                        MainForm = new Form1(args[1]);

                }
                else
                {
                    MainForm = new Form1();
                }
            }
            catch (Exception e)
            {

                ErrorReportForm erf = new ErrorReportForm(e);
                erf.ShowDialog();
            }
            Application.Run(MainForm);
        }
        public static String AppPath
        {
            get
            {
                return Assembly.GetEntryAssembly().Location.Replace("Qi.exe", "") + "\\";
            }
        }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ErrorReportForm erf = new ErrorReportForm(e.Exception);
            erf.ShowDialog();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorReportForm erf = new ErrorReportForm(null);
            erf.ShowDialog();
        }
    }
}
