using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

namespace Qi.Views
{
    public partial class ThemeView : View
    {
        public String ID { get; set; }
        public Color PrimaryColor
        {
            get
            {
                return this.panel1.BackColor;
            }
            set
            {
                this.panel1.BackColor = value;
            }
        }
        public Color ForegroundColor
        {
            get
            {
                return this.label1.ForeColor;
            }
            set
            {
                this.label1.ForeColor = value;
                this.panel1.BackColor = value;
            }
        }
        public Color BackgroundColor
        {
            get
            {
                return this.panel3.BackColor;
            }
            set
            {
                this.panel3.BackColor = value;
            }
        }
        public ThemeView(XmlElement element, ThemeCollectionView parent, Form1 mainForm)
            :base(mainForm)
        {
            this.themeManager = parent;
            InitializeComponent();
            this.BackgroundColor = ColorTranslator.FromHtml(((XmlElement)element.GetElementsByTagName("bgcolor")[0]).GetAttribute("color"));
            this.ForegroundColor = ColorTranslator.FromHtml(((XmlElement)element.GetElementsByTagName("fgcolor")[0]).GetAttribute("color"));
            this.PrimaryColor = ColorTranslator.FromHtml(((XmlElement)element.GetElementsByTagName("primary")[0]).GetAttribute("color"));

        }
        public ThemeView(ThemeCollectionView parent, Form1 mainForm)
            :base(mainForm)
        {
            this.themeManager = parent;
            InitializeComponent();

        }
        private ThemeCollectionView themeManager;
        public ThemeView()
        {
            InitializeComponent();
            this.radioButton1.CheckedChanged += radioButton1_CheckedChanged;
        }
     

        void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.Active = radioButton1.Checked;

        }
        public override bool AcceptsUri(string uri)
        {
            return new Regex("spoyler:internal:control:theme").IsMatch(uri);
        }
        
        public bool Active
        {
            get
            {
                return this.radioButton1.Checked;
            }
            set
            {
                if(value)
                    foreach (ThemeView theme in themeManager.Themes)
                    {
                        theme.Active = false;
                    }
                this.radioButton1.Checked = value;
                
            }
        }
        public override void Navigate(string uri)
        {
            base.Navigate(uri);
        }
        private void ThemeView_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ThemeView_Click(object sender, EventArgs e)
        {
            this.Active = true;
        }
    }
}
