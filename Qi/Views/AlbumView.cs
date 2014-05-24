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
    public partial class AlbumView : View
    {
        AppView appHome;
        
        public AlbumView(Form1 mainForm)
            : base(mainForm)
        {
            this.appHome = new AppView(mainForm);
            this.Controls.Add(appHome);
            appHome.Dock = DockStyle.Fill;
        }
        public override void Navigate(string uri)
        {
            uri = uri.Replace("spoyler:", "spoyler:app:album:");
            appHome.Navigate(uri);
        }
        public override bool AcceptsUri(string uri)
        {
            var regex = new Regex("^spoyler:album:([a-zA-Z0-9]+)$");
            return regex.IsMatch(uri);
        }
        public AlbumView()
        {
            InitializeComponent();
        }

        private void PlaylistView_Load(object sender, EventArgs e)
        {

        }
    }
}
