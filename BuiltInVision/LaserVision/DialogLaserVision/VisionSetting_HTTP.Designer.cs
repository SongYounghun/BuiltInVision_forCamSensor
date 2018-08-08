namespace BuiltInVision.LaserVision.DialogLaserVision
{
    partial class VisionSetting_HTTP
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_vis_index = new System.Windows.Forms.ComboBox();
            this.checkBox_use_vision = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_address = new System.Windows.Forms.TextBox();
            this.textBox_stream_address = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_pw = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_timeout = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_status = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button_apply = new System.Windows.Forms.Button();
            this.timer_status = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timeout)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "Vision index :";
            // 
            // comboBox_vis_index
            // 
            this.comboBox_vis_index.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_vis_index.FormattingEnabled = true;
            this.comboBox_vis_index.Location = new System.Drawing.Point(101, 6);
            this.comboBox_vis_index.Name = "comboBox_vis_index";
            this.comboBox_vis_index.Size = new System.Drawing.Size(61, 20);
            this.comboBox_vis_index.TabIndex = 26;
            this.comboBox_vis_index.SelectedIndexChanged += new System.EventHandler(this.comboBox_vis_index_SelectedIndexChanged);
            // 
            // checkBox_use_vision
            // 
            this.checkBox_use_vision.AutoSize = true;
            this.checkBox_use_vision.Location = new System.Drawing.Point(186, 8);
            this.checkBox_use_vision.Name = "checkBox_use_vision";
            this.checkBox_use_vision.Size = new System.Drawing.Size(83, 16);
            this.checkBox_use_vision.TabIndex = 28;
            this.checkBox_use_vision.Text = "Use vision";
            this.checkBox_use_vision.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 23);
            this.label1.TabIndex = 29;
            this.label1.Text = "IP : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_address
            // 
            this.textBox_address.Location = new System.Drawing.Point(138, 54);
            this.textBox_address.Name = "textBox_address";
            this.textBox_address.Size = new System.Drawing.Size(315, 21);
            this.textBox_address.TabIndex = 30;
            // 
            // textBox_stream_address
            // 
            this.textBox_stream_address.Location = new System.Drawing.Point(138, 81);
            this.textBox_stream_address.Name = "textBox_stream_address";
            this.textBox_stream_address.Size = new System.Drawing.Size(315, 21);
            this.textBox_stream_address.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 23);
            this.label3.TabIndex = 31;
            this.label3.Text = "Stream address : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_id
            // 
            this.textBox_id.Location = new System.Drawing.Point(138, 108);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(159, 21);
            this.textBox_id.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 23);
            this.label4.TabIndex = 33;
            this.label4.Text = "ID : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_pw
            // 
            this.textBox_pw.Location = new System.Drawing.Point(138, 135);
            this.textBox_pw.Name = "textBox_pw";
            this.textBox_pw.Size = new System.Drawing.Size(159, 21);
            this.textBox_pw.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 23);
            this.label5.TabIndex = 35;
            this.label5.Text = "Password : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 23);
            this.label6.TabIndex = 37;
            this.label6.Text = "Timeout : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_timeout
            // 
            this.numericUpDown_timeout.Location = new System.Drawing.Point(138, 162);
            this.numericUpDown_timeout.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_timeout.Name = "numericUpDown_timeout";
            this.numericUpDown_timeout.Size = new System.Drawing.Size(93, 21);
            this.numericUpDown_timeout.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(237, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 39;
            this.label7.Text = "ms";
            // 
            // textBox_status
            // 
            this.textBox_status.Location = new System.Drawing.Point(349, 12);
            this.textBox_status.Name = "textBox_status";
            this.textBox_status.ReadOnly = true;
            this.textBox_status.Size = new System.Drawing.Size(104, 21);
            this.textBox_status.TabIndex = 41;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(291, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 12);
            this.label8.TabIndex = 40;
            this.label8.Text = "Status : ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(378, 189);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(75, 23);
            this.button_apply.TabIndex = 42;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // timer_status
            // 
            this.timer_status.Enabled = true;
            this.timer_status.Interval = 300;
            this.timer_status.Tick += new System.EventHandler(this.timer_status_Tick);
            // 
            // VisionSetting_HTTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 224);
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.textBox_status);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDown_timeout);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_pw);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_id);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_stream_address);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_address);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_use_vision);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_vis_index);
            this.Name = "VisionSetting_HTTP";
            this.Text = "VisionSetting_HTTP";
            this.Load += new System.EventHandler(this.VisionSetting_HTTP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_vis_index;
        private System.Windows.Forms.CheckBox checkBox_use_vision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_address;
        private System.Windows.Forms.TextBox textBox_stream_address;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_pw;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_timeout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_status;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.Timer timer_status;
    }
}