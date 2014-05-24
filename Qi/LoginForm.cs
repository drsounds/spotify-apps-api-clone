using Parse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qi
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        public bool IsLoading
        {
            get
            {
                return this.pbThrobber.Visible;
            }
            set
            {
                this.pbThrobber.Visible = value;
                pSignup.Visible = !value;

            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tbEmail.Visible = rbSignup.Checked;
            lblEmail.Visible = rbSignup.Checked;
            button1.Text = rbSignup.Checked ? "Sign up" : "Log in";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (this.rbSignup.Checked)
            {
                ConfirmPasswordForm cpf = new ConfirmPasswordForm();
                if(cpf.ShowDialog() == DialogResult.OK)
                {
                    if (cpf.Password == tbPassword.Text)
                    {
                        // Register with parse
                        this.IsLoading = true;
                        var user = new ParseUser()
                        {
                            Username = tbUserName.Text,
                            Password = tbPassword.Text,
                            Email = tbEmail.Text
                        };
                        try
                        {
                            await user.SignUpAsync();
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occured during sign up");

                        }
                        this.IsLoading = false;
                    }
                }
            }
            else
            {  // Register with parse

                this.IsLoading = true;
                try
                {
                    await ParseUser.LogInAsync(tbUserName.Text, tbPassword.Text);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not log in. Wrong password/username");

                }
                this.IsLoading = false;

            }
        }

        private void rbSignup_CheckedChanged(object sender, EventArgs e)
        {
            tbEmail.Visible = rbSignup.Checked;
            lblEmail.Visible = rbSignup.Checked;
            tbEmail.Visible = rbSignup.Checked;
            button1.Text = rbSignup.Checked ? "Sign up" : "Log in";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.artistconnector.com/?utm_source=mashcastlogin");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Process.Start("http://www.parse.com/?utm_source=mashcastlogin");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://mashcast.fm/privacy-policy/?utm_source=mashcastlogin");
        }

        private void pSignup_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FacebookLogin fbLogin = new FacebookLogin();
            if (fbLogin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Could not log in through Facebook");
            }
        }

    }
}
