using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Qi
{
    /// <summary>
    /// Interaction logic for FBLogin.xaml
    /// </summary>
    public partial class FBLogin : UserControl
    {
        public FBLogin()
        {
            InitializeComponent();

        }
        public event EventHandler LoginComplete;

        public async void StartLogin()
        {

            try
            {
                webView.Width = 640;
                webView.Height = 480;
                webView.Navigated += webView_Navigated;
                webView.Navigating += webView_Navigating;
                ParseUser user = await Parse.ParseFacebookUtils.LogInAsync(webView, new String[] { "publish_stream", "email" });
                LoginSuccess(this, new EventArgs());
            }
            catch (Exception ex)
            {
                LoginFailed(this, new EventArgs());
            }
        }

        void webView_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            e.Cancel = false;
        }

        void webView_Navigated(object sender, NavigationEventArgs e)
        {
        }
        public event EventHandler LoginSuccess;
        public event EventHandler LoginFailed;
    }
}
