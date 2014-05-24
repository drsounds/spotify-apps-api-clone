using Qi.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qi
{
    public partial class Form1 : Form
    {
        private Stack<String> history = new Stack<string>();
        private Stack<String> future = new Stack<string>();

        private static byte LightenColorComponent(byte colorComponent, float fFactor)
        {
            var inverse = 255 - colorComponent;
            colorComponent += (byte)(inverse * fFactor);

            return colorComponent < 255
                       ? colorComponent
                       : (byte)255;
        }
        public static Color LightenColor(Color color, float lightFactor)
        {
            // Lighten
            // We do this by approaching 256 for a light factor of 2.0f
            float fFactor2 = lightFactor;
            if (fFactor2 > 1.0f)
            {
                fFactor2 -= 1.0f;
            }

            var red = LightenColorComponent(color.R, fFactor2);
            var green = LightenColorComponent(color.G, fFactor2);
            var blue = LightenColorComponent(color.B, fFactor2);

            return Color.FromArgb(red, green, blue);
        }

        public Color PrimaryColor = ColorTranslator.FromHtml("#94d800");
        public Color ViewStackColor
        {
            get
            {
                return this.viewStack1.BackColor;
            }
        }
        public Form1()
        {
            InitializeComponent();

            Initialize("spoyler:app:start");
        }
        // Override the CreateParams property
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ClassStyle |= CS_DROPSHADOW;
            return cp;
        }
    }
        private void Initialize(string uri)
        {
#if(false)
            this.Hide();
            
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }
            else
            {
                Close();
            }
