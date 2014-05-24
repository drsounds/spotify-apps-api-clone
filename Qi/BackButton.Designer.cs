namespace Qi
{
    partial class BackButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BackButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Qi.Properties.Resources.back_normal;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DoubleBuffered = true;
            this.Name = "BackButton";
            this.Size = new System.Drawing.Size(34, 34);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BackButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.BackButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.BackButton_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BackButton_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BackButton_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
