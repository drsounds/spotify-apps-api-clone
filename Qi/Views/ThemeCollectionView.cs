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
using System.Reflection;
using System.IO;

namespace Qi.Views
{
    public partial class ThemeCollectionView : View
    {
       
        private List<ThemeView> themes;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ThemeView> Themes
        {
            get
            {
                return themes;
            }
        }
        public void LoadThemes()
        {
            string executableDirectory = Assembly.GetEntryAssembly().Location.Replace("Qi.exe", "");
            string[] themes = Directory.GetFiles(executableDirectory + "\\themes");
            foreach (string theme in themes)
            {
                if(theme.EndsWith(".xml")) {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(theme);
                    ThemeView themeView = new ThemeView(xmlDoc.DocumentElement, this, MainForm);
                    this.Controls.Add(themeView);
                    this.themes.Add(themeView);
                    themeView.Dock = DockStyle.Left;
                    themeView.Width = 120;
                }
            }
        }
        public Form1 MainForm
        {
            get;
            set;
        }
        public ThemeCollectionView() 
        {
            
            this.themes = new List<ThemeView>();
            // Iniitalize the white theme


            InitializeComponent();
        }
        public void LoadActiveTheme()
        {
            foreach (ThemeView theme in this.themes)
            {
                if (theme.ID == Properties.Settings.Default.Theme)
                {
                    theme.Active = true;
                }
            }
        }
        public void ApplyTheme()
        {
            ThemeView activeTheme = this.ActiveTheme;
            Properties.Settings.Default.BackColor = activeTheme.BackgroundColor;
            Properties.Settings.Default.ForeColor = activeTheme.ForegroundColor;
            Properties.Settings.Default.PrimaryColor = activeTheme.PrimaryColor;
            Properties.Settings.Default.Theme = activeTheme.ID;
            Properties.Settings.Default.Save();
            MessageBox.Show("Change will be effective upon restart");
        }
        public ThemeView ActiveTheme
        {
            get
            {
                foreach (ThemeView theme in themes)
                {
                    if (theme.Active)
                    {
                        return theme;
                    }
                }
                return null;
            }
        }
        public override bool AcceptsUri(string uri)
        {
            return new Regex("spoyler:internal:control:theme").IsMatch(uri);
        }
        public override void Navigate(string uri)
        {
            base.Navigate(uri);
        }
        private void ThemeCollectionView_Load(object sender, EventArgs e)
        {

        }
    }
}
