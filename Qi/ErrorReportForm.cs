using Parse;
using System;
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
    public partial class ErrorReportForm : Form
    {
        public ErrorReportForm()
        {
            InitializeComponent();
        }
        public Exception BaseException;
        public ErrorReportForm(Exception baseException)
        {
            InitializeComponent();
            this.BaseException = baseException;
            if(baseException != null)
            {
                this.textBox2.Text = baseException.ToString();
                if (BaseException.GetType() == typeof(Exception))
                {
                    this.BaseException = BaseException;
                    this.textBox2.Text = BaseException.Source + "\n" + BaseException.Message;
                    this.textBox2.Text += "\nStacktrace ----";
                    this.textBox2.Text += "\n" + BaseException.StackTrace;
                }
            }
            else
            {
                this.textBox2.Text = "Error info could not be generated";
            }
          
           
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            ParseObject comment = new ParseObject("Comment");
            comment.GetRelation<ParseUser>("user").Add(ParseUser.CurrentUser);
            comment["text"] = textBox1.Text;
            comment["time"] = DateTime.Now;
            comment["type"] = "Exception in application";
            comment["stack"] = textBox2.Text;
            pictureBox2.Visible = true;
            try
            {
                comment.SaveAsync();
                MessageBox.Show("Thanks for your error report! This will help us ensure this service will work well!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                lStatus.Text = ("Oh! An error to happened! Is it something really bad today? E-mail me info@mashcast.fm");
            }
            pictureBox2.Visible = false;
        }
    }
}
