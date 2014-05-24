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
    public partial class Button : UserControl
    {
        Form1 mainForm;
        public Button(Form1 parent)
        {
            this.mainForm = parent;
            this.BackColor = mainForm.PrimaryColor;
            this.ForeColor = mainForm.ForeColor;
            InitializeComponent();
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Form1 MainForm
        {
            get
            {
                return mainForm;
            }
            set
            {
                this.mainForm = value;
                this.BackColor = mainForm.PrimaryColor;
            }
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
        public Button()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackColor = Form1.LightenColor(mainForm.PrimaryColor, -1.0f);

        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackColor = mainForm.PrimaryColor;

        }
    }
}
