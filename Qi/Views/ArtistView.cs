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
    public partial class ArtistView : View
    {
       AppView appHome;
        public ArtistView(Form1 mainForm)
            : base(mainForm)
        {
            InitializeComponent();
            appHome = new AppView(mainForm);
            appHome.Dock = DockStyle.Fill;
            this.Controls.Add(appHome);
        }
        public override void Navigate(string uri)
        {
            appHome.Navigate("spoyler:app:artist:" + uri.Replace("spoyler:artist:", ""));
        }
        public override bool AcceptsUri(string uri)
        {
            return new Regex("^spoyler:artist:(.*)").IsMatch(uri);
        }
        private void ArtistView_Load(object sender, EventArgs e)
        {

        }
    }
}
