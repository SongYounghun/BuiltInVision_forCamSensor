namespace GalvoScanner.LaserVision.DialogLaserVision
{
    partial class ImageProcess_TemplateMatch
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
            this.button_save_template_image = new System.Windows.Forms.Button();
            this.textBox_template_image_path = new System.Windows.Forms.TextBox();
            this.button_load_template_image = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_getfrom_insp_roi = new System.Windows.Forms.Button();
            this.button_apply = new System.Windows.Forms.Button();
            this.textBox_insp_h = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_insp_w = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_insp_y = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_insp_x = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_threshold_socre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_button_targetPos_set_toView = new System.Windows.Forms.Button();
            this.button_targetPos_get_fromView = new System.Windows.Forms.Button();
            this.textBox_target_offsetY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_target_offsetX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_template_image = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_match_score = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_exec_tempate_match = new System.Windows.Forms.Button();
            this.pictureBox_match_result = new System.Windows.Forms.PictureBox();
            this.button_add_templatematch_process = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_template_image)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_match_result)).BeginInit();
            this.SuspendLayout();
            // 
            // button_save_template_image
            // 
            this.button_save_template_image.Location = new System.Drawing.Point(6, 20);
            this.button_save_template_image.Name = "button_save_template_image";
            this.button_save_template_image.Size = new System.Drawing.Size(136, 23);
            this.button_save_template_image.TabIndex = 0;
            this.button_save_template_image.Text = "Save template image";
            this.button_save_template_image.UseVisualStyleBackColor = true;
            this.button_save_template_image.Click += new System.EventHandler(this.button_save_template_image_Click);
            // 
            // textBox_template_image_path
            // 
            this.textBox_template_image_path.Location = new System.Drawing.Point(6, 49);
            this.textBox_template_image_path.Name = "textBox_template_image_path";
            this.textBox_template_image_path.ReadOnly = true;
            this.textBox_template_image_path.Size = new System.Drawing.Size(385, 21);
            this.textBox_template_image_path.TabIndex = 1;
            // 
            // button_load_template_image
            // 
            this.button_load_template_image.Location = new System.Drawing.Point(148, 20);
            this.button_load_template_image.Name = "button_load_template_image";
            this.button_load_template_image.Size = new System.Drawing.Size(136, 23);
            this.button_load_template_image.TabIndex = 2;
            this.button_load_template_image.Text = "Load template image";
            this.button_load_template_image.UseVisualStyleBackColor = true;
            this.button_load_template_image.Click += new System.EventHandler(this.button_load_template_image_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_getfrom_insp_roi);
            this.groupBox1.Controls.Add(this.button_apply);
            this.groupBox1.Controls.Add(this.textBox_insp_h);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBox_insp_w);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox_insp_y);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_insp_x);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_threshold_socre);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.pictureBox_template_image);
            this.groupBox1.Controls.Add(this.button_save_template_image);
            this.groupBox1.Controls.Add(this.button_load_template_image);
            this.groupBox1.Controls.Add(this.textBox_template_image_path);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 299);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Template image";
            // 
            // button_getfrom_insp_roi
            // 
            this.button_getfrom_insp_roi.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_getfrom_insp_roi.Location = new System.Drawing.Point(303, 232);
            this.button_getfrom_insp_roi.Name = "button_getfrom_insp_roi";
            this.button_getfrom_insp_roi.Size = new System.Drawing.Size(89, 35);
            this.button_getfrom_insp_roi.TabIndex = 22;
            this.button_getfrom_insp_roi.Text = "Get from view(Insp roi)";
            this.button_getfrom_insp_roi.UseVisualStyleBackColor = true;
            this.button_getfrom_insp_roi.Click += new System.EventHandler(this.button_getfrom_insp_roi_Click);
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(303, 272);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(89, 21);
            this.button_apply.TabIndex = 21;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // textBox_insp_h
            // 
            this.textBox_insp_h.Location = new System.Drawing.Point(245, 272);
            this.textBox_insp_h.Name = "textBox_insp_h";
            this.textBox_insp_h.Size = new System.Drawing.Size(52, 21);
            this.textBox_insp_h.TabIndex = 20;
            this.textBox_insp_h.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(159, 275);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "Insp Height : ";
            // 
            // textBox_insp_w
            // 
            this.textBox_insp_w.Location = new System.Drawing.Point(88, 272);
            this.textBox_insp_w.Name = "textBox_insp_w";
            this.textBox_insp_w.Size = new System.Drawing.Size(52, 21);
            this.textBox_insp_w.TabIndex = 18;
            this.textBox_insp_w.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 275);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "Insp Width\' : ";
            // 
            // textBox_insp_y
            // 
            this.textBox_insp_y.Location = new System.Drawing.Point(245, 240);
            this.textBox_insp_y.Name = "textBox_insp_y";
            this.textBox_insp_y.Size = new System.Drawing.Size(52, 21);
            this.textBox_insp_y.TabIndex = 14;
            this.textBox_insp_y.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(186, 243);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "Insp Y : ";
            // 
            // textBox_insp_x
            // 
            this.textBox_insp_x.Location = new System.Drawing.Point(88, 240);
            this.textBox_insp_x.Name = "textBox_insp_x";
            this.textBox_insp_x.Size = new System.Drawing.Size(52, 21);
            this.textBox_insp_x.TabIndex = 12;
            this.textBox_insp_x.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "Insp X : ";
            // 
            // textBox_threshold_socre
            // 
            this.textBox_threshold_socre.Location = new System.Drawing.Point(285, 205);
            this.textBox_threshold_socre.Name = "textBox_threshold_socre";
            this.textBox_threshold_socre.Size = new System.Drawing.Size(106, 21);
            this.textBox_threshold_socre.TabIndex = 10;
            this.textBox_threshold_socre.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Threshold socre : ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_button_targetPos_set_toView);
            this.groupBox3.Controls.Add(this.button_targetPos_get_fromView);
            this.groupBox3.Controls.Add(this.textBox_target_offsetY);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBox_target_offsetX);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(177, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 124);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Target offset position (pixel)";
            // 
            // button_button_targetPos_set_toView
            // 
            this.button_button_targetPos_set_toView.Location = new System.Drawing.Point(11, 76);
            this.button_button_targetPos_set_toView.Name = "button_button_targetPos_set_toView";
            this.button_button_targetPos_set_toView.Size = new System.Drawing.Size(197, 32);
            this.button_button_targetPos_set_toView.TabIndex = 12;
            this.button_button_targetPos_set_toView.Text = "Set";
            this.button_button_targetPos_set_toView.UseVisualStyleBackColor = true;
            this.button_button_targetPos_set_toView.Click += new System.EventHandler(this.button_button_targetPos_set_toView_Click);
            // 
            // button_targetPos_get_fromView
            // 
            this.button_targetPos_get_fromView.Location = new System.Drawing.Point(153, 22);
            this.button_targetPos_get_fromView.Name = "button_targetPos_get_fromView";
            this.button_targetPos_get_fromView.Size = new System.Drawing.Size(55, 48);
            this.button_targetPos_get_fromView.TabIndex = 11;
            this.button_targetPos_get_fromView.Text = "Get from view";
            this.button_targetPos_get_fromView.UseVisualStyleBackColor = true;
            this.button_targetPos_get_fromView.Click += new System.EventHandler(this.button_targetPos_get_fromView_Click);
            // 
            // textBox_target_offsetY
            // 
            this.textBox_target_offsetY.Location = new System.Drawing.Point(40, 49);
            this.textBox_target_offsetY.Name = "textBox_target_offsetY";
            this.textBox_target_offsetY.Size = new System.Drawing.Size(106, 21);
            this.textBox_target_offsetY.TabIndex = 10;
            this.textBox_target_offsetY.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y : ";
            // 
            // textBox_target_offsetX
            // 
            this.textBox_target_offsetX.Location = new System.Drawing.Point(40, 22);
            this.textBox_target_offsetX.Name = "textBox_target_offsetX";
            this.textBox_target_offsetX.Size = new System.Drawing.Size(106, 21);
            this.textBox_target_offsetX.TabIndex = 8;
            this.textBox_target_offsetX.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "X : ";
            // 
            // pictureBox_template_image
            // 
            this.pictureBox_template_image.Location = new System.Drawing.Point(7, 76);
            this.pictureBox_template_image.Name = "pictureBox_template_image";
            this.pictureBox_template_image.Size = new System.Drawing.Size(157, 124);
            this.pictureBox_template_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_template_image.TabIndex = 3;
            this.pictureBox_template_image.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_match_score);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.button_exec_tempate_match);
            this.groupBox2.Controls.Add(this.pictureBox_match_result);
            this.groupBox2.Location = new System.Drawing.Point(12, 317);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 235);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Template match";
            // 
            // textBox_match_score
            // 
            this.textBox_match_score.Location = new System.Drawing.Point(269, 22);
            this.textBox_match_score.Name = "textBox_match_score";
            this.textBox_match_score.ReadOnly = true;
            this.textBox_match_score.Size = new System.Drawing.Size(122, 21);
            this.textBox_match_score.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(212, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "Socre : ";
            // 
            // button_exec_tempate_match
            // 
            this.button_exec_tempate_match.Location = new System.Drawing.Point(7, 20);
            this.button_exec_tempate_match.Name = "button_exec_tempate_match";
            this.button_exec_tempate_match.Size = new System.Drawing.Size(107, 23);
            this.button_exec_tempate_match.TabIndex = 0;
            this.button_exec_tempate_match.Text = "Excute template match";
            this.button_exec_tempate_match.UseVisualStyleBackColor = true;
            this.button_exec_tempate_match.Click += new System.EventHandler(this.button_exec_tempate_match_Click);
            // 
            // pictureBox_match_result
            // 
            this.pictureBox_match_result.Location = new System.Drawing.Point(7, 49);
            this.pictureBox_match_result.Name = "pictureBox_match_result";
            this.pictureBox_match_result.Size = new System.Drawing.Size(316, 180);
            this.pictureBox_match_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_match_result.TabIndex = 4;
            this.pictureBox_match_result.TabStop = false;
            this.pictureBox_match_result.Click += new System.EventHandler(this.pictureBox_match_result_Click);
            // 
            // button_add_templatematch_process
            // 
            this.button_add_templatematch_process.Location = new System.Drawing.Point(11, 558);
            this.button_add_templatematch_process.Name = "button_add_templatematch_process";
            this.button_add_templatematch_process.Size = new System.Drawing.Size(398, 36);
            this.button_add_templatematch_process.TabIndex = 6;
            this.button_add_templatematch_process.Text = "Add Template match process";
            this.button_add_templatematch_process.UseVisualStyleBackColor = true;
            this.button_add_templatematch_process.Click += new System.EventHandler(this.button_add_templatematch_process_Click);
            // 
            // ImageProcess_TemplateMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 604);
            this.Controls.Add(this.button_add_templatematch_process);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ImageProcess_TemplateMatch";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageProcess_TemplateMatch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageProcess_TemplateMatch_FormClosing);
            this.Load += new System.EventHandler(this.ImageProcess_TemplateMatch_Load);
            this.VisibleChanged += new System.EventHandler(this.ImageProcess_TemplateMatch_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_template_image)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_match_result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_save_template_image;
        private System.Windows.Forms.TextBox textBox_template_image_path;
        private System.Windows.Forms.Button button_load_template_image;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox_template_image;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_exec_tempate_match;
        private System.Windows.Forms.TextBox textBox_match_score;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox_match_result;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_button_targetPos_set_toView;
        private System.Windows.Forms.Button button_targetPos_get_fromView;
        private System.Windows.Forms.TextBox textBox_target_offsetY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_target_offsetX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_add_templatematch_process;
        private System.Windows.Forms.TextBox textBox_threshold_socre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_insp_x;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_insp_y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_insp_h;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_insp_w;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.Button button_getfrom_insp_roi;
    }
}