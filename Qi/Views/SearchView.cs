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
    public partial class SearchView : View
    {
        public SearchView()
        {
            InitializeComponent();
        }
        AppView appHome;
        public SearchView(Form1 mainForm)
            : base(mainForm)
        {
            InitializeComponent();
            appHome = new AppView(mainForm);
            appHome.Dock = DockStyle.Fill;
            this.Controls.Add(appHome);
        }
        public override void Navigate(string uri)
        {
            IEnumerable<String> arguments = uri.Split(':').Skip(2);
            appHome.Navigate("spoyler:app:search:" + String.Join(":", arguments));
        }
        public override bool AcceptsUri(string uri)
        {
            return new Regex("^spoyler:search:(.*)").IsMatch(uri);
        }
        private void SearchView_Load(object sender, EventArgs e)
        {

        }
    }
}
