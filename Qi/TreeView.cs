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
    public partial class TreeView : UserControl
    {
        private Form1 mainForm;
       
        private List<TreeItem> items = new List<TreeItem>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<TreeItem> Items
        {
            get
            {
                return this.items;
            }
        }
        public TreeView(Form1 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }
        public TreeItem AddItem(String text, String uri)
        {
            TreeItem treeItem = new TreeItem(this);
            treeItem.Text = text;
            treeItem.Uri = uri;
            this.items.Add(treeItem);
            treeItem.Dock = DockStyle.Top;
            this.Controls.Clear();
            this.Controls.Add(treeItem);
            treeItem.Dock = DockStyle.Top;
            foreach (TreeItem item in items.Reverse<TreeItem>())
            {
                this.Controls.Add(item);
                item.Dock = DockStyle.Top;
            }
            treeItem.Height = 24;
            treeItem.ItemSelected += treeItem_ItemSelected;
            if (this.items.Count == 1)
            {
                treeItem.Selected = true;
            }
            return treeItem;
        }

        void treeItem_ItemSelected(object sender, TreeItem.TreeItemEventArgs e)
        {
            foreach (TreeItem item in items)
            {
                if (item != e.Item)
                {
                    item.Selected = false;
                }
            }
            if (this.ItemSelected != null)
            {
                this.ItemSelected(this, new TreeViewItemSelectedEventArgs() { Item = e.Item });
            }
        }
        public class TreeViewItemSelectedEventArgs
        {
            public TreeItem Item;
        }
        public delegate void TreeViewItemSelectedEventHandler(object sender, TreeViewItemSelectedEventArgs e);
        public event TreeViewItemSelectedEventHandler ItemSelected;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Form1 MainForm
        {
            get
            {
                return mainForm;
            }
            set
            {
                mainForm = value;
                this.BackColor = Form1.LightenColor(this.mainForm.BackColor, -1.45f);
            }
        }
        public TreeView()
        {
            InitializeComponent();
        }

        private void TreeView_Load(object sender, EventArgs e)
        {

        }
    }
}
