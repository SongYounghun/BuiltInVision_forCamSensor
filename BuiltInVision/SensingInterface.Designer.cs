namespace BuiltInVision
{
    partial class SensingInterface
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
            this.propertyGrid_setting_sens_interface = new System.Windows.Forms.PropertyGrid();
            this.comboBox_sensor_index = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_clear_error_list = new System.Windows.Forms.Button();
            this.listView_error = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_status_proc = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_test_inputsig = new System.Windows.Forms.Button();
            this.button_show_vision_ui = new System.Windows.Forms.Button();
            this.button_stop_all_proc = new System.Windows.Forms.Button();
            this.button_start_all_proc = new System.Windows.Forms.Button();
            this.button_stop_process = new System.Windows.Forms.Button();
            this.button_start_proc = new System.Windows.Forms.Button();
            this.timer_monitor = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_error_img_path = new System.Windows.Forms.TextBox();
            this.button_err_img_path = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid_setting_sens_interface
            // 
            this.propertyGrid_setting_sens_interface.Location = new System.Drawing.Point(6, 20);
            this.propertyGrid_setting_sens_interface.Name = "propertyGrid_setting_sens_interface";
            this.propertyGrid_setting_sens_interface.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid_setting_sens_interface.Size = new System.Drawing.Size(284, 136);
            this.propertyGrid_setting_sens_interface.TabIndex = 0;
            this.propertyGrid_setting_sens_interface.ToolbarVisible = false;
            this.propertyGrid_setting_sens_interface.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_setting_sens_interface_PropertyValueChanged);
            // 
            // comboBox_sensor_index
            // 
            this.comboBox_sensor_index.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sensor_index.FormattingEnabled = true;
            this.comboBox_sensor_index.Location = new System.Drawing.Point(12, 12);
            this.comboBox_sensor_index.Name = "comboBox_sensor_index";
            this.comboBox_sensor_index.Size = new System.Drawing.Size(73, 20);
            this.comboBox_sensor_index.TabIndex = 1;
            this.comboBox_sensor_index.SelectedIndexChanged += new System.EventHandler(this.comboBox_sensor_index_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_err_img_path);
            this.groupBox1.Controls.Add(this.textBox_error_img_path);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.propertyGrid_setting_sens_interface);
            this.groupBox1.Location = new System.Drawing.Point(91, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 220);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IO Mapping / Setting";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_clear_error_list);
            this.groupBox2.Controls.Add(this.listView_error);
            this.groupBox2.Controls.Add(this.label_status_proc);
            this.groupBox2.Location = new System.Drawing.Point(91, 257);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 188);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // button_clear_error_list
            // 
            this.button_clear_error_list.Location = new System.Drawing.Point(222, 159);
            this.button_clear_error_list.Name = "button_clear_error_list";
            this.button_clear_error_list.Size = new System.Drawing.Size(75, 23);
            this.button_clear_error_list.TabIndex = 0;
            this.button_clear_error_list.Text = "Clear";
            this.button_clear_error_list.UseVisualStyleBackColor = true;
            this.button_clear_error_list.Click += new System.EventHandler(this.button_clear_error_list_Click);
            // 
            // listView_error
            // 
            this.listView_error.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_error.GridLines = true;
            this.listView_error.Location = new System.Drawing.Point(6, 52);
            this.listView_error.MultiSelect = false;
            this.listView_error.Name = "listView_error";
            this.listView_error.Size = new System.Drawing.Size(291, 101);
            this.listView_error.TabIndex = 1;
            this.listView_error.UseCompatibleStateImageBehavior = false;
            this.listView_error.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = " Errors";
            this.columnHeader1.Width = 250;
            // 
            // label_status_proc
            // 
            this.label_status_proc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_status_proc.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_status_proc.Location = new System.Drawing.Point(6, 17);
            this.label_status_proc.Name = "label_status_proc";
            this.label_status_proc.Size = new System.Drawing.Size(291, 32);
            this.label_status_proc.TabIndex = 0;
            this.label_status_proc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_test_inputsig);
            this.groupBox3.Controls.Add(this.button_show_vision_ui);
            this.groupBox3.Controls.Add(this.button_stop_all_proc);
            this.groupBox3.Controls.Add(this.button_start_all_proc);
            this.groupBox3.Controls.Add(this.button_stop_process);
            this.groupBox3.Controls.Add(this.button_start_proc);
            this.groupBox3.Location = new System.Drawing.Point(91, 451);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 101);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // button_test_inputsig
            // 
            this.button_test_inputsig.Location = new System.Drawing.Point(193, 78);
            this.button_test_inputsig.Name = "button_test_inputsig";
            this.button_test_inputsig.Size = new System.Drawing.Size(110, 23);
            this.button_test_inputsig.TabIndex = 5;
            this.button_test_inputsig.Text = "Test input signal";
            this.button_test_inputsig.UseVisualStyleBackColor = true;
            this.button_test_inputsig.Click += new System.EventHandler(this.button_test_inputsig_Click);
            // 
            // button_show_vision_ui
            // 
            this.button_show_vision_ui.Location = new System.Drawing.Point(251, 20);
            this.button_show_vision_ui.Name = "button_show_vision_ui";
            this.button_show_vision_ui.Size = new System.Drawing.Size(46, 52);
            this.button_show_vision_ui.TabIndex = 4;
            this.button_show_vision_ui.Text = "Show vision UI";
            this.button_show_vision_ui.UseVisualStyleBackColor = true;
            this.button_show_vision_ui.Click += new System.EventHandler(this.button_show_vision_ui_Click);
            // 
            // button_stop_all_proc
            // 
            this.button_stop_all_proc.Location = new System.Drawing.Point(130, 49);
            this.button_stop_all_proc.Name = "button_stop_all_proc";
            this.button_stop_all_proc.Size = new System.Drawing.Size(115, 23);
            this.button_stop_all_proc.TabIndex = 3;
            this.button_stop_all_proc.Text = "ALL Stop process";
            this.button_stop_all_proc.UseVisualStyleBackColor = true;
            this.button_stop_all_proc.Click += new System.EventHandler(this.button_stop_all_proc_Click);
            // 
            // button_start_all_proc
            // 
            this.button_start_all_proc.Location = new System.Drawing.Point(6, 49);
            this.button_start_all_proc.Name = "button_start_all_proc";
            this.button_start_all_proc.Size = new System.Drawing.Size(115, 23);
            this.button_start_all_proc.TabIndex = 2;
            this.button_start_all_proc.Text = "ALL Start process";
            this.button_start_all_proc.UseVisualStyleBackColor = true;
            this.button_start_all_proc.Click += new System.EventHandler(this.button_start_all_proc_Click);
            // 
            // button_stop_process
            // 
            this.button_stop_process.Location = new System.Drawing.Point(130, 20);
            this.button_stop_process.Name = "button_stop_process";
            this.button_stop_process.Size = new System.Drawing.Size(115, 23);
            this.button_stop_process.TabIndex = 1;
            this.button_stop_process.Text = "Stop process";
            this.button_stop_process.UseVisualStyleBackColor = true;
            this.button_stop_process.Click += new System.EventHandler(this.button_stop_process_Click);
            // 
            // button_start_proc
            // 
            this.button_start_proc.Location = new System.Drawing.Point(6, 20);
            this.button_start_proc.Name = "button_start_proc";
            this.button_start_proc.Size = new System.Drawing.Size(115, 23);
            this.button_start_proc.TabIndex = 0;
            this.button_start_proc.Text = "Start process";
            this.button_start_proc.UseVisualStyleBackColor = true;
            this.button_start_proc.Click += new System.EventHandler(this.button_start_proc_Click);
            // 
            // timer_monitor
            // 
            this.timer_monitor.Enabled = true;
            this.timer_monitor.Tick += new System.EventHandler(this.timer_monitor_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Erorr image path : ";
            // 
            // textBox_error_img_path
            // 
            this.textBox_error_img_path.Location = new System.Drawing.Point(36, 188);
            this.textBox_error_img_path.Name = "textBox_error_img_path";
            this.textBox_error_img_path.ReadOnly = true;
            this.textBox_error_img_path.Size = new System.Drawing.Size(222, 21);
            this.textBox_error_img_path.TabIndex = 2;
            // 
            // button_err_img_path
            // 
            this.button_err_img_path.Location = new System.Drawing.Point(264, 186);
            this.button_err_img_path.Name = "button_err_img_path";
            this.button_err_img_path.Size = new System.Drawing.Size(26, 23);
            this.button_err_img_path.TabIndex = 3;
            this.button_err_img_path.Text = "...";
            this.button_err_img_path.UseVisualStyleBackColor = true;
            this.button_err_img_path.Click += new System.EventHandler(this.button_err_img_path_Click);
            // 
            // SensingInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 558);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.comboBox_sensor_index);
            this.Controls.Add(this.groupBox1);
            this.Name = "SensingInterface";
            this.Text = "Sensing interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SensingInterface_FormClosing);
            this.Load += new System.EventHandler(this.SensingInterface_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid_setting_sens_interface;
        private System.Windows.Forms.ComboBox comboBox_sensor_index;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_stop_all_proc;
        private System.Windows.Forms.Button button_start_all_proc;
        private System.Windows.Forms.Button button_stop_process;
        private System.Windows.Forms.Button button_start_proc;
        private System.Windows.Forms.Label label_status_proc;
        private System.Windows.Forms.Timer timer_monitor;
        private System.Windows.Forms.Button button_show_vision_ui;
        private System.Windows.Forms.ListView listView_error;
        private System.Windows.Forms.Button button_clear_error_list;
        private System.Windows.Forms.Button button_test_inputsig;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button_err_img_path;
        private System.Windows.Forms.TextBox textBox_error_img_path;
        private System.Windows.Forms.Label label1;
    }
}