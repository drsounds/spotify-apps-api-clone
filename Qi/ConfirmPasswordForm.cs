﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qi
{
    public partial class ConfirmPasswordForm : Form
    {
        public ConfirmPasswordForm()
        {
            InitializeComponent();
        }
        public String Password
        {
            get
            {
                return textBox1.Text;
            }

        }

        private void ConfirmPasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
