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

namespace Qi.Views
{
    public partial class PlaylistView : View
    {
        AppView appHome;
        
        public PlaylistView(Form1 mainForm)
            : base(mainForm)
        {
            this.appHome = new AppView(mainForm);
            this.Controls.Add(appHome);
            appHome.Dock = DockStyle.Fill;
        }
        public override void Navigate(string uri)
        {
            uri = uri.Replace("spoyler:", "spoyler:app:playlist:");
            appHome.Navigate(uri);
        }
        public override bool AcceptsUri(string uri)
        {
            var regex = new Regex("^spoyler:user:([a-zA-Z0-9]+):playlist:([a-zA-Z0-9]+)$");
            return regex.IsMatch(uri)
                || new Regex("^spoyler:user:([a-zA-Z-0-9]+):top:tracks$").IsMatch(uri)
                || new Regex("^spoyler:user:([a-zA-Z-0-9]+):starred$").IsMatch(uri)
               || new Regex("^spoyler:user:([a-zA-Z-0-9]+):toplist$").IsMatch(uri);
        }
        public PlaylistView()
        {
            InitializeComponent();
        }

        private void PlaylistView_Load(object sender, EventArgs e)
        {

        }
    }
}
