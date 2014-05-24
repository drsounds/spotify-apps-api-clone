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
using CefSharp.WinForms;

namespace Qi.Views
{
    public partial class MashcastView : AppView
    {
        public MashcastView(Form1 mainForm)
            : base(mainForm)
        {
            this.Navigate("spoyler:app:player");
            this.AutoScroll = false;
        }
        public override bool AcceptsUri(string uri)
        {
            return new Regex("^spoyler:app:player").IsMatch(uri);
        }
        public override void Navigate(string uri)
        {
            base.Navigate(uri);
            foreach(WebView wv in base.Apps.Values)
            {
                wv.ContentsWidth = 10;
                wv.ContentsHeight = 10;
                wv.Dock = DockStyle.Fill;
                this.MashcastWebView = wv;
            }
        }
        public MashcastView()
        {
            InitializeComponent();
        }

        private void MashcastView_Load(object sender, EventArgs e)
        {

        }
    }
}
