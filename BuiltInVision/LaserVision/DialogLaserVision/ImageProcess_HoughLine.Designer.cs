namespace BuiltInVision.LaserVision.DialogLaserVision
{
    partial class ImageProcess_HoughLine
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_insp_in_result_roi = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button_apply = new System.Windows.Forms.Button();
            this.textBox_hough_thresold = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_can_thresold2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_can_thresold1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_bin_thresold = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_tilt_range = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_tilt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_add_houghline_process = new System.Windows.Forms.Button();
            this.button_exec_hough_line = new System.Windows.Forms.Button();
            this.pictureBox_hough_result = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_hough_result)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_insp_in_result_roi);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button_apply);
            this.groupBox1.Controls.Add(this.textBox_hough_thresold);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_can_thresold2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_can_thresold1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_bin_thresold);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_tilt_range);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_tilt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 145);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
            // 
            // checkBox_insp_in_result_roi
            // 
            this.checkBox_insp_in_result_roi.AutoSize = true;
            this.checkBox_insp_in_result_roi.Location = new System.Drawing.Point(234, 77);
            this.checkBox_insp_in_result_roi.Name = "checkBox_insp_in_result_roi";
            this.checkBox_insp_in_result_roi.Size = new System.Drawing.Size(155, 16);
            this.checkBox_insp_in_result_roi.TabIndex = 32;
            this.checkBox_insp_in_result_roi.Text = "Inspection in result ROI";
            this.checkBox_insp_in_result_roi.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(380, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "º";
            this.label8.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(380, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "º";
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(334, 107);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(75, 32);
            this.button_apply.TabIndex = 29;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // textBox_hough_thresold
            // 
            this.textBox_hough_thresold.Location = new System.Drawing.Point(131, 101);
            this.textBox_hough_thresold.Name = "textBox_hough_thresold";
            this.textBox_hough_thresold.Size = new System.Drawing.Size(66, 21);
            this.textBox_hough_thresold.TabIndex = 28;
            this.textBox_hough_thresold.Text = "50";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "Hough thresold : ";
            // 
            // textBox_can_thresold2
            // 
            this.textBox_can_thresold2.Location = new System.Drawing.Point(131, 74);
            this.textBox_can_thresold2.Name = "textBox_can_thresold2";
            this.textBox_can_thresold2.Size = new System.Drawing.Size(66, 21);
            this.textBox_can_thresold2.TabIndex = 26;
            this.textBox_can_thresold2.Text = "50";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "Canny thresold(2) : ";
            // 
            // textBox_can_thresold1
            // 
            this.textBox_can_thresold1.Location = new System.Drawing.Point(131, 47);
            this.textBox_can_thresold1.Name = "textBox_can_thresold1";
            this.textBox_can_thresold1.Size = new System.Drawing.Size(66, 21);
            this.textBox_can_thresold1.TabIndex = 24;
            this.textBox_can_thresold1.Text = "50";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "Canny thresold(1) : ";
            // 
            // textBox_bin_thresold
            // 
            this.textBox_bin_thresold.Location = new System.Drawing.Point(131, 20);
            this.textBox_bin_thresold.Name = "textBox_bin_thresold";
            this.textBox_bin_thresold.Size = new System.Drawing.Size(66, 21);
            this.textBox_bin_thresold.TabIndex = 22;
            this.textBox_bin_thresold.Text = "120";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "Binary thresold : ";
            // 
            // textBox_tilt_range
            // 
            this.textBox_tilt_range.Location = new System.Drawing.Point(308, 47);
            this.textBox_tilt_range.Name = "textBox_tilt_range";
            this.textBox_tilt_range.Size = new System.Drawing.Size(66, 21);
            this.textBox_tilt_range.TabIndex = 20;
            this.textBox_tilt_range.Text = "2";
            this.textBox_tilt_range.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(232, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "Tilt range : ";
            this.label1.Visible = false;
            // 
            // textBox_tilt
            // 
            this.textBox_tilt.Location = new System.Drawing.Point(308, 20);
            this.textBox_tilt.Name = "textBox_tilt";
            this.textBox_tilt.Size = new System.Drawing.Size(66, 21);
            this.textBox_tilt.TabIndex = 18;
            this.textBox_tilt.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(268, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "Tilt : ";
            // 
            // button_add_houghline_process
            // 
            this.button_add_houghline_process.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add_houghline_process.Location = new System.Drawing.Point(12, 471);
            this.button_add_houghline_process.Name = "button_add_houghline_process";
            this.button_add_houghline_process.Size = new System.Drawing.Size(412, 36);
            this.button_add_houghline_process.TabIndex = 7;
            this.button_add_houghline_process.Text = "Add Hough line process";
            this.button_add_houghline_process.UseVisualStyleBackColor = true;
            this.button_add_houghline_process.Click += new System.EventHandler(this.button_add_houghline_process_Click);
            // 
            // button_exec_hough_line
            // 
            this.button_exec_hough_line.Location = new System.Drawing.Point(12, 163);
            this.button_exec_hough_line.Name = "button_exec_hough_line";
            this.button_exec_hough_line.Size = new System.Drawing.Size(412, 49);
            this.button_exec_hough_line.TabIndex = 8;
            this.button_exec_hough_line.Text = "Execute hough line";
            this.button_exec_hough_line.UseVisualStyleBackColor = true;
            this.button_exec_hough_line.Click += new System.EventHandler(this.button_exec_hough_line_Click);
            // 
            // pictureBox_hough_result
            // 
            this.pictureBox_hough_result.Location = new System.Drawing.Point(12, 218);
            this.pictureBox_hough_result.Name = "pictureBox_hough_result";
            this.pictureBox_hough_result.Size = new System.Drawing.Size(412, 238);
            this.pictureBox_hough_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_hough_result.TabIndex = 9;
            this.pictureBox_hough_result.TabStop = false;
            this.pictureBox_hough_result.DoubleClick += new System.EventHandler(this.pictureBox_hough_result_DoubleClick);
            // 
            // ImageProcess_HoughLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(436, 519);
            this.Controls.Add(this.pictureBox_hough_result);
            this.Controls.Add(this.button_exec_hough_line);
            this.Controls.Add(this.button_add_houghline_process);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageProcess_HoughLine";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageProcess_HoughLine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageProcess_HoughLine_FormClosing);
            this.Load += new System.EventHandler(this.ImageProcess_HoughLine_Load);
            this.VisibleChanged += new System.EventHandler(this.ImageProcess_HoughLine_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_hough_result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_tilt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_can_thresold2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_can_thresold1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_bin_thresold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_tilt_range;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_add_houghline_process;
        private System.Windows.Forms.TextBox textBox_hough_thresold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.Button button_exec_hough_line;
        private System.Windows.Forms.PictureBox pictureBox_hough_result;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox_insp_in_result_roi;
    }
}