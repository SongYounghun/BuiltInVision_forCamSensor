namespace GalvoScanner.LaserVision.DialogLaserVision
{
    partial class ImageProcess_FitCircle
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
            this.tbMedianFilterAperture = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbThreshold = new System.Windows.Forms.TextBox();
            this.tbSearchGV = new System.Windows.Forms.TextBox();
            this.tbSearchLength = new System.Windows.Forms.TextBox();
            this.tbSearchStart = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEnabled = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbColorType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCircleFitting = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSearchDirection = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAbsoluteGV = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button_inspect_fit_circle = new System.Windows.Forms.Button();
            this.pictureBox_match_result = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_add_fitcircle_process = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_match_result)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbMedianFilterAperture
            // 
            this.tbMedianFilterAperture.Location = new System.Drawing.Point(146, 122);
            this.tbMedianFilterAperture.Name = "tbMedianFilterAperture";
            this.tbMedianFilterAperture.Size = new System.Drawing.Size(113, 21);
            this.tbMedianFilterAperture.TabIndex = 17;
            this.tbMedianFilterAperture.TextChanged += new System.EventHandler(this.tbMedianFilterAperture_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(129, 12);
            this.label13.TabIndex = 16;
            this.label13.Text = "Median Filter Aperture";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbMedianFilterAperture);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.tbThreshold);
            this.groupBox2.Controls.Add(this.tbSearchGV);
            this.groupBox2.Controls.Add(this.tbSearchLength);
            this.groupBox2.Controls.Add(this.tbSearchStart);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(6, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 157);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameters";
            // 
            // tbThreshold
            // 
            this.tbThreshold.Location = new System.Drawing.Point(146, 95);
            this.tbThreshold.Name = "tbThreshold";
            this.tbThreshold.Size = new System.Drawing.Size(113, 21);
            this.tbThreshold.TabIndex = 15;
            this.tbThreshold.TextChanged += new System.EventHandler(this.tbThreshold_TextChanged);
            // 
            // tbSearchGV
            // 
            this.tbSearchGV.Location = new System.Drawing.Point(146, 68);
            this.tbSearchGV.Name = "tbSearchGV";
            this.tbSearchGV.Size = new System.Drawing.Size(113, 21);
            this.tbSearchGV.TabIndex = 14;
            this.tbSearchGV.TextChanged += new System.EventHandler(this.tbSearchGV_TextChanged);
            // 
            // tbSearchLength
            // 
            this.tbSearchLength.Location = new System.Drawing.Point(146, 41);
            this.tbSearchLength.Name = "tbSearchLength";
            this.tbSearchLength.Size = new System.Drawing.Size(113, 21);
            this.tbSearchLength.TabIndex = 13;
            this.tbSearchLength.TextChanged += new System.EventHandler(this.tbSearchLength_TextChanged);
            // 
            // tbSearchStart
            // 
            this.tbSearchStart.Location = new System.Drawing.Point(146, 14);
            this.tbSearchStart.Name = "tbSearchStart";
            this.tbSearchStart.Size = new System.Drawing.Size(113, 21);
            this.tbSearchStart.TabIndex = 12;
            this.tbSearchStart.TextChanged += new System.EventHandler(this.tbSearchStart_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Threshold";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "Search GV";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "Search Length";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "Search Start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enabled";
            // 
            // cbEnabled
            // 
            this.cbEnabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEnabled.FormattingEnabled = true;
            this.cbEnabled.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cbEnabled.Location = new System.Drawing.Point(146, 14);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(113, 20);
            this.cbEnabled.TabIndex = 2;
            this.cbEnabled.SelectedIndexChanged += new System.EventHandler(this.cbEnabled_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbColorType);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbCircleFitting);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbSearchDirection);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbAbsoluteGV);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbEnabled);
            this.groupBox1.Location = new System.Drawing.Point(6, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 156);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // cbColorType
            // 
            this.cbColorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColorType.FormattingEnabled = true;
            this.cbColorType.Location = new System.Drawing.Point(146, 118);
            this.cbColorType.Name = "cbColorType";
            this.cbColorType.Size = new System.Drawing.Size(113, 20);
            this.cbColorType.TabIndex = 10;
            this.cbColorType.SelectedIndexChanged += new System.EventHandler(this.cbColorType_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 121);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 12);
            this.label12.TabIndex = 9;
            this.label12.Text = "Color Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Circle Fitting";
            // 
            // cbCircleFitting
            // 
            this.cbCircleFitting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCircleFitting.FormattingEnabled = true;
            this.cbCircleFitting.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cbCircleFitting.Location = new System.Drawing.Point(146, 92);
            this.cbCircleFitting.Name = "cbCircleFitting";
            this.cbCircleFitting.Size = new System.Drawing.Size(113, 20);
            this.cbCircleFitting.TabIndex = 8;
            this.cbCircleFitting.SelectedIndexChanged += new System.EventHandler(this.cbCircleFitting_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Search Direction";
            // 
            // cbSearchDirection
            // 
            this.cbSearchDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchDirection.FormattingEnabled = true;
            this.cbSearchDirection.Items.AddRange(new object[] {
            "In to Out",
            "Out to In"});
            this.cbSearchDirection.Location = new System.Drawing.Point(146, 66);
            this.cbSearchDirection.Name = "cbSearchDirection";
            this.cbSearchDirection.Size = new System.Drawing.Size(113, 20);
            this.cbSearchDirection.TabIndex = 6;
            this.cbSearchDirection.SelectedIndexChanged += new System.EventHandler(this.cbSearchDirection_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Absolute GV";
            // 
            // cbAbsoluteGV
            // 
            this.cbAbsoluteGV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAbsoluteGV.FormattingEnabled = true;
            this.cbAbsoluteGV.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cbAbsoluteGV.Location = new System.Drawing.Point(146, 40);
            this.cbAbsoluteGV.Name = "cbAbsoluteGV";
            this.cbAbsoluteGV.Size = new System.Drawing.Size(113, 20);
            this.cbAbsoluteGV.TabIndex = 4;
            this.cbAbsoluteGV.SelectedIndexChanged += new System.EventHandler(this.cbAbsoluteGV_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(284, 354);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Setting";
            // 
            // button_inspect_fit_circle
            // 
            this.button_inspect_fit_circle.Location = new System.Drawing.Point(6, 20);
            this.button_inspect_fit_circle.Name = "button_inspect_fit_circle";
            this.button_inspect_fit_circle.Size = new System.Drawing.Size(75, 23);
            this.button_inspect_fit_circle.TabIndex = 10;
            this.button_inspect_fit_circle.Text = "Inspect";
            this.button_inspect_fit_circle.UseVisualStyleBackColor = true;
            this.button_inspect_fit_circle.Click += new System.EventHandler(this.button_inspect_fit_circle_Click);
            // 
            // pictureBox_match_result
            // 
            this.pictureBox_match_result.Location = new System.Drawing.Point(6, 49);
            this.pictureBox_match_result.Name = "pictureBox_match_result";
            this.pictureBox_match_result.Size = new System.Drawing.Size(274, 201);
            this.pictureBox_match_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_match_result.TabIndex = 11;
            this.pictureBox_match_result.TabStop = false;
            this.pictureBox_match_result.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox_match_result);
            this.groupBox3.Controls.Add(this.button_inspect_fit_circle);
            this.groupBox3.Location = new System.Drawing.Point(303, 30);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(286, 62);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Inspect Fit circle";
            // 
            // button_add_fitcircle_process
            // 
            this.button_add_fitcircle_process.Location = new System.Drawing.Point(303, 98);
            this.button_add_fitcircle_process.Name = "button_add_fitcircle_process";
            this.button_add_fitcircle_process.Size = new System.Drawing.Size(286, 36);
            this.button_add_fitcircle_process.TabIndex = 13;
            this.button_add_fitcircle_process.Text = "Add Fit circle process";
            this.button_add_fitcircle_process.UseVisualStyleBackColor = true;
            this.button_add_fitcircle_process.Click += new System.EventHandler(this.button_add_fitcircle_process_Click);
            // 
            // ImageProcess_FitCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 388);
            this.Controls.Add(this.button_add_fitcircle_process);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Name = "ImageProcess_FitCircle";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageProcess_FitCircle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageProcess_FitCircle_FormClosing);
            this.Load += new System.EventHandler(this.ImageProcess_FitCircle_Load);
            this.VisibleChanged += new System.EventHandler(this.ImageProcess_FitCircle_VisibleChanged);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_match_result)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbMedianFilterAperture;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbThreshold;
        private System.Windows.Forms.TextBox tbSearchGV;
        private System.Windows.Forms.TextBox tbSearchLength;
        private System.Windows.Forms.TextBox tbSearchStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbEnabled;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbColorType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbCircleFitting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSearchDirection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAbsoluteGV;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button_inspect_fit_circle;
        private System.Windows.Forms.PictureBox pictureBox_match_result;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_add_fitcircle_process;
    }
}