#endif
            this.mashcastView = new Views.MashcastView(this);
            this.BackColor = Properties.Settings.Default.BackColor;
            this.ForeColor = Properties.Settings.Default.ForeColor;
            this.PrimaryColor = Properties.Settings.Default.PrimaryColor;
          //  this.panel1.BackColor = this.PrimaryColor;
            this.panel1.ForeColor = this.BackColor;
            this.panel3.BackColor = LightenColor(this.BackColor, -1.2f);
            this.splitContainer1.Panel2.BackColor = LightenColor(this.BackColor, -1.5f);
            this.treeView1.MainForm = this;
            this.treeView1.AddItem("App Finder", "mashcast:app:finder");
            this.treeView1.ItemSelected += treeView1_ItemSelected;
            this.viewStack1.RegisterView(new Views.AlbumView(this));
            this.viewStack1.RegisterView(new Views.SearchView(this));
            this.viewStack1.RegisterView(new Views.ArtistView(this));
            this.viewStack1.RegisterView(new Views.HomeView(this));
            this.viewStack1.RegisterView(new Views.QiView(this));
            this.viewStack1.RegisterView(new Views.AppView(this));
            this.viewStack1.RegisterView(new Views.PlaylistView(this));
            this.viewStack1.RegisterView(new Views.UserView(this));
            this.viewStack1.RegisterView(new Views.PreferencesView(this));
            this.viewStack1.RegisterView(new Views.AppyView(this, "job"));
            this.viewStack1.RegisterView(new Views.AppyView(this, "channel"));
            this.viewStack1.RegisterView(new Views.AppyView(this, "podcast"));
            this.viewStack1.RegisterView(new Views.AppyView(this, "category"));
            this.viewStack1.RegisterView(new Views.AppyView(this, "player"));
            this.viewStack1.RegisterView(new Views.AppyView(this, "debug"));
            this.viewStack1.RegisterView(new Views.AppyView(this, "sport"));
            this.viewStack1.RegisterView(new Views.AppyView(this, "service"));
            this.viewStack1.RegisterView(new Views.AppyView(this, "genre"));
            this.panel4.Controls.Add(this.mashcastView);
            this.mashcastView.Dock = DockStyle.Fill;
            this.Navigate(uri);
            Player player = new Player(this);
            CefSharp.CEF.RegisterJsObject("player", player);
        }
        public AppView MashcastAppView;
        private MashcastView mashcastView;
        public Form1(string uri)
        {
            InitializeComponent();
            Initialize(uri);
            

        }
        public void SetSelectedItem(string uri)
        {
            foreach (TreeItem item in this.treeView1.Items)
            {
                if ("spoyler:" + String.Join(":", item.Uri.Split(':').Skip(1)) == "spoyler:" + String.Join(":", uri.Split(':').Skip(1)))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }

        void treeView1_ItemSelected(object sender, TreeView.TreeViewItemSelectedEventArgs e)
        {
            this.Navigate(e.Item.Uri);
        }
        public void Navigate(string uri)
        {

            this.Navigate(uri, true);
        }
        private string currentUri = null;
        public void GoBack()
        {
            if (this.history.Count > 0)
            {
                if(currentUri != null)
                    future.Push(this.currentUri);
                Navigate(history.Pop(), false);
            }
        }
        public void GoForward()
        {
            if (this.future.Count > 0)
            {
                if (currentUri != null)
                    history.Push(this.currentUri);
                Navigate(future.Pop(), false);
            }
        }
        public void Navigate(string uri, bool invoke_history)
        {
            if (!(uri.StartsWith("spoyler:") || uri.StartsWith("spotify:") || uri.StartsWith("mashcast:") || uri.StartsWith("radioflow:")))
            {
                uri = "spoyler:search:" + uri;
            }
            if (uri.StartsWith("radioflow:"))
            {
                uri = "spoyler:" + String.Join(":", uri.Split(':').Skip(1));
            }
            if (uri.StartsWith("mashcast:"))
            {
                uri = "spoyler:" + String.Join(":", uri.Split(':').Skip(1));
            }
            if (uri.StartsWith("spotify:"))
            {
                uri = "spoyler:" + String.Join(":", uri.Split(':').Skip(1));
            }
            bool foundView = this.viewStack1.Navigate(uri);
            if (foundView)
            {
                SetSelectedItem(uri);
                infoBar1.Hide();
                
                if (invoke_history)
                {
                    currentUri = uri;
                    if (currentUri != null)
                    {
                        this.history.Push(currentUri);
                    }
                    this.future.Clear();
                }
            }
            else
            {
                infoBar1.Show("The URI could not be found");
            }
        }
        public class Player
        {
            public Form1 mainForm;
            public Player(Form1 mainForm)
            {
                this.mainForm = mainForm;
            }
            public void PostMessage(object obj, string origin)
            {
                var data = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                
                mainForm.mashcastView.ExecuteScript("window.postMessage(" + data + ", '" + origin + "');");
            }
        }
        public void ShowPopup(int type, string title, string message)
        {
            this.notifyIcon1.ShowBalloonTip(0, title, message, ToolTipIcon.Info);
        }
        public void ShowMessage(int type, String message)
        {
            this.infoBar1.Show(message);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public void PlayMedia(string url)
        {
            this.axWindowsMediaPlayer1.URL = url;
            this.axWindowsMediaPlayer1.PlayStateChange += axWindowsMediaPlayer1_PlayStateChange;
        }

        void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8)
            {
                if (this.MashcastAppView != null)
                {
                    if (this.MashcastAppView.MashcastWebView != null)
                    {
                        this.MashcastAppView.ExecuteScript("window.onmediaended();");

                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            this.Navigate(this.searchBox1.Text);
        }

        private void button1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Navigate(searchBox1.Text);
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void searchBox1_Search(object sender, EventArgs e)
        {
            this.Navigate(searchBox1.Text);
        }
        private const int CS_DROPSHADOW = 0x00020000;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void forwardButton1_Click(object sender, EventArgs e)
        {
            GoForward();
        }

        private void backButton1_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void searchBox1_Load(object sender, EventArgs e)
        {

        }
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTTTOP = 12;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTBOTTOMRIGHT, 0);

        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTBOTTOMLEFT, 0);

        }
    }
}
