namespace GalvoScanner.IO.DialogIO
{
    partial class IONameSetting
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
            this.label_io_index = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_io_index
            // 
            this.label_io_index.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_io_index.Location = new System.Drawing.Point(13, 15);
            this.label_io_index.Name = "label_io_index";
            this.label_io_index.Size = new System.Drawing.Size(84, 21);
            this.label_io_index.TabIndex = 0;
            this.label_io_index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(103, 15);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(251, 21);
            this.textBox_name.TabIndex = 1;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(367, 13);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // IONameSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 59);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_io_index);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IONameSetting";
            this.Text = "IONameSetting";
            this.Load += new System.EventHandler(this.IONameSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_io_index;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Button button_OK;
    }
}