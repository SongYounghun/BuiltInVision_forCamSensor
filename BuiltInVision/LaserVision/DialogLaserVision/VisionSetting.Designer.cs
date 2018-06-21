namespace GalvoScanner.LaserVision.DialogLaserVision
{
    partial class VisionSetting
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
            this.button_Ok = new System.Windows.Forms.Button();
            this.propertyGrid_vision_setting = new System.Windows.Forms.PropertyGrid();
            this.comboBox_cam_num = new System.Windows.Forms.ComboBox();
            this.checkBox_use_vision = new System.Windows.Forms.CheckBox();
            this.timer_initial_setting = new System.Windows.Forms.Timer(this.components);
            this.button_get_captureproperty = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_vis_index = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Ok
            // 
            this.button_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Ok.Location = new System.Drawing.Point(286, 455);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(84, 29);
            this.button_Ok.TabIndex = 2;
            this.button_Ok.Text = "OK";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // propertyGrid_vision_setting
            // 
            this.propertyGrid_vision_setting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid_vision_setting.Enabled = false;
            this.propertyGrid_vision_setting.Location = new System.Drawing.Point(12, 58);
            this.propertyGrid_vision_setting.Name = "propertyGrid_vision_setting";
            this.propertyGrid_vision_setting.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid_vision_setting.Size = new System.Drawing.Size(355, 391);
            this.propertyGrid_vision_setting.TabIndex = 19;
            this.propertyGrid_vision_setting.ToolbarVisible = false;
            // 
            // comboBox_cam_num
            // 
            this.comboBox_cam_num.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_cam_num.FormattingEnabled = true;
            this.comboBox_cam_num.Location = new System.Drawing.Point(201, 32);
            this.comboBox_cam_num.Name = "comboBox_cam_num";
            this.comboBox_cam_num.Size = new System.Drawing.Size(61, 20);
            this.comboBox_cam_num.TabIndex = 20;
            this.comboBox_cam_num.SelectedIndexChanged += new System.EventHandler(this.comboBox_cam_num_SelectedIndexChanged);
            // 
            // checkBox_use_vision
            // 
            this.checkBox_use_vision.AutoSize = true;
            this.checkBox_use_vision.Location = new System.Drawing.Point(179, 12);
            this.checkBox_use_vision.Name = "checkBox_use_vision";
            this.checkBox_use_vision.Size = new System.Drawing.Size(83, 16);
            this.checkBox_use_vision.TabIndex = 21;
            this.checkBox_use_vision.Text = "Use vision";
            this.checkBox_use_vision.UseVisualStyleBackColor = true;
            this.checkBox_use_vision.CheckedChanged += new System.EventHandler(this.checkBox_use_vision_CheckedChanged);
            // 
            // timer_initial_setting
            // 
            this.timer_initial_setting.Interval = 7500;
            this.timer_initial_setting.Tick += new System.EventHandler(this.timer_initial_setting_Tick);
            // 
            // button_get_captureproperty
            // 
            this.button_get_captureproperty.Location = new System.Drawing.Point(268, 12);
            this.button_get_captureproperty.Name = "button_get_captureproperty";
            this.button_get_captureproperty.Size = new System.Drawing.Size(93, 40);
            this.button_get_captureproperty.TabIndex = 22;
            this.button_get_captureproperty.Text = "Get Capture property";
            this.button_get_captureproperty.UseVisualStyleBackColor = true;
            this.button_get_captureproperty.Click += new System.EventHandler(this.button_get_captureproperty_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "Cam number :";
            // 
            // comboBox_vis_index
            // 
            this.comboBox_vis_index.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_vis_index.FormattingEnabled = true;
            this.comboBox_vis_index.Location = new System.Drawing.Point(34, 23);
            this.comboBox_vis_index.Name = "comboBox_vis_index";
            this.comboBox_vis_index.Size = new System.Drawing.Size(61, 20);
            this.comboBox_vis_index.TabIndex = 24;
            this.comboBox_vis_index.SelectedIndexChanged += new System.EventHandler(this.comboBox_vis_index_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "Vision index :";
            // 
            // VisionSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 496);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_vis_index);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_get_captureproperty);
            this.Controls.Add(this.checkBox_use_vision);
            this.Controls.Add(this.comboBox_cam_num);
            this.Controls.Add(this.propertyGrid_vision_setting);
            this.Controls.Add(this.button_Ok);
            this.Name = "VisionSetting";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisionSetting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisionSetting_FormClosing);
            this.Load += new System.EventHandler(this.VisionSetting_Load);
            this.VisibleChanged += new System.EventHandler(this.VisionSetting_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.PropertyGrid propertyGrid_vision_setting;
        private System.Windows.Forms.ComboBox comboBox_cam_num;
        private System.Windows.Forms.CheckBox checkBox_use_vision;
        private System.Windows.Forms.Timer timer_initial_setting;
        private System.Windows.Forms.Button button_get_captureproperty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_vis_index;
        private System.Windows.Forms.Label label2;

    }
}