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
    public partial class FacebookLogin : Form
    {
        public FacebookLogin()
        {
            InitializeComponent();
        }

        private void FBLogin_Load(object sender, EventArgs e)
        {
            FBLogin fbLogin = new FBLogin();
            this.elementHost1.Child = fbLogin;
            fbLogin.LoginFailed += fbLogin_LoginFailed;
            fbLogin.LoginSuccess += fbLogin_LoginSuccess;
            fbLogin.StartLogin();
        }

        void fbLogin_LoginSuccess(object sender, EventArgs e)
        {

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        void fbLogin_LoginFailed(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();

        }

        private void FacebookLogin_Load(object sender, EventArgs e)
        {

        }
    
    }
}
