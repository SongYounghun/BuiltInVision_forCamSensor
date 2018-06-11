namespace GalvoScanner.LaserVision.DialogLaserVision
{
    partial class ImageProcessViewer
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
            this.pictureBox_image = new System.Windows.Forms.PictureBox();
            this.panel_outer = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).BeginInit();
            this.panel_outer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_image
            // 
            this.pictureBox_image.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_image.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_image.Name = "pictureBox_image";
            this.pictureBox_image.Size = new System.Drawing.Size(412, 286);
            this.pictureBox_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_image.TabIndex = 0;
            this.pictureBox_image.TabStop = false;
            this.pictureBox_image.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_image_Paint);
            this.pictureBox_image.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_image_MouseDown);
            this.pictureBox_image.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_image_MouseMove);
            this.pictureBox_image.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_image_MouseUp);
            // 
            // panel_outer
            // 
            this.panel_outer.AutoScroll = true;
            this.panel_outer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_outer.Controls.Add(this.pictureBox_image);
            this.panel_outer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_outer.Location = new System.Drawing.Point(0, 0);
            this.panel_outer.Name = "panel_outer";
            this.panel_outer.Size = new System.Drawing.Size(461, 405);
            this.panel_outer.TabIndex = 1;
            // 
            // ImageProcessViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 405);
            this.Controls.Add(this.panel_outer);
            this.Name = "ImageProcessViewer";
            this.Text = "Image viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageProcessViewer_FormClosing);
            this.Load += new System.EventHandler(this.ImageProcessViewer_Load);
            this.VisibleChanged += new System.EventHandler(this.ImageProcessViewer_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).EndInit();
            this.panel_outer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_image;
        private System.Windows.Forms.Panel panel_outer;
    }
}