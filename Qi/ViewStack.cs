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
    public partial class ViewStack : UserControl
    {
        private List<View> views;
        public ViewStack()
        {
            this.views = new List<View>();
            InitializeComponent();
        }
        public bool Navigate(String uri)
        {
            bool foundView = false;
            foreach (View view in views)
            {
                if (view.AcceptsUri(uri))
                {
                    foreach (View v in views)
                    {
                        v.Hide();
                    }
                    view.Show();
                    view.Navigate(uri);
                    foundView = true;
                    return foundView;
                }
            }
            return foundView;
        }
        public void RegisterView(View view)
        {
            this.views.Add(view);
            this.Controls.Add(view);
            view.Dock = DockStyle.Fill;
            view.Hide();
        }
        private void ViewStack_Load(object sender, EventArgs e)
        {   

        }
    }
}
