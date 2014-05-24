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
    public partial class ForwardButton : UserControl
    {
        public ForwardButton()
        {
            InitializeComponent();
        }
        private void ForwardButton_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.forward_pressed;
        }

        private void ForwardButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = Properties.Resources.forward_pressed;
        }

        private void ForwardButton_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = Properties.Resources.forward_normal;
        }

        private void ForwardButton_MouseLeave(object sender, EventArgs e)
        {

            this.BackgroundImage = Properties.Resources.forward_normal;
        }
        private void ForwardButton_Load(object sender, EventArgs e)
        {

        }
    }
}
