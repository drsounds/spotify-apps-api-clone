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
    public partial class AppyView : AppView
    {
       AppView appHome;
        public AppyView(Form1 mainForm, string app)
            : base(mainForm)
        {
            this.app = app;
            InitializeComponent();
            appHome = new AppView(mainForm);
            appHome.Dock = DockStyle.Fill;
            this.Controls.Add(appHome);
        }
        private string app;
        public override void Navigate(string uri)
        {
            appHome.Navigate("spoyler:app:" + app + ":" + uri.Replace("spoyler:" + app + ":", ""));
        }
        public override bool AcceptsUri(string uri)
        {
            return new Regex("^spoyler:" + app + ":(.*)").IsMatch(uri);
        }
       
        private void AppyView_Load(object sender, EventArgs e)
        {

        }
    }
}
