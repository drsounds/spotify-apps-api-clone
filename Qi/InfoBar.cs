using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qi
{
    public partial class InfoBar : UserControl
    {
        public InfoBar()
        {
            InitializeComponent();
        }
        public new String Text
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }
        public void Show(String text)
        {
            this.label1.Text = text;
            this.BeginInvoke(new Action(() => this.Show()));
        }
        private void InfoBar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
