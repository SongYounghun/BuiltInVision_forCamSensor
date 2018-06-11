namespace GalvoScanner.Utils
{
    partial class LightControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_light_off = new System.Windows.Forms.Button();
            this.button_light_on = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textBox_light_value_total = new System.Windows.Forms.TextBox();
            this.trackBar_light_value_total = new System.Windows.Forms.TrackBar();
            this.checkBox_totalControl = new System.Windows.Forms.CheckBox();
            this.textBox_light_value8 = new System.Windows.Forms.TextBox();
            this.textBox_light_value7 = new System.Windows.Forms.TextBox();
            this.textBox_light_value6 = new System.Windows.Forms.TextBox();
            this.textBox_light_value5 = new System.Windows.Forms.TextBox();
            this.textBox_light_value4 = new System.Windows.Forms.TextBox();
            this.textBox_light_value3 = new System.Windows.Forms.TextBox();
            this.textBox_light_value2 = new System.Windows.Forms.TextBox();
            this.textBox_light_value1 = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.trackBar_light_value8 = new System.Windows.Forms.TrackBar();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.trackBar_light_value7 = new System.Windows.Forms.TrackBar();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.trackBar_light_value6 = new System.Windows.Forms.TrackBar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.trackBar_light_value5 = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.trackBar_light_value4 = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.trackBar_light_value3 = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trackBar_light_value2 = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBar_light_value1 = new System.Windows.Forms.TrackBar();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value_total)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value8)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value7)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value6)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value5)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value4)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value3)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_light_off
            // 
            this.button_light_off.Location = new System.Drawing.Point(205, 25);
            this.button_light_off.Name = "button_light_off";
            this.button_light_off.Size = new System.Drawing.Size(75, 23);
            this.button_light_off.TabIndex = 40;
            this.button_light_off.Text = "Light Off";
            this.button_light_off.UseVisualStyleBackColor = true;
            this.button_light_off.Click += new System.EventHandler(this.button_light_off_Click);
            // 
            // button_light_on
            // 
            this.button_light_on.Location = new System.Drawing.Point(124, 25);
            this.button_light_on.Name = "button_light_on";
            this.button_light_on.Size = new System.Drawing.Size(75, 23);
            this.button_light_on.TabIndex = 39;
            this.button_light_on.Text = "Light On";
            this.button_light_on.UseVisualStyleBackColor = true;
            this.button_light_on.Click += new System.EventHandler(this.button_light_on_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "Unit : %";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.textBox_light_value_total);
            this.groupBox9.Controls.Add(this.trackBar_light_value_total);
            this.groupBox9.Controls.Add(this.checkBox_totalControl);
            this.groupBox9.Location = new System.Drawing.Point(5, 60);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(291, 93);
            this.groupBox9.TabIndex = 37;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Total control";
            // 
            // textBox_light_value_total
            // 
            this.textBox_light_value_total.Location = new System.Drawing.Point(201, 54);
            this.textBox_light_value_total.Name = "textBox_light_value_total";
            this.textBox_light_value_total.Size = new System.Drawing.Size(78, 21);
            this.textBox_light_value_total.TabIndex = 2;
            this.textBox_light_value_total.TextChanged += new System.EventHandler(this.textBox_light_value_total_TextChanged);
            // 
            // trackBar_light_value_total
            // 
            this.trackBar_light_value_total.Location = new System.Drawing.Point(3, 45);
            this.trackBar_light_value_total.Maximum = 100;
            this.trackBar_light_value_total.Name = "trackBar_light_value_total";
            this.trackBar_light_value_total.Size = new System.Drawing.Size(192, 45);
            this.trackBar_light_value_total.TabIndex = 1;
            this.trackBar_light_value_total.ValueChanged += new System.EventHandler(this.trackBar_light_value_total_ValueChanged);
            // 
            // checkBox_totalControl
            // 
            this.checkBox_totalControl.AutoSize = true;
            this.checkBox_totalControl.Checked = true;
            this.checkBox_totalControl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_totalControl.Location = new System.Drawing.Point(7, 21);
            this.checkBox_totalControl.Name = "checkBox_totalControl";
            this.checkBox_totalControl.Size = new System.Drawing.Size(63, 16);
            this.checkBox_totalControl.TabIndex = 0;
            this.checkBox_totalControl.Text = "Enable";
            this.checkBox_totalControl.UseVisualStyleBackColor = true;
            this.checkBox_totalControl.CheckedChanged += new System.EventHandler(this.checkBox_totalControl_CheckedChanged);
            // 
            // textBox_light_value8
            // 
            this.textBox_light_value8.Location = new System.Drawing.Point(209, 520);
            this.textBox_light_value8.Name = "textBox_light_value8";
            this.textBox_light_value8.Size = new System.Drawing.Size(46, 21);
            this.textBox_light_value8.TabIndex = 36;
            this.textBox_light_value8.Tag = "7";
            this.textBox_light_value8.TextChanged += new System.EventHandler(this.textBox_ValueChanged);
            // 
            // textBox_light_value7
            // 
            this.textBox_light_value7.Location = new System.Drawing.Point(209, 473);
            this.textBox_light_value7.Name = "textBox_light_value7";
            this.textBox_light_value7.Size = new System.Drawing.Size(46, 21);
            this.textBox_light_value7.TabIndex = 35;
            this.textBox_light_value7.Tag = "6";
            this.textBox_light_value7.TextChanged += new System.EventHandler(this.textBox_ValueChanged);
            // 
            // textBox_light_value6
            // 
            this.textBox_light_value6.Location = new System.Drawing.Point(209, 426);
            this.textBox_light_value6.Name = "textBox_light_value6";
            this.textBox_light_value6.Size = new System.Drawing.Size(46, 21);
            this.textBox_light_value6.TabIndex = 34;
            this.textBox_light_value6.Tag = "5";
            this.textBox_light_value6.TextChanged += new System.EventHandler(this.textBox_ValueChanged);
            // 
            // textBox_light_value5
            // 
            this.textBox_light_value5.Location = new System.Drawing.Point(209, 379);
            this.textBox_light_value5.Name = "textBox_light_value5";
            this.textBox_light_value5.Size = new System.Drawing.Size(46, 21);
            this.textBox_light_value5.TabIndex = 33;
            this.textBox_light_value5.Tag = "4";
            this.textBox_light_value5.TextChanged += new System.EventHandler(this.textBox_ValueChanged);
            // 
            // textBox_light_value4
            // 
            this.textBox_light_value4.Location = new System.Drawing.Point(209, 332);
            this.textBox_light_value4.Name = "textBox_light_value4";
            this.textBox_light_value4.Size = new System.Drawing.Size(46, 21);
            this.textBox_light_value4.TabIndex = 32;
            this.textBox_light_value4.Tag = "3";
            this.textBox_light_value4.TextChanged += new System.EventHandler(this.textBox_ValueChanged);
            // 
            // textBox_light_value3
            // 
            this.textBox_light_value3.Location = new System.Drawing.Point(209, 285);
            this.textBox_light_value3.Name = "textBox_light_value3";
            this.textBox_light_value3.Size = new System.Drawing.Size(46, 21);
            this.textBox_light_value3.TabIndex = 31;
            this.textBox_light_value3.Tag = "2";
            this.textBox_light_value3.TextChanged += new System.EventHandler(this.textBox_ValueChanged);
            // 
            // textBox_light_value2
            // 
            this.textBox_light_value2.Location = new System.Drawing.Point(209, 238);
            this.textBox_light_value2.Name = "textBox_light_value2";
            this.textBox_light_value2.Size = new System.Drawing.Size(46, 21);
            this.textBox_light_value2.TabIndex = 30;
            this.textBox_light_value2.Tag = "1";
            this.textBox_light_value2.TextChanged += new System.EventHandler(this.textBox_ValueChanged);
            // 
            // textBox_light_value1
            // 
            this.textBox_light_value1.Location = new System.Drawing.Point(209, 191);
            this.textBox_light_value1.Name = "textBox_light_value1";
            this.textBox_light_value1.Size = new System.Drawing.Size(46, 21);
            this.textBox_light_value1.TabIndex = 29;
            this.textBox_light_value1.Tag = "0";
            this.textBox_light_value1.TextChanged += new System.EventHandler(this.textBox_ValueChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.trackBar_light_value8);
            this.groupBox8.Location = new System.Drawing.Point(3, 503);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(197, 47);
            this.groupBox8.TabIndex = 28;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "CH8";
            // 
            // trackBar_light_value8
            // 
            this.trackBar_light_value8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_light_value8.Location = new System.Drawing.Point(3, 17);
            this.trackBar_light_value8.Maximum = 100;
            this.trackBar_light_value8.Name = "trackBar_light_value8";
            this.trackBar_light_value8.Size = new System.Drawing.Size(191, 27);
            this.trackBar_light_value8.TabIndex = 0;
            this.trackBar_light_value8.Tag = "7";
            this.trackBar_light_value8.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.trackBar_light_value7);
            this.groupBox7.Location = new System.Drawing.Point(3, 456);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(197, 47);
            this.groupBox7.TabIndex = 27;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "CH7";
            // 
            // trackBar_light_value7
            // 
            this.trackBar_light_value7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_light_value7.Location = new System.Drawing.Point(3, 17);
            this.trackBar_light_value7.Maximum = 100;
            this.trackBar_light_value7.Name = "trackBar_light_value7";
            this.trackBar_light_value7.Size = new System.Drawing.Size(191, 27);
            this.trackBar_light_value7.TabIndex = 0;
            this.trackBar_light_value7.Tag = "6";
            this.trackBar_light_value7.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.trackBar_light_value6);
            this.groupBox6.Location = new System.Drawing.Point(3, 409);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(197, 47);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "CH6";
            // 
            // trackBar_light_value6
            // 
            this.trackBar_light_value6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_light_value6.Location = new System.Drawing.Point(3, 17);
            this.trackBar_light_value6.Maximum = 100;
            this.trackBar_light_value6.Name = "trackBar_light_value6";
            this.trackBar_light_value6.Size = new System.Drawing.Size(191, 27);
            this.trackBar_light_value6.TabIndex = 0;
            this.trackBar_light_value6.Tag = "5";
            this.trackBar_light_value6.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.trackBar_light_value5);
            this.groupBox5.Location = new System.Drawing.Point(3, 362);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(197, 47);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "CH5";
            // 
            // trackBar_light_value5
            // 
            this.trackBar_light_value5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_light_value5.Location = new System.Drawing.Point(3, 17);
            this.trackBar_light_value5.Maximum = 100;
            this.trackBar_light_value5.Name = "trackBar_light_value5";
            this.trackBar_light_value5.Size = new System.Drawing.Size(191, 27);
            this.trackBar_light_value5.TabIndex = 0;
            this.trackBar_light_value5.Tag = "4";
            this.trackBar_light_value5.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.trackBar_light_value4);
            this.groupBox4.Location = new System.Drawing.Point(3, 315);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(197, 47);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CH4";
            // 
            // trackBar_light_value4
            // 
            this.trackBar_light_value4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_light_value4.Location = new System.Drawing.Point(3, 17);
            this.trackBar_light_value4.Maximum = 100;
            this.trackBar_light_value4.Name = "trackBar_light_value4";
            this.trackBar_light_value4.Size = new System.Drawing.Size(191, 27);
            this.trackBar_light_value4.TabIndex = 0;
            this.trackBar_light_value4.Tag = "3";
            this.trackBar_light_value4.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.trackBar_light_value3);
            this.groupBox3.Location = new System.Drawing.Point(3, 268);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 47);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CH3";
            // 
            // trackBar_light_value3
            // 
            this.trackBar_light_value3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_light_value3.Location = new System.Drawing.Point(3, 17);
            this.trackBar_light_value3.Maximum = 100;
            this.trackBar_light_value3.Name = "trackBar_light_value3";
            this.trackBar_light_value3.Size = new System.Drawing.Size(191, 27);
            this.trackBar_light_value3.TabIndex = 0;
            this.trackBar_light_value3.Tag = "2";
            this.trackBar_light_value3.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trackBar_light_value2);
            this.groupBox2.Location = new System.Drawing.Point(3, 221);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 47);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CH2";
            // 
            // trackBar_light_value2
            // 
            this.trackBar_light_value2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_light_value2.Location = new System.Drawing.Point(3, 17);
            this.trackBar_light_value2.Maximum = 100;
            this.trackBar_light_value2.Name = "trackBar_light_value2";
            this.trackBar_light_value2.Size = new System.Drawing.Size(191, 27);
            this.trackBar_light_value2.TabIndex = 0;
            this.trackBar_light_value2.Tag = "1";
            this.trackBar_light_value2.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBar_light_value1);
            this.groupBox1.Location = new System.Drawing.Point(3, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 47);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CH1";
            // 
            // trackBar_light_value1
            // 
            this.trackBar_light_value1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_light_value1.Location = new System.Drawing.Point(3, 17);
            this.trackBar_light_value1.Maximum = 100;
            this.trackBar_light_value1.Name = "trackBar_light_value1";
            this.trackBar_light_value1.Size = new System.Drawing.Size(191, 27);
            this.trackBar_light_value1.TabIndex = 0;
            this.trackBar_light_value1.Tag = "0";
            this.trackBar_light_value1.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // LightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_light_off);
            this.Controls.Add(this.button_light_on);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.textBox_light_value8);
            this.Controls.Add(this.textBox_light_value7);
            this.Controls.Add(this.textBox_light_value6);
            this.Controls.Add(this.textBox_light_value5);
            this.Controls.Add(this.textBox_light_value4);
            this.Controls.Add(this.textBox_light_value3);
            this.Controls.Add(this.textBox_light_value2);
            this.Controls.Add(this.textBox_light_value1);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "LightControl";
            this.Size = new System.Drawing.Size(303, 577);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value_total)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value8)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value7)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value6)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value5)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value4)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value3)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_light_value1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_light_off;
        private System.Windows.Forms.Button button_light_on;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox textBox_light_value_total;
        private System.Windows.Forms.TrackBar trackBar_light_value_total;
        private System.Windows.Forms.CheckBox checkBox_totalControl;
        private System.Windows.Forms.TextBox textBox_light_value8;
        private System.Windows.Forms.TextBox textBox_light_value7;
        private System.Windows.Forms.TextBox textBox_light_value6;
        private System.Windows.Forms.TextBox textBox_light_value5;
        private System.Windows.Forms.TextBox textBox_light_value4;
        private System.Windows.Forms.TextBox textBox_light_value3;
        private System.Windows.Forms.TextBox textBox_light_value2;
        private System.Windows.Forms.TextBox textBox_light_value1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TrackBar trackBar_light_value8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TrackBar trackBar_light_value7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TrackBar trackBar_light_value6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TrackBar trackBar_light_value5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TrackBar trackBar_light_value4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar trackBar_light_value3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackBar_light_value2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBar_light_value1;
    }
}
