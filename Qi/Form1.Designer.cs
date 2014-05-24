namespace Qi
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.forwardButton1 = new Qi.ForwardButton();
            this.backButton1 = new Qi.BackButton();
            this.searchBox1 = new Qi.SearchBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new Qi.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.viewStack1 = new Qi.ViewStack();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.infoBar1 = new Qi.InfoBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.forwardButton1);
            this.panel1.Controls.Add(this.backButton1);
            this.panel1.Controls.Add(this.searchBox1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 78);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // panel7
            // 
            this.panel7.Location = new System.Drawing.Point(0, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(18, 19);
            this.panel7.TabIndex = 13;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Location = new System.Drawing.Point(972, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(18, 19);
            this.panel6.TabIndex = 12;
            this.panel6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel6_MouseDown);
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel4.Location = new System.Drawing.Point(367, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(357, 60);
            this.panel4.TabIndex = 11;
            this.panel4.Visible = false;
            // 
            // forwardButton1
            // 
            this.forwardButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("forwardButton1.BackgroundImage")));
            this.forwardButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.forwardButton1.Location = new System.Drawing.Point(54, 26);
            this.forwardButton1.Name = "forwardButton1";
            this.forwardButton1.Size = new System.Drawing.Size(37, 31);
            this.forwardButton1.TabIndex = 10;
            this.forwardButton1.Click += new System.EventHandler(this.forwardButton1_Click);
            // 
            // backButton1
            // 
            this.backButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backButton1.BackgroundImage")));
            this.backButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.backButton1.Location = new System.Drawing.Point(12, 26);
            this.backButton1.Name = "backButton1";
            this.backButton1.Size = new System.Drawing.Size(36, 31);
            this.backButton1.TabIndex = 3;
            this.backButton1.Click += new System.EventHandler(this.backButton1_Click);
            // 
            // searchBox1
            // 
            this.searchBox1.BackgroundImage = global::Qi.Properties.Resources.searchbar;
            this.searchBox1.Location = new System.Drawing.Point(111, 26);
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.Size = new System.Drawing.Size(235, 24);
            this.searchBox1.TabIndex = 2;
            this.searchBox1.Search += new System.EventHandler(this.searchBox1_Search);
            this.searchBox1.Load += new System.EventHandler(this.searchBox1_Load);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(781, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 524);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(991, 48);
            this.panel2.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel8.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this.panel8.Location = new System.Drawing.Point(3, 28);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(15, 17);
            this.panel8.TabIndex = 1;
            this.panel8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel8_MouseDown);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.panel5.Location = new System.Drawing.Point(970, 28);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(21, 18);
            this.panel5.TabIndex = 0;
            this.panel5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel5_MouseDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 121);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(991, 403);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 9;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(187, 249);
            this.treeView1.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.axWindowsMediaPlayer1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 249);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(187, 154);
            this.panel3.TabIndex = 12;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(187, 154);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.viewStack1);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(803, 403);
            this.splitContainer2.SplitterDistance = 620;
            this.splitContainer2.TabIndex = 1;
            // 
            // viewStack1
            // 
            this.viewStack1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewStack1.Location = new System.Drawing.Point(0, 0);
            this.viewStack1.Name = "viewStack1";
            this.viewStack1.Size = new System.Drawing.Size(803, 403);
            this.viewStack1.TabIndex = 1;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // infoBar1
            // 
            this.infoBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.infoBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoBar1.ForeColor = System.Drawing.Color.Black;
            this.infoBar1.Location = new System.Drawing.Point(0, 78);
            this.infoBar1.Name = "infoBar1";
            this.infoBar1.Size = new System.Drawing.Size(991, 43);
            this.infoBar1.TabIndex = 8;
            this.infoBar1.Visible = false;
            // 
            // Form1
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(991, 572);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.infoBar1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private InfoBar infoBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TreeView treeView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private ForwardButton forwardButton1;
        private BackButton backButton1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ViewStack viewStack1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private SearchBox searchBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
    }
}

