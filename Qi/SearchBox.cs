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
    public partial class SearchBox : UserControl
    {
        public event EventHandler Search;
        public new String Text
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }
        public SearchBox()
        {
            InitializeComponent();
        }
       

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
         
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Return && textBox1.Text != "")
            {
                e.SuppressKeyPress = true;
                if(this.Search != null)
                this.Search(this, e);
            }
        }
    }
}
