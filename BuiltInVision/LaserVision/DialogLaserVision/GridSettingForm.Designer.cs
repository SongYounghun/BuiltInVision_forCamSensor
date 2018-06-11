namespace GalvoScanner.LaserCanvas.DialogLaserGalvo
{
    partial class GridSettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_grid_step = new System.Windows.Forms.TextBox();
            this.button_set = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Step : ";
            // 
            // textBox_grid_step
            // 
            this.textBox_grid_step.Location = new System.Drawing.Point(61, 10);
            this.textBox_grid_step.Name = "textBox_grid_step";
            this.textBox_grid_step.Size = new System.Drawing.Size(100, 21);
            this.textBox_grid_step.TabIndex = 1;
            this.textBox_grid_step.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_grid_step_KeyPress);
            // 
            // button_set
            // 
            this.button_set.Location = new System.Drawing.Point(196, 8);
            this.button_set.Name = "button_set";
            this.button_set.Size = new System.Drawing.Size(52, 23);
            this.button_set.TabIndex = 2;
            this.button_set.Text = "Set";
            this.button_set.UseVisualStyleBackColor = true;
            this.button_set.Click += new System.EventHandler(this.button_set_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "mm";
            // 
            // GridSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 52);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_set);
            this.Controls.Add(this.textBox_grid_step);
            this.Controls.Add(this.label1);
            this.Name = "GridSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GridSettingForm";
            this.Load += new System.EventHandler(this.GridSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_grid_step;
        private System.Windows.Forms.Button button_set;
        private System.Windows.Forms.Label label2;
    }
}