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
    public partial class HomeView : View
    {
        public HomeView()
        {
            InitializeComponent();
        }
        AppView appHome;
        public HomeView(Form1 mainForm)
            : base(mainForm)
        {
            InitializeComponent();
            appHome = new AppView(mainForm);
            appHome.Dock = DockStyle.Fill;
            this.Controls.Add(appHome);
        }
        public override void Navigate(string uri)
        {
            appHome.Navigate("spoyler:app:home");
        }
        public override bool AcceptsUri(string uri)
        {
            return new Regex("^spoyler:internal:home").IsMatch(uri);
        }
        private void HomeView_Load(object sender, EventArgs e)
        {

        }
    }
}
