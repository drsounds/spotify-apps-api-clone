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
    public partial class TreeItem : UserControl
    {
        private TreeView treeView;
        public TreeItem(TreeView treeView)
        {
            this.treeView = treeView;
            InitializeComponent();
            this.panel1.BackColor = treeView.MainForm.PrimaryColor;
            this.label1.ForeColor = Form1.LightenColor(treeView.Parent.ForeColor, -1.1f);
                    
        }
        public class TreeItemEventArgs 
        {
            public TreeItem Item;
        }
        public String Uri { get; set; }
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
        public delegate void TreeItemSelectedEventHandler(object sender, TreeItemEventArgs e);
        public event TreeItemSelectedEventHandler ItemSelected;
        private bool selected;
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                if (selected)
                {
                    this.BackColor = Form1.LightenColor(this.treeView.BackColor, 0.1f);
                    this.ForeColor = treeView.MainForm.PrimaryColor;
                    this.label1.Font = new Font(this.label1.Font, FontStyle.Bold);
                    this.panel1.Show();
                }
                else
                {
                    this.label1.Font = this.label1.Font = new Font(this.label1.Font, FontStyle.Regular);
                    this.label1.ForeColor = Form1.LightenColor(treeView.Parent.ForeColor, -1.1f);
                    this.BackColor = treeView.BackColor;
                    this.panel1.Hide();
                }
            }
        }
        public TreeItem()
        {
            InitializeComponent();
        }

        private void TreeItem_Load(object sender, EventArgs e)
        {

        }

        private void TreeItem_Click(object sender, EventArgs e)
        {
            this.Selected = true;
            if (this.ItemSelected != null)
            {
                this.ItemSelected(this, new TreeItemEventArgs() { Item = this });
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.OnClick( e);
        }
    }
}
