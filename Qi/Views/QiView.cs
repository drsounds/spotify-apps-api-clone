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
    public partial class QiView : View
    {
        public QiView()
        {
            InitializeComponent();
        }
        AppView appHome;
        public QiView(Form1 mainForm)
            : base(mainForm)
        {
            InitializeComponent();
            appHome = new AppView(mainForm);
            appHome.Dock = DockStyle.Fill;
            this.Controls.Add(appHome);
        }
        public override void Navigate(string uri)
        {
            appHome.Navigate("spoyler:app:qi:" + String.Join(":", uri.Split(':').Skip(3)));
        }
        public override bool AcceptsUri(string uri)
        {
            return new Regex("spoyler:qi:([a-zA-Z0-9]+)").IsMatch(uri);
        }

        private void QiView_Load(object sender, EventArgs e)
        {

        }
    }
}
