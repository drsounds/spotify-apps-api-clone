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
    public partial class View : UserControl
    {
        private Form1 mainForm;
        public View(Form1 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }
        public View()
        {
            InitializeComponent();
        }
        public virtual bool AcceptsUri(string uri)
        {
            return false;
        }
        public virtual void Navigate(string uri)
        {

        }
        private void View_Load(object sender, EventArgs e)
        {

        }
    }
}
