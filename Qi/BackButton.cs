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
    public partial class BackButton : UserControl
    {
        public BackButton()
        {
            InitializeComponent();
        }

        private void BackButton_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.back_pressed;
        }

        private void BackButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = Properties.Resources.back_pressed;
        }

        private void BackButton_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = Properties.Resources.back_normal;
        }

        private void BackButton_MouseLeave(object sender, EventArgs e)
        {

            this.BackgroundImage = Properties.Resources.back_normal;
        }

        private void BackButton_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
