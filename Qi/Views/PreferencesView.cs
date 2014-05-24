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
    public partial class PreferencesView : View
    {
        public Form1 MainForm;
        public PreferencesView(Form1 mainForm) : base(mainForm)
        {
            InitializeComponent();
            this.MainForm = mainForm;
            this.themeCollectionView1.LoadThemes();
        }
        public PreferencesView()
        {
            InitializeComponent();
        }
        public override bool AcceptsUri(string uri)
        {
            Regex regex = new Regex("^spoyler:config");
            return regex.IsMatch(uri);
        }
        public override void Navigate(string uri)
        {
            
        }
        private void PreferencesView_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.themeCollectionView1.ApplyTheme();
        }
    }
}
